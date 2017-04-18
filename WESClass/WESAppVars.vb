Imports System.Net.Mail
Imports System.Configuration
Imports System.Net
Imports System.Text.RegularExpressions

Namespace WESAppVars

    Public Class WESTestValues
        Public Shared PersonalTraining As Integer = 100
    End Class

    Public Class WebsiteValues
        Public Shared WebsiteAddress As String = ConfigurationManager.AppSettings("Website")

        Public Shared FromAddress As String = ConfigurationManager.AppSettings("FromAddress")
        Public Shared FromName As String = ConfigurationManager.AppSettings("FromName")
    End Class

    Public Class ErrorItems
        Public Shared ErrorFromAddress As String = ConfigurationManager.AppSettings("ErrorFromAddress")
        Public Shared ErrorFromName As String = ConfigurationManager.AppSettings("ErrorFromName")
        Public Shared ErrorToAddress As String = ConfigurationManager.AppSettings("ErrorToAddress")
    End Class

    Public Class WESSQLConnection
        Public Shared connString As String = ConfigurationManager.ConnectionStrings("WESConnection").ConnectionString
    End Class

    Public Class ErrorLogging

        Public Sub EmailError(ByVal reqSubject As String, ByVal reqBody As String, ByVal reqE As Exception, ByVal reqMethod As String, Optional ByVal reqClassName As String = "SC.AppVars")
            reqSubject = "WES: " + reqSubject
            ' so the log files do not send emails 2x for the same thing, before addition this could happen in parent procs which called several children procs
            reqBody += Chr(13) + Chr(13) + "===================================" + Chr(13) + Chr(13)
            'reqBody += "Username Who Had Error: " + zVal_DbaseWhoCreatedUserName + Chr(13)
            'reqBody += "UserId Who Had Error: " + zVal_DbaseWhoCreatedId.ToString() + Chr(13)
            reqBody += "Class where Error occurred: " + reqClassName + Chr(13) + Chr(13)
            reqBody += "Method where Error occurred: " + reqMethod + Chr(13) + Chr(13)
            reqBody += Chr(13) + "===================================" + Chr(13) + Chr(13)
            Email.SendEmailError(ErrorItems.ErrorToAddress, ErrorItems.ErrorFromAddress, ErrorItems.ErrorFromName, reqSubject, reqBody, reqE)

        End Sub

    End Class

    Public Class Email
        Public Overloads Shared Sub SendEmailError(ByVal reqSendTo As String, ByVal reqSendFromEmail As String, ByVal reqSendFromEmailName As String, ByVal reqSubject As String, ByVal reqMessageBody As String, ByVal e As Exception)

            Dim locErrorMessage As String
            Try
                reqMessageBody = reqMessageBody & "Exception Message: " & e.Message & e.StackTrace
                SendEmail(reqSendTo,
                 reqSendFromEmail,
                 reqSendFromEmailName,
                 reqSubject,
                 reqMessageBody,
                 "",
                 "",
                 "",
                 False)
            Catch ex As Exception
                locErrorMessage = ex.Message
            End Try
        End Sub

        Public Shared Sub SendEmail(ByVal reqEmailAddressTo As String,
                 ByVal reqEmailAddressFrom As String,
                 ByVal reqEmailAddressFromDisplayName As String,
                 ByVal reqEmailMessage As String,
                 ByVal reqEmailBody As String,
                 Optional ByVal reqAttachments As String = "",
                 Optional ByVal reqEmailAddressCC As String = "",
                 Optional ByVal reqEmailAddressBCC As String = "",
                 Optional ByVal reqEmailHtml As Boolean = True)
            Dim i As Integer = 0

            Try
                If NullToString(reqEmailAddressTo) = "" Or
                  NullToString(reqEmailAddressFrom) = "" Or
                  NullToString(reqEmailAddressFromDisplayName) = "" Or
                  NullToString(reqEmailMessage) = "" Or
                  NullToString(reqEmailBody) = "" Then
                    Throw New Exception("SendEmail: Missing required fields TO or BODY or Message")
                Else
                    Using locNewMessage As New MailMessage()
                        ' TO
                        Dim locReqToEmailAddresses() As String = reqEmailAddressTo.Split(";")
                        If locReqToEmailAddresses.Length <> 1 Then
                            For i = 0 To locReqToEmailAddresses.Length - 1
                                If IsEmailValid(locReqToEmailAddresses(i)) Then
                                    locNewMessage.To.Add(New MailAddress(locReqToEmailAddresses(i)))
                                Else
                                    Throw New Exception("SendEmail: One or more TO email addresses (" + locReqToEmailAddresses(i) + ") are mal-formed.")
                                End If
                            Next
                        Else
                            If IsEmailValid(reqEmailAddressTo) Then
                                locNewMessage.To.Add(New MailAddress(reqEmailAddressTo))
                            Else
                                Throw New Exception("SendEmail: One or more TO email addresses (" + reqEmailAddressTo + ") are mal-formed.")
                            End If
                        End If  ' end email address validation 
                        ' FROM / SUBJECT / BODY
                        locNewMessage.From = New MailAddress(reqEmailAddressFrom, reqEmailAddressFromDisplayName)
                        locNewMessage.Subject = reqEmailMessage
                        locNewMessage.Body = reqEmailBody
                        ' HTML
                        locNewMessage.IsBodyHtml = reqEmailHtml
                        ' ATTACHMENTS
                        Try
                            If (reqAttachments <> "") Then
                                Dim locReqAttachments() As String = reqAttachments.Split(";")
                                If locReqAttachments.Length <> 1 Then
                                    For i = 0 To locReqAttachments.Length - 1
                                        locNewMessage.Attachments.Add(New Attachment(locReqAttachments(i)))
                                    Next
                                Else
                                    locNewMessage.Attachments.Add(New Attachment(reqAttachments))
                                End If
                            End If ' end attachments
                        Catch ex As Exception
                            Throw New Exception("SendEmail: Problem Adding attachments.")
                        End Try
                        ' CC
                        Try
                            If reqEmailAddressCC <> "" Then
                                Dim locReqCCEmailAddresses() As String = reqEmailAddressCC.Split(";")
                                If locReqCCEmailAddresses.Length <> 1 Then
                                    For i = 0 To locReqCCEmailAddresses.Length - 1
                                        If IsEmailValid(locReqCCEmailAddresses(i)) Then
                                            locNewMessage.CC.Add(New MailAddress(locReqCCEmailAddresses(i)))
                                        Else
                                            Throw New Exception("SendEmail: One or more CC email addresses (" + locReqCCEmailAddresses(i) + ") are mal-formed.")
                                        End If
                                    Next
                                Else
                                    If IsEmailValid(reqEmailAddressCC) Then
                                        locNewMessage.To.Add(New MailAddress(reqEmailAddressCC))
                                    Else
                                        Throw New Exception("SendEmail: One or more CC email addresses (" + reqEmailAddressCC + ") are mal-formed.")
                                    End If
                                End If
                            End If ' end CC email address validation 
                        Catch exNew2 As Exception
                            Throw New Exception("SendEmail: Problem with Carbon-Copy email addresses." + exNew2.Message)
                        End Try
                        ' BCC
                        Try
                            If reqEmailAddressBCC <> "" Then
                                Dim locReqBCCEmailAddresses() As String = reqEmailAddressBCC.Split(";")
                                If locReqBCCEmailAddresses.Length <> 1 Then
                                    For i = 0 To locReqBCCEmailAddresses.Length - 1
                                        If IsEmailValid(reqEmailAddressBCC) Then
                                            locNewMessage.Bcc.Add(New MailAddress(locReqBCCEmailAddresses(i)))
                                        Else
                                            Throw New Exception("SendEmail: One or more BCC email addresses (" + locReqBCCEmailAddresses(i) + ") are mal-formed.")
                                        End If
                                    Next
                                Else
                                    If IsEmailValid(reqEmailAddressBCC) Then
                                        locNewMessage.To.Add(New MailAddress(reqEmailAddressBCC))
                                    Else
                                        Throw New Exception("SendEmail: One or more BCC email addresses (" + reqEmailAddressBCC + ") are mal-formed.")
                                    End If
                                End If
                            End If ' end BCC email address validation 
                        Catch exNew As Exception
                            Throw New Exception("SendEmail: Problem with Blind Carbon-Copy email addresses." + exNew.Message)
                        End Try
                        ' SEND EMAIL
                        Dim locNewMailClient2 As New SmtpClient()
                        locNewMailClient2.Host = "smtpout.secureserver.net"
                        locNewMailClient2.Port = 80
                        locNewMailClient2.EnableSsl = False

                        Dim authInfo As New NetworkCredential("info@worldexercisesystem.com", "Jacque0412")

                        locNewMailClient2.UseDefaultCredentials = False
                        locNewMailClient2.Credentials = authInfo

                        '-- Send Message
                        locNewMailClient2.Send(locNewMessage)
                    End Using
                End If  ' end required fields
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Public Shared Function IsEmailValid(ByVal reqEmail As String) As Boolean
            Dim locRegExpressionString As String
            Dim locRegExpression As Regex

            Try
                reqEmail = NullToString(reqEmail)
                locRegExpressionString = "^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3} \.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)"
                'locRegExpressionString = "^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" + "\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + ".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                locRegExpression = New Regex(locRegExpressionString)
                If locRegExpression.IsMatch(reqEmail) Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message + ex.InnerException.ToString)
            End Try
        End Function

        Public Shared Function NullToString(ByVal reqObject As Object) As String
            Try
                If reqObject Is Nothing Then
                    Return ""
                ElseIf reqObject Is System.DBNull.Value Then
                    Return ""
                Else
                    Return reqObject.ToString
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message + ex.InnerException.ToString)
            End Try
        End Function
    End Class
End Namespace

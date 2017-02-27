Imports System.IO
Imports System.Reflection
Imports System.Security.Cryptography
Imports System.Text
Imports WESClass.WESAppVars
Imports WESClass.WESData
Imports WESClass.WESObjects
Imports WESClass.WESSessions

Namespace WESSecurity

    Public Class ClsLoginSecurity

        Public Shared Function Encrypt(ByVal originalString As String, ByVal reqString As String) As String
            Try
                Dim bytes As Byte() = ASCIIEncoding.ASCII.GetBytes(reqString)

                If Not [String].IsNullOrEmpty(originalString) Then
                    Dim cryptoProvider As New DESCryptoServiceProvider()
                    Dim memoryStream As New MemoryStream()
                    Dim cryptoStream As New CryptoStream(memoryStream, cryptoProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write)
                    Dim writer As New StreamWriter(cryptoStream)
                    writer.Write(originalString)
                    writer.Flush()
                    cryptoStream.FlushFinalBlock()
                    writer.Flush()
                    Return Convert.ToBase64String(memoryStream.GetBuffer(), 0, CInt(memoryStream.Length))
                Else
                    Return originalString
                End If
            Catch ex As Exception
                Dim locErrorHandler As New ErrorLogging
                Dim locSF As StackFrame = New StackFrame()
                Dim locMB As MethodBase = locSF.GetMethod()
                Dim locClass As System.Type = locMB.DeclaringType

                Return ""
            End Try
        End Function


        Public Shared Function Decrypt(ByVal cryptedString As String, ByVal reqString As String) As String
            Try
                Dim bytes As Byte() = ASCIIEncoding.ASCII.GetBytes(reqString)

                If Not [String].IsNullOrEmpty(cryptedString) Then
                    Dim cryptoProvider As New DESCryptoServiceProvider()
                    Dim memoryStream As New MemoryStream(Convert.FromBase64String(cryptedString))
                    Dim cryptoStream As New CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read)
                    Dim reader As New StreamReader(cryptoStream)
                    Return reader.ReadToEnd()
                Else
                    Return cryptedString
                End If
            Catch ex As Exception
                Dim locErrorHandler As New ErrorLogging
                Dim locSF As StackFrame = New StackFrame()
                Dim locMB As MethodBase = locSF.GetMethod()
                Dim locClass As System.Type = locMB.DeclaringType

                Return ""
            End Try
        End Function

        Public Function DecryptCookie(ByVal reqPassword As String) As String
            Return Decrypt(reqPassword, "WESEncry")
        End Function

        Public Function EncryptPassword(ByVal reqPassword As String) As String
            Return Encrypt(reqPassword, "WEST2016")
        End Function

        Public Function EncryptCookie(ByVal reqPassword As String) As String
            Return Encrypt(reqPassword, "WESEncry")
        End Function

        Public Function ProcessCredentials(ByVal reqUserName As String, ByVal reqPassword As String, ByRef reqMessage As String) As Boolean
            Dim locSuccess As Boolean = False
            Dim locCurrentLogin As New ClsLoginUser_Session

            Try
                locCurrentLogin.TraineeID = 0
                locCurrentLogin.EmailStatus = 0
                locCurrentLogin.AdminStatus = 0

                Dim locLoginSearch As New WESTrainee_Search
                Dim locLoginSearchInput As New WESTrainee_Search.ClsInputParamsLogin
                Dim locLoginSearchResult As New ClsDataSearchReturn

                locLoginSearchInput.reqUsername = reqUserName
                locLoginSearchInput.reqPassword = EncryptPassword(reqPassword)
                locLoginSearch.zValInput = locLoginSearchInput

                If locLoginSearch.zProSearch(locLoginSearchResult) Then
                    If locLoginSearchResult.zValReturnList.Count > 0 Then
                        Dim locTrainee As ClsTrainee = locLoginSearchResult.zValReturnList(0)

                        locCurrentLogin.TraineeID = locTrainee.TraineeID
                        locCurrentLogin.EmailStatus = locTrainee.EmailVerified
                        locCurrentLogin.AdminStatus = locTrainee.AdminUser
                        locCurrentLogin.TestAvailable = locTrainee.PTEAvailable
                        locCurrentLogin.TraineeEmail = locTrainee.EmailAddress
                        locCurrentLogin.TraineeGUID = locTrainee.TraineeGUID

                        locSuccess = True
                        reqMessage = "Login Successful"
                    Else
                        locSuccess = False
                        reqMessage = "Invalid username or password"
                    End If
                Else
                    locSuccess = False
                    reqMessage = "Please try again"
                End If
            Catch ex As Exception
                locSuccess = False
                reqMessage = reqMessage

                Dim locErrorHandler As New ErrorLogging
                Dim locSF As StackFrame = New StackFrame()
                Dim locMB As MethodBase = locSF.GetMethod()
                Dim locClass As System.Type = locMB.DeclaringType
                locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
            End Try

            Return locSuccess
        End Function

        Public Function ProcessTraineeID(ByVal reqTraineeID As Long, ByRef reqMessage As String) As Boolean
            Dim locSuccess As Boolean = False
            Dim locCurrentLogin As New ClsLoginUser_Session

            Try
                locCurrentLogin.TraineeID = 0
                locCurrentLogin.EmailStatus = 0
                locCurrentLogin.AdminStatus = 0

                Dim locLoginSearch As New WESTrainee_Search
                Dim locLoginSearchInput As New WESTrainee_Search.ClsInputParamsTrainee
                Dim locLoginSearchResult As New ClsDataSearchReturn

                locLoginSearchInput.reqTraineeID = reqTraineeID
                locLoginSearch.zValInput = locLoginSearchInput

                If locLoginSearch.zProSearch(locLoginSearchResult) Then
                    If locLoginSearchResult.zValReturnList.Count > 0 Then
                        Dim locTrainee As ClsTrainee = locLoginSearchResult.zValReturnList(0)

                        locCurrentLogin.TraineeID = locTrainee.TraineeID
                        locCurrentLogin.EmailStatus = locTrainee.EmailVerified
                        locCurrentLogin.AdminStatus = locTrainee.AdminUser
                        locCurrentLogin.TestAvailable = locTrainee.PTEAvailable

                        locSuccess = True
                        reqMessage = "Login Successful"
                    Else
                        locSuccess = False
                        reqMessage = "Invalid username or password"
                    End If
                Else
                    locSuccess = False
                    reqMessage = "Please try again"
                End If
            Catch ex As Exception
                locSuccess = False
                reqMessage = reqMessage

                Dim locErrorHandler As New ErrorLogging
                Dim locSF As StackFrame = New StackFrame()
                Dim locMB As MethodBase = locSF.GetMethod()
                Dim locClass As System.Type = locMB.DeclaringType
                locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
            End Try

            Return locSuccess
        End Function

    End Class


End Namespace

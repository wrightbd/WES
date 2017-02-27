Imports System.Reflection
Imports System.Web
Imports WESClass.WESAppVars
Imports WESClass.WESObjects


Namespace WESSessions

    Public Class ClsLoginUser_Session

        Public Property TraineeID() As Long
            Get
                If Not IsNothing(HttpContext.Current) AndAlso Not IsNothing(HttpContext.Current.Session) Then
                    If HttpContext.Current.Session("ClsLoginUser_Session_TraineeID") Is Nothing OrElse IsDBNull(HttpContext.Current.Session("ClsLoginUser_Session_TraineeID")) Then HttpContext.Current.Session("ClsLoginUser_Session_TraineeID") = 0

                    Return CType(HttpContext.Current.Session("ClsLoginUser_Session_TraineeID"), Long)
                Else
                    Return 0
                End If
            End Get
            Set(ByVal value As Long)
                Try
                    If IsDBNull(HttpContext.Current.Session("ClsLoginUser_Session_TraineeID")) Then
                        If IsNumeric(HttpContext.Current.Session("ClsLoginUser_Session_TraineeID")) Then
                            If HttpContext.Current.Session("ClsLoginUser_Session_TraineeID") > 0 Then
                                If value = HttpContext.Current.Session("ClsLoginUser_Session_TraineeID") Then
                                    Throw New ArgumentException("InternetUserIdError(001a)MAJOR_ERROR: Trying to assign InternetUserId to the same value. (VALUE=" + value.ToString + ")")
                                Else
                                    Throw New ArgumentException("InternetUserIdError(001b)MAJOR_ERROR: Trying to assign InternetUserId Which Already Exists to the different value. (VALUE=" + value.ToString + ") and (SCInternetAccount_IntUserProfile_InternetUserId=" + HttpContext.Current.Session("SCInternetAccount_IntUserProfile_InternetUserId").ToString() + ")")
                                End If
                            End If
                        End If
                    End If
                Catch ex As Exception
                    Dim locErrorHandler As New ErrorLogging
                    Dim locSF As StackFrame = New StackFrame()
                    Dim locMB As MethodBase = locSF.GetMethod()
                    Dim locClass As System.Type = locMB.DeclaringType
                    locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
                Finally
                    HttpContext.Current.Session("ClsLoginUser_Session_TraineeID") = value
                End Try
            End Set
        End Property

        Public Property AdminStatus As Integer
            Get
                If Not IsNothing(HttpContext.Current) Then
                    If HttpContext.Current.Session("ClsLoginUser_Session_AdminStatus") Is Nothing OrElse IsDBNull(HttpContext.Current.Session("ClsLoginUser_Session_AdminStatus")) Then HttpContext.Current.Session("ClsLoginUser_Session_AdminStatus") = 0

                    Return CType(HttpContext.Current.Session("ClsLoginUser_Session_AdminStatus"), Integer)
                Else
                    Return 0
                End If
            End Get
            Set(ByVal value As Integer)
                'WHEN NOTHING IS SUBMITTED(SO WE CAN NOT CHECK FOR IT;) IT  IS THE SAME AS 0
                HttpContext.Current.Session("ClsLoginUser_Session_AdminStatus") = value
            End Set
        End Property

        Public Property TestAvailable As Integer
            Get
                If Not IsNothing(HttpContext.Current) Then
                    If HttpContext.Current.Session("ClsLoginUser_Session_TestAvailable") Is Nothing OrElse IsDBNull(HttpContext.Current.Session("ClsLoginUser_Session_TestAvailable")) Then HttpContext.Current.Session("ClsLoginUser_Session_TestAvailable") = 0

                    Return CType(HttpContext.Current.Session("ClsLoginUser_Session_TestAvailable"), Integer)
                Else
                    Return 0
                End If
            End Get
            Set(ByVal value As Integer)
                'WHEN NOTHING IS SUBMITTED(SO WE CAN NOT CHECK FOR IT;) IT  IS THE SAME AS 0
                HttpContext.Current.Session("ClsLoginUser_Session_TestAvailable") = value
            End Set
        End Property

        Public Property EmailStatus As Integer
            Get
                If Not IsNothing(HttpContext.Current) Then
                    If HttpContext.Current.Session("ClsLoginUser_Session_EmailStatus") Is Nothing OrElse IsDBNull(HttpContext.Current.Session("ClsLoginUser_Session_EmailStatus")) Then HttpContext.Current.Session("ClsLoginUser_Session_EmailStatus") = 0

                    Return CType(HttpContext.Current.Session("ClsLoginUser_Session_EmailStatus"), Integer)
                Else
                    Return 0
                End If
            End Get
            Set(ByVal value As Integer)
                'WHEN NOTHING IS SUBMITTED(SO WE CAN NOT CHECK FOR IT;) IT  IS THE SAME AS 0
                HttpContext.Current.Session("ClsLoginUser_Session_EmailStatus") = value
            End Set
        End Property

        Public Property TraineeEmail As String
            Get
                If Not IsNothing(HttpContext.Current) Then
                    If HttpContext.Current.Session("ClsLoginUser_Session_TraineeEmail") Is Nothing OrElse IsDBNull(HttpContext.Current.Session("ClsLoginUser_Session_TraineeEmail")) Then HttpContext.Current.Session("ClsLoginUser_Session_TraineeEmail") = ""

                    Return CType(HttpContext.Current.Session("ClsLoginUser_Session_TraineeEmail"), String)
                Else
                    Return ""
                End If
            End Get
            Set(ByVal value As String)
                If value Is Nothing OrElse value Is System.DBNull.Value Then
                    HttpContext.Current.Session("ClsLoginUser_Session_TraineeEmail") = ""
                Else
                    HttpContext.Current.Session("ClsLoginUser_Session_TraineeEmail") = value
                End If
            End Set
        End Property

        Public Property TraineeGUID As String
            Get
                If Not IsNothing(HttpContext.Current) Then
                    If HttpContext.Current.Session("ClsLoginUser_Session_TraineeGUID") Is Nothing OrElse IsDBNull(HttpContext.Current.Session("ClsLoginUser_Session_TraineeGUID")) Then HttpContext.Current.Session("ClsLoginUser_Session_TraineeGUID") = ""

                    Return CType(HttpContext.Current.Session("ClsLoginUser_Session_TraineeGUID"), String)
                Else
                    Return ""
                End If
            End Get
            Set(ByVal value As String)
                If value Is Nothing OrElse value Is System.DBNull.Value Then
                    HttpContext.Current.Session("ClsLoginUser_Session_TraineeGUID") = ""
                Else
                    HttpContext.Current.Session("ClsLoginUser_Session_TraineeGUID") = value
                End If
            End Set
        End Property

        Public Property TraineeName As String
            Get
                If Not IsNothing(HttpContext.Current) Then
                    If HttpContext.Current.Session("ClsLoginUser_Session_TraineeName") Is Nothing OrElse IsDBNull(HttpContext.Current.Session("ClsLoginUser_Session_TraineeName")) Then HttpContext.Current.Session("ClsLoginUser_Session_TraineeName") = ""

                    Return CType(HttpContext.Current.Session("ClsLoginUser_Session_TraineeName"), String)
                Else
                    Return ""
                End If
            End Get
            Set(ByVal value As String)
                If value Is Nothing OrElse value Is System.DBNull.Value Then
                    HttpContext.Current.Session("ClsLoginUser_Session_TraineeName") = ""
                Else
                    HttpContext.Current.Session("ClsLoginUser_Session_TraineeName") = value
                End If
            End Set
        End Property

    End Class

End Namespace

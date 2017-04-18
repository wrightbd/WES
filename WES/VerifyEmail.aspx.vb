Imports System.Reflection
Imports WESClass.WESAppVars
Imports WESClass.WESData
Imports WESClass.WESObjects

Public Class VerifyEmail
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pnlSuccess.Visible = False
            pnlError.Visible = False
            lblError.Text = ""

            Dim locGUID As String = ""

            If Request.QueryString.AllKeys.Contains("p1") Then
                locGUID = Request.QueryString("p1")
            End If

            VerifyEmail(locGUID)
        End If
    End Sub

    Private Sub VerifyEmail(ByVal reqGUID As String)
        Dim locSuccess As Boolean = False
        Dim locMessage As String = "There was an error verifying your email. Please retry."

        Try
            If reqGUID <> "" Then
                Dim locVerify As New WESTrainee_Save
                Dim locVerifyInput As New WESTrainee_Save.ClsInputParamsVerifyEmail
                Dim locVerifyResult As New ClsDataSearchReturnSave

                locVerifyInput.reqRowGuid = reqGUID
                locVerify.zValInput = locVerifyInput

                If locVerify.zProSearch(locVerifyResult) Then
                    If locVerifyResult.zValUpdateStatus = 1 Then
                        locSuccess = True
                    Else
                        locMessage = locVerifyResult.zValUpdateMessage
                    End If
                End If
            Else
                locMessage = "No Account supplied. Please verify the address used."
            End If

            pnlSuccess.Visible = locSuccess
            pnlError.Visible = Not locSuccess
            lblError.Text = locMessage
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub

End Class
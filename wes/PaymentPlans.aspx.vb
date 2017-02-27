Imports WESClass.WESSessions

Public Class PaymentPlans
    Inherits System.Web.UI.Page

    ReadOnly zVarLoginSession As New ClsLoginUser_Session

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If zVarLoginSession.TraineeID > 0 Then
            linkGetStarted.NavigateUrl = "~/ChoosePlan.aspx"
        Else
            linkGetStarted.NavigateUrl = "~/CreateAccount.aspx"
        End If
    End Sub

End Class
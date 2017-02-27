
Imports WESClass.WESSessions

Public Class _Default
    Inherits System.Web.UI.Page

    ReadOnly zVarLoginSession As New ClsLoginUser_Session

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        pnlMember.Visible = zVarLoginSession.TraineeID > 0 AndAlso zVarLoginSession.EmailStatus <> 0

        pnlTest.Visible = zVarLoginSession.TraineeID > 0 AndAlso zVarLoginSession.TestAvailable = 1
    End Sub

End Class
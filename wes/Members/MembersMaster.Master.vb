Imports WESClass.WESSessions

Public Class MembersMaster
    Inherits System.Web.UI.MasterPage

    ReadOnly zVarLoginSession As New ClsLoginUser_Session

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If zVarLoginSession.TraineeID <= 0 Then
            Response.Redirect("~/Login.aspx")
        End If

        If Not IsPostBack Then
            liManage.Visible = zVarLoginSession.TraineeID > 0 AndAlso zVarLoginSession.EmailStatus <> 0

            liMember.Visible = zVarLoginSession.TraineeID > 0 AndAlso zVarLoginSession.EmailStatus <> 0

            liAdmin.Visible = zVarLoginSession.TraineeID > 0 AndAlso zVarLoginSession.EmailStatus <> 0 AndAlso zVarLoginSession.AdminStatus <> 0

            itemTest.Visible = zVarLoginSession.TraineeID > 0 AndAlso zVarLoginSession.TestAvailable = 1

            liSignIn.Visible = zVarLoginSession.TraineeID = 0
            liSignOut.Visible = zVarLoginSession.TraineeID > 0
            linkCreateAccount.Visible = zVarLoginSession.TraineeID = 0
        End If
    End Sub

    Public Sub RefreshMenu()
        liManage.Visible = zVarLoginSession.TraineeID > 0 AndAlso zVarLoginSession.EmailStatus <> 0

        liMember.Visible = zVarLoginSession.TraineeID > 0 AndAlso zVarLoginSession.EmailStatus <> 0

        liAdmin.Visible = zVarLoginSession.TraineeID > 0 AndAlso zVarLoginSession.EmailStatus <> 0 AndAlso zVarLoginSession.AdminStatus <> 0

        itemTest.Visible = zVarLoginSession.TraineeID > 0 AndAlso zVarLoginSession.TestAvailable = 1

        liSignIn.Visible = zVarLoginSession.TraineeID = 0
        liSignOut.Visible = zVarLoginSession.TraineeID > 0
        linkCreateAccount.Visible = zVarLoginSession.TraineeID = 0
    End Sub

End Class
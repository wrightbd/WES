Imports System.Reflection
Imports WESClass.WESAppVars
Imports WESClass.WESData
Imports WESClass.WESObjects
Imports WESClass.WESSessions

Public Class Videos
    Inherits System.Web.UI.Page

    ReadOnly zVarLoginSession As New ClsLoginUser_Session

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ClearForm()
        End If
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Try
            Dim locSave As New WESTraineeVideo_Save
            Dim locSaveInput As New WESTraineeVideo_Save.ClsInputParamsSave
            Dim locSaveResult As New ClsDataSearchReturnSave

            locSaveInput.reqTraineeID = zVarLoginSession.TraineeID
            locSaveInput.reqYouTubeID = txtVideoCode.Text
            locSave.zValInput = locSaveInput

            If locSave.zProSearch(locSaveResult) Then
                If locSaveResult.zValUpdateStatus = 1 Then
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "SaveVideoClientScript", "alert('Your video has been submitted. We will contact you after reviewing your submission.');", True)

                    ClearForm()
                Else
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "ErrorVideoClientScript", "alert('Error saving submission: " + locSaveResult.zValUpdateMessage + "');", True)
                End If
            Else
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "ErrorVideoClientScript", "alert('Error saving submission. Please try again.');", True)
            End If
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub

    Private Sub ClearForm()
        txtVideoCode.Text = ""
    End Sub
End Class
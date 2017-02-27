Imports System.Reflection
Imports WESClass.WESAppVars
Imports WESClass.WESData
Imports WESClass.WESObjects

Public Class WESMediaCenter
    Inherits System.Web.UI.Page

    Private Property zVarCategories As New List(Of ClsMediaCategory)
    Private Property zVarMedia As New List(Of ClsMediaItem)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadCategories()
            LoadMedia()
            ClearAddEditForm()
        Else
            If Not IsNothing(ViewState("WESMEDIA")) Then
                zVarMedia = ViewState("WESMEDIA")
            End If

            If Not IsNothing(ViewState("WESCATEGORY")) Then
                zVarCategories = ViewState("WESCATEGORY")
            End If
        End If
    End Sub

    Protected Sub btnAddMediaItem_Click(sender As Object, e As EventArgs) Handles btnAddMediaItem.Click
        Try
            ClearAddEditForm()
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "AddClientScript", "javascript: OpenDialog();", True)
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub

    Protected Sub btnRefreshGrid_Click(sender As Object, e As EventArgs) Handles btnRefreshGrid.Click
        linqMedia.DataBind()
        gvMedia.DataBind()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim locMessage As String = ""

        If SaveForm(locMessage) Then
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "CloseCompanyClientScript", "javascript:CloseDialogSuccess();", True)
        Else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "CloseCompanyClientScript", "javascript:CloseDialogError('" + locMessage.Replace("'", "''") + "');", True)
        End If
    End Sub

    Protected Sub gvMedia_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvMedia.RowCommand
        Try
            Dim locRowIndex As Integer = e.CommandArgument
            Dim locMediaID As Long = gvMedia.DataKeys(locRowIndex).Value

            If e.CommandName = "EditMedia" Then
                Dim locMedia As ClsMediaItem = zVarMedia.FirstOrDefault(Function(x) x.MediaItemID = locMediaID)

                If Not IsNothing(locMedia) AndAlso locMedia.MediaItemID > 0 Then
                    ClearAddEditForm()

                    hiddenMediaItemID.Value = locMedia.MediaItemID
                    txtMediaName.Text = locMedia.MediaName
                    txtMediaDescription.Text = locMedia.MediaDescription
                    txtWebsiteURL.Text = locMedia.WebsiteURL

                    linqCategory.DataBind()
                    ddlCategory.DataBind()

                    ddlCategory.SelectedValue = locMedia.MediaCategoryID

                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "AddClientScript", "javascript: OpenDialog();", True)
                End If
            ElseIf e.CommandName = "RemoveMedia" Then
                Dim locRemoveMedia As New WESMediaItems_Save
                Dim locRemoveMediaInput As New WESMediaItems_Save.ClsInputParamsRemove
                Dim locRemoveMediaResults As New ClsDataSearchReturnSave

                locRemoveMediaInput.reqMediaItemID = locMediaID
                locRemoveMedia.zValInput = locRemoveMediaInput

                If locRemoveMedia.zProSave(locRemoveMediaResults) Then
                    If locRemoveMediaResults.zValUpdateStatus = 1 Then
                        LoadMedia()
                    Else
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "AddClientScript", "alert('Error removing Media: " + locRemoveMediaResults.zValUpdateMessage + "');", True)
                    End If
                End If
            End If
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub

    Protected Sub linqCategory_Selecting(sender As Object, e As LinqDataSourceSelectEventArgs) Handles linqCategory.Selecting
        Try
            If IsNothing(zVarCategories) Then LoadCategories()

            e.Result = zVarCategories
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub

    Protected Sub linqMedia_Selecting(sender As Object, e As LinqDataSourceSelectEventArgs) Handles linqMedia.Selecting
        Try
            If IsNothing(zVarMedia) Then LoadMedia()

            e.Result = zVarMedia
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub


    Private Sub ClearAddEditForm()
        hiddenMediaItemID.Value = 0
        txtMediaName.Text = ""
        txtMediaDescription.Text = ""
        txtWebsiteURL.Text = ""
    End Sub

    Private Sub LoadCategories()
        Try
            zVarCategories = New List(Of ClsMediaCategory)

            Dim locLoadCategory As New WESMediaCategory_Search
            Dim locLoadCategoryInput As New WESMediaCategory_Search.ClsInputParamsAllCategories
            Dim locLoadCategoryResult As New ClsDataSearchReturn

            locLoadCategory.zValInput = locLoadCategoryInput

            If locLoadCategory.zProSearch(locLoadCategoryResult) Then
                zVarCategories = locLoadCategoryResult.zValReturnList
            End If

            ViewState("WESCATEGORY") = zVarCategories

            linqCategory.DataBind()
            ddlCategory.DataBind()
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub

    Private Sub LoadMedia()
        Try
            zVarMedia = New List(Of ClsMediaItem)

            Dim locLoadMedia As New WESMediaItems_Search
            Dim locLoadMediaInput As New WESMediaItems_Search.ClsInputParamsAllItems
            Dim locLoadMediaResult As New ClsDataSearchReturn

            locLoadMedia.zValInput = locLoadMediaInput

            If locLoadMedia.zProSearch(locLoadMediaResult) Then
                zVarMedia = locLoadMediaResult.zValReturnList
            End If

            ViewState("WESMEDIA") = zVarMedia

            linqMedia.DataBind()
            gvMedia.DataBind()
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub

    Private Function SaveForm(ByRef reqMessage As String) As Boolean
        Dim locSuccess As Boolean = False

        Try
            Dim locSaveMedia As New WESMediaItems_Save
            Dim locSaveMediaInput As New WESMediaItems_Save.ClsInputParamsSave
            Dim locSaveMediaResults As New ClsDataSearchReturnSave

            locSaveMediaInput.reqMediaItemID = hiddenMediaItemID.Value
            locSaveMediaInput.reqMediaName = txtMediaName.Text
            locSaveMediaInput.reqMediaDescription = txtMediaDescription.Text
            locSaveMediaInput.reqMediaCategoryID = ddlCategory.SelectedValue
            locSaveMediaInput.reqWebsiteURL = txtWebsiteURL.Text

            locSaveMedia.zValInput = locSaveMediaInput

            If locSaveMedia.zProSave(locSaveMediaResults) Then
                locSuccess = locSaveMediaResults.zValUpdateStatus = 1
                reqMessage = locSaveMediaResults.zValUpdateMessage

                If locSuccess Then
                    LoadMedia()
                End If
            End If
        Catch ex As Exception
            reqMessage = ex.Message
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try

        Return locSuccess
    End Function

End Class
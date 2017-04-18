Imports System.Reflection
Imports WESClass.WESAppVars
Imports WESClass.WESData
Imports WESClass.WESObjects
Imports WESClass.WESSessions

Public Class WESItems
    Inherits System.Web.UI.Page

    Private Property zVarCountry As List(Of ClsCountry)
    Private Property zVarState As List(Of ClsState)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadCountry()
            LoadState()
            ddlItemType.SelectedIndex = 0
            ClearCountryForm()
            ClearStateForm()
        Else
            If Not IsNothing(ViewState("WESCOUNTRY")) Then
                zVarCountry = ViewState("WESCOUNTRY")
            End If

            If Not IsNothing(ViewState("WESSTATE")) Then
                zVarState = ViewState("WESSTATE")
            End If
        End If
    End Sub

    Protected Sub btnAddItem_Click(sender As Object, e As EventArgs) Handles btnAddItem.Click
        If ddlItemType.SelectedValue = "Country" Then
            ClearCountryForm()
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "AddCountryScript", "javascript: OpenCountry();", True)
        ElseIf ddlItemType.SelectedValue = "State" Then
            ClearStateForm()
        End If
    End Sub

    Protected Sub btnRefreshGrid_Click(sender As Object, e As EventArgs) Handles btnRefreshGrid.Click
        linqItem.DataBind()
        gvItem.DataBind()
    End Sub

    Protected Sub btnCountrySave_Click(sender As Object, e As EventArgs) Handles btnCountrySave.Click
        Dim locMessage As String = ""

        If SaveCountryForm(locMessage) Then
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "CloseCountryClientScript", "javascript:CloseCountrySuccess();", True)
        Else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "CloseCountryClientScript", "javascript:CloseError('" + locMessage.Replace("'", "''") + "');", True)
        End If
    End Sub

    Protected Sub btnStateSave_Click(sender As Object, e As EventArgs) Handles btnStateSave.Click
        Dim locMessage As String = ""

        If SaveStateForm(locMessage) Then
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "CloseStateClientScript", "javascript:CloseStateSuccess();", True)
        Else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "CloseStateClientScript", "javascript:CloseError('" + locMessage.Replace("'", "''") + "');", True)
        End If
    End Sub

    Protected Sub ddlItemType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlItemType.SelectedIndexChanged
        linqItem.DataBind()
        gvItem.DataBind()
    End Sub

    Protected Sub gvItem_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvItem.RowCommand
        Try
            Dim locRowIndex As Integer = e.CommandArgument
            Dim locItemID As Long = gvItem.DataKeys(locRowIndex).Value

            If e.CommandName = "EditItem" Then
                If ddlItemType.SelectedValue = "Country" Then
                    Dim locCountry As ClsCountry = zVarCountry.FirstOrDefault(Function(x) x.ZCountryID = locItemID)

                    If Not IsNothing(locCountry) AndAlso locCountry.ZCountryID > 0 Then
                        ClearCountryForm()

                        hiddenCountryID.Value = locCountry.ZCountryID
                        txtCountryName.Text = locCountry.CountryName
                        chkCountryActive.Checked = locCountry.Active = 1

                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "AddClientScript", "javascript: OpenCountry();", True)
                    End If
                ElseIf ddlItemType.SelectedValue = "State" Then
                    Dim locState As ClsState = zVarState.FirstOrDefault(Function(x) x.ZStateID = locItemID)

                    If Not IsNothing(locState) AndAlso locState.ZStateID > 0 Then
                        ClearStateForm()

                        linqCountry.DataBind()
                        ddlCountry.DataBind()

                        hiddenStateID.Value = locState.ZStateID
                        txtStateName.Text = locState.StateName
                        txtAbbreviation.Text = locState.Abbreviation
                        ddlCountry.SelectedValue = locState.ZCountryID
                        chkStateActive.Checked = locState.Active = 1

                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "AddClientScript", "javascript: OpenState();", True)
                    End If
                End If

            ElseIf e.CommandName = "RemoveItem" Then
                If ddlItemType.SelectedValue = "Country" Then
                    Dim locSaveCountry As New WESCountry_Save
                    Dim locSaveCountryInput As New WESCountry_Save.ClsInputParamsDelete
                    Dim locSaveCountryResults As New ClsDataSearchReturnSave

                    locSaveCountryInput.reqZCountryID = locItemID
                    locSaveCountry.zValInput = locSaveCountryInput

                    If locSaveCountry.zProSearch(locSaveCountryResults) Then
                        If locSaveCountryResults.zValUpdateStatus = 1 Then
                            LoadCountry()

                            linqItem.DataBind()
                            gvItem.DataBind()
                        Else
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "ErrorClientScript", "alert('Error removing Country: " + locSaveCountryResults.zValUpdateMessage.Replace("'", "''") + "');", True)
                        End If
                    End If
                ElseIf ddlItemType.SelectedValue = "State" Then
                    Dim locSaveState As New WESState_Save
                    Dim locSaveStateInput As New WESState_Save.ClsInputParamsDelete
                    Dim locSaveStateResults As New ClsDataSearchReturnSave

                    locSaveStateInput.reqZStateID = locItemID
                    locSaveState.zValInput = locSaveStateInput

                    If locSaveState.zProSearch(locSaveStateResults) Then
                        If locSaveStateResults.zValUpdateStatus = 1 Then
                            LoadState()

                            linqItem.DataBind()
                            gvItem.DataBind()
                        Else
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "ErrorClientScript", "alert('Error removing State: " + locSaveStateResults.zValUpdateMessage.Replace("'", "''") + "');", True)
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
        End Try
    End Sub

    Protected Sub linqCountry_Selecting(sender As Object, e As LinqDataSourceSelectEventArgs) Handles linqCountry.Selecting
        Try
            e.Result = zVarCountry.OrderBy(Function(x) x.CountryName)
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub

    Protected Sub linqItem_Selecting(sender As Object, e As LinqDataSourceSelectEventArgs) Handles linqItem.Selecting
        Try
            If ddlItemType.SelectedValue = "State" Then
                e.Result = From x In zVarState
                           Order By x.StateName
                           Select ItemName = x.StateName, ItemID = x.ZStateID, Active = x.Active = 1, ParentName = x.CountryName
            Else
                e.Result = From x In zVarCountry
                           Order By x.CountryName
                           Select ItemName = x.CountryName, ItemID = x.ZCountryID, Active = x.Active = 1, ParentName = ""
            End If
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub


    Private Sub ClearCountryForm()
        hiddenCountryID.Value = 0
        txtCountryName.Text = ""

        chkCountryActive.Checked = False
    End Sub

    Private Sub ClearStateForm()
        hiddenStateID.Value = 0
        txtStateName.Text = ""
        txtAbbreviation.Text = ""
        ddlCountry.SelectedIndex = 0

        chkStateActive.Checked = False
    End Sub

    Private Sub LoadCountry()
        Try
            zVarCountry = New List(Of ClsCountry)

            Dim locLoadCountry As New WESCountry_Search
            Dim locLoadCountryInput As New WESCountry_Search.ClsInputParamsAllCountries
            Dim locLoadCountryResult As New ClsDataSearchReturn

            locLoadCountry.zValInput = locLoadCountryInput

            If locLoadCountry.zProSearch(locLoadCountryResult) Then
                zVarCountry = locLoadCountryResult.zValReturnList
            End If

            ViewState("WESCOUNTRY") = zVarCountry
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub

    Private Sub LoadState()
        Try
            zVarState = New List(Of ClsState)

            Dim locLoadState As New WESState_Search
            Dim locLoadStateInput As New WESState_Search.ClsInputParamsAllStates
            Dim locLoadStateResult As New ClsDataSearchReturn

            locLoadState.zValInput = locLoadStateInput

            If locLoadState.zProSearch(locLoadStateResult) Then
                zVarState = locLoadStateResult.zValReturnList
            End If

            ViewState("WESSTATE") = zVarState
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub

    Private Function SaveCountryForm(ByRef reqMessage As String) As Boolean
        Dim locSuccess As Boolean = False

        Try
            Dim locSaveCountry As New WESCountry_Save
            Dim locSaveCountryInput As New WESCountry_Save.ClsInputParamsSave
            Dim locSaveCountryResults As New ClsDataSearchReturnSave

            locSaveCountryInput.reqZCountryID = hiddenCountryID.Value
            locSaveCountryInput.reqCountryName = txtCountryName.Text

            If chkCountryActive.Checked Then locSaveCountryInput.reqActive = 1

            locSaveCountry.zValInput = locSaveCountryInput

            If locSaveCountry.zProSearch(locSaveCountryResults) Then
                If locSaveCountryResults.zValUpdateStatus = 1 Then
                    locSuccess = True

                    Dim locCountry As ClsCountry = Nothing

                    If hiddenCountryID.Value > 0 Then
                        locCountry = zVarCountry.FirstOrDefault(Function(x) x.ZCountryID = hiddenCountryID.Value)
                    End If

                    locCountry.CountryName = txtCountryName.Text

                    If chkCountryActive.Checked Then
                        locCountry.Active = 1
                    Else
                        locCountry.Active = 0
                    End If

                    If hiddenCountryID.Value = 0 Then
                        locCountry.ZCountryID = locSaveCountryResults.zValUpdateID
                        zVarCountry.Add(locCountry)
                    End If
                Else
                    reqMessage = locSaveCountryResults.zValUpdateMessage
                End If
            Else
                reqMessage = "Please try again"
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

    Private Function SaveStateForm(ByRef reqMessage As String) As Boolean
        Dim locSuccess As Boolean = False

        Try
            Dim locSaveState As New WESState_Save
            Dim locSaveStateInput As New WESState_Save.ClsInputParamsSave
            Dim locSaveStateResults As New ClsDataSearchReturnSave

            locSaveStateInput.reqZStateID = hiddenStateID.Value
            locSaveStateInput.reqStateName = txtStateName.Text
            locSaveStateInput.reqAbbreviation = txtAbbreviation.Text
            locSaveStateInput.reqZCountryID = ddlCountry.SelectedValue

            If chkStateActive.Checked Then locSaveStateInput.reqActive = 1

            locSaveState.zValInput = locSaveStateInput

            If locSaveState.zProSearch(locSaveStateResults) Then
                If locSaveStateResults.zValUpdateStatus = 1 Then
                    locSuccess = True

                    Dim locState As ClsState = Nothing

                    If hiddenStateID.Value > 0 Then
                        locState = zVarState.FirstOrDefault(Function(x) x.ZStateID = hiddenStateID.Value)
                    End If

                    locState.StateName = txtStateName.Text
                    locState.Abbreviation = txtAbbreviation.Text
                    locState.ZCountryID = ddlCountry.SelectedValue

                    If ddlCountry.SelectedIndex >= 0 Then
                        locState.CountryName = ddlCountry.SelectedItem.Text
                    End If

                    If chkStateActive.Checked Then
                        locState.Active = 1
                    Else
                        locState.Active = 0
                    End If

                    If hiddenStateID.Value = 0 Then
                        locState.ZStateID = locSaveStateResults.zValUpdateID
                        zVarState.Add(locState)
                    End If
                Else
                    reqMessage = locSaveStateResults.zValUpdateMessage
                End If
            Else
                reqMessage = "Please try again"
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
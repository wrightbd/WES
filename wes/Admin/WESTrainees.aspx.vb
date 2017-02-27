Imports System.Reflection
Imports WESClass.WESAppVars
Imports WESClass.WESData
Imports WESClass.WESObjects
Imports WESClass.WESSessions

Public Class WESTrainees
    Inherits System.Web.UI.Page

    'ReadOnly zVarTestAttemptArray As New ClsTestAttempt_Array
    'ReadOnly zVarTestAnswerArray As New ClsTestAnswer_Array

    Private Property zVarTrainees As New List(Of ClsTrainee)
    Private Property zVarCountry As List(Of ClsCountry)
    Private Property zVarState As List(Of ClsState)
    Private Property zVarAttempts As List(Of ClsTestAttempt)
    Private Property zVarAnswers As List(Of ClsTestAnswer)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadCountry()
            LoadState()
            LoadTrainees()
            ClearAddEditForm()

            zVarAttempts = New List(Of ClsTestAttempt)
            ViewState("WESATTEMPT") = zVarAttempts
            'pnlAnswers.Visible = False
        Else
            If Not IsNothing(ViewState("WESTRAINEE")) Then
                zVarTrainees = ViewState("WESTRAINEE")
            End If

            If Not IsNothing(ViewState("WESCOUNTRY")) Then
                zVarCountry = ViewState("WESCOUNTRY")
            End If

            If Not IsNothing(ViewState("WESSTATE")) Then
                zVarState = ViewState("WESSTATE")
            End If

            If Not IsNothing(ViewState("WESATTEMPT")) Then
                zVarAttempts = ViewState("WESATTEMPT")
            End If

            If Not IsNothing(ViewState("WESANSWERS")) Then
                zVarAnswers = ViewState("WESANSWERS")
            End If
        End If
    End Sub

    Protected Sub btnRefreshGrid_Click(sender As Object, e As EventArgs) Handles btnRefreshGrid.Click
        linqTrainees.DataBind()
        gvTrainees.DataBind()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim locMessage As String = ""

        If SaveForm(locMessage) Then
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "CloseCompanyClientScript", "javascript:CloseDialogSuccess();", True)
        Else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "CloseCompanyClientScript", "javascript:CloseDialogError('" + locMessage.Replace("'", "''") + "');", True)
        End If
    End Sub

    Protected Sub ddlCountry_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCountry.SelectedIndexChanged
        linqState.DataBind()
        ddlState.DataBind()
    End Sub

    Protected Sub gvAttempts_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvAttempts.RowCommand
        Try
            Dim locRowIndex As Integer = e.CommandArgument
            Dim locAttemptID As Long = gvAttempts.DataKeys(locRowIndex).Value

            If e.CommandName = "TestAnswers" Then
                zVarAnswers = New List(Of ClsTestAnswer)
                Dim locAnswers As New WESTestAnswer_Search
                Dim locAnswersInput As New WESTestAnswer_Search.ClsInputParamsTestAttempt
                Dim locAnswersResult As New ClsDataSearchReturn

                locAnswersInput.reqTestAttemptID = locAttemptID
                locAnswers.zValInput = locAnswersInput

                If locAnswers.zProSearch(locAnswersResult) Then
                    zVarAnswers = locAnswersResult.zValReturnList
                End If

                ViewState("WESANSWERS") = zVarAnswers

                linqAnswers.DataBind()
                gvAnswers.DataBind()

                pnlAnswers.Visible = True
            End If
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub

    Protected Sub gvTrainees_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvTrainees.RowCommand
        Try
            Dim locRowIndex As Integer = e.CommandArgument
            Dim locTraineeID As Long = gvTrainees.DataKeys(locRowIndex).Value

            If e.CommandName = "EditTrainee" Then
                Dim locTrainee As ClsTrainee = zVarTrainees.FirstOrDefault(Function(x) x.TraineeID = locTraineeID)

                If Not IsNothing(locTrainee) AndAlso locTrainee.TraineeID > 0 Then
                    ClearAddEditForm()

                    hiddenTraineeID.Value = locTrainee.TraineeID
                    txtEmailAddress.Text = locTrainee.EmailAddress
                    txtFirstName.Text = locTrainee.FirstName
                    txtMiddleName.Text = locTrainee.MiddleName
                    txtLastName.Text = locTrainee.LastName
                    txtSuffix.Text = locTrainee.LastNameSuffix

                    ddlCountry.SelectedValue = locTrainee.ZCountryID
                    linqState.DataBind()
                    ddlState.DataBind()

                    txtAddress1.Text = locTrainee.Address1
                    txtAddress2.Text = locTrainee.Address2
                    txtCity.Text = locTrainee.City
                    ddlState.SelectedValue = locTrainee.ZStateID
                    txtZipCode.Text = locTrainee.ZipCode

                    chkAdminUser.Checked = locTrainee.AdminUser = 1

                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "AddClientScript", "javascript: OpenDialog();", True)
                End If
            ElseIf e.CommandName = "TraineeTests" Then
                zVarAttempts = New List(Of ClsTestAttempt)
                Dim locTraineeTests As New WESTestAttempt_Search
                Dim locTraineeTestsInput As New WESTestAttempt_Search.ClsInputParamsTrainee
                Dim locTraineeTestsResult As New ClsDataSearchReturn

                locTraineeTestsInput.reqTraineeID = locTraineeID
                locTraineeTests.zValInput = locTraineeTestsInput

                If locTraineeTests.zProSearch(locTraineeTestsResult) Then
                    zVarAttempts = locTraineeTestsResult.zValReturnList
                End If

                ViewState("WESATTEMPT") = zVarAttempts

                linqAttempts.DataBind()
                gvAttempts.DataBind()
                'pnlAnswers.Visible = False

                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "TestClientScript", "javascript: OpenTests();", True)
            ElseIf e.CommandName = "RemoveTrainee" Then
                Dim locSaveTrainee As New WESTrainee_Save
                Dim locSaveTraineeInput As New WESTrainee_Save.ClsInputParamsDelete
                Dim locSaveTraineeResults As New ClsDataSearchReturnSave

                locSaveTraineeInput.reqTraineeID = locTraineeID
                locSaveTrainee.zValInput = locSaveTraineeInput

                If locSaveTrainee.zProSearch(locSaveTraineeResults) Then
                    If locSaveTraineeResults.zValUpdateStatus = 1 Then
                        LoadTrainees()
                    Else
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "AddClientScript", "alert('Error removing Trainee: " + locSaveTraineeResults.zValUpdateMessage + "');", True)
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

    'Protected Sub linqAnswers_Selecting(sender As Object, e As LinqDataSourceSelectEventArgs) Handles linqAnswers.Selecting
    '    Try
    '        e.Result = zVarTestAnswerArray.TestAnswers
    '    Catch ex As Exception
    '        Dim locErrorHandler As New ErrorLogging
    '        Dim locSF As StackFrame = New StackFrame()
    '        Dim locMB As MethodBase = locSF.GetMethod()
    '        Dim locClass As System.Type = locMB.DeclaringType
    '        locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
    '    End Try
    'End Sub

    Protected Sub linqAttempts_Selecting(sender As Object, e As LinqDataSourceSelectEventArgs) Handles linqAttempts.Selecting
        Try
            e.Result = zVarAttempts.OrderByDescending(Function(x) x.AttemptDate)
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
            Dim locBlankList As New List(Of ClsCountry)
            Dim locBlank As New ClsCountry
            locBlank.ZCountryID = 0
            locBlank.CountryName = "(Select a Country)"
            locBlankList.Add(locBlank)

            e.Result = locBlankList.Concat(zVarCountry.Where(Function(x) x.Active = 1).OrderBy(Function(x) x.CountryName))
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub

    Protected Sub linqState_Selecting(sender As Object, e As LinqDataSourceSelectEventArgs) Handles linqState.Selecting
        Try
            Dim locBlankList As New List(Of ClsState)
            Dim locBlank As New ClsState
            locBlank.ZStateID = 0
            locBlank.StateName = "(Select a State)"
            locBlankList.Add(locBlank)

            Dim locCountryID As Long = 0

            Long.TryParse(ddlCountry.SelectedValue, locCountryID)

            e.Result = locBlankList.Concat(zVarState.Where(Function(x) x.Active = 1 And x.ZCountryID = locCountryID).OrderBy(Function(y) y.StateName))
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub

    Protected Sub linqTrainees_Selecting(sender As Object, e As LinqDataSourceSelectEventArgs) Handles linqTrainees.Selecting
        Try
            If IsNothing(zVarTrainees) Then LoadTrainees()

            e.Result = zVarTrainees
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub


    Private Sub ClearAddEditForm()
        hiddenTraineeID.Value = 0
        txtEmailAddress.Text = ""
        txtFirstName.Text = ""
        txtMiddleName.Text = ""
        txtLastName.Text = ""
        txtSuffix.Text = ""
        txtAddress1.Text = ""
        txtAddress2.Text = ""
        txtCity.Text = ""
        ddlState.SelectedIndex = 0
        txtZipCode.Text = ""
        ddlCountry.SelectedIndex = 0

        chkAdminUser.Checked = False
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

            linqCountry.DataBind()
            ddlCountry.DataBind()
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

            linqState.DataBind()
            ddlState.DataBind()
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub

    Private Sub LoadTrainees()
        Try
            zVarTrainees = New List(Of ClsTrainee)
            Dim locLoadTrainee As New WESTrainee_Search
            Dim locLoadTraineeInput As New WESTrainee_Search.ClsInputParamsAllTrainees
            Dim locLoadTraineeResults As New ClsDataSearchReturn

            locLoadTrainee.zValInput = locLoadTraineeInput

            If locLoadTrainee.zProSearch(locLoadTraineeResults) Then
                zVarTrainees = locLoadTraineeResults.zValReturnList
            End If

            ViewState("WESTRAINEE") = zVarTrainees

            linqTrainees.DataBind()
            gvTrainees.DataBind()
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ": Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub

    Private Function SaveForm(ByRef reqMessage As String) As Boolean
        Dim locSuccess As Boolean = False

        Try
            Dim locSaveTrainee As New WESTrainee_Save
            Dim locSaveTraineeInput As New WESTrainee_Save.ClsInputParamsSave
            Dim locSaveTraineeResults As New ClsDataSearchReturnSave

            locSaveTraineeInput.reqTraineeID = hiddenTraineeID.Value
            locSaveTraineeInput.reqEmailAddress = txtEmailAddress.Text
            locSaveTraineeInput.reqFirstName = txtFirstName.Text
            locSaveTraineeInput.reqMiddleName = txtMiddleName.Text
            locSaveTraineeInput.reqLastName = txtLastName.Text
            locSaveTraineeInput.reqLastNameSuffix = txtSuffix.Text
            locSaveTraineeInput.reqAddress1 = txtAddress1.Text
            locSaveTraineeInput.reqAddress2 = txtAddress2.Text
            locSaveTraineeInput.reqCity = txtCity.Text
            locSaveTraineeInput.reqState = ddlState.SelectedValue
            locSaveTraineeInput.reqZipCode = txtZipCode.Text
            locSaveTraineeInput.reqCountry = ddlCountry.SelectedValue

            If chkAdminUser.Checked Then
                locSaveTraineeInput.reqAdminUser = 1
            End If

            locSaveTrainee.zValInput = locSaveTraineeInput

            If locSaveTrainee.zProSearch(locSaveTraineeResults) Then
                locSuccess = locSaveTraineeResults.zValUpdateStatus = 1
                reqMessage = locSaveTraineeResults.zValUpdateMessage

                If locSuccess Then
                    LoadTrainees()
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
Imports System.Reflection
Imports WESClass.WESAppVars
Imports WESClass.WESData
Imports WESClass.WESObjects
Imports WESClass.WESSecurity
Imports WESClass.WESSessions

Public Class ManageAccount
    Inherits System.Web.UI.Page

    ReadOnly zVarLoginSession As New ClsLoginUser_Session
    ReadOnly zVarLoginSecurity As New ClsLoginSecurity

    Private Property zVarOrders As New List(Of ClsWESOrder)
    Private Property zVarAttempts As List(Of ClsTestAttempt)
    Private Property zVarCountry As List(Of ClsCountry)
    Private Property zVarState As List(Of ClsState)
    Private Property zVarAnswers As List(Of ClsTestAnswer)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ClearForm()
            LoadCountry()
            LoadState()
            LoadTrainee()
            LoadOrders()
            LoadTestAttempts()
        Else
            If Not IsNothing(ViewState("WESCOUNTRY")) Then
                zVarCountry = ViewState("WESCOUNTRY")
            End If

            If Not IsNothing(ViewState("WESSTATE")) Then
                zVarState = ViewState("WESSTATE")
            End If

            If Not IsNothing(ViewState("WESORDER")) Then
                zVarOrders = ViewState("WESORDER")
            End If

            If Not IsNothing(ViewState("WESATTEMPT")) Then
                zVarAttempts = ViewState("WESATTEMPT")
            End If

            If Not IsNothing(ViewState("WESANSWERS")) Then
                zVarAnswers = ViewState("WESANSWERS")
            End If
        End If
    End Sub

    Protected Sub btnChangePassword_Click(sender As Object, e As EventArgs) Handles btnChangePassword.Click
        UpdatePassword()
    End Sub

    Protected Sub btnUpdateInfo_Click(sender As Object, e As EventArgs) Handles btnUpdateInfo.Click
        UpdateDetails()
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

    Protected Sub linqAnswers_Selecting(sender As Object, e As LinqDataSourceSelectEventArgs) Handles linqAnswers.Selecting
        Try
            e.Result = zVarAnswers
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub

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

    Protected Sub linqOrders_Selecting(sender As Object, e As LinqDataSourceSelectEventArgs) Handles linqOrders.Selecting
        Try
            e.Result = zVarOrders.OrderByDescending(Function(x) x.OrderDate)
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


    Protected Sub ClearForm()
        txtFirstName.Text = ""
        txtMiddleName.Text = ""
        txtLastName.Text = ""

        txtEmailAddress.Text = ""

        txtAddress1.Text = ""
        txtAddress2.Text = ""
        txtCity.Text = ""
        ddlState.SelectedIndex = 0
        ddlCountry.SelectedIndex = 0
        txtZipCode.Text = ""

        pnlAnswers.Visible = False
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

    Protected Sub LoadOrders()
        Try
            zVarOrders = New List(Of ClsWESOrder)
            Dim locLoadOrder As New WESOrder_Search
            Dim locLoadOrderInput As New WESOrder_Search.ClsInputParamsTrainee
            Dim locLoadOrderResults As New ClsDataSearchReturn

            locLoadOrderInput.reqTraineeID = zVarLoginSession.TraineeID
            locLoadOrder.zValInput = locLoadOrderInput

            If locLoadOrder.zProSearch(locLoadOrderResults) Then
                zVarOrders = locLoadOrderResults.zValReturnList
            End If

            ViewState("WESORDER") = zVarOrders

            linqOrders.DataBind()
            gvOrders.DataBind()
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub

    Protected Sub LoadTestAttempts()
        Try
            Dim locTraineeTests As New WESTestAttempt_Search
            Dim locTraineeTestsInput As New WESTestAttempt_Search.ClsInputParamsTrainee
            Dim locTraineeTestsResult As New ClsDataSearchReturn

            locTraineeTestsInput.reqTraineeID = zVarLoginSession.TraineeID
            locTraineeTests.zValInput = locTraineeTestsInput

            If locTraineeTests.zProSearch(locTraineeTestsResult) Then
                zVarAttempts = locTraineeTestsResult.zValReturnList
            End If

            ViewState("WESATTEMPT") = zVarAttempts

            linqAttempts.DataBind()
            gvAttempts.DataBind()
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub

    Protected Sub LoadTrainee()
        Try
            Dim locLoadTrainee As New WESTrainee_Search
            Dim locLoadTraineeInput As New WESTrainee_Search.ClsInputParamsTrainee
            Dim locLoadTraineeResult As New ClsDataSearchReturn

            locLoadTraineeInput.reqTraineeID = zVarLoginSession.TraineeID
            locLoadTrainee.zValInput = locLoadTraineeInput

            If locLoadTrainee.zProSearch(locLoadTraineeResult) Then
                If locLoadTraineeResult.zValReturnList.Count > 0 Then
                    Dim locTrainee As ClsTrainee = locLoadTraineeResult.zValReturnList(0)

                    txtEmailAddress.Text = locTrainee.EmailAddress

                    txtFirstName.Text = locTrainee.FirstName
                    txtMiddleName.Text = locTrainee.MiddleName
                    txtLastName.Text = locTrainee.LastName

                    ddlCountry.SelectedValue = locTrainee.ZCountryID
                    linqState.DataBind()
                    ddlState.DataBind()

                    txtAddress1.Text = locTrainee.Address1
                    txtAddress2.Text = locTrainee.Address2
                    txtCity.Text = locTrainee.City
                    ddlState.SelectedValue = locTrainee.ZStateID
                    txtZipCode.Text = locTrainee.ZipCode
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

    Protected Sub UpdateDetails()
        Dim locSuccess As Boolean = False
        Dim locMessage As String = ""

        Try
            Dim locSaveTrainee As New WESTrainee_Save
            Dim locSaveTraineeInput As New WESTrainee_Save.ClsInputParamsSaveNoUserNoAdmin
            Dim locSaveTraineeResult As New ClsDataSearchReturnSave

            locSaveTraineeInput.reqTraineeID = zVarLoginSession.TraineeID
            locSaveTraineeInput.reqFirstName = txtFirstName.Text
            locSaveTraineeInput.reqMiddleName = txtMiddleName.Text
            locSaveTraineeInput.reqLastName = txtLastName.Text

            locSaveTraineeInput.reqEmailAddress = txtEmailAddress.Text

            locSaveTraineeInput.reqAddress1 = txtAddress1.Text
            locSaveTraineeInput.reqAddress2 = txtAddress2.Text
            locSaveTraineeInput.reqCity = txtCity.Text
            locSaveTraineeInput.reqState = ddlState.SelectedValue
            locSaveTraineeInput.reqZipCode = txtZipCode.Text
            locSaveTraineeInput.reqCountry = ddlCountry.SelectedValue
            locSaveTrainee.zValInput = locSaveTraineeInput

            If locSaveTrainee.zProSearch(locSaveTraineeResult) Then
                locSuccess = locSaveTraineeResult.zValUpdateStatus = 1
                locMessage = locSaveTraineeResult.zValUpdateMessage
            End If
        Catch ex As Exception
            locMessage = "Please try again."

            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try

        If locSuccess Then
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "CloseCompanyClientScript", "alert('Details updated successfully.');", True)
        Else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "CloseCompanyClientScript", "alert('Error updating details: " + locMessage + "');", True)
        End If
    End Sub

    Protected Sub UpdatePassword()
        Dim locSuccess As Boolean = False
        Dim locMessage As String = ""

        Try
            If txtNewPassword.Text = txtConfirmPassword.Text Then
                Dim locSaveTrainee As New WESTrainee_Save
                Dim locSaveTraineeInput As New WESTrainee_Save.ClsInputParamsChangePassword
                Dim locSaveTraineeResult As New ClsDataSearchReturnSave

                locSaveTraineeInput.reqTraineeID = zVarLoginSession.TraineeID
                locSaveTraineeInput.reqPassword = zVarLoginSecurity.EncryptPassword(txtNewPassword.Text)
                locSaveTrainee.zValInput = locSaveTraineeInput

                If locSaveTrainee.zProSearch(locSaveTraineeResult) Then
                    locSuccess = locSaveTraineeResult.zValUpdateStatus = 1
                    locMessage = locSaveTraineeResult.zValUpdateMessage
                End If
            Else
                locMessage = "Passwords do not match. Please make sure both fields match."
            End If
        Catch ex As Exception
            locMessage = "Please try again."

            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try

        If locSuccess Then
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "CloseCompanyClientScript", "alert('Password changed successfully.');", True)
        Else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "CloseCompanyClientScript", "alert('Error changing password: " + locMessage + "');", True)
        End If
    End Sub

End Class
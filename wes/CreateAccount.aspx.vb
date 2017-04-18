Imports System.Reflection
Imports WESClass.WESAppVars
Imports WESClass.WESData
Imports WESClass.WESObjects
Imports WESClass.WESSecurity
Imports WESClass.WESSessions

Public Class CreateAccount
    Inherits System.Web.UI.Page

    ReadOnly zVarLoginSecurity As New ClsLoginSecurity

    Private Property zVarCountry As List(Of ClsCountry)
    Private Property zVarState As List(Of ClsState)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ClearForm()
            LoadCountry()
            LoadState()
        Else
            If Not IsNothing(ViewState("WSCOUNTRY")) Then
                zVarCountry = ViewState("WSCOUNTRY")
            End If

            If Not IsNothing(ViewState("WSSTATE")) Then
                zVarState = ViewState("WSSTATE")
            End If
        End If
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        ClearForm()
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim locMessage As String = ""

        If ValidateForm(locMessage) Then
            If CreateAccount(locMessage) Then
                'ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "SuccessClientScript", "alert('Account created successfully. An email has been sent to verify your email address.');", True)
                Response.Redirect("https://www.worldexercisesystem.com/ChoosePlan.aspx")
            Else
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "ErrorClientScript", "alert('Error creating your Account:  " + locMessage.Replace("'", "''") + "');", True)
            End If
        Else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "ErrorClientScript", "alert('Error creating your Account:\n" + locMessage.Replace("'", "''") + "');", True)
        End If
    End Sub

    Protected Sub ddlCountry_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCountry.SelectedIndexChanged
        linqState.DataBind()
        ddlState.DataBind()
    End Sub

    Protected Sub linqCountry_Selecting(sender As Object, e As LinqDataSourceSelectEventArgs) Handles linqCountry.Selecting
        Try
            e.Result = zVarCountry.Where(Function(x) x.Active = 1).OrderBy(Function(x) x.CountryName)
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

    Private Sub ClearForm()
        txtUsername.Text = ""
        txtPassword.Text = ""
        txtConfirmEmail.Text = ""

        txtFirstName.Text = ""
        txtMiddleName.Text = ""
        txtLastName.Text = ""

        txtAddress1.Text = ""
        txtAddress2.Text = ""
        txtCity.Text = ""
        txtZipCode.Text = ""
    End Sub

    Private Function CreateAccount(ByRef reqMessage As String) As Boolean
        Dim locSuccess As Boolean = False

        Try
            Dim locTraineeSave As New WESTrainee_Save
            Dim locTraineeSaveInput As New WESTrainee_Save.ClsInputParamsSave
            Dim locTraineeSaveResult As New ClsDataSearchReturnSave

            locTraineeSaveInput.reqEmailAddress = txtUsername.Text
            locTraineeSaveInput.reqAddress1 = txtAddress1.Text
            locTraineeSaveInput.reqAddress2 = txtAddress2.Text
            locTraineeSaveInput.reqCity = txtCity.Text
            locTraineeSaveInput.reqCountry = ddlCountry.SelectedValue
            locTraineeSaveInput.reqFirstName = txtFirstName.Text
            locTraineeSaveInput.reqLastName = txtLastName.Text
            locTraineeSaveInput.reqLastNameSuffix = ""
            locTraineeSaveInput.reqMiddleName = txtMiddleName.Text
            locTraineeSaveInput.reqPassword = zVarLoginSecurity.EncryptPassword(txtPassword.Text)
            locTraineeSaveInput.reqState = ddlState.SelectedValue
            locTraineeSaveInput.reqUsername = txtUsername.Text
            locTraineeSaveInput.reqZipCode = txtZipCode.Text
            locTraineeSave.zValInput = locTraineeSaveInput

            If locTraineeSave.zProSearch(locTraineeSaveResult) Then
                If locTraineeSaveResult.zValUpdateStatus = 1 Then
                    locSuccess = True
                    reqMessage = "Account Saved Successfully."

                    Dim locCurrentLogin As New ClsLoginUser_Session
                    locCurrentLogin.TraineeID = locTraineeSaveResult.zValUpdateID

                    EmailVerification(locTraineeSaveResult.zValUpdateRowGUID)
                Else
                    reqMessage = locTraineeSaveResult.zValUpdateMessage
                End If
            Else
                reqMessage = locTraineeSaveResult.zValReturnErrorStatus.zValStatusErrorMessage
            End If
        Catch ex As Exception
            locSuccess = False
            reqMessage = ex.Message

            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try

        Return locSuccess
    End Function

    Private Sub EmailVerification(ByVal reqTraineeGUID As String)
        Try
            Dim locFromName As String = WebsiteValues.FromName
            Dim locFromAddress As String = WebsiteValues.FromAddress

            Dim locBody As String = "Please click the following link or copy the address into your browser to verify your email address:<br /><br /><a href='" + WebsiteValues.WebsiteAddress + "/VerifyEmail.aspx?p1=" + reqTraineeGUID + "'>Click Here</a><br /><br />" + WebsiteValues.WebsiteAddress + "/VerifyEmail.aspx?p1=" + reqTraineeGUID

            Email.SendEmail(txtUsername.Text, locFromAddress, locFromName, "Please verify your email address", locBody)
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
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

            ViewState("WSCOUNTRY") = zVarCountry

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

            ViewState("WSSTATE") = zVarState

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

    Private Function ValidateForm(ByRef reqMessage As String) As Boolean
        Dim locValid As Boolean = True

        Try
            If txtUsername.Text.Trim = "" Then
                locValid = False

                If reqMessage = "" Then
                    reqMessage = "Username is required"
                Else
                    reqMessage = reqMessage + "\n" + "Username is required"
                End If
            ElseIf Not Email.IsEmailValid(txtUsername.Text.Trim) Then
                locValid = False

                If reqMessage = "" Then
                    reqMessage = "Email Address is not a valid email"
                Else
                    reqMessage = reqMessage + "\n" + "Email Address is not a valid email"
                End If
            ElseIf Not ValidateUsername(txtUsername.Text, reqMessage) Then
                locValid = False
            End If

            If txtConfirmEmail.Text.Trim = "" Then
                locValid = False

                If reqMessage = "" Then
                    reqMessage = "Confirm Email is required"
                Else
                    reqMessage = reqMessage + "\n" + "Confirm Email is required"
                End If
            ElseIf txtUsername.Text <> txtConfirmEmail.Text Then
                locValid = False

                If reqMessage = "" Then
                    reqMessage = "Emails do not match"
                Else
                    reqMessage = reqMessage + "\n" + "Emails do not match"
                End If
            End If

            If txtPassword.Text.Trim = "" Then
                locValid = False

                If reqMessage = "" Then
                    reqMessage = "Password is required"
                Else
                    reqMessage = reqMessage + "\n" + "Password is required"
                End If
            ElseIf txtConfirmPass.Text.Trim = "" Then
                locValid = False

                If reqMessage = "" Then
                    reqMessage = "Confirm Password is required"
                Else
                    reqMessage = reqMessage + "\n" + "Confirm Password is required"
                End If
            ElseIf txtPassword.Text.Trim <> txtConfirmPass.Text.Trim Then
                locValid = False

                If reqMessage = "" Then
                    reqMessage = "Password must match Confirm Password"
                Else
                    reqMessage = reqMessage + "\n" + "Password must match Confirm Password"
                End If
            End If

            If txtFirstName.Text.Trim = "" Then
                locValid = False

                If reqMessage = "" Then
                    reqMessage = "First Name is required"
                Else
                    reqMessage = reqMessage + "\n" + "First Name is required"
                End If
            End If

            If txtLastName.Text.Trim = "" Then
                locValid = False

                If reqMessage = "" Then
                    reqMessage = "Last Name is required"
                Else
                    reqMessage = reqMessage + "\n" + "Last Name is required"
                End If
            End If
        Catch ex As Exception
            locValid = False
            reqMessage = ex.Message

            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try

        Return locValid
    End Function

    Public Function ValidateUsername(ByVal reqUsername As String, ByRef reqMessage As String) As Boolean
        Dim locUnique As Boolean = True

        Try
            Dim locUsernameSearch As New WESTrainee_Search
            Dim locUsernameSearchInput As New WESTrainee_Search.ClsInputParamsUsernameSearch
            Dim locUserNameSearchResult As New ClsDataSearchReturn

            locUsernameSearchInput.reqUsername = reqUsername
            locUsernameSearch.zValInput = locUsernameSearchInput

            If locUsernameSearch.zProSearch(locUserNameSearchResult) Then
                locUnique = locUserNameSearchResult.zValReturnList.Count = 0

                If Not locUnique Then
                    reqMessage = "Username already in use. Please choose another username."
                End If
            End If
        Catch ex As Exception
            locUnique = False
            reqMessage = ex.Message
        End Try

        Return locUnique
    End Function

End Class
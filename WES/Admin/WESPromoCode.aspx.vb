Imports System.Reflection
Imports WESClass.WESAppVars
Imports WESClass.WESData
Imports WESClass.WESObjects

Public Class WESPromoCode
    Inherits System.Web.UI.Page

    Private Property PromoCodeArray As List(Of ClsPromoCode)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadPromoCodes()
            ClearAddEditForm()
        End If
    End Sub

    Protected Sub btnAddPromoCode_Click(sender As Object, e As EventArgs) Handles btnAddPromoCode.Click
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
        linqPromoCodes.DataBind()
        gvPromoCodes.DataBind()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim locMessage As String = ""

        If SaveForm(locMessage) Then
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "CloseCompanyClientScript", "javascript:CloseDialogSuccess();", True)
        Else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "CloseCompanyClientScript", "javascript:CloseDialogError('" + locMessage.Replace("'", "''") + "');", True)
        End If
    End Sub

    Protected Sub gvPromoCodes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvPromoCodes.RowCommand
        Try
            Dim locRowIndex As Integer = e.CommandArgument
            Dim locPromoCodeID As Long = gvPromoCodes.DataKeys(locRowIndex).Value

            If e.CommandName = "EditPromoCode" Then
                Dim locPromoCode As ClsPromoCode = Nothing

                Dim locLoadPromoCode As New WESPromoCode_Search
                Dim locLoadPromoCodeInput As New WESPromoCode_Search.ClsInputParamsPromoCodeID
                Dim locLoadPromoCodeResult As New ClsDataSearchReturn

                locLoadPromoCodeInput.reqPromoCodeID = locPromoCodeID
                locLoadPromoCode.zValInput = locLoadPromoCodeInput

                If locLoadPromoCode.zProSearch(locLoadPromoCodeResult) Then
                    If locLoadPromoCodeResult.zValReturnList.Count > 0 Then
                        locPromoCode = locLoadPromoCodeResult.zValReturnList(0)
                    End If
                End If

                If Not IsNothing(locPromoCode) AndAlso locPromoCode.PromoCodeID > 0 Then
                    ClearAddEditForm()

                    txtPromoCodeName.Text = locPromoCode.PromoCodeName
                    hiddenPromoCodeID.Value = locPromoCode.PromoCodeID

                    txtPromoCode.Text = locPromoCode.PromoCode

                    txtDiscount.Text = locPromoCode.Discount.ToString()

                    If locPromoCode.DateEnd.HasValue Then
                        txtDateEnd.Text = locPromoCode.DateEnd.Value.ToString("MM/dd/yyyy")
                    Else
                        txtDateEnd.Text = DateTime.Today.AddYears(1)
                    End If

                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "AddClientScript", "javascript: OpenDialog();", True)
                    End If
                ElseIf e.CommandName = "RemovePromoCode" Then
                Dim locSavePromoCode As New WESPromoCode_Save
                Dim locSavePromoCodeInput As New WESPromoCode_Save.ClsInputParamsPromoCodeDelete
                Dim locSavePromoCodeResults As New ClsDataSearchReturnSave

                locSavePromoCodeInput.reqPromoCodeID = locPromoCodeID
                locSavePromoCode.zValInput = locSavePromoCodeInput

                If locSavePromoCode.zProSearch(locSavePromoCodeResults) Then
                    If locSavePromoCodeResults.zValUpdateStatus = 1 Then
                        LoadPromoCodes()
                    Else
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "AddClientScript", "alert('Error removing Promo Code: " + locSavePromoCodeResults.zValUpdateMessage + "');", True)
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

    Protected Sub linqPromoCodes_Selecting(sender As Object, e As LinqDataSourceSelectEventArgs) Handles linqPromoCodes.Selecting
        Try
            If IsNothing(PromoCodeArray) Then LoadPromoCodes()

            e.Result = PromoCodeArray
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub


    Private Sub ClearAddEditForm()
        hiddenPromoCodeID.Value = 0
        txtPromoCodeName.Text = ""

        txtPromoCode.Text = ""
        txtDiscount.Text = "0"

        txtDateEnd.Text = DateTime.Today.AddYears(1).ToString("MM/dd/yyyy")
    End Sub

    Private Sub LoadPromoCodes()
        Try
            PromoCodeArray = New List(Of ClsPromoCode)
            Dim locLoadPromoCode As New WESPromoCode_Search
            Dim locLoadPromoCodeInput As New WESPromoCode_Search.ClsInputParamsAllPromoCodes
            Dim locLoadPromoCodeResults As New ClsDataSearchReturn

            locLoadPromoCode.zValInput = locLoadPromoCodeInput

            If locLoadPromoCode.zProSearch(locLoadPromoCodeResults) Then
                PromoCodeArray = locLoadPromoCodeResults.zValReturnList
            End If

            linqPromoCodes.DataBind()
            gvPromoCodes.DataBind()
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
            Dim locSavePromoCode As New WESPromoCode_Save
            Dim locSavePromoCodeInput As New WESPromoCode_Save.ClsInputParamsPromoCodeSave
            Dim locSavePromoCodeResults As New ClsDataSearchReturnSave

            locSavePromoCodeInput.reqPromoCodeID = hiddenPromoCodeID.Value
            locSavePromoCodeInput.reqPromoCodeName = txtPromoCodeName.Text
            locSavePromoCodeInput.reqPromoCode = txtPromoCode.Text

            locSavePromoCodeInput.reqDiscount = txtDiscount.Text
            locSavePromoCodeInput.reqDateEnd = txtDateEnd.Text

            locSavePromoCode.zValInput = locSavePromoCodeInput

            If locSavePromoCode.zProSearch(locSavePromoCodeResults) Then
                locSuccess = locSavePromoCodeResults.zValUpdateStatus = 1
                reqMessage = locSavePromoCodeResults.zValUpdateMessage

                If locSuccess Then
                    LoadPromoCodes()
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
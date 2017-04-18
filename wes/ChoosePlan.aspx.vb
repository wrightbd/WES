Imports WESClass.WESData
Imports WESClass.WESObjects
Imports WESClass.WESSessions

Public Class ChoosePlan
    Inherits System.Web.UI.Page

    ReadOnly zVarLoginSession As New ClsLoginUser_Session

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If zVarLoginSession.TraineeID <= 0 Then
                'Response.Redirect("~/Login.aspx")
            End If

            'pnlAccount.Visible = False

            'If Request.QueryString("a") = "1" Then
            '    pnlAccount.Visible = True
            'End If
        End If
    End Sub

    'Protected Sub btnPurchase_Click(sender As Object, e As ImageClickEventArgs) Handles btnPurchase.Click
    '    Dim locDiscount As Double = 0

    '    If txtPromoCode.Text.Trim <> "" Then
    '        Dim locSearchPromo As New WESPromoCode_Search
    '        Dim locSearchPromoInput As New WESPromoCode_Search.ClsInputParamsPromoCodeName
    '        Dim locSearchPromoResult As New ClsDataSearchReturn

    '        locSearchPromoInput.reqPromoCode = txtPromoCode.Text.Trim
    '        locSearchPromo.zValInput = locSearchPromoInput

    '        If locSearchPromo.zProSearch(locSearchPromoResult) Then
    '            If locSearchPromoResult.zValReturnList.Count > 0 Then
    '                Dim locPromoCode As ClsPromoCode = locSearchPromoResult.zValReturnList(0)

    '                locDiscount = locPromoCode.Discount
    '            End If
    '        End If
    '    End If

    '    Dim locParams As New StringBuilder

    '    Dim locSandboxAddress As String = "https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_s-xclick"
    '    Dim locSandboxButton As String = "4ADDJY2T6BRWC"

    '    Dim locLiveAddress As String = "https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick"
    '    Dim locLiveButton As String = "5NPDV38UQBBMJ"

    '    locParams.Append(locLiveAddress)
    '    locParams.Append("&hosted_button_id=" + locLiveButton)
    '    locParams.Append("&custom=" + zVarLoginSession.TraineeID.ToString())

    '    If locDiscount > 0 Then
    '        locParams.Append("&discount_amount=" + locDiscount.ToString())
    '    End If

    '    Response.Redirect(locParams.ToString(), True)
    'End Sub
End Class
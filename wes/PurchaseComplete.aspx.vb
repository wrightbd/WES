Imports System.IO
Imports System.Net
Imports System.Reflection
Imports WESClass.WESAppVars
Imports WESClass.WESData
Imports WESClass.WESObjects
Imports WESClass.WESSecurity
Imports WESClass.WESSessions

Public Class PurchaseComplete
    Inherits System.Web.UI.Page

    ReadOnly zVarLoginSession As New ClsLoginUser_Session

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ProcessPayment()
    End Sub

    Private Sub ProcessPayment()
        Dim locPurchaseMessage As String = ""

        Try
            ' CUSTOMIZE THIS: This is the seller's Payment Data Transfer authorization token.
            ' Replace this with the PDT token in "Website Payment Preferences" under your account.
            Dim authToken As String = "t5RI9TRdFxYrU__lLuqy74Yoa64a9zzetepv3NIfked1W4MGgKZfHUreR2C" 'Test = "FI0ffWiFr124Y-M5z1W713YIXoF6cYO73zs1SdNwI2G84EhjgIqdguTh3Vy", Live = "t5RI9TRdFxYrU__lLuqy74Yoa64a9zzetepv3NIfked1W4MGgKZfHUreR2C"
            Dim txToken As String = Request.QueryString("tx")
            Dim strRequest As String = "cmd=_notify-synch&tx=" & txToken & "&at=" & authToken

            'Post back to either sandbox or live
            Dim strSandbox As String = "https://www.sandbox.paypal.com/cgi-bin/webscr"
            Dim strLive As String = "https://www.paypal.com/cgi-bin/webscr"

            Dim req As HttpWebRequest = CType(WebRequest.Create(strLive), HttpWebRequest)

            'Set values for the request back
            req.Method = "POST"
            req.ContentType = "application/x-www-form-urlencoded"
            req.ContentLength = strRequest.Length

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 Or SecurityProtocolType.Tls Or SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12

            'Send the request to PayPal and get the response
            Dim streamOut As StreamWriter = New StreamWriter(req.GetRequestStream(), Encoding.ASCII)
            streamOut.Write(strRequest)
            streamOut.Close()
            Dim streamIn As StreamReader = New StreamReader(req.GetResponse().GetResponseStream())
            Dim strResponse As String = streamIn.ReadToEnd()
            streamIn.Close()

            If Not String.IsNullOrEmpty(strResponse) Then
                Dim results As New Dictionary(Of String, String)
                Dim reader As New StringReader(strResponse)
                Dim line As String = reader.ReadLine()
                If line = "SUCCESS" Then
                    locPurchaseMessage = "<p><h1>Thank you for your purchase.</h1></p>"
                    locPurchaseMessage = locPurchaseMessage + "<p><h3>Your order has been received.</h3></p>"
                    locPurchaseMessage = locPurchaseMessage + "<h4>See your PayPal account for your receipt.</h4>"

                    While True
                        Dim aLine As String
                        aLine = reader.ReadLine
                        If aLine IsNot Nothing Then
                            Dim strArr() As String
                            strArr = aLine.Split("=")
                            results.Add(strArr(0), strArr(1))
                        Else
                            Exit While
                        End If
                    End While

                    Dim locTraineeID As Long = 0

                    Dim locLoginSecurity As New ClsLoginSecurity
                    If results.Keys.Contains("custom") Then
                        Dim locMessage As String = ""
                        Long.TryParse(results("custom"), locTraineeID)

                        locLoginSecurity.ProcessTraineeID(locTraineeID, locMessage)
                    End If

                    Dim locAmount As Double = 0
                    Double.TryParse(results("payment_gross"), locAmount)

                    SaveOrder(locAmount, results("payment_type"), results("item_number"), txToken, locTraineeID)

                    locPurchaseMessage = locPurchaseMessage + "<hr>"
                ElseIf line = "FAIL" Then
                    'log for manual investigation
                    locPurchaseMessage = "<h2>Unable to retrieve purchase details from PayPal. Please refer to your PayPal account for your receipt.</h2>"
                End If
            Else
                locPurchaseMessage = "<h2>Unknown Error. Please check your PayPal account for more information.</h2>"
            End If
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try

        lblPurchase.Text = locPurchaseMessage
    End Sub

    Private Sub SaveOrder(ByVal reqOrderAmount As Double, ByVal reqPaymentType As String, ByVal reqPayPalItemID As String, ByVal reqTransactionID As String, ByVal reqTraineeID As Long)
        Try
            Dim locSaveOrder As New WESOrder_Save()
            Dim locSaveOrderInput As New WESOrder_Save.ClsInputParamsSave()
            Dim locSaveOrderResult As New ClsDataSearchReturnSave()

            locSaveOrderInput.reqOrderAmount = reqOrderAmount
            locSaveOrderInput.reqOrderDate = DateTime.Now
            locSaveOrderInput.reqPaymentType = reqPaymentType
            locSaveOrderInput.reqPayPalItemID = reqPayPalItemID
            locSaveOrderInput.reqPayPalTransID = reqTransactionID
            locSaveOrderInput.reqTraineeID = reqTraineeID
            locSaveOrder.zValInput = locSaveOrderInput

            locSaveOrder.zProSave(locSaveOrderResult)
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub

End Class
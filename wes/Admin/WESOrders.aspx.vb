Imports System.Reflection
Imports WESClass.WESAppVars
Imports WESClass.WESData
Imports WESClass.WESObjects
Imports WESClass.WESSessions

Public Class WESOrders
    Inherits System.Web.UI.Page

    'ReadOnly zVarOrderArray As New ClsWESOrder_Array
    Private Property zVarOrders As New List(Of ClsWESOrder)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadOrders()
        Else
            If Not IsNothing(ViewState("WESORDER")) Then
                zVarOrders = ViewState("WESORDER")
            End If
        End If
    End Sub

    Protected Sub btnRefreshGrid_Click(sender As Object, e As EventArgs) Handles btnRefreshGrid.Click
        linqOrders.DataBind()
        gvOrders.DataBind()
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

    Private Sub LoadOrders()
        Try
            zVarOrders = New List(Of ClsWESOrder)
            Dim locLoadOrder As New WESOrder_Search
            Dim locLoadOrderInput As New WESOrder_Search.ClsInputParamsAllOrders
            Dim locLoadOrderResults As New ClsDataSearchReturn

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
            locErrorHandler.EmailError(locMB.Name + ": Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub

End Class
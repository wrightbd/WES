Imports System.Reflection
Imports WESClass.WESAppVars
Imports WESClass.WESData
Imports WESClass.WESObjects

Namespace Members

    Public Class MediaCenter
        Inherits System.Web.UI.Page

        'Private Property zVarMedia As New List(Of ClsMediaItem)

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            'If Not IsPostBack Then
            '    LoadMedia()
            'Else
            '    If Not IsNothing(ViewState("WESMEDIA")) Then
            '        zVarMedia = ViewState("WESMEDIA")
            '    End If
            'End If
        End Sub

        'Protected Sub linqHeaders_Selecting(sender As Object, e As LinqDataSourceSelectEventArgs) Handles linqHeaders.Selecting
        '    Try
        '        If IsNothing(zVarMedia) Then LoadMedia()

        '        e.Result = From x In zVarMedia
        '            Group x By CategoryName = x.CategoryName Into Group
        '            Select CategoryName, Items = Group
        '    Catch ex As Exception
        '        Dim locErrorHandler As New ErrorLogging
        '        Dim locSF As StackFrame = New StackFrame()
        '        Dim locMB As MethodBase = locSF.GetMethod()
        '        Dim locClass As System.Type = locMB.DeclaringType
        '        locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        '    End Try
        'End Sub

        'Protected Sub rptHeaders_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptHeaders.ItemDataBound
        '    Try
        '        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
        '            Dim locChildRepeater As Repeater = e.Item.FindControl("childRepeater")

        '            locChildRepeater.DataSource = e.Item.DataItem.Items
        '            locChildRepeater.DataBind()
        '        End If
        '    Catch ex As Exception
        '        Dim locErrorHandler As New ErrorLogging
        '        Dim locSF As StackFrame = New StackFrame()
        '        Dim locMB As MethodBase = locSF.GetMethod()
        '        Dim locClass As System.Type = locMB.DeclaringType
        '        locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        '    End Try
        'End Sub

        'Private Sub LoadMedia()
        '    Try
        '        zVarMedia = New List(Of ClsMediaItem)

        '        Dim locLoadMedia As New WESMediaItems_Search
        '        Dim locLoadMediaInput As New WESMediaItems_Search.ClsInputParamsAllItems
        '        Dim locLoadMediaResult As New ClsDataSearchReturn

        '        locLoadMedia.zValInput = locLoadMediaInput

        '        If locLoadMedia.zProSearch(locLoadMediaResult) Then
        '            zVarMedia = locLoadMediaResult.zValReturnList
        '        End If

        '        ViewState("WESMEDIA") = zVarMedia

        '        linqHeaders.DataBind()
        '        rptHeaders.DataBind()
        '    Catch ex As Exception
        '        Dim locErrorHandler As New ErrorLogging
        '        Dim locSF As StackFrame = New StackFrame()
        '        Dim locMB As MethodBase = locSF.GetMethod()
        '        Dim locClass As System.Type = locMB.DeclaringType
        '        locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        '    End Try
        'End Sub


    End Class
End NameSpace
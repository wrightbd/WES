Imports System.Reflection
Imports WESClass.WESAppVars
Imports WESClass.WESData
Imports WESClass.WESObjects

Public Class WESTraineeVideos
    Inherits System.Web.UI.Page

    Private Property zVarVideos As List(Of ClsTraineeVideo)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ClearForm()
            LoadVideos()
        Else
            If Not IsNothing(ViewState("WESVIDEOS")) Then
                zVarVideos = ViewState("WESVIDEOS")
            End If
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadVideos()
    End Sub

    Protected Sub linqVideos_Selecting(sender As Object, e As LinqDataSourceSelectEventArgs) Handles linqVideos.Selecting
        Try
            e.Result = From x In zVarVideos
                       Select x.TraineeVideoID, x.TraineeName, x.DateCreated, x.VideoStatusName, YouTubeLink = If(x.YouTubeID.ToLower().Contains("http"), x.YouTubeID, "http://www.youtube.com/watch?v=" + x.YouTubeID)
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub


    Private Sub ClearForm()
        ddlStatus.SelectedIndex = 0

        txtStartDate.Text = DateTime.Today.AddYears(-1).ToString("MM/dd/yyyy")
        txtEndDate.Text = DateTime.Today.AddDays(1).ToString("MM/dd/yyyy")
    End Sub

    Private Sub LoadVideos()
        Try
            zVarVideos = New List(Of ClsTraineeVideo)

            Dim locSearch As New WESTraineeVideo_Search
            Dim locSearchInput As New WESTraineeVideo_Search.ClsInputParamsDateRange
            Dim locSearchResult As New ClsDataSearchReturn

            locSearchInput.reqVideoStatus = ddlStatus.SelectedValue
            DateTime.TryParse(txtStartDate.Text, locSearchInput.reqStartDate)
            DateTime.TryParse(txtEndDate.Text, locSearchInput.reqEndDate)
            locSearch.zValInput = locSearchInput

            If locSearch.zProSearch(locSearchResult) Then
                zVarVideos = locSearchResult.zValReturnList
            End If

            ViewState("WESVIDEOS") = zVarVideos

            linqVideos.DataBind()
            gvVideos.DataBind()
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub

End Class
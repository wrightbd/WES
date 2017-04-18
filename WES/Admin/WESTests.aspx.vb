Imports System.Reflection
Imports WESClass.WESAppVars
Imports WESClass.WESData
Imports WESClass.WESObjects
Imports WESClass.WESSessions

Public Class WESTests
    Inherits System.Web.UI.Page

    'ReadOnly zVarTestArray As New ClsTest_Array
    Private Property TestArray As List(Of ClsTest)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadTests()
            ClearAddEditForm()
        End If
    End Sub

    Protected Sub btnAddTest_Click(sender As Object, e As EventArgs) Handles btnAddTest.Click
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
        linqTests.DataBind()
        gvTests.DataBind()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim locMessage As String = ""

        If SaveForm(locMessage) Then
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "CloseCompanyClientScript", "javascript:CloseDialogSuccess();", True)
        Else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "CloseCompanyClientScript", "javascript:CloseDialogError('" + locMessage.Replace("'", "''") + "');", True)
        End If
    End Sub

    Protected Sub gvTests_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvTests.RowCommand
        Try
            Dim locRowIndex As Integer = e.CommandArgument
            Dim locTestID As Long = gvTests.DataKeys(locRowIndex).Value

            If e.CommandName = "EditTest" Then
                Dim locTest As ClsTest = Nothing

                Dim locLoadTest As New WESTest_Search
                Dim locLoadTestInput As New WESTest_Search.ClsInputParamsTest
                Dim locLoadTestResult As New ClsDataSearchReturn

                locLoadTestInput.reqTestID = locTestID
                locLoadTest.zValInput = locLoadTestInput

                If locLoadTest.zProSearch(locLoadTestResult) Then
                    If locLoadTestResult.zValReturnList.Count > 0 Then
                        locTest = locLoadTestResult.zValReturnList(0)
                    End If
                End If

                If Not IsNothing(locTest) AndAlso locTest.TestID > 0 Then
                    ClearAddEditForm()

                    txtTestName.Text = locTest.TestName
                    hiddenTestID.Value = locTest.TestID

                    txtDescription.Text = locTest.TestDesc

                    txtMemberCost.Text = locTest.TestCostMember.ToString()

                    txtPassingGrade.Text = locTest.PassingGrade.ToString()

                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "AddClientScript", "javascript: OpenDialog();", True)
                End If
            ElseIf e.CommandName = "RemoveTest" Then
                Dim locSaveTest As New WESTest_Save
                Dim locSaveTestInput As New WESTest_Save.ClsInputParamsTestDelete
                Dim locSaveTestResults As New ClsDataSearchReturnSave

                locSaveTestInput.reqTestID = locTestID
                locSaveTest.zValInput = locSaveTestInput

                If locSaveTest.zProSearch(locSaveTestResults) Then
                    If locSaveTestResults.zValUpdateStatus = 1 Then
                        Dim locTest As ClsTest = TestArray.FirstOrDefault(Function(x) x.TestID = locTestID)

                        LoadTests()
                    Else
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "AddClientScript", "alert('Error removing Test: " + locSaveTestResults.zValUpdateMessage + "');", True)
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

    Protected Sub linqTests_Selecting(sender As Object, e As LinqDataSourceSelectEventArgs) Handles linqTests.Selecting
        Try
            If IsNothing(TestArray) Then LoadTests()

            e.Result = TestArray
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub


    Private Sub ClearAddEditForm()
        hiddenTestID.Value = 0
        txtTestName.Text = ""

        txtDescription.Text = ""
        txtMemberCost.Text = "0"

        txtPassingGrade.Text = "0"
    End Sub

    Private Sub LoadTests()
        Try
            TestArray = New List(Of ClsTest)
            Dim locLoadTest As New WESTest_Search
            Dim locLoadTestInput As New WESTest_Search.ClsInputParamsAllTests
            Dim locLoadTestResults As New ClsDataSearchReturn

            locLoadTest.zValInput = locLoadTestInput

            If locLoadTest.zProSearch(locLoadTestResults) Then
                TestArray = locLoadTestResults.zValReturnList
            End If

            linqTests.DataBind()
            gvTests.DataBind()
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
            Dim locSaveTest As New WESTest_Save
            Dim locSaveTestInput As New WESTest_Save.ClsInputParamsTestSave
            Dim locSaveTestResults As New ClsDataSearchReturnSave

            locSaveTestInput.reqTestID = hiddenTestID.Value
            locSaveTestInput.reqTestName = txtTestName.Text
            locSaveTestInput.reqDescription = txtDescription.Text

            locSaveTestInput.reqMemberCost = txtMemberCost.Text
            locSaveTestInput.reqNonMemberCost = txtMemberCost.Text

            locSaveTestInput.reqPassingGrade = txtPassingGrade.Text

            locSaveTest.zValInput = locSaveTestInput

            If locSaveTest.zProSearch(locSaveTestResults) Then
                locSuccess = locSaveTestResults.zValUpdateStatus = 1
                reqMessage = locSaveTestResults.zValUpdateMessage

                If locSuccess Then
                    LoadTests()
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
Imports System.Reflection
Imports WESClass.WESAppVars
Imports WESClass.WESData
Imports WESClass.WESObjects
Imports WESClass.WESSessions

Public Class WESTestQuestions
    Inherits System.Web.UI.Page

    'ReadOnly zVarTestArray As New ClsTest_Array
    Private Property TestArray As List(Of ClsTest)
    Private Property QuestionArray As List(Of ClsTestQuestion)

    Private Property OptionArray As List(Of ClsTestQuestionOption)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadTests()

            btnAddQuestion.Visible = ddlTest.SelectedIndex > -1

            If ddlTest.SelectedIndex > -1 Then
                LoadQuestions(ddlTest.SelectedValue)
            Else
                LoadQuestions(0)
            End If

            ClearAddEditForm()
            ClearAnswerForm()
        Else
            If Not IsNothing(ViewState("OptionArray")) Then
                OptionArray = ViewState("OptionArray")
            End If
        End If
    End Sub

    Protected Sub btnAddAnswer_Click(sender As Object, e As EventArgs) Handles btnAddAnswer.Click
        Try
            ClearAnswerForm()

            If Not IsNothing(OptionArray.Where(Function(x) x.TestQuestionID = hiddenTestQuestionID.Value)) AndAlso OptionArray.Where(Function(x) x.TestQuestionID = hiddenTestQuestionID.Value).Count > 0 Then
                hiddenSortOrder.Value = OptionArray.Where(Function(x) x.TestQuestionID = hiddenTestQuestionID.Value).Max(Function(x) x.SortOrder) + 1
            Else
                hiddenSortOrder.Value = 1
            End If

            pnlAnswer.Visible = True
            pnlSave.Visible = False
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub

    Protected Sub btnAddQuestion_Click(sender As Object, e As EventArgs) Handles btnAddQuestion.Click
        Try
            ClearAddEditForm()

            linqAnswers.DataBind()
            gvAnswers.DataBind()
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "AddClientScript", "javascript: OpenDialog();", True)
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub

    Protected Sub btnCancelAnswer_Click(sender As Object, e As EventArgs) Handles btnCancelAnswer.Click
        Try
            ClearAnswerForm()
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub

    Protected Sub btnRefreshAnswer_Click(sender As Object, e As EventArgs) Handles btnRefreshAnswer.Click
        linqAnswers.DataBind()
        gvAnswers.DataBind()
    End Sub

    Protected Sub btnRefreshGrid_Click(sender As Object, e As EventArgs) Handles btnRefreshGrid.Click
        linqQuestions.DataBind()
        gvQuestions.DataBind()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim locMessage As String = ""

        If SaveQuestion(locMessage) Then
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "CloseCompanyClientScript", "javascript:CloseDialogSuccess();", True)
        Else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "CloseCompanyClientScript", "javascript:CloseDialogError('" + locMessage.Replace("'", "''") + "');", True)
        End If
    End Sub

    Protected Sub btnSaveAnswer_Click(sender As Object, e As EventArgs) Handles btnSaveAnswer.Click
        Dim locMessage As String = ""

        Try
            If hiddenTestQuestionID.Value > 0 Then
                If SaveAnswer(locMessage) Then
                    pnlAnswer.Visible = False
                    pnlSave.Visible = True

                    linqAnswers.DataBind()
                    gvAnswers.DataBind()
                Else
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "CloseCompanyClientScript", "javascript:CloseAnswerError('" + locMessage.Replace("'", "''") + "');", True)
                End If
            Else
                Dim locAnswer As ClsTestQuestionOption

                locAnswer = New ClsTestQuestionOption
                locAnswer.TestQuestionOptionID = 0

                locAnswer.TestQuestionID = hiddenTestQuestionID.Value
                locAnswer.TestID = hiddenTestID.Value
                locAnswer.SortOrder = hiddenSortOrder.Value

                locAnswer.OptionText = txtAnswer.Text

                If chkCorrect.Checked Then
                    locAnswer.CorrectAnswer = 1
                Else
                    locAnswer.CorrectAnswer = 0
                End If

                OptionArray.Add(locAnswer)

                pnlAnswer.Visible = False
                pnlSave.Visible = True

                linqAnswers.DataBind()
                gvAnswers.DataBind()
            End If
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try

    End Sub

    Protected Sub ddlTest_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTest.SelectedIndexChanged
        LoadQuestions(ddlTest.SelectedValue)
        LoadAnswers(ddlTest.SelectedValue)
    End Sub

    Protected Sub gvAnswers_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvAnswers.RowCommand
        Try
            Dim locRowIndex As Integer = e.CommandArgument
            Dim locTestQuestionOptionID As Long = gvAnswers.DataKeys(locRowIndex).Value
            Dim locAnswer As ClsTestQuestionOption = Nothing

            If e.CommandName = "RemoveAnswer" Then
                If hiddenTestQuestionID.Value > 0 Then
                    Dim locSaveTest As New WESTestQuestionOption_Save
                    Dim locSaveTestInput As New WESTestQuestionOption_Save.ClsInputParamsTestQuestionOptionDelete
                    Dim locSaveTestResults As New ClsDataSearchReturnSave

                    locSaveTestInput.reqTestQuestionOptionID = locTestQuestionOptionID
                    locSaveTest.zValInput = locSaveTestInput

                    If locSaveTest.zProSearch(locSaveTestResults) Then
                        If locSaveTestResults.zValUpdateStatus = 1 Then
                            locAnswer = OptionArray.FirstOrDefault(Function(x) x.TestQuestionOptionID = locTestQuestionOptionID)

                            OptionArray.Remove(locAnswer)

                            linqAnswers.DataBind()
                            gvAnswers.DataBind()
                        Else
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "AddClientScript", "alert('Error removing Test: " + locSaveTestResults.zValUpdateMessage.Replace("'", "''") + "');", True)
                        End If
                    End If
                Else
                    locAnswer = OptionArray.FirstOrDefault(Function(x) x.TestQuestionOptionID = locTestQuestionOptionID)

                    OptionArray.Remove(locAnswer)

                    linqAnswers.DataBind()
                    gvAnswers.DataBind()
                End If
            ElseIf e.CommandName = "EditAnswer" Then
                Dim locLoadAnswer As New WESTestQuestionOption_Search
                Dim locLoadAnswerInput As New WESTestQuestionOption_Search.ClsInputParamsTestQuestionOption
                Dim locLoadAnswerResult As New ClsDataSearchReturn

                locLoadAnswerInput.reqTestQuestionOptionID = locTestQuestionOptionID
                locLoadAnswer.zValInput = locLoadAnswerInput

                If locLoadAnswer.zProSearch(locLoadAnswerResult) Then
                    If locLoadAnswerResult.zValReturnList.Count > 0 Then
                        locAnswer = locLoadAnswerResult.zValReturnList(0)
                    End If
                End If

                ClearAnswerForm()

                If Not IsNothing(locAnswer) Then
                    hiddenTestQuestionOptionID.Value = locAnswer.TestQuestionOptionID
                    txtAnswer.Text = locAnswer.OptionText
                    chkCorrect.Checked = locAnswer.CorrectAnswer = 1
                    hiddenSortOrder.Value = locAnswer.SortOrder
                End If

                pnlAnswer.Visible = True
                pnlSave.Visible = False
            End If
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub

    Protected Sub gvQuestions_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvQuestions.RowCommand
        Try
            Dim locRowIndex As Integer = e.CommandArgument
            Dim locTestQuestionID As Long = gvQuestions.DataKeys(locRowIndex).Value

            If e.CommandName = "EditQuestion" Then
                Dim locQuestion As ClsTestQuestion = Nothing

                Dim locLoadQuestion As New WESTestQuestion_Search
                Dim locLoadQuestionInput As New WESTestQuestion_Search.ClsInputParamsTestQuestion
                Dim locLoadQuestionResult As New ClsDataSearchReturn

                locLoadQuestionInput.reqTestQuestionID = locTestQuestionID
                locLoadQuestion.zValInput = locLoadQuestionInput

                If locLoadQuestion.zProSearch(locLoadQuestionResult) Then
                    If locLoadQuestionResult.zValReturnList.Count > 0 Then
                        locQuestion = locLoadQuestionResult.zValReturnList(0)
                    End If
                End If

                If Not IsNothing(locQuestion) AndAlso locQuestion.TestQuestionID > 0 Then
                    ClearAddEditForm()

                    hiddenTestQuestionID.Value = locQuestion.TestQuestionID
                    hiddenTestID.Value = locQuestion.TestID

                    txtQuestion.Text = locQuestion.Question
                    txtExplanation.Text = locQuestion.AnswerExplanation

                    Dim locLoadAnswer As New WESTestQuestionOption_Search
                    Dim locLoadAnswerInput As New WESTestQuestionOption_Search.ClsInputParamsTestQuestion
                    Dim locLoadAnswerResult As New ClsDataSearchReturn

                    locLoadAnswerInput.reqTestQuestionID = locQuestion.TestQuestionID
                    locLoadAnswer.zValInput = locLoadAnswerInput

                    If locLoadAnswer.zProSearch(locLoadAnswerResult) Then
                        OptionArray = locLoadAnswerResult.zValReturnList
                        ViewState("OptionArray") = OptionArray
                    End If

                    linqAnswers.DataBind()
                    gvAnswers.DataBind()

                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "AddClientScript", "javascript: OpenDialog();", True)
                End If
            ElseIf e.CommandName = "RemoveQuestion" Then
                Dim locSaveTest As New WESTestQuestion_Save
                Dim locSaveTestInput As New WESTestQuestion_Save.ClsInputParamsTestQuestionDelete
                Dim locSaveTestResults As New ClsDataSearchReturnSave

                locSaveTestInput.reqTestQuestionID = locTestQuestionID
                locSaveTest.zValInput = locSaveTestInput

                If locSaveTest.zProSearch(locSaveTestResults) Then
                    If locSaveTestResults.zValUpdateStatus = 1 Then
                        LoadQuestions(ddlTest.SelectedValue)
                    Else
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "AddClientScript", "alert('Error removing Test: " + locSaveTestResults.zValUpdateMessage.Replace("'", "''") + "');", True)
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

    Protected Sub linqAnswers_Selecting(sender As Object, e As LinqDataSourceSelectEventArgs) Handles linqAnswers.Selecting
        Try
            If IsNothing(OptionArray) Then LoadAnswers(ddlTest.SelectedValue)
            Dim locTestQuestionID As Long = 0

            Long.TryParse(hiddenTestQuestionID.Value, locTestQuestionID)

            e.Result = OptionArray.Where(Function(x) x.TestQuestionID = locTestQuestionID).OrderBy(Function(x) x.SortOrder)
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub

    Protected Sub linqQuestions_Selecting(sender As Object, e As LinqDataSourceSelectEventArgs) Handles linqQuestions.Selecting
        Try
            If IsNothing(QuestionArray) Then LoadQuestions(ddlTest.SelectedValue)

            e.Result = QuestionArray.OrderBy(Function(x) x.Question)
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub

    Protected Sub linqTest_Selecting(sender As Object, e As LinqDataSourceSelectEventArgs) Handles linqTest.Selecting
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
        hiddenTestQuestionID.Value = 0

        If ddlTest.SelectedIndex > -1 Then
            hiddenTestID.Value = ddlTest.SelectedValue
        Else
            hiddenTestID.Value = 0
        End If

        txtQuestion.Text = ""
        txtExplanation.Text = ""
    End Sub

    Private Sub ClearAnswerForm()
        pnlAnswer.Visible = False
        pnlSave.Visible = True
        hiddenTestQuestionOptionID.Value = 0
        hiddenSortOrder.Value = 0

        txtAnswer.Text = ""
        chkCorrect.Checked = False
    End Sub

    Private Sub LoadAnswers(ByVal reqTestID As Long)
        Try
            OptionArray = New List(Of ClsTestQuestionOption)
            Dim locLoadTest As New WESTestQuestionOption_Search
            Dim locLoadTestInput As New WESTestQuestionOption_Search.ClsInputParamsTest
            Dim locLoadTestResults As New ClsDataSearchReturn

            locLoadTestInput.reqTestID = reqTestID
            locLoadTest.zValInput = locLoadTestInput

            If locLoadTest.zProSearch(locLoadTestResults) Then
                OptionArray = locLoadTestResults.zValReturnList
            End If
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub

    Private Sub LoadQuestions(ByVal reqTestID As Long)
        Try
            QuestionArray = New List(Of ClsTestQuestion)
            Dim locLoadTest As New WESTestQuestion_Search
            Dim locLoadTestInput As New WESTestQuestion_Search.ClsInputParamsTest
            Dim locLoadTestResults As New ClsDataSearchReturn

            locLoadTestInput.reqTestID = reqTestID
            locLoadTest.zValInput = locLoadTestInput

            If locLoadTest.zProSearch(locLoadTestResults) Then
                QuestionArray = locLoadTestResults.zValReturnList
            End If

            linqQuestions.DataBind()
            gvQuestions.DataBind()
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
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

            linqTest.DataBind()
            ddlTest.DataBind()
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub

    Private Function SaveAnswer(ByRef reqMessage As String) As Boolean
        Dim locSuccess As Boolean = False

        Try
            Dim locSaveTest As New WESTestQuestionOption_Save
            Dim locSaveTestInput As New WESTestQuestionOption_Save.ClsInputParamsTestQuestionOptionSave
            Dim locSaveTestResults As New ClsDataSearchReturnSave

            locSaveTestInput.reqTestQuestionOptionID = hiddenTestQuestionOptionID.Value
            locSaveTestInput.reqTestQuestionID = hiddenTestQuestionID.Value
            locSaveTestInput.reqTestID = hiddenTestID.Value

            locSaveTestInput.reqOptionText = txtAnswer.Text
            locSaveTestInput.reqSortOrder = hiddenSortOrder.Value

            If chkCorrect.Checked Then
                locSaveTestInput.reqCorrectAnswer = 1
            Else
                locSaveTestInput.reqCorrectAnswer = 0
            End If

            locSaveTest.zValInput = locSaveTestInput

            If locSaveTest.zProSearch(locSaveTestResults) Then
                locSuccess = locSaveTestResults.zValUpdateStatus = 1
                reqMessage = locSaveTestResults.zValUpdateMessage

                If locSuccess Then
                    LoadAnswers(ddlTest.SelectedValue)
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

    Private Function SaveQuestion(ByRef reqMessage As String) As Boolean
        Dim locSuccess As Boolean = False

        Try
            Dim locSaveTest As New WESTestQuestion_Save
            Dim locSaveTestInput As New WESTestQuestion_Save.ClsInputParamsTestQuestionSave
            Dim locSaveTestResults As New ClsDataSearchReturnSave

            locSaveTestInput.reqTestQuestionID = hiddenTestQuestionID.Value
            locSaveTestInput.reqTestID = hiddenTestID.Value

            locSaveTestInput.reqQuestion = txtQuestion.Text
            locSaveTestInput.reqAnswerExplanation = txtExplanation.Text

            locSaveTest.zValInput = locSaveTestInput

            If locSaveTest.zProSearch(locSaveTestResults) Then
                locSuccess = locSaveTestResults.zValUpdateStatus = 1
                reqMessage = locSaveTestResults.zValUpdateMessage

                If locSuccess Then
                    LoadQuestions(ddlTest.SelectedValue)
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
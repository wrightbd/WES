Imports System.Reflection
Imports WESClass.WESAppVars
Imports WESClass.WESData
Imports WESClass.WESObjects
Imports WESClass.WESSessions

Public Class WESPTC
    Inherits System.Web.UI.Page

    ReadOnly zVarLoginSession As New ClsLoginUser_Session

    Private Property QuestionArray As List(Of ClsTestQuestion)
    Private Property OptionArray As List(Of ClsTestQuestionOption)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If zVarLoginSession.TraineeID <= 0 Then
        '    Response.Redirect("~/Login.aspx", True)
        'ElseIf zVarLoginSession.TestAvailable = 0 Then
        '    Response.Redirect("~/ChoosePlan.aspx", True)
        'End If

        If Not IsPostBack Then
            LoadTest()
        Else
            If Not IsNothing(ViewState("WESQ")) Then
                QuestionArray = ViewState("WESQ")
            End If

            If Not IsNothing(ViewState("WESQO")) Then
                OptionArray = ViewState("WESQO")
            End If

            BuildTable()
        End If
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim locMessage As String = ""

        pnlTest.Visible = True
        pnlResult.Visible = False
        lblResult.Text = ""

        If SaveGradeTest(locMessage) Then
            pnlTest.Visible = False
            pnlResult.Visible = True
            lblResult.Text = locMessage
        Else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "ErrorClientScript", "javascript:alert('" + locMessage.Replace("'", "''") + "');", True)
        End If
    End Sub

    Private Sub BuildTable()
        Dim locQuestionNum As Integer = 1
        For Each locTestQuestion As ClsTestQuestion In QuestionArray
            Dim locRow As New TableRow

            Dim locQuestionCell As New TableCell
            locQuestionCell.ColumnSpan = 2

            Dim locLabel As New Label
            locLabel.ID = "lbl" + locTestQuestion.TestQuestionID.ToString
            locLabel.Text = locQuestionNum.ToString() + ") " + locTestQuestion.Question + "&nbsp;&nbsp;&nbsp;"
            locQuestionCell.Controls.Add(locLabel)
            locQuestionCell.HorizontalAlign = HorizontalAlign.Left
            locRow.Cells.Add(locQuestionCell)
            tblTest.Rows.Add(locRow)


            locRow = New TableRow
            Dim locBlankCell As New TableCell
            locBlankCell.Text = "&nbsp;"
            locBlankCell.VerticalAlign = VerticalAlign.Top
            locBlankCell.Width = Unit.Percentage(5)


            Dim locAnswerCell As New TableCell
            locAnswerCell.Width = Unit.Percentage(95)
            Dim locRadioList As New RadioButtonList
            locRadioList.ID = "rbl" + locTestQuestion.TestQuestionID.ToString
            locRadioList.CssClass = "radio"
            locRadioList.CellPadding = 5

            Dim locOptionNumber As Integer = 1
            For Each locTestOption As ClsTestQuestionOption In OptionArray.Where(Function(x) x.TestQuestionID = locTestQuestion.TestQuestionID).OrderBy(Function(s) s.SortOrder)
                Dim locItem As New ListItem
                If locOptionNumber = 4 Then
                    locItem.Text = "d) " + locTestOption.OptionText
                ElseIf locOptionNumber = 3 Then
                    locItem.Text = "c) " + locTestOption.OptionText
                ElseIf locOptionNumber = 2 Then
                    locItem.Text = "b) " + locTestOption.OptionText
                Else
                    locItem.Text = "a) " + locTestOption.OptionText
                End If

                locItem.Value = locOptionNumber
                locRadioList.Items.Add(locItem)

                locOptionNumber += 1
            Next


            Dim locValidation As New RequiredFieldValidator
            locValidation.ControlToValidate = locRadioList.ID
            locValidation.ErrorMessage = "Answer required"
            locValidation.ForeColor = Drawing.Color.Red
            locValidation.Font.Bold = True


            Dim locHidden As New HiddenField
            locHidden.ID = "hidden" + locTestQuestion.TestQuestionID.ToString()
            locHidden.Value = locTestQuestion.TestQuestionID


            locLabel = New Label
            locLabel.Text = "&nbsp;"

            locAnswerCell.Controls.Add(locRadioList)
            locAnswerCell.Controls.Add(locHidden)
            locAnswerCell.Controls.Add(locLabel)
            locQuestionCell.Controls.Add(locValidation)


            locRow.Cells.Add(locBlankCell)
            locRow.Cells.Add(locAnswerCell)

            tblTest.Rows.Add(locRow)

            locQuestionNum += 1
        Next
    End Sub

    Private Sub LoadTest()
        Try
            hiddenTestID.Value = WESTestValues.PersonalTraining
            hiddenTraineeID.Value = zVarLoginSession.TraineeID

            pnlTest.Visible = True
            pnlResult.Visible = False
            lblResult.Text = ""

            Dim locTest As New WESTest_Search
            Dim locTestInput As New WESTest_Search.ClsInputParamsTest
            Dim locTestResult As New ClsDataSearchReturn

            locTestInput.reqTestID = hiddenTestID.Value
            locTest.zValInput = locTestInput

            If locTest.zProSearch(locTestResult) Then
                If locTestResult.zValReturnList.Count > 0 Then
                    Dim locReturnTest As ClsTest = locTestResult.zValReturnList(0)
                    lblTestName.Text = locReturnTest.TestName
                End If
            End If

            Dim locTestQuestions As New WESTestQuestion_Search
            Dim locTestQuestionsInput As New WESTestQuestion_Search.ClsInputParamsTestRandom
            Dim locTestQuestionsResult As New ClsDataSearchReturn

            locTestQuestionsInput.reqTestID = WESTestValues.PersonalTraining
            locTestQuestions.zValInput = locTestQuestionsInput

            If locTestQuestions.zProSearch(locTestQuestionsResult) Then
                QuestionArray = locTestQuestionsResult.zValReturnList
                ViewState("WESQ") = QuestionArray

                Dim locTestOptions As New WESTestQuestionOption_Search
                Dim locTestOptionsInput As New WESTestQuestionOption_Search.ClsInputParamsTest
                Dim locTestOptionsResult As New ClsDataSearchReturn

                locTestOptionsInput.reqTestID = WESTestValues.PersonalTraining
                locTestOptions.zValInput = locTestOptionsInput

                If locTestOptions.zProSearch(locTestOptionsResult) Then
                    OptionArray = locTestOptionsResult.zValReturnList

                    ViewState("WESQO") = OptionArray
                End If
            End If

            BuildTable()
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub

    Private Function SaveGradeTest(ByRef reqMessage As String) As Boolean
        Dim locSuccess As Boolean = True

        Try
            Dim locTotalQuestions As Integer = QuestionArray.Count
            Dim locNumWrong As Integer = 0

            Dim locQuestionID As Long = 0
            For Each locRow As TableRow In tblTest.Rows
                If locRow.Cells.Count = 2 Then
                    Dim locCell As TableCell = locRow.Cells(1)
                    Dim locHidden As HiddenField = locCell.Controls(1)
                    Dim locRadioList As RadioButtonList = locCell.Controls(0)

                    locQuestionID = locHidden.Value

                    Dim locTestAnswers As List(Of ClsTestQuestionOption) = OptionArray.Where(Function(x) x.TestQuestionID = locQuestionID).ToList()

                    Dim locCorrectAnswer As String = ""
                    Dim locTraineeAnswer As String = ""
                    For Each locAnswer As ClsTestQuestionOption In locTestAnswers
                        If locAnswer.SortOrder = locRadioList.SelectedValue Then
                            locTraineeAnswer = locAnswer.OptionText

                            If locAnswer.CorrectAnswer <> 1 Then
                                locNumWrong += 1
                            Else
                                locCorrectAnswer = locAnswer.OptionText
                            End If
                        ElseIf locAnswer.CorrectAnswer = 1 Then
                            locCorrectAnswer = locAnswer.OptionText
                        End If
                    Next

                    Try
                        Dim locTestAnswer As New WESTestAnswer_Save
                        Dim locTestAnswerInput As New WESTestAnswer_Save.ClsInputParamsSave
                        Dim locTestAnswerResult As New ClsDataSearchReturnSave

                        locTestAnswerInput.reqTraineeID = hiddenTraineeID.Value
                        locTestAnswerInput.reqTestID = hiddenTestID.Value
                        locTestAnswerInput.reqTestQuestionID = locQuestionID

                        locTestAnswerInput.reqTraineeAnswer = locTraineeAnswer
                        locTestAnswerInput.reqCorrectAnswer = locCorrectAnswer
                        locTestAnswer.zValInput = locTestAnswerInput

                        locTestAnswer.zProSearch(locTestAnswerResult)
                    Catch ex As Exception
                        Dim locErrorHandler As New ErrorLogging
                        Dim locSF As StackFrame = New StackFrame()
                        Dim locMB As MethodBase = locSF.GetMethod()
                        Dim locClass As System.Type = locMB.DeclaringType
                        locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
                    End Try
                End If
            Next

            Dim locScoreNeeded As Double = 100

            Dim locTest As New WESTest_Search
            Dim locTestInput As New WESTest_Search.ClsInputParamsTest
            Dim locTestResult As New ClsDataSearchReturn

            locTestInput.reqTestID = hiddenTestID.Value
            locTest.zValInput = locTestInput

            If locTest.zProSearch(locTestResult) Then
                If locTestResult.zValReturnList.Count > 0 Then
                    Dim locReturnTest As ClsTest = locTestResult.zValReturnList(0)
                    locScoreNeeded = locReturnTest.PassingGrade
                End If
            End If

            Dim locGrade As Double = ((locTotalQuestions - locNumWrong) / locTotalQuestions) * 100

            Dim locAttempt As New WESTestAttempt_Save
            Dim locAttemptInput As New WESTestAttempt_Save.ClsInputParamsSave
            Dim locAttemptResult As New ClsDataSearchReturnSave

            locAttemptInput.reqTestID = hiddenTestID.Value
            locAttemptInput.reqTraineeID = hiddenTraineeID.Value
            locAttemptInput.reqGrade = locGrade

            If locGrade >= locScoreNeeded Then
                locAttemptInput.reqPass = 1
            Else
                locAttemptInput.reqPass = 0
            End If

            locAttempt.zValInput = locAttemptInput

            If locAttempt.zProSearch(locAttemptResult) Then
                Dim locAnswerUpdate As New WESTestAnswer_Save
                Dim locAnswerUpdateInput As New WESTestAnswer_Save.ClsInputParamsTestAttemptUpdate
                Dim locAnswerUpdateResult As New ClsDataSearchReturnSave

                locAnswerUpdateInput.reqTraineeID = hiddenTraineeID.Value
                locAnswerUpdateInput.reqTestID = hiddenTestID.Value
                locAnswerUpdateInput.reqTestAttemptID = locAttemptResult.zValUpdateID
                locAnswerUpdate.zValInput = locAnswerUpdateInput

                locAnswerUpdate.zProSearch(locAnswerUpdateResult)
            End If


            zVarLoginSession.TestAvailable = 0

            reqMessage = "You missed " + locNumWrong.ToString() + " of " + locTotalQuestions.ToString() + " questions.<br />Your Score is: " + locGrade.ToString("N2")

            If locGrade >= locScoreNeeded Then
                reqMessage = reqMessage + "<br/><br/>Congratulations, you passed this test."
            End If
        Catch ex As Exception
            locSuccess = False
            reqMessage = "There was an error grading the test. Please try again."

            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try

        Return locSuccess
    End Function

End Class
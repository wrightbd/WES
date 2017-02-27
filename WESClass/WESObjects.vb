Imports PetaPoco
Imports WESClass.WESInterface

Namespace WESObjects

    Public Class ClsErrorStatus
        Implements IWESErrorStatus
        Public Property zValStatusError As Integer = 0 Implements IWESErrorStatus.zValStatusError
        Public Property zValStatusErrorCode As String = "" Implements IWESErrorStatus.zValStatusErrorCode
        Public Property zValStatusErrorMessage As String = "" Implements IWESErrorStatus.zValStatusErrorMessage
    End Class

    Public Class ClsDataSearchReturn
        Implements IWESDataSearchReturn
        Public Property zValReturnDataTable As New DataTable() Implements IWESDataSearchReturn.ZValReturnDataTable
        Public Property zValReturnErrorStatus As IWESErrorStatus = New ClsErrorStatus() Implements IWESDataSearchReturn.ZValReturnErrorStatus
        Public Property zValReturnList As IEnumerable(Of Object)
    End Class

    Public Class ClsDataSearchReturnSave
        Implements IWESDataSearchReturn
        Public Property zValReturnDataTable As New DataTable() Implements IWESDataSearchReturn.ZValReturnDataTable
        Public Property zValReturnErrorStatus As IWESErrorStatus = New ClsErrorStatus() Implements IWESDataSearchReturn.ZValReturnErrorStatus
        Public Property zValUpdateStatus As Integer = 0
        Public Property zValUpdateMessage As String = ""
        Public Property zValUpdateID As Long = 0
        Public Property zValUpdateRowGUID As String = ""
    End Class

    <Serializable>
    Public Class ClsCountry
        Public Property ZCountryID As Long = 0
        Public Property CountryName As String = ""
        Public Property Active As Integer = 0
    End Class

    <Serializable>
    Public Class ClsMediaCategory
        Public Property MediaCategoryID As Long = 0
        Public Property CategoryName As String = ""
        Public Property CategoryDescription As String = ""
        Public Property SortOrder As Integer = 0
    End Class

    <Serializable>
    Public Class ClsMediaItem
        Public Property MediaItemID As Long = 0
        Public Property MediaName As String = ""
        Public Property MediaDescription As String = ""
        Public Property MediaCategoryID As Long = 0
        Public Property CategoryName As String = ""
        Public Property WebsiteURL As String = ""
    End Class

    <Serializable>
    Public Class ClsPromoCode
        Public Property PromoCodeID As Long = 0
        Public Property PromoCodeName As String = ""
        Public Property PromoCode As String = ""
        Public Property Active As Integer = 0
        Public Property DateEnd As Nullable(Of DateTime)
        Public Property Discount As Double = 0
    End Class

    <Serializable>
    Public Class ClsState
        Public Property ZStateID As Long = 0
        Public Property StateName As String = ""
        Public Property Abbreviation As String = ""
        Public Property ZCountryID As Long = 0
        Public Property CountryName As String = ""
        Public Property Active As Integer = 0
    End Class

    <Serializable>
    Public Class ClsTest
        Public Property TestID As Long = 0
        Public Property TestName As String = ""
        Public Property TestDesc As String = ""
        Public Property TestCostMember As Double = 0
        Public Property TestCostNonMember As Double = 0
        Public Property PassingGrade As Integer = 0
    End Class

    <Serializable>
    Public Class ClsTestAnswer
        Public Property TestAnswerID As Long = 0
        Public Property TraineeID As Long = 0
        Public Property TestID As Long = 0
        Public Property TestQuestionID As Long = 0
        Public Property Question As String = ""
        Public Property TraineeAnswer As String = ""
        Public Property CorrectAnswer As String = ""
        Public Property DateAttempt As Nullable(Of DateTime)

        Public ReadOnly Property IsWrong As Boolean
            Get
                Return TraineeAnswer <> CorrectAnswer
            End Get
        End Property
    End Class

    <Serializable>
    Public Class ClsTestAttempt
        Public Property TestAttemptID As Long = 0
        Public Property TraineeID As Long = 0
        Public Property TestID As Long = 0
        Public Property TestName As String = ""
        Public Property FirstName As String = ""
        Public Property MiddleName As String = ""
        Public Property LastName As String = ""
        Public Property LastNameSuffix As String = ""
        Public Property AttemptDate As Nullable(Of DateTime)
        Public Property Grade As Double = 0
        Public Property Pass As Integer = 0

        Public ReadOnly Property TraineeName As String
            Get
                Dim locName As String

                locName = LastName

                If LastNameSuffix <> "" Then
                    locName = locName + " " + LastNameSuffix
                End If

                If MiddleName <> "" Then
                    locName = locName + ", " + FirstName + " " + MiddleName
                Else
                    locName = locName + ", " + FirstName
                End If

                Return locName
            End Get
        End Property

        Public ReadOnly Property GradeString As String
            Get
                If Pass = 1 Then
                    Return "P"
                Else
                    Return "F"
                End If
            End Get
        End Property
    End Class

    <Serializable>
    Public Class ClsTestQuestion
        Public Property TestQuestionID As Long = 0
        Public Property TestID As Long = 0
        Public Property TestName As String = ""
        Public Property Question As String = ""
        Public Property AnswerExplanation As String = ""
    End Class

    <Serializable>
    Public Class ClsTestQuestionOption
        Public Property TestQuestionOptionID As Long = 0
        Public Property TestQuestionID As Long = 0
        Public Property TestID As Long = 0
        Public Property OptionText As String = ""
        Public Property CorrectAnswer As Integer = 0
        Public Property SortOrder As Integer = 0
    End Class

    <Serializable>
    Public Class ClsTrainee
        Public Property TraineeID As Long = 0
        Public Property FirstName As String = ""
        Public Property MiddleName As String = ""
        Public Property LastName As String = ""
        Public Property LastNameSuffix As String = ""
        Public Property Address1 As String = ""
        Public Property Address2 As String = ""
        Public Property City As String = ""
        Public Property ZStateID As Long = 0
        Public Property StateName As String = ""
        Public Property ZipCode As String = ""
        Public Property ZCountryID As Long = 0
        Public Property CountryName As String = ""
        Public Property EmailAddress As String = ""
        Public Property PhoneNumber As String = ""
        Public Property Username As String = ""
        Public Property Password As String = ""
        Public Property DateCreated As DateTime
        Public Property EmailVerified As Integer = 0
        Public Property AdminUser As Integer = 0
        Public Property PTEAvailable As Integer = 0
        <Column("rowguid")>
        Public Property TraineeGUID As String = ""

        Public ReadOnly Property TraineeName As String
            Get
                Dim locName As String

                locName = LastName

                If LastNameSuffix <> "" Then
                    locName = locName + " " + LastNameSuffix
                End If

                If MiddleName <> "" Then
                    locName = locName + ", " + FirstName + " " + MiddleName
                Else
                    locName = locName + ", " + FirstName
                End If

                Return locName
            End Get
        End Property
    End Class

    <Serializable>
    Public Class ClsTraineeVideo
        Public Property TraineeVideoID As Long = 0
        Public Property TraineeID As Long = 0
        Public Property FirstName As String = ""
        Public Property MiddleName As String = ""
        Public Property LastName As String = ""
        Public Property LastNameSuffix As String = ""
        Public Property YouTubeID As String = ""
        Public Property DateCreated As Nullable(Of DateTime)
        Public Property VideoStatus As String = ""
        Public Property StatusMessage As String = ""

        Public ReadOnly Property TraineeName As String
            Get
                Dim locName As String

                locName = LastName

                If LastNameSuffix <> "" Then
                    locName = locName + " " + LastNameSuffix
                End If

                If MiddleName <> "" Then
                    locName = locName + ", " + FirstName + " " + MiddleName
                Else
                    locName = locName + ", " + FirstName
                End If

                Return locName
            End Get
        End Property

        Public ReadOnly Property VideoStatusName As String
            Get
                If VideoStatus = 1 Then
                    Return "Approved"
                ElseIf VideoStatus = 2 Then
                    Return "Denied"
                Else
                    Return "Needs Review"
                End If
            End Get
        End Property
    End Class

    <Serializable>
    Public Class ClsWESOrder
        Public Property WESOrderID As Long = 0
        Public Property TraineeID As Long = 0
        Public Property FirstName As String = ""
        Public Property MiddleName As String = ""
        Public Property LastName As String = ""
        Public Property LastNameSuffix As String = ""
        Public Property TestID As Long = 0
        Public Property TestName As String = ""
        Public Property PayPalItemID As String = ""
        Public Property PayPalTransID As String = ""
        Public Property OrderDate As Nullable(Of DateTime)
        Public Property OrderAmount As Double = 0
        Public Property PaymentType As String = ""

        Public ReadOnly Property TraineeName As String
            Get
                Dim locName As String = LastName

                If Not String.IsNullOrEmpty(LastNameSuffix) Then locName = locName + " " + LastNameSuffix

                locName = locName + ", " + FirstName

                If Not String.IsNullOrEmpty(MiddleName) Then locName = locName + " " + MiddleName

                Return locName
            End Get
        End Property
    End Class

End Namespace

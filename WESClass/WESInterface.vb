Namespace WESInterface

    Public Interface IWESDataSearch
        Public Interface IWESDataSearchInput
        End Interface
        Property zValInput As IWESDataSearchInput
        Function zProSearch(ByRef reqDataResults As IWESDataSearchReturn) As Boolean
    End Interface

    Public Interface IWESDataSearchReturn
        Property ZValReturnDataTable As DataTable
        Property ZValReturnErrorStatus As IWESErrorStatus
    End Interface

    Public Interface IWESErrorStatus
        Property zValStatusError As Integer
        Property zValStatusErrorCode As String
        Property zValStatusErrorMessage As String
    End Interface

End Namespace
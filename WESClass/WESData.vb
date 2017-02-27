Imports System.Data.SqlClient
Imports System.Reflection
Imports PetaPoco
Imports WESClass.WESAppVars
Imports WESClass.WESInterface
Imports WESClass.WESObjects

Namespace WESData

#Region "Country"
    Public Class WESCountry_Search
        Implements IWESDataSearch
        Private zlocInputParams As ClsInputParamsAllFields

        Public Sub New()
            zlocInputParams = New ClsInputParamsAllFields()
        End Sub

        Public Property zValInput As IWESDataSearch.IWESDataSearchInput Implements IWESDataSearch.zValInput
            Get
                Return zlocInputParams
            End Get
            Set(ByVal value As IWESDataSearch.IWESDataSearchInput)
                If TypeOf value Is ClsInputParamsAllCountries Then
                    Dim locInput As ClsInputParamsAllCountries = value

                    zlocInputParams.reqSearchType = 1
                Else
                    zlocInputParams = value
                End If
            End Set
        End Property

        Public Class ClsInputParamsAllFields
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqSearchType As Integer = 0
            Public Property reqSearchValueOne As String = "0"
        End Class

        Public Class ClsInputParamsAllCountries
            Implements IWESDataSearch.IWESDataSearchInput

        End Class

        Public Function zProSearch(ByRef reqReturnData As IWESDataSearchReturn) As Boolean Implements IWESDataSearch.zProSearch
            Dim locSuccess As Boolean = False

            Dim locReturnObject As New ClsDataSearchReturn()
            Try
                Dim conn As New SqlConnection()
                conn.ConnectionString = WESSQLConnection.connString
                conn.Open()
                Try
                    Dim locDB As New Database(conn)
                    locDB.EnableAutoSelect = False

                    locReturnObject.zValReturnList = locDB.Query(Of ClsCountry)("exec Proc_ListCountry @0, @1",
                                                                                zlocInputParams.reqSearchType, zlocInputParams.reqSearchValueOne).ToList()
                    locSuccess = True
                Catch ex As Exception
                    locSuccess = False

                    Dim locErrorHandler As New ErrorLogging
                    Dim locSF As StackFrame = New StackFrame()
                    Dim locMB As MethodBase = locSF.GetMethod()
                    Dim locClass As System.Type = locMB.DeclaringType
                    locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
                Finally
                    conn.Close()
                End Try
            Catch ex As Exception
                locSuccess = False

                Dim locErrorHandler As New ErrorLogging
                Dim locSF As StackFrame = New StackFrame()
                Dim locMB As MethodBase = locSF.GetMethod()
                Dim locClass As System.Type = locMB.DeclaringType
                locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
            Finally
                If locSuccess Then
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 0
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "no errors-procedure executed properly"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = "no errors-procedure executed properly"
                Else
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 1
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "550"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = ""
                End If
                reqReturnData = locReturnObject
            End Try

            Return locSuccess
        End Function

    End Class

    Public Class WESCountry_Save
        Implements IWESDataSearch
        Private zlocInputParams As ClsInputParamsAllFields

        Public Sub New()
            zlocInputParams = New ClsInputParamsAllFields()
        End Sub

        Public Property zValInput As IWESDataSearch.IWESDataSearchInput Implements IWESDataSearch.zValInput
            Get
                Return zlocInputParams
            End Get
            Set(ByVal value As IWESDataSearch.IWESDataSearchInput)
                If TypeOf value Is ClsInputParamsSave Then
                    Dim locInput As ClsInputParamsSave = value

                    zlocInputParams.reqZCountryID = locInput.reqZCountryID
                    zlocInputParams.reqCountryName = locInput.reqCountryName
                    zlocInputParams.reqActive = locInput.reqActive

                    zlocInputParams.reqUpdateType = 1
                ElseIf TypeOf value Is ClsInputParamsDelete Then
                    Dim locInput As ClsInputParamsDelete = value

                    zlocInputParams.reqZCountryID = locInput.reqZCountryID

                    zlocInputParams.reqUpdateType = 2
                Else
                    zlocInputParams = value
                End If
            End Set
        End Property

        Public Class ClsInputParamsAllFields
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqUpdateType As Integer = 0
            Public Property reqZCountryID As Long = 0
            Public Property reqCountryName As String = ""
            Public Property reqActive As Integer = 0
        End Class

        Public Class ClsInputParamsSave
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqZCountryID As Long = 0
            Public Property reqCountryName As String = ""
            Public Property reqActive As Integer = 0
        End Class

        Public Class ClsInputParamsDelete
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqZCountryID As Long = 0
        End Class

        Public Function zProSearch(ByRef reqReturnData As IWESDataSearchReturn) As Boolean Implements IWESDataSearch.zProSearch
            Dim locSuccess As Boolean = False

            Dim locReturnObject As New ClsDataSearchReturnSave()
            Try
                Dim conn As New SqlConnection()
                conn.ConnectionString = WESSQLConnection.connString
                Dim cmd As New SqlCommand("Proc_ListCountrySave", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@reqUpdateType", zlocInputParams.reqUpdateType)
                cmd.Parameters.AddWithValue("@reqZCountryID", zlocInputParams.reqZCountryID)
                cmd.Parameters.AddWithValue("@reqCountryName", zlocInputParams.reqCountryName)
                cmd.Parameters.AddWithValue("@reqActive", zlocInputParams.reqActive)

                conn.Open()
                Dim locSdr As SqlDataReader = cmd.ExecuteReader()
                Try
                    If locSdr.Read() Then
                        If Not (locSdr("UpdateStatus") Is DBNull.Value) Then
                            locReturnObject.zValUpdateStatus = locSdr("UpdateStatus") 'Integer
                        End If
                        If Not (locSdr("UpdateMessage") Is DBNull.Value) Then
                            locReturnObject.zValUpdateMessage = locSdr("UpdateMessage") 'String
                        End If
                        If Not (locSdr("ZCountryID") Is DBNull.Value) Then
                            locReturnObject.zValUpdateID = locSdr("ZCountryID") 'Long
                        End If
                    End If

                    locSuccess = True
                Catch ex As Exception
                    locSuccess = False

                    Dim locErrorHandler As New ErrorLogging
                    Dim locSF As StackFrame = New StackFrame()
                    Dim locMB As MethodBase = locSF.GetMethod()
                    Dim locClass As System.Type = locMB.DeclaringType
                    locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
                Finally
                    cmd.Dispose()
                    conn.Close()
                End Try
            Catch ex As Exception
                locSuccess = False

                Dim locErrorHandler As New ErrorLogging
                Dim locSF As StackFrame = New StackFrame()
                Dim locMB As MethodBase = locSF.GetMethod()
                Dim locClass As System.Type = locMB.DeclaringType
                locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
            Finally
                If locSuccess Then
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 0
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "no errors-procedure executed properly"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = "no errors-procedure executed properly"
                Else
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 1
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "550"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = ""
                End If
                reqReturnData = locReturnObject
            End Try

            Return locSuccess
        End Function

    End Class
#End Region

#Region "Media Center"
    Public Class WESMediaCategory_Search
        Implements IWESDataSearch
        Private zlocInputParams As ClsInputParamsAllFields

        Public Sub New()
            zlocInputParams = New ClsInputParamsAllFields()
        End Sub

        Public Property zValInput As IWESDataSearch.IWESDataSearchInput Implements IWESDataSearch.zValInput
            Get
                Return zlocInputParams
            End Get
            Set(ByVal value As IWESDataSearch.IWESDataSearchInput)
                If TypeOf value Is ClsInputParamsAllCategories Then
                    Dim locInput As ClsInputParamsAllCategories = value

                    zlocInputParams.reqSearchType = 1
                Else
                    zlocInputParams = value
                End If
            End Set
        End Property

        Public Class ClsInputParamsAllFields
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqSearchType As Integer = 0
        End Class

        Public Class ClsInputParamsAllCategories
            Implements IWESDataSearch.IWESDataSearchInput

        End Class

        Public Function zProSearch(ByRef reqReturnData As IWESDataSearchReturn) As Boolean Implements IWESDataSearch.zProSearch
            Dim locSuccess As Boolean = False

            Dim locReturnObject As New ClsDataSearchReturn()
            Try
                Dim conn As New SqlConnection()
                conn.ConnectionString = WESSQLConnection.connString
                conn.Open()
                Try
                    Dim locDB As New Database(conn)
                    locDB.EnableAutoSelect = False

                    locReturnObject.zValReturnList = locDB.Query(Of ClsMediaCategory)("exec Proc_MediaCategorySummary @0",
                                                                                      zlocInputParams.reqSearchType).ToList()
                    locSuccess = True
                Catch ex As Exception
                    locSuccess = False

                    Dim locErrorHandler As New ErrorLogging
                    Dim locSF As StackFrame = New StackFrame()
                    Dim locMB As MethodBase = locSF.GetMethod()
                    Dim locClass As System.Type = locMB.DeclaringType
                    locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
                Finally
                    conn.Close()
                End Try
            Catch ex As Exception
                locSuccess = False

                Dim locErrorHandler As New ErrorLogging
                Dim locSF As StackFrame = New StackFrame()
                Dim locMB As MethodBase = locSF.GetMethod()
                Dim locClass As System.Type = locMB.DeclaringType
                locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
            Finally
                If locSuccess Then
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 0
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "no errors-procedure executed properly"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = "no errors-procedure executed properly"
                Else
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 1
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "550"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = ""
                End If
                reqReturnData = locReturnObject
            End Try

            Return locSuccess
        End Function

    End Class

    Public Class WESMediaItems_Search
        Implements IWESDataSearch
        Private zlocInputParams As ClsInputParamsAllFields

        Public Sub New()
            zlocInputParams = New ClsInputParamsAllFields()
        End Sub

        Public Property zValInput As IWESDataSearch.IWESDataSearchInput Implements IWESDataSearch.zValInput
            Get
                Return zlocInputParams
            End Get
            Set(ByVal value As IWESDataSearch.IWESDataSearchInput)
                If TypeOf value Is ClsInputParamsAllItems Then
                    Dim locInput As ClsInputParamsAllItems = value

                    zlocInputParams.reqSearchType = 1
                ElseIf TypeOf value Is ClsInputParamsCategory Then
                    Dim locInput As ClsInputParamsCategory = value

                    zlocInputParams.reqSearchValueOne = locInput.reqCategoryID

                    zlocInputParams.reqSearchType = 2
                Else
                    zlocInputParams = value
                End If
            End Set
        End Property

        Public Class ClsInputParamsAllFields
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqSearchType As Integer = 0
            Public Property reqSearchValueOne As String = "0"
        End Class

        Public Class ClsInputParamsAllItems
            Implements IWESDataSearch.IWESDataSearchInput

        End Class

        Public Class ClsInputParamsCategory
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqCategoryID As Long = 0
        End Class

        Public Function zProSearch(ByRef reqReturnData As IWESDataSearchReturn) As Boolean Implements IWESDataSearch.zProSearch
            Dim locSuccess As Boolean = False

            Dim locReturnObject As New ClsDataSearchReturn()
            Try
                Dim conn As New SqlConnection()
                conn.ConnectionString = WESSQLConnection.connString
                conn.Open()
                Try
                    Dim locDB As New Database(conn)
                    locDB.EnableAutoSelect = False

                    locReturnObject.zValReturnList = locDB.Query(Of ClsMediaItem)("exec Proc_MediaItemSummary @0, @1",
                                                                                  zlocInputParams.reqSearchType, zlocInputParams.reqSearchValueOne).ToList()
                    locSuccess = True
                Catch ex As Exception
                    locSuccess = False

                    Dim locErrorHandler As New ErrorLogging
                    Dim locSF As StackFrame = New StackFrame()
                    Dim locMB As MethodBase = locSF.GetMethod()
                    Dim locClass As System.Type = locMB.DeclaringType
                    locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
                Finally
                    conn.Close()
                End Try
            Catch ex As Exception
                locSuccess = False

                Dim locErrorHandler As New ErrorLogging
                Dim locSF As StackFrame = New StackFrame()
                Dim locMB As MethodBase = locSF.GetMethod()
                Dim locClass As System.Type = locMB.DeclaringType
                locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
            Finally
                If locSuccess Then
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 0
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "no errors-procedure executed properly"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = "no errors-procedure executed properly"
                Else
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 1
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "550"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = ""
                End If
                reqReturnData = locReturnObject
            End Try

            Return locSuccess
        End Function

    End Class

    Public Class WESMediaItems_Save
        Implements IWESDataSearch
        Private zlocInputParams As ClsInputParamsAllFields

        Public Sub New()
            zlocInputParams = New ClsInputParamsAllFields()
        End Sub

        Public Property zValInput As IWESDataSearch.IWESDataSearchInput Implements IWESDataSearch.zValInput
            Get
                Return zlocInputParams
            End Get
            Set(ByVal value As IWESDataSearch.IWESDataSearchInput)
                If TypeOf value Is ClsInputParamsSave Then
                    Dim locInput As ClsInputParamsSave = value

                    zlocInputParams.reqMediaItemID = locInput.reqMediaItemID
                    zlocInputParams.reqMediaName = locInput.reqMediaName
                    zlocInputParams.reqMediaDescription = locInput.reqMediaDescription
                    zlocInputParams.reqMediaCategoryID = locInput.reqMediaCategoryID
                    zlocInputParams.reqWebsiteURL = locInput.reqWebsiteURL

                    zlocInputParams.reqUpdateType = 1
                ElseIf TypeOf value Is ClsInputParamsRemove Then
                    Dim locInput As ClsInputParamsRemove = value

                    zlocInputParams.reqMediaItemID = locInput.reqMediaItemID

                    zlocInputParams.reqUpdateType = 2
                Else
                    zlocInputParams = value
                End If
            End Set
        End Property

        Public Class ClsInputParamsAllFields
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqUpdateType As Integer = 0
            Public Property reqMediaItemID As Long = 0
            Public Property reqMediaName As String = ""
            Public Property reqMediaDescription As String = ""
            Public Property reqMediaCategoryID As Long = 0
            Public Property reqWebsiteURL As String = ""
        End Class

        Public Class ClsInputParamsSave
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqMediaItemID As Long = 0
            Public Property reqMediaName As String = ""
            Public Property reqMediaDescription As String = ""
            Public Property reqMediaCategoryID As Long = 0
            Public Property reqWebsiteURL As String = ""
        End Class

        Public Class ClsInputParamsRemove
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqMediaItemID As Long = 0
        End Class

        Public Function zProSave(ByRef reqReturnData As IWESDataSearchReturn) As Boolean Implements IWESDataSearch.zProSearch
            Dim locSuccess As Boolean = False

            Dim locReturnObject As New ClsDataSearchReturnSave()
            Try
                Dim conn As New SqlConnection()
                conn.ConnectionString = WESSQLConnection.connString
                Dim cmd As New SqlCommand("Proc_MediaItemSave", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@reqUpdateType", zlocInputParams.reqUpdateType)
                cmd.Parameters.AddWithValue("@reqMediaItemID", zlocInputParams.reqMediaItemID)
                cmd.Parameters.AddWithValue("@reqMediaName", zlocInputParams.reqMediaName)
                cmd.Parameters.AddWithValue("@reqMediaDescription", zlocInputParams.reqMediaDescription)
                cmd.Parameters.AddWithValue("@reqMediaCategoryID", zlocInputParams.reqMediaCategoryID)
                cmd.Parameters.AddWithValue("@reqWebsiteURL", zlocInputParams.reqWebsiteURL)

                conn.Open()
                Dim locSdr As SqlDataReader = cmd.ExecuteReader()
                Try
                    If locSdr.Read() Then
                        If Not (locSdr("UpdateStatus") Is DBNull.Value) Then
                            locReturnObject.zValUpdateStatus = locSdr("UpdateStatus") 'Integer
                        End If
                        If Not (locSdr("UpdateMessage") Is DBNull.Value) Then
                            locReturnObject.zValUpdateMessage = locSdr("UpdateMessage") 'String
                        End If
                        If Not (locSdr("MediaItemID") Is DBNull.Value) Then
                            locReturnObject.zValUpdateID = locSdr("MediaItemID") 'Long
                        End If
                    End If

                    locSuccess = True
                Catch ex As Exception
                    locSuccess = False

                    Dim locErrorHandler As New ErrorLogging
                    Dim locSF As StackFrame = New StackFrame()
                    Dim locMB As MethodBase = locSF.GetMethod()
                    Dim locClass As System.Type = locMB.DeclaringType
                    locErrorHandler.EmailError(locMB.Name + ": Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
                Finally
                    cmd.Dispose()
                    conn.Close()
                End Try
            Catch ex As Exception
                locSuccess = False

                Dim locErrorHandler As New ErrorLogging
                Dim locSF As StackFrame = New StackFrame()
                Dim locMB As MethodBase = locSF.GetMethod()
                Dim locClass As System.Type = locMB.DeclaringType
                locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
            Finally
                If locSuccess Then
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 0
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "no errors-procedure executed properly"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = "no errors-procedure executed properly"
                Else
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 1
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "550"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = ""
                End If
                reqReturnData = locReturnObject
            End Try

            Return locSuccess
        End Function

    End Class
#End Region

#Region "Order"
    Public Class WESOrder_Search
        Implements IWESDataSearch
        Private zlocInputParams As ClsInputParamsAllFields

        Public Sub New()
            zlocInputParams = New ClsInputParamsAllFields()
        End Sub

        Public Property zValInput As IWESDataSearch.IWESDataSearchInput Implements IWESDataSearch.zValInput
            Get
                Return zlocInputParams
            End Get
            Set(ByVal value As IWESDataSearch.IWESDataSearchInput)
                If TypeOf value Is ClsInputParamsAllOrders Then
                    Dim locInput As ClsInputParamsAllOrders = value

                    zlocInputParams.reqSearchType = 1
                ElseIf TypeOf value Is ClsInputParamsTrainee Then
                    Dim locInput As ClsInputParamsTrainee = value

                    zlocInputParams.reqSearchValueOne = locInput.reqTraineeID

                    zlocInputParams.reqSearchType = 2
                Else
                    zlocInputParams = value
                End If
            End Set
        End Property

        Public Class ClsInputParamsAllFields
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqSearchType As Integer = 0
            Public Property reqSearchValueOne As String = "0"
        End Class

        Public Class ClsInputParamsAllOrders
            Implements IWESDataSearch.IWESDataSearchInput

        End Class

        Public Class ClsInputParamsTrainee
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqTraineeID As Long = 0
        End Class

        Public Function zProSearch(ByRef reqReturnData As IWESDataSearchReturn) As Boolean Implements IWESDataSearch.zProSearch
            Dim locSuccess As Boolean = False

            Dim locReturnObject As New ClsDataSearchReturn()
            Try
                Dim conn As New SqlConnection()
                conn.ConnectionString = WESSQLConnection.connString
                conn.Open()
                Try
                    Dim locDB As New Database(conn)
                    locDB.EnableAutoSelect = False

                    locReturnObject.zValReturnList = locDB.Query(Of ClsWESOrder)("exec Proc_WESOrderSummary @0, @1",
                                                                                 zlocInputParams.reqSearchType, zlocInputParams.reqSearchValueOne).ToList()
                    locSuccess = True
                Catch ex As Exception
                    locSuccess = False

                    Dim locErrorHandler As New ErrorLogging
                    Dim locSF As StackFrame = New StackFrame()
                    Dim locMB As MethodBase = locSF.GetMethod()
                    Dim locClass As System.Type = locMB.DeclaringType
                    locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
                Finally
                    conn.Close()
                End Try
            Catch ex As Exception
                locSuccess = False

                Dim locErrorHandler As New ErrorLogging
                Dim locSF As StackFrame = New StackFrame()
                Dim locMB As MethodBase = locSF.GetMethod()
                Dim locClass As System.Type = locMB.DeclaringType
                locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
            Finally
                If locSuccess Then
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 0
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "no errors-procedure executed properly"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = "no errors-procedure executed properly"
                Else
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 1
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "550"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = ""
                End If
                reqReturnData = locReturnObject
            End Try

            Return locSuccess
        End Function

    End Class

    Public Class WESOrder_Save
        Implements IWESDataSearch
        Private zlocInputParams As ClsInputParamsAllFields

        Public Sub New()
            zlocInputParams = New ClsInputParamsAllFields()
        End Sub

        Public Property zValInput As IWESDataSearch.IWESDataSearchInput Implements IWESDataSearch.zValInput
            Get
                Return zlocInputParams
            End Get
            Set(ByVal value As IWESDataSearch.IWESDataSearchInput)
                If TypeOf value Is ClsInputParamsSave Then
                    Dim locInput As ClsInputParamsSave = value

                    zlocInputParams.reqWESOrderID = locInput.reqWESOrderID
                    zlocInputParams.reqTraineeID = locInput.reqTraineeID
                    zlocInputParams.reqTestID = locInput.reqTestID
                    zlocInputParams.reqPayPalItemID = locInput.reqPayPalItemID
                    zlocInputParams.reqPayPalTransID = locInput.reqPayPalTransID
                    zlocInputParams.reqOrderDate = locInput.reqOrderDate
                    zlocInputParams.reqOrderAmount = locInput.reqOrderAmount
                    zlocInputParams.reqPaymentType = locInput.reqPaymentType

                    zlocInputParams.reqSaveType = 1
                Else
                    zlocInputParams = value
                End If
            End Set
        End Property

        Public Class ClsInputParamsAllFields
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqSaveType As Integer = 0
            Public Property reqWESOrderID As Long = 0
            Public Property reqTraineeID As Long = 0
            Public Property reqTestID As Long = 0
            Public Property reqPayPalItemID As String = ""
            Public Property reqPayPalTransID As String = ""
            Public Property reqOrderDate As DateTime
            Public Property reqOrderAmount As Double
            Public Property reqPaymentType As String = ""
        End Class

        Public Class ClsInputParamsSave
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqWESOrderID As Long = 0
            Public Property reqTraineeID As Long = 0
            Public Property reqTestID As Long = 0
            Public Property reqPayPalItemID As String = ""
            Public Property reqPayPalTransID As String = ""
            Public Property reqOrderDate As DateTime
            Public Property reqOrderAmount As Double
            Public Property reqPaymentType As String = ""
        End Class

        Public Function zProSave(ByRef reqReturnData As IWESDataSearchReturn) As Boolean Implements IWESDataSearch.zProSearch
            Dim locSuccess As Boolean = False

            Dim locReturnObject As New ClsDataSearchReturnSave()
            Try
                Dim conn As New SqlConnection()
                conn.ConnectionString = WESSQLConnection.connString
                Dim cmd As New SqlCommand("Proc_WesOrderSave", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@reqSaveType", zlocInputParams.reqSaveType)
                cmd.Parameters.AddWithValue("@reqWESOrderID", zlocInputParams.reqWESOrderID)
                cmd.Parameters.AddWithValue("@reqTraineeID", zlocInputParams.reqTraineeID)
                cmd.Parameters.AddWithValue("@reqTestID", zlocInputParams.reqTestID)
                cmd.Parameters.AddWithValue("@reqPayPalItemID", zlocInputParams.reqPayPalItemID)
                cmd.Parameters.AddWithValue("@reqPayPalTransID", zlocInputParams.reqPayPalTransID)
                cmd.Parameters.AddWithValue("@reqOrderDate", zlocInputParams.reqOrderDate)
                cmd.Parameters.AddWithValue("@reqOrderAmount", zlocInputParams.reqOrderAmount)
                cmd.Parameters.AddWithValue("@reqPaymentType", zlocInputParams.reqPaymentType)

                conn.Open()
                Dim locSdr As SqlDataReader = cmd.ExecuteReader()
                Try
                    If locSdr.Read() Then
                        If Not (locSdr("Status") Is DBNull.Value) Then
                            locReturnObject.zValUpdateStatus = locSdr("Status") 'Integer
                        End If
                        If Not (locSdr("Message") Is DBNull.Value) Then
                            locReturnObject.zValUpdateMessage = locSdr("Message") 'String
                        End If
                        If Not (locSdr("OrderID") Is DBNull.Value) Then
                            locReturnObject.zValUpdateID = locSdr("OrderID") 'Long
                        End If
                    End If

                    locSuccess = True
                Catch ex As Exception
                    locSuccess = False

                    Dim locErrorHandler As New ErrorLogging
                    Dim locSF As StackFrame = New StackFrame()
                    Dim locMB As MethodBase = locSF.GetMethod()
                    Dim locClass As System.Type = locMB.DeclaringType
                    locErrorHandler.EmailError(locMB.Name + ": Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
                Finally
                    cmd.Dispose()
                    conn.Close()
                End Try
            Catch ex As Exception
                locSuccess = False

                Dim locErrorHandler As New ErrorLogging
                Dim locSF As StackFrame = New StackFrame()
                Dim locMB As MethodBase = locSF.GetMethod()
                Dim locClass As System.Type = locMB.DeclaringType
                locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
            Finally
                If locSuccess Then
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 0
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "no errors-procedure executed properly"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = "no errors-procedure executed properly"
                Else
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 1
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "550"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = ""
                End If
                reqReturnData = locReturnObject
            End Try

            Return locSuccess
        End Function

    End Class
#End Region

#Region "Promo Code"
    Public Class WESPromoCode_Search
        Implements IWESDataSearch
        Private zlocInputParams As ClsInputParamsAllFields

        Public Sub New()
            zlocInputParams = New ClsInputParamsAllFields()
        End Sub

        Public Property zValInput As IWESDataSearch.IWESDataSearchInput Implements IWESDataSearch.zValInput
            Get
                Return zlocInputParams
            End Get
            Set(ByVal value As IWESDataSearch.IWESDataSearchInput)
                If TypeOf value Is ClsInputParamsAllPromoCodes Then
                    Dim locInput As ClsInputParamsAllPromoCodes = value

                    zlocInputParams.reqSearchType = 1
                ElseIf TypeOf value Is ClsInputParamsPromoCodeID Then
                    Dim locInput As ClsInputParamsPromoCodeID = value
                    zlocInputParams.reqSearchValueOne = locInput.reqPromoCodeID

                    zlocInputParams.reqSearchType = 2
                ElseIf TypeOf value Is ClsInputParamsPromoCodeName Then
                    Dim locInput As ClsInputParamsPromoCodeName = value
                    zlocInputParams.reqSearchValueOne = locInput.reqPromoCode

                    zlocInputParams.reqSearchType = 3
                Else
                    zlocInputParams = value
                End If
            End Set
        End Property

        Public Class ClsInputParamsAllFields
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqSearchType As Integer = 0
            Public Property reqSearchValueOne As String = "0"
        End Class

        Public Class ClsInputParamsAllPromoCodes
            Implements IWESDataSearch.IWESDataSearchInput

        End Class

        Public Class ClsInputParamsPromoCodeID
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqPromoCodeID As Long = 0
        End Class

        Public Class ClsInputParamsPromoCodeName
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqPromoCode As String = ""
        End Class

        Public Function zProSearch(ByRef reqReturnData As IWESDataSearchReturn) As Boolean Implements IWESDataSearch.zProSearch
            Dim locSuccess As Boolean = False

            Dim locReturnObject As New ClsDataSearchReturn()
            Try
                Dim conn As New SqlConnection()
                conn.ConnectionString = WESSQLConnection.connString
                conn.Open()
                Try
                    Dim locDB As New Database(conn)
                    locDB.EnableAutoSelect = False

                    locReturnObject.zValReturnList = locDB.Query(Of ClsPromoCode)("exec Proc_PromoCodeSummary @0, @1",
                                                                                  zlocInputParams.reqSearchType, zlocInputParams.reqSearchValueOne).ToList()
                    locSuccess = True
                Catch ex As Exception
                    locSuccess = False

                    Dim locErrorHandler As New ErrorLogging
                    Dim locSF As StackFrame = New StackFrame()
                    Dim locMB As MethodBase = locSF.GetMethod()
                    Dim locClass As System.Type = locMB.DeclaringType
                    locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
                Finally
                    conn.Close()
                End Try
            Catch ex As Exception
                locSuccess = False

                Dim locErrorHandler As New ErrorLogging
                Dim locSF As StackFrame = New StackFrame()
                Dim locMB As MethodBase = locSF.GetMethod()
                Dim locClass As System.Type = locMB.DeclaringType
                locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
            Finally
                If locSuccess Then
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 0
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "no errors-procedure executed properly"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = "no errors-procedure executed properly"
                Else
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 1
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "550"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = ""
                End If
                reqReturnData = locReturnObject
            End Try

            Return locSuccess
        End Function

    End Class

    Public Class WESPromoCode_Save
        Implements IWESDataSearch
        Private zlocInputParams As ClsInputParamsAllFields

        Public Sub New()
            zlocInputParams = New ClsInputParamsAllFields()
        End Sub

        Public Property zValInput As IWESDataSearch.IWESDataSearchInput Implements IWESDataSearch.zValInput
            Get
                Return zlocInputParams
            End Get
            Set(ByVal value As IWESDataSearch.IWESDataSearchInput)
                If TypeOf value Is ClsInputParamsPromoCodeSave Then
                    Dim locInput As ClsInputParamsPromoCodeSave = value

                    zlocInputParams.reqPromoCodeID = locInput.reqPromoCodeID
                    zlocInputParams.reqPromoCodeName = locInput.reqPromoCodeName
                    zlocInputParams.reqPromoCode = locInput.reqPromoCode
                    zlocInputParams.reqDateEnd = locInput.reqDateEnd
                    zlocInputParams.reqDiscount = locInput.reqDiscount

                    zlocInputParams.reqUpdateType = 1
                ElseIf TypeOf value Is ClsInputParamsPromoCodeDelete Then
                    Dim locInput As ClsInputParamsPromoCodeDelete = value

                    zlocInputParams.reqPromoCodeID = locInput.reqPromoCodeID

                    zlocInputParams.reqUpdateType = 2
                Else
                    zlocInputParams = value
                End If
            End Set
        End Property

        Public Class ClsInputParamsAllFields
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqUpdateType As Integer = 0
            Public Property reqPromoCodeID As Long = 0
            Public Property reqPromoCodeName As String = ""
            Public Property reqPromoCode As String = ""
            Public Property reqDateEnd As DateTime
            Public Property reqDiscount As Double = 0
        End Class

        Public Class ClsInputParamsPromoCodeSave
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqPromoCodeID As Long = 0
            Public Property reqPromoCodeName As String = ""
            Public Property reqPromoCode As String = ""
            Public Property reqDateEnd As DateTime
            Public Property reqDiscount As Double = 0
        End Class

        Public Class ClsInputParamsPromoCodeDelete
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqPromoCodeID As Long = 0
        End Class

        Public Function zProSearch(ByRef reqReturnData As IWESDataSearchReturn) As Boolean Implements IWESDataSearch.zProSearch
            Dim locSuccess As Boolean = False

            Dim locReturnObject As New ClsDataSearchReturnSave()
            Try
                Dim conn As New SqlConnection()
                conn.ConnectionString = WESSQLConnection.connString
                Dim cmd As New SqlCommand("Proc_PromoCodeSave", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@reqUpdateType", zlocInputParams.reqUpdateType)
                cmd.Parameters.AddWithValue("@reqPromoCodeID", zlocInputParams.reqPromoCodeID)
                cmd.Parameters.AddWithValue("@reqPromoCodeName", zlocInputParams.reqPromoCodeName)
                cmd.Parameters.AddWithValue("@reqPromoCode", zlocInputParams.reqPromoCode)
                cmd.Parameters.AddWithValue("@reqDiscount", zlocInputParams.reqDiscount)
                cmd.Parameters.AddWithValue("@reqDateEnd", zlocInputParams.reqDateEnd)

                conn.Open()
                Dim locSdr As SqlDataReader = cmd.ExecuteReader()
                Try
                    If locSdr.Read() Then
                        If Not (locSdr("UpdateStatus") Is DBNull.Value) Then
                            locReturnObject.zValUpdateStatus = locSdr("UpdateStatus") 'Integer
                        End If
                        If Not (locSdr("UpdateMessage") Is DBNull.Value) Then
                            locReturnObject.zValUpdateMessage = locSdr("UpdateMessage") 'String
                        End If
                        If Not (locSdr("PromoCodeID") Is DBNull.Value) Then
                            locReturnObject.zValUpdateID = locSdr("PromoCodeID") 'Long
                        End If
                    End If

                    locSuccess = True
                Catch ex As Exception
                    locSuccess = False

                    Dim locErrorHandler As New ErrorLogging
                    Dim locSF As StackFrame = New StackFrame()
                    Dim locMB As MethodBase = locSF.GetMethod()
                    Dim locClass As System.Type = locMB.DeclaringType
                    locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
                Finally
                    cmd.Dispose()
                    conn.Close()
                End Try
            Catch ex As Exception
                locSuccess = False

                Dim locErrorHandler As New ErrorLogging
                Dim locSF As StackFrame = New StackFrame()
                Dim locMB As MethodBase = locSF.GetMethod()
                Dim locClass As System.Type = locMB.DeclaringType
                locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
            Finally
                If locSuccess Then
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 0
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "no errors-procedure executed properly"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = "no errors-procedure executed properly"
                Else
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 1
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "550"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = ""
                End If
                reqReturnData = locReturnObject
            End Try

            Return locSuccess
        End Function

    End Class
#End Region

#Region "State"
    Public Class WESState_Search
        Implements IWESDataSearch
        Private zlocInputParams As ClsInputParamsAllFields

        Public Sub New()
            zlocInputParams = New ClsInputParamsAllFields()
        End Sub

        Public Property zValInput As IWESDataSearch.IWESDataSearchInput Implements IWESDataSearch.zValInput
            Get
                Return zlocInputParams
            End Get
            Set(ByVal value As IWESDataSearch.IWESDataSearchInput)
                If TypeOf value Is ClsInputParamsAllStates Then
                    Dim locInput As ClsInputParamsAllStates = value

                    zlocInputParams.reqSearchType = 1
                ElseIf TypeOf value Is ClsInputParamsCountry Then
                    Dim locInput As ClsInputParamsCountry = value

                    zlocInputParams.reqSearchValueOne = locInput.reqZCountryID

                    zlocInputParams.reqSearchType = 2
                Else
                    zlocInputParams = value
                End If
            End Set
        End Property

        Public Class ClsInputParamsAllFields
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqSearchType As Integer = 0
            Public Property reqSearchValueOne As String = "0"
        End Class

        Public Class ClsInputParamsAllStates
            Implements IWESDataSearch.IWESDataSearchInput

        End Class

        Public Class ClsInputParamsCountry
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqZCountryID As Long = 0
        End Class

        Public Function zProSearch(ByRef reqReturnData As IWESDataSearchReturn) As Boolean Implements IWESDataSearch.zProSearch
            Dim locSuccess As Boolean = False

            Dim locReturnObject As New ClsDataSearchReturn()
            Try
                Dim conn As New SqlConnection()
                conn.ConnectionString = WESSQLConnection.connString
                conn.Open()
                Try
                    Dim locDB As New Database(conn)
                    locDB.EnableAutoSelect = False

                    locReturnObject.zValReturnList = locDB.Query(Of ClsState)("exec Proc_ListState @0, @1",
                                                                              zlocInputParams.reqSearchType, zlocInputParams.reqSearchValueOne).ToList()
                    locSuccess = True
                Catch ex As Exception
                    locSuccess = False

                    Dim locErrorHandler As New ErrorLogging
                    Dim locSF As StackFrame = New StackFrame()
                    Dim locMB As MethodBase = locSF.GetMethod()
                    Dim locClass As System.Type = locMB.DeclaringType
                    locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
                Finally
                    conn.Close()
                End Try
            Catch ex As Exception
                locSuccess = False

                Dim locErrorHandler As New ErrorLogging
                Dim locSF As StackFrame = New StackFrame()
                Dim locMB As MethodBase = locSF.GetMethod()
                Dim locClass As System.Type = locMB.DeclaringType
                locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
            Finally
                If locSuccess Then
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 0
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "no errors-procedure executed properly"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = "no errors-procedure executed properly"
                Else
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 1
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "550"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = ""
                End If
                reqReturnData = locReturnObject
            End Try

            Return locSuccess
        End Function

    End Class

    Public Class WESState_Save
        Implements IWESDataSearch
        Private zlocInputParams As ClsInputParamsAllFields

        Public Sub New()
            zlocInputParams = New ClsInputParamsAllFields()
        End Sub

        Public Property zValInput As IWESDataSearch.IWESDataSearchInput Implements IWESDataSearch.zValInput
            Get
                Return zlocInputParams
            End Get
            Set(ByVal value As IWESDataSearch.IWESDataSearchInput)
                If TypeOf value Is ClsInputParamsSave Then
                    Dim locInput As ClsInputParamsSave = value

                    zlocInputParams.reqZStateID = locInput.reqZStateID
                    zlocInputParams.reqStateName = locInput.reqStateName
                    zlocInputParams.reqAbbreviation = locInput.reqAbbreviation
                    zlocInputParams.reqZCountryID = locInput.reqZCountryID
                    zlocInputParams.reqActive = locInput.reqActive

                    zlocInputParams.reqUpdateType = 1
                ElseIf TypeOf value Is ClsInputParamsDelete Then
                    Dim locInput As ClsInputParamsDelete = value

                    zlocInputParams.reqZStateID = locInput.reqZStateID

                    zlocInputParams.reqUpdateType = 2
                Else
                    zlocInputParams = value
                End If
            End Set
        End Property

        Public Class ClsInputParamsAllFields
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqUpdateType As Integer = 0
            Public Property reqZStateID As Long = 0
            Public Property reqStateName As String = ""
            Public Property reqAbbreviation As String = ""
            Public Property reqZCountryID As Long = 0
            Public Property reqActive As Integer = 0
        End Class

        Public Class ClsInputParamsSave
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqZStateID As Long = 0
            Public Property reqStateName As String = ""
            Public Property reqAbbreviation As String = ""
            Public Property reqZCountryID As Long = 0
            Public Property reqActive As Integer = 0
        End Class

        Public Class ClsInputParamsDelete
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqZStateID As Long = 0
        End Class

        Public Function zProSearch(ByRef reqReturnData As IWESDataSearchReturn) As Boolean Implements IWESDataSearch.zProSearch
            Dim locSuccess As Boolean = False

            Dim locReturnObject As New ClsDataSearchReturnSave()
            Try
                Dim conn As New SqlConnection()
                conn.ConnectionString = WESSQLConnection.connString
                Dim cmd As New SqlCommand("Proc_ListStateSave", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@reqUpdateType", zlocInputParams.reqUpdateType)
                cmd.Parameters.AddWithValue("@reqZStateID", zlocInputParams.reqZStateID)
                cmd.Parameters.AddWithValue("@reqStateName", zlocInputParams.reqStateName)
                cmd.Parameters.AddWithValue("@reqAbbreviation", zlocInputParams.reqAbbreviation)
                cmd.Parameters.AddWithValue("@reqZCountryID", zlocInputParams.reqZCountryID)
                cmd.Parameters.AddWithValue("@reqActive", zlocInputParams.reqActive)

                conn.Open()
                Dim locSdr As SqlDataReader = cmd.ExecuteReader()
                Try
                    If locSdr.Read() Then
                        If Not (locSdr("UpdateStatus") Is DBNull.Value) Then
                            locReturnObject.zValUpdateStatus = locSdr("UpdateStatus") 'Integer
                        End If
                        If Not (locSdr("UpdateMessage") Is DBNull.Value) Then
                            locReturnObject.zValUpdateMessage = locSdr("UpdateMessage") 'String
                        End If
                        If Not (locSdr("ZStateID") Is DBNull.Value) Then
                            locReturnObject.zValUpdateID = locSdr("ZStateID") 'Long
                        End If
                    End If

                    locSuccess = True
                Catch ex As Exception
                    locSuccess = False

                    Dim locErrorHandler As New ErrorLogging
                    Dim locSF As StackFrame = New StackFrame()
                    Dim locMB As MethodBase = locSF.GetMethod()
                    Dim locClass As System.Type = locMB.DeclaringType
                    locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
                Finally
                    cmd.Dispose()
                    conn.Close()
                End Try
            Catch ex As Exception
                locSuccess = False

                Dim locErrorHandler As New ErrorLogging
                Dim locSF As StackFrame = New StackFrame()
                Dim locMB As MethodBase = locSF.GetMethod()
                Dim locClass As System.Type = locMB.DeclaringType
                locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
            Finally
                If locSuccess Then
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 0
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "no errors-procedure executed properly"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = "no errors-procedure executed properly"
                Else
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 1
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "550"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = ""
                End If
                reqReturnData = locReturnObject
            End Try

            Return locSuccess
        End Function

    End Class
#End Region

#Region "Test"
    Public Class WESTest_Search
        Implements IWESDataSearch
        Private zlocInputParams As ClsInputParamsAllFields

        Public Sub New()
            zlocInputParams = New ClsInputParamsAllFields()
        End Sub

        Public Property zValInput As IWESDataSearch.IWESDataSearchInput Implements IWESDataSearch.zValInput
            Get
                Return zlocInputParams
            End Get
            Set(ByVal value As IWESDataSearch.IWESDataSearchInput)
                If TypeOf value Is ClsInputParamsAllTests Then
                    Dim locInput As ClsInputParamsAllTests = value

                    zlocInputParams.reqSearchType = 1
                ElseIf TypeOf value Is ClsInputParamsTest Then
                    Dim locInput As ClsInputParamsTest = value
                    zlocInputParams.reqSearchValueOne = locInput.reqTestID

                    zlocInputParams.reqSearchType = 2
                Else
                    zlocInputParams = value
                End If
            End Set
        End Property

        Public Class ClsInputParamsAllFields
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqSearchType As Integer = 0
            Public Property reqSearchValueOne As String = "0"
        End Class

        Public Class ClsInputParamsAllTests
            Implements IWESDataSearch.IWESDataSearchInput

        End Class

        Public Class ClsInputParamsTest
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqTestID As Long = 0
        End Class

        Public Function zProSearch(ByRef reqReturnData As IWESDataSearchReturn) As Boolean Implements IWESDataSearch.zProSearch
            Dim locSuccess As Boolean = False

            Dim locReturnObject As New ClsDataSearchReturn()
            Try
                Dim conn As New SqlConnection()
                conn.ConnectionString = WESSQLConnection.connString
                conn.Open()
                Try
                    Dim locDB As New Database(conn)
                    locDB.EnableAutoSelect = False

                    locReturnObject.ZValReturnList = locDB.Query(Of ClsTest)("exec Proc_TestSummary @0, @1",
                                                                             zlocInputParams.reqSearchType, zlocInputParams.reqSearchValueOne).ToList()
                    locSuccess = True
                Catch ex As Exception
                    locSuccess = False

                    Dim locErrorHandler As New ErrorLogging
                    Dim locSF As StackFrame = New StackFrame()
                    Dim locMB As MethodBase = locSF.GetMethod()
                    Dim locClass As System.Type = locMB.DeclaringType
                    locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
                Finally
                    conn.Close()
                End Try
            Catch ex As Exception
                locSuccess = False

                Dim locErrorHandler As New ErrorLogging
                Dim locSF As StackFrame = New StackFrame()
                Dim locMB As MethodBase = locSF.GetMethod()
                Dim locClass As System.Type = locMB.DeclaringType
                locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
            Finally
                If locSuccess Then
                    locReturnObject.ZValReturnErrorStatus.zValStatusError = 0
                    locReturnObject.ZValReturnErrorStatus.zValStatusErrorCode = "no errors-procedure executed properly"
                    locReturnObject.ZValReturnErrorStatus.zValStatusErrorMessage = "no errors-procedure executed properly"
                Else
                    locReturnObject.ZValReturnErrorStatus.zValStatusError = 1
                    locReturnObject.ZValReturnErrorStatus.zValStatusErrorCode = "550"
                    locReturnObject.ZValReturnErrorStatus.zValStatusErrorMessage = ""
                End If
                reqReturnData = locReturnObject
            End Try

            Return locSuccess
        End Function

    End Class

    Public Class WESTest_Save
        Implements IWESDataSearch
        Private zlocInputParams As ClsInputParamsAllFields

        Public Sub New()
            zlocInputParams = New ClsInputParamsAllFields()
        End Sub

        Public Property zValInput As IWESDataSearch.IWESDataSearchInput Implements IWESDataSearch.zValInput
            Get
                Return zlocInputParams
            End Get
            Set(ByVal value As IWESDataSearch.IWESDataSearchInput)
                If TypeOf value Is ClsInputParamsTestSave Then
                    Dim locInput As ClsInputParamsTestSave = value

                    zlocInputParams.reqTestID = locInput.reqTestID
                    zlocInputParams.reqTestName = locInput.reqTestName
                    zlocInputParams.reqDescription = locInput.reqDescription
                    zlocInputParams.reqMemberCost = locInput.reqMemberCost
                    zlocInputParams.reqNonMemberCost = locInput.reqNonMemberCost
                    zlocInputParams.reqPassingGrade = locInput.reqPassingGrade

                    zlocInputParams.reqUpdateType = 1
                ElseIf TypeOf value Is ClsInputParamsTestDelete Then
                    Dim locInput As ClsInputParamsTestDelete = value

                    zlocInputParams.reqTestID = locInput.reqTestID

                    zlocInputParams.reqUpdateType = 2
                Else
                    zlocInputParams = value
                End If
            End Set
        End Property

        Public Class ClsInputParamsAllFields
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqUpdateType As Integer = 0
            Public Property reqTestID As Long = 0
            Public Property reqTestName As String = ""
            Public Property reqDescription As String = ""
            Public Property reqMemberCost As Double = 0
            Public Property reqNonMemberCost As Double = 0
            Public Property reqPassingGrade As Integer = 0
        End Class

        Public Class ClsInputParamsTestSave
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqTestID As Long = 0
            Public Property reqTestName As String = ""
            Public Property reqDescription As String = ""
            Public Property reqMemberCost As Double = 0
            Public Property reqNonMemberCost As Double = 0
            Public Property reqPassingGrade As Integer = 0
        End Class

        Public Class ClsInputParamsTestDelete
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqTestID As Long = 0
        End Class

        Public Function zProSearch(ByRef reqReturnData As IWESDataSearchReturn) As Boolean Implements IWESDataSearch.zProSearch
            Dim locSuccess As Boolean = False

            Dim locReturnObject As New ClsDataSearchReturnSave()
            Try
                Dim conn As New SqlConnection()
                conn.ConnectionString = WESSQLConnection.connString
                Dim cmd As New SqlCommand("Proc_TestSave", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@reqUpdateType", zlocInputParams.reqUpdateType)
                cmd.Parameters.AddWithValue("@reqTestID", zlocInputParams.reqTestID)
                cmd.Parameters.AddWithValue("@reqTestName", zlocInputParams.reqTestName)
                cmd.Parameters.AddWithValue("@reqTestDesc", zlocInputParams.reqDescription)
                cmd.Parameters.AddWithValue("@reqTestCostMember", zlocInputParams.reqMemberCost)
                cmd.Parameters.AddWithValue("@reqTestCostNonMember", zlocInputParams.reqNonMemberCost)
                cmd.Parameters.AddWithValue("@reqPassingGrade", zlocInputParams.reqPassingGrade)

                conn.Open()
                Dim locSdr As SqlDataReader = cmd.ExecuteReader()
                Try
                    If locSdr.Read() Then
                        If Not (locSdr("UpdateStatus") Is DBNull.Value) Then
                            locReturnObject.zValUpdateStatus = locSdr("UpdateStatus") 'Integer
                        End If
                        If Not (locSdr("UpdateMessage") Is DBNull.Value) Then
                            locReturnObject.zValUpdateMessage = locSdr("UpdateMessage") 'String
                        End If
                        If Not (locSdr("TestID") Is DBNull.Value) Then
                            locReturnObject.zValUpdateID = locSdr("TestID") 'Long
                        End If
                    End If

                    locSuccess = True
                Catch ex As Exception
                    locSuccess = False

                    Dim locErrorHandler As New ErrorLogging
                    Dim locSF As StackFrame = New StackFrame()
                    Dim locMB As MethodBase = locSF.GetMethod()
                    Dim locClass As System.Type = locMB.DeclaringType
                    locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
                Finally
                    cmd.Dispose()
                    conn.Close()
                End Try
            Catch ex As Exception
                locSuccess = False

                Dim locErrorHandler As New ErrorLogging
                Dim locSF As StackFrame = New StackFrame()
                Dim locMB As MethodBase = locSF.GetMethod()
                Dim locClass As System.Type = locMB.DeclaringType
                locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
            Finally
                If locSuccess Then
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 0
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "no errors-procedure executed properly"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = "no errors-procedure executed properly"
                Else
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 1
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "550"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = ""
                End If
                reqReturnData = locReturnObject
            End Try

            Return locSuccess
        End Function

    End Class

    Public Class WESTestAnswer_Search
        Implements IWESDataSearch
        Private zlocInputParams As ClsInputParamsAllFields

        Public Sub New()
            zlocInputParams = New ClsInputParamsAllFields()
        End Sub

        Public Property zValInput As IWESDataSearch.IWESDataSearchInput Implements IWESDataSearch.zValInput
            Get
                Return zlocInputParams
            End Get
            Set(ByVal value As IWESDataSearch.IWESDataSearchInput)
                If TypeOf value Is ClsInputParamsTestTrainee Then
                    Dim locInput As ClsInputParamsTestTrainee = value
                    zlocInputParams.reqSearchValueOne = locInput.reqTraineeID
                    zlocInputParams.reqSearchValueTwo = locInput.reqTestID

                    zlocInputParams.reqSearchType = 1
                ElseIf TypeOf value Is ClsInputParamsTestAnswer Then
                    Dim locInput As ClsInputParamsTestAnswer = value
                    zlocInputParams.reqSearchValueOne = locInput.reqTestAnswerID

                    zlocInputParams.reqSearchType = 2
                ElseIf TypeOf value Is ClsInputParamsTestAttempt Then
                    Dim locInput As ClsInputParamsTestAttempt = value
                    zlocInputParams.reqSearchValueOne = locInput.reqTestAttemptID

                    zlocInputParams.reqSearchType = 3
                Else
                    zlocInputParams = value
                End If
            End Set
        End Property

        Public Class ClsInputParamsAllFields
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqSearchType As Integer = 0
            Public Property reqSearchValueOne As String = "0"
            Public Property reqSearchValueTwo As String = "0"
        End Class

        Public Class ClsInputParamsTestTrainee
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqTestID As Long = 0
            Public Property reqTraineeID As Long = 0
        End Class

        Public Class ClsInputParamsTestAnswer
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqTestAnswerID As Long = 0
        End Class

        Public Class ClsInputParamsTestAttempt
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqTestAttemptID As Long = 0
        End Class

        Public Function zProSearch(ByRef reqReturnData As IWESDataSearchReturn) As Boolean Implements IWESDataSearch.zProSearch
            Dim locSuccess As Boolean = False

            Dim locReturnObject As New ClsDataSearchReturn()
            Try
                Dim conn As New SqlConnection()
                conn.ConnectionString = WESSQLConnection.connString
                conn.Open()
                Try
                    Dim locDB As New Database(conn)
                    locDB.EnableAutoSelect = False

                    locReturnObject.zValReturnList = locDB.Query(Of ClsTestAnswer)("exec Proc_TestAnswerSummary @0, @1, @2",
                                                                                   zlocInputParams.reqSearchType, zlocInputParams.reqSearchValueOne, zlocInputParams.reqSearchValueTwo).ToList()
                    locSuccess = True
                Catch ex As Exception
                    locSuccess = False

                    Dim locErrorHandler As New ErrorLogging
                    Dim locSF As StackFrame = New StackFrame()
                    Dim locMB As MethodBase = locSF.GetMethod()
                    Dim locClass As System.Type = locMB.DeclaringType
                    locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
                Finally
                    conn.Close()
                End Try
            Catch ex As Exception
                locSuccess = False

                Dim locErrorHandler As New ErrorLogging
                Dim locSF As StackFrame = New StackFrame()
                Dim locMB As MethodBase = locSF.GetMethod()
                Dim locClass As System.Type = locMB.DeclaringType
                locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
            Finally
                If locSuccess Then
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 0
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "no errors-procedure executed properly"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = "no errors-procedure executed properly"
                Else
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 1
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "550"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = ""
                End If
                reqReturnData = locReturnObject
            End Try

            Return locSuccess
        End Function

    End Class

    Public Class WESTestAnswer_Save
        Implements IWESDataSearch
        Private zlocInputParams As ClsInputParamsAllFields

        Public Sub New()
            zlocInputParams = New ClsInputParamsAllFields()
        End Sub

        Public Property zValInput As IWESDataSearch.IWESDataSearchInput Implements IWESDataSearch.zValInput
            Get
                Return zlocInputParams
            End Get
            Set(ByVal value As IWESDataSearch.IWESDataSearchInput)
                If TypeOf value Is ClsInputParamsSave Then
                    Dim locInput As ClsInputParamsSave = value

                    zlocInputParams.reqTestAnswerID = locInput.reqTestAnswerID
                    zlocInputParams.reqTraineeID = locInput.reqTraineeID
                    zlocInputParams.reqTestID = locInput.reqTestID
                    zlocInputParams.reqTestQuestionID = locInput.reqTestQuestionID
                    zlocInputParams.reqTraineeAnswer = locInput.reqTraineeAnswer
                    zlocInputParams.reqCorrectAnswer = locInput.reqCorrectAnswer

                    zlocInputParams.reqUpdateType = 1
                ElseIf TypeOf value Is ClsInputParamsTestAttemptUpdate Then
                    Dim locInput As ClsInputParamsTestAttemptUpdate = value

                    zlocInputParams.reqTestAttemptID = locInput.reqTestAttemptID
                    zlocInputParams.reqTraineeID = locInput.reqTraineeID
                    zlocInputParams.reqTestID = locInput.reqTestID

                    zlocInputParams.reqUpdateType = 2
                Else
                    zlocInputParams = value
                End If
            End Set
        End Property

        Public Class ClsInputParamsAllFields
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqUpdateType As Integer = 0
            Public Property reqTestAnswerID As Long = 0
            Public Property reqTestAttemptID As Long = 0
            Public Property reqTraineeID As Long = 0
            Public Property reqTestID As Long = 0
            Public Property reqTestQuestionID As Long = 0
            Public Property reqTraineeAnswer As String = ""
            Public Property reqCorrectAnswer As String = ""
        End Class

        Public Class ClsInputParamsSave
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqTestAnswerID As Long = 0
            Public Property reqTraineeID As Long = 0
            Public Property reqTestID As Long = 0
            Public Property reqTestQuestionID As Long = 0
            Public Property reqTraineeAnswer As String = ""
            Public Property reqCorrectAnswer As String = ""
        End Class

        Public Class ClsInputParamsTestAttemptUpdate
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqTestAttemptID As Long = 0
            Public Property reqTraineeID As Long = 0
            Public Property reqTestID As Long = 0
        End Class

        Public Function zProSearch(ByRef reqReturnData As IWESDataSearchReturn) As Boolean Implements IWESDataSearch.zProSearch
            Dim locSuccess As Boolean = False

            Dim locReturnObject As New ClsDataSearchReturnSave()
            Try
                Dim conn As New SqlConnection()
                conn.ConnectionString = WESSQLConnection.connString
                Dim cmd As New SqlCommand("Proc_TestAnswerSave", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@reqUpdateType", zlocInputParams.reqUpdateType)
                cmd.Parameters.AddWithValue("@reqTestAnswerID", zlocInputParams.reqTestAnswerID)
                cmd.Parameters.AddWithValue("@reqTestAttemptID", zlocInputParams.reqTestAttemptID)
                cmd.Parameters.AddWithValue("@reqTraineeID", zlocInputParams.reqTraineeID)
                cmd.Parameters.AddWithValue("@reqTestID", zlocInputParams.reqTestID)
                cmd.Parameters.AddWithValue("@reqTestQuestionID", zlocInputParams.reqTestQuestionID)
                cmd.Parameters.AddWithValue("@reqTraineeAnswer", zlocInputParams.reqTraineeAnswer)
                cmd.Parameters.AddWithValue("@reqCorrectAnswer", zlocInputParams.reqCorrectAnswer)

                conn.Open()
                Dim locSdr As SqlDataReader = cmd.ExecuteReader()
                Try
                    If locSdr.Read() Then
                        If Not (locSdr("UpdateStatus") Is DBNull.Value) Then
                            locReturnObject.zValUpdateStatus = locSdr("UpdateStatus") 'Integer
                        End If
                        If Not (locSdr("UpdateMessage") Is DBNull.Value) Then
                            locReturnObject.zValUpdateMessage = locSdr("UpdateMessage") 'String
                        End If
                        If Not (locSdr("TestAnswerID") Is DBNull.Value) Then
                            locReturnObject.zValUpdateID = locSdr("TestAnswerID") 'Long
                        End If
                    End If

                    locSuccess = True
                Catch ex As Exception
                    locSuccess = False

                    Dim locErrorHandler As New ErrorLogging
                    Dim locSF As StackFrame = New StackFrame()
                    Dim locMB As MethodBase = locSF.GetMethod()
                    Dim locClass As System.Type = locMB.DeclaringType
                    locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
                Finally
                    cmd.Dispose()
                    conn.Close()
                End Try
            Catch ex As Exception
                locSuccess = False

                Dim locErrorHandler As New ErrorLogging
                Dim locSF As StackFrame = New StackFrame()
                Dim locMB As MethodBase = locSF.GetMethod()
                Dim locClass As System.Type = locMB.DeclaringType
                locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
            Finally
                If locSuccess Then
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 0
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "no errors-procedure executed properly"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = "no errors-procedure executed properly"
                Else
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 1
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "550"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = ""
                End If
                reqReturnData = locReturnObject
            End Try

            Return locSuccess
        End Function

    End Class

    Public Class WESTestAttempt_Search
        Implements IWESDataSearch
        Private zlocInputParams As ClsInputParamsAllFields

        Public Sub New()
            zlocInputParams = New ClsInputParamsAllFields()
        End Sub

        Public Property zValInput As IWESDataSearch.IWESDataSearchInput Implements IWESDataSearch.zValInput
            Get
                Return zlocInputParams
            End Get
            Set(ByVal value As IWESDataSearch.IWESDataSearchInput)
                If TypeOf value Is ClsInputParamsTrainee Then
                    Dim locInput As ClsInputParamsTrainee = value
                    zlocInputParams.reqSearchValueOne = locInput.reqTraineeID

                    zlocInputParams.reqSearchType = 1
                ElseIf TypeOf value Is ClsInputParamsTest Then
                    Dim locInput As ClsInputParamsTest = value
                    zlocInputParams.reqSearchValueOne = locInput.reqTestID

                    zlocInputParams.reqSearchType = 2
                Else
                    zlocInputParams = value
                End If
            End Set
        End Property

        Public Class ClsInputParamsAllFields
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqSearchType As Integer = 0
            Public Property reqSearchValueOne As String = "0"
        End Class

        Public Class ClsInputParamsTrainee
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqTraineeID As Long = 0
        End Class

        Public Class ClsInputParamsTest
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqTestID As Long = 0
        End Class

        Public Function zProSearch(ByRef reqReturnData As IWESDataSearchReturn) As Boolean Implements IWESDataSearch.zProSearch
            Dim locSuccess As Boolean = False

            Dim locReturnObject As New ClsDataSearchReturn()
            Try
                Dim conn As New SqlConnection()
                conn.ConnectionString = WESSQLConnection.connString
                conn.Open()
                Try
                    Dim locDB As New Database(conn)
                    locDB.EnableAutoSelect = False

                    locReturnObject.zValReturnList = locDB.Query(Of ClsTestAttempt)("exec Proc_TestAttemptSummary @0, @1",
                                                                                   zlocInputParams.reqSearchType, zlocInputParams.reqSearchValueOne).ToList()
                    locSuccess = True
                Catch ex As Exception
                    locSuccess = False

                    Dim locErrorHandler As New ErrorLogging
                    Dim locSF As StackFrame = New StackFrame()
                    Dim locMB As MethodBase = locSF.GetMethod()
                    Dim locClass As System.Type = locMB.DeclaringType
                    locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
                Finally
                    conn.Close()
                End Try
            Catch ex As Exception
                locSuccess = False

                Dim locErrorHandler As New ErrorLogging
                Dim locSF As StackFrame = New StackFrame()
                Dim locMB As MethodBase = locSF.GetMethod()
                Dim locClass As System.Type = locMB.DeclaringType
                locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
            Finally
                If locSuccess Then
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 0
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "no errors-procedure executed properly"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = "no errors-procedure executed properly"
                Else
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 1
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "550"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = ""
                End If
                reqReturnData = locReturnObject
            End Try

            Return locSuccess
        End Function

    End Class

    Public Class WESTestAttempt_Save
        Implements IWESDataSearch
        Private zlocInputParams As ClsInputParamsAllFields

        Public Sub New()
            zlocInputParams = New ClsInputParamsAllFields()
        End Sub

        Public Property zValInput As IWESDataSearch.IWESDataSearchInput Implements IWESDataSearch.zValInput
            Get
                Return zlocInputParams
            End Get
            Set(ByVal value As IWESDataSearch.IWESDataSearchInput)
                If TypeOf value Is ClsInputParamsSave Then
                    Dim locInput As ClsInputParamsSave = value

                    zlocInputParams.reqTestAttemptID = locInput.reqTestAttemptID
                    zlocInputParams.reqTraineeID = locInput.reqTraineeID
                    zlocInputParams.reqTestID = locInput.reqTestID
                    zlocInputParams.reqGrade = locInput.reqGrade
                    zlocInputParams.reqPass = locInput.reqPass

                    zlocInputParams.reqUpdateType = 1
                Else
                    zlocInputParams = value
                End If
            End Set
        End Property

        Public Class ClsInputParamsAllFields
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqUpdateType As Integer = 0
            Public Property reqTestAttemptID As Long = 0
            Public Property reqTraineeID As Long = 0
            Public Property reqTestID As Long = 0
            Public Property reqGrade As Double = 0
            Public Property reqPass As Integer = 0
        End Class

        Public Class ClsInputParamsSave
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqTestAttemptID As Long = 0
            Public Property reqTraineeID As Long = 0
            Public Property reqTestID As Long = 0
            Public Property reqGrade As Double = 0
            Public Property reqPass As Integer = 0
        End Class

        Public Function zProSearch(ByRef reqReturnData As IWESDataSearchReturn) As Boolean Implements IWESDataSearch.zProSearch
            Dim locSuccess As Boolean = False

            Dim locReturnObject As New ClsDataSearchReturnSave()
            Try
                Dim conn As New SqlConnection()
                conn.ConnectionString = WESSQLConnection.connString
                Dim cmd As New SqlCommand("Proc_TestAttemptSave", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@reqUpdateType", zlocInputParams.reqUpdateType)
                cmd.Parameters.AddWithValue("@reqTestAttemptID", zlocInputParams.reqTestAttemptID)
                cmd.Parameters.AddWithValue("@reqTraineeID", zlocInputParams.reqTraineeID)
                cmd.Parameters.AddWithValue("@reqTestID", zlocInputParams.reqTestID)
                cmd.Parameters.AddWithValue("@reqGrade", zlocInputParams.reqGrade)
                cmd.Parameters.AddWithValue("@reqPass", zlocInputParams.reqPass)

                conn.Open()
                Dim locSdr As SqlDataReader = cmd.ExecuteReader()
                Try
                    If locSdr.Read() Then
                        If Not (locSdr("UpdateStatus") Is DBNull.Value) Then
                            locReturnObject.zValUpdateStatus = locSdr("UpdateStatus") 'Integer
                        End If
                        If Not (locSdr("UpdateMessage") Is DBNull.Value) Then
                            locReturnObject.zValUpdateMessage = locSdr("UpdateMessage") 'String
                        End If
                        If Not (locSdr("TestAttemptID") Is DBNull.Value) Then
                            locReturnObject.zValUpdateID = locSdr("TestAttemptID") 'Long
                        End If
                    End If

                    locSuccess = True
                Catch ex As Exception
                    locSuccess = False

                    Dim locErrorHandler As New ErrorLogging
                    Dim locSF As StackFrame = New StackFrame()
                    Dim locMB As MethodBase = locSF.GetMethod()
                    Dim locClass As System.Type = locMB.DeclaringType
                    locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
                Finally
                    cmd.Dispose()
                    conn.Close()
                End Try
            Catch ex As Exception
                locSuccess = False

                Dim locErrorHandler As New ErrorLogging
                Dim locSF As StackFrame = New StackFrame()
                Dim locMB As MethodBase = locSF.GetMethod()
                Dim locClass As System.Type = locMB.DeclaringType
                locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
            Finally
                If locSuccess Then
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 0
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "no errors-procedure executed properly"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = "no errors-procedure executed properly"
                Else
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 1
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "550"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = ""
                End If
                reqReturnData = locReturnObject
            End Try

            Return locSuccess
        End Function

    End Class

    Public Class WESTestQuestion_Search
        Implements IWESDataSearch
        Private zlocInputParams As ClsInputParamsAllFields

        Public Sub New()
            zlocInputParams = New ClsInputParamsAllFields()
        End Sub

        Public Property zValInput As IWESDataSearch.IWESDataSearchInput Implements IWESDataSearch.zValInput
            Get
                Return zlocInputParams
            End Get
            Set(ByVal value As IWESDataSearch.IWESDataSearchInput)
                If TypeOf value Is ClsInputParamsTest Then
                    Dim locInput As ClsInputParamsTest = value
                    zlocInputParams.reqSearchValueOne = locInput.reqTestID

                    zlocInputParams.reqSearchType = 1
                ElseIf TypeOf value Is ClsInputParamsTestQuestion Then
                    Dim locInput As ClsInputParamsTestQuestion = value
                    zlocInputParams.reqSearchValueOne = locInput.reqTestQuestionID

                    zlocInputParams.reqSearchType = 2
                ElseIf TypeOf value Is ClsInputParamsTestRandom Then
                    Dim locInput As ClsInputParamsTestRandom = value
                    zlocInputParams.reqSearchValueOne = locInput.reqTestID

                    zlocInputParams.reqSearchType = 3
                Else
                    zlocInputParams = value
                End If
            End Set
        End Property

        Public Class ClsInputParamsAllFields
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqSearchType As Integer = 0
            Public Property reqSearchValueOne As String = "0"
        End Class

        Public Class ClsInputParamsTest
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqTestID As Long = 0
        End Class

        Public Class ClsInputParamsTestQuestion
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqTestQuestionID As Long = 0
        End Class

        Public Class ClsInputParamsTestRandom
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqTestID As Long = 0
        End Class

        Public Function zProSearch(ByRef reqReturnData As IWESDataSearchReturn) As Boolean Implements IWESDataSearch.zProSearch
            Dim locSuccess As Boolean = False

            Dim locReturnObject As New ClsDataSearchReturn()
            Try
                Dim conn As New SqlConnection()
                conn.ConnectionString = WESSQLConnection.connString
                conn.Open()
                Try
                    Dim locDB As New Database(conn)
                    locDB.EnableAutoSelect = False

                    locReturnObject.ZValReturnList = locDB.Query(Of ClsTestQuestion)("exec Proc_TestQuestionSummary @0, @1",
                                                                                     zlocInputParams.reqSearchType, zlocInputParams.reqSearchValueOne).ToList()
                    locSuccess = True
                Catch ex As Exception
                    locSuccess = False

                    Dim locErrorHandler As New ErrorLogging
                    Dim locSF As StackFrame = New StackFrame()
                    Dim locMB As MethodBase = locSF.GetMethod()
                    Dim locClass As System.Type = locMB.DeclaringType
                    locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
                Finally
                    conn.Close()
                End Try
            Catch ex As Exception
                locSuccess = False

                Dim locErrorHandler As New ErrorLogging
                Dim locSF As StackFrame = New StackFrame()
                Dim locMB As MethodBase = locSF.GetMethod()
                Dim locClass As System.Type = locMB.DeclaringType
                locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
            Finally
                If locSuccess Then
                    locReturnObject.ZValReturnErrorStatus.zValStatusError = 0
                    locReturnObject.ZValReturnErrorStatus.zValStatusErrorCode = "no errors-procedure executed properly"
                    locReturnObject.ZValReturnErrorStatus.zValStatusErrorMessage = "no errors-procedure executed properly"
                Else
                    locReturnObject.ZValReturnErrorStatus.zValStatusError = 1
                    locReturnObject.ZValReturnErrorStatus.zValStatusErrorCode = "550"
                    locReturnObject.ZValReturnErrorStatus.zValStatusErrorMessage = ""
                End If
                reqReturnData = locReturnObject
            End Try

            Return locSuccess
        End Function

    End Class

    Public Class WESTestQuestion_Save
        Implements IWESDataSearch
        Private zlocInputParams As ClsInputParamsAllFields

        Public Sub New()
            zlocInputParams = New ClsInputParamsAllFields()
        End Sub

        Public Property zValInput As IWESDataSearch.IWESDataSearchInput Implements IWESDataSearch.zValInput
            Get
                Return zlocInputParams
            End Get
            Set(ByVal value As IWESDataSearch.IWESDataSearchInput)
                If TypeOf value Is ClsInputParamsTestQuestionSave Then
                    Dim locInput As ClsInputParamsTestQuestionSave = value

                    zlocInputParams.reqTestQuestionID = locInput.reqTestQuestionID
                    zlocInputParams.reqTestID = locInput.reqTestID
                    zlocInputParams.reqQuestion = locInput.reqQuestion
                    zlocInputParams.reqAnswerExplanation = locInput.reqAnswerExplanation

                    zlocInputParams.reqUpdateType = 1
                ElseIf TypeOf value Is ClsInputParamsTestQuestionDelete Then
                    Dim locInput As ClsInputParamsTestQuestionDelete = value

                    zlocInputParams.reqTestQuestionID = locInput.reqTestQuestionID

                    zlocInputParams.reqUpdateType = 2
                Else
                    zlocInputParams = value
                End If
            End Set
        End Property

        Public Class ClsInputParamsAllFields
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqUpdateType As Integer = 0
            Public Property reqTestQuestionID As Long = 0
            Public Property reqTestID As Long = 0
            Public Property reqQuestion As String = ""
            Public Property reqAnswerExplanation As String = ""
        End Class

        Public Class ClsInputParamsTestQuestionSave
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqTestQuestionID As Long = 0
            Public Property reqTestID As Long = 0
            Public Property reqQuestion As String = ""
            Public Property reqAnswerExplanation As String = ""
        End Class

        Public Class ClsInputParamsTestQuestionDelete
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqTestQuestionID As Long = 0
        End Class

        Public Function zProSearch(ByRef reqReturnData As IWESDataSearchReturn) As Boolean Implements IWESDataSearch.zProSearch
            Dim locSuccess As Boolean = False

            Dim locReturnObject As New ClsDataSearchReturnSave()
            Try
                Dim conn As New SqlConnection()
                conn.ConnectionString = WESSQLConnection.connString
                Dim cmd As New SqlCommand("Proc_TestQuestionSave", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@reqUpdateType", zlocInputParams.reqUpdateType)
                cmd.Parameters.AddWithValue("@reqTestQuestionID", zlocInputParams.reqTestQuestionID)
                cmd.Parameters.AddWithValue("@reqTestID", zlocInputParams.reqTestID)
                cmd.Parameters.AddWithValue("@reqQuestion", zlocInputParams.reqQuestion)
                cmd.Parameters.AddWithValue("@reqAnswerExplanation", zlocInputParams.reqAnswerExplanation)

                conn.Open()
                Dim locSdr As SqlDataReader = cmd.ExecuteReader()
                Try
                    If locSdr.Read() Then
                        If Not (locSdr("UpdateStatus") Is DBNull.Value) Then
                            locReturnObject.zValUpdateStatus = locSdr("UpdateStatus") 'Integer
                        End If
                        If Not (locSdr("UpdateMessage") Is DBNull.Value) Then
                            locReturnObject.zValUpdateMessage = locSdr("UpdateMessage") 'String
                        End If
                        If Not (locSdr("TestQuestionID") Is DBNull.Value) Then
                            locReturnObject.zValUpdateID = locSdr("TestQuestionID") 'Long
                        End If
                    End If

                    locSuccess = True
                Catch ex As Exception
                    locSuccess = False

                    Dim locErrorHandler As New ErrorLogging
                    Dim locSF As StackFrame = New StackFrame()
                    Dim locMB As MethodBase = locSF.GetMethod()
                    Dim locClass As System.Type = locMB.DeclaringType
                    locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
                Finally
                    cmd.Dispose()
                    conn.Close()
                End Try
            Catch ex As Exception
                locSuccess = False

                Dim locErrorHandler As New ErrorLogging
                Dim locSF As StackFrame = New StackFrame()
                Dim locMB As MethodBase = locSF.GetMethod()
                Dim locClass As System.Type = locMB.DeclaringType
                locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
            Finally
                If locSuccess Then
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 0
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "no errors-procedure executed properly"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = "no errors-procedure executed properly"
                Else
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 1
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "550"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = ""
                End If
                reqReturnData = locReturnObject
            End Try

            Return locSuccess
        End Function

    End Class

    Public Class WESTestQuestionOption_Search
        Implements IWESDataSearch
        Private zlocInputParams As ClsInputParamsAllFields

        Public Sub New()
            zlocInputParams = New ClsInputParamsAllFields()
        End Sub

        Public Property zValInput As IWESDataSearch.IWESDataSearchInput Implements IWESDataSearch.zValInput
            Get
                Return zlocInputParams
            End Get
            Set(ByVal value As IWESDataSearch.IWESDataSearchInput)
                If TypeOf value Is ClsInputParamsTest Then
                    Dim locInput As ClsInputParamsTest = value
                    zlocInputParams.reqSearchValueOne = locInput.reqTestID

                    zlocInputParams.reqSearchType = 1
                ElseIf TypeOf value Is ClsInputParamsTestQuestion Then
                    Dim locInput As ClsInputParamsTestQuestion = value
                    zlocInputParams.reqSearchValueOne = locInput.reqTestQuestionID

                    zlocInputParams.reqSearchType = 2
                ElseIf TypeOf value Is ClsInputParamsTestQuestionOption Then
                    Dim locInput As ClsInputParamsTestQuestionOption = value
                    zlocInputParams.reqSearchValueOne = locInput.reqTestQuestionOptionID

                    zlocInputParams.reqSearchType = 3
                Else
                    zlocInputParams = value
                End If
            End Set
        End Property

        Public Class ClsInputParamsAllFields
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqSearchType As Integer = 0
            Public Property reqSearchValueOne As String = "0"
        End Class

        Public Class ClsInputParamsTest
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqTestID As Long = 0
        End Class

        Public Class ClsInputParamsTestQuestion
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqTestQuestionID As Long = 0
        End Class

        Public Class ClsInputParamsTestQuestionOption
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqTestQuestionOptionID As Long = 0
        End Class

        Public Function zProSearch(ByRef reqReturnData As IWESDataSearchReturn) As Boolean Implements IWESDataSearch.zProSearch
            Dim locSuccess As Boolean = False

            Dim locReturnObject As New ClsDataSearchReturn()
            Try
                Dim conn As New SqlConnection()
                conn.ConnectionString = WESSQLConnection.connString
                conn.Open()
                Try
                    Dim locDB As New Database(conn)
                    locDB.EnableAutoSelect = False

                    locReturnObject.ZValReturnList = locDB.Query(Of ClsTestQuestionOption)("exec Proc_TestQuestionOptionSummary @0, @1",
                                                                                           zlocInputParams.reqSearchType, zlocInputParams.reqSearchValueOne).ToList()
                    locSuccess = True
                Catch ex As Exception
                    locSuccess = False

                    Dim locErrorHandler As New ErrorLogging
                    Dim locSF As StackFrame = New StackFrame()
                    Dim locMB As MethodBase = locSF.GetMethod()
                    Dim locClass As System.Type = locMB.DeclaringType
                    locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
                Finally
                    conn.Close()
                End Try
            Catch ex As Exception
                locSuccess = False

                Dim locErrorHandler As New ErrorLogging
                Dim locSF As StackFrame = New StackFrame()
                Dim locMB As MethodBase = locSF.GetMethod()
                Dim locClass As System.Type = locMB.DeclaringType
                locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
            Finally
                If locSuccess Then
                    locReturnObject.ZValReturnErrorStatus.zValStatusError = 0
                    locReturnObject.ZValReturnErrorStatus.zValStatusErrorCode = "no errors-procedure executed properly"
                    locReturnObject.ZValReturnErrorStatus.zValStatusErrorMessage = "no errors-procedure executed properly"
                Else
                    locReturnObject.ZValReturnErrorStatus.zValStatusError = 1
                    locReturnObject.ZValReturnErrorStatus.zValStatusErrorCode = "550"
                    locReturnObject.ZValReturnErrorStatus.zValStatusErrorMessage = ""
                End If
                reqReturnData = locReturnObject
            End Try

            Return locSuccess
        End Function

    End Class

    Public Class WESTestQuestionOption_Save
        Implements IWESDataSearch
        Private zlocInputParams As ClsInputParamsAllFields

        Public Sub New()
            zlocInputParams = New ClsInputParamsAllFields()
        End Sub

        Public Property zValInput As IWESDataSearch.IWESDataSearchInput Implements IWESDataSearch.zValInput
            Get
                Return zlocInputParams
            End Get
            Set(ByVal value As IWESDataSearch.IWESDataSearchInput)
                If TypeOf value Is ClsInputParamsTestQuestionOptionSave Then
                    Dim locInput As ClsInputParamsTestQuestionOptionSave = value

                    zlocInputParams.reqTestQuestionOptionID = locInput.reqTestQuestionOptionID
                    zlocInputParams.reqTestQuestionID = locInput.reqTestQuestionID
                    zlocInputParams.reqTestID = locInput.reqTestID
                    zlocInputParams.reqOptionText = locInput.reqOptionText
                    zlocInputParams.reqCorrectAnswer = locInput.reqCorrectAnswer
                    zlocInputParams.reqSortOrder = locInput.reqSortOrder

                    zlocInputParams.reqUpdateType = 1
                ElseIf TypeOf value Is ClsInputParamsTestQuestionOptionDelete Then
                    Dim locInput As ClsInputParamsTestQuestionOptionDelete = value

                    zlocInputParams.reqTestQuestionOptionID = locInput.reqTestQuestionOptionID

                    zlocInputParams.reqUpdateType = 2
                Else
                    zlocInputParams = value
                End If
            End Set
        End Property

        Public Class ClsInputParamsAllFields
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqTestQuestionOptionID As Long = 0
            Public Property reqUpdateType As Integer = 0
            Public Property reqTestQuestionID As Long = 0
            Public Property reqTestID As Long = 0
            Public Property reqOptionText As String = ""
            Public Property reqCorrectAnswer As Integer = 0
            Public Property reqSortOrder As Integer = 0
        End Class

        Public Class ClsInputParamsTestQuestionOptionSave
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqTestQuestionOptionID As Long = 0
            Public Property reqTestQuestionID As Long = 0
            Public Property reqTestID As Long = 0
            Public Property reqOptionText As String = ""
            Public Property reqCorrectAnswer As Integer = 0
            Public Property reqSortOrder As Integer = 0
        End Class

        Public Class ClsInputParamsTestQuestionOptionDelete
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqTestQuestionOptionID As Long = 0
        End Class

        Public Function zProSearch(ByRef reqReturnData As IWESDataSearchReturn) As Boolean Implements IWESDataSearch.zProSearch
            Dim locSuccess As Boolean = False

            Dim locReturnObject As New ClsDataSearchReturnSave()
            Try
                Dim conn As New SqlConnection()
                conn.ConnectionString = WESSQLConnection.connString
                Dim cmd As New SqlCommand("Proc_TestQuestionOptionSave", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@reqUpdateType", zlocInputParams.reqUpdateType)
                cmd.Parameters.AddWithValue("@reqTestQuestionOptionID", zlocInputParams.reqTestQuestionOptionID)
                cmd.Parameters.AddWithValue("@reqTestQuestionID", zlocInputParams.reqTestQuestionID)
                cmd.Parameters.AddWithValue("@reqTestID", zlocInputParams.reqTestID)
                cmd.Parameters.AddWithValue("@reqOptionText", zlocInputParams.reqOptionText)
                cmd.Parameters.AddWithValue("@reqCorrectAnswer", zlocInputParams.reqCorrectAnswer)
                cmd.Parameters.AddWithValue("@reqSortOrder", zlocInputParams.reqSortOrder)

                conn.Open()
                Dim locSdr As SqlDataReader = cmd.ExecuteReader()
                Try
                    If locSdr.Read() Then
                        If Not (locSdr("UpdateStatus") Is DBNull.Value) Then
                            locReturnObject.zValUpdateStatus = locSdr("UpdateStatus") 'Integer
                        End If
                        If Not (locSdr("UpdateMessage") Is DBNull.Value) Then
                            locReturnObject.zValUpdateMessage = locSdr("UpdateMessage") 'String
                        End If
                        If Not (locSdr("TestQuestionOptionID") Is DBNull.Value) Then
                            locReturnObject.zValUpdateID = locSdr("TestQuestionOptionID") 'Long
                        End If
                    End If

                    locSuccess = True
                Catch ex As Exception
                    locSuccess = False

                    Dim locErrorHandler As New ErrorLogging
                    Dim locSF As StackFrame = New StackFrame()
                    Dim locMB As MethodBase = locSF.GetMethod()
                    Dim locClass As System.Type = locMB.DeclaringType
                    locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
                Finally
                    cmd.Dispose()
                    conn.Close()
                End Try
            Catch ex As Exception
                locSuccess = False

                Dim locErrorHandler As New ErrorLogging
                Dim locSF As StackFrame = New StackFrame()
                Dim locMB As MethodBase = locSF.GetMethod()
                Dim locClass As System.Type = locMB.DeclaringType
                locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
            Finally
                If locSuccess Then
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 0
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "no errors-procedure executed properly"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = "no errors-procedure executed properly"
                Else
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 1
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "550"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = ""
                End If
                reqReturnData = locReturnObject
            End Try

            Return locSuccess
        End Function

    End Class
#End Region

#Region "Trainee"
    Public Class WESTrainee_Search
        Implements IWESDataSearch
        Private zlocInputParams As ClsInputParamsAllFields

        Public Sub New()
            zlocInputParams = New ClsInputParamsAllFields()
        End Sub

        Public Property zValInput As IWESDataSearch.IWESDataSearchInput Implements IWESDataSearch.zValInput
            Get
                Return zlocInputParams
            End Get
            Set(ByVal value As IWESDataSearch.IWESDataSearchInput)
                If TypeOf value Is ClsInputParamsAllTrainees Then
                    Dim locInput As ClsInputParamsAllTrainees = value

                    zlocInputParams.reqSearchType = 1
                ElseIf TypeOf value Is ClsInputParamsTrainee Then
                    Dim locInput As ClsInputParamsTrainee = value
                    zlocInputParams.reqSearchValueOne = locInput.reqTraineeID

                    zlocInputParams.reqSearchType = 2
                ElseIf TypeOf value Is ClsInputParamsUsernameSearch Then
                    Dim locInput As ClsInputParamsUsernameSearch = value
                    zlocInputParams.reqSearchValueOne = locInput.reqUsername

                    zlocInputParams.reqSearchType = 3
                ElseIf TypeOf value Is ClsInputParamsLogin Then
                    Dim locInput As ClsInputParamsLogin = value
                    zlocInputParams.reqSearchValueOne = locInput.reqUsername
                    zlocInputParams.reqSearchValueTwo = locInput.reqPassword

                    zlocInputParams.reqSearchType = 4
                Else
                    zlocInputParams = value
                End If
            End Set
        End Property

        Public Class ClsInputParamsAllFields
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqSearchType As Integer = 0
            Public Property reqSearchValueOne As String = "0"
            Public Property reqSearchValueTwo As String = "0"
        End Class

        Public Class ClsInputParamsAllTrainees
            Implements IWESDataSearch.IWESDataSearchInput

        End Class

        Public Class ClsInputParamsTrainee
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqTraineeID As Long = 0
        End Class

        Public Class ClsInputParamsUsernameSearch
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqUsername As String = ""
        End Class

        Public Class ClsInputParamsLogin
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqUsername As String = ""
            Public Property reqPassword As String = ""
        End Class

        Public Function zProSearch(ByRef reqReturnData As IWESDataSearchReturn) As Boolean Implements IWESDataSearch.zProSearch
            Dim locSuccess As Boolean = False

            Dim locReturnObject As New ClsDataSearchReturn()
            Try
                Dim conn As New SqlConnection()
                conn.ConnectionString = WESSQLConnection.connString
                conn.Open()
                Try
                    Dim locDB As New Database(conn)
                    locDB.EnableAutoSelect = False

                    locReturnObject.zValReturnList = locDB.Query(Of ClsTrainee)("exec Proc_TraineeSummary @0, @1, @2",
                                                                                zlocInputParams.reqSearchType, zlocInputParams.reqSearchValueOne, zlocInputParams.reqSearchValueTwo).ToList()
                    locSuccess = True
                Catch ex As Exception
                    locSuccess = False

                    Dim locErrorHandler As New ErrorLogging
                    Dim locSF As StackFrame = New StackFrame()
                    Dim locMB As MethodBase = locSF.GetMethod()
                    Dim locClass As System.Type = locMB.DeclaringType
                    locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
                Finally
                    conn.Close()
                End Try
            Catch ex As Exception
                locSuccess = False

                Dim locErrorHandler As New ErrorLogging
                Dim locSF As StackFrame = New StackFrame()
                Dim locMB As MethodBase = locSF.GetMethod()
                Dim locClass As System.Type = locMB.DeclaringType
                locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
            Finally
                If locSuccess Then
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 0
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "no errors-procedure executed properly"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = "no errors-procedure executed properly"
                Else
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 1
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "550"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = ""
                End If
                reqReturnData = locReturnObject
            End Try

            Return locSuccess
        End Function

    End Class

    Public Class WESTrainee_Save
        Implements IWESDataSearch
        Private zlocInputParams As ClsInputParamsAllFields

        Public Sub New()
            zlocInputParams = New ClsInputParamsAllFields()
        End Sub

        Public Property zValInput As IWESDataSearch.IWESDataSearchInput Implements IWESDataSearch.zValInput
            Get
                Return zlocInputParams
            End Get
            Set(ByVal value As IWESDataSearch.IWESDataSearchInput)
                If TypeOf value Is ClsInputParamsSave Then
                    Dim locInput As ClsInputParamsSave = value

                    zlocInputParams.reqTraineeID = locInput.reqTraineeID
                    zlocInputParams.reqFirstName = locInput.reqFirstName
                    zlocInputParams.reqMiddleName = locInput.reqMiddleName
                    zlocInputParams.reqLastName = locInput.reqLastName
                    zlocInputParams.reqLastNameSuffix = locInput.reqLastNameSuffix
                    zlocInputParams.reqAddress1 = locInput.reqAddress1
                    zlocInputParams.reqAddress2 = locInput.reqAddress2
                    zlocInputParams.reqCity = locInput.reqCity
                    zlocInputParams.reqState = locInput.reqState
                    zlocInputParams.reqZipCode = locInput.reqZipCode
                    zlocInputParams.reqCountry = locInput.reqCountry
                    zlocInputParams.reqUsername = locInput.reqUsername
                    zlocInputParams.reqPassword = locInput.reqPassword
                    zlocInputParams.reqEmailAddress = locInput.reqEmailAddress
                    zlocInputParams.reqAdminUser = locInput.reqAdminUser

                    zlocInputParams.reqUpdateType = 1
                ElseIf TypeOf value Is ClsInputParamsSaveNoUser Then
                    Dim locInput As ClsInputParamsSaveNoUser = value

                    zlocInputParams.reqTraineeID = locInput.reqTraineeID
                    zlocInputParams.reqFirstName = locInput.reqFirstName
                    zlocInputParams.reqMiddleName = locInput.reqMiddleName
                    zlocInputParams.reqLastName = locInput.reqLastName
                    zlocInputParams.reqLastNameSuffix = locInput.reqLastNameSuffix
                    zlocInputParams.reqAddress1 = locInput.reqAddress1
                    zlocInputParams.reqAddress2 = locInput.reqAddress2
                    zlocInputParams.reqCity = locInput.reqCity
                    zlocInputParams.reqState = locInput.reqState
                    zlocInputParams.reqZipCode = locInput.reqZipCode
                    zlocInputParams.reqCountry = locInput.reqCountry
                    zlocInputParams.reqEmailAddress = locInput.reqEmailAddress
                    zlocInputParams.reqAdminUser = locInput.reqAdminUser

                    zlocInputParams.reqUpdateType = 5
                ElseIf TypeOf value Is ClsInputParamsSaveNoUserNoAdmin Then
                    Dim locInput As ClsInputParamsSaveNoUserNoAdmin = value

                    zlocInputParams.reqTraineeID = locInput.reqTraineeID
                    zlocInputParams.reqFirstName = locInput.reqFirstName
                    zlocInputParams.reqMiddleName = locInput.reqMiddleName
                    zlocInputParams.reqLastName = locInput.reqLastName
                    zlocInputParams.reqLastNameSuffix = locInput.reqLastNameSuffix
                    zlocInputParams.reqAddress1 = locInput.reqAddress1
                    zlocInputParams.reqAddress2 = locInput.reqAddress2
                    zlocInputParams.reqCity = locInput.reqCity
                    zlocInputParams.reqState = locInput.reqState
                    zlocInputParams.reqZipCode = locInput.reqZipCode
                    zlocInputParams.reqCountry = locInput.reqCountry
                    zlocInputParams.reqEmailAddress = locInput.reqEmailAddress

                    zlocInputParams.reqUpdateType = 6
                ElseIf TypeOf value Is ClsInputParamsDelete Then
                    Dim locInput As ClsInputParamsDelete = value

                    zlocInputParams.reqTraineeID = locInput.reqTraineeID

                    zlocInputParams.reqUpdateType = 2
                ElseIf TypeOf value Is ClsInputParamsChangePassword Then
                    Dim locInput As ClsInputParamsChangePassword = value

                    zlocInputParams.reqTraineeID = locInput.reqTraineeID
                    zlocInputParams.reqPassword = locInput.reqPassword

                    zlocInputParams.reqUpdateType = 3
                ElseIf TypeOf value Is ClsInputParamsVerifyEmail Then
                    Dim locInput As ClsInputParamsVerifyEmail = value

                    zlocInputParams.reqRowGuid = locInput.reqRowGuid

                    zlocInputParams.reqUpdateType = 4
                Else
                    zlocInputParams = value
                End If
            End Set
        End Property

        Public Class ClsInputParamsAllFields
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqUpdateType As Integer = 0
            Public Property reqTraineeID As Long = 0
            Public Property reqFirstName As String = ""
            Public Property reqMiddleName As String = ""
            Public Property reqLastName As String = ""
            Public Property reqLastNameSuffix As String = ""
            Public Property reqAddress1 As String = ""
            Public Property reqAddress2 As String = ""
            Public Property reqCity As String = ""
            Public Property reqState As Long = 0
            Public Property reqZipCode As String = ""
            Public Property reqCountry As Long = 0
            Public Property reqUsername As String = ""
            Public Property reqPassword As String = ""
            Public Property reqEmailAddress As String = ""
            Public Property reqAdminUser As Integer = 0
            Public Property reqRowGuid As String = ""
        End Class

        Public Class ClsInputParamsSave
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqTraineeID As Long = 0
            Public Property reqFirstName As String = ""
            Public Property reqMiddleName As String = ""
            Public Property reqLastName As String = ""
            Public Property reqLastNameSuffix As String = ""
            Public Property reqAddress1 As String = ""
            Public Property reqAddress2 As String = ""
            Public Property reqCity As String = ""
            Public Property reqState As Long = 0
            Public Property reqZipCode As String = ""
            Public Property reqCountry As Long = 0
            Public Property reqUsername As String = ""
            Public Property reqPassword As String = ""
            Public Property reqEmailAddress As String = ""
            Public Property reqAdminUser As Integer = 0
        End Class

        Public Class ClsInputParamsSaveNoUser
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqTraineeID As Long = 0
            Public Property reqFirstName As String = ""
            Public Property reqMiddleName As String = ""
            Public Property reqLastName As String = ""
            Public Property reqLastNameSuffix As String = ""
            Public Property reqAddress1 As String = ""
            Public Property reqAddress2 As String = ""
            Public Property reqCity As String = ""
            Public Property reqState As Long = 0
            Public Property reqZipCode As String = ""
            Public Property reqCountry As Long = 0
            Public Property reqEmailAddress As String = ""
            Public Property reqAdminUser As Integer = 0
        End Class

        Public Class ClsInputParamsSaveNoUserNoAdmin
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqTraineeID As Long = 0
            Public Property reqFirstName As String = ""
            Public Property reqMiddleName As String = ""
            Public Property reqLastName As String = ""
            Public Property reqLastNameSuffix As String = ""
            Public Property reqAddress1 As String = ""
            Public Property reqAddress2 As String = ""
            Public Property reqCity As String = ""
            Public Property reqState As Long = 0
            Public Property reqZipCode As String = ""
            Public Property reqCountry As Long = 0
            Public Property reqEmailAddress As String = ""
        End Class

        Public Class ClsInputParamsChangePassword

            Public Property reqTraineeID As Long = 0
            Public Property reqPassword As String = ""
        End Class

        Public Class ClsInputParamsDelete
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqTraineeID As Long = 0
        End Class

        Public Class ClsInputParamsVerifyEmail
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqRowGuid As String = ""
        End Class

        Public Function zProSearch(ByRef reqReturnData As IWESDataSearchReturn) As Boolean Implements IWESDataSearch.zProSearch
            Dim locSuccess As Boolean = False

            Dim locReturnObject As New ClsDataSearchReturnSave()
            Try
                Dim conn As New SqlConnection()
                conn.ConnectionString = WESSQLConnection.connString
                Dim cmd As New SqlCommand("Proc_TraineeSave", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@reqUpdateType", zlocInputParams.reqUpdateType)
                cmd.Parameters.AddWithValue("@reqTraineeID", zlocInputParams.reqTraineeID)
                cmd.Parameters.AddWithValue("@reqFirstName", zlocInputParams.reqFirstName)
                cmd.Parameters.AddWithValue("@reqMiddleName", zlocInputParams.reqMiddleName)
                cmd.Parameters.AddWithValue("@reqLastName", zlocInputParams.reqLastName)
                cmd.Parameters.AddWithValue("@reqLastNameSuffix", zlocInputParams.reqLastNameSuffix)
                cmd.Parameters.AddWithValue("@reqAddress1", zlocInputParams.reqAddress1)
                cmd.Parameters.AddWithValue("@reqAddress2", zlocInputParams.reqAddress2)
                cmd.Parameters.AddWithValue("@reqCity", zlocInputParams.reqCity)
                cmd.Parameters.AddWithValue("@reqState", zlocInputParams.reqState)
                cmd.Parameters.AddWithValue("@reqZipCode", zlocInputParams.reqZipCode)
                cmd.Parameters.AddWithValue("@reqCountry", zlocInputParams.reqCountry)
                cmd.Parameters.AddWithValue("@reqUsername", zlocInputParams.reqUsername)
                cmd.Parameters.AddWithValue("@reqPassword", zlocInputParams.reqPassword)
                cmd.Parameters.AddWithValue("@reqEmailAddress", zlocInputParams.reqEmailAddress)
                cmd.Parameters.AddWithValue("@reqAdminUser", zlocInputParams.reqAdminUser)
                cmd.Parameters.AddWithValue("@reqRowGuid", zlocInputParams.reqRowGuid)

                conn.Open()
                Dim locSdr As SqlDataReader = cmd.ExecuteReader()
                Try
                    If locSdr.Read() Then
                        If Not (locSdr("UpdateStatus") Is DBNull.Value) Then
                            locReturnObject.zValUpdateStatus = locSdr("UpdateStatus") 'Integer
                        End If
                        If Not (locSdr("UpdateMessage") Is DBNull.Value) Then
                            locReturnObject.zValUpdateMessage = locSdr("UpdateMessage") 'String
                        End If
                        If Not (locSdr("TraineeID") Is DBNull.Value) Then
                            locReturnObject.zValUpdateID = locSdr("TraineeID") 'Long
                        End If
                        If Not (locSdr("TraineeGUID") Is DBNull.Value) Then
                            locReturnObject.zValUpdateRowGUID = locSdr("TraineeGUID") 'String
                        End If
                    End If

                    locSuccess = True
                Catch ex As Exception
                    locSuccess = False

                    Dim locErrorHandler As New ErrorLogging
                    Dim locSF As StackFrame = New StackFrame()
                    Dim locMB As MethodBase = locSF.GetMethod()
                    Dim locClass As System.Type = locMB.DeclaringType
                    locErrorHandler.EmailError(locMB.Name + ": Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
                Finally
                    cmd.Dispose()
                    conn.Close()
                End Try
            Catch ex As Exception
                locSuccess = False

                Dim locErrorHandler As New ErrorLogging
                Dim locSF As StackFrame = New StackFrame()
                Dim locMB As MethodBase = locSF.GetMethod()
                Dim locClass As System.Type = locMB.DeclaringType
                locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
            Finally
                If locSuccess Then
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 0
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "no errors-procedure executed properly"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = "no errors-procedure executed properly"
                Else
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 1
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "550"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = ""
                End If
                reqReturnData = locReturnObject
            End Try

            Return locSuccess
        End Function

    End Class
#End Region

#Region "Trainee Video"
    Public Class WESTraineeVideo_Search
        Implements IWESDataSearch
        Private zlocInputParams As ClsInputParamsAllFields

        Public Sub New()
            zlocInputParams = New ClsInputParamsAllFields()
        End Sub

        Public Property zValInput As IWESDataSearch.IWESDataSearchInput Implements IWESDataSearch.zValInput
            Get
                Return zlocInputParams
            End Get
            Set(ByVal value As IWESDataSearch.IWESDataSearchInput)
                If TypeOf value Is ClsInputParamsDateRange Then
                    Dim locInput As ClsInputParamsDateRange = value
                    zlocInputParams.reqSearchValueOne = locInput.reqVideoStatus
                    zlocInputParams.reqDateOne = locInput.reqStartDate
                    zlocInputParams.reqDateTwo = locInput.reqEndDate

                    zlocInputParams.reqSearchType = 1
                ElseIf TypeOf value Is ClsInputParamsTrainee Then
                    Dim locInput As ClsInputParamsTrainee = value
                    zlocInputParams.reqSearchValueOne = locInput.reqTraineeID

                    zlocInputParams.reqSearchType = 2
                ElseIf TypeOf value Is ClsInputParamsTraineeVideo Then
                    Dim locInput As ClsInputParamsTraineeVideo = value
                    zlocInputParams.reqSearchValueOne = locInput.reqTraineeVideoID

                    zlocInputParams.reqSearchType = 3
                Else
                    zlocInputParams = value
                End If
            End Set
        End Property

        Public Class ClsInputParamsAllFields
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqSearchType As Integer = 0
            Public Property reqSearchValueOne As String = "0"
            Public Property reqDateOne As DateTime = "1/1/1990"
            Public Property reqDateTwo As DateTime = DateTime.Now
        End Class

        Public Class ClsInputParamsDateRange
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqVideoStatus As Integer = 0
            Public Property reqStartDate As DateTime = "1/1/1990"
            Public Property reqEndDate As DateTime = DateTime.Now
        End Class

        Public Class ClsInputParamsTrainee
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqTraineeID As Long = 0
        End Class

        Public Class ClsInputParamsTraineeVideo
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqTraineeVideoID As Long = 0
        End Class

        Public Function zProSearch(ByRef reqReturnData As IWESDataSearchReturn) As Boolean Implements IWESDataSearch.zProSearch
            Dim locSuccess As Boolean = False

            Dim locReturnObject As New ClsDataSearchReturn()
            Try
                Dim conn As New SqlConnection()
                conn.ConnectionString = WESSQLConnection.connString
                conn.Open()
                Try
                    Dim locDB As New Database(conn)
                    locDB.EnableAutoSelect = False

                    locReturnObject.zValReturnList = locDB.Query(Of ClsTraineeVideo)("exec Proc_TraineeVideoSummary @0, @1, @2, @3",
                                                                                     zlocInputParams.reqSearchType, zlocInputParams.reqSearchValueOne, zlocInputParams.reqDateOne, zlocInputParams.reqDateTwo).ToList()
                    locSuccess = True
                Catch ex As Exception
                    locSuccess = False

                    Dim locErrorHandler As New ErrorLogging
                    Dim locSF As StackFrame = New StackFrame()
                    Dim locMB As MethodBase = locSF.GetMethod()
                    Dim locClass As System.Type = locMB.DeclaringType
                    locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
                Finally
                    conn.Close()
                End Try
            Catch ex As Exception
                locSuccess = False

                Dim locErrorHandler As New ErrorLogging
                Dim locSF As StackFrame = New StackFrame()
                Dim locMB As MethodBase = locSF.GetMethod()
                Dim locClass As System.Type = locMB.DeclaringType
                locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
            Finally
                If locSuccess Then
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 0
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "no errors-procedure executed properly"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = "no errors-procedure executed properly"
                Else
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 1
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "550"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = ""
                End If
                reqReturnData = locReturnObject
            End Try

            Return locSuccess
        End Function

    End Class

    Public Class WESTraineeVideo_Save
        Implements IWESDataSearch
        Private zlocInputParams As ClsInputParamsAllFields

        Public Sub New()
            zlocInputParams = New ClsInputParamsAllFields()
        End Sub

        Public Property zValInput As IWESDataSearch.IWESDataSearchInput Implements IWESDataSearch.zValInput
            Get
                Return zlocInputParams
            End Get
            Set(ByVal value As IWESDataSearch.IWESDataSearchInput)
                If TypeOf value Is ClsInputParamsSave Then
                    Dim locInput As ClsInputParamsSave = value

                    zlocInputParams.reqTraineeVideoID = locInput.reqTraineeVideoID
                    zlocInputParams.reqTraineeID = locInput.reqTraineeID
                    zlocInputParams.reqYouTubeID = locInput.reqYouTubeID
                    zlocInputParams.reqVideoStatus = locInput.reqVideoStatus
                    zlocInputParams.reqStatusMessage = locInput.reqStatusMessage

                    zlocInputParams.reqUpdateType = 1
                ElseIf TypeOf value Is ClsInputParamsUpdateStatus Then
                    Dim locInput As ClsInputParamsUpdateStatus = value

                    zlocInputParams.reqTraineeVideoID = locInput.reqTraineeVideoID
                    zlocInputParams.reqVideoStatus = locInput.reqVideoStatus
                    zlocInputParams.reqStatusMessage = locInput.reqStatusMessage

                    zlocInputParams.reqUpdateType = 2
                Else
                    zlocInputParams = value
                End If
            End Set
        End Property

        Public Class ClsInputParamsAllFields
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqUpdateType As Integer = 0
            Public Property reqTraineeVideoID As Long = 0
            Public Property reqTraineeID As Long = 0
            Public Property reqYouTubeID As String = ""
            Public Property reqVideoStatus As Integer = 0
            Public Property reqStatusMessage As String = ""
        End Class

        Public Class ClsInputParamsSave
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqTraineeVideoID As Long = 0
            Public Property reqTraineeID As Long = 0
            Public Property reqYouTubeID As String = ""
            Public Property reqVideoStatus As Integer = 0
            Public Property reqStatusMessage As String = ""
        End Class

        Public Class ClsInputParamsUpdateStatus
            Implements IWESDataSearch.IWESDataSearchInput

            Public Property reqTraineeVideoID As Long = 0
            Public Property reqVideoStatus As Integer = 0
            Public Property reqStatusMessage As String = ""
        End Class

        Public Function zProSearch(ByRef reqReturnData As IWESDataSearchReturn) As Boolean Implements IWESDataSearch.zProSearch
            Dim locSuccess As Boolean = False

            Dim locReturnObject As New ClsDataSearchReturnSave()
            Try
                Dim conn As New SqlConnection()
                conn.ConnectionString = WESSQLConnection.connString
                Dim cmd As New SqlCommand("Proc_TraineeVideoSave", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@reqUpdateType", zlocInputParams.reqUpdateType)
                cmd.Parameters.AddWithValue("@reqTraineeVideoID", zlocInputParams.reqTraineeVideoID)
                cmd.Parameters.AddWithValue("@reqTraineeID", zlocInputParams.reqTraineeID)
                cmd.Parameters.AddWithValue("@reqYouTubeID", zlocInputParams.reqYouTubeID)
                cmd.Parameters.AddWithValue("@reqVideoStatus", zlocInputParams.reqVideoStatus)
                cmd.Parameters.AddWithValue("@reqStatusMessage", zlocInputParams.reqStatusMessage)

                conn.Open()
                Dim locSdr As SqlDataReader = cmd.ExecuteReader()
                Try
                    If locSdr.Read() Then
                        If Not (locSdr("UpdateStatus") Is DBNull.Value) Then
                            locReturnObject.zValUpdateStatus = locSdr("UpdateStatus") 'Integer
                        End If
                        If Not (locSdr("UpdateMessage") Is DBNull.Value) Then
                            locReturnObject.zValUpdateMessage = locSdr("UpdateMessage") 'String
                        End If
                        If Not (locSdr("TraineeVideoID") Is DBNull.Value) Then
                            locReturnObject.zValUpdateID = locSdr("TraineeVideoID") 'Long
                        End If
                    End If

                    locSuccess = True
                Catch ex As Exception
                    locSuccess = False

                    Dim locErrorHandler As New ErrorLogging
                    Dim locSF As StackFrame = New StackFrame()
                    Dim locMB As MethodBase = locSF.GetMethod()
                    Dim locClass As System.Type = locMB.DeclaringType
                    locErrorHandler.EmailError(locMB.Name + ": Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
                Finally
                    cmd.Dispose()
                    conn.Close()
                End Try
            Catch ex As Exception
                locSuccess = False

                Dim locErrorHandler As New ErrorLogging
                Dim locSF As StackFrame = New StackFrame()
                Dim locMB As MethodBase = locSF.GetMethod()
                Dim locClass As System.Type = locMB.DeclaringType
                locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
            Finally
                If locSuccess Then
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 0
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "no errors-procedure executed properly"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = "no errors-procedure executed properly"
                Else
                    locReturnObject.zValReturnErrorStatus.zValStatusError = 1
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorCode = "550"
                    locReturnObject.zValReturnErrorStatus.zValStatusErrorMessage = ""
                End If
                reqReturnData = locReturnObject
            End Try

            Return locSuccess
        End Function

    End Class
#End Region

    Public Class ZMaintenance
        Public Shared Sub ClearSessions()
            Try
                Dim conn As New SqlConnection()
                conn.ConnectionString = WESSQLConnection.connString
                Dim cmd As New SqlCommand("DeleteExpiredSessions", conn)
                cmd.CommandType = CommandType.StoredProcedure

                conn.Open()
                cmd.ExecuteNonQuery()

                cmd.Dispose()
                conn.Close()
            Catch ex As Exception
                Dim locErrorHandler As New ErrorLogging
                Dim locSF As StackFrame = New StackFrame()
                Dim locMB As MethodBase = locSF.GetMethod()
                Dim locClass As System.Type = locMB.DeclaringType
                locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
            End Try
        End Sub
    End Class

End Namespace


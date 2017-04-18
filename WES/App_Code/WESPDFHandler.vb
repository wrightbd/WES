Imports System.IO
Imports System.Web
Imports WESClass.WESSessions

Namespace WES.Handlers
    Public Class WESPDFHandler
        Implements IHttpHandler, IRequiresSessionState

        ReadOnly zVarLoginSession As New ClsLoginUser_Session

        Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
            Get
                ' Return false in case your Managed Handler cannot be reused for another request.
                ' Usually this would be false in case you have some state information preserved per request.
                Return True
            End Get
        End Property

        Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
            Select Case context.Request.HttpMethod
                Case "GET"
                    ' Is the user logged-in?
                    If zVarLoginSession.TraineeID <= 0 Then
                        context.Response.Redirect("~/Login.aspx")
                        Return
                    Else
                        Dim requestedFile As String = context.Server.MapPath(context.Request.FilePath)

                        SendContentTypeAndFile(context, requestedFile)
                    End If

                    Exit Select
            End Select
        End Sub

        Private Function SendContentTypeAndFile(context As HttpContext, strFile As [String]) As HttpContext
            context.Response.ContentType = GetContentType(strFile)
            context.Response.TransmitFile(strFile)
            context.Response.End()

            Return context
        End Function

        Private Function GetContentType(filename As String) As String
            ' used to set the encoding for the reponse stream
            Dim res As String = Nothing
            Dim fileinfo As New FileInfo(filename)

            If fileinfo.Exists Then
                Select Case fileinfo.Extension.Remove(0, 1).ToLower()
                    Case "pdf"
                        If True Then
                            res = "application/pdf"
                            Exit Select
                        End If
                End Select

                Return res
            End If

            Return Nothing
        End Function

    End Class
End Namespace
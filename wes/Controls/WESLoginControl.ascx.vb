Imports System.IO
Imports System.Reflection
Imports System.Security.Cryptography
Imports WESClass.WESAppVars
Imports WESClass.WESSecurity
Imports WESClass.WESSessions

Public Class WESLoginControl
    Inherits System.Web.UI.UserControl

    ReadOnly zVarCurrentLogin As New ClsLoginUser_Session
    ReadOnly zVarSecurity As New ClsLoginSecurity

    Dim userCookie As New HttpCookie("WESTrain")

    Public Property ShowCreateAccount As Boolean = True

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ClearForm()
        End If
    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim locLoginSecurity As New ClsLoginSecurity
        Dim locMessage As String = ""

        If txtUsername.Text <> "" AndAlso txtPassword.Text <> "" Then
            If chkRememberMe.Checked Then
                If Not [String].IsNullOrEmpty(Server.HtmlEncode(txtUsername.Text) <> "") Then
                    userCookie.Values("TRAINEENAME") = zVarSecurity.EncryptCookie(Server.HtmlEncode(txtUsername.Text))
                Else
                    userCookie.Values("TRAINEENAME") = ""
                End If

                If Not [String].IsNullOrEmpty(Server.HtmlEncode(txtPassword.Text) <> "") Then
                    userCookie.Values("TRAINEEPWD") = zVarSecurity.EncryptCookie(Server.HtmlEncode(txtPassword.Text))
                Else
                    userCookie.Values("TRAINEEPWD") = ""
                End If

                userCookie.Expires = DateTime.Now.AddYears(1)
                Response.Cookies.Add(userCookie)
            Else
                userCookie.Values("TRAINEENAME") = ""
                userCookie.Values("TRAINEEPWD") = ""
                userCookie.Expires = DateTime.Now.AddDays(-1)
                Response.Cookies.Add(userCookie)
            End If

            If locLoginSecurity.ProcessCredentials(txtUsername.Text, txtPassword.Text, locMessage) Then
                pnlLogin.Visible = zVarCurrentLogin.TraineeID < 1
                pnlEmail.Visible = zVarCurrentLogin.TraineeID > 0 AndAlso zVarCurrentLogin.EmailStatus = 0

                If Not IsNothing(Page.Master) Then
                    Dim locMaster As SiteMaster = Page.Master
                    locMaster.RefreshMenu()
                End If

                If zVarCurrentLogin.TraineeID > 0 AndAlso zVarCurrentLogin.EmailStatus > 0 Then
                    Response.Redirect("~/Default.aspx")
                End If
            Else
                pnlLogin.Visible = True
                pnlEmail.Visible = False

                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "LoginScript", "alert('Error verifying login information: " + locMessage + "');", True)
            End If
        Else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "LoginScript", "alert('Error verifying login information: Missing Username or Password.');", True)
        End If
    End Sub


    Private Sub ClearForm()
        pnlLogin.Visible = zVarCurrentLogin.TraineeID < 1
        pnlEmail.Visible = zVarCurrentLogin.TraineeID > 0 AndAlso zVarCurrentLogin.EmailStatus = 0

        If Not Request.Cookies("WESTrain") Is Nothing Then
            txtUsername.Text = zVarSecurity.DecryptCookie(Request.Cookies("WESTrain")("TRAINEENAME"))
            txtPassword.Attributes.Add("value", zVarSecurity.DecryptCookie(Request.Cookies("WESTrain")("TRAINEEPWD")))
            chkRememberMe.Checked = True
        Else
            txtUsername.Text = ""
            txtPassword.Text = ""
            chkRememberMe.Checked = False
        End If

        linkCreateAccount.Visible = ShowCreateAccount
    End Sub

    Protected Sub btnResendEmail_Click(sender As Object, e As EventArgs) Handles btnResendEmail.Click
        Try
            Dim locFromName As String = WebsiteValues.FromName
            Dim locFromAddress As String = WebsiteValues.FromAddress

            Dim locBody As String = "Please click the following link or copy the address into your browser to verify your email address:<br /><br /><a href='" + WebsiteValues.WebsiteAddress + "/VerifyEmail.aspx?p1=" + zVarCurrentLogin.TraineeGUID + "'>Click Here</a><br /><br />" + WebsiteValues.WebsiteAddress + "/VerifyEmail.aspx?p1=" + zVarCurrentLogin.TraineeGUID

            Email.SendEmail(zVarCurrentLogin.TraineeEmail, locFromAddress, locFromName, "Please verify your email address", locBody)

            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "ResendEmailScript", "alert('Email has been sent. If you do not receive it in the next few minutes, please check your spam folder.');", True)
        Catch ex As Exception
            Dim locErrorHandler As New ErrorLogging
            Dim locSF As StackFrame = New StackFrame()
            Dim locMB As MethodBase = locSF.GetMethod()
            Dim locClass As System.Type = locMB.DeclaringType
            locErrorHandler.EmailError(locMB.Name + ":Produced EXCEPTION", ex.Message, ex, locMB.Name, locClass.Name)
        End Try
    End Sub
End Class
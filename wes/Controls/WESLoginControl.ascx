<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="WESLoginControl.ascx.vb" Inherits="WES.WESLoginControl" %>

<asp:Panel ID="pnlLogin" runat="server">
    <div class="panel panel-darkblue">
        <div class="panel-heading">
            <h3 style="color: white;">Member Login&nbsp;&nbsp;&nbsp;<span style="color: #d8282f;">|</span>&nbsp;&nbsp;<asp:HyperLink ID="linkCreateAccount" runat="server" NavigateUrl="https://www.worldexercisesystem.com/CreateAccount.aspx" style="color: white;">Create Account</asp:HyperLink></h3>
        </div>
        <table class="table table-condensed table-customgray">
            <tr>
                <td><label for="txtUsername">Username:&nbsp;</label></td>
                <td><asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" ClientIDMode="Static" placeholder="Email Address"></asp:TextBox></td>
            </tr>
            <tr>
                <td><label for="txtPassword">Password:&nbsp;</label></td>
                <td><asp:TextBox ID="txtPassword" TextMode="Password" CssClass="form-control" runat="server" ClientIDMode="Static" placeholder="Password"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2" style="vertical-align: top;">
                    <label for="chkRememberMe">Remember Login:&nbsp;</label><asp:CheckBox ID="chkRememberMe" runat="server" ClientIDMode="Static" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: right;">
                    <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-danger" />
                </td>
            </tr>
        </table>
    </div>
</asp:Panel>
<asp:Panel ID="pnlEmail" runat="server">
    <h2>Your email has not been verified yet. Please click the link in the email address supplied.</h2>
    <asp:Button ID="btnResendEmail" runat="server" CssClass="btn btn-link" Text="Resend Verification Email" />
</asp:Panel>
<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="WESLoginControl.ascx.vb" Inherits="WES.WESLoginControl" %>

<asp:Panel ID="pnlLogin" runat="server">
    <div class="form-horizontal">
        <div class="form-group">
            <label for="txtUsername" class="col-md-2 control-label">Username:</label>
            <div class="col-md-10">
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" ClientIDMode="Static" placeholder="Email Address" />
            </div>
        </div>

        <div class="form-group">
            <label for="txtPassword" class="col-md-2 control-label">Password:</label>
            <div class="col-md-10">
                <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="form-control" runat="server" ClientIDMode="Static" placeholder="Password" />
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <div class="checkbox">
                    <label>
                        <asp:CheckBox ID="chkRememberMe" runat="server" ClientIDMode="Static" />
                        <strong>Remember Login</strong>
                    </label>
                </div>
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-12 text-right">
                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-danger" />
            </div>
        </div>
    </div>
</asp:Panel>

<asp:Panel ID="pnlEmail" runat="server">
    <h2>Your email has not been verified yet. Please click the link in the email address supplied.</h2>
    <asp:Button ID="btnResendEmail" runat="server" CssClass="btn btn-link" Text="Resend Verification Email" />
</asp:Panel>

<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master" CodeBehind="VerifyEmail.aspx.vb" Inherits="WES.VerifyEmail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"/>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-sm-12">
                <asp:Panel ID="pnlSuccess" runat="server">
                    Your email was verified successfully.<br />
                    <br />
                    <a href="Default.aspx">Click Here to login</a>
                </asp:Panel>
                <asp:Panel ID="pnlError" runat="server">
                    There was an issue verifying your email address:
                    <asp:Label ID="lblError" runat="server"/>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>

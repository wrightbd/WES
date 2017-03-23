<%@ Page Title="Login" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master" CodeBehind="Login.aspx.vb" Inherits="WES.Login" %>

<%@ Register TagPrefix="WES" TagName="WESLoginControl" Src="~/Controls/WESLoginControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="description" content="Log into World Exercise System." />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-sm-12">
                <div class="page-header">
                    <h1>Member Login</h1>
                </div>

                <WES:WESLoginControl ID="WESLoginControl1" runat="server" />
                Need an account?
                <asp:HyperLink ID="linkCreateAccount" runat="server" NavigateUrl="https://www.worldexercisesystem.com/CreateAccount.aspx">Create one now.</asp:HyperLink>
            </div>
        </div>
    </div>
</asp:Content>

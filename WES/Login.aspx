<%@ Page Title="Login" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master" CodeBehind="Login.aspx.vb" Inherits="WES.Login" %>

<%@ Register TagPrefix="WES" TagName="WESLoginControl" Src="~/Controls/WESLoginControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="description" content="Log into World Exercise System." />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-xs-12">
                <div class="page-header">
                    <h1>Member Login</h1>
                </div>
            </div>
            <div class="col-sm-6 col-sm-offset-3 col-md-4 col-md-offset-4">
                <div class="well well-lg">
                    <WES:WESLoginControl ID="WESLoginControl1" runat="server" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<%@ Page Title="Create Your Account" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master" CodeBehind="CreateAccount.aspx.vb" Inherits="WES.CreateAccount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="description" content="Create your World Exercise System account and get started toward your Certified Personal Trainer Certificate." />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <br />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-pagetitle">
                <div class="panel-heading">
                    <h2>Create Account</h2>
                </div>
            </div>
        </div>
    </div>
    <div class="form form-horizontal">
        <div class="row">
            <div class="col-md-12">
                <h2 style="font-family: 'Franchise Bold'; color: #055385;">Login Information</h2>
            </div>
        </div>
         <div class="row">
            <div class="col-md-6" style="color: #055385;">
                Username (Email Address): *
                <asp:TextBox ID="txtUsername" runat="server" ClientIDMode="Static" CssClass="form-control" placeholder="Email Address" />
            </div>
            <div class="col-md-6" style="color: #055385;">
                Confirm Email Address: *
                <asp:TextBox ID="txtConfirmEmail" runat="server" ClientIDMode="Static" CssClass="form-control" placeholder="Confirm Email Address"/>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6" style="color: #055385;">
                Password: *
                <asp:TextBox ID="txtPassword" runat="server" ClientIDMode="Static" TextMode="Password" CssClass="form-control" placeholder="Password" />
            </div>
            <div class="col-md-6" style="color: #055385;">
                Confirm Password: *
                <asp:TextBox ID="txtConfirmPass" runat="server" ClientIDMode="Static" TextMode="Password" CssClass="form-control" placeholder="Confirm Password" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                &nbsp;
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <h2 style="font-family: 'Franchise Bold'; color: #055385;">Personal Information</h2>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                First Name: * 
                <asp:TextBox ID="txtFirstName" runat="server" ClientIDMode="Static" CssClass="form-control" />
            </div>
            <div class="col-md-4">
                Middle Name:
                <asp:TextBox ID="txtMiddleName" runat="server" ClientIDMode="Static" CssClass="form-control" />
            </div>
            <div class="col-md-4">
                Last Name: *
                <asp:TextBox ID="txtLastName" runat="server" ClientIDMode="Static" CssClass="form-control" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                &nbsp;
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                Address:
                <asp:TextBox ID="txtAddress1" runat="server" ClientIDMode="Static" CssClass="form-control" />
            </div>
            <div class="col-md-6">
                &nbsp;
                <asp:TextBox ID="txtAddress2" runat="server" ClientIDMode="Static" CssClass="form-control" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                City:
                <asp:TextBox ID="txtCity" runat="server" ClientIDMode="Static" CssClass="form-control" />
            </div>
            <div class="col-md-4">
                State:
                <asp:DropDownList ID="ddlState" runat="server" ClientIDMode="Static" DataSourceID="linqState" DataTextField="StateName" DataValueField="ZStateID" CssClass="form-control" />
                <asp:LinqDataSource ID="linqState" runat="server" />
            </div>
            <div class="col-md-4">
                Zip Code:
                <asp:TextBox ID="txtZipCode" runat="server" ClientIDMode="Static" CssClass="form-control" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                Country:
                <asp:DropDownList ID="ddlCountry" runat="server" ClientIDMode="Static" DataSourceID="linqCountry" DataTextField="CountryName" DataValueField="ZCountryID" AutoPostBack="true" CssClass="form-control" />
                <asp:LinqDataSource ID="linqCountry" runat="server" />
            </div>
            <div class="col-md-8">
                &nbsp;
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                &nbsp;
            </div>
        </div>
        <div class="row">
            <div class="col-md-12" style="text-align: left;">
                <asp:Button ID="btnSubmit" runat="server" Text="Next >>" CssClass="btn btn-default" />&nbsp;&nbsp;&nbsp;<asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-default" />
            </div>
        </div>
    </div>
</asp:Content>

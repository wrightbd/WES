<%@ Page Title="Create Your Account" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master" CodeBehind="CreateAccount.aspx.vb" Inherits="WES.CreateAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="description" content="Create your World Exercise System account and get started toward your Certified Personal Trainer Certificate." />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-sm-12">
                <div class="page-header">
                    <h1>Create Account</h1>
                </div>
            </div>
        </div>
        <div class="form-horizontal well well-lg">
            <div class="form-group">
                <div class="col-md-12">
                    <h2>Login Information</h2>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-6">
                    <label for="txtUsername" class="control-label">Username (Email Address): *</label>
                    <asp:TextBox ID="txtUsername" runat="server" ClientIDMode="Static" CssClass="form-control" placeholder="Email Address" />
                </div>
                <div class="col-md-6">
                    <label for="txtConfirmEmail">Confirm Email Address: *</label>
                    <asp:TextBox ID="txtConfirmEmail" runat="server" ClientIDMode="Static" CssClass="form-control" placeholder="Confirm Email Address" />
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-6">
                    <label for="txtPassword">Password: *</label>
                    <asp:TextBox ID="txtPassword" runat="server" ClientIDMode="Static" TextMode="Password" CssClass="form-control" placeholder="Password" />
                </div>
                <div class="col-md-6">
                    <label for="txtConfirmPass">Confirm Password: *</label>
                    <asp:TextBox ID="txtConfirmPass" runat="server" ClientIDMode="Static" TextMode="Password" CssClass="form-control" placeholder="Confirm Password" />
                </div>
            </div>
            
            <div class="row">
                <div class="col-md-12">
                    <h2>Personal Information</h2>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-4">
                    <label for="txtFirstName" class="control-label">First Name: *</label>
                    <asp:TextBox ID="txtFirstName" runat="server" ClientIDMode="Static" CssClass="form-control" />
                </div>
                <div class="col-md-4">
                    <label for="txtMiddleName" class="control-label">Middle Name:</label>
                    <asp:TextBox ID="txtMiddleName" runat="server" ClientIDMode="Static" CssClass="form-control" />
                </div>
                <div class="col-md-4">
                    <label for="txtLastName" class="control-label">Last Name: *</label>
                    <asp:TextBox ID="txtLastName" runat="server" ClientIDMode="Static" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-6">
                    <label for="txtAddress1" class="control-label">Address:</label>
                    <asp:TextBox ID="txtAddress1" runat="server" ClientIDMode="Static" CssClass="form-control" />
                </div>
                <div class="col-md-6">
                    <label for="txtAddress2" class="control-label">Address 2:</label>
                    <asp:TextBox ID="txtAddress2" runat="server" ClientIDMode="Static" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-4">
                    <label for="txtCity" class="control-label">City:</label>
                    <asp:TextBox ID="txtCity" runat="server" ClientIDMode="Static" CssClass="form-control" />
                </div>
                <div class="col-md-4">
                    <label for="ddlState" class="control-label">State:</label>
                    <asp:DropDownList ID="ddlState" runat="server" ClientIDMode="Static" DataSourceID="linqState" DataTextField="StateName" DataValueField="ZStateID" CssClass="form-control" />
                    <asp:LinqDataSource ID="linqState" runat="server" />
                </div>
                <div class="col-md-4">
                    <label for="txtZipCode" class="control-label">Zip Code:</label>
                    <asp:TextBox ID="txtZipCode" runat="server" ClientIDMode="Static" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-4">
                    <label for="ddlCountry" class="control-label">Country:</label>
                    <asp:DropDownList ID="ddlCountry" runat="server" ClientIDMode="Static" DataSourceID="linqCountry" DataTextField="CountryName" DataValueField="ZCountryID" AutoPostBack="true" CssClass="form-control" />
                    <asp:LinqDataSource ID="linqCountry" runat="server" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    &nbsp;
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-12">
                    <asp:Button ID="btnSubmit" runat="server" Text="Next >>" CssClass="btn btn-primary btn-next" />
                    <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

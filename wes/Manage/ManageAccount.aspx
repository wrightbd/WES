<%@ Page Title="Account Management" Language="vb" AutoEventWireup="false" MasterPageFile="~/Manage/ManageMaster.Master" CodeBehind="ManageAccount.aspx.vb" Inherits="WES.ManageAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="scriptManager" runat="server"></asp:ScriptManager>
    <br />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h2>Manage Account</h2>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <!-- Nav tabs -->
            <ul class="nav nav-tabs" role="tablist">
                <li role="presentation" class="active"><a href="#account" aria-controls="account" role="tab" data-toggle="tab">Account</a></li>
                <li role="presentation"><a href="#orders" aria-controls="orders" role="tab" data-toggle="tab">Orders</a></li>
                <li role="presentation"><a href="#tests" aria-controls="tests" role="tab" data-toggle="tab">Tests</a></li>
                <li role="presentation"><a href="#password" aria-controls="password" role="tab" data-toggle="tab">Password</a></li>
            </ul>

            <!-- Tab panes -->
            <div class="tab-content">
                <div role="tabpanel" class="tab-pane active" id="account">
                    <asp:UpdatePanel ID="updateAccountDetails" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-sm-12">
                                    <h4>Account Details</h4>
                                </div>
                            </div>
                            <div class="row">
                                <label class="control-label col-sm-2 text-right">First Name</label>
                                <div class="col-sm-4 text-left">
                                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                                <label class="control-label col-sm-2 text-right">Middle Name</label>
                                <div class="col-sm-4 text-left">
                                    <asp:TextBox ID="txtMiddleName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <label class="control-label col-sm-2 text-right">Last Name</label>
                                <div class="col-sm-4 text-left">
                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-6">&nbsp;</div>
                            </div>

                            <div class="row">
                                <div class="col-sm-12">&nbsp;</div>
                            </div>

                            <div class="row">
                                <label class="control-label col-sm-2 text-right">Email Address</label>
                                <div class="col-sm-4 text-left">
                                    <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-6">&nbsp;</div>
                            </div>

                            <div class="row">
                                <div class="col-sm-12">&nbsp;</div>
                            </div>

                            <div class="row">
                                <label class="control-label col-sm-2 text-right">Address</label>
                                <div class="col-sm-4 text-left">
                                    <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                                <label class="control-label col-sm-2 text-right"></label>
                                <div class="col-sm-4 text-left">
                                    <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row">
                                <label class="control-label col-sm-2 text-right">City</label>
                                <div class="col-sm-4 text-left">
                                    <asp:TextBox ID="txtCity" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                                <label class="control-label col-sm-2 text-right">State</label>
                                <div class="col-sm-4 text-left">
                                    <asp:DropDownList ID="ddlState" runat="server" ClientIDMode="Static" DataSourceID="linqState" DataTextField="StateName" DataValueField="ZStateID" CssClass="form-control" />
                                    <asp:LinqDataSource ID="linqState" runat="server" />
                                </div>
                            </div>

                            <div class="row">
                                <label class="control-label col-sm-2 text-right">Zip Code</label>
                                <div class="col-sm-4 text-left">
                                    <asp:TextBox ID="txtZipCode" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                                <label class="control-label col-sm-2 text-right">Country</label>
                                <div class="col-sm-4 text-left">
                                    <asp:DropDownList ID="ddlCountry" runat="server" ClientIDMode="Static" DataSourceID="linqCountry" DataTextField="CountryName" DataValueField="ZCountryID" AutoPostBack="true" CssClass="form-control" />
                                    <asp:LinqDataSource ID="linqCountry" runat="server" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-12">&nbsp;</div>
                            </div>

                            <div class="row">
                                <div class="col-sm-12 text-right">
                                    <asp:Button ID="btnUpdateInfo" runat="server" Text="Save Changes" CssClass="btn btn-default" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                </div>
                <div role="tabpanel" class="tab-pane" id="orders">
                    <div class="row">
                        <div class="col-sm-12">
                            <h4>Orders</h4>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            Order details can be found under your PayPal account.
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:GridView ID="gvOrders" runat="server" DataSourceID="linqOrders" AutoGenerateColumns="False" DataKeyNames="WESOrderID" CssClass="table table-condensed">
                                <Columns>
                                    <asp:BoundField DataField="TraineeName" HeaderText="Trainee Name">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TestName" HeaderText="Test">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OrderDate" HeaderText="Order Date" DataFormatString="{0:g}">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PayPalTransID" HeaderText="Transaction ID">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                            <asp:LinqDataSource ID="linqOrders" runat="server" />
                        </div>
                    </div>
                </div>
                <div role="tabpanel" class="tab-pane" id="tests">
                    <asp:UpdatePanel ID="updateTests" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-sm-12">
                                    <h4>Tests</h4>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:GridView ID="gvAttempts" runat="server" DataSourceID="linqAttempts" AutoGenerateColumns="False" DataKeyNames="TestAttemptID" CssClass="table table-condensed">
                                        <Columns>
                                            <asp:ButtonField ButtonType="Link" CommandName="TestAnswers" Text="Answers">
                                                <HeaderStyle Width="75px" />
                                                <ItemStyle Width="75px" HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="TestName" HeaderText="Test">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AttemptDate" HeaderText="Date" DataFormatString="{0:g}">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="GradeString" HeaderText="Grade">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Grade" HeaderText="Score" DataFormatString="{0:N2}">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:LinqDataSource ID="linqAttempts" runat="server" /> 
                                </div>
                            </div>


                            <asp:Panel ID="pnlAnswers" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-sm-12">
                                        &nbsp;
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <b>Answers:</b><br />
                                        <asp:GridView ID="gvAnswers" runat="server" DataSourceID="linqAnswers" AutoGenerateColumns="False" CssClass="table table-bordered">
                                            <Columns>
                                                <asp:BoundField DataField="Question" HeaderText="Question">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TraineeAnswer" HeaderText="Trainee Answer">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CorrectAnswer" HeaderText="Correct Answer">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:CheckBoxField DataField="IsWrong" HeaderText="Wrong">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:CheckBoxField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:LinqDataSource ID="linqAnswers" runat="server" />
                                    </div>
                                </div>                            
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                </div>
                <div role="tabpanel" class="tab-pane" id="password">
                    <asp:UpdatePanel ID="updatePasswords" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-sm-12">
                                    <h4>Change Password</h4>
                                </div>
                            </div>
                            <div class="form form-horizontal">
                                <div class="form-group">
                                    <div class="col-sm-2">
                                        <label class="control-label">New Password</label>
                                    </div>
                                    <div class="col-sm-10">
                                        <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-2">
                                        <label class="control-label">Confirm Password</label>
                                    </div>
                                    <div class="col-sm-10">
                                        <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-12 text-right">
                                        <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

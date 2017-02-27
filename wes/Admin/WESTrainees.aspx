<%@ Page Title="Trainees" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/AdminMaster.Master" CodeBehind="WESTrainees.aspx.vb" Inherits="WES.WESTrainees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function OpenDialog() {
            $("#divTraineeAddEdit").dialog("open");
            return false;
        }

        function CloseDialog() {
            $("#divTraineeAddEdit").dialog("close");
            return false;
        }

        function OpenTests() {
            $("#divTraineeTests").dialog("open");
            return false;
        }

        function CloseTests() {
            $("#divTraineeTests").dialog("close");
            return false;
        }

        function CloseDialogError(reqMessage) {
            alert('Error Saving Trainee: ' + reqMessage);
            return false;
        }

        function CloseDialogSuccess() {
            alert('Trainee Saved Successfully');

            $("#btnRefreshGrid").click();
            $("#divTraineeAddEdit").dialog("close");
            return false;
        }

        function CloseRefreshGrid() {
            $("#btnRefreshGrid").click();
            $("#divTraineeAddEdit").dialog("close");
            return false;
        }

        function RefreshGrid() {
            $("#btnRefreshGrid").click();
            return false;
        }

        $(function () {
            $("#divTraineeAddEdit").dialog({
                autoOpen: false,
                height: 650,
                width: 580,
                modal: true,
                closeOnEscape: true,
                draggable: false,
                resizable: false,
                title: 'Add/Edit Trainee',
                open: function (type, data) {
                    $(this).parent().appendTo("form");
                }
            });

            $("#divTraineeTests").dialog({
                autoOpen: false,
                height: 450,
                width: 680,
                modal: true,
                closeOnEscape: true,
                draggable: false,
                resizable: false,
                title: 'Trainee Tests',
                open: function (type, data) {
                    $(this).parent().appendTo("form");
                }
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <asp:UpdatePanel ID="pnlTests" runat="server">
                <ContentTemplate>
                    <center>
                        <table>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Button ID="btnRefreshGrid" runat="server" ClientIDMode="Static" Width="20px" ToolTip="Refresh Trainees" Text="Refresh" Style="visibility: hidden;"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gvTrainees" runat="server" DataSourceID="linqTrainees" AutoGenerateColumns="False" DataKeyNames="TraineeID" CssClass="table table-bordered">
                                        <Columns>
                                            <asp:ButtonField ButtonType="Link" CommandName="EditTrainee" Text="Edit">
                                                <HeaderStyle Width="75px" />
                                                <ItemStyle Width="75px" HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                            <asp:ButtonField ButtonType="Link" CommandName="TraineeTests" Text="Tests">
                                                <HeaderStyle Width="75px" />
                                                <ItemStyle Width="75px" HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                            <asp:ButtonField ButtonType="Link" CommandName="RemoveTrainee" Text="Delete">
                                                <HeaderStyle Width="75px" />
                                                <ItemStyle Width="75px" HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="TraineeName" HeaderText="Trainee Name">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="EmailAddress" HeaderText="Email">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:LinqDataSource ID="linqTrainees" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </center>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>



    <div id="divTraineeAddEdit" class="form-group">
        <asp:UpdatePanel ID="pnlAddEdit" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        Personal Information
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        Email Address: *
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtEmailAddress" runat="server" ClientIDMode="Static" Width="350px" CssClass="form-control" />
                        <asp:HiddenField ID="hiddenTraineeID" runat="server" />
                    </div>
                </div>
                <asp:Panel ID="pnlEmailVerification" runat="server">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:LinkButton ID="btnEmailVerify" runat="server" Text="Resend Email Verification"></asp:LinkButton>
                        </div>
                    </div>
                </asp:Panel>
                <div class="row">
                    <div class="col-md-12">
                        &nbsp;
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        First Name: *
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtFirstName" runat="server" ClientIDMode="Static" Width="350px" CssClass="form-control" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        Middle Name:
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtMiddleName" runat="server" ClientIDMode="Static" Width="350px" CssClass="form-control" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        Last Name: *
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtLastName" runat="server" ClientIDMode="Static" Width="350px" CssClass="form-control" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        Suffix:
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtSuffix" runat="server" ClientIDMode="Static" Width="350px" CssClass="form-control" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        &nbsp;
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        Country:
                    </div>
                    <div class="col-md-8">
                        <asp:DropDownList ID="ddlCountry" runat="server" ClientIDMode="Static" DataSourceID="linqCountry" DataTextField="CountryName" DataValueField="ZCountryID" AutoPostBack="true" Width="350px" CssClass="form-control" />
                        <asp:LinqDataSource ID="linqCountry" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        &nbsp;
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        Address:
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtAddress1" runat="server" ClientIDMode="Static" Width="350px" CssClass="form-control" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        &nbsp;
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtAddress2" runat="server" ClientIDMode="Static" Width="350px" CssClass="form-control" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        City:
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtCity" runat="server" ClientIDMode="Static" Width="350px" CssClass="form-control" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        State:
                    </div>
                    <div class="col-md-8">
                        <asp:DropDownList ID="ddlState" runat="server" ClientIDMode="Static" DataSourceID="linqState" DataTextField="StateName" DataValueField="ZStateID" Width="350px" CssClass="form-control" />
                        <asp:LinqDataSource ID="linqState" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        Zip Code:
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtZipCode" runat="server" ClientIDMode="Static" Width="350px" CssClass="form-control" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        &nbsp;
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        Admin Access:
                    </div>
                    <div class="col-md-8">
                        <asp:CheckBox ID="chkAdminUser" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" style="text-align: right;">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClientClick="javascript:CloseDialog();" CssClass="btn btn-default" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div id="divTraineeTests" class="form-group">
        <asp:UpdatePanel ID="updateTraineeTests" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <b>Test Attempts</b>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
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
                <div class="row">
                    <div class="col-md-12">
                        <asp:Panel ID="pnlAnswers" runat="server" Visible="false">
                            <b>Answers:</b><br />
                            <asp:GridView ID="gvAnswers" runat="server" DataSourceID="linqAnswers" AutoGenerateColumns="False" CssClass="table table-bordered">
                                <Columns>
                                    <asp:BoundField DataField="Question" HeaderText="Question">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TraineeAnswer" HeaderText="Trainee Answer">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CorrectAnswer" HeaderText="Corrent Answer">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                            <asp:LinqDataSource ID="linqAnswers" runat="server" />
                        </asp:Panel>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" style="text-align: right;">
                        <asp:Button ID="btnTestsClose" runat="server" Text="Close" OnClientClick="javascript:CloseTests();" CssClass="btn btn-default" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

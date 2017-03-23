<%@ Page Title="Tests" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/AdminMaster.Master" CodeBehind="WESTests.aspx.vb" Inherits="WES.WESTests" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function OpenDialog() {
            $("#divTestAddEdit").dialog("open");
            return false;
        }

        function CloseDialog() {
            $("#divTestAddEdit").dialog("close");
            return false;
        }

        function CloseDialogError(reqMessage) {
            alert('Error Saving Form: ' + reqMessage);
            return false;
        }

        function CloseDialogSuccess() {
            alert('Form Saved Successfully');

            $("#btnRefreshGrid").click();
            $("#divTestAddEdit").dialog("close");
            return false;
        }

        function CloseRefreshGrid() {
            $("#btnRefreshGrid").click();
            $("#divTestAddEdit").dialog("close");
            return false;
        }

        function RefreshGrid() {
            $("#btnRefreshGrid").click();
            return false;
        }

        $(function () {
            $("#divTestAddEdit").dialog({
                autoOpen: false,
                height: 350,
                width: 620,
                modal: true,
                closeOnEscape: true,
                draggable: false,
                resizable: false,
                title: 'Add/Edit Test',
                open: function (type, data) {
                    $(this).parent().appendTo("form");
                }
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <div class="page-header">
                    <h1>Tests</h1>
                </div>
                <asp:UpdatePanel ID="pnlTests" runat="server">
                    <ContentTemplate>
                        <table class="table">
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Button ID="btnRefreshGrid" runat="server" ClientIDMode="Static" Width="20px" ToolTip="Refresh Tests" Text="Refresh" Style="visibility: hidden;"/>
                                    <asp:Button ID="btnAddTest" runat="server" Text="Add Test" CssClass="btn btn-default" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gvTests" runat="server" DataSourceID="linqTests" AutoGenerateColumns="False" DataKeyNames="TestID" CssClass="table table-bordered">
                                        <Columns>
                                            <asp:ButtonField ButtonType="Link" CommandName="EditTest" Text="Edit">
                                                <HeaderStyle Width="75px" />
                                                <ItemStyle Width="75px" HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                            <asp:ButtonField ButtonType="Link" CommandName="RemoveTest" Text="Delete">
                                                <HeaderStyle Width="75px" />
                                                <ItemStyle Width="75px" HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="TestName" HeaderText="Test Name">
                                                <HeaderStyle Width="350px" />
                                                <ItemStyle Width="350px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:LinqDataSource ID="linqTests" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>


    <div id="divTestAddEdit" class="form-group">
        <asp:UpdatePanel ID="pnlAddEdit" runat="server">
            <ContentTemplate>
                <table border="0" style="padding: 0;" class="table table-condensed">
                    <tr>
                        <td style="vertical-align: top; text-align: right;">Test Name:&nbsp;</td>
                        <td style="vertical-align: top; text-align: left;">
                            <asp:TextBox ID="txtTestName" runat="server" Width="425px" CssClass="form-control"></asp:TextBox>
                            <asp:HiddenField ID="hiddenTestID" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; text-align: right;">Description:&nbsp;</td>
                        <td style="vertical-align: top; text-align: left;">
                            <asp:TextBox ID="txtDescription" runat="server" Width="425px" TextMode="MultiLine" Rows="3" CssClass="form-control"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; text-align: right;">Cost:&nbsp;$</td>
                        <td style="vertical-align: top; text-align: left;">
                            <asp:TextBox ID="txtMemberCost" runat="server" Width="200px" CssClass="form-control"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="Filter_txtMemberCost" runat="server" TargetControlID="txtMemberCost" FilterType="Numbers, Custom" ValidChars="." />
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; text-align: right;">Passing Grade:&nbsp;</td>
                        <td style="vertical-align: top; text-align: left;">
                            <asp:TextBox ID="txtPassingGrade" runat="server" Width="200px" CssClass="form-control"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="txtPassingGrade_FilteredTextBoxExtender" runat="server" TargetControlID="txtPassingGrade" FilterType="Numbers" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: right;">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClientClick="javascript:CloseDialog();" CssClass="btn btn-default" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

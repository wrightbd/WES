<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/AdminMaster.Master" CodeBehind="WESTestQuestions.aspx.vb" Inherits="WES.WESTestQuestions" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function OpenDialog() {
            $("#divAddEdit").dialog("open");
            return false;
        }

        function CloseDialog() {
            $("#divAddEdit").dialog("close");
            return false;
        }

        function CloseDialogError(reqMessage) {
            alert('Error Saving Question: ' + reqMessage);
            return false;
        }

        function CloseDialogSuccess() {
            alert('Question Saved Successfully');

            $("#btnRefreshGrid").click();
            $("#divAddEdit").dialog("close");
            return false;
        }

        function CloseAnswerError(reqMessage) {
            alert('Error Saving Answer: ' + reqMessage);
            return false;
        }

        function CloseRefreshGrid() {
            $("#btnRefreshGrid").click();
            $("#divAddEdit").dialog("close");
            return false;
        }

        function RefreshGrid() {
            $("#btnRefreshGrid").click();
            return false;
        }

        $(function () {
            $("#divAddEdit").dialog({
                autoOpen: false,
                height: 680,
                width: 620,
                modal: true,
                closeOnEscape: true,
                draggable: false,
                resizable: false,
                title: 'Add/Edit Question',
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
                    <h1>Test Questions</h1>
                </div>
                <asp:UpdatePanel ID="pnlQuestions" runat="server">
                    <ContentTemplate>
                        <div style="text-align: center;">
                            Test:
                            <asp:DropDownList ID="ddlTest" runat="server" DataSourceID="linqTest" DataTextField="TestName" DataValueField="TestID"></asp:DropDownList>
                            <asp:LinqDataSource ID="linqTest" runat="server"></asp:LinqDataSource>
                        </div>

                        <table class="table">
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Button ID="btnRefreshGrid" runat="server" ClientIDMode="Static" Width="20px" ToolTip="Refresh Tests" Text="Refresh" Style="visibility: hidden;" />
                                    <asp:Button ID="btnAddQuestion" runat="server" Text="Add Question" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gvQuestions" runat="server" DataSourceID="linqQuestions" AutoGenerateColumns="False" DataKeyNames="TestQuestionID" CssClass="table table-bordered">
                                        <Columns>
                                            <asp:ButtonField ButtonType="Link" CommandName="EditQuestion" Text="Edit">
                                                <HeaderStyle Width="75px" />
                                                <ItemStyle Width="75px" HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                            <asp:ButtonField ButtonType="Link" CommandName="RemoveQuestion" Text="Delete">
                                                <HeaderStyle Width="75px" />
                                                <ItemStyle Width="75px" HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="Question" HeaderText="Question">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:LinqDataSource ID="linqQuestions" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>


    <div id="divAddEdit" class="form-group">
        <asp:UpdatePanel ID="pnlAddEdit" runat="server">
            <ContentTemplate>
                <table class="table table-condensed">
                    <tr>
                        <td style="vertical-align: top; text-align: right;">Question:&nbsp;</td>
                        <td style="vertical-align: top; text-align: left;">
                            <asp:TextBox ID="txtQuestion" runat="server" Width="450px" TextMode="MultiLine" Rows="3" CssClass="form-control"></asp:TextBox>
                            <asp:HiddenField ID="hiddenTestQuestionID" runat="server" />
                            <asp:HiddenField ID="hiddenTestID" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; text-align: right;">Explanation:&nbsp;</td>
                        <td style="vertical-align: top; text-align: left;">
                            <asp:TextBox ID="txtExplanation" runat="server" Width="450px" TextMode="MultiLine" Rows="3" CssClass="form-control"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: right;">
                            <asp:Button ID="btnRefreshAnswer" runat="server" ClientIDMode="Static" Width="20px" ToolTip="Refresh Tests" Text="Refresh" Style="visibility: hidden;" />
                            <asp:Button ID="btnAddAnswer" runat="server" Text="Add Answer" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gvAnswers" runat="server" DataSourceID="linqAnswers" AutoGenerateColumns="False" DataKeyNames="TestQuestionOptionID" Width="550px" CssClass="table-condensed">
                                <Columns>
                                    <asp:ButtonField ButtonType="Link" CommandName="EditAnswer" Text="Edit">
                                        <HeaderStyle Width="75px" />
                                        <ItemStyle Width="75px" HorizontalAlign="Center" />
                                    </asp:ButtonField>
                                    <asp:ButtonField ButtonType="Link" CommandName="RemoveAnswer" Text="Delete">
                                        <HeaderStyle Width="75px" />
                                        <ItemStyle Width="75px" HorizontalAlign="Center" />
                                    </asp:ButtonField>
                                    <asp:BoundField DataField="OptionText" HeaderText="OptionText">
                                        <HeaderStyle Width="350px" />
                                        <ItemStyle Width="350px" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Correct">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCorrect" runat="server" Text='<%# If(Eval("CorrectAnswer") = 1, "Correct", "") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:LinqDataSource ID="linqAnswers" runat="server" ContextTypeName=""></asp:LinqDataSource>
                        </td>
                    </tr>
                    <asp:Panel ID="pnlAnswer" runat="server" Visible="false">
                        <tr>
                            <td colspan="2" style="vertical-align: top; text-align: center;"><b>Answer</b></td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; text-align: right;">Answer:&nbsp;</td>
                            <td style="vertical-align: top; text-align: left;">
                                <asp:TextBox ID="txtAnswer" runat="server" Width="450px" TextMode="MultiLine" Rows="2" CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hiddenTestQuestionOptionID" runat="server" />
                                <asp:HiddenField ID="hiddenSortOrder" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; text-align: right;">Correct:&nbsp;</td>
                            <td style="vertical-align: top; text-align: left;">
                                <asp:CheckBox ID="chkCorrect" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: right;">
                                <asp:Button ID="btnSaveAnswer" runat="server" Text="Save Answer" CssClass="btn btn-default" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancelAnswer" runat="server" Text="Cancel Answer" CssClass="btn btn-default" />
                            </td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                    <asp:Panel ID="pnlSave" runat="server">
                        <tr>
                            <td colspan="2" style="text-align: right;">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClientClick="javascript:CloseDialog();" CssClass="btn btn-default" />
                            </td>
                        </tr>
                    </asp:Panel>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>


</asp:Content>

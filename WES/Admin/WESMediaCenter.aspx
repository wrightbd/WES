<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/AdminMaster.Master" CodeBehind="WESMediaCenter.aspx.vb" Inherits="WES.WESMediaCenter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function OpenDialog() {
            $("#divMediaAddEdit").dialog("open");
            return false;
        }

        function CloseDialog() {
            $("#divMediaAddEdit").dialog("close");
            return false;
        }

        function CloseDialogError(reqMessage) {
            alert('Error Saving Media: ' + reqMessage);
            return false;
        }

        function CloseDialogSuccess() {
            alert('Media Saved Successfully');

            $("#btnRefreshGrid").click();
            $("#divMediaAddEdit").dialog("close");
            return false;
        }

        function CloseRefreshGrid() {
            $("#btnRefreshGrid").click();
            $("#divMediaAddEdit").dialog("close");
            return false;
        }

        function RefreshGrid() {
            $("#btnRefreshGrid").click();
            return false;
        }

        $(function () {
            $("#divMediaAddEdit").dialog({
                autoOpen: false,
                height: 650,
                width: 580,
                modal: true,
                closeOnEscape: true,
                draggable: false,
                resizable: false,
                title: 'Add/Edit Media',
                open: function (type, data) {
                    $(this).parent().appendTo("form");
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-sm-12">
                <asp:UpdatePanel ID="pnlMedia" runat="server">
                    <ContentTemplate>
                        <center>
                        <table>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Button ID="btnRefreshGrid" runat="server" ClientIDMode="Static" Width="20px" ToolTip="Refresh Media Items" Text="Refresh" Style="visibility: hidden;"/>
                                    <asp:Button ID="btnAddMediaItem" runat="server" Text="Add Media Item" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gvMedia" runat="server" DataSourceID="linqMedia" AutoGenerateColumns="False" DataKeyNames="MediaItemID" CssClass="table table-bordered">
                                        <Columns>
                                            <asp:ButtonField ButtonType="Link" CommandName="EditMedia" Text="Edit">
                                                <HeaderStyle Width="75px" />
                                                <ItemStyle Width="75px" HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                            <asp:ButtonField ButtonType="Link" CommandName="RemoveMedia" Text="Tests">
                                                <HeaderStyle Width="75px" />
                                                <ItemStyle Width="75px" HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="MediaName" HeaderText="Media Name">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:LinqDataSource ID="linqMedia" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </center>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div id="divMediaAddEdit" class="form-group">
        <asp:UpdatePanel ID="pnlAddEdit" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        Media Information
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        Category:
                    </div>
                    <div class="col-md-8">
                        <asp:DropDownList ID="ddlCategory" runat="server" ClientIDMode="Static" DataSourceID="linqCategory" DataTextField="CategoryName" DataValueField="MediaCategoryID" Width="350px" CssClass="form-control" />
                        <asp:LinqDataSource ID="linqCategory" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        Media Name: *
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtMediaName" runat="server" ClientIDMode="Static" Width="350px" CssClass="form-control" />
                        <asp:HiddenField ID="hiddenMediaItemID" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        Description: 
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtMediaDescription" runat="server" ClientIDMode="Static" Width="350px" CssClass="form-control" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        YouTube Video ID:
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtWebsiteURL" runat="server" ClientIDMode="Static" Width="350px" CssClass="form-control" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        &nbsp;
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
</asp:Content>

<%@ Page Title="Miscellaneous Items" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/AdminMaster.Master" CodeBehind="WESItems.aspx.vb" Inherits="WES.WESItems" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function CloseError(reqMessage) {
            alert('Error Saving Item: ' + reqMessage);
            return false;
        }

        function OpenCountry() {
            $("#divCountryAddEdit").dialog("open");
            return false;
        }

        function CloseCountry() {
            $("#divCountryAddEdit").dialog("close");
            return false;
        }

        function CloseCountrySuccess() {
            alert('Country Saved Successfully');

            $("#divCountryAddEdit").dialog("close");

            RefreshGrid();
            return false;
        }

        function OpenState() {
            $("#divStateAddEdit").dialog("open");
            return false;
        }

        function CloseState() {
            $("#divStateAddEdit").dialog("close");
            return false;
        }

        function CloseStateSuccess() {
            alert('State Saved Successfully');

            $("#divStateAddEdit").dialog("close");

            RefreshGrid();
            return false;
        }

        function RefreshGrid() {
            $("#btnRefreshGrid").click();
            return false;
        }

        $(function () {
            $("#divCountryAddEdit").dialog({
                autoOpen: false,
                height: 200,
                width: 580,
                modal: true,
                closeOnEscape: true,
                draggable: false,
                resizable: false,
                title: 'Add/Edit Country',
                open: function (type, data) {
                    $(this).parent().appendTo("form");
                }
            });

            $("#divStateAddEdit").dialog({
                autoOpen: false,
                height: 260,
                width: 580,
                modal: true,
                closeOnEscape: true,
                draggable: false,
                resizable: false,
                title: 'Add/Edit State',
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
            <asp:UpdatePanel ID="pnlCountry" runat="server">
                <ContentTemplate>
                    <center>
                        <table>
                            <tr>
                                <td style="text-align: center;">
                                    Item Type:&nbsp;
                                    <asp:DropDownList ID="ddlItemType" runat="server" AutoPostBack="true">
                                        <asp:ListItem Text="Country" Value="Country" />
                                        <asp:ListItem Text="State" Value="State" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Button ID="btnRefreshGrid" runat="server" ClientIDMode="Static" Width="20px" ToolTip="Refresh Items" Text="Refresh" Style="visibility: hidden;"/>
                                    <asp:Button ID="btnAddItem" runat="server" Text="Add Item" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gvItem" runat="server" DataSourceID="linqItem" AutoGenerateColumns="False" DataKeyNames="ItemID" Width="500px">
                                        <Columns>
                                            <asp:ButtonField ButtonType="Link" CommandName="EditItem" Text="Edit">
                                                <HeaderStyle Width="75px" />
                                                <ItemStyle Width="75px" HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                            <asp:ButtonField ButtonType="Link" CommandName="RemoveItem" Text="Delete">
                                                <HeaderStyle Width="75px" />
                                                <ItemStyle Width="75px" HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="ItemName" HeaderText="Item Name">
                                                <HeaderStyle Width="200px" />
                                                <ItemStyle Width="200px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ParentName" HeaderText="Parent">
                                                <HeaderStyle Width="100px" />
                                                <ItemStyle Width="100px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:CheckBoxField DataField="Active" HeaderText="Active">
                                                <HeaderStyle Width="50px" />
                                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                            </asp:CheckBoxField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:LinqDataSource ID="linqItem" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </center>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>



    <div id="divCountryAddEdit" class="form-group">
        <asp:UpdatePanel ID="pnlCountryAddEdit" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-4">
                        Country Name:
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtCountryName" runat="server" ClientIDMode="Static" CssClass="form-control" Width="350px"/>
                        <asp:HiddenField ID="hiddenCountryID" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        Active:
                    </div>
                    <div class="col-md-8">
                        <asp:CheckBox ID="chkCountryActive" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        &nbsp;
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" style="text-align: right;">
                        <asp:Button ID="btnCountrySave" runat="server" Text="Save" CssClass="btn btn-default" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCountryCancel" runat="server" Text="Cancel" OnClientClick="javascript:CloseCountry();" CssClass="btn btn-default" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div id="divStateAddEdit" class="form-group">
        <asp:UpdatePanel ID="pnlStateAddEdit" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-4">
                        State Name:
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtStateName" runat="server" ClientIDMode="Static" CssClass="form-control" />
                        <asp:HiddenField ID="hiddenStateID" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        Abbreviation:
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtAbbreviation" runat="server" ClientIDMode="Static" CssClass="form-control" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        Country:
                    </div>
                    <div class="col-md-8">
                        <asp:DropDownList ID="ddlCountry" runat="server" ClientIDMode="Static" DataSourceID="linqCountry" DataTextField="CountryName" DataValueField="ZCountryID" CssClass="form-control" />
                        <asp:LinqDataSource ID="linqCountry" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        Active:
                    </div>
                    <div class="col-md-8">
                        <asp:CheckBox ID="chkStateActive" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        &nbsp;
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" style="text-align: right;">
                        <asp:Button ID="btnStateSave" runat="server" Text="Save" CssClass="btn btn-default" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnStateCancel" runat="server" Text="Cancel" OnClientClick="javascript:CloseState();" CssClass="btn btn-default" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
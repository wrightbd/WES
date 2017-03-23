<%@ Page Title="Promo Codes" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/AdminMaster.Master" CodeBehind="WESPromoCode.aspx.vb" Inherits="WES.WESPromoCode" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function OpenDialog() {
            $("#divPromoCodeAddEdit").dialog("open");
            return false;
        }

        function CloseDialog() {
            $("#divPromoCodeAddEdit").dialog("close");
            return false;
        }

        function CloseDialogError(reqMessage) {
            alert('Error Saving Promo Code: ' + reqMessage);
            return false;
        }

        function CloseDialogSuccess() {
            alert('Promo Code Saved Successfully');

            $("#btnRefreshGrid").click();
            $("#divPromoCodeAddEdit").dialog("close");
            return false;
        }

        function CloseRefreshGrid() {
            $("#btnRefreshGrid").click();
            $("#divPromoCodeAddEdit").dialog("close");
            return false;
        }

        function RefreshGrid() {
            $("#btnRefreshGrid").click();
            return false;
        }

        $(function () {
            $("#divPromoCodeAddEdit").dialog({
                autoOpen: false,
                height: 350,
                width: 620,
                modal: true,
                closeOnEscape: true,
                draggable: false,
                resizable: false,
                title: 'Add/Edit Promo Code',
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
            <div class="col-md-12">
                <asp:UpdatePanel ID="pnlPromoCodes" runat="server">
                    <ContentTemplate>
                        <center>
                        <table>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Button ID="btnRefreshGrid" runat="server" ClientIDMode="Static" Width="20px" ToolTip="Refresh Tests" Text="Refresh" Style="visibility: hidden;"/>
                                    <asp:Button ID="btnAddPromoCode" runat="server" Text="Add Promo Code" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gvPromoCodes" runat="server" DataSourceID="linqPromoCodes" AutoGenerateColumns="False" DataKeyNames="PromoCodeID" Width="500px"
                                        CssClass="table table-bordered">
                                        <Columns>
                                            <asp:ButtonField ButtonType="Link" CommandName="EditPromoCode" Text="Edit">
                                                <HeaderStyle Width="75px" />
                                                <ItemStyle Width="75px" HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                            <asp:ButtonField ButtonType="Link" CommandName="RemovePromoCode" Text="Delete">
                                                <HeaderStyle Width="75px" />
                                                <ItemStyle Width="75px" HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="PromoCodeName" HeaderText="Promo Code Name">
                                                <HeaderStyle Width="350px" />
                                                <ItemStyle Width="350px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:LinqDataSource ID="linqPromoCodes" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </center>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>



    <div id="divPromoCodeAddEdit" class="form-group">
        <asp:UpdatePanel ID="pnlAddEdit" runat="server">
            <ContentTemplate>
                <table border="0" style="padding: 0;" class="table-condensed">
                    <tr>
                        <td style="vertical-align: top; text-align: right;">Promo Code Name:&nbsp;</td>
                        <td style="vertical-align: top; text-align: left;">
                            <asp:TextBox ID="txtPromoCodeName" runat="server" Width="425px" CssClass="form-control"></asp:TextBox>
                            <asp:HiddenField ID="hiddenPromoCodeID" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; text-align: right;">Promo Code:&nbsp;</td>
                        <td style="vertical-align: top; text-align: left;">
                            <asp:TextBox ID="txtPromoCode" runat="server" Width="425px" CssClass="form-control"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; text-align: right;">Discount:&nbsp;</td>
                        <td style="vertical-align: top; text-align: left;">
                            <asp:TextBox ID="txtDiscount" runat="server" Width="425px" CssClass="form-control"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="Filter_txtDiscount" runat="server" TargetControlID="txtDiscount" FilterType="Numbers, Custom" ValidChars="." />
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; text-align: right;">End Date:&nbsp;</td>
                        <td style="vertical-align: top; text-align: left;">
                            <asp:TextBox ID="txtDateEnd" runat="server" Width="425px" CssClass="form-control"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender_txtDateEnd" TargetControlID="txtDateEnd" runat="server" />
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

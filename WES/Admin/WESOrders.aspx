<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/AdminMaster.Master" CodeBehind="WESOrders.aspx.vb" Inherits="WES.WESOrders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" />

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <asp:UpdatePanel ID="pnlTests" runat="server">
                    <ContentTemplate>
                        <center>
                        <table>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Button ID="btnRefreshGrid" runat="server" ClientIDMode="Static" Width="20px" ToolTip="Refresh Orders" Text="Refresh" Style="visibility: hidden;"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gvOrders" runat="server" DataSourceID="linqOrders" AutoGenerateColumns="False" DataKeyNames="WESOrderID" CssClass="table table-bordered">
                                        <Columns>
                                            <asp:BoundField DataField="WESOrderID" HeaderText="Order Num">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
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
                                </td>
                            </tr>
                        </table>
                    </center>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>

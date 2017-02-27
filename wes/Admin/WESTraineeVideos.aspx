<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/AdminMaster.Master" CodeBehind="WESTraineeVideos.aspx.vb" Inherits="WES.WESTraineeVideos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <asp:UpdatePanel ID="updateVideos" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>Status:</td>
                            <td>
                                <asp:DropDownList ID="ddlStatus" runat="server">
                                    <asp:ListItem Text="Needs Review" Value="0" Selected="true"></asp:ListItem>
                                    <asp:ListItem Text="Approved" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Denied" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Start Date:</td>
                            <td><asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>End Date:</td>
                            <td><asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: left">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="gvVideos" runat="server" DataSourceID="linqVideos" AutoGenerateColumns="False" Width="500px" EmptyDataText="No Videos" AllowSorting="true">
                                    <Columns>
                                        <asp:BoundField DataField="TraineeName" HeaderText="Trainee Name" SortExpression="TraineeName">
                                            <HeaderStyle Width="200px" />
                                            <ItemStyle Width="200px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DateCreated" HeaderText="Submission Date" SortExpression="DateCreated" DataFormatString="{0:g}">
                                            <HeaderStyle Width="150px" />
                                            <ItemStyle Width="150px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="VideoStatusName" HeaderText="Status">
                                            <HeaderStyle Width="100px" />
                                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:HyperLinkField DataNavigateUrlFields="YouTubeLink" Text="View Video" Target="_blank">
                                            <HeaderStyle Width="100px" />
                                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                                        </asp:HyperLinkField>
                                    </Columns>
                                </asp:GridView>
                                <asp:LinqDataSource ID="linqVideos" runat="server" />
                            </td>
                        </tr>
                    </table>
                    
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

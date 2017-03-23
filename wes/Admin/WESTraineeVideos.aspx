<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/AdminMaster.Master" CodeBehind="WESTraineeVideos.aspx.vb" Inherits="WES.WESTraineeVideos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" />

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-sm-12">
                <div class="page-header">
                    <h1>Trainee Videos</h1>
                </div>
                <asp:UpdatePanel ID="updateVideos" runat="server">
                    <ContentTemplate>
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="ddlStatus" class="col-sm-3 control-label">Status:</label>
                                <div class="col-sm-9">
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Needs Review" Value="0" Selected="true"></asp:ListItem>
                                        <asp:ListItem Text="Approved" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Denied" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="txtStartDate" class="col-sm-3 control-label">Start Date:</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" />
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="txtEndDate" class="col-sm-3 control-label">End Date:</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" />
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-sm-12">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-default" />
                                </div>
                            </div>
                        </div>

                        <asp:GridView ID="gvVideos" runat="server" DataSourceID="linqVideos" AutoGenerateColumns="False" EmptyDataText="No Videos" AllowSorting="true" CssClass="table table-bordered">
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

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>

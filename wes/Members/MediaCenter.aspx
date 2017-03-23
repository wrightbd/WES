<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Members/MembersMaster.Master" CodeBehind="MediaCenter.aspx.vb" Inherits="WES.MediaCenter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-sm-12">
                <div class="page-header">
                    <h1>Media Center</h1>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <asp:Repeater ID="rptHeaders" runat="server" DataSourceID="linqHeaders">
                    <ItemTemplate>
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3><asp:Label runat="server" ID="Label1" Text='<%# Eval("CategoryName") %>' /></h3>
                            </div>
                            <div class="panel-body">
                                <asp:Repeater ID="childRepeater" runat="server">
                                    <ItemTemplate>
                                        <div class="col-sm-2 text-center">
                                            <asp:Image ID="image1" runat="server" ImageUrl='<%# "https://img.youtube.com/vi/" + Eval("WebsiteURL") + "/default.jpg" %>' /><br />
                                            <asp:HyperLink ID="Link1" runat="server" NavigateUrl='<%# "https://youtu.be/" + Eval("WebsiteURL") %>' Target="_blank" Text='<%# Eval("MediaName") %>' />
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:LinqDataSource ID="linqHeaders" runat="server"/>
            </div>
        </div>
    </div>
</asp:Content>

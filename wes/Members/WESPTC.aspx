<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Members/MembersMaster.Master" CodeBehind="WESPTC.aspx.vb" Inherits="WES.WESPTC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" />

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <asp:Panel ID="pnlTest" runat="server">
                    <h2><asp:Label ID="lblTestName" runat="server"/></h2>
                    <hr />
                    <asp:Table ID="tblTest" runat="server"></asp:Table>
                    <asp:HiddenField ID="hiddenTestID" runat="server" />
                    <asp:HiddenField ID="hiddenTraineeID" runat="server" />

                    <table>
                        <tr>
                            <td style="text-align: left;">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                                <br />
                                <br />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlResult" runat="server">
                    <h4><asp:Label ID="lblResult" runat="server"></asp:Label></h4>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>

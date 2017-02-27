<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master" CodeBehind="ChoosePlan.aspx.vb" Inherits="WES.ChoosePlan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Panel ID="pnlAccount" runat="server">
        <div class="row">
            <div class="col-sm-12">
                <div class="panel panel-pagetitle">
                    <div class="panel-heading">
                        <h3>Your account has been created successfully. An email has been sent to verify your email address.</h3>
                        Please select the payment plan you wish to purchase below:
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    
    <div class="well">
        <div class="row">
            <div class="col-sm-6">
                <div class="row">
                    <div class="col-xs-12">
                        <asp:Image ID="imgLogo" runat="server" ImageUrl="~/img/brand_paypal.png" /><br />
                        <h5><b>Personal Fitness Trainer</b></h5>
                        <h5><b>Certificate Program</b></h5>
                    </div>
                </div>
            </div>
            <div class="col-sm-6">
                <h5>
                    <asp:RadioButtonList ID="rblCPT" runat="server" Font-Bold="true" >
                        <asp:ListItem Text="Full Payment of $499.00" Value="1" Selected="True" />
                        <asp:ListItem Text="(Coming Soon) Two Payments of $285.00" Value="2" Enabled="False" />
                        <asp:ListItem Text="(Coming Soon) Five Payments of $135.00" Value="3" Enabled="False" />
                    </asp:RadioButtonList>
                </h5>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-4 col-sm-offset-3">
                <div class="well-sm" style="background-color: white;">
                    <span style="color: #055385;">For any program purchase you receive for <span style="color: red;"><b>FREE</b></span><br /><br />
                    All Access to Exclusive E-Book, Study Guide, Materials and Video Center</span>
                </div>
            </div>
            <div class="col-sm-5">
                &nbsp;
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-sm-3 col-sm-offset-9">
            <div class="panel panel-darkblue">
                <div class="panel-heading">
                    <h4>Promo Code</h4>
                </div>
                <div class="panel-body">
                    <asp:TextBox ID="txtPromoCode" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>


    <div class="row">
        <div class="col-sm-12 text-right">
            <asp:ImageButton ID="btnPurchase" runat="server" ImageUrl="https://www.paypal.com/en_US/i/btn/btn_buynowCC_LG.gif" BorderStyle="None" AlternateText="Buy Now" />
            <img alt="" border="0" src="https://www.paypal.com/en_US/i/scr/pixel.gif" width="1" height="1" />
        </div>
    </div>
</asp:Content>

<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Members/MembersMaster.Master" CodeBehind="UpgradeMembership.aspx.vb" Inherits="WES.Members.UpgradeMembership" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"/>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="banner" class="banner-interior">
        <div class="container">
            <div class="row">
                <div class="col-xs-12 page-title">
                    <h1>Upgrade Membership</h1>
                </div>
            </div>
        </div>
    </div>
    <div class="container">
        <div class="row">
            <div class="col-xs-12">
                <p class="lead">Continue access to our great member site!</p>
                <p>Your initial certification comes with 1 year access to our member site.  To continue access after this time, please choose the option below and 
                    we will upgrade or continue your membership.
                </p>
            </div>
        </div>
        
        <div class="row">
            <div class="col-md-4 col-md-offset-4">
                <div class="well choose-plan">
                    <h3><span class="text-success">1</span> Full Payment</h3>
                    <h3><span class="text-danger">$99</span></h3>
                    <p>
                        1 Year Access to Member Site
                    </p>
                    <iframe src="../paypal-btn-04.html" class="paypal-btn" scrolling="no"></iframe>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

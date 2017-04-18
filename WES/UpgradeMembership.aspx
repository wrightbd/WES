<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master" CodeBehind="UpgradeMembership.aspx.vb" Inherits="WES.UpgradeMembership" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" />

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="banner" class="banner-interior">
        <img src="~/images/banner-interior.jpg" alt="" runat="server" />
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
                <p>
                    Your initial membership with certification comes with 1 year access to our Member portal full of videos, information and more.  If you would like to continue your
                    membership, we offer a continuing membership for $99 per year. Please select the option below to pay for your continued membership.
                </p>
            </div>
        </div>

        <div class="row">
            <div class="col-md-4">
                <div class="well choose-plan">
                    <h3><span class="text-success">1</span> Full Time Payment</h3>
                    <h3><span class="text-danger">$99</span></h3>
                    <p>
                        1 Year Access to Member Site<br />
                    </p>
                    <iframe src="paypal-btn-04.html" class="paypal-btn" scrolling="no"></iframe>
                </div>
            </div>

            <div class="col-md-4">
            </div>

            <div class="col-md-4">
            </div>
        </div>
    </div>
</asp:Content>

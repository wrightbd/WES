<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Members/MembersMaster.Master" CodeBehind="UpgradeMembership.aspx.vb" Inherits="WES.Members.UpgradeMembership" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"/>

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
                <p class="lead">3 easy payment options for our Certified Personal Trainer Certificate Exam.</p>
            </div>
        </div>
        
        <div class="row">
            <div class="col-md-4 col-md-offset-4">
                <div class="well choose-plan">
                    <h3><span class="text-success">1</span> Full Time Payment</h3>
                    <h3><span class="text-danger">$499</span></h3>
                    <p>Savings of $176<br/>
                        Lifetime Certificate<br/>
                        1 Year Access to Member Site<br/>
                        <span class="text-primary">Best Offer</span>
                    </p>
                    <iframe src="../paypal-btn-04.html" class="paypal-btn" scrolling="no"></iframe>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

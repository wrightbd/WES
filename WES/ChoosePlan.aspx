<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master" CodeBehind="ChoosePlan.aspx.vb" Inherits="WES.ChoosePlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" />

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="banner" class="banner-interior">
        <div class="container">
            <div class="row">
                <div class="col-xs-12 page-title">
                    <h1>Choose A Plan</h1>
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
            <div class="col-md-4">
                <div class="well choose-plan">
                    <h3><span class="text-success">1</span> Full Time Payment</h3>
                    <h3><span class="text-danger">$499</span></h3>
                    <p>Savings of $176<br/>
                        Lifetime Certificate<br/>
                        1 Year Access to Member Site<br/>
                        <span class="text-primary">Best Offer</span>
                    </p>
                    <iframe src="paypal-btn-01.html" class="paypal-btn" scrolling="no"></iframe>
                </div>
            </div>
            
            <div class="col-md-4">
                <div class="well choose-plan">
                    <h3><span class="text-warning">2</span> Payments</h3>
                    <h3><span class="text-danger">$285/payment</span></h3>
                    <p>Savings of $105<br/>
                        3 Year Certificate<br/>
                        1 Year Access to Member Site
                    </p>
                    <iframe src="paypal-btn-02.html" class="paypal-btn" scrolling="no"></iframe>
                </div>
            </div>
            
            <div class="col-md-4">
                <div class="well choose-plan">
                    <h3><span class="text-danger">5</span> Payments</h3>
                    <h3><span class="text-danger">$135/payment</span></h3>
                    <p>2 Year Certificate<br/>
                        1 Year Access to Member Site
                    </p>
                    <iframe src="paypal-btn-03.html" class="paypal-btn" scrolling="no"></iframe>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-xs-12">
                <iframe src="paypal-btn-test.html" scrolling="no"></iframe>
            </div>
        </div>
    </div>
</asp:Content>

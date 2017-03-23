<%@ Page Title="Payment Options" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master" CodeBehind="PaymentPlans.aspx.vb" Inherits="WES.PaymentPlans" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="description" content="Availble Payment Options for World Exercise System's Certified Personal Trainer Certificate Exam" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-sm-12">
                <div class="panel panel-pagetitle">
                    <div class="panel-heading text-center">
                        <h2>Wherever you are in your fitness career, Get Certified Today.</h2>
                    </div>
                </div>
            </div>
        </div>

        <div class="row" style="background-image: url('img/brand_background.png'); background-position: center; background-repeat: no-repeat;">
            <div class="col-xs-12">
                <div class="row">
                    <div class="col-xs-8 col-xs-offset-4 text-center">
                        <h3 style="color: #055385;">Easy Payment Options</h3>
                        <hr style="color: #055385" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-8 col-xs-offset-4 text-center">
                        <h4>3 payment options for our Certified Personal Trainer Certificate Exam</h4>
                    </div>
                </div>

                <div class="row">
                    <div class="col-sm-12">
                        <hr style="color: #055385" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-sm-4 text-left">
                        <h3>Retail: $675.00</h3>
                    </div>
                    <div class="col-sm-8">
                        <div class="row">
                            <div class="col-xs-8 text-right">
                                <h4><b>One full time payment</b> (savings of $176.00)</h4>
                                <h4>Receive Lifetime Certificate</h4>
                                <h4 style="color: #055385">Best Offer</h4>
                            </div>
                            <div class="col-xs-4 text-center">
                                <h3>$499.00</h3>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <hr style="color: #055385" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-8 text-right">
                                <h4><b>(Coming Soon) Two payments</b> (savings of $105.00)</h4>
                                <h4>Receive 3 Year Certificate</h4>
                            </div>
                            <div class="col-xs-4 text-center">
                                <h3>$285.00/payment</h3>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <hr style="color: #055385" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-8 text-right">
                                <h4><b>(Coming Soon) Five payments</b></h4>
                                <h4>Receive 2 Year Certificate</h4>
                            </div>
                            <div class="col-xs-4 text-center">
                                <h3>$135.00/payment</h3>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">&nbsp;</div>
                </div>
                <div class="row">
                    <div class="col-sm-12 text-right">
                        <h2>
                            <asp:HyperLink ID="linkGetStarted" runat="server" ForeColor="Red" Font-Bold="true" NavigateUrl="~/CreateAccount.aspx">Click Here to Get Started</asp:HyperLink>
                        </h2>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

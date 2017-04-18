<%@ Page Title="How to Become a CPT" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master" CodeBehind="GetCertified.aspx.vb" Inherits="WES.BecomeCPT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="description" content="Our certification encompasses all areas of fitness, from training athletes to senior citizens along with how to run your own personal training business." />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="banner" class="banner-interior">
        <img src="~/images/banner-interior.jpg" alt="" runat="server" />
        <div class="container">
            <div class="row">
                <div class="col-xs-12 page-title">
                    <h1>Get Certified</h1>
                </div>
            </div>
        </div>
    </div>
    <div class="container">
        <div class="row">
            <div class="col-xs-12">
                <p class="text-center">How would you like to become the top trainer in your area? Then don't waste another day!</p>
                <p class="text-center">
                    At World Exercise System, we have perfected the Personal Training Certification model! Our certification encompasses all areas of fitness, training athletes to senior citizens and helps
                you with how to run your own personal training business.
                </p>
                <h3 class="text-center">PERSONAL TRAINING IS AN EXCITING AND AMAZING BUSINESS!<br />
                    JUST IMAGINE GETTING PAID TO DO WHAT YOU ARE PASSIONATE ABOUT!
                </h3>
                <div class="row">
                    <div class="col-xs-12">
                        <a class="text-center" role="button" href="~/ChoosePlan.aspx" runat="server">
                            <img src="images/get-certified-btn-large.jpg" alt="Start your CPT Certification Now" class="img-responsive" />
                        </a>
                    </div>

                </div>
            </div>

        </div>
    </div>
</asp:Content>

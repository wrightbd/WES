<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master" CodeBehind="ThankYou.aspx.vb" Inherits="WES.ThankYou" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" />

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="banner" class="banner-interior">
        <div class="container">
            <div class="row">
                <div class="col-xs-12 page-title">
                    <h1>Liability Agreement</h1>
                </div>
            </div>
        </div>
    </div>
    <div class="container">
        <div class="row">
            <div class="col-xs-12">
                <p class="lead">
                    Thank You for your payment and for becoming a site member of World Exercise Sytem!
                </p>
                <p>
                    To continue with your certification requirements and to access your member dashboard, please click the continue button below. If you are a new member, after registering you will need
                    to complete and pass your test and upload your required video. Once all three requirements (payment, test, video) are completed, we will verify all information and then issue and email
                    your certificate.
                </p>
                <p><a href="~/CreateAccount.aspx" class="btn btn-danger btn-lg" runat="server">Continue</a></p>

               
            </div>
        </div>
    </div>
</asp:Content>

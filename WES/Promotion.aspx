<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master" CodeBehind="Promotion.aspx.vb" Inherits="WES.Promotion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" />

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="banner" class="banner-interior">
        <div class="container">
            <div class="row">
                <div class="col-xs-12 page-title">
                    <h1>Promotion</h1>
                </div>
            </div>
        </div>
    </div>
    <div class="container">
        <div class="row">
            <div class="col-xs-12">
                <p class="lead">Special Promotion</p>
            </div>
        </div>

        <div class="row">
            <div class="col-md-4 col-md-offset-4">
                <div class="well choose-plan">
                    <h3><span class="text-success">1</span> Time Payment</h3>
                    <h3><span class="text-danger">$199</span></h3>
                    
                    <iframe src="paypal-btn-promotion.html" class="paypal-btn" scrolling="no"></iframe>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

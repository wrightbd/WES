<%@ Page Title="World Exercise System E-Book" Language="vb" AutoEventWireup="false" MasterPageFile="~/Members/MembersMaster.Master" CodeBehind="EBook.aspx.vb" Inherits="WES.EBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="robots" content="nofollow" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="banner" class="banner-interior">
        <img src="~/images/banner-interior.jpg" alt="" runat="server" />
        <div class="container">
            <div class="row">
                <div class="col-xs-12 page-title">
                    <h1>E-Book</h1>
                </div>
            </div>
        </div>
    </div>
    <div class="container">
        <div class="row">
            <div class="col-sm-12">
                <p>If you wish to learn more, then you can read our E-Book, available to members.</p>
                <p>Below is an exerpt from the E-Book:</p>
                <blockquote>
                    There are some similarities between training athletes and training the general public. However, there are also some vast differences. 
                It does not matter how in shape a client is, whether they are a current athlete, former athlete, or have never been an athlete 
                our World Exercise System’s motto applies to everyone. "Each work-out gets you ready for your next work-out."
                </blockquote>
                <a class="btn btn-primary btn-lg" href="Docs/WESEBook.pdf">Download E-Book</a>
            </div>
        </div>
    </div>
</asp:Content>

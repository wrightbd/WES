<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Members/MembersMaster.Master" CodeBehind="MemberDashboard.aspx.vb" Inherits="WES.Members.MemberDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"/>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="banner" class="banner-interior">
        <img src="~/images/banner-interior.jpg" alt="" runat="server" />
        <div class="container">
            <div class="row">
                <div class="col-xs-12 page-title">
                    <h1>Member Dashboard</h1>
                </div>
            </div>
        </div>
    </div>
    <div class="container">
        <div class="row">
            <div class="col-xs-12">
                <p class="lead">Checkout our quick links and member dashboard below. 
                    Other options may also be available in the “Members” menu option above. 
                    Please contact us should you have any issues with the page, process, 
                    or technical issues at <a href="mailto:web@worldexercisesystem.com">web@worldexercisesystem.com</a>.</p>
            </div>
        </div>
    </div>
    
    <div class="dashboard-container">
        <div class="container">
            
            <div class="row">
                <div class="col-xs-12 text-center">
                    <h2>Three Steps to Completing Your Certification</h2>
                </div>
            </div>

            <div class="row">
                <div class="col-sm-4">
                    <a href="~/ChoosePlan.aspx" class="well dashboard-tile dashboard-tile-top" runat="server">
                        <i class="fa fa-cc-paypal fa-5x"></i>
                        <h3>Pay for Your Certification</h3>
                        <p>If you have already paid, please proceed to take your test.</p>
                    </a>
                </div>
                <div class="col-sm-4">
                    <a href="~/Members/WESPTC.aspx" class="well dashboard-tile dashboard-tile-top" runat="server">
                        <i class="fa fa-file-text fa-5x"></i>
                        <h3>Take Your Certification Test</h3>
                        <p>If you have already taken your test your results will be displayed.</p>
                    </a>
                </div>
                <div class="col-sm-4">
                    <a href="~/Members/Videos.aspx" class="well dashboard-tile dashboard-tile-top" runat="server">
                        <i class="fa fa-upload fa-5x"></i>
                        <h3>Upload Your Video for Review</h3>
                        <p>You must upload a video for review. Once uploaded, we will process.</p>
                    </a>
                </div>
            </div>
            
            <div class="row">
                <div class="col-xs-12 text-center">
                    <h3>After completion of these 3 steps, we will email your certificate to you.</h3>
                    <p>For any questions, contact us at <a href="mailto:info@worldexercisesystem.com">info@worldexercisesystem.com</a>.</p>
                </div>
            </div>

        </div>
    </div>
    
    <div class="dashboard-container dashboard-container-mid">
        <div class="container">
            
            <div class="row">
                <div class="col-xs-12 text-center">
                    <h2>Member Benefits & Options</h2>
                </div>
            </div>

            <div class="row">
                <div class="col-sm-4">
                    <a href="~/Members/EBook.aspx" class="well dashboard-tile dashboard-tile-mid" runat="server">
                        <i class="fa fa-book fa-5x"></i>
                        <h3>Download E-Book</h3>
                        <p>Download our informative E-book with great tips & info.</p>
                    </a>
                </div>
                <div class="col-sm-4">
                    <a href="~/Members/MediaCenter.aspx" class="well dashboard-tile dashboard-tile-mid" runat="server">
                        <i class="fa fa-play-circle-o fa-5x"></i>
                        <h3>Media Center</h3>
                        <p>See helpful videos, guides, and other information.</p>
                    </a>
                </div>
                <div class="col-sm-4">
                    <a href="~/Members/StudyGuide.aspx" class="well dashboard-tile dashboard-tile-mid" runat="server">
                        <i class="fa fa-pencil-square fa-5x"></i>
                        <h3>Download Study Guide</h3>
                        <p>Download an informative study guide with tips and info on our test.</p>
                    </a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


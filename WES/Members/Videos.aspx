<%@ Page Title="Video Submission" Language="vb" AutoEventWireup="false" MasterPageFile="~/Members/MembersMaster.Master" CodeBehind="Videos.aspx.vb" Inherits="WES.Videos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" />

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="banner" class="banner-interior">
        <div class="container">
            <div class="row">
                <div class="col-xs-12 page-title">
                    <h1>Upload Videos</h1>
                </div>
            </div>
        </div>
    </div>
    <div class="container">
        <div class="row">
            <div class="col-sm-12">
                <h2>Instructions on submitting video for certificate approval:</h2>
                <ol class="video-list">
                    <li>You MUST receive a passing grade on your test before you can submit a video.</li>
                    <li>Upload your video to YouTube or Vimeo</li>
                    <li>Make sure the video is either Public or Unlisted</li>
                    <li>View the video on YouTube or Vimeo & copy the web address url in the top bar of your browser & put in box below and press submit.</li>
                    <li>We will send you an email once your video has been reviewed.</li>
                </ol>
                <asp:TextBox ID="txtVideoCode" runat="server" Width="500"/>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" />
                <h4><i class="fa fa-arrow-right fa-1.5x" aria-hidden="true"></i> <a href="~/Members/MemberDashboard.aspx" runat="server"> RETURN TO MEMBER DASHBOARD</a></h4>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <h2>How To Upload:</h2>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-4 col-sm-offset-2">
                <h4 id="back-video">Example Video Upload #1</h4>
                <iframe src="https://player.vimeo.com/video/246795361?color=ff0303&byline=0&portrait=0" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>
            </div>
            <div class="col-sm-4">
                <h4 id="triceps-video">Example Video Upload #2</h4>
                <iframe src="https://player.vimeo.com/video/246797517?color=ff0808&byline=0&portrait=0" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>
            </div>
        </div>
    </div>
</asp:Content>

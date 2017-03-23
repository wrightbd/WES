<%@ Page Title="Video Submission" Language="vb" AutoEventWireup="false" MasterPageFile="~/Members/MembersMaster.Master" CodeBehind="Videos.aspx.vb" Inherits="WES.Videos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" />

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-sm-12">
                <div class="page-header">
                    <h1>Videos</h1>
                </div>
                <h4>Instructions on submitting video for certificate approval:</h4>
                <ol>
                    <li>Upload your video to YouTube</li>
                    <li>Make sure the video is either Public or Unlisted</li>
                    <li>View the video on YouTube and look at the web address</li>
                    <li>Copy the characters that appear after: https://www.youtube.com/watch?v= into the textbox below and submit.</li>
                    <li>We will send you an email once your video has been reviewed.</li>
                </ol>
                <asp:TextBox ID="txtVideoCode" runat="server" Width="200"/>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" />
            </div>
        </div>
    </div>
</asp:Content>

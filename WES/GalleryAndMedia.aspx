<%@ Page Title="Contact Us" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master" CodeBehind="GalleryAndMedia.aspx.vb" Inherits="WES.GalleryAndMedia" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="banner" class="banner-interior">
        <div class="container">
            <div class="row">
                <div class="col-xs-12 page-title">
                    <h1>Gallery and Media</h1>
                </div>
            </div>
        </div>
    </div>
    <div class="container">
        <div class="row">
            <div class="col-sm-12">
                <h2>Please choose a video to watch:</h2>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="col-sm-4">
                    <h4 id="no-quit">No Quit</h4>
                    <iframe src="https://player.vimeo.com/video/239751802" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>
                </div>
                <div class="col-sm-4">
                    <h4 id="usta">Usta</h4>
                    <iframe src="https://player.vimeo.com/video/239751529" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

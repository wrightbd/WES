<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Members/MembersMaster.Master" CodeBehind="~/Members/MediaCenter.aspx.vb" Inherits="WES.Members.MediaCenter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="banner" class="banner-interior">
        <div class="container">
            <div class="row">
                <div class="col-xs-12 page-title">
                    <h1>Media Center</h1>
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
            <div class="col-sm-4">
                <h2 id="back-video"><i class="fa fa-arrow-right fa-1.5x" aria-hidden="true"></i>Back</h2>
                <iframe src="https://player.vimeo.com/video/216243444?color=ff0303&byline=0&portrait=0" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>
            </div>
            <div class="col-sm-4">
                <h2 id="triceps-video"><i class="fa fa-arrow-right fa-1.5x" aria-hidden="true"></i>Triceps</h2>
                <iframe src="https://player.vimeo.com/video/216245488?color=ff0808&byline=0&portrait=0" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>
            </div>
            <div class="col-sm-4">
                <h2 id="chest-video"><i class="fa fa-arrow-right fa-1.5x" aria-hidden="true"></i>Chest</h2>
                <iframe src="https://player.vimeo.com/video/216245588?color=ff0808&byline=0&portrait=0" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>
            </div>
            </div>
    </div>
    </div>
</asp:Content>

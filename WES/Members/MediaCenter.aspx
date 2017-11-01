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
                <h4 id="back-video">Back</h4>
                <iframe src="https://player.vimeo.com/video/216243444?color=ff0303&byline=0&portrait=0" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>
            </div>
            <div class="col-sm-4">
                <h4 id="triceps-video">Triceps</h4>
                <iframe src="https://player.vimeo.com/video/216245488?color=ff0808&byline=0&portrait=0" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>
            </div>
            <div class="col-sm-4">
                <h4 id="chest-video">Chest</h4>
                <iframe src="https://player.vimeo.com/video/216245588?color=ff0808&byline=0&portrait=0" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-4">
                <h4 id="first-workout">Training for First Workouts</h4>
                <iframe src="https://player.vimeo.com/video/226359236" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>
            </div>
            <div class="col-sm-4">
                <h4 id="client-dizzy">If a Client Gets Dizzy or Light-headed</h4>
                <iframe src="https://player.vimeo.com/video/226365336" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>
            </div>
            <div class="col-sm-4">
                <h4 id="workout">The 324E Workout</h4>
                <iframe src="https://player.vimeo.com/video/226367747" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-4">
                <h4 id="overtraining">Overtraining</h4>
                <iframe src="https://player.vimeo.com/video/226369775" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>
            </div>
            <div class="col-sm-4">
                <h4 id="personal-training">Personal Training Sales</h4>
                <iframe src="https://player.vimeo.com/video/226365849" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>
            </div>
            <div class="col-sm-4">
                <h4 id="usta">Usta</h4>
                <iframe src="https://player.vimeo.com/video/239751529" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>
            </div>
        </div>
        
        <div class="row">
            <div class="col-sm-4">
                <h4 id="no-quit">No Quit</h4>
                <iframe src="https://player.vimeo.com/video/239751802" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>
            </div>
            <div class="col-sm-4">
                <h4 id="nutrition">Nutrition and Diet</h4>
                <iframe src="https://player.vimeo.com/video/240600958" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>
            </div>
        </div>
    </div>
</asp:Content>

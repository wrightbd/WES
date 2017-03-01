<%@ Page Title="World Exercise System" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master" CodeBehind="Default.aspx.vb" Inherits="WES._Default" %>

<%@ Register TagPrefix="WES" TagName="WESLoginControl" Src="~/Controls/WESLoginControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="description" content="Earn your Certified Personal Trainer Certificate and show that not only do you know the terminology, but you know how to properly use the techniques." />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .video-container {
            position: relative;
            padding-bottom: 56.25%;
            padding-top: 30px;
            height: 0;
            overflow: hidden;
        }

            .video-container iframe, .video-container object, .video-container embed {
                position: absolute;
                top: 0;
                left: 0;
                width: 100%;
                height: 100%;
            }
    </style>


    <asp:Panel ID="pnlMember" runat="server" Visible="false">
        <div class="row">
            <div class="col-sm-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h4>Member Quick Links</h4>
                    </div>
                    <div class="panel-body">
                        <div class="col-sm-2">
                            <a class="btn btn-darkblue btn-lg btn-block" role="button" href="~/Members/EBook.aspx" runat="server">
                                <br />
                                <span class="glyphicon glyphicon-book"></span>
                                <br />
                                E-Book</a>
                        </div>
                        <div class="col-sm-2">
                            <a class="btn btn-darkblue btn-lg btn-block" role="button" href="~/Members/StudyGuide.aspx" runat="server">
                                <br />
                                <span class="glyphicon glyphicon-folder-open"></span>
                                <br />
                                Study Guide</a>
                        </div>
                        <asp:Panel ID="pnlTest" runat="server" Visible="false">
                            <div class="col-sm-2">
                                <a class="btn btn-darkblue btn-lg btn-block" role="button" href="~/Members/WESPTC.aspx" runat="server">
                                    <br />
                                    <span class="glyphicon glyphicon-file"></span>
                                    <br />
                                    Test</a>
                            </div>
                        </asp:Panel>
                        <div class="col-sm-2">
                            <a class="btn btn-darkblue btn-lg btn-block" role="button" href="~/Members/Videos.aspx" runat="server"><span class="glyphicon glyphicon-cloud-upload"></span>
                                <br />
                                Video<br />
                                Submission</a>
                        </div>
                        <div class="col-sm-2">
                            <a class="btn btn-darkblue btn-lg btn-block" role="button" href="~/Members/MediaCenter.aspx" runat="server">
                                <br />
                                <span class="glyphicon glyphicon-film"></span>
                                <br />
                                Media Center</a>
                        </div>
                        <div class="col-sm-2">
                            &nbsp;
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <div id="banner">
        <img src="~/images/01.jpg" alt="World Exercise System" runat="server" />
        <div class="container">
            <div class="row">
                <div class="col-sm-6 col-md-4 info">
                    <h2>What Makes World Exercise System the RIGHT CHOICE?</h2>
                    <p>
                        World Exercise System is an industry leader in Personal Fitness Certification. 
                       Our Functional business skills are proven to increase sales, to create client fitness success and retention. 
                       WORLD EXERCISE SYSTEM is THE RIGHT CHOICE!
                    </p>
                </div>
            </div>
        </div>
    </div>

    <div id="call-to-action">
        <div class="container">
            <div class="row">
                <div class="col-xs-12 col-sm-10 col-md-8 center-block call-to-action">
                    <h2>Whether you're wanting to gain knowledge for your own fitness program.....
                        Getting started as a fitness professional or an experienced trainer seeking to upgrade your skills.....
                        World Exercise System provides a functional and proven skillset to take you to the next level.</h2>
                    <a href="https://www.worldexercisesystem.com/CreateAccount.aspx" class="btn btn-get-started">Get Started</a>
                </div>
            </div>
        </div>
    </div>



</asp:Content>

<%@ Page Title="World Exercise System" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master" CodeBehind="Default.aspx.vb" Inherits="WES._Default" %>
<%@ Register TagPrefix="WES" TagName="WESLoginControl" Src="~/Controls/WESLoginControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="description" content="Earn your Certified Personal Trainer Certificate and show that not only do you know the terminology, but you know how to properly use the techniques." />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
    <div id="banner">
        <img src="~/images/01.jpg" alt="World Exercise System" runat="server" />
        <div class="container">
            <div class="row">
                <div class="col-sm-6 col-md-4 info">
                    <h2>What Makes World Exercise System the RIGHT CHOICE?</h2>
                    <p>
                        World Exercise System is an industry leader in PERSONAL FITNESS CERTIFICATION. 
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
                    <a href="https://www.worldexercisesystem.com/ChoosePlan.aspx" class="btn btn-alt">Get Started</a>
                </div>
            </div>
        </div>
    </div>
    
    <div id="callout-info">
        <div class="container">
            <div class="row">
                <div class="col-xs-12 text-center">
                    <h2 class="callout-info-heading">World Exercise System is THE Right Choice for A Rewarding Career!</h2>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <div class="callout-info callout-info-01">
                        <img src="~/images/info01.jpg" runat="server" class="img-rounded img-responsive" alt=""/>
                        <div class="caption">
                            <h3>An Industry Leader</h3>
                            <p>World Exercise System is an industry leader when it comes to the Business of Personal Training.</p>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="callout-info callout-info-02">
                        <img src="~/images/info02.jpg" runat="server" class="img-rounded img-responsive" alt=""/>
                        <div class="caption">
                            <h3>Our Methodology</h3>
                            <p>Our Methodology has been developed by personal trainers through the success of tens of thousands of training sessions.</p>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="callout-info callout-info-03">
                        <img src="~/images/info03.jpg" runat="server" class="img-rounded img-responsive" alt=""/>
                        <div class="caption">
                            <h3>Client & Training Consistency</h3>
                            <p>Our Program Development Model, 324-E, adapts to any fitness program which creates client training consistency.</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <div id="banner-links" class="banner-links">
        <div class="container">
            <div class="row">
                <div class="col-xs-12 col-md-8 center-block text-center">
                    <h2>Get Certified Today!</h2>
                    <p>We will show you how to prepare your clients for each workout. Each workout gets them ready for one thing; which is their next workout.  Our functional business skills are proven to increase sales, to create client fitness success and retention.</p>
                    <a href="https://www.worldexercisesystem.com/ChoosePlan.aspx" class="btn btn-alt">Get Started</a>
                </div>
            </div>
        </div>
    </div>
    
    <div id="video" class="certification-info">
        <div class="container">
            <div class="row">
                <div class="col-md-4 col-md-offset-1">
                    <h2>The BEST Choice for Certification</h2>
                    <p>Designed by trainers with tens of thousands of client training sessions, the certification developer has more experience with personal training than other certification companies. We still have clients that have trained with us consistantly over the past 3 decades.</p>
                </div>
                <div class="col-md-6 col-md-offset-1">
                    <div class="embed-responsive embed-responsive-16by9">
                        <iframe class="embed-responsive-item" src="https://www.youtube.com/embed/NKIX-u-pVDI?ecver=2?rel=0" allowfullscreen></iframe>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

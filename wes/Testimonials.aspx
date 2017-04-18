<%@ Page Title="Testimonials" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master" CodeBehind="Testimonials.aspx.vb" Inherits="WES.Testimonials" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="description" content="Testimonials for World Exercise System" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="banner" class="banner-interior">
        <img src="~/images/banner-interior.jpg" alt="" runat="server" />
        <div class="container">
            <div class="row">
                <div class="col-xs-12 page-title">
                    <h1>Testimonials</h1>
                </div>
            </div>
        </div>
    </div>

    <div class="container">
        <div class="row">
            <div class="col-sm-12">

               <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-4 col-md-2">
                                <img src="images/chat-kleinpeter.jpg" alt="Chat Kleinpeter" class="img-responsive img-rounded" />
                            </div>
                            <div class="col-sm-8 col-md-10">
                                <h2>Dr. Chat Kleinpeter</h2>
                                <p>
                                    I began a lifelong journey of fitness with World Exercises System’s owner over thirty years ago. 
                           Today, at 62, I’m still  working out with him and have never felt better! 
                           Utilizing the program is incredible, motivating, and it makes perfect sense. 
                           He taught me that each work-out prepares me for my next work-out.
                                </p>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-4 col-md-2">
                                <img src="images/joe-martin.jpg" alt="Joe Martin" class="img-responsive img-rounded" />
                            </div>
                            <div class="col-sm-8 col-md-10">
                                <h2>Joe Martin<br />
                                    <small>ITI Technical College President</small></h2>
                                <p>
                                    Because of the World Exercise System, I’m in the best shape of my life at 58 years old. 
                            The 324-E fitness philosophy has allowed me to have the mindset to be consistent with my workouts even with my demanding schedule.  
                            As a result, I have achieved and maintain my fitness goals.
                                </p>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-4 col-md-2">
                                <img src="images/billy-potter.jpg" alt="Billy Potter" class="img-responsive img-rounded" />
                            </div>
                            <div class="col-sm-8 col-md-10">
                                <h2>Billy Potter</h2>
                                <p>
                                    Four years ago, I was going through life as a successful business man. 
                            Except, I was 50 pounds overweight and miserable. In the past when starting a new fitness program, I would always over-train and was constantly sore. 
                            The owner of World Exercise System started me on a fitness foundation building program, taught me proper conditioning protocol and intensity levels. 
                            I lost 50 pounds and gained muscle. I was encouraged to compete in fitness shows, because of my success. 
                            I highly recommend World Exercise System to anyone with interest in becoming a trainer.
                                </p>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>

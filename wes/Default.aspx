<%@ Page Title="World Exercise System" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master" CodeBehind="Default.aspx.vb" Inherits="WES._Default" %>
<%@ Register TagPrefix="WES" TagName="WESLoginControl" Src="~/Controls/WESLoginControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="description" content="Earn your Certified Personal Trainer Certificate and show that not only do you know the terminology, but you know how to properly use the techniques." />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .video-container {
	        position:relative;
	        padding-bottom:56.25%;
	        padding-top:30px;
	        height:0;
	        overflow:hidden;
        }

        .video-container iframe, .video-container object, .video-container embed {
	        position:absolute;
	        top:0;
	        left:0;
	        width:100%;
	        height:100%;
        }
    </style>

    <br />
    <asp:Panel ID="pnlMember" runat="server" Visible="false">
        <div class="row">
            <div class="col-sm-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h4>Member Quick Links</h4>
                    </div>
                    <div class="panel-body">
                        <div class="col-sm-2">
                            <a class="btn btn-darkblue btn-lg btn-block" role="button" href="~/Members/EBook.aspx" runat="server"><br /><span class="glyphicon glyphicon-book"></span><br />E-Book</a>
                        </div>
                        <div class="col-sm-2">
                            <a class="btn btn-darkblue btn-lg btn-block" role="button" href="~/Members/StudyGuide.aspx" runat="server"><br /><span class="glyphicon glyphicon-folder-open"></span><br />Study Guide</a>
                        </div>
                        <asp:Panel ID="pnlTest" runat="server" Visible="false">
                            <div class="col-sm-2">
                                <a class="btn btn-darkblue btn-lg btn-block" role="button" href="~/Members/WESPTC.aspx" runat="server"><br /><span class="glyphicon glyphicon-file"></span><br />Test</a>
                            </div>
                        </asp:Panel>
                        <div class="col-sm-2">
                            <a class="btn btn-darkblue btn-lg btn-block" role="button" href="~/Members/Videos.aspx" runat="server"><span class="glyphicon glyphicon-cloud-upload"></span><br />Video<br />Submission</a>
                        </div>
                        <div class="col-sm-2">
                            <a class="btn btn-darkblue btn-lg btn-block" role="button" href="~/Members/MediaCenter.aspx" runat="server"><br /><span class="glyphicon glyphicon-film"></span><br />Media Center</a>
                        </div>
                        <div class="col-sm-2">
                            &nbsp;
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3>What Makes World Exercise System THE RIGHT CHOICE?</h3><br />
                </div>
            </div>
            <div class="container-Home">
                <p>World Exercise System is an industry leader when it comes to the <span style="color: darkorange; font-weight: bold;">Business of Personal Training</span>.</p>

                <p>Our <span style="color: darkorange; font-weight: bold;">Methodology</span> has been developed by personal trainers through the success of tens of thousands of training sessions.</p>

                <p>Our Program Development Model, <span style="color: darkorange; font-weight: bold;">324-E</span>, adapts to any fitness program which creates client training consistency.</p>

                <p>Our <span style="color: darkorange; font-weight: bold;">Functional business skills</span> are proven to increase sales, to create client fitness success and retention.</p>
                <p>
                    &nbsp;&nbsp;&nbsp;&nbsp;<span style="color: #d8282f;font-weight: bold;">WORLD EXERCISE SYSTEM  is THE RIGHT CHOICE!</span><br />
		            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;...A REWARDING CAREER<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="BecomeCPT.aspx" class="btn btn-warning">Get Started</a>
                </p> 
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-darkblue">
                <div class="panel-heading text-center">
                    <h1>Learn more about World Exercise System Resources</h1>
                </div>
                <div class="panel-body" style="text-align: center;">
                    <br />
                    <a class="btn-resource" role="button" data-toggle="collapse" href="#certificateText" aria-expanded="false" aria-controls="certificateText">CPT Certificate</a>
                    <div class="collapse" id="certificateText">
                        <div class="well" style="text-align: left;">
                            Create your account and follow instructions to have full access to World Exercise System CPT Certificate. Upon completion of the Study Guide, submission of Video, and earning a passing grade on the World Exercise System test you will receive your certificate.<br /><br />
                            Three Certificate Options:<br />
                            <ul>
                                <li class="listglobe">Two-year Certificate</li>
                                <li class="listglobe">Three-year Certificate</li>
                                <li class="listglobe">Lifetime Certificate</li>
                            </ul>
                            <br />
                            FREE access to exclusive World Exercise System training and resources.
                        </div>
                    </div>
                    <br /><br />
                    <a class="btn-resource" role="button" data-toggle="collapse" href="#ebookText" aria-expanded="false" aria-controls="ebookText">E-Book</a>
                    <div class="collapse" id="ebookText">
                        <div class="well" style="text-align: left;">
                            An in-depth course manual which provides technique and practical application of fitness programing, keys to increase your personal training income and client retention.
                            <br /><br />
                            <ul>
                                <li class="listglobe">Personal Training 101 “Each Workout gets you ready for your next workout"</li>
                                <li class="listglobe">Exercise Program Development “Doing what you are supposed to do; not what the client feels like doing.” </li>
                                <li class="listglobe">324-E Fitness Program</li>
                                <li class="listglobe">The Business of Personal Training</li>
                                <li class="listglobe">Developing  A Balanced Life Style</li>
                                <li class="listglobe">Sales and Marketing “ The Art of Selling Yourself”</li>
                                <li class="listglobe">Image Building</li>
                            </ul>
                        </div>
                    </div>
                    <br /><br />
                    <a class="btn-resource" role="button" data-toggle="collapse" href="#studyGuideText" aria-expanded="false" aria-controls="studyGuideText">Study Guide</a>
                    <div class="collapse" id="studyGuideText">
                        <div class="well" style="text-align: left;">
                            It encompasses a detailed explanation of every test question and answer, so you will have a comprehensive understanding of personal training. Included in the guide are quizzes which are designed to aid in test preparedness.
                        </div>
                    </div>
                    <br /><br />
                    <a class="btn-resource" role="button" data-toggle="collapse" href="#videoSubText" aria-expanded="false" aria-controls="studyGuideText">Video Submission</a>
                    <div class="collapse" id="videoSubText">
                        <div class="well" style="text-align: left;">
                            You are required to submit a short workout video (2-3 minutes in length). You personally will complete the sets/reps for each exercise to be included in the video. (5 reps/set). The purpose of the video submission is our unique way of assisting you in fine tuning your technique and knowledge as a fitness trainer. 
                        </div>
                    </div>
                    <br /><br />
                    <a class="btn-resource" role="button" data-toggle="collapse" href="#testText" aria-expanded="false" aria-controls="testText">Test</a>
                    <div class="collapse" id="testText">
                        <div class="well" style="text-align: left;">
                                It is comprised of 117 True/False and Multiple Choice questions. The test is design to equip you with real world skills to boost  your personal training  business.
                        </div>
                    </div>

                    <br /><br />
                    <a class="btn-resource" role="button" data-toggle="collapse" href="#videosText" aria-expanded="false" aria-controls="videosText">Media Center</a>
                    <div class="collapse" id="videosText">
                        <div class="well" style="text-align: left;">
                            Media Center-  24/7 access to World Exercise System Video Library. 
                            <br /><br />
                            <ul>
                                <li class="listglobe">Course Application Videos</li>
                                <li class="listglobe">Fitness Concepts/Theory Videos</li>
                                <li class="listglobe">Fitness Technique Videos</li>
                                <li class="listglobe">Exercise Program Videos</li>
                                <li class="listglobe">Sales and Marketing Videos</li>
                                <li class="listglobe">Balanced Life Videos</li>
                            </ul>
                        </div>
                    </div>
                    <br /><br />
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-sm-12 text-center">
            <div class="video-container"><iframe width="560" height="315" src="https://www.youtube-nocookie.com/embed/NKIX-u-pVDI?rel=0&amp;showinfo=0" frameborder="0" allowfullscreen></iframe></div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            &nbsp;
        </div>
    </div>
    
</asp:Content>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default2.aspx.vb" Inherits="WES.Default2" %>
<%@ Register TagPrefix="WES" TagName="WESLoginControl" Src="~/Controls/WESLoginControl.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>World Exercise System</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="apple-touch-icon" href="apple-touch-icon.png" />

    <script src="js/vendor/jquery-1.11.3.js"></script>
    <script src="js/vendor/bootstrap.js"></script>

    <link href="css/bootstrap.css?20160825" rel="stylesheet" />
    <link href="css/bootstrap-theme.css?20160825" rel="stylesheet" />
    <link href="css/main.css?20160825" rel="stylesheet" />

    <script src="https://use.typekit.net/wkx6ugm.js"></script>
    <script>try{Typekit.load({ async: true });}catch(e){}</script>
    <script src="js/vendor/modernizr-2.8.3-respond-1.4.2.min.js"></script>

    <script>
      (function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
      (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
      m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
      })(window,document,'script','https://www.google-analytics.com/analytics.js','ga');

      ga('create', 'UA-86196324-1', 'auto');
      ga('send', 'pageview');

    </script>

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

    <meta name="robots" content="index, follow, archive" />
    <meta name="description" content="Earn your Certified Personal Trainer Certificate and show that not only do you know the terminology, but you know how to properly use the techniques." />
    <meta name="keywords" content="fitness,personal training certification,personal trainer certification,fitness trainer,personal fitness trainer,best personal training certification,how to become a certified personal trainer,cpt certification,personal trainer certification online,how to become a personal trainer" />
</head>
<body class="container-Home">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        
        <div class="navbar navbar-default">
            <div class="container">
                <a runat="server" class="navbar-brand" href="~/Default.aspx"></a>
                <div class="jumbotron">
                    <div class="slide slide1">
                        <br /><br /><br /><br /><br />
                        <h1 class="jumbotron-h1">PERSONAL TRAINING CERTIFICATION COMPANY</h1>

                    </div>
                </div>
               
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                </div>
                <div class="navbar-collapse collapse" id="bs-example-navbar-collapse-1">
                    <ul class="nav navbar-nav main-nav">
                        <li>
                            <a runat="server" href="~/Default.aspx">Home</a>
                        </li>
                        <li class="dropdown">
                            <a runat="server" href="~/AboutUs.aspx">About Us<span class="caret"></span></a>
                            <ul class="dropdown-menu subnav">
                                <li><a href="~/AboutUs.aspx" runat="server">About Us</a></li>
                                <li><a href="~/Mission.aspx" runat="server">Mission Statement</a></li>
                                <li><a href="~/FAQ.aspx" runat="server">FAQ</a></li>
                            </ul>
                        </li>
                        <li class="dropdown">
                            <a runat="server" href="~/Testimonials.aspx">Professional Recognitions<span class="caret"></span></a>
                            <ul class="dropdown-menu subnav">
                                <li><a href="~/Testimonials.aspx#chetk" runat="server">Chat Kleinpeter</a></li>
                                <li><a href="~/Testimonials.aspx#joem" runat="server">Joe Martin</a></li>
                                <li><a href="~/Testimonials.aspx#billyp" runat="server">Billy Potter</a></li>
                            </ul>
                        </li>
                        <li class="dropdown">
                            <a runat="server">Certificate<span class="caret"></span></a>
                            <ul class="dropdown-menu subnav">
                                <li><a href="~/BecomeCPT.aspx" runat="server">How to become a CPT</a></li>
                                <li><a href="~/PaymentPlans.aspx" runat="server">Payment Options</a></li>
                            </ul>
                        </li>
                        <li class="dropdown" id="liMember" runat="server">
                            <a runat="server">Member<span class="caret"></span></a>
                            <ul class="dropdown-menu subnav">
                                <li><a href="~/Members/StudyGuide.aspx" runat="server">Study Guide</a></li>
                                <li><a href="~/Members/EBook.aspx" runat="server">E-Book</a></li>
                                <li id="itemTest" runat="server"><a href="~/Members/WESPTC.aspx" runat="server">Test</a></li>
                                <li><a href="~/Members/Videos.aspx" runat="server">Video Submission</a></li>
                                <li><a href="~/Members/MediaCenter.aspx" runat="server">Media Center</a></li>
                            </ul>
                        </li>
                        <li class="dropdown" id="liAdmin" runat="server">
                            <a runat="server">Admin <span class="caret"></span></a>
                            <ul class="dropdown-menu subnav">
                                <li><a href="~/Admin/WesTrainees.aspx" runat="server">Trainees</a></li>
                                <li><a href="~/Admin/WesTraineeVideos.aspx" runat="server">Trainee Videos</a></li>
                                <li><a href="~/Admin/WesTests.aspx" runat="server">Tests</a></li>
                                <li><a href="~/Admin/WesTestQuestions.aspx" runat="server">Test Questions</a></li>
                                <li><a href="~/Admin/WesOrders.aspx" runat="server">Orders</a></li>
                                <li><a href="~/Admin/WesPromoCode.aspx" runat="server">Promo Codes</a></li>
                                <li><a href="~/Admin/WesMediaCenter.aspx" runat="server">Media Center</a></li>
                                <li><a href="~/Admin/WesItems.aspx" runat="server">Miscellaneous Items</a></li>
                            </ul>
                        </li>
                        <li class="dropdown" id="liManage" runat="server">
                            <a runat="server" href="~/Manage/ManageAccount.aspx">My Account</a>
                        </li>
                        <li id="liSignIn" runat="server" class="dropdown">
                            <a href="~/Login.aspx" runat="server">Sign in</a>
                            <ul class="dropdown-menu subnav">
                                <li>
                                    <div class="panel panel-darkblue">
                                        <div class="panel-heading">
                                            <asp:HyperLink ID="linkCreateAccount" runat="server" NavigateUrl="https://www.worldexercisesystem.com/CreateAccount.aspx" ForeColor="white"><h3>Create Account</h3></asp:HyperLink>
                                        </div>
                                    </div>
                                </li>
                                <li>
                                    <WES:WESLoginControl ID="WESLoginControlMenu" runat="server" ShowCreateAccount="False" />
                                </li>
                            </ul>
                        </li>
                        <li id="liSignOut" runat="server"><a href="~/Logout.aspx" runat="server">Sign out</a></li>
                        <li class="text-right">
                            <a href="mailto:wesptcertification@gmail.com?subject=Question"><span class="glyphicon glyphicon-envelope"></span></a>
                        </li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="container container-HomeBanner">
            <p>World Exercise System is an industry leader when it comes to the <span style="color: #d8282f; font-weight: bold;">Business of Personal Training</span>.</p>

            <p>Our <span style="color: #d8282f; font-weight: bold;">Methodology</span> has been developed by personal trainers through the success of tens of thousands of training sessions.</p>

            <p>Our Program Development Model, <span style="color: #d8282f; font-weight: bold;">324-E</span>, adapts to any fitness program which creates client training consistency.</p>

            <p>Our <span style="color: #d8282f; font-weight: bold;">Functional business skills</span> are proven to increase sales, to create client fitness success and retention.</p>
            <p>
                &nbsp;&nbsp;&nbsp;&nbsp;<span style="color: #d8282f;font-weight: bold;">WORLD EXERCISE SYSTEM  is THE RIGHT CHOICE!</span><br />
		        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;...A REWARDING CAREER<br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="BecomeCPT.aspx" class="btn btn-warning">Get Started</a>
            </p> 
            <p>&nbsp;</p>
            <p>&nbsp;</p>
            <p>&nbsp;</p>
            <p>&nbsp;</p>
            <p>&nbsp;</p>
        </div>

        <div class="container">
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel-homedarkblue">
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
                            <br />
                            <div class="row">
                                <div class="col-sm-12">&nbsp;</div>
                            </div>

                            <div class="row">
                                <div class="col-xs-2">
                                    &nbsp;
                                </div>
                                <div class="col-xs-8">
                                    <div class="panel panel-homegray">
                                        <div class="panel-body" style="text-align: center;">
                                            <div class="col-sm-4 text-center">
                                                &nbsp;
                                            </div>
                                            <div class="col-sm-4 text-center">
                                                <div class="video-container"><iframe width="560" height="315" src="https://www.youtube-nocookie.com/embed/NKIX-u-pVDI?rel=0&amp;showinfo=0" frameborder="0" allowfullscreen></iframe></div>
                                            </div>
                                            <div class="col-sm-4 text-center">
                                                &nbsp;
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-2">
                                    &nbsp;
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>



        <div class="container">
            <div class="row">
                <div class="col-sm-12">
                    &nbsp;
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12 backgroundMainBlue">
                    <div class="col-sm-4">
                        <asp:Panel ID="pnlLogin" runat="server">
                            <WES:WESLoginControl ID="WESLoginControl1" runat="server" />
                        </asp:Panel>
                    </div>      
                    <div class="col-sm-4">
                        <div class="panel panel-darkblue">
                            <div class="panel-heading">
                                <h4>Upcoming Events</h4>
                            </div>
                            <div class="panel-body">
                            
                            </div>
                        </div>
                    </div>   
                    <div class="col-sm-4">
                        <h3 style="color: white;">Connect with us</h3>
                        <a href="mailto:wesptcertification@gmail.com?subject=Question" class="btn btn-darkblue"><span class="glyphicon glyphicon-envelope"></span></a>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-bodyfooter">
            <div class="panel-heading text-right">
                <small><b>&copy;2016 | World Exercise System</b></small>
            </div>
        </div>
    </form>
</body>
</html>

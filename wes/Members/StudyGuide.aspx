<%@ Page Title="CPT Study Guide" Language="vb" AutoEventWireup="false" MasterPageFile="~/Members/MembersMaster.Master" CodeBehind="StudyGuide.aspx.vb" Inherits="WES.StudyGuide" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="robots" content="nofollow" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="banner" class="banner-interior">
        <img src="~/images/banner-interior.jpg" alt="" runat="server" />
        <div class="container">
            <div class="row">
                <div class="col-xs-12 page-title">
                    <h1>Study Guide</h1>
                </div>
            </div>
        </div>
    </div>
    <div class="container">
        <div class="row">
            <div class="col-sm-12">
                <p>If you wish to study for the Personal Trainer Certification, then you can download the Study Guide below.</p>
                <p>Below is an exerpt from the Study Guide:</p>
                <blockquote>
                    If you have to talk about your qualifications then you have not sold your potential client yet. You have to believe in yourself and have confidence. 
                Take charge of each client, not in a bossy way, but in an ‘I know what I am doing’ way. There is a difference between the two; be confident not cocky. 
                You are the leader of the client. You need to develop leadership skills and be confident and nice at the same time. 
                It is important to talk about your potential client’s goal and their limitations rather than what your goals are and all of your qualifications.
                </blockquote>
                <a class="btn btn-danger btn-lg" href="Docs/WESStudyGuide.pdf">Download Study Guide</a>
            </div>
        </div>
    </div>
</asp:Content>

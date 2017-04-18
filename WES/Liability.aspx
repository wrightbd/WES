<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master" CodeBehind="Liability.aspx.vb" Inherits="WES.Members.UpgradeMembership" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" />

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="banner" class="banner-interior">
        <img src="~/images/banner-interior.jpg" alt="" runat="server" />
        <div class="container">
            <div class="row">
                <div class="col-xs-12 page-title">
                    <h1>Liability Agreement</h1>
                </div>
            </div>
        </div>
    </div>
    <div class="container">
        <div class="row">
            <div class="col-xs-12">
                <p class="lead">
                    By clicking continue, you agree to the following agreement below. Your acceptance of this agreement is required in order to setup a user account
                    within our system. Please read below and select "continue & register".
                </p>
                <div class="row">
                    <div class="col-xs-12">
                        <a class="btn btn-lg btn-danger" role="button" href="~/CreateAccount.aspx" runat="server">Continue & Register</a>
                    </div>
                </div>
                <p>
                    World Exercise System, LLC (“us”, “we”, or “our”) operates worldexercisesystem.com (the “WebSite”).  This Agreement is between World Exercise System, LLC and 
                    Users of the Website.  By accessing or using the Website in any manner, including, but not limited to, visiting or browsing the Website or contributing content 
                    or other materials to the Website, you agree to be bound by the Terms and Conditions herein.
                </p>

                <p>Definitions: </p>
                <ul>
                    <li>“WES”, “us” or “we” or “our” refers to World Exercise System LLC, the owner of the Website.</li>
                    <li>“Member” is an individual that has registered with WES to use our Service.</li>
                    <li>“Visitor” is an individual who browses our Website, but has not registered as Member.</li>
                    <li>“Service” is the functionality and features offered through WES’s Website to our Members.</li>
                    <li>“Content” is all text, information, graphics, audio, video, and data offered through WES’ Website. </li>
                    <li>“User” or “You” collectively refers to a Member or a Visitor as defined above.</li>
                    <li>You promise not to sue, and to release and indemnify, World Exercise System, LLC and employees/operators from all claims identified herein.</li>
                </ul>

                <p>
                    World Exercise System recommends, and You agree, that personal trainers obtain a letter of consent from the personal physician for all clients, without regard to appearance 
                    of health status, and prior to any services being offered by you. You further understand that to participate in our Services, You must be at least 18 years old and must 
                    complete an in-person practicum with a qualified personal trainer, consisting of a minimum of 25 hours, before acting without supervision, and that You will pursue and 
                    maintain continuing education as well as obtaining liability insurance appropriate to your activities as a personal trainer.
                </p>
                <p>
                    You assume any and all risks associated with the performance or usage of any of the Content or Service provided by World Exercise System, LLC and waive, release, and 
                    forever discharges World Exercise System, LLC of all actions of whatever kind or nature either in law or in equity arising from or by reason of bodily injury or personal 
                    injuries known or unknown, death, or property damage resulting during the use of our Content or Service. You agree and assume full responsibility for the risk of your own 
                    bodily damage, death, or property damage during your use of any Content or Service provided by World Exercise System, LLC.  You agree and assume full responsibility for 
                    the risk of bodily damage, death, or property damage to third parties and/or your clients during your use of any Content or Service provided by World Exercise System, LLC.  
                    You agree and shall hold World Exercise System, LLC harmless for any liability thereof as set forth herein. 
                </p>
                <p>
                    User understand that this Agreement is a contract and agree that if a lawsuit is commenced against World Exercise System, LLC, its trainers, its operators, agents, and/or 
                    employees for any injury or damage alleged by, or allegedly attributable to User, User will pay all attorney’s fees and costs reasonably incurred by us to defend that lawsuit.
                </p>
                <p>
                    User hereby acknowledges and agrees that User assumes full responsibility for his/her own safety with using Content and Service located on the Website. User understands 
                    that he/she assumes 100% of the risk of injury, death, and property damage as provided by this Agreement. This release shall be governed by the laws of the state of Louisiana. 
                    If ANY portion of this release is held invalid by a court, it is agreed that the remainder of the release shall continue in full legal force and effect notwithstanding the 
                    invalidity of any portion of it.
                </p>
                <p>
                    Nothing on the Website, should be construed to be a warranty of any type whatsoever. World Exercise System, LLC makes no representation as to the accuracy, nor completeness of 
                    the Content on the Website, and you agree to hold World Exercise System harmless for any errors in the delivery of Services or the Content provided. 
                </p>
                <p>
                    You agree that certifications issued by World Exercise System, LLC does not constitute an assessment of actual skill or training and should not be relied upon by employers or 
                    individuals as evidence of qualifications. Any misrepresentation of our certifications constitutes fraud and You agree that no liability whatsoever will extend to World Exercise 
                    System, LLC its employees, principles, or shareholders for any such use of the certification.
                </p>
                <p>
                    This release is given on behalf of the User, User’s spouse, User’s legal representatives, administrators, executors, heirs, and the legal representatives of any children the 
                    Users have. This release is an ongoing release and remains in effect indefinitely.

                </p>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <a class="btn btn-lg btn-danger" role="button" href="~/CreateAccount.aspx" runat="server">Continue & Register</a>
            </div>
        </div>
    </div>
</asp:Content>

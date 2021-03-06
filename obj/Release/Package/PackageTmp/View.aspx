﻿<%@ Page Title="View" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="QRJ.View" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper" id="InactiveHeader" runat="server">
            <hgroup class="title">
                <h2>Please activate your QR Code.</h2>
            </hgroup>
        </div>
        <div class="content-wrapper" id="ActiveHeader" runat="server">
            <hgroup class="title">
                <h2>Code not linked to a video.</h2>
            </hgroup>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div id="InactiveBody" runat="server">
        <h3>Instructions:</h3>
        <ol class="round">
            <li class="one">
                <h5>Visit our web page</h5>
                Go to <a id="homePage" runat="server"></a>
            </li>
            <li class="two">
                <h5>Register or login</h5>
                Click on Register to create an account, or Login if you already have one.
            </li>
            <li class="three">
                <h5>Activate product</h5>
                On your account page, click on “Activate new product” button. Enter the activation code:
                <h5 id="activationCode" runat="server"></h5>
            </li>
            <li class="four">
                <h5>Upload</h5>
                Upload your video
            </li>
        </ol>
    </div>
    <div id="ActiveBody" runat="server">
        <h3>The code has been activated by a user but it has not been linked to a video.</h3>
    </div>
</asp:Content>
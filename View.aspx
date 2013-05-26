﻿<%@ Page Title="View" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="QRJ.View" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h2>Please activate your QR Code.</h2>
            </hgroup>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Instructions:</h3>
    <ol class="round">
        <li class="one">
            <h5>Create an account</h5>
            Go to <a id="homePage" runat="server"></a> and register
        </li>
        <li class="two">
            <h5>Activate product</h5>
            On your account page, click on “Activate new product” button. Enter the activation code:
            <h5 id="activationCode" runat="server"></h5>
        </li>
        <li class="three">
            <h5>Upload</h5>
            Upload your video
        </li>
    </ol>
</asp:Content>
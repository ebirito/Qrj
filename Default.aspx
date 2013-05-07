<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="QRJ._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h2>Upload videos and link them to your jewelry.</h2>
            </hgroup>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Just 4 easy steps:</h3>
    <ol class="round">
        <li class="one">
            <h5>Create an account</h5>
            Only an e-mail is required
            <a href="Account/Register">Register now!</a>
        </li>
        <li class="two">
            <h5>Activate your QR jewelry</h5>
            Using the activation code
        </li>
        <li class="three">
            <h5>Upload your video</h5>
            Your video will be linked to your QR jewelry
        </li>
        <li class="four">
            <h5>Scan and watch</h5>
            Use a QR Scanner app and scan your QR jewelry
        </li>
    </ol>
</asp:Content>

<%@ Page Title="View" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FileSizeError.aspx.cs" Inherits="QRJ.FileSizeError" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h2>File exceeds limit.</h2>
            </hgroup>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>You have tried to upload a file that exceeds the 50 MB file limit. Please check your file size and try again.</h3>
</asp:Content>
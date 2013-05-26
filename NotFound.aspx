<%@ Page Title="View" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NotFound.aspx.cs" Inherits="QRJ.NotFound" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h2>Code not found.</h2>
            </hgroup>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>The code you have scanned is invalid.</h3>
</asp:Content>
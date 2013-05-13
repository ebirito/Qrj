<%@ Page Title="Generate" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Generate.aspx.cs" Inherits="QRJ.AdminPages.Generate" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h2>Generate new QR codes.</h2>
    </hgroup>
    <div>
        <asp:Label ID="lblNumber" runat="server" AssociatedControlID="Number" style="display:inline">Number of Codes to generate:</asp:Label>
        <asp:TextBox ID="Number" runat="server" style="display:inline" Width="50px"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="Number"
                                CssClass="field-validation-error" Display="Dynamic" ErrorMessage="Number is required." />
        <asp:RangeValidator runat="server" ControlToValidate="Number" CssClass="field-validation-error" Display="Dynamic" MinimumValue="1" MaximumValue="100" ErrorMessage="Number must be between 1 and 100" />
    </div>
    <asp:Button ID="btnSubmit" Text="Generate" runat="server" OnClick="btnSubmit_Click" />
</asp:Content>

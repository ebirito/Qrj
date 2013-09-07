<%@ Page Title="Category" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CategoryContent.aspx.cs" Inherits="QRJ.AdminPages.CategoryContent" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h2>Video.</h2>
    </hgroup>
    <div>
        <asp:Label ID="lblName" runat="server" style="display:inline" AssociatedControlID="Name">Name:</asp:Label>
        <asp:TextBox ID="Name" runat="server" style="display:inline" Width="150px"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="Name"
                                CssClass="field-validation-error" Display="Dynamic" ErrorMessage="Name is required." />
    </div>
    <div id="errorsDiv" class="validation-summary-errors" runat="server" visible="false">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </div>
    <asp:Button ID="btnSubmit" Text="Save" runat="server" OnClick="btnSubmit_Click" />
    <asp:Button ID="btnCancel" Text="Cancel" runat="server" />
</asp:Content>

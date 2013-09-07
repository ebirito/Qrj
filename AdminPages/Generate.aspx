<%@ Page Title="Generate" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Generate.aspx.cs" Inherits="QRJ.AdminPages.Generate" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h2>Generate new URLs.</h2>
    </hgroup>
    <div>
        <div>
            <asp:Label ID="lblNumber" runat="server" AssociatedControlID="Number" style="display:inline">Number of URLs to generate:</asp:Label>
            <asp:TextBox ID="Number" runat="server" style="display:inline" Width="50px"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="Number"
                                    CssClass="field-validation-error" Display="Dynamic" ErrorMessage="Number is required." />
            <asp:RangeValidator runat="server" ControlToValidate="Number" CssClass="field-validation-error" Display="Dynamic" Type="Integer" MinimumValue="1" MaximumValue="100" ErrorMessage="Number must be between 1 and 100" />
        </div>
        <div>
            <asp:Label ID="lblFormat" runat="server" AssociatedControlID="Format" style="display:inline">URL format:</asp:Label>
            <asp:DropDownList ID="Format" runat="server">
                <asp:ListItem Text="QR Code" Value="QRCode" Selected="True" />
                <asp:ListItem Text="Text" Value="Text" />
            </asp:DropDownList>
        </div>
        <asp:Button ID="btnSubmit" Text="Generate" runat="server" OnClick="btnSubmit_Click" />
    </div>
    <hr />
    <div>
        <asp:Button runat="server" id="btnManageContent" text="Manage Content" OnClientClick="window.location.href='ManageContent'; return false;" />
    </div>
</asp:Content>

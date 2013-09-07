<%@ Page Title="Category" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Category.aspx.cs" Inherits="QRJ.AdminPages.Category" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h2>Category.</h2>
    </hgroup>
    <div>
        <asp:Label ID="lblName" runat="server" style="display:inline" AssociatedControlID="Name">Name:</asp:Label>
        <asp:TextBox ID="Name" runat="server" style="display:inline" Width="150px"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="Name"
                                CssClass="field-validation-error" Display="Dynamic" ErrorMessage="Name is required." />
    </div>
    <div>
        <asp:Label ID="lblFrequency" runat="server" AssociatedControlID="Frequency" style="display:inline">Display frequency:</asp:Label>
        <asp:DropDownList ID="Frequency" runat="server">
            <asp:ListItem Text="Daily" Value="Daily" Selected="True" />
            <asp:ListItem Text="On-Demand" Value="OnDemand" />
        </asp:DropDownList>
    </div>
    <div id="errorsDiv" class="validation-summary-errors" runat="server" visible="false">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </div>
    <asp:Button ID="btnSubmit" Text="Save" runat="server" OnClick="btnSubmit_Click" />
</asp:Content>

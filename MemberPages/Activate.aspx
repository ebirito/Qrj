<%@ Page Title="Activate" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Activate.aspx.cs" Inherits="QRJ.AdminPages.Activate" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h2>Activate new product.</h2>
    </hgroup>
    <div>
        <asp:Label ID="lblActivationCode" runat="server" style="display:inline">Activation Code (include dashes):</asp:Label>
        <asp:TextBox ID="ActivationCode" runat="server" style="display:inline" Width="190px" MaxLength="19" CssClass="uppercase"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="ActivationCode"
                                CssClass="field-validation-error" Display="Dynamic" ErrorMessage="Code is required." />
         <asp:RegularExpressionValidator runat="server"
                                    CssClass="field-validation-error" Display="Dynamic"     
                                    ErrorMessage="Invalid activation code format. Correct format: ABCD-EFGH-IJKL-MNOP" 
                                    ControlToValidate="ActivationCode"     
                                    ValidationExpression="^[a-zA-Z]{4}-[a-zA-Z]{4}-[a-zA-Z]{4}-[a-zA-Z]{4}$" />
    </div>
    <div>
        <asp:Label ID="lblName" runat="server" style="display:inline">Product name (for example "Dad’s pendant", or "Mom's bracelet):</asp:Label>
        <asp:TextBox ID="ProductName" runat="server" style="display:inline" Width="150px"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="ProductName"
                                CssClass="field-validation-error" Display="Dynamic" ErrorMessage="Product name is required." />
    </div>
    <div id="errorsDiv" class="validation-summary-errors" runat="server" visible="false">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </div>
    <asp:Button ID="btnSubmit" Text="Activate" runat="server" OnClick="btnSubmit_Click" />
</asp:Content>

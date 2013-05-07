<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="QRJ.Account.Login" %>
<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Request.QueryString["Role"] %> <%: Title %>.</h1>
    </hgroup>
    <asp:Login runat="server" ViewStateMode="Disabled" RenderOuterTable="false">
        <LayoutTemplate>
            <p class="validation-summary-errors">
                <asp:Literal runat="server" ID="FailureText" />
            </p>
            <fieldset>
                <legend>Log in Form</legend>
                <ol>
                    <li>
                        <asp:Label runat="server" AssociatedControlID="UserName">Email</asp:Label>
                        <asp:TextBox runat="server" ID="UserName" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName" CssClass="field-validation-error" Display="Dynamic"
                             ErrorMessage="Email is required." />
                        <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" 
                            ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="UserName" 
                            CssClass="field-validation-error" Display="Dynamic" ErrorMessage="Invalid Email."></asp:RegularExpressionValidator>
                    </li>
                    <li>
                        <asp:Label runat="server" AssociatedControlID="Password">Password</asp:Label>
                        <asp:TextBox runat="server" ID="Password" TextMode="Password" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="field-validation-error" ErrorMessage="Password is required." />
                    </li>
                    <!--<li>
                        <asp:CheckBox runat="server" ID="RememberMe" />
                        <asp:Label runat="server" AssociatedControlID="RememberMe" CssClass="checkbox">Remember me?</asp:Label>
                    </li>-->
                </ol>
                <asp:Button runat="server" CommandName="Login" Text="Log in" />
            </fieldset>
        </LayoutTemplate>
    </asp:Login>
    <p>
        <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled">Register</asp:HyperLink>
        if you don't have an account.
    </p>
    <!--
    <section id="socialLoginForm">
        <h2>Use another service to log in.</h2>
        <uc:OpenAuthProviders runat="server" ID="OpenAuthLogin" />
    </section>
    -->
</asp:Content>

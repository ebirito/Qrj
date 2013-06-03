<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="QRJ.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h2>Please create a new account.</h2>
    </hgroup>

    <asp:CreateUserWizard runat="server" ID="RegisterUser" ViewStateMode="Disabled" OnCreatingUser="RegisterUser_CreatingUser" OnCreatedUser="RegisterUser_CreatedUser" RequireEmail="false">
        <LayoutTemplate>
            <asp:PlaceHolder runat="server" ID="wizardStepPlaceholder" />
            <asp:PlaceHolder runat="server" ID="navigationPlaceholder" />
        </LayoutTemplate>
        <WizardSteps>
            <asp:CreateUserWizardStep runat="server" ID="RegisterUserWizardStep">
                <ContentTemplate>
                    <p class="message-info">
                        Password is required to be a minimum of <%: Membership.MinRequiredPasswordLength %> characters in length and contain both letters and numbers.
                    </p>

                    <p class="validation-summary-errors">
                        <asp:Literal runat="server" ID="ErrorMessage" />
                    </p>

                    <fieldset>
                        <legend>Registration Form</legend>
                        <ol>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="UserName">Email</asp:Label>
                                <asp:TextBox runat="server" ID="UserName" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                                    CssClass="field-validation-error" Display="Dynamic" ErrorMessage="Email is required." />
                                <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" 
                                    ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="UserName" 
                                    CssClass="field-validation-error" Display="Dynamic" ErrorMessage="Invalid Email."></asp:RegularExpressionValidator>
                            </li>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="Password">Password</asp:Label>
                                <asp:TextBox runat="server" ID="Password" TextMode="Password" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                                    CssClass="field-validation-error" Display="Dynamic" ErrorMessage="Password is required." />
                                <asp:RegularExpressionValidator runat="server" 
                                    ValidationExpression="[A-Za-z].*[0-9]|[0-9].*[A-Za-z]" ControlToValidate="Password" 
                                    CssClass="field-validation-error" Display="Dynamic" 
                                    ErrorMessage="Password must contain both letters and numbers."></asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator runat="server" ControlToValidate="Password"
                                   ValidationExpression=".{8}.*"
                                   CssClass="field-validation-error" Display="Dynamic" 
                                   ErrorMessage="Password must be at least 8 characters."></asp:RegularExpressionValidator>
                            </li>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="ConfirmPassword">Confirm password</asp:Label>
                                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                                     CssClass="field-validation-error" Display="Dynamic" ErrorMessage="Confirm password is required." />
                                <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                                     CssClass="field-validation-error" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
                            </li>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="FirstName">First Name</asp:Label>
                                <asp:TextBox runat="server" ID="FirstName" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="FirstName"
                                    CssClass="field-validation-error" ErrorMessage="First Name is required." />
                            </li>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="LastName">Last Name</asp:Label>
                                <asp:TextBox runat="server" ID="LastName" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="LastName"
                                    CssClass="field-validation-error" ErrorMessage="Last Name is required." />
                            </li>
                        </ol>
                        <asp:Button runat="server" CommandName="MoveNext" Text="Register" />
                    </fieldset>
                </ContentTemplate>
                <CustomNavigationTemplate />
            </asp:CreateUserWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>
</asp:Content>
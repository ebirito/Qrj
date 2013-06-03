<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="QRJ.Account.ForgotPassword" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:PasswordRecovery ID="PasswordRecovery1" runat="server">
           <UserNameTemplate>
              <table border="0" cellpadding="1">
                <tr>
                  <td>
                    <table border="0" cellpadding="0">
                      <tr>
                        <td align="left" colspan="2">
                          Forgot Your Password?</td>
                      </tr>
                      <tr>
                        <td align="left" colspan="2">
                          Enter your email to retrieve your password.</td>
                      </tr>
                      <tr>
                        <td align="right">
                          <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Email:</asp:Label></td>
                        <td>
                          <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                            ErrorMessage="Email is required." CssClass="field-validation-error" Display="Dynamic" ValidationGroup="PasswordRecovery1"></asp:RequiredFieldValidator>
                          <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" 
                            ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="UserName" 
                            CssClass="field-validation-error" Display="Dynamic" ErrorMessage="Invalid Email." ValidationGroup="PasswordRecovery1"></asp:RegularExpressionValidator>
                        </td>
                      </tr>
                      <tr>
                        <td align="center" colspan="2" style="color: red">
                          <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                        </td>
                      </tr>
                      <tr>
                        <td align="left" colspan="2">
                          <asp:Button ID="SubmitButton" runat="server" CommandName="Submit" Text="Submit" ValidationGroup="PasswordRecovery1" />
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
            </UserNameTemplate>
    </asp:PasswordRecovery>
</asp:Content>

<%@ Page Title="Category" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="QRJ.AdminPages.Product" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h2>Activate product.</h2>
    </hgroup>
    <div>
        <asp:Label ID="lblName" runat="server" style="display:inline" AssociatedControlID="Name">Name:</asp:Label>
        <asp:TextBox ID="Name" runat="server" style="display:inline" Width="150px"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="Name"
                                CssClass="field-validation-error" Display="Dynamic" ErrorMessage="Name is required." />
        <asp:Label ID="lblExplanation" runat="server" style="display:inline" AssociatedControlID="Name">(for example "Dad’s pendant", or "Mom's bracelet)</asp:Label>
    </div>
    <div id="errorsDiv" class="validation-summary-errors" runat="server" visible="false">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </div>
    <div id="subscriptionsDiv" runat="server">
        <hr />
        <div>
            <asp:Label ID="lblFrequency" runat="server" AssociatedControlID="Frequency" style="display:inline">Subscription type:</asp:Label>
            <asp:DropDownList ID="Frequency" OnSelectedIndexChanged="Frequency_SelectedIndexChanged" AutoPostBack="true" runat="server">
                <asp:ListItem Text="Daily" Value="Daily" Selected="True" />
                <asp:ListItem Text="On-Demand" Value="OnDemand" />
            </asp:DropDownList>
        </div>
        <asp:GridView ID="Subscriptions" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="Id" SelectMethod="Subscriptions_GetData" DeleteMethod="CategoryContents_DeleteItem"
            AllowPaging="False" PageSize="100" EnableViewState="True" CssClass="mGrid" AlternatingRowStyle-CssClass="alt" 
            EmptyDataText="No subscriptions available for this type" OnRowDataBound="Subscriptions_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox ID="SubscriptionSelector" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Name" HeaderText="Name" 
                    SortExpression="Name" />
            </Columns>
        </asp:GridView>
    </div>
    <asp:Button ID="btnSubmit" Text="Save" runat="server" OnClick="btnSubmit_Click" />
    <asp:Button ID="btnCancel" Text="Cancel" runat="server" OnClientClick="window.location.href='Home'; return false;" />
</asp:Content>

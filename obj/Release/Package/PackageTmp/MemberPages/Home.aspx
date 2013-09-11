<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="QRJ.MemberPages.Home" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h2>Your activated products.</h2>
    </hgroup>
    <asp:GridView ID="Products" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="Id" SelectMethod="Products_GetData" 
        AllowPaging="False" EnableViewState="True" CssClass="mGrid" AlternatingRowStyle-CssClass="alt" 
        EmptyDataText="You currently do not have any activated products" OnRowEditing="Products_RowEditing" DeleteMethod="Products_DeleteItem">
        <Columns>
            <asp:BoundField DataField="ProductName" HeaderText="Product Name" 
                SortExpression="ProductName" />
            <asp:BoundField DataField="ActivatedOn" HeaderText="Activated On" 
                SortExpression="ActivatedOn" />
            <asp:BoundField DataField="Subscriptions" HeaderText="Subscriptions" 
                SortExpression="Subscriptions" />
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
        </Columns>
    </asp:GridView>
    <h3>Scan a product using your smartphone to activate it!.</h3>
</asp:Content>


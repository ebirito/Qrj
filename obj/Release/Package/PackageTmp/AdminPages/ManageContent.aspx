<%@ Page Title="Manage Content" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageContent.aspx.cs" Inherits="QRJ.AdminPages.ManageContent" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h2>Content Categories.</h2>
    </hgroup>
    <div>
        <asp:GridView ID="Categories" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="Id" SelectMethod="Categories_GetData" DeleteMethod="Categories_DeleteItem"
            AllowPaging="True" PageSize="5" EnableViewState="True" CssClass="mGrid" AlternatingRowStyle-CssClass="alt" 
            EmptyDataText="No categories configured">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Name" 
                    SortExpression="Name" />
                <asp:BoundField DataField="Frequency" HeaderText="Frequency" 
                    SortExpression="Frequency" />
                <asp:BoundField DataField="NumberOfContents" HeaderText="Number of Videos" 
                    SortExpression="NumberOfContents" />
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            </Columns>
        </asp:GridView>
    </div>
    <div>
        <asp:Button runat="server" id="btnNewCategory" text="Add New" OnClientClick="window.location.href='Category'; return false;" />
    </div>
</asp:Content>

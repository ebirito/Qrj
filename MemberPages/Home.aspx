<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="QRJ.MemberPages.Home" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h2>Your QR codes.</h2>
    </hgroup>
    <a href="Activate">Activate new product</a>
    <asp:GridView ID="QRCodes" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="Id" SelectMethod="QRCodes_GetData" 
        AllowPaging="False" EnableViewState="True" CssClass="mGrid" AlternatingRowStyle-CssClass="alt" 
        EmptyDataText="You currently do not have any activated products,">
        <Columns>
            <asp:TemplateField HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="ProductSelector" runat="server" onclick="GridCheckBoxClicked();" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ProductName" HeaderText="Product Name" 
                SortExpression="ProductName" />
            <asp:BoundField DataField="ActivatedOn" HeaderText="Activated On" 
                SortExpression="ActivatedOn" />
        </Columns>
    </asp:GridView>
    <div>
        Please select using the checkboxes the products that you would like to link to this video.
    </div>
    <div>
        <asp:FileUpload id="FileUpload" runat="server" onchange="FileUploadChanged();" />
        <asp:RegularExpressionValidator ID="FileUploadValidator" runat="server" ControlToValidate="FileUpload"
         ErrorMessage="Only .mp4 format is supported" 
         ValidationExpression=".+\.([Mm][Pp][4])" CssClass="field-validation-error" Display="Dynamic"></asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ControlToValidate="FileUpload" 
            CssClass="field-validation-error" Display="Dynamic" ErrorMessage="Please select a file." />
        <asp:Button runat="server" id="UploadVideo" text="Upload Video" onclick="UploadVideo_Click" UseSubmitBehavior="true" Enabled="false" />
     </div>
    <div>
        Maximum file size is 50 MB.
    </div>
</asp:Content>


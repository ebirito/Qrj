<%@ Page Title="Video" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CategoryContent.aspx.cs" Inherits="QRJ.AdminPages.CategoryContent" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <script type = "text/javascript">
        function clientuploadComplete(sender) {
            window.location = window.location.href;
        }
    </script>

    <hgroup class="title">
        <h2>Video.</h2>
    </hgroup>
    <div>
        <asp:Label ID="lblName" runat="server" style="display:inline" AssociatedControlID="Name">Name:</asp:Label>
        <asp:TextBox ID="Name" runat="server" style="display:inline" Width="150px"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="Name"
                                CssClass="field-validation-error" Display="Dynamic" ErrorMessage="Name is required." />
    </div>
    <div id="errorsDiv" class="validation-summary-errors" runat="server" visible="false">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </div>
    <div id="divUploader" runat="server">
        <ajaxToolkit:AjaxFileUpload
            id="FileUpload" AllowedFileTypes="mp4" MaximumNumberOfFiles="1" 
            OnUploadComplete="FileUpload_UploadComplete" OnClientUploadComplete="clientuploadComplete" StoreToAzure="true" AzureContainerName="videos"
            runat="server"  />
    </div>
    <asp:Button ID="btnSubmit" Text="Save" runat="server" OnClick="btnSubmit_Click" />
    <asp:Button ID="btnCancel" Text="Cancel" runat="server" />
    <div id="divVideo" runat="server">
        <h3>Preview</h3>
        <video width="576" height="320" controls>
            <source id="videoSource" runat="server" type="video/mp4" />
        </video>
    </div>
</asp:Content>

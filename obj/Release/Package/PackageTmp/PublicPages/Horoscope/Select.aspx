<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Select.aspx.cs" Inherits="QRJ.PublicPages.Horoscope.Select" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="../../Content/themes/base/jquery.mobile-1.4.2.min.css">
    <script src="../../Scripts/jquery-1.8.2.min.js"></script>
    <script src="../../Scripts/jquery.mobile-1.4.2.min.js"></script>
</head>
<body>
    <form id="form1" runat="server" data-ajax="false">
        <input type="button" value="Adult" onclick="$('#btnAdult').click();">
		<input type="button" value="Teen" onclick="$('#btnTeen').click();" >
        <label>
            <input id="chkRemember" runat="server" type="checkbox" checked="checked">Remember this
        </label>
        <div style="display:none">
            <asp:Button id="btnAdult" runat="server" onclick="btnAdult_Click" />
            <asp:Button id="btnTeen" runat="server" onclick="btnTeen_Click" />
            <asp:TextBox ID="txtTimezone" runat="server" />
        </div>
    </form>
    <script type="text/javascript">
        $(window).load(function () {
            $('#txtTimezone').val(new Date().getTimezoneOffset() / 60);
        });
    </script>
</body>
</html>

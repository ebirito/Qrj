<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Select.aspx.cs" Inherits="QRJ.PublicPages.Horoscope.Select" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="../../Content/themes/base/jquery.mobile-1.4.2.min.css">
    <script src="../../Scripts/jquery-1.8.2.min.js"></script>
    <script src="../../Scripts/jquery.mobile-1.4.2.min.js"></script>
    <style>
        .ui-btn {
            margin-top: 0px;
            word-wrap: break-word !important;
            white-space: normal !important;
            color: darkblue !important;
        }
        .ui-checkbox {
            margin-bottom: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" data-ajax="false">
        <div style="background-color:#000061">
            <img src="../../Content/themes/base/images/demo/SelectionBackgroundTop.jpg" style="height:auto; width:100%" />
            <table width="100%" style="background-color:#000061">
                <tr>
                    <td width="45%" style="vertical-align:top">
                        <img src="../../Content/themes/base/images/demo/SelectionBackgroundLeft.jpg" style="height:auto; width:100%" />
                    </td>
                    <td width="10%">
                        <input type="button" value="ADULT HOROSCOPES" onclick="$('#btnAdult').click();">
		                <input type="button" value="TEEN HOROSCOPES" onclick="$('#btnTeen').click();" >
                        <label style="text-align:center">
                            <input id="chkRemember" runat="server" type="checkbox" checked="checked">REMEMBER SELECTION
                        </label>
                    </td>
                    <td width="45%" style="vertical-align:top">
                        <img src="../../Content/themes/base/images/demo/SelectionBackgroundRight.jpg" style="height:auto; width:100%"  />
                    </td>
                </tr>
            </table>
            <img src="../../Content/themes/base/images/demo/SelectionBackgroundBottom.jpg" style="height:auto; width:100%" />
        </div>
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

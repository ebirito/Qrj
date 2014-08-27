<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="QRJ.PublicPages.Horoscope.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="../../Scripts/jquery-1.8.2.min.js"></script>
    <script src="../../Scripts/jquery.fullbg.min.js"></script>
    <style type="text/css">
        .fullBg {
	        position: fixed;
	        top: 0;
	        left: 0;
	        overflow: hidden;
        }
        .text {
            text-align: justify;
            color: #fff;
        }
    </style>
</head>
<body>
    <div id="divBackgroundImage" runat="server">
        <img id="imgBackground" runat="server" alt="" />
    </div>
    <div id="txtHoroscope" class="text" runat="server" />
    <script type="text/javascript">
        $(window).load(function () {
            $("#imgBackground").fullBg();
        });
    </script>
</body>
</html>

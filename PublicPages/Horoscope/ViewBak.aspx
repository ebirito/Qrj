<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewBak.aspx.cs" Inherits="QRJ.PublicPages.Horoscope.ViewBak" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../../Scripts/jquery-1.8.2.min.js"></script>
    <script src="../../Scripts/jquery.fullbg.min.js"></script>
    <style type="text/css">
        .fullBg {
	        position: fixed;
	        top: 0;
	        left: 0;
	        overflow: hidden;
        }
    </style>
</head>
<body>
    <div id="divUnderConstruction" runat="server">
        <h1>Under Construction...</h1>
        <div id="signText" runat="server"></div>
    </div>
    <div id="divBackgroundImage" runat="server">
        <img id="imgBackground" runat="server" alt="" />
    </div>
    <script type="text/javascript">
        $(window).load(function () {
            $("#imgBackground").fullBg();
        });
    </script>
</body>
</html>

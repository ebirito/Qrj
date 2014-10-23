<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="QRJ.PublicPages.Horoscope.View" %>

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
            z-index:100;
            position:absolute;    
            font-size:40px;
            top:450px;
            width:70%;
            left:15%
        }
        .link {
            z-index:100;
            position:absolute;    
            font-size:60px;
            top:1300px;
            left:22%
        }
        #divContainer
        {
            position:relative;
        }
    </style>
</head>
<body>
    <form runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div id="divContainer" runat="server">
            <img id="imgBackground" runat="server" alt="" />
            <div id="txtHoroscope" class="text" runat="server" />
            <a class="link" href="http://www.astrobandz.com">www.astrobandz.com</a>
        </div>
        <script type="text/javascript">
            $(window).load(function () {
                $("#imgBackground").fullBg();
            });
        </script>
    </form>
</body>
</html>

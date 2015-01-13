<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="QRJ.PublicPages.Horoscope.View" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="../../Scripts/jquery-1.8.2.min.js"></script>
    <script src="../../Scripts/jquery.fullbg.min.js"></script>
    <style type="text/css">
        .fullBg{
            background-size: cover;

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
            bottom:10px;
            left:22%;
            color:black;
            text-shadow:
            -1px -1px 0 #FFF,
            1px -1px 0 #FFF,
            -1px 1px 0 #FFF,
            1px 1px 0 #FFF;
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
            <img id="imgBackground" class="fullBg" runat="server" alt="" />
            <div id="txtHoroscope" class="text" runat="server" />
            <a id="lnkAstrobanz" runat="server" class="link" href="http://www.astrobandz.com">www.astrobandz.com</a>
        </div>
    </form>
</body>
</html>

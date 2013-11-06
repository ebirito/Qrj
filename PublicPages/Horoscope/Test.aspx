<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="QRJ.PublicPages.Horoscope.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .title {
            text-align: center;
	        font-family: "Museo";
	        font-size: 100px; text-transform: uppercase;
	        color: #fff;
	        text-shadow: 0 0 10px #fff, 0 0 20px #fff, 0 0 30px #fff, 0 0 40px #ff00de, 0 0 70px #ff00de, 0 0 80px #ff00de, 0 0 100px #ff00de, 0 0 150px #ff00de;
        }
        .text {
            text-align: center;
            color: #fff;
            text-shadow: 0px -1px 4px white, 0px -2px 10px yellow, 0px -10px 20px #ff8000, 0px -18px 40px red;
            font: 50px 'BlackJackRegular';
        }
        #bg {
          position: fixed; 
          top: -50%; 
          left: -50%; 
          width: 200%; 
          height: 200%;
          z-index: -1;
          opacity: 0.75;
        }
        #bg img {
          position: absolute; 
          top: 0; 
          left: 0; 
          right: 0; 
          bottom: 0; 
          margin: auto; 
          min-width: 50%;
          min-height: 50%;
        }
    </style>
</head>
<body>
    <div id="bg">
      <img id="imgBackground" runat="server" alt="" />
    </div>
    <div id="divSign" runat="server">
        <div id="txtSign" class="title" runat="server"></div>
        <div id="txtHoroscope" class="text" runat="server"></div>
    </div>
</body>
</html>

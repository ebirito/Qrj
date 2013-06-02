<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Watch.aspx.cs" Inherits="QRJ.Watch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <div>
        <video width="576" height="320" controls autoplay>
            <source id="videoSource" runat="server" type="video/mp4" />
        </video>
    </div>
</body>
</html>

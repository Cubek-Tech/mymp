<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="watermark.aspx.cs" Inherits="RESTFulWCFService.watermark" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:FileUpload ID="FileUpload1" runat="server" /><asp:Button ID="Button1" runat="server" Text="Button"  OnClick="Button1_Click"/>
    </div>
    </form>
</body>
</html>

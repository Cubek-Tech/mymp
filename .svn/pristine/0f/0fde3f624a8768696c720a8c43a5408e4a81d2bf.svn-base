<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="RESTFulWCFService.Admin.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  
	<title>Admin-Login | MyMassagePartner.com</title>
    <link rel="icon" type="image/png" href="../img/mp.png">
    <style type="text/css">
        .auto-style1
        {
            width: 38%;
            height: 226px;
            /*border: 1px solid grey;*/
            border-collapse:collapse;
                    padding: 6px 3px 2px 1px;
                  box-shadow:black 3px 1px 1px 1px 
        }
        .auto-style2
        {
                padding: 11px 16px 9px 62px;
            /*border: 1px solid grey;*/
            width: 617px;
        }
    </style>
</head>
<body style="background-color:white">
    <form id="form1" runat="server">
        <center>
        <table class="auto-style1" style="border:1px solid grey; background-color:white">
            <tr class="auto-style2">
                <td style="padding: 11px 16px 9px 40px;" colspan="2">ADMIN PANEL</td>
            </tr>
            <tr class="auto-style2">
                <td class="auto-style2">Login Id</td>
                <td class="auto-style2">
                    <asp:TextBox ID="txtLoginID" runat="server" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true" ControlToValidate="txtLoginID"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="auto-style2">
                <td class="auto-style2">Password</td>
                <td class="auto-style2">
                    <asp:TextBox ID="txtPassword" runat="server" Width="205px" TextMode="Password"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" SetFocusOnError="true" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="auto-style2">
                <td class="auto-style2">
                    <asp:Button ID="btnlogin" runat="server" OnClick="btnlogin_Click" Text="Login" />
                    <br />
                    <asp:Label ID="lblerror" runat="server"></asp:Label>
                </td>
                <td class="auto-style2">
                    &nbsp;</td>
            </tr>
        </table>
            </center>
        <div>
        </div>
    </form>
</body>
</html>

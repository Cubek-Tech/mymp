<%@ Page Language="C#" MasterPageFile="~/CrebasMaster.master" AutoEventWireup="true" Inherits="Paypal_Success" Codebehind="Success.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
Your subscription has been successful.
<br />
Thanks
<br />
Click here to continue your registration.
<asp:Button ID="btn_continue" runat="server" Text="Continue"/>

</asp:Content>


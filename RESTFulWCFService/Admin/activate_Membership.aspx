<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="activate_Membership.aspx.cs" Inherits="RESTFulWCFService.Admin.activate_Membership" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
           tr,td
        {
            padding-left:40px;
            padding-top:15px
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <center>  
       <div style="height:300px; width:600px; border:1px solid grey; padding:0px 0px 0px 52px; box-shadow:grey 7px 4px 4px -2px">
        <br />
           <table>
               <tr>
                   <td>
                       Enter Email Id:
                   </td>
                   <td>
                       <asp:UpdatePanel ID="UpdatePanel5" runat="server"><Triggers><asp:AsyncPostBackTrigger ControlID="btnActivate" /></Triggers><ContentTemplate>
                       <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                             </ContentTemplate></asp:UpdatePanel>
                   </td>
                   <td>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="member_valid" SetFocusOnError="true" ForeColor="Red"  ErrorMessage="Required" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                       <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="member_valid" SetFocusOnError="true" ForeColor="Red" ControlToValidate="txtEmail" ErrorMessage="Wrong format" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                   </td>
               </tr>
                <tr>
                   <td>
                       Years(For Membership):
                   </td>
                   <td>
                       <asp:UpdatePanel ID="UpdatePanel4" runat="server"><Triggers><asp:AsyncPostBackTrigger ControlID="btnActivate" /></Triggers><ContentTemplate>
                       <asp:TextBox ID="txtYear" runat="server"></asp:TextBox>
                              </ContentTemplate></asp:UpdatePanel>
                   </td>
                    <td>
                        <asp:RangeValidator ID="RangeValidator1" runat="server" ValidationGroup="member_valid" SetFocusOnError="true" ForeColor="Red" MinimumValue="1" MaximumValue="3" Type="Integer"  ErrorMessage="1 to 3" ControlToValidate="txtYear"></asp:RangeValidator>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="member_valid" SetFocusOnError="true" ForeColor="Red"  ErrorMessage="Required" ControlToValidate="txtYear"></asp:RequiredFieldValidator>
                   </td>
               </tr>
                <tr>
                   <td>
                       Payment thru:
                   </td>
                   <td>
                         <asp:UpdatePanel ID="UpdatePanel6" runat="server"><ContentTemplate>
                      <asp:DropDownList runat="server" id="ddlPayThru" CssClass="form-control">
                          <asp:ListItem Value="0">Select</asp:ListItem>
                          <asp:ListItem Value="P">Paypal</asp:ListItem>
                          <asp:ListItem Value="S">Stripe</asp:ListItem>
                          <asp:ListItem Value="O">Other</asp:ListItem>
                      </asp:DropDownList>
                        </ContentTemplate></asp:UpdatePanel>
                   </td>
                    <td>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="member_valid" SetFocusOnError="true" ForeColor="Red"  ErrorMessage="Required" InitialValue="0" ControlToValidate="ddlPayThru"></asp:RequiredFieldValidator>
                   </td>
               </tr>
               <tr>
                   <td>
                       Security Code:
                   </td>
                   <td>
                         <asp:UpdatePanel ID="UpdatePanel3" runat="server"><Triggers><asp:AsyncPostBackTrigger ControlID="btnActivate" /></Triggers><ContentTemplate>
                       <asp:TextBox ID="txtSecurity" runat="server" TextMode="Password"></asp:TextBox>
                        </ContentTemplate></asp:UpdatePanel>
                   </td>
                    <td>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="member_valid" SetFocusOnError="true" ForeColor="Red"  ErrorMessage="Required" ControlToValidate="txtSecurity"></asp:RequiredFieldValidator>
                   </td>
               </tr>
               <tr>
                   <td>
                       <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
                       <asp:Button ID="btnActivate" runat="server" Text="Activate"  ValidationGroup="member_valid" OnClick="btnActivate_Click"></asp:Button>
                           </ContentTemplate></asp:UpdatePanel>
                   </td>
                   <td colspan="2">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server"><Triggers><asp:AsyncPostBackTrigger ControlID="btnActivate" /></Triggers><ContentTemplate>
                       <asp:Label ID="lblResult" runat="server" ForeColor="Green"></asp:Label>
                             </ContentTemplate></asp:UpdatePanel>
                   </td>
               </tr>
           </table>
        </div>
        </center>
</asp:Content>

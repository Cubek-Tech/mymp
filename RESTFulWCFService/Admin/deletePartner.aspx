<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="deletePartner.aspx.cs" Inherits="RESTFulWCFService.Admin.deletePartner" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <style>
        tr,td
        {
            padding-left:40px;
            padding-top:15px
        }
        #ctl00_ContentPlaceHolder1_Calendar1
        {
            height:25px;
            width:25px
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <center>  
       <div style="height:199px; width:482px; border:1px solid grey; padding:0px 0px 0px 52px; box-shadow:grey 7px 4px 4px -2px">
        <br />
           <table>
               <tr>
                   <td>Email ID <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="chk_valid" ErrorMessage="*" InitialValue="0" ControlToValidate="txtEmail_id" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator></td></td><td>
                       <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                           <ContentTemplate>
                       <asp:TextBox ID="txtEmail_id" runat="server" CssClass="form-control"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                   </td>
               </tr>
               <tr>

                   <td colspan="2">
                      <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                           <ContentTemplate><asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Delete Partner" ValidationGroup="chk_valid" OnClick="btnDelete_Click"></asp:Button>
                                </ContentTemplate>
                       </asp:UpdatePanel>
                   </td>
               </tr>
               <tr>
                   <td colspan="2">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                           <ContentTemplate>
                       <asp:Label ID="lblResult" runat="server" ForeColor="Green"></asp:Label>
                              </ContentTemplate>
                            </asp:UpdatePanel>
                   </td>
               </tr>
               </table>
           </div>
         </center>
</asp:Content>

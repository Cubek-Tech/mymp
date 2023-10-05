<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="UpdatePTMlink.aspx.cs" Inherits="RESTFulWCFService.Admin.UpdatePTMlink" %>
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
       <div style="height:145px; width:600px; border:1px solid grey; padding:0px 0px 0px 52px; box-shadow:grey 7px 4px 4px -2px">
        <br />
           <table>
                <tr>
                   <td>
                       Enter Paytm Link:
                   </td>
                   <td>
                       <asp:UpdatePanel ID="UpdatePanel4" runat="server"><Triggers><asp:AsyncPostBackTrigger ControlID="btnUpdateLink" /></Triggers><ContentTemplate>
                       <asp:TextBox ID="txtPtm" runat="server"></asp:TextBox>
                              </ContentTemplate></asp:UpdatePanel>
                   </td>
                    <td>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="member_valid" SetFocusOnError="true" ForeColor="Red"  ErrorMessage="Required" ControlToValidate="txtPtm"></asp:RequiredFieldValidator>
                   </td>
               </tr>
               <tr>
                   <td>
                       <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
                       <asp:Button ID="btnUpdateLink" runat="server" Text="Update Link"  ValidationGroup="member_valid" OnClick="btnUpdateLink_Click"></asp:Button>
                           </ContentTemplate></asp:UpdatePanel>
                   </td>
                   <td colspan="2">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server"><Triggers><asp:AsyncPostBackTrigger ControlID="btnUpdateLink" /></Triggers><ContentTemplate>
                       <asp:Label ID="lblResult" runat="server" ForeColor="Green"></asp:Label>
                             </ContentTemplate></asp:UpdatePanel>
                   </td>
               </tr>
           </table>
        </div>
        <br />
          <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                           <ContentTemplate>
          <asp:GridView runat="server" id="grdviewPaymentUpdates" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="772px" AutoGenerateColumns="False">
              <Columns>
                  <asp:BoundField DataField="Parameter_desc" HeaderText="About" />
                  <asp:BoundField DataField="parameter_value" HeaderText="Value" />
              </Columns>
              <FooterStyle BackColor="White" ForeColor="#000066" />
              <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
              <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
              <RowStyle ForeColor="#000066" />
              <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
              <SortedAscendingCellStyle BackColor="#F1F1F1" />
              <SortedAscendingHeaderStyle BackColor="#007DBB" />
              <SortedDescendingCellStyle BackColor="#CAC9C9" />
              <SortedDescendingHeaderStyle BackColor="#00547E" />

          </asp:GridView>
                                </ContentTemplate></asp:UpdatePanel>
        </center>
</asp:Content>

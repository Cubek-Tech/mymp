<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="check_Rgist.aspx.cs" Inherits="RESTFulWCFService.Admin.check_Rgist" %>
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
       <div style="height:300px; width:600px; border:1px solid grey; padding:0px 0px 0px 52px; box-shadow:grey 7px 4px 4px -2px">
        <br />
           <table>
               <tr>
                   <td>Date</td><td>
                       <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                           <ContentTemplate>
                       <asp:TextBox ID="txtdate" runat="server" ReadOnly="true"></asp:TextBox><asp:ImageButton ID="imgcalender" runat="server" ImageUrl="Images/calendar-23684_960_720.png" Width="30px" Height="26px" Style="position:absolute" OnClick="imgcalender_Click"></asp:ImageButton>
                                </ContentTemplate>
                       </asp:UpdatePanel> 
                               </td>
               </tr>
               <tr>

                   <td>Select Country</td><td>
                       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                           <ContentTemplate>
                       <asp:DropDownList ID="ddlcountry" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged"></asp:DropDownList>
                       </ContentTemplate>
                       </asp:UpdatePanel>
                   </td>
                   <td><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="chk_valid" ErrorMessage="*" InitialValue="0" ControlToValidate="ddlcountry" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator></td>
               </tr>
               <tr>
                   <td>Select State</td><td>
                         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddlcountry" />
                             </Triggers>
                           <ContentTemplate>
                       <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></asp:DropDownList>
                                    </ContentTemplate>
                       </asp:UpdatePanel></td>
                </tr>
               <tr>
                   <td>Select City</td><td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddlcountry" />
                                 <asp:AsyncPostBackTrigger ControlID="ddlState" />
                             </Triggers>
                           <ContentTemplate><asp:DropDownList ID="ddlCity" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged"></asp:DropDownList>
                               </ContentTemplate>
                       </asp:UpdatePanel></td>
               </tr>
               <tr><td colspan="2">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                           <ContentTemplate><asp:Button ID="btnfind" runat="server" Text="Find" OnClick="btnfind_Click"></asp:Button>
                                </ContentTemplate>
                       </asp:UpdatePanel></td></tr>
               <tr>
                   <td colspan="2">
                          <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                           <ContentTemplate>
                                  <asp:Label ID="lblResult" runat="server" ForeColor="Green"></asp:Label>
                               </ContentTemplate>
                               </asp:UpdatePanel>
                   </td>
               </tr>
             </table>
        </div>
        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="imgcalender" />
            </Triggers>
            <ContentTemplate>
          <asp:Calendar ID="Calendar1" runat="server" Visible="False" Height="114px" Width="139px" OnSelectionChanged="Calendar1_SelectionChanged" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black">
                           <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                           <NextPrevStyle VerticalAlign="Bottom" />
                           <OtherMonthDayStyle ForeColor="#808080" />
                           <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                           <SelectorStyle BackColor="#CCCCCC" />
                           <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                           <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                           <WeekendDayStyle BackColor="#FFFFCC" />
                               </asp:Calendar>
         </ContentTemplate>
                   </asp:UpdatePanel>
        </center>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="Review.aspx.cs" Inherits="RESTFulWCFService.Admin.Review" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>  <div style="width:804px; height:530px; border:1px solid grey; padding:0px 0px 0px 52px; box-shadow:grey 7px 4px 4px -2px">
        <h3>Add Review:</h3>
       <%-- <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
        <table style="width:808px; height:472px">
            <tr>
                <td>Country</td>
                <td>
                     <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                          <Triggers>
                              <asp:AsyncPostBackTrigger ControlID="btnsave" />
                          </Triggers>
                        <ContentTemplate>
                    <asp:DropDownList ID="ddlcountry" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="review_valid" SetFocusOnError="true" ControlToValidate="ddlcountry" InitialValue="0"></asp:RequiredFieldValidator>
                  </ContentTemplate>
                        </asp:UpdatePanel>
                            </td>
                </tr>
            <tr>
                <td>Comment by</td>
                <td>
                     <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                          <Triggers>
                              <asp:AsyncPostBackTrigger ControlID="btnsave" />
                          </Triggers>
                        <ContentTemplate>
                    <asp:TextBox ID="txtcommentby" runat="server" Width="250"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="review_valid" SetFocusOnError="true" ControlToValidate="txtcommentby"></asp:RequiredFieldValidator>
                  </ContentTemplate>
                        </asp:UpdatePanel>
                            </td>
                </tr><tr><td>Rating</td>
                <td>
                     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                          <Triggers>
                              <asp:AsyncPostBackTrigger ControlID="btnsave" />
                          </Triggers>
                        <ContentTemplate>
                    <asp:DropDownList ID="DropDownList1" runat="server">
                    <%--<asp:ListItem>-Select Rating-</asp:ListItem>--%>
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                    </asp:DropDownList>
                              </ContentTemplate>
                        </asp:UpdatePanel></td>
                </tr><tr><td>Message</td>
                <td>
                      <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                          <Triggers>
                              <asp:AsyncPostBackTrigger ControlID="btnsave" />
                          </Triggers>
                        <ContentTemplate>
                    <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Height="238" Width="498"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="review_valid" runat="server" SetFocusOnError="true" ControlToValidate="txtMessage"></asp:RequiredFieldValidator>
               </ContentTemplate>
                        </asp:UpdatePanel>
                                  </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                    <asp:Button ID="btnsave" runat="server" Text="Button" ValidationGroup="review_valid" OnClick="btnsave_Click"></asp:Button>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                          <Triggers>
                              <asp:AsyncPostBackTrigger ControlID="btnsave" />
                          </Triggers>
                        <ContentTemplate>
                    <asp:Label ID="lblresult" runat="server"></asp:Label>
                              </ContentTemplate>
                        </asp:UpdatePanel></td>
            </tr>

        </table>
    </div></center>
</asp:Content>

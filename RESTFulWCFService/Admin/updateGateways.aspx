<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="updateGateways.aspx.cs" Inherits="RESTFulWCFService.Admin.updateGateways" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .checkbox input[type=checkbox] {
            position: absolute;
            margin-top: 4px;
            margin-left: 0px;
        }

        .font-weight {
            font-weight: 100;
            height: 30px;
            padding: 5px 10px;
            font-size: 12px;
            line-height: 1.5;
            border-radius: 3px;
        }

        .font-weight1 {
            font-weight: 100;
            height: 30px;
            padding: 5px 10px;
            font-size: 12px;
            line-height: 1.5;
            border-radius: 3px;
            margin-left: -8px;
            position: absolute;
        }

        input.invalid, textarea.invalid {
            border: 2px solid red;
        }

        input.valid, textarea.valid {
            border: 2px solid green;
        }

        label {
            display: inline-block;
            max-width: 100%;
            margin-bottom: 5px;
            font-weight: 500;
        }

        table#ctl00_ContentPlaceHolder1_chkPModes label {
            padding-left: 11px;
            padding-right: 22px;
        }

        table#ctl00_ContentPlaceHolder1_chkPModes tr td {
            border: none;
        }

        table#default_mode tr td {
            /*border: 1px solid lightgray;*/
            padding: 5px 1px 2px 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>  
       <div style="height:auto; width:600px; border:1px solid grey; padding:0px 0px 0px 52px; box-shadow:grey 7px 4px 4px -2px">
        <br />
           <table id="default_mode" style="color:red">
               <tr>
                   <td style="padding-right: 49px;">Default Payment Mode</td>
                   <td>
                         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                           <ContentTemplate>
                       <asp:CheckBoxList ID="chkPModes" runat="server" AutoPostBack="true" onchange="MakeChange(this);" OnSelectedIndexChanged="chkPModes_SelectedIndexChanged" RepeatDirection="Horizontal"></asp:CheckBoxList>
                               </ContentTemplate>
                             </asp:UpdatePanel>
                               </td>
               </tr>
           </table>
           <hr />
           <table>
               <tr>
                   <td>Select Country</td><td>
                       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                           <ContentTemplate>
                             <div style="height: 120px; overflow: auto; border: 1px solid lightgrey; color: gray; padding-left: 8px; background-color: white">
                            <asp:TextBox ID="txtSearch" runat="server" Width="14%" CssClass="font-weight1" onkeyup="SearchCountries(this,'#chkCountries');"
                                placeholder="Write Country Name">
                            </asp:TextBox>
                            <br />
                            <br />
                            <asp:CheckBoxList ID="chkCountries" runat="server"  CssClass="font-weight" ClientIDMode="Static"></asp:CheckBoxList>
                        </div>
                       </ContentTemplate>
                       </asp:UpdatePanel>
                           <asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="reg" SetFocusOnError="true" ForeColor="Red" ClientValidationFunction="chkCountries"></asp:CustomValidator>
                   </td>
               </tr>
               <tr style="padding-left:0px; padding-top:15px">
                   <td  style="padding-left:0px; padding-top:15px">Select Payment Gateway</td><td  style="padding-left:0px; padding-top:15px">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                           <ContentTemplate><asp:DropDownList ID="ddlGateways" runat="server" CssClass="form-control"></asp:DropDownList>
                               </ContentTemplate>
                       </asp:UpdatePanel></td>
               </tr>
               <tr  style="padding-left:0px; padding-top:15px"><td colspan="2"  style="padding-left:0px; padding-top:15px">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                           <ContentTemplate><asp:Button ID="btnUpdate" runat="server" Text="Update Gateway" OnClick="btnUpdate_Click" CssClass="btn btn-gradiant"></asp:Button>
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
           <br />
           <br />
           <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal" Width="276px">
               <Columns>
                   <asp:BoundField DataField="country_name" HeaderText="Country Name" SortExpression="country_name" />
                   <asp:BoundField DataField="parameter_name" HeaderText="Payment Gateway" ReadOnly="True" SortExpression="parameter_name" />
               </Columns>
               <FooterStyle BackColor="White" ForeColor="#333333" />
               <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
               <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
               <RowStyle BackColor="White" ForeColor="#333333" />
               <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
               <SortedAscendingCellStyle BackColor="#F7F7F7" />
               <SortedAscendingHeaderStyle BackColor="#487575" />
               <SortedDescendingCellStyle BackColor="#E5E5E5" />
               <SortedDescendingHeaderStyle BackColor="#275353" />
           </asp:GridView>
           <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MP_devConnectionString %>" SelectCommand="select country_name,(select parameter_name from t_parameter where parameter_sk=p_gateway)as parameter_name from t_country where p_gateway is not null"></asp:SqlDataSource>
        </div>
         </center>
    <script type="text/javascript">
        function ValidateCheckBoxList(sender, args) {
            var checkBoxList = document.getElementById("<%=chkCountries.ClientID %>");
            var checkboxes = checkBoxList.getElementsByTagName("input");
            var isValid = false;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    isValid = true;
                    break;
                }
            }
            args.IsValid = isValid;
        }
        function SearchCountries(txtSearch, cblEmployees) {
            if ($(txtSearch).val() != "") {
                var count = 0;
                $(cblEmployees).children('tbody').children('tr').each(function () {
                    var match = false;
                    $(this).children('td').children('label').each(function () {
                        if ($(this).text().toUpperCase().indexOf($(txtSearch).val().toUpperCase()) > -1)
                            match = true;
                    });
                    if (match) {
                        $(this).show();
                        count++;
                    }
                    else { $(this).hide(); }
                });
                $('#spnCount').html((count) + ' match');
            }
            else {
                $(cblEmployees).children('tbody').children('tr').each(function () {
                    $(this).show();
                });
                $('#spnCount').html('');
            }
        }
        function MakeChange(e) {

            debugger
            var a = document.getElementById(e.id).getElementsByTagName('input');
            if (a == null) { return; }
            for (var i = 0; a.length - 1; i++) {
                var b = a[i];
                if (b == null) { break; }
                if (b.type == "checkbox" && b.id != event.srcElement.id) { b.checked = false; }
            }
        }

    </script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="partner_update_percent.aspx.cs" Inherits="RESTFulWCFService.Admin.partner_update_percent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="Text/css">
        .css {
            padding-top: 62px;
            padding-left: 69px;
        }

        .css1 {
            margin-left: 25px;
        }

        .css2 {
            padding: 8px;
            height: 47px;
        }

        #myTable tr td {
            padding: 8px;
        }

        #chkCountries tr td {
            padding: 0px !important;
        }
        table#ctl00_ContentPlaceHolder1_Grd1 {
        width:40%;
        }
        table#ctl00_ContentPlaceHolder1_Grd1 tr td {
            padding: 6px;
                text-align: center;
        }
         .spinner1
        {
            left: 48%;
            color: #FFFFFF;
            position: fixed;
            top: 42%;
            z-index: 99;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:updateprogress id="UpdateProgress2" runat="server">
        <ProgressTemplate>
            <div class="overlay">
            </div>
            <div class="spinner1">
                <img src="<%# Constants__.WEB_ROOT%>/Images/ajax-loader1.gif" alt="ajax" width="30" height="30" />
            </div>
        </ProgressTemplate>
    </asp:updateprogress>
     <center>
      <div class="css">
        <table border="5" id="myTable">
            <tr class="css2">
                <td align="right">
                   Select Country :
                </td>
                <td>
               <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                           <ContentTemplate>
                             <div style="height: 120px; overflow: auto; border: 1px solid lightgrey; color: gray; background-color: white">
                            <asp:TextBox ID="txtSearch" runat="server" Width="100%" CssClass="font-weight1" onkeyup="SearchCountries(this,'#chkCountries');"
                                placeholder="Write Country Name">
                            </asp:TextBox>
                            <br />
                            <br />
                            <asp:CheckBoxList ID="chkCountries" runat="server"  CssClass="font-weight" ClientIDMode="Static" OnSelectedIndexChanged="chkCountries_SelectedIndexChanged" AutoPostBack="True"></asp:CheckBoxList>
                        </div>
                       </ContentTemplate>
                       </asp:UpdatePanel>
                           <asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="reg" SetFocusOnError="true" ForeColor="Red" ClientValidationFunction="chkCountries"></asp:CustomValidator>
                </td>
            </tr>
             <tr class="css2">
                <td align="right">
                  One Year Amount:
                </td>
                <td>
                   <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                           <ContentTemplate><asp:TextBox runat="server" id="txtOneYear" CssClass="font-weight1"></asp:TextBox>
                               </ContentTemplate>
                       </asp:UpdatePanel>
                </td>
            </tr>
            <tr class="css2">
                <td align="right">
                  Two Year Amount:
                </td>
                <td>
                   <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                           <ContentTemplate><asp:TextBox runat="server" id="txtTwoYear" CssClass="font-weight1"></asp:TextBox>
                               </ContentTemplate>
                       </asp:UpdatePanel>
                </td>
            </tr>
             <tr class="css2">
                <td align="right">
                  Three Year Amount:
                </td>
                <td>
                   <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                           <ContentTemplate><asp:TextBox runat="server" id="txtThreeYear" CssClass="font-weight1"></asp:TextBox>
                               </ContentTemplate>
                       </asp:UpdatePanel>
                </td>
            </tr>
           <tr class="css2"> 
             <td>
                 <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                           <ContentTemplate><asp:Button ID="btnUpdate" runat="server" Text="Update Membership Pricing Plan" CssClass="btn btn-gradiant" OnClick="btnUpdate_Click"></asp:Button>
                                </ContentTemplate>
                       </asp:UpdatePanel>
            </td>
               <td>
                   <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                           <ContentTemplate>
                                  <asp:Label ID="lblResult" runat="server" ForeColor="Green"></asp:Label>
                               </ContentTemplate>
                               </asp:UpdatePanel>
               </td>
            </tr>
        </table>
    </div>
         <br/>
           <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                           <ContentTemplate>
<asp:GridView runat="server" id="Grd1" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
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
                                   </ContentTemplate>
                               </asp:UpdatePanel>
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
        function MutExChkList(chk) {
            var chkList = chk.parentNode.parentNode.parentNode;
            var chks = chkList.getElementsByTagName("input");
            for (var i = 0; i < chks.length; i++) {
                if (chks[i] != chk && chk.checked) {
                    chks[i].checked = false;
                }
            }
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
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {

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
        }

    </script>
</asp:Content>

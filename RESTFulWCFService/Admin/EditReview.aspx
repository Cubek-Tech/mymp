<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="EditReview.aspx.cs" Inherits="RESTFulWCFService.Admin.EditReview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="assets/js/jquery.3.2.1.min.js"></script>
    <style>
        input[title=Message]
        {
            text-align: justify;
            width: 300px;
            height: 166px;
        }

        input,
        textarea
        {
            display: block;
            margin-bottom: 1em;
        }
    </style>
    <script>
        $(document).ready(function () {
            $('input[title=Message]').each(function () {
                var element = this;
                chalLength = element.value.length;
            if (chalLength > 10) {
                var parent = element.parentNode;
                parent.removeChild(element);
                if (element.type == 'text') {
                    parent.innerHTML = '<textarea name="' + element.name + '" cols=100 rows=5>' + element.value + '</textarea>';
                } else {
                    parent.innerHTML = '<input type="text" name="' + element.name + '"   value="' + element.value + '" />';
                }
                element = null;
            
            }
            })
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>  <div style="height:530px; border:1px solid grey; padding:0px 0px 0px 52px; box-shadow:grey 7px 4px 4px -2px">

        <br />
         <table>
             <tr>
                 <td><asp:DropDownList ID="ddlcountry" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged"></asp:DropDownList></td>
             </tr>
         </table>
         <br />
         <br />
         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="review_sk" DataSourceID="SqlDataSource1" PageSize="5" AllowPaging="True" AllowSorting="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="1143px">
             <Columns>
                 <asp:CommandField ShowEditButton="True" />
                 <asp:BoundField DataField="review_sk" HeaderText="review_sk" InsertVisible="False" ReadOnly="True" SortExpression="review_sk" />
                 <asp:BoundField DataField="country_sk" HeaderText="country_sk"  SortExpression ="country_sk" />
                 <asp:BoundField DataField="name" HeaderText="Review by"  SortExpression="name" />
                 <asp:BoundField DataField="rating" HeaderText="Rating" SortExpression="rating" />
                 <asp:BoundField DataField="review" HeaderText="Message" SortExpression="review" />
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
         <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Crebas %>" DeleteCommand="DELETE FROM [t_massage_partner_reviews] WHERE [review_sk] = @review_sk" InsertCommand="INSERT INTO [t_massage_partner_reviews] ([country_sk], [name], [rating], [review]) VALUES (@country_sk, @name, @rating, @review)" SelectCommand="SELECT * FROM [t_massage_partner_reviews] WHERE ([country_sk] = @country_sk)" UpdateCommand="UPDATE [t_massage_partner_reviews] SET [country_sk] = @country_sk, [name] = @name, [rating] = @rating, [review] = @review WHERE [review_sk] = @review_sk">
             <DeleteParameters>
                 <asp:Parameter Name="review_sk" Type="Int16" />
             </DeleteParameters>
             <InsertParameters>
                 <asp:Parameter Name="country_sk" Type="Int16" />
                 <asp:Parameter Name="name" Type="String" />
                 <asp:Parameter Name="rating" Type="Int16" />
                 <asp:Parameter Name="review" Type="String" />
             </InsertParameters>
             <SelectParameters>
                 <asp:ControlParameter ControlID="ddlcountry" DefaultValue="null" Name="country_sk" PropertyName="SelectedValue" Type="Int16" />
             </SelectParameters>
             <UpdateParameters>
                 <asp:Parameter Name="country_sk" Type="Int16" />
                 <asp:Parameter Name="name" Type="String" />
                 <asp:Parameter Name="rating" Type="Int16" />
                 <asp:Parameter Name="review" Type="String" />
                 <asp:Parameter Name="review_sk" Type="Int16" />
             </UpdateParameters>
         </asp:SqlDataSource>
         </div>
         </center>

</asp:Content>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControl1.ascx.cs" Inherits="RESTFulWCFService.MassagePartener.user_control.WebUserControl1" %>
<style>
    .lbl
    {    padding: 0px 100px 0px 100px;
    /*border: 1px solid black;*/
    }
    @media only screen and (max-width: 786px) {
 
    .pagination .lbl{
        padding: 0px 10px 0px 10px;
    }
}
</style>
<asp:HiddenField ID="hdpage" runat="server" />
<asp:HiddenField ID="hdntotalpages" runat="server" />
<table>
    <tr>
        <td>
            <asp:Button ID="btnFirstRecord" runat="server" CssClass="btn btn-gradiant" Text="&lt;&lt;" OnClick="btnFirstRecord_Click" />
        </td>
        <td>
            <asp:Button ID="btnPrevious" runat="server" CssClass="btn btn-gradiant" OnClick="btnPrevious_Click" Text="&lt;" />
        </td>
        <td>
            <asp:Label ID="lblCurrentPage" runat="server" Text="Label" CssClass="lbl"></asp:Label>
        </td>
        <td>
            <asp:Button ID="btnNext" runat="server" CssClass="btn btn-gradiant" OnClick="btnNext_Click" Text="&gt;" />
        </td>
        <td>
            <asp:Button ID="btnLastRecord" runat="server" Text="&gt;&gt;" CssClass="btn btn-gradiant" OnClick="btnLastRecord_Click" />
        </td>
    </tr>
</table>
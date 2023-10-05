<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="subscriptionCurrency.aspx.cs" Inherits="RESTFulWCFService.Admin.subscriptionCurrency" %>
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
           <table id="default_mode">
                <tr class="css2">
                <td align="right">
                   Select Country :
                </td>
                <td>
                    <asp:DropDownList runat="server" id="ddlCountry_seeker" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_seeker_SelectedIndexChanged"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="ddlCountry_seeker" initialvalue="0" ValidationGroup="membership"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="css2">
                <td align="right">
                  Basic Unit Price:
                </td>
                <td>
                    <asp:TextBox id="txtBasicUnit_Seeker" runat="server"  CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtBasicUnit_Seeker"  ValidationGroup="membership"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Not Valid Price(&lt;=3)" ControlToValidate="txtBasicUnit_Seeker"  ValidationGroup="membership" MaximumValue="1000000"  MinimumValue="0" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
                </td>
            </tr>
             <tr class="css2">
                <td align="right">
                Conversion Rate(to USD):
                </td>
                <td>
                    <asp:TextBox id="txtRateSeeker" runat="server"  CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="txtRateSeeker"  ValidationGroup="membership"></asp:RequiredFieldValidator>

                </td>
            </tr>
                <tr class="css2">
                <td align="right">
             Security Password:
                </td>
                <td>
                    <asp:TextBox id="txtSecuritySeeker" runat="server"  CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="txtSecuritySeeker"  ValidationGroup="membership"></asp:RequiredFieldValidator>

                </td>
            </tr>
           <tr class="css2"> 
             <td>
                <asp:Button ID="btnAssignSubscription_seeker" runat="server" Text="Assign Subscription" CssClass="btn btn-danger"  ValidationGroup="membership" OnClick="btnAssignSubscription_seeker_Click"/>
            </td>
               <td>
                   <asp:Label runat="server" ID="Label1" Text=""></asp:Label>
               </td>
            </tr>
             </table>
           </center>
</asp:Content>

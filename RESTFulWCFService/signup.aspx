﻿<%@ Page Title="" Language="C#" MasterPageFile="~/massagepartener.Master" AutoEventWireup="true" CodeBehind="signup.aspx.cs" Inherits="RESTFulWCFService.signup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Body Massage - Find your body massage partner nearby | My Massage Partner</title>

    <meta name="Description" content="Massage Partner feature providing you a platform where you can find massage partner, meet, contact, and chat in regards of body massage. On the basis of your interest and desires you can easily start body massage sessions with your selected partner in home and hotel.">
    <meta name="Keywords" content="body massage nearby | erotic massage near me | happy ending massage near me | need body massage | full body massage | female to male Massage | sensual Massage | body massage at home | body massage in hotel | want body massage | massage body pain | massage near me | female massager near me | free body massage | male to female body massage | cheap massage near me">
    <style>
        .checkbox input[type=checkbox] {
            position: absolute;
            margin-top: 4px;
            /*margin-left: 0px;*/
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
    </style>
    <asp:PlaceHolder ID="PlaceHolder2" runat="server">
    <script src="js/Validate.js"></script>
    <script type="text/javascript">
        function SearchEmployees(txtSearch, cblEmployees) {
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
        function validatenumerics(key) {
            //getting key code of pressed key
            var keycode = (key.which) ? key.which : key.keyCode;
            //comparing pressed keycodes
            if (keycode > 31 && (keycode < 48 || keycode > 57)) {
                return false;
            }
            else {
                return true;
            }


        }
       

    </script>

    <script src='https://www.google.com/recaptcha/api.js'></script>
    <style type="text/css">
        .sign-up-form {
            padding: 20px;
            background-color: #eee;
            margin: 20px 0px;
        }
    </style>
         </asp:PlaceHolder>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
        <ProgressTemplate>
            <div class="overlay">
            </div>
            <div class="UpdateProgress_style">
                <img src="<%=Constants__.WEB_ROOT%>/Images/ajax-loader1.gif" alt="ajax" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="container">
        <h4 class="text-center title ">New User? Register Now!</h4>
        <div class="sign-up-form">
            <div class="row">
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="control-label">Name:</label>
                        <asp:TextBox ID="txtname" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="reg" ForeColor="Red" ControlToValidate="txtname" SetFocusOnError="true"></asp:RequiredFieldValidator>

                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="control-label">Email Id:</label>
                        <asp:TextBox ID="txtEmailID" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters, Custom"
                         ValidChars="_-.@" TargetControlID="txtEmailID" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="reg" ForeColor="Red" ControlToValidate="txtEmailID" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEmailID" ValidationGroup="reg" ForeColor="Red" SetFocusOnError="true" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="control-label">Password:</label>
                        <asp:TextBox ID="txtPassword1" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="reg" ForeColor="Red" ControlToValidate="txtPassword1" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="control-label">Gender:</label><br>
                        <div class=" form-inline">
                            <asp:DropDownList ID="ddlgender" runat="server" CssClass="form-control">
                                <asp:ListItem Value="M" Selected>Male</asp:ListItem>
                                <asp:ListItem Value="F">Female</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

            </div>
            <div class="row">
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="control-label">Date Of Birth</label>
                        <div class=" form-inline">
                            <asp:DropDownList ID="ddlmonth" CssClass="form-control" runat="server">
                                <asp:ListItem Text="Month"></asp:ListItem>
                                <asp:ListItem Text="Jan"></asp:ListItem>
                                <asp:ListItem Text="Feb"></asp:ListItem>
                                <asp:ListItem Text="Mar"></asp:ListItem>
                                <asp:ListItem Text="Apr"></asp:ListItem>
                                <asp:ListItem Text="May"></asp:ListItem>
                                <asp:ListItem Text="Jun"></asp:ListItem>
                                <asp:ListItem Text="Jul"></asp:ListItem>
                                <asp:ListItem Text="Aug"></asp:ListItem>
                                <asp:ListItem Text="Sep"></asp:ListItem>
                                <asp:ListItem Text="Oct"></asp:ListItem>
                                <asp:ListItem Text="Nov"></asp:ListItem>
                                <asp:ListItem Text="Dec"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlday" CssClass="form-control db" runat="server">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlyear" CssClass="form-control db" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="reg" ForeColor="Red" ControlToValidate="ddlday" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="reg" ForeColor="Red" ControlToValidate="ddlmonth" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="reg" ForeColor="Red" ControlToValidate="ddlyear" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="control-label">Contact No.:</label>
                        <asp:TextBox ID="txtcontact" runat="server" CssClass="form-control" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');"></asp:TextBox>
           <%--             <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" CssClass="invalid" ValidationGroup="reg" ForeColor="Red" ControlToValidate="txtcontact" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="control-label">Type(s) of Massage You Like<asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="reg" SetFocusOnError="true" ErrorMessage="*" ForeColor="Red" ClientValidationFunction="ValidateCheckBoxList"></asp:CustomValidator></label>
                        <div style="height: 120px; overflow: auto; border: 1px solid lightgrey; color: gray; padding-left: 8px; background-color: white">
                            <asp:TextBox ID="txtSearch" runat="server" Width="92%" CssClass="font-weight1" onkeyup="SearchEmployees(this,'#ctl00_ContentPlaceHolder1_ddlmassagetype');"
                                placeholder="Write Massage Type">
                            </asp:TextBox>
                            <br />
                            <br />
                            <asp:CheckBoxList ID="ddlmassagetype" runat="server" CssClass="font-weight"></asp:CheckBoxList>
                        </div>
                        

                    </div>
                    <script type="text/javascript">
                        function ValidateCheckBoxList(sender, args) {
                            var checkBoxList = document.getElementById("<%=ddlmassagetype.ClientID %>");
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
                    </script>
                </div>
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="control-label">Partner's Gender</label>
                        <asp:DropDownList ID="ddlpartgen" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                            <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                            <asp:ListItem Text="Any" Value="O" Selected></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="control-label">Partner's Age</label>
                        <asp:DropDownList ID="ddlpartage" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="reg" ForeColor="Red" ControlToValidate="ddlpartage" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="control-label">Country</label>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlcountry" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlcountry" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlcountry" SetFocusOnError="true" ForeColor="Red" InitialValue="0" ValidationGroup="reg"></asp:RequiredFieldValidator>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="control-label">State</label>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlcountry" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlstate" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged"></asp:DropDownList>
                              <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlstate" SetFocusOnError="true" ForeColor="Red" InitialValue="0" ValidationGroup="reg"></asp:RequiredFieldValidator>--%>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="control-label">City</label>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlcountry" />
                                <asp:AsyncPostBackTrigger ControlID="ddlstate" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlcity" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlcity_SelectedIndexChanged"></asp:DropDownList>
                               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlcity" SetFocusOnError="true" ForeColor="Red" InitialValue="0" ValidationGroup="reg"></asp:RequiredFieldValidator>--%>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="control-label">Area</label>
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlcountry" />
                                <asp:AsyncPostBackTrigger ControlID="ddlstate" />
                                <asp:AsyncPostBackTrigger ControlID="ddlcity" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlArea" runat="server" CssClass="form-control"></asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                </div>
                <div class="col-sm-6" style="display:none">
                    <div class="form-group">
                        <label class="control-label">Zip/Post Code</label>
                        <asp:TextBox ID="txtzip" runat="server" CssClass="form-control" onkeypress="return validatenumerics(event);"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <label class="control-label">I am a</label>
                        <asp:DropDownList ID="ddlIam" runat="server" CssClass="form-control">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                            <asp:ListItem Value="T">Massage Therapist / Professional</asp:ListItem>
                            <asp:ListItem Value="M">Manual Massager / Enthusiast</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlIam" SetFocusOnError="true" ForeColor="Red" InitialValue="0" ValidationGroup="reg"></asp:RequiredFieldValidator>

                    </div>
                    <div class="form-group ">

                        <asp:CheckBox ID="CheckBox1" runat="server" Checked="true" />
                        I confirm that I have read and accept MyMassagePartner.com's <a href="<%# Constants__.WEB_ROOT%>/terms" target="_blank">Terms & Condition</a>

                    </div>
                    <div class="form-group">
                        <label class="control-label">Captcha</label>
                        <div class="g-recaptcha" data-sitekey="6Ld-6TcUAAAAAE_AyQDx38X4ciOetJCal-Pppc-6"></div>
                    </div>



                    <div class="form-group ">

                        <asp:Button ID="btnsave" runat="server" OnClick="btnsave_Click" ValidationGroup="reg" CssClass="btn btn-gradiant" Text="Save" />

                    </div>
                    <div class="form-group">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblmsg" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnsave" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>






        </div>
    </div>

    <script>
        function forgot_close_button() {
            $('#ctl00_ContentPlaceHolder1_txtforgotemail').val('');
            $('#ctl00_ContentPlaceHolder1_lblreportmsg').hide();
            document.getElementById('ctl00_ContentPlaceHolder1_btnforgot').disabled = '';
            document.getElementById("ctl00_ContentPlaceHolder1_txtforgotemail").disabled = '';
        }
    </script>
    </asp:PlaceHolder>
</asp:Content>

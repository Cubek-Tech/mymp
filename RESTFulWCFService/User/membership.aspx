<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="membership.aspx.cs" Inherits="RESTFulWCFService.User.membership" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Fup" %>
<%@ Register Src="~/user control/WebUserControl1.ascx" TagName="UCPager" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MyMassagePartner | Membership</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <style>
        #main_popup1
        {
            border-radius: 5px 5px 5px 5px;
            height: auto;
            width: 100%;
            margin: 35px auto auto;
            max-width: 100% !important;
        }

        .welcome_text_signuppage123
        {
            background-color: #FFF;
            border-radius: 5px 5px 5px 5px;
            color: #C33;
            font-family: inherit MS;
            font-size: 18px;
            font-weight: 700;
            height: 11px;
            margin-top: 37px;
            text-align: center;
            width: 344px;
            padding-bottom: 22px;
        }

        .showno
        {
            display: inline-block;
            padding: 1px 6px;
            margin-bottom: 0;
            font-size: 12px;
            font-weight: 100;
            line-height: 1.42857143;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            -ms-touch-action: manipulation;
            touch-action: manipulation;
            cursor: pointer;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            background-image: none;
            border: 1px solid transparent;
            border-radius: 4px;
        }

        .box_Login2, .box_Login_wellcome_seek
        {
            color: #888;
            height: 425px;
            left: 100%;
            position: fixed;
            display: none;
            top: 1%;
            width: 730px;
            max-width: 100% !important;
            z-index: 1001;
            border-radius: 10px;
            -moz-border-radius: 10px;
            opacity: 6;
        }

        .welcome_signup_page
        {
            background-color: #FFF;
            border-radius: 10px 10px 10px 10px;
            float: right;
            height: 164px;
            margin-right: 0;
            width: 424px;
            max-width: 100% !important;
            border: 12px solid #F0E68C;
        }

        .content_signup
        {
            background-color: #FFF;
            font-family: Arial,Helvetica,sans-serif;
            height: 111px;
            line-height: 18px;
            margin-left: 2px;
            margin-top: 20px;
            text-align: center;
            width: 400px;
            max-width: 100% !important;
            padding: 2px;
        }

        a.boxcanceel
        {
            background: url(/Images/canceel.png) no-repeat scroll left top transparent;
            cursor: pointer;
            float: right;
            height: 22px;
            left: 0;
            position: relative;
            top: 0px;
            width: 22px;
        }

        .UpdateProgress_style
        {
            color: #FFFFFF;
            left: 72%;
            margin-left: -330px;
            position: fixed;
            top: 42%;
            z-index: 9999;
        }

        .change_pasd124
        {
            height: auto !important;
            font-size: 12px;
            width: auto !important;
            overflow-y: scroll;
            overflow-x: hidden;
            max-height: 450px;
        }

        .yearcheckdiv
        {
            width: 33%;
            position: relative;
            float: left;
            text-align: center;
            align-items: center;
        }

        .search_text__main566
        {
            padding: 3px 10px;
            height: 27px;
            margin-bottom: 5px;
        }
    </style>
    <link id="fevicon_icon" rel="shortcut icon" href="../img/mp.png" type="image/x-icon"
        runat="server" />
    <%--Other Payment Option css--%>
    <style>
        .theme-color
        {
            color: #fd0014;
        }

        .red-color
        {
            color: #ff0000;
        }

        .payment-page .member-ship li
        {
            padding: 10px 20px;
        }

        .payment-page .ptm-img
        {
            max-height: 300px;
            border: 2px solid #ccc;
        }

        .form-box
        {
            max-width: 300px;
            border: 1px solid #ccc;
            padding: 20px;
            background-color: #ddd;
            margin: 20px 10px;
        }

        .overlay
        {
            background: #000;
            bottom: 0;
            left: 0;
            position: fixed;
            right: 0;
            top: 0;
            z-index: 1000;
            opacity: 0.5;
            display: block;
        }

        .UpdateProgress_style
        {
            color: #FFFFFF;
            left: 72%;
            margin-left: -330px;
            position: fixed;
            top: 42%;
            z-index: 9999;
        }

        .form-box .theme-btn
        {
            background: #cb202d !important;
            color: #fff;
            border: 1px solid #cb202d;
        }
    </style>
    <script src="../js/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script>
        function validateEmail(email) {
            var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            return re.test(email);
        }
        function call_request_mail() {
            var val_id = $('#txttransaction').val();
            var val_id1 = $('#txtregisteredmail_id').val();
            if (val_id != '' && val_id1 != '') {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "User/service_subscription.aspx/Send_Request_mail",
                    dataType: "json",
                    data: "{'id':'" + $('#txtregisteredmail_id').val() + "','trans_no':'" + $('#txttransaction').val() + "','sk':'" + $('#partner_sk').val() + "'}",
                    success: function () {

                        $('#txttransaction').val('');
                        document.getElementById('divResult').style.color = 'Green';
                        $("#divResult").html("Details sent successfully. Your membership will be activated in next moment.");
                    },
                    error: function (e) {
                        alert(e.messageText);
                        document.getElementById('divResult').style.color = 'Red';
                        $("#divResult").html("something Wrong!");
                    }
                });
            }
            else {
                $('#txttransaction').focus();
            }
        }
        function required_mail_id() {
            var val_id = $('#txtregisteredmail_id').val();
            if (val_id == '') {
                $('#txtregisteredmail_id').focus();
            }
            else if (validateEmail(val_id))
            { }
            else { $('#txtregisteredmail_id').focus(); }
        }
    </script>
    <%--End Other Payment Option css--%>
</head>
<body>
    <form id="form1" runat="server">
        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
            <ProgressTemplate>
                <div class="overlay">
                </div>
                <div class="UpdateProgress_style">
                    <img src="<%#Constants__.WEB_ROOT%>/Images/ajax-loader1.gif" alt="ajax" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div class="container payment-page">
            <div class="row">
                <div class="col-sm-12">
                    <input type="hidden" runat="server" name="partner_sk" id="partner_sk" />
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <a class="boxclose" style="top: 0" onclick="closeDialogPaypal('popuppaypal');"></a>
                    <div class="head_up_ChangeEmailId head_up_ChangeEmailId122" style="width: 99% !important;">
                        <h3 class="theme-color">MyMassagePartner Membership </h3>
                    </div>
                    <div id="Div5" runat="server" style="padding-top: 22px; text-align: center; margin-left: 10px; width: 98%;">
                        <div style="width: 100%; font-size: 14px; font-weight: bolder;">
                            MyMassagePartner's <b>
                                <lable runat="server" id="lblyearsub" class="lblyearsub">1 Year </lable>
                            </b>Membership In Just <b>
                                <lable runat="server" id="price" class="price_pr"></lable>
                            </b>
                        </div>
                        <br>
                        <br>
                        <div style="width: 100%; font-size: 14px; font-weight: bolder;">
                            <div class='yearcheckdiv'>
                                <p class="">
                                    1 Year
                            <asp:CheckBox CssClass="checkboxoucallCM_provider" runat="server" ID="checkboxOneYear_provider"
                                Checked="True" Text="" />
                                </p>
                            </div>
                            <div class='yearcheckdiv'>
                                <p class="">
                                    2 Year
                            <asp:CheckBox CssClass="checkboxoucallCM_provider" runat="server" ID="checkboxTwoYear_provider"
                                Checked="false" Text="" />
                                </p>
                            </div>
                            <div class='yearcheckdiv'>
                                <p class="">
                                    3 Year
                            <asp:CheckBox CssClass="checkboxoucallCM_provider" runat="server" ID="checkboxThreeYear_provider"
                                Checked="false" Text="" />
                                </p>
                            </div>
                        </div>
                        <br>
                        <br>
                        <br>
                        <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btnPayNow" CssClass="btn345 btn-lg btn-info fontsize_25px" OnClick="paypal_Click"
                                    Style="margin-right: 15px; padding: 7px 13px; margin-right: 15px;" runat="server"
                                    Text="Pay Now" CausesValidation="false" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <script type="text/javascript">
                            $(".checkboxoucallCM_provider input:checkbox").live('change', function () {
                                var group = ".checkboxoucallCM_provider< :checkbox";
                                if ($(this).is(':checked')) {
                                    $(group).not($(this)).attr("checked", false);

                                    $('.price_pr').text($(this).next().text());

                                    if ($("#checkboxOneYear_provider").is(':checked'))
                                    { $('.lblyearsub').text("1 Year"); }
                                    else if ($("#checkboxTwoYear_provider").is(':checked'))
                                    { $('.lblyearsub').text("2 Year"); }
                                    else if ($("#checkboxThreeYear_provider").is(':checked'))
                                    { $('.lblyearsub').text("3 Year"); } else
                                    { $('.lblyearsub').text("1 Year"); }

                                }

                            }); </script>
                    </div>
                    <div id="divpaypalccavanue_1" runat="server" style="width: 700px; margin-top: 25px;">
                        <table width="99%" align="center">
                            <tr>
                                <td>
                                    <a style="" id="Tab_1" class="Btn Clicked">Pay via Credit Card
                                <p class="smallhinttext" style="color: white;">
                                    temp
                                </p>
                                    </a><a style="" id="Tab_2" class="Initial">Pay via Debit Card
                                <p class="smallhinttext" style="color: white;">
                                    temp
                                </p>
                                    </a><a id="Tab_3" class="Initial">Pay via Internet Banking
                                <p class="smallhinttext" style="color: white;">
                                    (Except ICICI Bank)
                                </p>
                                    </a><a style="" id="Tab_4" class="Initial">Pay via Other Methods
                                <p class="smallhinttext">
                                    (Wallet, Cash Card, UPI, Mobile Payment)
                                </p>
                                    </a>
                                    <div id="MainView" class="MainViewcss">
                                        <div id="View_1">
                                            <table class="tabsdiv">
                                                <tr>
                                                    <td style="height: 50px; vertical-align: TOP;">
                                                        <asp:DropDownList ID="ddlCreditCartBankNames" runat="server" TabIndex="5" CssClass="Dropdown_main Dropdown_sub_width">
                                                        </asp:DropDownList>
                                                        <asp:UpdatePanel runat="server" ID="UpdatePanel25">
                                                            <ContentTemplate>
                                                                <asp:Label ID="lblddlCreditCartBankNames" class="ErrorMsg" runat="server"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                    <td style="height: 50px; vertical-align: TOP;">
                                                        <asp:DropDownList ID="ddlCreditCartTypes" runat="server" TabIndex="5" CssClass="Dropdown_main Dropdown_sub_width">
                                                        </asp:DropDownList>
                                                        <asp:UpdatePanel runat="server" ID="UpdatePanel26">
                                                            <ContentTemplate>
                                                                <asp:Label ID="lblddlCreditCartTypes" class="ErrorMsg" runat="server"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                    <td style="height: 50px; vertical-align: TOP;">
                                                        <asp:UpdatePanel runat="server" ID="UpdatePanel30">
                                                            <ContentTemplate>
                                                                <asp:Button ID="PaymentCreditcard" CssClass="btn345 btn-lg btn-info fontsize_25px"
                                                                    Style="font-size: 18px !important; margin-right: 15px; height: 43px;" OnClick="CreditCard_Payment_Click"
                                                                    runat="server" Text="Go to Payment Screen" CausesValidation="false" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div id="View_2" style="display: none;">
                                            <table class="tabsdiv">
                                                <tr>
                                                    <td style="height: 50px; vertical-align: TOP;">
                                                        <asp:DropDownList ID="ddlDebitCartBankNames" runat="server" TabIndex="5" CssClass="Dropdown_main Dropdown_sub_width">
                                                        </asp:DropDownList>
                                                        <asp:UpdatePanel runat="server" ID="UpdatePanel27">
                                                            <ContentTemplate>
                                                                <asp:Label ID="lblddlDebitCartBankNames" class="ErrorMsg" runat="server"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                    <td style="height: 50px; vertical-align: TOP;">
                                                        <asp:DropDownList ID="ddlDebitCartTypes" runat="server" TabIndex="5" CssClass="Dropdown_main Dropdown_sub_width">
                                                        </asp:DropDownList>
                                                        <asp:UpdatePanel runat="server" ID="UpdatePanel28">
                                                            <ContentTemplate>
                                                                <asp:Label ID="lblddlDebitCartTypes" class="ErrorMsg" runat="server"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                    <td style="height: 50px; vertical-align: TOP;">
                                                        <asp:UpdatePanel runat="server" ID="UpdatePanel29">
                                                            <ContentTemplate>
                                                                <asp:Button ID="PaymentDebitcard" CssClass="btn345 btn-lg btn-info fontsize_25px"
                                                                    Style="font-size: 18px !important; margin-right: 15px; height: 43px;" OnClick="DebitCard_Payment_Click"
                                                                    runat="server" Text="Go to Payment Screen" CausesValidation="false" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div id="View_3" style="display: none;">
                                            <table class="tabsdiv">
                                                <tr>
                                                    <td>
                                                        <div style="margin-left: 34%; margin-top: 0px;">
                                                            <asp:UpdatePanel runat="server" ID="UpdatePanel31">
                                                                <ContentTemplate>
                                                                    <asp:Button ID="PaymentNetbanking" CssClass="btn345 btn-lg btn-info fontsize_25px"
                                                                        Style="font-size: 18px !important; margin-right: 15px; height: 43px;" OnClick="paypal_Click_CCAVANUE_Netbanking"
                                                                        runat="server" Text="Go to Payment Screen" CausesValidation="false" />
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div id="View_4" style="display: none;">
                                            <table class="tabsdiv">
                                                <tr>
                                                    <td>
                                                        <div style="margin-left: 34%; margin-top: 0px;">
                                                            <asp:UpdatePanel runat="server" ID="UpdatePanel33">
                                                                <ContentTemplate>
                                                                    <asp:Button ID="PaymentOtherPayment" CssClass="btn345 btn-lg btn-info fontsize_25px"
                                                                        Style="font-size: 18px !important; margin-right: 15px; height: 43px;" OnClick="paypal_Click_CCAVANUE_OtherPayment"
                                                                        runat="server" Text="Go to Payment Screen" CausesValidation="false" />
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div id="divpaypalStripe_1" runat="server" style="width: 100%; margin-left: 10px;">
                        <div class="stripe-logo-phone">

                            <%-- <center><img src="<%# Constants__.WEB_ROOT_CDN%>/image/Stripelogo.png" style="width: 150px;" /></center>--%>
                            <br />
                        </div>
                        <table width="68%" align="center">

                            <tr>
                                <td style="width: 222px;">Card Name</td>
                                <td style="width: 88px;">
                                    <asp:TextBox runat="server" MaxLength="20" CssClass="search_text__main566 form-control"
                                        TabIndex="2" ID="txtCardName_1" placeholder="">
                                    </asp:TextBox>
                                </td>
                                <td style="width: 134px;">
                                    <asp:RequiredFieldValidator CssClass="ErrorMsg" ID="RequiredFieldValidator10_1" runat="server"
                                        ValidationGroup="regStripe_1" ControlToValidate="txtCardName_1" ErrorMessage="Required field"
                                        Display="Dynamic" />
                                </td>
                                <td rowspan="6" style="vertical-align: top;" class="stripe-logo">
                                    <img src="<%# Constants__.WEB_ROOT_CDN%>/image/Stripelogo.png" style="width: 150px;" />
                                </td>
                            </tr>


                            <tr>
                                <td>Card Number</td>
                                <td>
                                    <asp:TextBox runat="server" MaxLength="20" CssClass="search_text__main566 form-control"
                                        TabIndex="2" ID="txtCardNumber_1" placeholder="XXXXXXXXXXXXXXXX">
                                    </asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsg" ID="RequiredFieldValidator12_1" runat="server"
                                        ValidationGroup="regStripe_1" ControlToValidate="txtCardNumber_1" ErrorMessage="Required field"
                                        Display="Dynamic" />
                                    <Fup:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtCardNumber" TargetControlID="txtCardNumber_1"
                                        FilterMode="ValidChars" runat="server" FilterType="Numbers"></Fup:FilteredTextBoxExtender>

                                </td>
                            </tr>

                            <tr>
                                <td>Card Exp. Month</td>
                                <td>
                                    <asp:TextBox runat="server" MaxLength="20" CssClass="search_text__main566 form-control"
                                        TabIndex="2" ID="txtCardExpirationMonth_1" placeholder="MM">
                                    </asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsg" ID="RequiredFieldValidator13_1" runat="server"
                                        ValidationGroup="regStripe_1" ControlToValidate="txtCardExpirationMonth_1" ErrorMessage="Required field"
                                        Display="Dynamic" />
                                    <Fup:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtCardExpirationMonth_1"
                                        FilterMode="ValidChars" runat="server" FilterType="Numbers"></Fup:FilteredTextBoxExtender>
                                </td>
                            </tr>

                            <tr>
                                <td>Card Exp. Year</td>
                                <td>
                                    <asp:TextBox runat="server" MaxLength="20" CssClass="search_text__main566 form-control "
                                        TabIndex="2" ID="txtCardExpirationYear_1" placeholder="YYYY">
                                    </asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsg" ID="RequiredFieldValidator14_1" runat="server"
                                        ValidationGroup="regStripe_1" ControlToValidate="txtCardExpirationYear_1" ErrorMessage="Required field" />
                                    <Fup:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtCardExpirationYear_1"
                                        FilterMode="ValidChars" runat="server" FilterType="Numbers"></Fup:FilteredTextBoxExtender>
                                </td>
                            </tr>

                            <tr>
                                <td></td>
                                <td>
                                    <%--   <asp:UpdatePanel runat="server" ID="UpdatePanel17">
                                <ContentTemplate>
                                    <asp:Button runat="server" ID="SubmitStripe" Style="margin-right: 15px; padding: 7px 13px;"
                                        CssClass="btn345 btn-lg btn-info fontsize_25px" Text="Pay Now" OnClick="SubmitStripe_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>--%>


                                    <asp:UpdatePanel runat="server" ID="UpdatePanel34">
                                        <ContentTemplate>
                                            <asp:Button ID="SubmitStripe_1" CssClass="btn345 btn-lg btn-info fontsize_25px"
                                                Style="font-size: 18px !important; margin-right: 15px; height: 43px;" OnClick="SubmitStripe_1_Click"
                                                runat="server" Text="Pay Now" ValidationGroup="regStripe_1" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td></td>
                            </tr>


                            <tr>
                                <td></td>
                                <td>

                                    <asp:UpdatePanel runat="server" ID="UpdatePanel35">
                                        <ContentTemplate>
                                            <asp:Label ID="lblmassage_1" runat="server" Text=""></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>


                                </td>
                                <td></td>
                            </tr>

                        </table>
                    </div>

                    <div style="padding: 15px;">
                        Benefits of <b>MyMassagePartner's</b> Membership.
                <ul style="margin-left: -22px">
                    <li>You will be able to chat with female and male body massage partner.</li>
                    <li>You will be able to see contact details of female or male body massage partner.</li>
                    <li>You will find best female and male massage partner nearby you.</li>
                    <li>You will get support from MyMassagePartner team members i.e. Rose, Jasmine, and others 24/7.</li>
                    <li>You will get the female and male body massage partner details in premium way.</li>
                    <li>You can find free body massage in your location.</li>
                </ul>
                    </div>
                    <style type="text/css">
                        .search_text__main566
                        {
                            color: #424449;
                            float: left;
                            font-size: 12px;
                            width: 141px;
                            resize: none;
                        }

                        .change_pasd123
                        {
                            height: auto !important;
                            font-size: 12px;
                            width: auto !important;
                        }

                        .Initial
                        {
                            display: block;
                            float: left;
                            background: white;
                            color: Black;
                            font-weight: bold;
                            border: none;
                            cursor: pointer;
                            font-size: 13px;
                            padding: 2px 12px;
                        }

                            .Initial:hover
                            {
                                color: black;
                                background: white;
                                border: none;
                            }

                        .Clicked
                        {
                            float: left;
                            display: block;
                            background: white;
                            padding: 4px 10px 4px 10px;
                            color: Black;
                            font-weight: bold;
                            color: #555;
                            cursor: default;
                            border: 1px solid #F4F1F1;
                            border-bottom-color: transparent;
                        }

                        .Btn
                        {
                            margin-right: 2px;
                            line-height: 1.428571429;
                            border-radius: 10px 10px 0 0;
                            position: relative;
                            display: block;
                            padding: 2px 13px;
                            border: 4px solid #c1c1c1;
                            border-bottom: transparent;
                            padding-bottom: -1px;
                            margin-bottom: -4px;
                            font-size: 12px;
                        }

                        .tabsdiv
                        {
                            width: 100%;
                            border: none;
                            border-top: 4px solid #c1c1c1;
                            padding-top: 25px;
                            padding-bottom: 38px;
                            border-bottom: 2px solid #bf7c07;
                        }

                        .MainViewcss
                        {
                            width: 100%;
                        }

                        .smallhinttext
                        {
                            font-size: 9px;
                            margin-left: 10px;
                            margin: 0px;
                            width: auto;
                            padding: 0px;
                            margin: 0px;
                            font-weight: 100;
                        }

                        .Dropdown_sub_width
                        {
                            cursor: pointer;
                        }
                    </style>
                    <script type="text/javascript">
                        $(document).ready(function () {


                            $('[id$=View_1]').css("display", "block");
                            $('#Tab_1').removeClass("Initial");
                            $('#Tab_1').addClass("Btn Clicked");
                            ////////////////////
                            $('[id$=View_2]').css("display", "none");
                            $('#Tab_2').removeClass("Btn Clicked");
                            $('#Tab_2').addClass("Initial");
                            ///////////////////
                            $('[id$=View_3]').css("display", "none");
                            $('#Tab_3').removeClass("Btn Clicked");
                            $('#Tab_3').addClass("Initial");
                            $('[id$=View_4]').css("display", "none");
                            $('#Tab_4').removeClass("Btn Clicked");
                            $('#Tab_4').addClass("Initial");

                            var variable = "101"
                            // $("select option:contains('Value " + variable + "')").attr("disabled", "disabled");
                            $("select option[value='" + variable + "']").attr('disabled', true);

                            //////////////////////////////////
                            $('#Tab_1').live('click', function () {

                                $('[id$=View_1]').css("display", "block");
                                $('#Tab_1').removeClass("Initial");
                                $('#Tab_1').addClass("Btn Clicked");

                                ////////////////////

                                $('[id$=View_2]').css("display", "none");
                                $('#Tab_2').removeClass("Btn Clicked");
                                $('#Tab_2').addClass("Initial");

                                ///////////////////

                                $('[id$=View_3]').css("display", "none");
                                $('#Tab_3').removeClass("Btn Clicked");
                                $('#Tab_3').addClass("Initial");

                                ///////////////////

                                $('[id$=View_4]').css("display", "none");
                                $('#Tab_4').removeClass("Btn Clicked");
                                $('#Tab_4').addClass("Initial");


                            });


                            $('#Tab_2').live('click', function () {

                                $('[id$=View_1]').css("display", "none");
                                $('#Tab_1').removeClass("Btn Clicked");
                                $('#Tab_1').addClass("Initial");


                                ///////////////////

                                $('[id$=View_2]').css("display", "block");
                                $('#Tab_2').removeClass("Initial");
                                $('#Tab_2').addClass("Btn Clicked");


                                ///////////////////


                                $('[id$=View_3]').css("display", "none");
                                $('#Tab_3').removeClass("Btn Clicked");
                                $('#Tab_3').addClass("Initial");

                                ///////////////////

                                $('[id$=View_4]').css("display", "none");
                                $('#Tab_4').removeClass("Btn Clicked");
                                $('#Tab_4').addClass("Initial");

                            });


                            $('#Tab_3').live('click', function () {

                                $('[id$=View_1]').css("display", "none");
                                $('#Tab_1').removeClass("Btn Clicked");
                                $('#Tab_1').addClass("Initial");

                                ///////////////////

                                $('[id$=View_2]').css("display", "none");
                                $('#Tab_2').removeClass("Btn Clicked");
                                $('#Tab_2').addClass("Initial");

                                ///////////////////

                                $('[id$=View_3]').css("display", "block");
                                $('#Tab_3').removeClass("Initial");
                                $('#Tab_3').addClass("Btn Clicked");

                                ///////////////////

                                $('[id$=View_4]').css("display", "none");
                                $('#Tab_4').removeClass("Btn Clicked");
                                $('#Tab_4').addClass("Initial");

                            });



                            $('#Tab_4').live('click', function () {

                                $('[id$=View_1]').css("display", "none");
                                $('#Tab_1').removeClass("Btn Clicked");
                                $('#Tab_1').addClass("Initial");

                                ///////////////////

                                $('[id$=View_2]').css("display", "none");
                                $('#Tab_2').removeClass("Btn Clicked");
                                $('#Tab_2').addClass("Initial");

                                ///////////////////

                                $('[id$=View_3]').css("display", "none");
                                $('#Tab_3').removeClass("Btn Clicked");
                                $('#Tab_3').addClass("Initial");

                                ///////////////////

                                $('[id$=View_4]').css("display", "block");
                                $('#Tab_4').removeClass("Initial");
                                $('#Tab_4').addClass("Btn Clicked");

                            });


                        });





                    </script>

                    <%--Other Payment Options--%>

                    <h3 class="theme-color">Other Membership Payment Options: </h3>
                    <br />
                    <h4>1) Net Banking or Cash Bank Transfer</h4>
                    <br />
                    <p>
                        If you are <strong>not able to pay via debit or credit card </strong>or if you <strong>don't want to use 
	online payment option</strong> due to privacy reasons then you can pay fees to our <strong>company bank account</strong>
                        via<strong> Net banking </strong>or <strong>cash bank transfer.</strong>
                    </p>
                    <p>Once payment done, send us <strong>transaction ID/No </strong>or <strong>receipt picture </strong>from your MyMassagePartner's registered email ID as soon as possible. We will check your email, verify then upgrade you as MyMassagePartner's premium member in next 30 minutes (maximum).</p>
                    <a href="#">Email to <span style="color: blue;"><u>info@mymassagepartner.com</u></span> if you like to transfer payment so  <strong>we can share our bank details. </strong>
                    </a>

                    <h4>2) Paytm</h4>
                    <br />
                    <p class="text-center">Scan the QR code and pay membership as per your wish plan:</p>
                    <ul class="list-inline text-center member-ship">
                        <li><strong>1 year Membership-	Rs 700 </strong></li>
                        <li><strong>2 year Membership-	Rs 1100	 </strong></li>
                        <li><strong>3 year Membership-	Rs 1800 </strong></li>
                    </ul>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6">
                    <img src="../image/ptm.jpg" class="img-responsive ptm-img" />
                </div>
                <div class="col-sm-6">
                    <p class="red-color">Once done please send us transaction ID/Reference ID</p>
                    <div class="form-box">
                        <div class="form-group">
                            <asp:TextBox ID="txtregisteredmail_id" runat="server" CssClass="form-control" onblur="required_mail_id();" placeholder="registered email id"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txttransaction" runat="server" CssClass="form-control" placeholder="Transaction ID or Reference ID"></asp:TextBox>
                        </div>
                        <a class="btn theme-btn" onclick="call_request_mail();">Send</a>
                        <div id="divResult"></div>
                    </div>
                    <p>For help please email us: <span style="color: blue;"><u>info@mymassagepartner.com</u></span></p>
                </div>
            </div>
        </div>

    </form>
</body>
</html>

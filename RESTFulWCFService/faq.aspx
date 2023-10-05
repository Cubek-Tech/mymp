<%@ Page Title="" Language="C#" MasterPageFile="~/SearchPartener.Master" AutoEventWireup="true" CodeBehind="faq.aspx.cs" Inherits="RESTFulWCFService.MassagePartener.faq" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Fup" %>
<%@ Register Src="~/Controls/DatePicker.ascx" TagName="uc" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <style>
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
    </style>
    <style>
        #main_popup
        {
            border-radius: 5px 5px 5px 5px;
            height: auto;
            width: 585px;
            margin: 35px auto auto;
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

        .change_pasd124
        {
            height: auto !important;
            font-size: 12px;
            width: auto !important;
        }

        .yearcheckdiv
        {
            width: 33%;
            position: relative;
            float: left;
            text-align: center;
            align-items: center;
        }
     
    </style>
    <title>FAQ - MyMassagePartner</title>
    <meta name="Description" content="Check all possible answers based on user's query.">
    <meta name="Keywords" content="massage partner questions | massage partner answer | faq for massage | faq for massage partner">
    <script>
        function validateEmail(email) {
            var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            return re.test(email);
        }
        function call_request_mail() {
            var val_id = $('#ctl00_ContentPlaceHolder1_txttransaction').val();
            var val_id1 = $('#ctl00_ContentPlaceHolder1_txtregisteredmail_id').val();
            if (val_id != '' && val_id1 != '') {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "faq.aspx/Send_Request_mail",
                    dataType: "json",
                    data: "{'id':'" + $('#ctl00_ContentPlaceHolder1_txtregisteredmail_id').val() + "','trans_no':'" + $('#ctl00_ContentPlaceHolder1_txttransaction').val() + "','sk':'" + $('#ctl00_ContentPlaceHolder1_partner_sk').val() + "'}",
                    success: function () {

                        $('#ctl00_ContentPlaceHolder1_txttransaction').val('');
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
                if ($('#ctl00_ContentPlaceHolder1_txtregisteredmail_id').val() == '') {
                    $('#ctl00_ContentPlaceHolder1_txtregisteredmail_id').focus();
                }
                else {
                    if ($('#ctl00_ContentPlaceHolder1_txttransaction').val() == '') {
                        $('#ctl00_ContentPlaceHolder1_txttransaction').focus();
                    }
                }

            }
        }
        function required_mail_id() {
            var val_id = $('#ctl00_ContentPlaceHolder1_txtregisteredmail_id').val();
            if (val_id == '') {
                $('#ctl00_ContentPlaceHolder1_txtregisteredmail_id').focus();
            }
            else if (validateEmail(val_id))
            { }
            else { $('#ctl00_ContentPlaceHolder1_txtregisteredmail_id').focus(); }
        }
    </script>
    <style>
    .theme-color{
	    color: #fd0014;
	}
	.red-color{
	   color:#ff0000;
	}
	.payment-page .member-ship li{
	padding:10px 5px;
	}
	.payment-page .ptm-img{
	max-height:300px;
	border:2px solid #ccc;
	}
	.form-box{
	max-width: 300px;
    border: 1px solid #ccc;
    padding: 20px;
    background-color: #ddd;
	margin:20px 10px;
	}
	.form-box .theme-btn{
	    background: #cb202d !important;
    color: #fff;
    border: 1px solid #cb202d;
	}
	@media only screen and (min-width: 768px) {
	.border-right{
	    border-right: 5px solid #eee;
	}
	}
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="overlay">
    </div>
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
    <asp:HiddenField ID="hdncountry" runat="server" />
    <asp:HiddenField ID="hdnpartnersubscribed" runat="server" />
    <asp:HiddenField ID="Merchant_Id" runat="server" />
    <asp:HiddenField ID="encRequest" runat="server" />
    <input type="hidden" runat="server" name="partner_sk" id="partner_sk" />
    <section class="wrapper">

        <div class="container wrapper-content ">
            <div class="main-content ">
                <div class="row">

                    <div class="col-sm-12">
                        <h2 class="text-center">Frequently Asked Questions (FAQs)</h2>
                    </div>

                </div>
                <div class="row">
                    <div class="col-sm-12">

                        <div class="accordition-section">
                            <%--  <h3 class="title text-center">Massage Provider/ Owner</h3>
					<br />--%>
                            <div class="panel-group" id="accordion">
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <span data-toggle="collapse">How I can find female and male massage partner here?
                                            </span>
                                        </h4>
                                    </div>
                                    <div>
                                        <div class="panel-body">
                                            <p>It’s easy to find massage partner i.e. female and male on MyMassagePartner. Only you need to create your profile and start searching massage partner as per your requirements and interests. You can select gender, location, and other criteria by which we can provide you best results in search page.</p>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <span data-toggle="collapse">Why membership required for massage partner?
                                            </span>
                                        </h4>
                                    </div>
                                    <div>
                                        <div class="panel-body">
                                            <p>We understand your concern, also would like to appreciate you to take membership and get ready for massage with your partner male or female. With MyMassagePartner membership, you will be able to send messages as well as make call to your massage partners anytime. Also, our 24/7 support team will help you in finding your massage partner if or when you are in need. We will recommend your profile to other registered users who looking for same buddy as his or her massage partner. Membership will definitely help you in finding best and most desirable female and male partner for massage.</p>
                                        </div>
                                    </div>
                                </div>

                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <span data-toggle="collapse">What are the benefits of massage partner membership?</span>
                                        </h4>
                                    </div>
                                    <div>
                                        <div class="panel-body">
                                            <p>
                                                <ul class="list-inline-image">
                                                    <li>You will be able to chat with female and male massage partner.</li>
                                                    <li>You will be able to see contact details of female or male massage partner.</li>
                                                    <li>You will find best female and male massage partner nearby you.</li>
                                                    <li>You will get support from MyMassagePartner team members i.e. Rose, Jasmine, and others 24/7.</li>
                                                    <li>You will get the female and male massage partner details in premium way.</li>
                                                    <li>You can find free massage in your location.</li>
                                                </ul>
                                            </p>
                                        </div>
                                    </div>
                                </div>

                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <span data-toggle="collapse">How can I see the contact numbers of female and male massage partners?</span>
                                        </h4>
                                    </div>
                                    <div>
                                        <div class="panel-body">
                                            <p>
                                                It’s is easy to see contact numbers and start chat with desired female and male massage partners anytime. Please take membership and you will be able to see the contact numbers of female and male massage partners and can chat as well 24/7.
                                                <asp:LinkButton ID="lnklogin" runat="server" OnClick="lnklogin_Click">Click and get your membership now!</asp:LinkButton><asp:LinkButton ID="lnkpayment" runat="server" data-toggle="modal" data-target="#popuppaypal" OnClientClick="return false;">Click and get your membership now!</asp:LinkButton>
                                            </p>
                                        </div>
                                    </div>
                                </div>

                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <span data-toggle="collapse">How can I pay for membership fees?</span>
                                        </h4>
                                    </div>
                                    <div i>
                                        <div class="panel-body">
                                            <p>
                                                It’s easy to pay for lifetime membership fees through our 3-D secured payment gateways . We have 100% secured and world’s best payment gateway i.e. Stripe and PayPal. You can pay annual membership fees through Stripe/PayPal via debit or credit card. 
                                    <br />
                                                <br />
                                                Here the steps:
                                    <br />
                                                <br />
                                                Step 1 - Login with your MyMassagePartner registered email ID and password.
                                                <br />
                                                Step 2 - Choose 'Membership Now!' and click on 'Pay Now'.
                                                <br />
                                                Step 3 - Pay via debit or credit card<br />
                                                <br />
                                                And you’re done!
                                                <br />
                                                <br />
                                                <%--<span style="color: red">Very Important:
                                                </span>--%>
                                             <%--   <br />
                                                <br />--%>
                                            </p>
                                            <div class="container payment-page" id="div_India_User" runat="server">
                                                <div class="row">
                                                    <h3 class="theme-color">Other Membership Payment Options: </h3>
                                                    <br />
                                                    <div class="col-sm-3 border-right">

                                                        <h4>1) Net Banking or Cash Bank&nbsp;&nbsp;&nbsp; Transfer</h4>
                                                        <br />
                                                        <p>
                                                            If you are <strong>unable to pay via credit or debit card </strong>or <strong>don't want to use 
	card for membership.</strong> In that case, you can pay fees to our <strong>company bank account</strong>
                        via<strong> Net banking </strong>or <strong>cash bank transfer.</strong>
                    </p>
                    <p>Once payment done, send us <strong>transaction number </strong>or <strong>picture </strong>from your registered email ID. We will verify and activate your membership in next 30 minutes(maximum).</p>
                     If you like to transfer payment via net banking or cash bank transfer then please email: <span style="color: blue;"><u>info@mymassagepartner.com</u></span>
 </div>
                                     <div class="col-sm-7">
                    <h4>2) <img src="image/paytm.png"     width="80px;" /></h4>
                                                        <p class="text-center">Scan the QR code and pay membership as per your wish plan:</p>
                                                        <ul class="list-inline text-center member-ship">
                                                            <li><strong>1 year Membership-	<asp:Label ID="lbloneyear" runat="server"></asp:Label> </strong></li>
                                                            <li><strong>2 years Membership-	<asp:Label ID="lbltwoyear" runat="server"></asp:Label>	 </strong></li>
                                                            <li><strong>3 years Membership-	<asp:Label ID="lblthreeyear" runat="server"></asp:Label> </strong></li>
                                                        </ul>
                                                        <div class="row">
                                                            <div class="col-sm-6">
                                                                <img src="image/ptm.jpg" class="img-responsive ptm-img">
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
                                                                    <br />
                                                                    <div id="divResult"></div>
                                                                </div>
                                                                <p>For help please email us: <span style="color:blue;"><u>info@mymassagepartner.com</u></span></p>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <%--paytm--%>


                                        <%--paytm--%>
                                    </div>
                                </div>

                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <span data-toggle="collapse">Why I need to fill my address on membership payment form? I want complete privacy.</span>
                                        </h4>
                                    </div>
                                    <div>
                                        <div class="panel-body">
                                            <p>
                                                Your billing address is just for payment information. We will not save your billing address also we will not communicate to your address. We respect your right to privacy and your privacy will be highly secured on MyMassagePartner.
                                            </p>
                                        </div>
                                    </div>
                                </div>

                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <span data-toggle="collapse">What about my privacy? I want complete privacy.</span>
                                        </h4>
                                    </div>
                                    <div>
                                        <div class="panel-body">
                                            <p>
                                                No worries! We understand you and your needs. MyMassagePartner respect ‘right to privacy’ and your all information will be 100% safe with us. We have 3-D level secured website (HTTPS) and great reputation in terms of our user’s privacy. So, let’s start and find your massage partner. 
                                            </p>
                                        </div>
                                    </div>
                                </div>

                            </div>

                        </div>



                    </div>
                </div>

            </div>
        </div>
    </section>

    <!----------------Payment Section------------------->
    <div id="main_popup" class="box_Login2" style="display: none;">
        <div style="width: 23px; height: 23px; float: right;">
            <a class="boxcanceel" onclick="closeOffersDialog('main_popup');" style="top: -6px; left: -20px;"></a>
        </div>
        <div class="welcome_signup_page">
            <div class="welcome_text_signuppage123" id="span_changtext1">
                <div id="divm2b1" runat="server" class="welcome_text_signuppage123">
                    Welcome to MyMassagePartner!
                </div>
                <div class="content_signup123">
                    <div class="css_search89">
                        Payment Successfully Done!
                    </div>
                </div>
            </div>
            <lable id="lblpaymentinfo" runat="server" style="display: none"></lable>
        </div>
    </div>
 </asp:Content>

﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="service_subscription.aspx.cs" Inherits="RESTFulWCFService.User.service_subscription" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title></title>
    <link id="fevicon_icon" rel="shortcut icon" href="../img/mp.png" type="image/x-icon"
        runat="server" />
    <%-- <script type="text/javascript" src="<%# ResolveUrl("~/js/jquery-1.7.1.min.js") %>"></script>--%>
    <script src="../js/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        function postbackurl() {
            frmpayment.submit();
            //  $('.postbackurl').click();
        }
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
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
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
      @media only screen and (min-width: 768px)
      {
          .border-right
          {
              border-right: 5px solid #eee;
          }
      }
  </style>
    <style>
        .fields
        {
            padding: 8px;
            height: 24px;
            width: 287px;
            border: 1px solid skyblue;
            border-radius: 2px;
            box-shadow: grey 5px 5px 3px -3px;
            margin-top: 10px;
            margin-bottom: 10px;
        }

        .btn
        {
            padding: 7px 28px 10px 28px;
            /*height: 41px;
            width: 79px;*/
            background-color: skyblue;
            color: black;
            font-weight: bold;
            border-radius: 2px;
            border: 1px solid black;
            cursor: pointer;
        }

        .maincss12
        {
            margin-left: 160px;
        }

        .paymenterror
        {
            color: red;
            margin-bottom: 0px;
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

        div.container4
        {
            height: 10em;
            position: relative;
        }

            div.container4 img
            {
                margin: 0;
                position: absolute;
                top: 50%;
                left: 50%;
                margin-right: -50%;
                transform: translate(-50%, -50%);
            }

        .text123
        {
            font-size: 12px;
            color: red;
            font-weight: bold;
            text-decoration: underline;
        }

        .text4554
        {
            font-size: 12px;
            color: red;
            text-decoration: underline;
        }

        .msgcss
        {
            margin: 130px;
            padding: 30px;
            margin-top: 13px;
            font-size: 14px;
            line-height: 1.5;
        }

        .txt233
        {
            text-decoration: underline;
        }

        .auto-style1
        {
            font-size: 12px;
            color: red;
            font-weight: bold;
        }

        .auto-style2
        {
            color: #3333FF;
        }

        .divtrans
        {
            position: absolute;
            margin-left: 551px;
            margin-top: -410px;
        }
    </style>
</head>
<body>
    <form runat="server" id="frmpayment" name="frmpayment" enctype="multipart/form-data"
        method="post" action="http://shadimaker.com/payment/pay.php">
        <input type="hidden" runat="server" name="partner_sk" id="partner_sk" />
        <input type="hidden" runat="server" name="OrderID" id="OrderID" value="11029" />
        <input type="hidden" runat="server" name="Name" id="Name" value="Gyan Dwivedi" />
        <input type="hidden" runat="server" name="Address" id="Address" value="Noid" />
        <input type="hidden" runat="server" name="Amount" id="Amount" value="100" />
        <input type="hidden" runat="server" name="Mobile" id="Mobile" value="9795554735" />
        <input type="hidden" runat="server" name="Email" id="Email" value="coolgcd@gmail.com" />
        <input type="hidden" runat="server" name="Zipcode" id="Zipcode" value="201301" />
        <input type="hidden" runat="server" name="City" id="City" value="Noida" />
        <input type="hidden" runat="server" name="State" id="State" value="Uttar Pradesh" />
        <input type="hidden" runat="server" name="Country" id="Country" value="India" />
        <input type="hidden" runat="server" name="SuccessURL" id="SuccessURL" value="http://massage2book.com/pagename.php" />
        <input type="hidden" runat="server" name="CancelURL" id="CancelURL" value="http://massage2book.com/pagename.php" />
        <div>
            <div class="container payment-page">
                <div class="row">
                    <div class="col-sm-12">
                        <div runat="server" id="paypalReturnscreen">
                            <div id="index_bg_main">
                                <div>
                                    <div id="search">
                                        <h4 class="red-color">Payment unsuccessful...</h4>
                                        <br>
                                        <lable class="red-color" id="lblerror_stripe" runat="server"></lable>
                                        <br>
                                       <h4> <a style="color: Blue;" id="Qucikhome" runat="server">Go back to homepage</a></h4>
                                        <br>
                                        <br>
                                        <%--  <a style="color: Blue;" href="<%#  Constants__.WEB_ROOT %>/contact">Contact us</a>--%>
                                    </div>
                                </div>
                                <div id="div_India_User" runat="server">
                                    <h3 class="theme-color">Other Membership Payment Options: </h3>
                                      <div class="col-sm-5 border-right">
                                    <br />
                                    <h4>1) Net Banking or Cash Bank Transfer</h4>
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
                    <h4>2) <img src="../image/paytm.png"     width="80px;" /></h4>
                                    <p class="text-center">Scan the QR code and pay membership as per your wish plan:</p>
                                    <ul class="list-inline text-center member-ship">
                                        <li><strong>1 year Membership-	Rs 700 </strong></li>
                                        <li><strong>2 year Membership-	Rs 1100	 </strong></li>
                                        <li><strong>3 year Membership-	Rs 1800 </strong></li>
                                    </ul>
                            <div class="row">
                                <div class="col-sm-6">
                                    <img src="../image/ptm.jpg" class="img-responsive ptm-img">
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
                            <div class="head_page_hight" style="display: none;">
                                <div class="address-resion">
                                    <lable id="lblSuccess" runat="server"></lable>
                                </div>
                                <div class="contect-form">
                                    <asp:LinkButton runat="server" Width="161px" CssClass="button" ID="lnkBack" Text="Go Back"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div runat="server" id="paypalGonescrren" class="container4" visible="true">
                        <div>
                            <img style="background-color: black" src="../image/mp_logo.png" />
                        </div>
                        </br> </br> </br>
                    <div>
                        <img style="margin-top: 50px;" src="../img/m2bpaymentprocessing.gif" />
                    </div>
                    </div>
                </div>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="postbackurl" Style="display: none;"
                    name="btnSubmitLink" PostBackUrl="http://www.ccavenue.com/shopzone/cc_details.jsp" />
                <asp:HiddenField ID="Merchant_Id" runat="server" />
                <asp:HiddenField ID="encRequest" runat="server" />
            </div>
        </div>
    </form>
</body>
</html>

///   Stripe Section  ///
function checkReqFields_stripe_() {
    var returnValue;
    var cardName = document.getElementById("txtCardName_1").value;
    var cardNumber = document.getElementById("txtCardNumber_1").value;
    var cardExpMonth = document.getElementById("txtCardExpirationMonth_1").value;
    var cardExpYear = document.getElementById("txtCardExpirationYear_1").value;

    returnValue = true;
    if (cardName.trim() == "") {
        document.getElementById("txtCardName_1").style.borderColor = "#f38181";
        document.getElementById("txtCardName_1").style.boxShadow = "rgba(0, 0, 0, 0.075) 0px 1px 1px inset, rgb(228, 87, 77) 0px 0px 7px";
        returnValue = false;
    }
    if (cardNumber.trim() == "") {
        document.getElementById("txtCardNumber_1").style.borderColor = "#f38181";
        document.getElementById("txtCardNumber_1").style.boxShadow = "rgba(0, 0, 0, 0.075) 0px 1px 1px inset, rgb(228, 87, 77) 0px 0px 7px";
        returnValue = false;
    }
    if (cardExpMonth.trim() == "") {
        document.getElementById("txtCardExpirationMonth_1").style.borderColor = "#f38181";
        document.getElementById("txtCardExpirationMonth_1").style.boxShadow = "rgba(0, 0, 0, 0.075) 0px 1px 1px inset, rgb(228, 87, 77) 0px 0px 7px";
        returnValue = false;
    }
    if (cardExpYear.trim() == "") {
        document.getElementById("txtCardExpirationYear_1").style.borderColor = "#f38181";
        document.getElementById("txtCardExpirationYear_1").style.boxShadow = "rgba(0, 0, 0, 0.075) 0px 1px 1px inset, rgb(228, 87, 77) 0px 0px 7px";
        returnValue = false;
    }
    return returnValue;
}
function checkFilled_(obj) {

    var val = document.getElementById(obj).value;
    if (val.trim() == "") {
        document.getElementById(obj).style.borderColor = "#f38181";
        document.getElementById(obj).style.boxShadow = "rgba(0, 0, 0, 0.075) 0px 1px 1px inset, rgb(228, 87, 77) 0px 0px 7px";
    }
    else {
        document.getElementById(obj).style.removeProperty('border-color');
        document.getElementById(obj).style.removeProperty('box-shadow');
        document.getElementById(obj).style.borderColor = '1px solid #ccc';
    }
}

///   Paytm Section  ///
function checkReqFields_paytm() {
    var returnValue;
    var user_Email = document.getElementById("txtregisteredmail_id").value;
    var order_no = document.getElementById("ctxttransaction").value;

    returnValue = true;
    if (user_Email.trim() == "") {
        document.getElementById("txtregisteredmail_id").style.borderColor = "#f38181";
        document.getElementById("txtregisteredmail_id").style.boxShadow = "rgba(0, 0, 0, 0.075) 0px 1px 1px inset, rgb(228, 87, 77) 0px 0px 7px";
        returnValue = false;
    }
    if (order_no.trim() == "") {
        document.getElementById("ctxttransaction").style.borderColor = "#f38181";
        document.getElementById("ctxttransaction").style.boxShadow = "rgba(0, 0, 0, 0.075) 0px 1px 1px inset, rgb(228, 87, 77) 0px 0px 7px";
        returnValue = false;
    }
    return returnValue;
}
function checkFilled_paytm() {
    var user_Email = document.getElementById("txtregisteredmail_id").value;
    var order_no = document.getElementById("ctxttransaction").value;
    if (user_Email.trim() == "") {
        document.getElementById("txtregisteredmail_id").style.borderColor = "#f38181";
        document.getElementById("txtregisteredmail_id").style.boxShadow = "rgba(0, 0, 0, 0.075) 0px 1px 1px inset, rgb(228, 87, 77) 0px 0px 7px";
    }
    else {
        document.getElementById('txtregisteredmail_id').style.removeProperty('border-color');
        document.getElementById('txtregisteredmail_id').style.removeProperty('box-shadow');
        document.getElementById("txtregisteredmail_id").style.borderColor = '1px solid #ccc';
    }
    if (order_no.trim() == "") {
        document.getElementById("ctxttransaction").style.borderColor = "#f38181";
        document.getElementById("ctxttransaction").style.boxShadow = "rgba(0, 0, 0, 0.075) 0px 1px 1px inset, rgb(228, 87, 77) 0px 0px 7px";
    }
    else {
        document.getElementById('ctxttransaction').style.removeProperty('border-color');
        document.getElementById('ctxttransaction').style.removeProperty('box-shadow');
        document.getElementById("ctxttransaction").style.borderColor = '1px solid #ccc';
    }
}

///   QR Code Section   ///

function checkReqFields_QR() {
    var returnValue;
    var user_Email = document.getElementById("txtqr_mail_id").value;
    var order_no = document.getElementById("txtQrTransaction").value;

    returnValue = true;
    if (user_Email.trim() == "") {
        document.getElementById("txtqr_mail_id").style.borderColor = "#f38181";
        document.getElementById("txtqr_mail_id").style.boxShadow = "rgba(0, 0, 0, 0.075) 0px 1px 1px inset, rgb(228, 87, 77) 0px 0px 7px";
        returnValue = false;
    }
    if (order_no.trim() == "") {
        document.getElementById("txtQrTransaction").style.borderColor = "#f38181";
        document.getElementById("txtQrTransaction").style.boxShadow = "rgba(0, 0, 0, 0.075) 0px 1px 1px inset, rgb(228, 87, 77) 0px 0px 7px";
        returnValue = false;
    }
    return returnValue;
}
function checkFilled_QR() {
    var user_Email = document.getElementById("txtqr_mail_id").value;
    var order_no = document.getElementById("txtQrTransaction").value;
    if (user_Email.trim() == "") {
        document.getElementById("txtqr_mail_id").style.borderColor = "#f38181";
        document.getElementById("txtqr_mail_id").style.boxShadow = "rgba(0, 0, 0, 0.075) 0px 1px 1px inset, rgb(228, 87, 77) 0px 0px 7px";
    }
    else {
        document.getElementById('txtqr_mail_id').style.removeProperty('border-color');
        document.getElementById('txtqr_mail_id').style.removeProperty('box-shadow');
        document.getElementById("txtqr_mail_id").style.borderColor = '1px solid #ccc';
    }
    if (order_no.trim() == "") {
        document.getElementById("txtQrTransaction").style.borderColor = "#f38181";
        document.getElementById("txtQrTransaction").style.boxShadow = "rgba(0, 0, 0, 0.075) 0px 1px 1px inset, rgb(228, 87, 77) 0px 0px 7px";
    }
    else {
        document.getElementById('txtQrTransaction').style.removeProperty('border-color');
        document.getElementById('txtQrTransaction').style.removeProperty('box-shadow');
        document.getElementById("txtQrTransaction").style.borderColor = '1px solid #ccc';
    }
}
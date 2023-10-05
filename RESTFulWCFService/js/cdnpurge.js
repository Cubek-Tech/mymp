$(document).ready(function () {
    start_purge();
});
arra = []
arra[0] = '/css/payment_popup_partner.css';
arra[1] = '/css/style.css';
arra[2] = '/css/crebas_seeker_popup.css';
arra[3] = '/js/partner_js.js';

function start_purge() {
    var d = { "cdn_id": "128049", "login": "neer_chat@yahoo.com", "passwd": "4cYJPqBMvXr9ybNkUHxjm8ECWOZ13fdz", "url[]": arra };
    $.ajax({
        url: "https://api.cdn77.com/v2.0/data/purge",
        type: 'POST',
        data: d,
        success: function (data) {
           console.log(JSON.stringify(data));
        },
        error: function () {
            //alert("Cannot get data");
        }
    });
}
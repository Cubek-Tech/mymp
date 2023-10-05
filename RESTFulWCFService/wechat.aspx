<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wechat.aspx.cs" Inherits="RESTFulWCFService.wechat" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://code.jquery.com/jquery-3.4.1.min.js"
        integrity="sha256-CSXorXvZcTkaix6Yvo6HppcZGetbYMGWSFlBw8HfCJo="
        crossorigin="anonymous"></script>
    <script src="https://js.stripe.com/v3/"></script>
    <script type="text/javascript">
        function initiate() {
            var stripe = Stripe('pk_test_U25Au5FN9qAbGsHSoWCWYI6B');
            stripe.createSource({
                type: 'wechat',
                amount: 1099,
                currency: 'usd',
            }).then(function (result) {
                document.getElementById('HiddenField1').value = result.source.wechat.qr_code_url;
                $.ajax({
                    url: result.source.wechat.qr_code_url,
                    contentType: "application/json; charset=utf-8",
                    type: 'POST',
                    dataType: "json",
                    data: JSON.stringify([]),
                });
                return false;
            });
        }
        $(document).ready(function () {
            initiate();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:HiddenField ID="HiddenField1" runat="server" ClientIDMode="Static" />
            <asp:Button ID="Button1" runat="server" Text="Button" OnClientClick="initiate();" />
        </div>
    </form>
</body>
</html>

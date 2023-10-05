<%@ Page Title="" Language="C#" AutoEventWireup="true" Inherits="ErrorMessage" CodeBehind="ErrorMessage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title id="title_head" runat="server"></title>
    <script src="js/jquery-1.7.1.min.js" type="text/javascript"></script>
    <link id="fevicon_icon" rel="shortcut icon" type="image/x-icon" />
    <script type="text/javascript">
        $(window).load(function () {

            var src;
            var res = window.location.hostname.search('book');
            if (res >= 0)
                src = 'ma.ico'
            else
                src = 'fi.ico'

            document.head = document.head || document.getElementsByTagName('head')[0];
            var link = document.createElement('link'),
                oldLink = document.getElementById('fevicon_icon');
            link.id = 'fevicon_icon';
            link.rel = 'shortcut icon';
            link.href = '<%# Constants__.WEB_ROOT%>/' + src;
            if (oldLink) {
                document.head.removeChild(oldLink);
            }
            document.head.appendChild(link);


        });


    </script>
    <style type="text/css">
        body
        {
            background: none repeat scroll 0% 0% rgb(249, 254, 232);
            margin: 0px;
            padding: 20px;
            text-align: center;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 14px;
            color: rgb(102, 102, 102);
        }
        .error_page
        {
            width: 650px;
            padding: 50px;
            margin: auto;
        }
        .error_page h1
        {
            margin: 20px 0px 0px;
        }
        .error_page p
        {
            margin: 10px 0px;
            padding: 0px;
        }
        .error_text
        {
            font-size: 35px;
            font-weight: bold;
        }
       
   .button { /*basic styles*/ width: 250px; height: 50px; color: white; background-color: #99CF00; text-align: center; font-size: 30px; line-height: 50px; /*gradient styles*/ background: -webkit-gradient(linear, 0% 0%, 0% 100%, from(#99CF00), to(#6DB700)); background: -moz-linear-gradient(19% 75% 90deg,#6DB700, #99CF00); /*border styles*/ border-left: solid 1px #c3f83a; border-top: solid 1px #c3f83a; border-right: solid 1px #82a528; border-bottom: solid 1px #58701b; -moz-border-radius: 10px; -webkit-border-radius: 10px; border-radius: 10px; -webkit-gradient(linear, 0% 0%, 0% 100%, from(#99CF00), to(#6DB700)) } .button h3 { font-size: 20px; line-height: 35px; font-family: helvetica, sans-serif; } .button p { font-size: 12px; line-height: 4px; font-family: helvetica, sans-serif; } a { text-decoration: none; color: fff; } .button:hover { background: -webkit-gradient(linear, 0% 0%, 0% 100%, from(#6DB700), to(#99CF00)); background: -moz-linear-gradient(19% 75% 90deg,#99CF00, #6DB700); } 
   background: -webkit-gradient(linear, 0% 0%, 0% 100%, from(#52a8e8), to(#377ad0));
}
    </style>
</head>
<body>
    <form id="form1" runat="server">

    <div>
        <div style="text-align: center; font-weight: bold; height: 70%;">
        </div>
        <div class="error_page">
            <img src="img/general_user_error_icon.png" width="100" height="100">
            <h1>
                <span id="divMsg" runat="server" class="error_text"></span>
            </h1>
            <p>
                <asp:LinkButton runat="server" Width="161px" CssClass="button" ID="lnkBack" Text="Go Back"></asp:LinkButton>
            </p>
        </div>
    </div> <img id="img_val" runat="server" />
    </form>
</body>
</html>

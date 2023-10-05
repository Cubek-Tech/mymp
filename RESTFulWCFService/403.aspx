<%@ Page Language="C#" AutoEventWireup="true" Inherits="_403" Codebehind="403.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <style>
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
            width: 600px;
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
            font-size: 60px;
            font-weight: bold;
        }
        a
        {
            color: rgb(156, 170, 109);
            text-decoration: none;
        }
        a:hover
        {
            color: rgb(156, 170, 109);
            text-decoration: underline;
            color: #666666;
        }
    </style>
</head>
<body class="login">
    <div class="error_page">
        <img src="img/404_icon.png" width="231" height="211">
        <div class="error_text">
            403</div>
        <h1>
            We're sorry...</h1>
        <p>
            You are not authorized to access this page.</p>
        <p>
            <a href="http://www.massage2book.com/">Return to Massage2Book homepage</a></p>
    </div>
</body>
</html>

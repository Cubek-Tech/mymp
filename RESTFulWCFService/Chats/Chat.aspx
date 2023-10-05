<%@ Page Language="C#" AutoEventWireup="true" Inherits="RESTFulWCFService.Chat" CodeBehind="Chat.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Styles/Site.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.6.4.js" type="text/javascript"></script>
    <script src="../Scripts/json2.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.signalR-2.2.2.min.js"></script>
   <%-- <script src="../Scripts/jquery.signalR-1.0.0-rc2.min.js" type="text/javascript"></script>--%>
    <script src="<%=ResolveUrl("~/signalr/hubs") %>" type="text/javascript"></script>

    
 <script type="text/javascript">
     $(function () {
         $('.showhide').click(function () {
             $(".chat-sidebar").slideToggle();

         });
     });
    </script>
    <script type="text/javascript">



        $(document).ready(function () {

            // Proxy created on the fly
            var chat = $.connection.chatHub;

            chat.client.OnlineStatus = function (connectionId, userList, userList_name) {
                $("img[id^=stat]").attr('src', 'images/offline.png');
                $(userList).each(function (index, obj) {
                   
                    var id = userList[index]
                    var name = userList_name[index];
                    var name = userList_name[index];
                    var UserName = document.getElementById("<%= hdnUserName.ClientID %>").value;

                    if (UserName != name) {

                        $("#All_online").append("<img ID='stat" + (index) + "' src='../Images/online.png' style='height:18px;width:18px;' Class='online' />");
                        $("#All_online").append(" <a id='status" + (index) + "' class='UserItem online' onclick='onchat_click(this);' data-userid='" + id + "' href='#'>" + name + "</a>");
                        $("#All_online").append("<br />");
                    }

                    chat.server.createGroup($('#hdnUserId').val(), $(this).attr('data-userid'));
                    var chatWindow = $("#divChatWindow").clone(true);
                    //$(chatWindow).css('display', 'block');
                    $(chatWindow).attr('chatToId', $(this).attr('data-userid'));
                    $("#chatContainer").append(chatWindow);
                    //});
                    //if (obj == "111") {
                    //    $("#stat1").attr('src', 'images/online.png');
                    //    $("#status1").addClass('online');
                    //}
                    //else if (obj == "222") {
                    //    $("#stat2").attr('src', 'images/online.png');
                    //    $("#status2").addClass('online');
                    //}
                    //else if (obj == "333") {
                    //    $("#stat3").attr('src', 'images/online.png');
                    //    $("#status3").addClass('online');
                    //}
                });

                if ($(userList).length == 1) {
                    $("#<%=lbl_user.ClientID%>").show();

                }
                else {

                    $("#<%=lbl_user.ClientID%>").hide();
                }


            };

            chat.client.joined = function (connectionId, userList, userList_name) {
                $(userList).each(function (index, obj) {

                    //$('#All_online').append("<asp:Image ID='xxx' runat='server' Width='18px' Height='18px' ImageUrl='~/Images/offline.png' /><br/><a id='x' class='UserItem' data-userid='111' href='#'>Kedar</a>");
                    //if (obj == "111") {
                    //  $("#stat1").attr('src', 'images/online.png');
                    // $("#stat1").addClass('online');
                    //}
                    //else if (obj == "222") {
                    //  $("#stat2").attr('src', 'images/online.png');
                    //  $("#stat2").addClass('online');
                    //}
                    //else if (obj == "333") {
                    //  $("#stat3").attr('src', 'images/online.png');
                    // $("#stat3").addClass('online');
                    // }

                    var id = userList[index]
                    var name = userList_name[index];
                    // $("#All_online").append("<img ID='stat" + (++index) + "' src='Images/online.png' Width='18px' Height='18px' Class='online'  />");
                    // $("#All_online").append(" <a id='status" + (++index) + "' class='UserItem online' data-userid='" + id + "' href='#'>'" + name + "'</a>");
                    // $("#All_online").append("<br />");
                });
            };

            chat.client.setChatWindow = function (strGroupName, strChatTo) {

                $('div[chatToId=' + strChatTo + ']').attr('groupname', strGroupName);
                $('div[chatToId=' + strChatTo + ']').css('display', 'block')
                
            };
            // Declare a function on the chat hub so the server can invoke it
            chat.client.addMessage = function (message, groupName,from_id,from_name) {
              
                if ($('div[groupname=' + groupName + ']').length == 0) {
                    var chatWindow = $("#divChatWindow").clone(true);
                    $(chatWindow).css('display', 'block');
                    $(chatWindow).attr('groupname', groupName);
                    $(chatWindow).attr('chatToId', from_id);
                    $(chatWindow).attr('Class', from_id);
                    $("#chatContainer").append(chatWindow);
                  
                    popups.unshift(from_id);
                    calculate_popups();
                }
                $('div[groupname=' + groupName + ']').find('ul').append('<LI>' + message + '');
                $('div[groupname=' + groupName + ']').find('span').text(from_name);

            };
            $("#broadcast").click(function () {
                // Call the chat method on the server
                chat.server.send($('#msg').val());
            });
            // Start the connection
            $.connection.hub.start(function () {
                chat.server.getAllOnlineStatus();
            });

            //$('.UserItem').click(function () {

            //});

            $(".ChatSend").click(function () {
                strChatText = $('.ChatText', $(this).parent()).val();
                if (strChatText != '') {
                    var strGroupName = $(this).parent().attr('groupname');
                    if (typeof strGroupName !== 'undefined' && strGroupName !== false)
                        
                        chat.server.send($("#hdnUserName").val() + ' : ' + strChatText, $(this).parent().attr('groupname'), $("#hdnUserId").val(), $("#hdnUserName").val());
                    $('.ChatText', $(this).parent()).find('ul').append(strChatText);
                    $('.ChatText', $(this).parent()).val('');
                }
                return false;
            });

            //function openchatBox() {
            //    alert('raz');
            //    //if ($(this).hasClass('online')) {
            //    chat.server.createGroup($('#hdnUserId').val(), $(this).attr('data-userid'));
            //    var chatWindow = $("#divChatWindow").clone(true);
            //    //$(chatWindow).css('display', 'block');
            //    $(chatWindow).attr('chatToId', $(this).attr('data-userid'));
            //    $("#chatContainer").append(chatWindow);
            //    //}
            //}
        });



        //this function can remove a array element.
        Array.remove = function (array, from, to) {
            var rest = array.slice((to || from) + 1 || array.length);
            array.length = from < 0 ? array.length + from : from;
            return array.push.apply(array, rest);
        };

        //this variable represents the total number of popups can be displayed according to the viewport width
        var total_popups = 0;

        //arrays of popups ids
        var popups = [];

        //displays the popups. Displays based on the maximum number of popups that can be displayed on the current viewport width
        function display_popups() {

            var right = 220;

            // var iii = 0;

            for (var iii = 0; iii < total_popups; iii++) {

                if (popups[iii] != undefined) {

                    var test = '.' + popups[iii];

                    $(test).css({ 'right': right + "px" });

                    right = right + 320;
                    $(test).css({ 'display': 'block' });

                }
            }


            for (var jjj = iii; jjj < popups.length; jjj++) {

                var test = '.' + popups[iii];
                $(test).css({ 'display': 'none' });
                //var element = document.getElementById(popups[jjj]);
                //element.style.display = "none";
            }
        }

        //calculate the total number of popups suitable and then populate the toatal_popups variable.
        function calculate_popups() {

            var width = window.innerWidth;

            if (width < 540) {
                total_popups = 0;
            }
            else {
                width = width - 200;

                //320 is width of a single popup box
                total_popups = parseInt(width / 320);

            }

            display_popups();

        }

        //this is used to close a popup
        function close_popup(obj) {
            alert('close')
            var t = $(obj).closest('#divChatWindow').attr('class');
            for (var iii = 0; iii < popups.length; iii++) {
                if (t == popups[iii]) {
                    alert('1');
                    Array.remove(popups, iii);
                    $("." + t).css({ 'display': 'none' });
                    $("." + t).remove();
                    //  document.getElementById(id).style.display = "none";

                    calculate_popups();

                    return;
                }
            }
        }



        function onchat_click(obj) {

            for (var iii = 0; iii < popups.length; iii++) {



                //already registered. Bring it to front.
                if ($(obj).attr('data-userid') == popups[iii]) {

                    Array.remove(popups, iii);

                    popups.unshift($(obj).attr('data-userid'));


                    calculate_popups();


                    return;
                }
            }

            var name = $(obj).text();
            $('#<%= lbl_username.ClientID %>').text(name);
            var chat = $.connection.chatHub;

            if ($(obj).hasClass('online')) {

                chat.server.createGroup($('#hdnUserId').val(), $(obj).attr('data-userid'));
                var chatWindow = $("#divChatWindow").clone(true);
                //$(chatWindow).css('display', 'block');
                $(chatWindow).attr('chatToId', $(obj).attr('data-userid'));
                $(chatWindow).attr('Class', $(obj).attr('data-userid'));
                $("#chatContainer").append(chatWindow);

                popups.unshift($(obj).attr('data-userid'));

                calculate_popups();

            }


            return false;


        }



        //recalculate when window is loaded and also when window is resized.
        window.addEventListener("resize", calculate_popups);
        window.addEventListener("load", calculate_popups);
    </script>
    <style type="text/css">

        .ChatText {
            resize:none;
            overflow-y:scroll;
            margin-top:4px;
            
        }
        
          .ChatSend {
            margin-left:5px;
            position:relative;
            top: -15px;
            width:45px;
            color:white;
        }

    .right-fixed {
	background-color: #ff8a00;
    border-radius: 50%;
    padding: 5px;
    position: absolute;
    top: 0px;
    right: 3px;
	box-shadow: 1px 2px 6.5px #58aa48;
    cursor:pointer;
    
}
           .left-fixed {
	background-color: #ff8a00;
    border-radius: 50%;
    padding: 5px;
    position: absolute;
    top: 0px;
    right: 210px;
	box-shadow: 1px 2px 6.5px #58aa48;
   
}

        .chat-sidebar
            {
                width: 16%;
                position: fixed;
                height: 70%;
                right: 0px;
                top: 100px;
                padding-top: 10px;
                padding-bottom: 10px;
                border: 1px solid rgba(29, 49, 91, .3);
                overflow-y: scroll;
                display:none;
              
            }
        .sidebar-name 
            {
                padding-left: 10px;
                padding-right: 10px;
                margin-bottom: 4px;
                font-size: 12px;
            }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
         <br />
        <asp:Label ID="lblUserName" runat="server" Font-Bold="true"/>
        <br />

        <div class="right-fixed">
        <a href="#" class="showhide">Messenger</a>
        </div>

        <div class="chat-sidebar" id="All_online"> 
            <asp:Label ID="lbl_user" runat="server" Text="Please refresh to reload the list." Font-Bold="true" />
        <%--style="border: thin solid #C0C0C0; font-family: Tunga; width: 200px; font-weight: 500;font-size:14px;"  #6d84b4--%>
       
               </div>
              <br />
        <div id="divChatWindow" style="border: 1px solid darkgreen; float: left; width: 365px; display: none; margin-right: 10px;opacity:1;background-color:white;">
          
            <div style="background-color: green;padding: 5px;color:white; font-weight: bold;font-size: 14px; clear: both;">
               
                    <asp:Label ID="lbl_username" runat="server"/>

                    <a id="close_chat" onclick="close_popup(this);" style="top: 0;float:right;cursor: pointer;">&#10005;</a>
              <%--  <a class="boxclose" style="top: 0" onclick="closeDialog('divChatWindow');"></a>--%>
                   
                <div style="clear: both"></div>
            </div>
            <div style="border: 1px solid ;overflow-y: scroll;background-color:white">
                <ul id="messages" style="width: 335px; height: 250px; font: normal 2 verdana"></ul>
            </div>
           
          <asp:TextBox runat="server" ID="msg" CssClass="ChatText" Width="295px" BorderColor="green"
                BorderStyle="Solid" BorderWidth="1px" Height="35px" TextMode="MultiLine"></asp:TextBox>
            <asp:Button ID="btnChatSend" runat="server" CssClass="ChatSend" Text="Send" OnClientClick="return false"
                BackColor="green" Font-Size="Smaller" BorderStyle="Solid" BorderWidth="1px" 
                BorderColor="DarkGreen" Height="35px" />
        </div>

       
    <%--    <div id="All_online1" style="border: thin solid #C0C0C0; font-family: Tunga; width: 70px; font-weight: 500;">            
            <asp:Image ID="stat1" runat="server" Width="18px" Height="18px" ImageUrl="~/Images/offline.png"  />
            <a id="status1" class="UserItem" data-userid="111" href="#">Kedar</a>
            <br />
            <asp:Image ID="stat2" runat="server" Width="18px" Height="18px" ImageUrl="~/Images/offline.png" />
            <a id="status2" class="UserItem" data-userid="222" href="#">abc</a>
            <br />
            <asp:Image ID="stat3" runat="server" Width="18px" Height="18px" ImageUrl="~/Images/offline.png" />
            <a id="status3" class="UserItem" data-userid="333" href="#">xyz</a>
        </div>--%>
        
        <div style="bottom: 0px; right: 220px;float:left;position:fixed;" id="chatContainer">
        </div>
         
        
     
        <asp:HiddenField ID="hdnUserId" runat="server" />
        <asp:HiddenField ID="hdnUserName" runat="server" />
    </form>
</body>
</html>

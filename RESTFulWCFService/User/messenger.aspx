<%@ Page Title="" Language="C#" MasterPageFile="~/SearchPartener.Master" AutoEventWireup="true" CodeBehind="messenger.aspx.cs" Inherits="RESTFulWCFService.User.messenger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="https://code.jquery.com/jquery-3.3.1.js" type="text/javascript"></script>
    <script src="<%#Constants__.WEB_ROOT_CDN %>/Scripts/json2.js" type="text/javascript"></script>
    <%--  <script src="../Scripts/jquery.signalR-2.2.2.min.js" type="text/javascript"></script>--%>
    <script src="<%#Constants__.WEB_ROOT_CDN %>/Scripts/jquery.signalR-2.2.2.min.js" type="text/javascript"></script>
    <%-- <script src="../Scripts/jquery.signalR-1.0.0-rc2.min.js" type="text/javascript"></script>--%>


    <script src="<%#Constants__.WEB_ROOT_CDN %>/signalr/hubs" type="text/javascript"></script>

    <script>
        var $j = $.noConflict(true);
        // Code that uses other library's $ can follow here.
    </script>
    <script type="text/javascript">
        $j(document).ready(function () {
            // Proxy created on the fly
            var chat = $j.connection.chatHub;
            chat.client.OnlineStatus = function (connectionId, userList, userList_name) {
                $j("img[id^=stat]").attr('src', 'image/offline.png');
                $j(userList).each(function (index, obj) {
                    var id = userList[index]
                    var name = userList_name[index];
                    var UserName = document.getElementById("<%# hdnUserName.ClientID %>").value;
                    if (UserName != name) {
                        $j('#ctl00_ContentPlaceHolder1_dtChatUserList').children('tbody').children('tr').each(function () {
                            var match = false;
                            $j(this).children('td').children('div').children('p').each(function () {
                                if ($j(this).text().toUpperCase().indexOf(name.toUpperCase()) > -1)
                                    match = true;
                            });
                            if (match) {
                                $j(this).children('td').children('div').insertBefore($('#ctl00_ContentPlaceHolder1_dtChatUserList_ctl00_user_div'));
                                var matched_val = $j(this).children('td').children('div').children('p');
                                //$j(this).html(matched_val.replace($j(txtsearch).val(), "<span class = 'highlight'>" + $j(txtsearch).val() + "</span>"));
                                $j(this).children('td').children('div').children('p').attr('data-userid', id);
                                $j(this).children('td').children('div').children('p').attr('class', 'UserItem online');
                                $j(this).children('td').children('div').children('p').children('span').children('span').children('img').attr('src', '../image/online.png');
                                //$j(this).show();
                                //count++;
                                $j('#ctl00_ContentPlaceHolder1_dtChatUserList').children('tbody').children('tr').show();
                            }
                            //else { $j(this).hide(); }
                        });
                    }
                    chat.server.createGroup($j('#ctl00_ContentPlaceHolder1_hdnUserId').val(), $j(this).attr('data-userid'));
                    var chatWindow = $j("#divChatWindow").clone(true);
                    $j(chatWindow).attr('chatToId', $j(this).attr('data-userid'));
                });

                if ($j(userList).length == 1) {
                    $j("#<%#lbl_user.ClientID%>").show();

                }
                else {

                    $j("#<%#lbl_user.ClientID%>").hide();
                }


            };

            chat.client.joined = function (connectionId, userList, userList_name) {
                $j(userList).each(function (index, obj) {
                    var id = userList[index]
                    var name = userList_name[index];
                    var UserName = document.getElementById("<%# hdnUserName.ClientID %>").value;
                    if (UserName != name) {
                        $j('#ctl00_ContentPlaceHolder1_dtChatUserList').children('tbody').children('tr').each(function () {
                            var match = false;
                            $j(this).children('td').children('div').children('p').each(function () {
                                if ($j(this).text().toUpperCase().indexOf(name.toUpperCase()) > -1)
                                    match = true;
                            });
                            if (match) {
                                $j(this).children('td').children('div').insertBefore($('#ctl00_ContentPlaceHolder1_dtChatUserList_ctl00_user_div'));
                                var matched_val = $j(this).children('td').children('div').children('p');
                                //$j(this).html(matched_val.replace($j(txtsearch).val(), "<span class = 'highlight'>" + $j(txtsearch).val() + "</span>"));
                                $j(this).children('td').children('div').children('p').attr('data-userid', id);
                                $j(this).children('td').children('div').children('p').attr('class', 'UserItem online');
                                $j(this).children('td').children('div').children('p').children('span').children('span').children('img').attr('src', '../image/online.png');
                                //$j(this).show();
                                //count++;
                                $j('#ctl00_ContentPlaceHolder1_dtChatUserList').children('tbody').children('tr').show();
                            }
                            //else { $j(this).hide(); }
                        });
                    }
                    chat.server.createGroup($j('#ctl00_ContentPlaceHolder1_hdnUserId').val(), $j(this).attr('data-userid'));
                    var chatWindow = $j("#divChatWindow").clone(true);
                    $j(chatWindow).attr('chatToId', $j(this).attr('data-userid'));
                });
            };

            chat.client.setChatWindow = function (strGroupName, strChatTo, currentUserId) {
                $j('div[chatToId=' + strChatTo + ']').attr('groupname', strGroupName);
                $j('div[chatToId=' + strChatTo + ']').attr('sender', currentUserId);
                $j('div[chatToId=' + strChatTo + ']').css('display', 'block')
            };
            // Declare a function on the chat hub so the server can invoke it
            chat.client.addMessage = function (message, groupName, from_id, from_name) {
                $j('#ctl00_ContentPlaceHolder1_dtChatUserList').children('tbody').children('tr').each(function () {
                    var match = false;
                    if (from_id == $j("#ctl00_ContentPlaceHolder1_hdnUserId").val()) { }
                    else
                    {
                        $j(this).children('td').children('p').each(function () {
                            if ($j(this).attr('data-userid') == from_id)
                                match = true;
                        });
                        if (match) {
                            //var matched_val = $j(this).children('td').children('p');
                            //$j(this).html(matched_val.replace($j(txtsearch).val(), "<span class = 'highlight'>" + $j(txtsearch).val() + "</span>"));
                            //$j(this).children('td').children('p').attr('data-userid', id);
                            if ($j('#divChatWindow').attr('groupname') == undefined) {
                                $j(this).children('td').children('p').addClass('msg_class')
                            }
                            else {
                                $j(this).children('td').children('p').removeClass('msg_class')
                            }//$j(this).show();
                            //count++;
                            //$j('#ctl00_ContentPlaceHolder1_dtChatUserList').children('tbody').children('tr').show();
                        }
                        //else { $j(this).hide(); }
                    }
                });

                if (from_id == $j("#ctl00_ContentPlaceHolder1_hdnUserId").val()) {
                    //$j('div[groupname=' + groupName + ']').find('ul').append('<LI style="margin-top: 8px; margin-left: 108px;word-wrap: break-word;text-align: justify;background-color: lightyellow;padding: 12px 13px 5px 5px;box-shadow: 1px slategray;border: 1px solid lightgrey;box-shadow: 3px 2px grey;">' + message + '');
                    $j('div[groupname=' + groupName + ']').find('ul').append('<LI class="media right-align-box">' + message + '');
                }
                else {
                    //$j('div[groupname=' + groupName + ']').find('ul').append('<LI style="margin-top: 8px;margin-right: 0px; word-wrap: break-word; margin-left:-38px;text-align: justify;padding: 12px 13px 5px 5px;box-shadow: 1px slategray;border: 1px solid lightgrey;box-shadow: 3px 2px grey;">' + message + '');
                    $j('div[groupname=' + groupName + ']').find('ul').append('<LI class="media left-align-box">' + message + '');
                    $j('div[groupname=' + groupName + ']').find('span').text(from_name);
                }



            };

            //};
            //chat.client.addsign = function (sign, groupName, from_id, from_name) {
            //    if (sign == '1') {
            //        if (from_id == $j("#ctl00_hdnUserId").val()) {
            //            //$j('div[groupname=' + groupName + ']').find('#typing_sign').innerHTML('');
            //        }
            //        else {
            //            //$j('div[groupname=' + groupName + ']').find('#typing_sign').style("display","block");
            //            $j('div[groupname=' + groupName + ']').find('#typing_sign').show();
            //            $j('div[groupname=' + groupName + ']').find('#typing_sign').text(from_name + ' is typing...');
            //            //$j('div[groupname=' + groupName + ']').find('span').text(from_name);
            //        }
            //    }
            //    else {
            //        $j('div[groupname=' + groupName + ']').find('#typing_sign').text('');
            //    }
            //};
            $j("#broadcast").click(function () {
                // Call the chat method on the server
                chat.server.send($j('#msg').val());
            });
            // Start the connection

            $j.connection.hub.start(function () {

                chat.server.getAllOnlineStatus();
            });
            $j(".ChatSend").click(function () {

                strChatText = $j('.ChatText', $j(this).parent()).val();
                if (strChatText != '') {
                    var strGroupName = $j(this).parent().attr('groupname');
                    var msg_sender = $j(this).parent().attr('sender');
                    var msg_reciever = $j(this).parent().attr('chattoid');
                    if (typeof strGroupName !== 'undefined' && strGroupName !== false) {
                        chat.server.send(strChatText, $j(this).parent().attr('groupname'), $j("#ctl00_ContentPlaceHolder1_hdnUserId").val(), $j("#ctl00_ContentPlaceHolder1_hdnUserName").val());
                        //chat.server.insert(msg_sender, msg_reciever,strGroupName, strChatText);
                        $j.ajax({
                            type: "Post",
                            contentType: "application/json; charset=utf-8",
                            url: $j("#ctl00_ContentPlaceHolder1_hdnWebMthodUrl1").val(),
                            dataType: "json",
                            data: "{'from_id':'" + msg_sender + "','group_id':'" + msg_reciever + "','message':'" + strChatText + "'}",
                            success: function (response) {
                                //alert('Done');
                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                                //alert(xhr.status);
                            }
                        });
                    }
                    $j('.ChatText', $j(this).parent()).find('ul').append(strChatText);
                    $j('.ChatText', $j(this).parent()).val('');
                    $j('.ChatText', $j(this).parent()).find('#typing_sign').val('');
                }
                return false;
            });
            $j(".ChatText").keydown(function () {
                var timer = null;
                clearTimeout(timer);
                timer = setTimeout(stopped_typing(this), 3000, Continue_typing(this))
            })
            function stopped_typing(obj) {
                var strGroupName = $j(obj).parent().attr('groupname');
                if (typeof strGroupName !== 'undefined' && strGroupName !== false)

                    //chat.server.send($j("#ctl00_hdnUserName").val() + ' : ' + strChatText, $j(this).parent().attr('groupname'), $j("#ctl00_hdnUserId").val(), $j("#ctl00_hdnUserName").val());
                    chat.server.signal_typing('0', $j(obj).parent().attr('groupname'), $j("#ctl00_ContentPlaceHolder1_hdnUserId").val(), $j("#ctl00_ContentPlaceHolder1_hdnUserName").val());
            }
            function Continue_typing(obj) {
                var strGroupName = $j(obj).parent().attr('groupname');
                if (typeof strGroupName !== 'undefined' && strGroupName !== false)

                    //chat.server.send($j("#ctl00_hdnUserName").val() + ' : ' + strChatText, $j(this).parent().attr('groupname'), $j("#ctl00_hdnUserId").val(), $j("#ctl00_hdnUserName").val());
                    chat.server.signal_typing('1', $j(obj).parent().attr('groupname'), $j("#ctl00_ContentPlaceHolder1_hdnUserId").val(), $j("#ctl00_ContentPlaceHolder1_hdnUserName").val());
            }
        });
        //this function can remove a array element.

        function Arrayremove(array, from, to) {


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

                    $j(test).css({ 'right': right + "px" });

                    right = right + 320;
                    $j(test).css({ 'display': 'block' });

                }
            }


            for (var jjj = iii; jjj < popups.length; jjj++) {

                var test = '.' + popups[iii];
                $j(test).css({ 'display': 'none' });
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

            var t = $j(obj).closest('#divChatWindow').attr('class');
            for (var iii = 0; iii < popups.length; iii++) {
                if (t == popups[iii]) {

                    $j(Arrayremove(popups, iii));
                    $j("." + t).css({ 'display': 'none' });
                    $j("." + t).remove();
                    //  document.getElementById(id).style.display = "none";

                    calculate_popups();

                    return;
                }
            }
        }



        function onchat_click(obj) {

            for (var iii = 0; iii < popups.length; iii++) {



                //already registered. Bring it to front.
                if ($j(obj).attr('data-userid') == popups[iii]) {

                    $j(Arrayremove(popups, iii));

                    popups.unshift($j(obj).attr('data-userid'));


                    calculate_popups();


                    return;
                }
            }

            var name = $j(obj).text();
            $j('#<%# lbl_username.ClientID %>').text(name);
              var chat = $j.connection.chatHub;

              if ($j(obj).hasClass('online')) {

                  chat.server.createGroup($j('#ctl00_ContentPlaceHolder1_hdnUserId').val(), $j(obj).attr('data-userid'));
                  //if ($j('#divChatWindow').attr('groupname') == undefined) {
                  var chatWindow = $j("#divChatWindow").clone(true);
                  //$j(chatWindow).css('display', 'block');
                  $j(chatWindow).attr('chatToId', $j(obj).attr('data-userid'));
                  $j(chatWindow).attr('Class', $j(obj).attr('data-userid'));

                  $j("#chatContainer").append(chatWindow);
                  //var d = $('#divChatWindow').children('#message_bx');
                  //alert($(d).height());
                  // $(d).scrollTop = $(d).height();}
                  //}
                  $('#divChatWindow').children('#message_bx').animate({
                      scrollTop: $('#divChatWindow').children('#message_bx').offset().top
                  },
                'fast');
                  $j(obj).removeClass('msg_class')
                  //d.animate({ scrollTop: d.prop('scrollHeight') }, 1000);
                  popups.unshift($j(obj).attr('data-userid'));

                  calculate_popups();
                  $j.ajax({
                      type: "POST",
                      contentType: "application/json; charset=utf-8",
                      url: $j("#ctl00_ContentPlaceHolder1_hdnWebMthodUrl").val(),
                      dataType: "json",
                      data: "{'from_id':'" + $j('#ctl00_ContentPlaceHolder1_hdnUserId').val() + "','to_id':'" + $j(obj).attr('data-userid') + "'}",
                      success: function (response) {
                          var customers = response.d;
                          $(customers).each(function () {
                              if (this.to == $j(obj).attr('data-userid')) {
                                  $j('div[chatToId=' + $j(obj).attr('data-userid') + ']').find('ul').append('<LI class="media right-align-box">' + this.message + '<br /><span style="font-size:x-small; padding-left:56px" class="text-primary">' + this.msg_date + '</span>');
                              }
                              else if (this.to == $j('#ctl00_ContentPlaceHolder1_hdnUserId').val()) {
                                  $j('div[chatToId=' + $j(obj).attr('data-userid') + ']').find('ul').append('<LI class="media left-align-box">' + this.message + '<br /><span style="font-size:x-small; padding-left:56px" class="text-primary">' + this.msg_date + '</span>');
                              }
                              // $('div[chatToId=' + $j(obj).attr('data-userid') + ']').scrollTop($('div[chatToId=' + $j(obj).attr('data-userid') + ']')[0].scrollHeight - $('div[chatToId=' + $j(obj).attr('data-userid') + ']')[0].clientHeight);
                          })
                      },
                      error: function (xhr, ajaxOptions, thrownError) {
                          alert(xhr.status);
                      }
                  });

              }
              else {
                  alert('User is offline!');

                  return false;
              }

          }



          //recalculate when window is loaded and also when window is resized.
          window.addEventListener("resize", calculate_popups);
          window.addEventListener("load", calculate_popups);
    </script>
    <style type="text/css">
        .ChatText
        {
            resize: none;
            overflow-y: scroll;
            margin-top: 4px;
        }
        .msg_class
        {
                text-shadow: 0px 1px red;
    box-shadow: 2px 2px grey;
        }
        .ChatSend
        {
            margin-left: 5px;
            position: relative;
            top: -15px;
            width: 45px;
            color: white;
        }

        .right-fixed-chat
        {
            /*background-color: #ff8a00;*/
            /*border-radius: 50%;*/
            padding: 5px;
            position: fixed;
            top: 270px;
            left: -5px;
            /*box-shadow: 1px 2px 6.5px #58aa48;*/
            cursor: pointer;
        }


        .chat-sidebar
        {
            width: 19%;
            position: fixed;
            height: 100%;
            right: -19px;
            top: 14%;
            padding-top: 10px;
            padding-bottom: 10px;
            border: 1px solid rgba(29, 49, 91, .3);
            overflow-y: scroll;
            display: none;
            background-color: #cb202d;
        }

        .sidebar-name
        {
            padding-left: 10px;
            padding-right: 10px;
            margin-bottom: 4px;
            font-size: 12px;
        }
    </style>
    <script>
        function search_users(txtsearch, div) {
            if ($(txtsearch).val() != "") {
                var count = 0;
                $(div).children('tbody').children('tr').each(function () {
                    var match = false;
                    $(this).children('td').children('p').each(function () {
                        if ($(this).text().toUpperCase().indexOf($(txtsearch).val().toUpperCase()) > -1)
                            match = true;
                    });
                    if (match) {
                        //var matched_val = $(this).children('td').children('p').html();
                        //$(this).html(matched_val.replace($(txtsearch).val(), "<span class = 'highlight'>" + $(txtsearch).val() + "</span>"));
                        $(this).show();
                        count++;
                    }
                    else { $(this).hide(); }
                });
                $('#spnCount').html((count) + ' match');
            }
            else {
                $(div).children('tbody').children('tr').each(function () {
                    $(this).show();
                });
                $('#spnCount').html('');
            }
        }
    </script>
    <style>
        .highlight
        {
            background-color: yellow;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
        <ProgressTemplate>
            <div class="overlay">
            </div>
            <div class="UpdateProgress_style">
                <img src="<%#Constants__.WEB_ROOT%>/Images/ajax-loader1.gif" alt="ajax" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:HiddenField ID="hdnUserId" runat="server" />
    <asp:HiddenField ID="hdnUserName" runat="server" />
    <asp:HiddenField ID="hdnWebMthodUrl" runat="server" />
    <asp:HiddenField ID="hdnWebMthodUrl1" runat="server" />
    <div id="overlay">
    </div>
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
    <asp:HiddenField ID="hdncountry" runat="server" />
    <asp:HiddenField ID="hdnpartnersubscribed" runat="server" />
    <section class="wrapper">

        <div class="container wrapper-content ">
            <div class="main-content ">

                <div class="row ">
                    <div class="col-sm-3" style="border: 1px solid grey; padding: 9px 4px 4px 8px; overflow-y: auto; height: 600px">
                        <center>Messenger
                            <hr />
                          <asp:TextBox ID="txtSearch" runat="server" Width="92%" CssClass="form-control" onkeyup="search_users(this,'#ctl00_ContentPlaceHolder1_dtChatUserList');"
                                placeholder="Search Messenger">
                            </asp:TextBox></center>
                        <asp:DataList ID="dtChatUserList" runat="server" OnItemDataBound="dtChatUserList_ItemDataBound">
                            <%--  <HeaderTemplate><p>Messenger </p></HeaderTemplate>--%>
                            <ItemTemplate>
                                <div id="user_div" runat="server">
                                <p onclick="onchat_click(this);" style="cursor: pointer">
                                    <span>
<%--                                        <img src="../image/online.png" />--%>
                                        <img src="../image/avator-male-1501786059.png" style="border-radius: 50%; height: 20%; width: 20%; padding: 11px 0 0 6px" /><span style="width:5%"><img id="img_status" style="width:6%" src="../image/offline.png" tooltip="Offline" /></span></span><%#Eval("massage_partner_name") %>
                                </p>
                                <hr style="margin-top: 0px; margin-bottom: 0px" />
                                <asp:HiddenField ID="hdnmassage_sk" runat="server" Value='<%#Eval("massage_partner_sk") %>' />
                                    </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div class="col-sm-9">
                        <h2 class="title text-center">Messages</h2>
                        <div class="message-box-section">
                            <div id="chatContainer">
                            </div>
                        </div>
                        <asp:HiddenField ID="hdnmsg" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="hdntomsg" runat="server" ClientIDMode="Static" />
                        <%--  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="Button1" runat="server" Text="Button" CssClass="hide" />
                            </ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </div>
                </div>
    </section>
    <asp:HiddenField ID="Merchant_Id" runat="server" />
    <asp:HiddenField ID="encRequest" runat="server" />
    <div>


        <div class="chat-sidebar" id="All_online">
            <div id="close_side_bar" style="cursor: pointer; color: white; width: 22px; border: 1px solid; margin-left: 0px; padding: 0 0 0 7px; margin-top: -10px;">x</div>
            <asp:Label ID="lbl_user" runat="server" Text="Please reload to refresh the list." Font-Bold="true" />
            <%--style="border: thin solid #C0C0C0; font-family: Tunga; width: 200px; font-weight: 500;font-size:14px;"  #6d84b4--%>
        </div>
        <%--<div id="divChatWindow" style="border: 1px solid #cb202d; width: 783px; display: none; margin-right: 0px; opacity: 1; background-color: white;">--%>
        <div id="divChatWindow" class="col-sm-8 message-box" style="display: none;">
            <div style="background-color: #cb202d; padding: 5px; color: white; font-weight: bold; font-size: 14px; clear: both;">

                <asp:Label ID="lbl_username" runat="server" />

                <a id="close_chat" onclick="close_popup(this);" style="top: 0; float: right; cursor: pointer;">&#10005;</a>
                <%--  <a class="boxclose" style="top: 0" onclick="closeDialog('divChatWindow');"></a>--%>

                <div style="clear: both"></div>
            </div>
            <div id="message_bx" style="border: 1px solid; overflow-y: scroll; height: 402px; background-color: white">
                <ul id="messages" style="list-style-type: none; width: 723px; font: normal 2 verdana"></ul>
            </div>
            <p id="typing_sign" style="display: block"></p>
            <asp:TextBox runat="server" ID="msg" CssClass="ChatText" Width="295px" BorderColor="#cb202d"
                BorderStyle="Solid" BorderWidth="1px" Height="35px" TextMode="MultiLine"></asp:TextBox>
            <asp:Button ID="btnChatSend" runat="server" CssClass="ChatSend" Text="Send" OnClientClick="return false"
                BackColor="#cb202d" Font-Size="Smaller" BorderStyle="Solid" BorderWidth="1px"
                BorderColor="#cb202d" Height="35px" Style="margin-bottom: -5px;" />
        </div>
    </div>
</asp:Content>

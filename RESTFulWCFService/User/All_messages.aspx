<%@ Page Title="" Language="C#" MasterPageFile="~/SearchPartener.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="All_messages.aspx.cs" Inherits="RESTFulWCFService.MassagePartener.User.All_messages" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Fup" %>
<%@ Register Src="~/Controls/DatePicker.ascx" TagName="uc" TagPrefix="asp" %>
<%@ Register Src="~/user control/WebUserControl1.ascx" TagName="UCPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Message | My Massage Partner</title>
   
 <meta name="Description" content="Check your inbox and reply to your massage partner  | My Massage Partner">
<meta name="Keywords" content="body massage nearby | erotic massage near me | happy ending massage near me | need massage | Full Body Massage | Female to Male Massage | Full body Massage | Sensual Massage">
    
    
    <script>
        function change_design(obj, btnShow) {
            //var div = document.getElementById(obj);
            var parentRow = $(btnShow).closest("tr");
            var div = parentRow.find('div[id=' + obj + ']');
            div.removeClass('media left-align-box');
            div.addClass('media right-align-box');


        }
        function change_visibility(wan, btnShow) {
            // alert();

            var parentRow = $(btnShow).closest("tr");
            var div = parentRow.find('div[id=reply_box]');
            //  alert(hiddenField.val());

            //return false;
            // alert(wan);
            if (wan == 'hide') {
                div.style.display = "none";
                //  alert('x');

            }
            else {
                //  alert('y');
                div.removeClass('hide');
            }
        }
        function funcall(btnShow) {

            var parentRow = $(btnShow).closest("tr");
            var msg = parentRow.find('textarea[id$=txtreply]');
            var hdnmsgto = parentRow.find('input[id$=hdnfromsk]');
            var obj1 = $('#hdnmsg');
            var obj2 = $('#hdntomsg');
            obj1.val(msg.val());
            obj2.val(hdnmsgto.val());
            document.getElementById('<%= Button1.ClientID%>').click();
            return false;
        }
        function Refresh_Page() {
            // alert('reload');
            location.reload();

        }

        ///Open Mobile nb Box
        function show_mobile(obj) {
            var parentRow = $(obj).closest("tr");
            var hdncontactno = parentRow.find('input[id$=hdncontactno]');
             $(obj).val(hdncontactno.val());
        };
        function show_mobile1(obj) {
            $(obj).val('Show Contact Number');
        };
    </script>
    <style>
        .hide
        {
            display: none;
        }
    </style>
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
    <div id="overlay">
    </div>
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
    <asp:HiddenField ID="hdncountry" runat="server" />
    <asp:HiddenField ID="hdnpartnersubscribed" runat="server" />
    <section class="wrapper">

        <div class="container wrapper-content ">
            <div class="main-content ">

                <div class="row ">
                    <div class="col-sm-12">
                        <h2 class="title text-center">No Messages Found!</h2>
                        <%--<div class="message-box-section">
                            <div class="row">
                                <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                                <ContentTemplate>--%>
                                <asp:DataList ID="DataList1" runat="server" OnItemDataBound="DataList1_ItemDataBound" OnItemCommand="DataList1_ItemCommand">
                                    <ItemStyle Width="1098" />
                                    <ItemTemplate>

                                        <div class="col-sm-2" id="image_bx" runat="server">
                                            <div class="image-box">
                                                <center>
                                           <asp:Image id="image1" runat="server" CssClass="img-responsive" ></asp:Image>
			                              <%--<image src="image/askpicture.jpg"  class="img-responsive"/>--%>
			            </center>

                                            </div>
                                            <br />
                                        </div>
                                        <div class="col-sm-8 message-box">
                                            <!-- left-aligned media object -->
                                            <div id="main_message_div" runat="server" class="media left-align-box">
                                                <div class="media-body">
                                                    <h4 class="media-heading sender-name"><%#Eval("from_name") %></h4>
                                                    <p><%#Eval("message_text") %></p>
                                                    <span class="text-primary"><%#Eval("message_datetime") %></span>

                                                    <%--<span style="margin-left: 9px" class="media-heading sender-name">--%>
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                                        <ContentTemplate>
                                                            <asp:LinkButton ID="lnkbtnreply" runat="server" CommandName="reply" CommandArgument='<%#Eval("to_massage_partner_sk") %>' OnClientClick="change_visibility('show',this);">Reply</asp:LinkButton><%--</span>--%>
                                                        </ContentTemplate>

                                                    </asp:UpdatePanel>
                                                    <asp:HiddenField ID="hdnfromsk" Value='<%#Eval("from_massage_partner_sk") %>' runat="server" ClientIDMode="Static" />
                                                </div>
                                            </div>
                                            <div id="reply_box" class="hide">
                                                <br />

                                                <asp:TextBox ID="txtreply" runat="server" TextMode="MultiLine" ClientIDMode="Static" class="form-control" Rows="3" placeholder="Type Your Message Here"></asp:TextBox>
                                                <span style="margin-left: 715px; margin-top: -51px; position: absolute;">
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="btnsend" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:LinkButton ID="btnsend" runat="server" CssClass="btn btn-gradiant" CommandName="send_reply" CommandArgument='<%#Eval("from_massage_partner_sk") %>' OnClientClick="funcall(this);">Send</asp:LinkButton>
                                                            <asp:LinkButton ID="btnsend1" runat="server" CssClass="btn btn-gradiant" CommandArgument='<%#Eval("to_massage_partner_sk") %>' OnClientClick="openpopuppaypal(); return false;">Send</asp:LinkButton>
                                                            </span>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                            </div>

                                            <!-- Right-aligned media object -->
                                            <%--      <div class="media right-align-box">
                                        <div class="media-body">
                                            <h4 class="media-heading sender-name">Right-aligned</h4>
                                            <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
                                            <span class="text-primary">31/08/2017 10:00pm</span>
                                        </div>

                                    </div>--%>


                                            <hr />
                                        </div>
                                        <div class="col-sm-2 text-center " id="divcontact" runat="server">
                                            <asp:Button ID="btnshowcontact" runat="server" CssClass="btn btn-gradiant" Text="Show Contact Number" OnClientClick="openpopuppaypal(); return false;" />
                                            <asp:Button ID="lblcontact" runat="server" CssClass="btn btn-gradiant" onmouseover="show_mobile(this)" onmouseout="show_mobile1(this)" OnClientClick="return false;" Text="Show Contact Number" />
                                            <%--<button type="submit" class="btn btn-gradiant ">Show Contact Number</button>--%>
                                        </div>
                                        <asp:HiddenField ID="hdncontactno" runat="server" Value='<%#Eval("contact_no") %>' ClientIDMode="Static" />
                                    </ItemTemplate>
                                </asp:DataList>
                                <%-- </ContentTemplate>
                                     
                                      </asp:UpdatePanel>--%>
                            </div>
                        </div>
                        <asp:HiddenField ID="hdnmsg" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="hdntomsg" runat="server" ClientIDMode="Static" />
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="Button1" runat="server" Text="Button" CssClass="hide" OnClick="Button1_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="row">
                            <div class="col-sm-12">
                                <center>
			    <ul class="pagination">
                    <uc1:UCPager ID="UCPager1" runat="server" />
                </ul>
			</center>
                            </div>
                        </div>
                    </div>
                </div>
    </section>
  </asp:Content>

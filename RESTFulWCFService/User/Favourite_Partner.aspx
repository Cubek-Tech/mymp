<%@ Page Title="" Language="C#" MasterPageFile="~/SearchPartener.Master" AutoEventWireup="true" CodeBehind="Favourite_Partner.aspx.cs" Inherits="RESTFulWCFService.MassagePartener.User.Favourite_Partner" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Fup" %>
<%@ Register Src="~/Controls/DatePicker.ascx" TagName="uc" TagPrefix="asp" %>
<%@ Register Src="~/user control/WebUserControl1.ascx" TagName="UCPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Favourite Massage Partner  | My Massage Partner</title>

    <meta name="Description" content="Check your favourite massage partner  | My Massage Partner"/>
    <meta name="Keywords" content="body massage nearby | erotic massage near me | happy ending massage near me | need massage | Full Body Massage | Female to Male Massage | Full body Massage | Sensual Massage"/>
    <script>
        function DispValue(btnShow) {
            var parentRow = $(btnShow).closest("tr");
            var hiddenField = parentRow.find('input[id$=hdn_massage_partner_sk]');
            var hdnto = $('#hdnto');
            hdnto.val(hiddenField.val());

            return false;
        }
        $(function () {
            $('#msg_close').click(function () { $('#ctl00_ContentPlaceHolder1_lblmsg').hide(); $('#ctl00_ContentPlaceHolder1_txtmessage').val(''); })
            $('#msg_close1').click(function () { $('#ctl00_ContentPlaceHolder1_lblmsg').hide(); $('#ctl00_ContentPlaceHolder1_txtmessage').val(''); })
            $('#ctl00_ContentPlaceHolder1_btnclose').click(function () { $('#ctl00_ContentPlaceHolder1_lblreportmsg').hide(); $('#ctl00_ContentPlaceHolder1_txtreport').val(''); })
            $('#report-close').click(function () { $('#ctl00_ContentPlaceHolder1_lblreportmsg').hide(); $('#ctl00_ContentPlaceHolder1_txtreport').val(''); })
        })
    </script>
    <!-----testing ---->
    <script>
        function DispValue1(btnShow) {
            var parentRow = $(btnShow).closest("tr");
            var lblname = parentRow.find('span[id$=lblname]');
            var lblcontact = parentRow.find('span[id$=lblcontact]');
            var hdnname = $('#hdnname');
            var hdncontact = $('#hdncontact');
            hdnname.val(lblname.text());
            hdncontact.val(lblcontact.text());
            return false;
        }
    </script>
    <style>
        #main_popup {
            border-radius: 5px 5px 5px 5px;
            height: auto;
            width: 585px;
            margin: 35px auto auto;
        }

        .showno {
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

        .box_Login2, .box_Login_wellcome_seek {
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

        .welcome_signup_page {
            background-color: #FFF;
            border-radius: 10px 10px 10px 10px;
            float: right;
            height: 164px;
            margin-right: 0;
            width: 424px;
            border: 12px solid #F0E68C;
        }

        .content_signup {
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

        a.boxcanceel {
            background: url(/Images/canceel.png) no-repeat scroll left top transparent;
            cursor: pointer;
            float: right;
            height: 22px;
            left: 0;
            position: relative;
            top: 0px;
            width: 22px;
        }

        .UpdateProgress_style {
            color: #FFFFFF;
            left: 72%;
            margin-left: -330px;
            position: fixed;
            top: 42%;
            z-index: 9999;
        }

        .change_pasd124 {
            height: auto !important;
            font-size: 12px;
            width: auto !important;
        }

        .yearcheckdiv {
            width: 33%;
            position: relative;
            float: left;
            text-align: center;
            align-items: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdnsource" runat="server" Value="x" />
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
    <asp:HiddenField ID="hdncountry" runat="server" />
    <asp:HiddenField ID="hdnpartnersubscribed" runat="server" />
    <input type="hidden" id="pageid" runat="server" />
    <section class="wrapper">
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
        <div class="container wrapper-content ">
            <div class="main-content ">
                <div class="row ">
                    <div class="col-sm-12">
                        <h2 class="title text-center">My Favourite Partners</h2>
                    </div>
                </div>
                <div class="row ">
                    <asp:DataList ID="DataList1" runat="server" OnItemDataBound="DataList1_ItemDataBound" OnItemCommand="DataList1_ItemCommand">
                        <ItemStyle Width="1098" />
                        <ItemTemplate>
                            <div class="partner-search-details" id="tr_row" runat="server">
                                <div class="row">
                                    <div class="col-sm-2">
                                        <div class="image-box">
                                            <center>
			                                    <asp:LinkButton ID="img_details" runat="server" CommandArgument='<%#Eval("massage_partner_sk") %>' CommandName="image_details" >
                                                <asp:Image id="img_partner" runat="server" CssClass="img-responsive"></asp:Image>
                                                </asp:LinkButton>
			                                </center>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="parner-details">
                                            <div class="details_massage">
                                                <div class="details">
                                                    <div class="details-left">Name :</div>
                                                    <span>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%#Eval("massage_partner_sk") %>' CommandName="name_details">
                                                                <asp:Label ID="lblname" runat="server" Text='<%#Eval("massage_partner_name") %>'></asp:Label>
                                                        </asp:LinkButton></span>
                                                </div>

                                                <div class="details">
                                                    <div class="details-left">Gender :</div>
                                                    <span>
                                                        <asp:Label ID="lblgender" runat="server" Text='<%#Eval("gender") %>'></asp:Label>
                                                        <asp:HiddenField ID="hdngender" Value='<%#Eval("gender") %>' runat="server" />
                                                    </span></div>
                                                <div class="details" id="divcontact_number" runat="server">
                                                    <div class="details-left">Contact no.:</div>
                                                    <span>
                                                        <asp:Label ID="lblcontact" runat="server" Text='<%#Eval("phone_nos") %>'></asp:Label>
                                                        <%-- <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                            <ContentTemplate>--%>
                                                        <asp:HiddenField ID="hdncontactnos" Value='<%#Eval("phone_nos") %>' runat="server" />
                                                        <asp:Button ID="btnshow" runat="server" CssClass="showno btn-gradiant" Text="Show Contact Number" OnClientClick="openpopuppaypal(); return false;" />
                                                        <%--  </ContentTemplate>
                                                                </asp:UpdatePanel>--%>

                                                    </span>
                                                </div>
                                                <div class="details">
                                                    <div class="details-left">Looking For :</div>
                                                    <span>
                                                        <asp:Label ID="lbllookingfor" runat="server" Text='<%#Eval("desired_gender") %>'></asp:Label></span></div>
                                                <div class="details">
                                                    <div class="details-left">Location:</div>
                                                    <span>
                                                        <asp:Label ID="lbllocation" runat="server" Text='<%#Eval("location") %>'></asp:Label></span></div>
                                                <div class="details" id="divmassagetype" runat="server">
                                                    <div class="details-left">Massage Type:</div>
                                                    <span>
                                                        <asp:Label ID="lblmassagetype" runat="server" Text='<%#Eval("specialty") %>'></asp:Label></span></div>
                                                
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="partner-link">
                                            <div class="link">
                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="lnkbtnsendsms" data-toggle="modal" data-target="#send-message-modal" runat="server" OnClientClick="DispValue(this);">Send Message</asp:LinkButton>
                                                        <asp:LinkButton ID="lnkbtnsendsms1" runat="server" OnClientClick="openpopuppaypal(); return false;">Send Message</asp:LinkButton>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <asp:HiddenField ID="hdn_massage_partner_sk" Value='<%#Eval("massage_partner_sk") %>' runat="server" />
                                                <%--  <a href="#" data-toggle="modal" data-target="#send-message-modal">Send Message</a>--%>
                                            </div>
                                            <div class="link">
                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="lnkfav" runat="server" CommandName="Favourite" CommandArgument='<%#Eval("massage_partner_sk") %>'>Favourite</asp:LinkButton>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="link">
                                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="lnkreport" data-toggle="modal" data-target="#report-abuse-modal" runat="server" CommandName="Report" OnClientClick="DispValue1(this);" CommandArgument='<%#Eval("massage_partner_sk") %>'>Report Abuse</asp:LinkButton>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="link">
                                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="lnkblck" runat="server" CommandName="Block" CommandArgument='<%#Eval("massage_partner_sk") %>'>Block</asp:LinkButton>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                    <div class="row">
                        <div class="col-sm-12">
                            <center>
			    <ul class="pagination">
                           <%--  <asp:Repeater ID="rptPager" runat="server">
                               
    <ItemTemplate>
        <li><asp:LinkButton ID="lnkPage" CssClass="pagination" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
            OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton></li>
   </ItemTemplate>
</asp:Repeater>--%>
                    <uc1:UCPager ID="UCPager1" runat="server" />
                </ul>
			</center>
                        </div>

                    </div>
                </div>
            </div>
    </section>
    <asp:HiddenField ID="hdnto" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnname" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdncontact" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="Merchant_Id" runat="server" />
    <asp:HiddenField ID="encRequest" runat="server" />
    <!------send-message Modal popup start ----->
    <div class="modal fade" id="send-message-modal" role="dialog">
        <div class="modal-dialog ">
            <!-- Modal content-->
            <div class="modal-content ">
                <div class="modal-header model-header-orange">
                    <button type="button" id="msg_close1" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title text-center ">Send Message</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-12">
                            Message:
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtmessage" ErrorMessage="Required" ForeColor="Red" ValidationGroup="message"></asp:RequiredFieldValidator>
                            <%-- <div class="alert alert-danger" role="alert">
                                You cannot send messages as a Free member.<br>
                                You may however reply to a message if it was sent by a Gold member (username has a gold star).<br>
                                <a href="#">To upgrade your account, please click here.</a>
                            </div>--%>
                            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnsend" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:TextBox ID="txtmessage" runat="server" TextMode="MultiLine" CssClass="form-control" placeholder="Type Your Message Here" Rows="3"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btnsend" runat="server" CssClass="btn btn-gradiant" ValidationGroup="message" OnClick="btnsend_Click" Text="Send" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <%--<button type="button" class="btn btn-gradiant" data-dismiss="modal">Send</button>--%>
                    <button type="button" id="msg_close" class="btn btn-default" data-dismiss="modal">Close</button>
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnsend" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Label ID="lblmsg" runat="server" Text="message sent successfully!!" ForeColor="Green" Visible="false"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>

        </div>
    </div>

    <!------send-message Modal popup End ----->


    <!------Report Abuse Modal popup start ----->
    <div class="modal fade" id="report-abuse-modal" role="dialog">
        <div class="modal-dialog ">

            <!-- Modal content-->
            <div class="modal-content ">
                <div class="modal-header model-header-orange">
                    <button type="button" id="report-close" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title text-center ">Report abuse</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">
                                <label>Please enter the reason you want to report: </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtreport" ErrorMessage="Required" ForeColor="Red" ValidationGroup="report"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtreport" runat="server" class="form-control" Rows="2" placeholder="Reply..." TextMode="MultiLine"></asp:TextBox>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnreport" runat="server" Text="Submit" ValidationGroup="report" class="btn btn-gradiant" OnClick="btnreport_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnclose" runat="server" Text="Close" data-dismiss="modal" CssClass="btn btn-default" />
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnreport" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Label ID="lblreportmsg" runat="server" Text="message sent successfully!!" ForeColor="Green" Visible="false"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

        </div>
    </div>

    <!------Report Abuse Modal popup End ----->
    <div id="main_popup" class="box_Login2" style="display: none; left: -15%">
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
    <!------Report Abuse Modal popup End ----->
</asp:Content>

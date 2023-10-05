<%@ Page Title="" Language="C#" MasterPageFile="~/SearchPartener.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="SearchPartener.aspx.cs" Inherits="RESTFulWCFService.MassagePartener.SearchPartener1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Fup" %>
<%@ Register Src="~/user control/WebUserControl1.ascx" TagName="UCPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
    <script type="text/javascript">
        function addParameterToURL(param) {
            var url = window.location.href;
            var hash = url.substring(url.lastIndexOf('/') + 1);
            if (history.pushState) {
                var newurl = window.location.protocol + "//" + window.location.host + window.location.pathname + "?page=" + param;
                window.history.pushState({ path: newurl }, '', newurl);
                //      window.location.href = window.location.href;
            }
        }
        $(document).ready(function () {
            var val = $('#ctl00_ContentPlaceHolder1_UCPager1_hdpage').val();

            $('#ctl00_ContentPlaceHolder1_hdntotalpages_val').val(val);
            var total_pages = $('#ctl00_ContentPlaceHolder1_UCPager1_hdntotalpages').val();
            //$('#ctl00_ContentPlaceHolder1_hdntotalpages_val').val(total_pages);
            //alert(total_pages);
            //alert(val);
            if (val == 1) {
                var url = window.location.href;
                var hash = url.substring(url.lastIndexOf('/') + 1);
                if (history.pushState) {
                    var newurl = window.location.protocol + "//" + window.location.host + window.location.pathname;
                    window.history.pushState({ path: newurl }, '', newurl);
                }
            }
            if (val > 1) {
                //alert(total_pages);
                //$('#ctl00_head_title_page').text().replace("| MyMassagePartner", "| page " + val + " - MyMassagePartner")
                //$('#ctl00_head_title_page').text().replace("| MyMassagePartner", "| page " + val + " - MyMassagePartner")
                //document.title = $('#ctl00_head_title_page').text().replace("| MyMassagePartner", "| page " + val + " - MyMassagePartner");
                if (parseInt(val) > parseInt(total_pages)) {
                    var url = window.location.href;
                    var hash = url.substring(url.lastIndexOf('/') + 1);
                    if (history.pushState) {
                        var newurl = window.location.protocol + "//" + window.location.host + window.location.pathname;
                        window.history.pushState({ path: newurl }, '', newurl);
                    }
                    var til = $('#ctl00_head_title_page').text();
                    var has = til.substring(til.lastIndexOf('|') + 1);
                    til = til.replace(has, " MyMassagePartner");
                    document.title = til;

                }
                else {
                    //var url = window.location.href;
                    //var hash = url.substring(url.lastIndexOf('/') + 1);
                    //if (history.pushState) {
                    //    var newurl = window.location.protocol + "//" + window.location.host + window.location.pathname;
                    //    window.history.pushState({ path: newurl }, '', newurl);
                    //}
                    var til = $('#ctl00_head_title_page').text();
                    var has = til.substring(til.lastIndexOf('|') + 1);
                    til = til.replace(has, " Page " + val + " - MyMassagePartner");
                    document.title = til;
                    addParameterToURL(val);
                }
            }
            //$('#ctl00_ContentPlaceHolder1_UCPager1_hdpage').val('');

        })
    </script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
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
.payment-logo {
    max-height: 40px;
    display: block;
    margin: 5px auto;
}
        #main_popup1 {
            border-radius: 5px 5px 5px 5px;
            height: auto;
            width: 100%;
            margin: 35px auto auto;
            max-width: 100% !important;
        }
.payment-logo-top {
            position: absolute;
            top: -70px;
            max-height: 65px;
            display: block;
            margin: 5px auto;
        }
/*.form-group {
    margin-bottom: 5px;
}*/
.modal-title {
    margin: 0;
    line-height: 1;
}
    
        .foot-azam {
            padding-top: 15px;
            color: white;
            text-decoration: underline;
        }

        /*/*a:hover
        {
            color: yellow;
        }*/

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
            max-width: 100% !important;
            z-index: 1001;
            border-radius: 10px;
            -moz-border-radius: 10px;
            opacity: 6;
        }

     a.boxcanceel {
            background: url(/Images/close.png) no-repeat scroll left top transparent;
            cursor: pointer;
            float: right;
            height: 22px;
            left: 0;
            position: relative;
            top: 0px;
            width: 22px;
        }

        /*.UpdateProgress_style {
            color: #FFFFFF;
            left: 72%;
            margin-left: -330px;
            position: fixed;
            top: 42%;
            z-index: 9999;
        }*/

        .change_pasd124 {
            height: auto !important;
            font-size: 12px;
            width: auto !important;
            overflow-y: scroll;
            overflow-x: hidden;
        }

        @media only screen and (max-width: 786x) {
            .change_pasd124 {
                max-height: 400;
            }
        }

        .yearcheckdiv {
            width: 33%;
            position: relative;
            float: left;
            text-align: center;
            align-items: center;
        }

        .banner_mob {
            display: none;
        }
        /* when screen is less than 600px wide
     show mobile version and hide desktop */
        @media (max-width: 600px) {
            .banner_mob {
                display: block;
            }

            .banner_desk {
                display: none;
            }

            .search_text__main566 {
                padding: 3px 10px;
                height: 27px;
                margin-bottom: 5px;
            }
        }
    </style>
    <script src="<%=Constants__.WEB_ROOT %>/js/jquery.scrollToTop.min.js"></script>
     <script type="text/javascript">
        $(function () {
            $("#toTop").scrollToTop(1000);
        });

        window.onscroll = function () { scrollFunction() };
        function scrollFunction() {
            if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
                $("#toTop").css("display", "block");
            } else {

                $("#toTop").css("display", "none");
            }
        }
    </script>
     <style>
        #toTop {
            display: none;
            position: fixed;
            bottom: 5px;
            right: -23px;
            width: 90px;
            height: 85px;
            background-repeat: no-repeat;
            opacity: .4;
            background-image: url(/img/up-arrow-icon.png);
        }

            #toTop:hover {
                opacity: .8;
                filter: alpha(opacity=80);
            }
    </style>
     <style type="text/css">
        .payment-mdl-popup .modal-header {
            background-color: #000;
        }

            .payment-mdl-popup .modal-header .modal-title {
                color: #fff;
            }

            .payment-mdl-popup .modal-header .close {
                color: #fff;
                opacity: 1;
            }popuppaypal

        .payment-mdl-popup table tr th {
            background-color: #000;
            color: #fff;
        }

        .btn-red, .btn-red:hover,
        .btn-red:active {
            color: #fff;
            background-color: red;
        }

        #feedbackimage {
            display: none;
        }

        @media only screen and (max-width: 768px) {
            #feedbackimage {
                display: none;
            }
        }
          .btn345 {
            color: #fff;
            background-color: #5bc0de;
            border-color: #46b8da;
            display: inline-block;
            padding: 0px 12px;
            margin-bottom: 0;
            font-size: 14px;
            font-weight: normal;
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
    </style>
     <title id="title_page" runat="server"></title>
    <meta id="metaDescription" runat="server" name="Description" content="" />
    <meta id="keywordcontent" runat="server" name="Keywords" content="" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdnsource" runat="server" Value="x" />
        <button type="button" id="btn_dvseekerpopuppayment" class="btn btn-info" data-toggle="modal" style="display: none;"
            data-target="#popuppaypal">
        </button>
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
    <asp:HiddenField ID="hdnpartnersubscribed" runat="server" />
    <asp:HiddenField ID="hdntotalpages_val" runat="server" />
    <asp:HiddenField ID="hdnpayforpartner" runat="server" />
    <input type="hidden" id="pageid" runat="server" />
    <section class="wrapper">
        <div class="container wrapper-content ">
            <div class="main-content ">
                <div class="row "> 
                <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                    <ContentTemplate>
                    <div class="col-sm-12">
                        <span id="links_srp" runat="server"></span>
                        <br />
                       <h1 id="page_title" runat="server" style="font-size: 1.5em; text-transform:capitalize"></h1><div id="li_whatsapp_share" runat="server" visible="false" style="display:none"><div class="st-custom-button" data-network="whatsapp"><img src="<%=Constants__.WEB_ROOT %>/img/whatsapp-share.png" height="35px" style="border-radius: 4px;" /></div></div>
                        <div class="partner-search-form">
                            <h4 class="title text-center"></h4>
                            <div class="row">
                                <div class="col-sm-6" id="div_left" runat="server">
                                    <div class="row">
                                        <div class="col-sm-3 padding5" style="display: none">
                                            <div class="form-group">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlcountry" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlcountry" Display="Dynamic" ForeColor="Red" ValidationGroup="search" ErrorMessage="*Required"></asp:RequiredFieldValidator>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 padding5">
                                            <div class="form-group">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlcountry" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlstate" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged"></asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 padding5">
                                            <div class="form-group">
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlcountry" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlstate" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlcity" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlcity_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 padding5">
                                            <div class="form-group">
                                                <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlcountry" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlstate" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlcity" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlarea" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                         <div class="col-sm-3 padding5" id="div_Gender" runat="server">
                                            <div class="form-group">
                                                <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlmassagetypes" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlgender" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlgender_SelectedIndexChanged">
                                                            <asp:ListItem Value="0" Selected>Looking for</asp:ListItem>
                                                            <asp:ListItem Value="M">Male</asp:ListItem>
                                                            <asp:ListItem Value="F">Female</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 padding5" id="div_Partner_Types" runat="server">
                                            <div class="form-group">
                                                <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlmassagetypes" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlPartner_Types" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPartner_Types_SelectedIndexChanged">
                                                            <asp:ListItem Value="0" Selected>From - to</asp:ListItem>
                                                            <asp:ListItem Value="FM">Female partner to Male partner</asp:ListItem>
                                                            <asp:ListItem Value="FF">Female partner to Female partner</asp:ListItem>
                                                            <asp:ListItem Value="MF">Male partner to Female partner</asp:ListItem>
                                                            <asp:ListItem Value="MM">Male partner to Male partner</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6" id="div_right" runat="server">
                                    <div class="row">
                                        <div class="col-sm-3 padding5" id="dev_Age" runat="server">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlage" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-sm-3 padding5" id="div_Looking_fr" runat="server">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddllookingfor" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="Interested In" Value="0" Selected></asp:ListItem>
                                                    <asp:ListItem Value="T">Massage Therapist/Professional</asp:ListItem>
                                                    <asp:ListItem Value="M">Manual Massager/Enthusiast</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                       <div class="col-sm-3 padding5" id="div_massage_types" runat="server">
                                            <div class="form-group">
                                                <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlgender" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlPartner_Types" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlmassagetypes" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlmassagetypes_SelectedIndexChanged" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 padding5" id="div_OutCall" runat="server">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlOutCall" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="OutCall" Value="0" Selected></asp:ListItem>
                                                    <asp:ListItem Value="Home">At Home</asp:ListItem>
                                                    <asp:ListItem Value="Hotel">At Hotel</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <asp:Button ID="btnsearch" runat="server" CssClass="btn btn-gradiant search-main" ValidationGroup="search" Text="Search" OnClick="btnsearch_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                       </ContentTemplate>
                        </asp:UpdatePanel>
                    <br />
                    <p id="des" runat="server" style="text-align: justify"></p>
                    <%--   <br />
                <img src="<%=Constants__.WEB_ROOT %>/image/poster_mp.png" />
                    <br />--%>
                    <img src="<%=Constants__.WEB_ROOT %>/image/bane.jpg" class="img-responsive banner_desk" />
                    <%-- <img src="<%=Constants__.WEB_ROOT %>/image/mymp_4.jpg" class="img-responsive banner_desk" />
                    <img src="<%=Constants__.WEB_ROOT %>/image/mymp_1.jpeg" class="img-responsive banner_mob" />--%>
                    <img src="<%=Constants__.WEB_ROOT %>/image/bane2.jpg" class="img-responsive banner_mob" />
                    <br />
                    <asp:DataList ID="DataList1" runat="server" OnItemDataBound="DataList1_ItemDataBound" OnItemCommand="DataList1_ItemCommand" Width="100%">
                        <ItemStyle Width="100%" />
                        <ItemTemplate>
                            <div class="partner-search-details" id="tr_row" runat="server">
                                <div class="row">
                                    <div class="col-sm-2">
                                        <div class="image-box">
                                            <center>
			    <%--<image src="image/askpicture.jpg"  class="img-responsive"/>--%>
                                                <asp:LinkButton ID="img_details" runat="server" CommandArgument='<%#Eval("massage_partner_sk") %>' CommandName="image_details" />
                                                <asp:Image id="img_partner" runat="server" CssClass="img-responsive"></asp:Image>
                                                </asp:Link Button>
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
                                                    </span>
                                                </div>
                                                <div class="details" id="divcontact_number" runat="server">
                                                    <div class="details-left">Contact no:</div>
                                                    <span>
                                                        <asp:Label ID="lblcontact" runat="server" Text='<%#Eval("phone_nos") %>'></asp:Label>
                                                        <asp:HiddenField ID="hdncontactnos" Value='<%#Eval("phone_nos") %>' runat="server" />
                                                        <%-- <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                            <ContentTemplate>--%>
                                                        <asp:Button ID="btnshow" runat="server" CssClass="showno btn-gradiant" Text="Show Contact Number" OnClientClick="openpopuppaypal(); return false;" />
                                                        <asp:Button ID="btnshow1" runat="server" CssClass="showno btn-gradiant" data-toggle="modal" data-target="#login-modal" OnClientClick="return false;" Text="Show Contact Number" />
                                                        <%--  </ContentTemplate>
                                                                </asp:UpdatePanel>--%>

                                                    </span>
                                                </div>
                                                <div class="details">
                                                    <div class="details-left">Looking For :</div>
                                                    <span>
                                                        <asp:Label ID="lbllookingfor" runat="server" Text='<%#Eval("desired_gender") %>'></asp:Label></span>
                                                </div>
                                                <div class="details">
                                                    <div class="details-left">Location:</div>
                                                    <span>
                                                        <asp:Label ID="lbllocation" runat="server" Text='<%#Eval("location") %>'></asp:Label></span>
                                                </div>
                                                <div class="details" id="divmassagetype" runat="server">
                                                    <div class="details-left">Massage Type:</div>
                                                    <span>
                                                        <asp:Label ID="lblmassagetype" runat="server" Text='<%#Eval("specialty") %>'></asp:Label>
                                                    </span>
                                                </div>
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
                                                        <asp:LinkButton ID="lnkbtnsendsms2" runat="server" data-toggle="modal" data-target="#login-modal">Send Message</asp:LinkButton>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <asp:HiddenField ID="hdn_massage_partner_sk" Value='<%#Eval("massage_partner_sk") %>' runat="server" />
                                                <%--  <a href="#" data-toggle="modal" data-target="#send-message-modal">Send Message</a>--%>
                                            </div>
                                            <div class="link">
                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="lnkfav" runat="server" CommandName="Favourite" CommandArgument='<%#Eval("massage_partner_sk") %>'>Favourite</asp:LinkButton>
                                                        <asp:LinkButton ID="lnkfav1" runat="server" data-toggle="modal" data-target="#login-modal">Favourite</asp:LinkButton>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="link">
                                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="lnkreport" data-toggle="modal" data-target="#report-abuse-modal" runat="server" CommandName="Report" OnClientClick="DispValue1(this);" CommandArgument='<%#Eval("massage_partner_sk") %>'>Report Abuse</asp:LinkButton>
                                                        <asp:LinkButton ID="lnkreport1" data-toggle="modal" data-target="#login-modal" runat="server" CommandArgument='<%#Eval("massage_partner_sk") %>'>Report Abuse</asp:LinkButton>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="link">
                                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="lnkblck" runat="server" CommandName="Block" CommandArgument='<%#Eval("massage_partner_sk") %>'>Block</asp:LinkButton>
                                                        <asp:LinkButton ID="lnkblck1" runat="server" data-toggle="modal" data-target="#login-modal">Block</asp:LinkButton>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="link" id="payForPartner" runat="server">
                                                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="LinkButton2" CssClass="showno btn-partner-pay" CommandName="Payforpartner" CommandArgument='<%#Eval("massage_partner_sk") %>' runat="server">Pay for Partner </asp:LinkButton>
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
                    </ContentTemplate>
                        </asp:UpdatePanel>
            </div>
           
         </div> 
            
    </section>
    <div class="wrapper" style="display:none">
     <div class="container xyz">
            <div class="row">
                <div class="col-sm-12 m2b_link_color">
                    <asp:UpdatePanel ID="UpdatePanel32" runat="server"><ContentTemplate>
                     <a ID="lnk_m2b_Srp1" runat="server" class="m2b_link" target="_blank"><u>Looking for body massage in Parlor or Spa only?</u></a>
                    </ContentTemplate></asp:UpdatePanel>
                        </div>
                </div>
         </div>
        </div>
    <style type="text/css">
        .m2b_link {
        float: left;
    transform: translate(-50%, 0%);
    top: 50%;
    color: white;
    font-weight: 600;
    font-size: 15px;
    left: 50%;
    position: relative;
}
        .m2b_link_color {
            background-color:black
        }
        @media screen and (max-width: 768px) {
     .m2b_link {
        float: left;
    transform: translate(-50%, 0%);
    top: 50%;
    color: white;
    font-weight: 600;
    font-size: 13px;
    left: 50%;
    position: relative;
}
        .xyz {
            background-color:black
        }

}
    </style>
    <asp:HiddenField ID="hdnto" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnname" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdncontact" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="Merchant_Id" runat="server" />
    <asp:HiddenField ID="encRequest" runat="server" />
    <div class="reviews-section" id="reviews" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-sm-12">
                    <h1 class="title">Top Reviews:</h1>
                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                        <ContentTemplate>
                            <asp:DataList ID="dlReviews" runat="server" OnItemDataBound="dlReviews_ItemDataBound">
                                <ItemTemplate>
                                    <div class="reviews-box">
                                        <h4><strong><%#Eval("name") %> :</strong>
                                            <span>
                                                <asp:Image ID="rating1" runat="server" />
                                            </span>
                                            <span>
                                                <asp:Image ID="rating2" runat="server" />
                                            </span>
                                            <span>
                                                <asp:Image ID="rating3" runat="server" />
                                            </span>
                                            <span>
                                                <asp:Image ID="rating4" runat="server" />
                                            </span>
                                            <span>
                                                <asp:Image ID="rating5" runat="server" />
                                            </span>
                                        </h4>
                                        <p>
                                            <%#Eval("review") %>
                                        </p>
                                    </div>
                                    <asp:HiddenField ID="HdnRatings" Value='<%#Eval("rating") %>' runat="server" />
                                    <hr />
                                </ItemTemplate>
                            </asp:DataList>
                            <asp:LinkButton ID="lnkSeemoreReview" runat="server" Text="See More.." ToolTip="Click to seemore reviews"
                                Style="font-size: 18px; margin: 10px; color: #000; font-weight: 100;" OnClick="lnkSeemoreReview_Click">
                                                                                                                                    
                            </asp:LinkButton>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
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
    <script type="text/javascript">


        function closeDialogPaypal(prospectElementID) {

            $(function ($) {

                $(document).ready(function () {
                    $('#' + prospectElementID).css('position', 'absolute');
                    // $('#' + prospectElementID).animate({ 'left': '-100%' }, 1200, function() {
                    $('#' + prospectElementID).css('position', 'fixed');
                    $('#' + prospectElementID).css('display', 'none');
                    $('#' + prospectElementID).css('left', '100%');
                    $('#overlay').fadeOut('fast');
                    // $('#overlay3').fadeOut('fast');

                });
                // });
            });
            //ResetChangePsw();
            // openOffersDialog();
        }

        $(document).ready(function () {


            $('[id$=View1]').css("display", "block");
            $('#Tab1').removeClass("Initial");
            $('#Tab1').addClass("Btn Clicked");
            ////////////////////
            $('[id$=View2]').css("display", "none");
            $('#Tab2').removeClass("Btn Clicked");
            $('#Tab2').addClass("Initial");
            ///////////////////
            $('[id$=View3]').css("display", "none");
            $('#Tab3').removeClass("Btn Clicked");
            $('#Tab3').addClass("Initial");
            $('[id$=View4]').css("display", "none");
            $('#Tab4').removeClass("Btn Clicked");
            $('#Tab4').addClass("Initial");

            var variable = "101"
            // $("select option:contains('Value " + variable + "')").attr("disabled", "disabled");
            $("select option[value='" + variable + "']").attr('disabled', true);

            //////////////////////////////////
            $('#Tab1').click(function () {

                $('[id$=View1]').css("display", "block");
                $('#Tab1').removeClass("Initial");
                $('#Tab1').addClass("Btn Clicked");

                ////////////////////

                $('[id$=View2]').css("display", "none");
                $('#Tab2').removeClass("Btn Clicked");
                $('#Tab2').addClass("Initial");

                ///////////////////

                $('[id$=View3]').css("display", "none");
                $('#Tab3').removeClass("Btn Clicked");
                $('#Tab3').addClass("Initial");

                ///////////////////

                $('[id$=View4]').css("display", "none");
                $('#Tab4').removeClass("Btn Clicked");
                $('#Tab4').addClass("Initial");


            });


            $('#Tab2').click(function () {

                $('[id$=View1]').css("display", "none");
                $('#Tab1').removeClass("Btn Clicked");
                $('#Tab1').addClass("Initial");


                ///////////////////

                $('[id$=View2]').css("display", "block");
                $('#Tab2').removeClass("Initial");
                $('#Tab2').addClass("Btn Clicked");


                ///////////////////


                $('[id$=View3]').css("display", "none");
                $('#Tab3').removeClass("Btn Clicked");
                $('#Tab3').addClass("Initial");

                ///////////////////

                $('[id$=View4]').css("display", "none");
                $('#Tab4').removeClass("Btn Clicked");
                $('#Tab4').addClass("Initial");

            });


            $('#Tab3').click(function () {

                $('[id$=View1]').css("display", "none");
                $('#Tab1').removeClass("Btn Clicked");
                $('#Tab1').addClass("Initial");

                ///////////////////

                $('[id$=View2]').css("display", "none");
                $('#Tab2').removeClass("Btn Clicked");
                $('#Tab2').addClass("Initial");

                ///////////////////

                $('[id$=View3]').css("display", "block");
                $('#Tab3').removeClass("Initial");
                $('#Tab3').addClass("Btn Clicked");

                ///////////////////

                $('[id$=View4]').css("display", "none");
                $('#Tab4').removeClass("Btn Clicked");
                $('#Tab4').addClass("Initial");

            });



            $('#Tab4').click(function () {

                $('[id$=View1]').css("display", "none");
                $('#Tab1').removeClass("Btn Clicked");
                $('#Tab1').addClass("Initial");

                ///////////////////

                $('[id$=View2]').css("display", "none");
                $('#Tab2').removeClass("Btn Clicked");
                $('#Tab2').addClass("Initial");

                ///////////////////

                $('[id$=View3]').css("display", "none");
                $('#Tab3').removeClass("Btn Clicked");
                $('#Tab3').addClass("Initial");

                ///////////////////

                $('[id$=View4]').css("display", "block");
                $('#Tab4').removeClass("Initial");
                $('#Tab4').addClass("Btn Clicked");

            });


        });
        function openpopuppaypal() {

            //document.getElementById('overlay_master').className = 'overlay';
            //$('#overlay_master').fadeIn('fast', function () {
            //    $('#popuppaypal1').css('display', 'block');
            //    $('#popuppaypal1').animate({ 'left': '24%' }, 500);
            //});
            $("#btn_dvseekerpopuppayment").click();
            return false;
        }
        function openOffersDialog() {
            document.getElementById('overlay').className = 'overlay';
            $('#overlay').fadeIn('fast', function () {
                $('#main_popup1').css('display', 'block');
                $('#main_popup1').animate({ 'left': '-28%' }, 500);

            });
        }

        function open_already_exist_popup(msg) {
            document.getElementById('overlay2').className = 'overlay';
            document.getElementById('msg_container').innerHTML = msg;
            $('#overlay2').fadeIn('fast', function () {

                $('#box_email_id_exist_popup').css('display', 'block');
                $('#box_email_id_exist_popup').animate({ 'left': '50%', 'margin-left': '-250px' }, 100);
            });

        }

        function email_already_exist_container_multi(msg) {
            document.getElementById('overlay2').className = 'overlay';
            document.getElementById('msg_container_multi').innerHTML = msg;
            $('#overlay2').fadeIn('fast', function () {

                $('#email_already_exist_container_multi').css('display', 'block');
                $('#email_already_exist_container_multi').animate({ 'left': '50%', 'margin-left': '-250px' }, 100);
            });

        }



        function popup_success_multi(msg) {
            document.getElementById('overlay2').className = 'overlay';
            document.getElementById('popup_success_contain').innerHTML = msg;
            $('#overlay2').fadeIn('fast', function () {
                $('#popup_success_multi').css('display', 'block');
                $('#popup_success_multi').animate({ 'left': '50%', 'margin-left': '-250px' }, 100);
            });
        }


        function closeDialog(prospectElementID) {
            $(function ($) {
                $(document).ready(function () {
                    $('#' + prospectElementID).css('position', 'absolute');
                    $('#' + prospectElementID).css('display', 'none');
                    $('#' + prospectElementID).css('position', 'fixed');
                    $('#' + prospectElementID).css('left', '100%');
                    $('#overlay2').fadeOut('fast');
                });

            });
        }


        function closeOffersDialog11(prospectElementID) {
            $(function ($) {
                $(document).ready(function () {
                    window.close();
                    window.opener.location.reload(true);
                    window.opener.reload();

                });
            });
        }


        function closeOffersDialog(prospectElementID) {
            // clearData();
            $(function ($) {
                $(document).ready(function () {
                    $('#main_popup1').css('display', 'none');
                    $('#' + prospectElementID).css('position', 'absolute');
                    $('#' + prospectElementID).css('position', 'fixed');
                    $('#' + prospectElementID).css('left', '100%');
                    $('#overlay').fadeOut('fast');
                    document.location.reload();
                });
                // });
            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <style type="text/css">
        .tablelinkcss
        {
            width: 105%;
            border-collapse: collapse;
            border: none;
            color: white;
            text-decoration: underline;
        }

            .tablelinkcss td, th
            {
                border: 0px solid #999;
                padding: 0.5rem;
                text-align: left;
            }

        .divcss34
        {
            font-size: 10px;
            padding-top: 10px;
            margin-top: 0px;
            padding-bottom: 10px;
            text-align: -webkit-center;
            text-align: -moz-center;
        }

        .tablelinkcss a
        {
            text-decoration: none !important;
            font: 11px;
            white-space: pre;
            color: #fffdfd;
        }

            .tablelinkcss a:hover
            {
                text-decoration: underline;
                color: #BF7C07;
            }

        .newtdcss
        {
            /*background:url(<%#  Page.ResolveUrl("~/ma.ico") %>) 2px 9px no-repeat;
          padding-left: 24px !important;   */
            font-size: 11px;
        }
    </style>
    <asp:UpdatePanel runat="server" ID="UpdatePanel23">
        <ContentTemplate>
            <div class="container">
                <div class="row" style="padding: 10px" id="divAreaSrpUrls" runat="server" visible="FALSE">
                    <asp:Label runat="server" ID="lblmassagelinksTitle" Visible="false" Style="color: #fff; FONT-SIZE: 19px; font-weight: bold; float: left;"> </asp:Label>
                    <br />
                    <br />
                    <input type="hidden" id="hdnAreaName" runat="server" />
                    <asp:Repeater ID="repeater_link" runat="server">
                        <ItemTemplate>
                            <div class="col-sm-4">
                                <%#Eval("link") %>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <%-- <asp:Table ID="tbllink" runat="server" CssClass="tablelinkcss" />--%>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="UpdatePanel19">
        <ContentTemplate>
            <div class="container">
                <div class="row" style="padding: 10px" id="divspecialitySrpUrls" runat="server" visible="FALSE">
                    <asp:Label runat="server" ID="lbllinkspecialitytitle" Visible="false" Style="color: #fff; FONT-SIZE: 19px; font-weight: bold; float: left;"> </asp:Label>
                    <br />
                    <br />
                    <input type="hidden" id="Hidden1" runat="server" />
                    <asp:Repeater ID="repeater_specialtiy" runat="server">
                        <ItemTemplate>
                            <div class="col-sm-4">
                                <%#Eval("link") %>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <%-- <asp:Table ID="tblspeciality" runat="server" CssClass="tablelinkcss" />--%>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

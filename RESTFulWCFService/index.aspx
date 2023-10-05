<%@ Page Title="" Language="C#" MasterPageFile="~/SearchPartener.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="RESTFulWCFService.MassagePartener.index" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Controls/DatePicker.ascx" TagName="uc" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <title>Body Massage - Find your body massage partner nearby | My Massage Partner</title>
   
<meta name="Description" content="Massage Partner feature providing you a platform where you can find massage partner, meet, contact, and chat in regards of body massage. On the basis of your interest and desires you can easily start body massage sessions with your selected partner in home and hotel.">
<meta name="Keywords" content="body massage nearby | erotic massage near me | happy ending massage near me | need body massage | full body massage | female to male Massage | sensual Massage | body massage at home | body massage in hotel | want body massage | massage body pain | massage near me | female massager near me | free body massage | male to female body massage | cheap massage near me">
<meta name="google-site-verification" content="HD4UoYQ7r6-Nu2WdC8x_f-ZB8pJIaEw-L_tn4wYzZrs" />
<meta name="yandex-verification" content="e0da8e52927fc168" />
<meta name="msvalidate.01" content="65D607D0E88D6FCFA3099C21B8BFD2E1" />
    <style type="text/css">
         .banner_mob
         {
             display: none;
         }
         /* when screen is less than 600px wide
     show mobile version and hide desktop */
         @media (max-width: 600px)
         {
             .banner_mob
             {
                 display: block;
             }

             .banner_desk
             {
                 display: none;
             }

             .search_text__main566
             {
                 padding: 3px 10px;
                 height: 27px;
                 margin-bottom: 5px;
             }
         }
     </style>
    <style>
        #toTop
        {
            display: none;
            position: fixed;
            bottom: 5px;
            right: -5px;
            width: 90px;
            height: 85px;
            background-repeat: no-repeat;
            opacity: .4;
            background-image: url(/img/up-arrow-icon.png);
        }

            #toTop:hover
            {
                opacity: .8;
                filter: alpha(opacity=80);
            }
    </style>
   <script  src="<%=Constants__.WEB_ROOT_CDN %>/js/jquery.scrollToTop.min.js"></script>
    <script type="text/javascript">
        //$(function () {
        //    $("#toTop").scrollToTop(1000);
        //});

        window.onscroll = function () { scrollFunction() };
        function scrollFunction() {
            if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
                $("#toTop").css("display", "block");
            } else {

                $("#toTop").css("display", "none");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div id="overlay">
    </div>
    <div class="home-top-section">
        <div class="container">

            <div class="row ">
                <div class="col-sm-12">

                    <div class="left-sidebar">

                       <div class="col-sm-12">

                     
                    <div class="col-sm-4">
                        <center>
                        <h3 style="color:red;">
                        Search Massage Partner
                        </h3>
                            </center>
                        <div>
                            
                            <div class="form-group">
                              <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlcountry" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlcountry" Display="Dynamic" ForeColor="Red" ValidationGroup="search" ErrorMessage="*Required"></asp:RequiredFieldValidator>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
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
                            <div class="form-group">
                                               <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlmassagetypes" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlgender" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlgender_SelectedIndexChanged">
                                                            <asp:ListItem Value="0" Selected>Gender</asp:ListItem>
                                                            <asp:ListItem Value="M">Male</asp:ListItem>
                                                            <asp:ListItem Value="F">Female</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                            <div id="div_Partner_Types" runat="server" visible="false">
                                            <div class="form-group">
                                                <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlmassagetypes" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlPartner_Types" runat="server" CssClass="form-control">
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
                            <div class="form-group">
                                                <asp:DropDownList ID="ddlage" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                            <div class="form-group">

                                                <asp:DropDownList ID="ddllookingfor" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="Interested In" Value="0" Selected></asp:ListItem>
                                                    <asp:ListItem Value="T">Massage Therapist/Professional</asp:ListItem>
                                                    <asp:ListItem Value="M">Manual Massager/Enthusiast</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                            <div class="form-group">
                                                 <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                                     <Triggers>
                                                         <asp:AsyncPostBackTrigger ControlID="ddlgender" />
                                                     </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlmassagetypes" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlmassagetypes_SelectedIndexChanged" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                            <div id="Div1" class="form-group"  runat="server" visible="false">
                                                <asp:DropDownList ID="ddlOutCall" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="OutCall" Value="0" Selected></asp:ListItem>
                                                    <asp:ListItem Value="Home">At Home</asp:ListItem>
                                                    <asp:ListItem Value="Hotel">At Hotel</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                            <div class="rowElem dvcss743">
                                <div class="search_text_box" name="selectmenu1">
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                        <ContentTemplate>
                                            <asp:Button runat="server" CausesValidation="false" ID="lnkbtnReset" CssClass="button_1 black"
                                                Text="Reset" Visible="false" />
                                            <center>
                                               <asp:Button ID="btnsearch" runat="server" CssClass="btn btn-gradiant" style="width:100%" ValidationGroup="search" Text="Search" OnClick="btnsearch_Click" />
                                                </center>
                                            <asp:HiddenField ID="hdnPageType" runat="server" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                           <br />
                        </div>
                    </div>
                
                        <div class="col-sm-8">
                        <h1 class="text-center title">About MyMassagePartner
	   
                        </h1>
                        <p class="text-justify">
                           MyMassagePartner.com providing you a platform where you can find female and male body massage partner from our thousands of registered female and male members. You can select female to male body massage, male to male body massage, male to female body massage, and female to female body massage nearby you. Here, we have professionals as well as manual or enthusiast as members so that you can make body massage therapies and fun together.<br /><br />
                            On the basis of your interests, requirements, and needs you can find and select body massage partner nearby you. And can meet personally, for body massage sessions. We do not appreciate paid body massage session here, so it's all about free body massage with partner and lots of fun. Find your body massage partner in your local place and start chatting. We are sure you can save lots of money with free body massage from your partner.<br /><br /> 
                            You can start with MyMassagePartner.com easily, just fill up the basic registration form and start searching for body massage partner in your location. You can find your body massage buddies based on Area, city, state/region/country, age group, gender (female/girl, male/boys, others), manual massagers or professional massage therapists, and body massage type.<br /><br />
                             MyMassagePartner.com know well that body massage is expensive and not easy to find nearby you so let's not wait in queue and get ready for friendly body massage session. You can chat with your female and male body massage partners, can select them favorite, and chat with them instantly. If you're traveler or love to travel then you can find body massage partner in many cities and have body massage fun. You can rub your massage partner's body and he or she can rub yours.<br /><br />
                            Relaxation and refreshment is much required in stressed world. And MyMassagePartner here to give you facility to overcome from all stress. Your smile matters to us!
                           
	
                        </p>
                           <%-- <p class="text-justify">
                           MyMassagePartner.com providing you a platform where you can find female and male massage partner from our thousands of registered female and male members. You can select female to male massage, male to male massage, male to female massage, and female to female massage nearby you. Here, we have professionals as well as manual or enthusiast as members so that you can make massage therapies.<br /><br />
                            On the basis of your interests, requirements, and needs you can find and select massage partner nearby you. And can meet personally, for massage sessions. We do not appreciate paid massage session here, so it's all about free massage with partner. Find your massage partner in your local place and start chatting. We are sure you can save lots of money with free massage from your partner.<br /><br /> 
                            You can start with MyMassagePartner.com easily, just fill up the basic registration form and start searching for massage partner in your location. You can find your massage buddies based on Area, city, state/region/country, age group, gender (female/girl, male/boys, others), manual massagers or professional massage therapists, and massage type.<br /><br />
                             MyMassagePartner.com know well that massage is expensive and not easy to find nearby you so let's not wait in queue and get ready for friendly massage session. You can chat with your female and male massage partners, can select them favorite, and chat with them instantly. If you're traveler or love to travel then you can find massage partner in many cities and have massage fun. You can rub your massage partner's and he or she can rub yours.<br /><br />
                            Relaxation and refreshment is much required in stressed world. And MyMassagePartner here to give you facility to overcome from all stress. Your smile matters to us!
                           
	
                        </p>--%>
                        </div>
                            
                             </div> 
                   
                    <img src="<%=Constants__.WEB_ROOT %>/image/bane.jpg" class="img-responsive banner_desk" />
<%--                        <img src="<%=Constants__.WEB_ROOT %>/image/mymp_4.jpg" class="img-responsive banner_desk" />--%>
                    <img src="<%=Constants__.WEB_ROOT %>/image/bane2.jpg" class="img-responsive banner_mob" />
                      <%--   <img src="<%=Constants__.WEB_ROOT %>/image/bane2.jpg" class="img-responsive banner_mob" />--%>
<%--                         <img src="<%=Constants__.WEB_ROOT %>/image/mymp_1.jpeg" class="img-responsive banner_mob" />--%>
                    
                        <h3 class="text-center title">Benefits of <span font-weight:bold">My</span>MassagePartner Membership
	   
                        </h3>
                        <ul class="list-inline-image">
                           <%-- <li>You will be able to chat with female and male body massage partner.</li>
                            <li>You will be able to see contact details of female or male body massage partner.</li>
                            <li>You will find best female and male massage partner nearby you.</li>
                            <li>You will get support from MyMassagePartner team members i.e. Rose, Jasmine, and others 24/7.</li>
                            <li>You will get the female and male body massage partner details in premium way.</li>
                            <li>You can find free body massage in your location.</li>--%>
                             <li>You will be able to chat with female and male massage partner.</li>
                            <li>You will be able to see contact details of female or male massage partner.</li>
                            <li>You will find best female and male massage partner nearby you.</li>
                            <li>You will get support from MyMassagePartner team members i.e. Rose, Jasmine, and others 24/7.</li>
                            <li>You will get the female and male massage partner details in premium way.</li>
                            <li>You can find free massage in your location.</li>
                        </ul>

                        <%-- <div class="text-center">
                            <button type="button" class="btn btn-gradiant">Read More</button>
                        </div>--%>
                    </div>
                </div>
            </div>
        </div>
          <!--for scroll to top-->
                        <a href="#top" id="toTop"></a>
    </div>

  


</asp:Content>

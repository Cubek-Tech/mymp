using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Business;
using AjaxControlToolkit;
using System.IO;
using BussinessEntity;
using System.Drawing;
using RESTFulWCFService;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using PayPalIntegration;
using System.Text.RegularExpressions;
using CCA.Util;
using Stripe;
using System.Net;

using System.Collections.Generic;
using java.nio.file;

namespace RESTFulWCFService.MassagePartener
{

    public partial class SearchPartener1 : System.Web.UI.Page
    {
        DataTable DT = new DataTable();
        BussinessPaypal obj = new BussinessPaypal();
        BussinessSendMail objmail = new BussinessSendMail();
        private bool PayPalReturnRequest = false;
        protected decimal OrderAmount = 0.00M;
        int img_id = 0;
        #region objects
        private int PageSize = 2;
        PagedDataSource adsource;
        GetSiteURL gestsiteurl = new GetSiteURL();
        RegistrationBusiness objRegistrationBusiness = new RegistrationBusiness();
        Business.BusinessLogin objBusinessLogin = new BusinessLogin();
        Business.BusinessSearch objBusinessSearch = new Business.BusinessSearch();
        BusinessMPartener objbusinessmpartener = new BusinessMPartener();
        DataTable dt;
        int pos;
        private int seeker_sk { get; set; }
        private string cities { get; set; }
        private string countries { get; set; }
        private string states { get; set; }
        private string Uip { get; set; }
        private string Ucountry { get; set; }
        private string Ustate { get; set; }
        private string Ucity { get; set; }
        private string UArea { get; set; }
        private string Lag { get; set; }
        private string Lat { get; set; }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "openOffersDialog();", true);
                HtmlGenericControl Divid = (HtmlGenericControl)Page.Master.FindControl("SRP_link");
                Divid.Visible = false;
                
                HtmlGenericControl Divid1 = (HtmlGenericControl)Page.Master.FindControl("SRP_link1");
                Divid1.Visible = false;

               
                if (Session["mp_login_sk"] != null)
                {
                    Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) - 120));
                    DataSet  ds_check = objbusinessmpartener.getPartnerSubsciption_record(Convert.ToInt32(Session["massage_partner_sk"].ToString()));
                    if (ds_check.Tables[3].Rows.Count > 0)
                    {
                        hdnpartnersubscribed.Value = "Y";
                    }
                    else
                        hdnpartnersubscribed.Value = "";

                 //   txtUserMailid.Text = Session["email_id"].ToString();

                    //ViewState["massage_partner_sk"] = Session["massage_partner_sk"].ToString();
                    //Session["mp_login_sk"].ToString() = Session["mp_login_sk"].ToString();
                    //ViewState["country_sk"] = Session["country_sk"].ToString();
                }
                if (!IsPostBack)
                {
                    //if (Session["Search_data"] == null)
                    //  {
                    fill_dropdowns();
                    bind_tool_tip();
                    if (RouteData.Values["country"] != null)
                    {
                        getCountryByGeoIP();
                        SRP_load();
                    }
                    else
                    {
                        GetIP();
                        fillDropdown();
                        SRP_load();
                    }
                    string url1 = Request.RawUrl;
                    if (url1.Contains("body-massage-partner"))
                    {
                        div_Gender.Visible = false;
                        div_Looking_fr.Visible = false;
                        dev_Age.Visible = false;
                        div_left.Attributes.Remove("class");
                        div_left.Attributes.Add("class", "col-sm-7");
                        div_right.Attributes.Remove("class");
                        div_right.Attributes.Add("class", "col-sm-5");
                        div_massage_types.Attributes.Remove("class");
                        div_massage_types.Attributes.Add("class", "col-sm-5 padding5");
                        DynamicMeta_partner_types();
                    }
                    else
                    {
                        div_OutCall.Visible = false;
                        div_Partner_Types.Visible = false;
                        DynamicMeta();

                    }
                    //}
                    //else
                    //{
                    //    fill_dropdowns();
                    //    GetIP();
                    //    //  getCountryByGeoIP();
                    //    fillDropdown();
                    //    SRP_load();
                    //    DynamicMeta();

                    //}





                    if (Session["mp_login_sk"] != null)
                    {

                        //ddlcountry.SelectedIndex = ddlcountry.Items.IndexOf(ddlcountry.Items.FindByValue(Session["country_sk"].ToString()));
                        //ddlcountry_SelectedIndexChanged(null, null);
                        //ddlstate_SelectedIndexChanged(null, null);


                        if (Session["Is_after_registration"] != null || Session["Is_after_login"] != null)
                        {
                            if (hdnpartnersubscribed.Value == null || hdnpartnersubscribed.Value == "" || hdnpartnersubscribed.Value == "N")
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp1sd", "<script type='text/javascript'>openpopuppaypal();</script>", false);
                            Session["Is_after_registration"] = null;
                            Session["Is_after_login"] = null;
                        }
                    }
                }

                //  btnsearch_Click(null, null);
                search();
                bind_revieews(Convert.ToInt32(ddlcountry.SelectedValue));
                if (Request.RawUrl.Contains("/India/") || Request.RawUrl.Contains("/india/"))
                {
                    li_whatsapp_share.Visible = true;
                }
                else
                {
                    li_whatsapp_share.Visible = false;

                }
                if (Session["counter"] != null)
                {
                    int  c = Convert.ToInt32(Session["counter"].ToString());
                    if (c >= 5)
                    {
                        btnsend.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message;
                var st = new System.Diagnostics.StackTrace(ex, true);
                StackFrame[] stackFrames = st.GetFrames();
                foreach (StackFrame stackFrame in stackFrames)
                {
                    Console.WriteLine(stackFrame.GetMethod().Name);   // write method name
                    BussinessEntity.ExceptionHandling._lineno = stackFrame.GetFileLineNumber();
                    BussinessEntity.ExceptionHandling._methodname = Convert.ToString(stackFrame.GetMethod().Name);
                    BussinessEntity.ExceptionHandling._pagename = Convert.ToString(Request.Url.AbsoluteUri);

                }

                Response.Redirect(Constants__.WEB_ROOT + "/ErrorMessage.aspx", false);
                return;
            }

        }

        #region methods
        private void bind_tool_tip()
        {
            //Outcall DDL
            foreach (ListItem item in ddlOutCall.Items)
            {
                item.Attributes.Add("Title", item.Text);
            }
            //  ddlOutCall.ToolTip = ddlOutCall.SelectedItem.Text;
            //Partner type DDL
            foreach (ListItem item in ddlPartner_Types.Items)
            {
                item.Attributes.Add("Title", item.Text);
            }
            //  ddlPartner_Types.ToolTip = ddlPartner_Types.SelectedItem.Text;
        }
        private void add_page_no()
        {
            if (UCPager1.CurrentPage > 1)
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "add_sp_url_page", "addParameterToURL(page=" + UCPager1.CurrentPage + ")", true);
        }
        private void DynamicMeta()
        {
            try
            {
                //-----meta tags ----------
                HtmlMeta Meta = new HtmlMeta();
                HtmlMeta keyword = new HtmlMeta();
                string country = "";
                string state = "";
                string city = "";
                string area = "";
                string looking = "";
                string Meta_ = "";
                string keyword_ = "";
                string mtypes = "";
                string gender = "";
                string description = "";
                // string looking = "";


                if (ddlcountry.SelectedIndex > 0)
                {
                    country = ddlcountry.SelectedItem.Text;

                }
                if (ddlstate.SelectedIndex > 0)
                {
                    state = ddlstate.SelectedItem.Text;

                }
                if (ddlcity.SelectedIndex > 0)
                {
                    city = ddlcity.SelectedItem.Text;

                }
                if (ddllookingfor.SelectedIndex > 0)
                {
                    looking = ddllookingfor.SelectedItem.Text;
                }
                if (ddlarea.SelectedIndex > 0)
                {
                    area = ddlarea.SelectedItem.Text;
                }
                if (ddlmassagetypes.SelectedIndex > 0)
                {
                    mtypes = ddlmassagetypes.SelectedItem.Text;
                }
                if (ddlgender.SelectedIndex > 0)
                {
                    gender = ddlgender.SelectedItem.Text;
                }
                if (ddllookingfor.SelectedIndex > 0)
                {
                    looking = ddllookingfor.SelectedItem.Text;
                }

                string title_ = "";
                //--title text--------   

                if (area != "" && city != "")
                {
                    // <a href='https://www.mymassagepartner.com" + "/massage-partner/" + country.Replace(' ', '-') + "/s/c/All-Area/Any/all-types/all" + "'>" + country + "</a> 
                    links_srp.InnerHtml = "<a href='https://www.mymassagepartner.com'>Home</a> > <a href='https://www.mymassagepartner.com" + "/massage-partner/" + country.Replace(' ', '-') + "/" + state.Replace(' ', '-') + "/a/All-Area/Any/all-types/all" + "'>" + state + "</a> > <a href='https://www.mymassagepartner.com" + "/massage-partner/" + country.Replace(' ', '-') + "/" + state.Replace(' ', '-') + "/" + city.Replace(' ', '-') + "/All-Area/Any/all-types/all" + "'>" + city + "</a> > " + area + "";
                    if (mtypes != "")
                    {
                        if (gender != "")
                        {
                            if (looking != "")
                            {
                                if (ddllookingfor.SelectedValue == "M")
                                {
                                    if (gender == "Male")
                                    { description = "At MyMassagePartner.com, find " + gender + " manual massage partner for " + mtypes + " in " + area + ", " + city + ". This page showing " + area + ", " + city + " based " + gender.ToLower() + " who looking for manual " + mtypes + " from his partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number " + gender.ToLower() + " who looking for " + mtypes + " from male or female in " + area + ", " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for " + mtypes + " in " + area + ", " + city + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + area + ", " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + area + ", " + city + " is a good place to exchange " + mtypes + " from " + gender + " massage partner."; }
                                    else
                                    {
                                        description = "At MyMassagePartner.com, find " + gender + " manual massage partner for " + mtypes + " in " + area + ", " + city + ". This page showing " + area + ", " + city + " based " + gender + " who looking for manual " + mtypes + " from her partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number " + gender + " who looking for " + mtypes + " from male or female in " + area + ", " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for " + mtypes + " in " + area + ", " + city + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + area + ", " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + area + ", " + city + " is a good place to exchange " + mtypes + " from " + gender + " massage partner.";
                                    }

                                    page_title.InnerHtml = gender + " partner for manual " + mtypes.Trim() + " in " + area;
                                    title_page.InnerHtml = "Find " + gender + " partner for manual " + mtypes.Trim() + " in " + area + " | MyMassagePartner";
                                    Meta_ = "Here you can find manual " + gender + " massage partner for " + mtypes.Trim() + " in " + area + " with their contact number, you can chat in regards of free " + mtypes.Trim() + " so let’s start " + mtypes.Trim() + " sessions with your selected partner in home and hotel - MyMassagePartner - " + gender + " partner for manual " + mtypes.Trim() + ", " + area;
                                    keyword_ = gender + " partner " + mtypes.Trim() + " " + area + " | free " + mtypes.Trim() + " " + gender + " partner " + area + " | " + mtypes.Trim() + " " + gender + " in " + area + " | need " + gender + " for " + mtypes.Trim() + " | " + mtypes.Trim() + " " + gender + " partner in " + area + " | female to male " + mtypes.Trim() + " in " + area + " | " + mtypes.Trim() + " service by " + gender + " in " + area + " | " + mtypes.Trim() + " by " + gender + " at home in " + area + " | " + mtypes.Trim() + " by " + gender + " in hotel in " + area + " | want " + gender + " for " + mtypes.Trim() + " in " + area + " | massage in " + area + " by " + gender + " | " + mtypes.Trim() + " near me by " + gender + " | " + gender + " massager in " + area + " | free " + mtypes.Trim() + " in " + area + " | male to female " + mtypes.Trim() + " | cheap " + mtypes.Trim() + " " + area + " | " + gender + " to male massage";
                                }
                                else
                                {
                                    if (gender == "Male")
                                    { description = "At MyMassagePartner.com, find " + gender + " professional massage partner for " + mtypes + " in " + area + ", " + city + ". This page showing " + area + ", " + city + " based " + gender + " who looking for professional " + mtypes + " from his partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number " + gender + " who looking for " + mtypes + " from male or female in " + area + ", " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for " + mtypes + " in " + area + ", " + city + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + area + ", " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + area + ", " + city + " is a good place to exchange " + mtypes + " from " + gender + " massage partner."; }
                                    else
                                    { description = "At MyMassagePartner.com, find " + gender + " professional massage partner for " + mtypes + " in " + area + ", " + city + ". This page showing " + area + ", " + city + " based " + gender + " who looking for professional " + mtypes + " from her partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number " + gender + " who looking for " + mtypes + " from male or female in " + area + ", " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for " + mtypes + " in " + area + ", " + city + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + area + ", " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + area + ", " + city + " is a good place to exchange " + mtypes + " from " + gender + " massage partner."; }

                                    page_title.InnerHtml = gender + " partner for professional " + mtypes.Trim() + " in " + area;
                                    title_page.InnerHtml = "Find " + gender + " partner for professional " + mtypes.Trim() + " in " + area + " | MyMassagePartner";
                                    Meta_ = "Here you can find professional " + gender + " massage partner for " + mtypes.Trim() + " in " + area + " with their contact number, you can chat in regards of free " + mtypes.Trim() + " so let’s start " + mtypes.Trim() + " sessions with your selected partner in home and hotel - MyMassagePartner - " + gender + " partner for professional " + mtypes.Trim() + ", " + area;
                                    keyword_ = gender + " partner " + mtypes.Trim() + " " + area + " | free " + mtypes.Trim() + " " + gender + " partner " + area + " | " + mtypes.Trim() + " " + gender + " in " + area + " | need " + gender + " for " + mtypes.Trim() + " | " + mtypes.Trim() + " " + gender + " partner in " + area + " | female to male " + mtypes.Trim() + " in " + area + " | " + mtypes.Trim() + " service by " + gender + " in " + area + " | " + mtypes.Trim() + " by " + gender + " at home in " + area + " | " + mtypes.Trim() + " by " + gender + " in hotel in " + area + " | want " + gender + " for " + mtypes.Trim() + " in " + area + " | massage in " + area + " by " + gender + " | " + mtypes.Trim() + " near me by " + gender + " | " + gender + " massager in " + area + " | free " + mtypes.Trim() + " in " + area + " | male to female " + mtypes.Trim() + " | cheap " + mtypes.Trim() + " " + area + " | " + gender + " to male massage";
                                }
                            }
                            else
                            {
                                if (gender == "Male")
                                { description = "At MyMassagePartner.com, find " + gender + " massage partner for " + mtypes + " in " + area + ", " + city + ". This page showing " + area + ", " + city + " based " + gender + " who looking for manual or professional " + mtypes + " from his partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number " + gender + " who looking for " + mtypes + " from male or female in " + area + ", " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for " + mtypes + " in " + area + ", " + city + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + area + ", " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + area + ", " + city + " is a good place to exchange " + mtypes + " from " + gender + " massage partner."; }
                                else
                                { description = "At MyMassagePartner.com, find " + gender + " massage partner for " + mtypes + " in " + area + ", " + city + ". This page showing " + area + ", " + city + " based " + gender + " who looking for manual or professional " + mtypes + " from her partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number " + gender + " who looking for " + mtypes + " from male or female in " + area + ", " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for " + mtypes + " in " + area + ", " + city + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + area + ", " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + area + ", " + city + " is a good place to exchange " + mtypes + " from " + gender + " massage partner."; }

                                page_title.InnerHtml = gender + " " + mtypes.Trim() + " partner in " + area + ", " + city;
                                title_page.InnerHtml = "Find " + gender + " " + mtypes.Trim() + " partner in " + area + ", " + city + " | MyMassagePartner";
                                Meta_ = "Here you can find " + gender + " partner for " + mtypes.Trim() + " in " + area + " with their contact number, you can chat in regards of free " + mtypes.Trim() + " so let’s start " + mtypes.Trim() + " sessions with your selected partner in home and hotel - MyMassagePartner - " + gender + " partner for " + mtypes.Trim() + ", " + area;
                                keyword_ = gender + " partner " + mtypes.Trim() + " " + area + " | free " + mtypes.Trim() + " " + gender + " partner " + area + " | " + mtypes.Trim() + " " + gender + " in " + area + " | need " + gender + " for " + mtypes.Trim() + " | " + mtypes.Trim() + " " + gender + " partner in " + area + " | female to male " + mtypes.Trim() + " in " + area + " | " + mtypes.Trim() + " service by " + gender + " in " + area + " | " + mtypes.Trim() + " by " + gender + " at home in " + area + " | " + mtypes.Trim() + " by " + gender + " in hotel in " + area + " | want " + gender + " for " + mtypes.Trim() + " in " + area + " | massage in " + area + " by " + gender + " | " + mtypes.Trim() + " near me by " + gender + " | " + gender + " massager in " + area + " | free " + mtypes.Trim() + " in " + area + " | male to female " + mtypes.Trim() + " | cheap " + mtypes.Trim() + " " + area + " | " + gender + " to male massage";
                            }
                        }
                        else
                        {
                            if (looking != "")
                            {
                                if (ddllookingfor.SelectedValue == "M")
                                {
                                    description = "At MyMassagePartner.com, find female and male manual massage partner for " + mtypes + " in " + area + ", " + city + ". This page showing " + area + ", " + city + " based female and male who looking for manual or professional " + mtypes + " from his or her partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number of female and male who looking for " + mtypes + " from male or female in " + area + ", " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available female and male massage partner for " + mtypes + " in " + area + ", " + city + ". Exchange " + mtypes + " with your female and male partner anytime, anywhere in " + area + ", " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + area + ", " + city + " is a good place to exchange " + mtypes + " from female and male massage partner.";
                                    page_title.InnerHtml = "manual " + mtypes.Trim() + " partner in " + area;
                                    title_page.InnerHtml = "Find manual " + mtypes.Trim() + " partner in " + area + " | MyMassagePartner";
                                    Meta_ = "Here you can find manual massage partner for " + mtypes.Trim() + " in " + area + " with their contact number, you can chat in regards of free " + mtypes.Trim() + " so let’s start " + mtypes.Trim() + " sessions with your selected partner in home and hotel - MyMassagePartner";
                                    keyword_ = "massage partner " + mtypes.Trim() + " " + area + " | free " + mtypes.Trim() + " " + gender + " partner " + area + " | " + mtypes.Trim() + " " + gender + " in " + area + " | need " + gender + " for " + mtypes.Trim() + " | " + mtypes.Trim() + " " + gender + " partner in " + area + " | female to male " + mtypes.Trim() + " in " + area + " | " + mtypes.Trim() + " service by " + gender + " in " + area + " | " + mtypes.Trim() + " by " + gender + " at home in " + area + " | " + mtypes.Trim() + " by " + gender + " in hotel in " + area + " | want " + gender + " for " + mtypes.Trim() + " in " + area + " | massage in " + area + " by " + gender + " | " + mtypes.Trim() + " near me by " + gender + " | " + gender + " massager in " + area + " | free " + mtypes.Trim() + " in " + area + " | male to female " + mtypes.Trim() + " | cheap " + mtypes.Trim() + " " + area;
                                }
                                else
                                {
                                    description = "At MyMassagePartner.com, find female and male professional massage partner for " + mtypes + " in " + area + ", " + city + ". This page showing " + area + ", " + city + " based female and male massage who looking for manual or professional " + mtypes + " from his or her partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number of female and male who looking for " + mtypes + " from male or female in " + area + ", " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available female and male massage partner for " + mtypes + " in " + area + ", " + city + ". Exchange " + mtypes + " with your female and male partner anytime, anywhere in " + area + ", " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + area + ", " + city + " is a good place to exchange " + mtypes + " from female and male massage partner.";
                                    page_title.InnerHtml = "professional " + mtypes.Trim() + " partner in " + area;
                                    title_page.InnerHtml = "Find professional " + mtypes.Trim() + " partner in " + area + " | MyMassagePartner";
                                    Meta_ = "Here you can find professional massage partner for " + mtypes.Trim() + " in " + area + " with their contact number, you can chat in regards of free " + mtypes.Trim() + " so let’s start " + mtypes.Trim() + " sessions with your selected partner in home and hotel - MyMassagePartner";
                                    keyword_ = "massage partner " + mtypes.Trim() + " " + area + " | free " + mtypes.Trim() + " " + gender + " partner " + area + " | " + mtypes.Trim() + " " + gender + " in " + area + " | need " + gender + " for " + mtypes.Trim() + " | " + mtypes.Trim() + " " + gender + " partner in " + area + " | female to male " + mtypes.Trim() + " in " + area + " | " + mtypes.Trim() + " service by " + gender + " in " + area + " | " + mtypes.Trim() + " by " + gender + " at home in " + area + " | " + mtypes.Trim() + " by " + gender + " in hotel in " + area + " | want " + gender + " for " + mtypes.Trim() + " in " + area + " | massage in " + area + " by " + gender + " | " + mtypes.Trim() + " near me by " + gender + " | " + gender + " massager in " + area + " | free " + mtypes.Trim() + " in " + area + " | male to female " + mtypes.Trim() + " | cheap " + mtypes.Trim() + " " + area;
                                }
                            }
                            else
                            {
                                description = "At MyMassagePartner.com, find female and male massage partner in " + area + ", " + city + ". This page showing " + area + ", " + city + " based female and male who looking for manual or professional " + mtypes + " from his or her partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number of female and male who looking for " + mtypes + " from male or female in " + area + ", " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available female and male massage partner for " + mtypes + " in " + area + ", " + city + ". Exchange " + mtypes + " with your female and male partner anytime, anywhere in " + area + ", " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + area + ", " + city + " is a good place to exchange " + mtypes + " from female and male massage partner.";
                                page_title.InnerHtml = mtypes.Trim() + " partner in " + area + ", " + city;
                                title_page.InnerHtml = "Find " + mtypes.Trim() + " partner in " + area + ", " + city + " | MyMassagePartner";
                                Meta_ = "Here you can find " + mtypes.Trim() + " female and male body massage partners in " + area + " with their contact number, you can chat in regards of free " + mtypes.Trim() + " so let’s start " + mtypes.Trim() + " sessions with your selected partner in home and hotel - MyMassagePartner, " + area;
                                keyword_ = mtypes.Trim() + " " + area + " | free " + mtypes.Trim() + " " + area + " | " + mtypes.Trim() + " in " + area + " | need " + mtypes.Trim() + " | " + mtypes.Trim() + " partner in " + area + " | female to male " + mtypes.Trim() + " in " + area + " | " + mtypes.Trim() + " service in " + area + " | " + mtypes.Trim() + " at home in " + area + " | " + mtypes.Trim() + " in hotel in " + area + " | want " + mtypes.Trim() + " in " + area + " | massage in " + area + " | " + mtypes.Trim() + " near me | female massager in " + area + " | free " + mtypes.Trim() + " in " + area + " | male to female " + mtypes.Trim() + " | cheap " + mtypes.Trim() + " " + area;
                            }
                        }
                    }
                    else
                    {
                        mtypes = "body massage";
                        if (gender != "")
                        {
                            if (looking != "")
                            {
                                if (ddllookingfor.SelectedValue == "M")
                                {
                                    if (gender == "Male")
                                    { description = "At MyMassagePartner.com, find " + gender.ToLower() + " manual massage partner in " + area + ", " + city + ". This page showing " + area + ", " + city + " based " + gender + " who looking for manual body massage from his partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number " + gender + " who looking for body massage from male or female in " + area + ", " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for body massage in " + area + ", " + city + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + area + ", " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + area + ", " + city + " is a good place to exchange body massage from " + gender + " massage partner."; }
                                    else
                                    { description = "At MyMassagePartner.com, find " + gender.ToLower() + " manual massage partner in " + area + ", " + city + ". This page showing " + area + ", " + city + " based " + gender + " who looking for manual body massage from her partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number " + gender + " who looking for body massage from male or female in " + area + ", " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for body massage in " + area + ", " + city + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + area + ", " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + area + ", " + city + " is a good place to exchange body massage from " + gender + " massage partner."; }

                                    page_title.InnerHtml = gender + " manual massage partner in " + area;
                                    title_page.InnerHtml = "Find " + gender + " manual massage partner in " + area + " | MyMassagePartner";
                                    Meta_ = "Here you can find manual " + gender + " massage partner in " + area + " with their contact number, you can chat in regards of free body massage so let’s start body massage sessions with your selected partner in home and hotel - MyMassagePartner";
                                    keyword_ = gender + " massage partner " + area + " | free massage by " + gender + " partner " + area + " | " + gender + " massagers in " + area + " | need " + gender + " for massage | " + gender + " partner in " + area + " | female to male in " + area + " | massage service by " + gender + " in " + area + " | massage by " + gender + " at home in " + area + " | massage by " + gender + " in hotel in " + area + " | want " + gender + " for massage in " + area + " | massage in " + area + " by " + gender + " | manual massager near me | " + gender + " massager in " + area + " | free massage in " + area + " | male to female Back massage | cheap Back massage " + area + " | " + gender + " to male massage";
                                }
                                else
                                {
                                    if (gender == "Male")
                                    { description = "At MyMassagePartner.com, find " + gender + " professional massage partner in " + area + ", " + city + ". This page showing " + area + ", " + city + " based " + gender + " who looking for professional body massage from his partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number " + gender + " who looking for body massage from male or female in " + area + ", " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for body massage in " + area + ", " + city + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + area + ", " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + area + ", " + city + " is a good place to exchange body massage from " + gender + " massage partner."; }
                                    else
                                    { description = "At MyMassagePartner.com, find " + gender + " professional massage partner in " + area + ", " + city + ". This page showing " + area + ", " + city + " based " + gender + " who looking for professional body massage from her partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number " + gender + " who looking for body massage from male or female in " + area + ", " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for body massage in " + area + ", " + city + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + area + ", " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + area + ", " + city + " is a good place to exchange body massage from " + gender + " massage partner."; }

                                    page_title.InnerHtml = gender + " professional massage partner in " + area;
                                    title_page.InnerHtml = "Find " + gender + " professional massage partner in " + area + " | MyMassagePartner";
                                    Meta_ = "Here you can find professional " + gender + " massage partner in " + area + " with their contact number, you can chat in regards of free body massage so let’s start body massage sessions with your selected partner in home and hotel - MyMassagePartner";
                                    keyword_ = gender + " massage partner " + area + " | free massage by " + gender + " partner " + area + " | " + gender + " massagers in " + area + " | need " + gender + " for massage | " + gender + " partner in " + area + " | female to male in " + area + " | massage service by " + gender + " in " + area + " | massage by " + gender + " at home in " + area + " | massage by " + gender + " in hotel in " + area + " | want " + gender + " for massage in " + area + " | massage in " + area + " by " + gender + " | professional massager near me | " + gender + " massager in " + area + " | free massage in " + area + " | male to female Back massage | cheap Back massage " + area + " | " + gender + " to male massage";
                                }
                            }
                            else
                            {
                                if (gender == "Male")
                                { description = "At MyMassagePartner.com, find " + gender + " massage partner in " + area + ", " + city + ". This page showing " + area + ", " + city + " based " + gender + " who looking for manual or professional body massage from his partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number " + gender + " who looking for body massage from male or female in " + area + ", " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for body massage in " + area + ", " + city + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + area + ", " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + area + ", " + city + " is a good place to exchange body massage from " + gender + " massage partner."; }
                                else
                                { description = "At MyMassagePartner.com, find " + gender + " massage partner in " + area + ", " + city + ". This page showing " + area + ", " + city + " based " + gender + " who looking for manual or professional body massage from her partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number " + gender + " who looking for body massage from male or female in " + area + ", " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for body massage in " + area + ", " + city + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + area + ", " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + area + ", " + city + " is a good place to exchange body massage from " + gender + " massage partner."; }

                                page_title.InnerHtml = gender + " massage partner in " + area;
                                title_page.InnerHtml = "Find " + gender + " massage partner in " + area + " | MyMassagePartner";
                                Meta_ = "Here you can find " + gender + " massage partner in " + area + " with their contact number, you can chat in regards of free body massage so let’s start body massage sessions with your selected partner in home and hotel - MyMassagePartner";
                                keyword_ = gender + " massage partner " + area + " | free massage by " + gender + " partner " + area + " | " + gender + " massagers in " + area + " | need " + gender + " for massage | " + gender + " partner in " + area + " | female to male in " + area + " | massage service by " + gender + " in " + area + " | massage by " + gender + " at home in " + area + " | massage by " + gender + " in hotel in " + area + " | want " + gender + " for massage in " + area + " | massage in " + area + " by " + gender + " | manual massager near me | " + gender + " massager in " + area + " | free massage in " + area + " | male to female Back massage | cheap Back massage " + area + " | " + gender + " to male massage";
                            }
                        }
                        else
                        {
                            if (looking != "")
                            {
                                if (ddllookingfor.SelectedValue == "M")
                                {
                                    description = "At MyMassagePartner.com, find male and female manual massage partner in " + area + ", " + city + ". This page showing " + area + ", " + city + " based female and male who looking for manual body massage from his or her partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number of female and male who looking for body massage from male or female in " + area + ", " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available massage partner female and male for same massage in " + area + ", " + city + ". Exchange body massage with your female and male partner anytime, anywhere in " + area + ", " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + area + ", " + city + " is a good place to exchange body massage from female and male massage partner.";
                                    page_title.InnerHtml = "Manual body massage partner in " + area + ", " + city;
                                    title_page.InnerHtml = "Find manual body massage partner in " + area + ", " + city + " | MyMassagePartner";
                                    Meta_ = "Here you can find manual female and male body massage partners in " + area + ", " + city + " with their contact number, you can chat in regards of free body massage so let’s start body massage sessions with your selected partner in home and hotel - MyMassagePartner, " + area;
                                    keyword_ = "body massage " + area + " | erotic massage " + area + " | happy ending massage " + area + " | need body massage | full body massage in " + area + " | female to male Massage in " + area + " | sensual Massage in " + area + " | body massage at home in " + area + " | body massage in hotel in " + area + " | want body massage in " + area + " | massage in " + area + " | massage near me | female massager in " + area + " | free body massage in " + area + " | male to female body massage | cheap massage " + area;
                                }
                                else
                                {
                                    description = "At MyMassagePartner.com, find male and female professional massage partner in " + area + ", " + city + ". This page showing " + area + ", " + city + " based female and male who looking for professional body massage from his or her partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number of female and male who looking for body massage from male or female in " + area + ", " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available massage partner female and male for same massage in " + area + ", " + city + ". Exchange body massage with your female and male partner anytime, anywhere in " + area + ", " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + area + ", " + city + " is a good place to exchange body massage from female and male massage partner.";
                                    page_title.InnerHtml = "Professional body massage partner in " + area + ", " + city;
                                    title_page.InnerHtml = "Find professional body massage partner in " + area + ", " + city + " | MyMassagePartner";
                                    Meta_ = "Here you can find professional female and male body massage partners in " + area + ", " + city + " with their contact number, you can chat in regards of free body massage so let’s start body massage sessions with your selected partner in home and hotel - MyMassagePartner, " + area;
                                    keyword_ = "body massage " + area + " | erotic massage " + area + " | happy ending massage " + area + " | need body massage | full body massage in " + area + " | female to male Massage in " + area + " | sensual Massage in " + area + " | body massage at home in " + area + " | body massage in hotel in " + area + " | want body massage in " + area + " | massage in " + area + " | massage near me | female massager in " + area + " | free body massage in " + area + " | male to female body massage | cheap massage " + area;
                                }
                            }
                            else
                            {
                                description = "At MyMassagePartner.com, find female and male massage partner in " + area + ", " + city + ". This page showing " + area + ", " + city + " based female and male who looking for manual or professional body massage from his or her partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number of female and male who looking for body massage from male or female in " + area + ", " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available female and male massage partner for body massage in " + area + ", " + city + ". Exchange " + mtypes + " with your female and male partner anytime, anywhere in " + area + ", " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + area + ", " + city + " is a good place to exchange body massage from female and male massage partner.";
                                page_title.InnerHtml = "body massage partner in " + area + ", " + city;
                                title_page.InnerHtml = "Find body massage partner in " + area + ", " + city + " | MyMassagePartner";
                                Meta_ = "Here you can find female and male body massage partners in " + area + ", " + city + " with their contact number, you can chat in regards of free body massage so let’s start body massage sessions with your selected partner in home and hotel - MyMassagePartner, " + area;
                                keyword_ = "body massage " + area + " | erotic massage " + area + " | happy ending massage " + area + " | need body massage | full body massage in " + area + " | female to male Massage in " + area + " | sensual Massage in " + area + " | body massage at home in " + area + " | body massage in hotel in " + area + " | want body massage in " + area + " | massage in " + area + " | massage near me | female massager in " + area + " | free body massage in " + area + " | male to female body massage | cheap massage " + area;
                            }
                        }
                    }

                }
                else if (city != "")
                {
                    links_srp.InnerHtml = "<a href='https://www.mymassagepartner.com'>Home</a> > <a href='https://www.mymassagepartner.com" + "/massage-partner/" + country.Replace(' ', '-') + "/" + state.Replace(' ', '-') + "/a/All-Area/Any/all-types/all" + "'>" + state + "</a> > " + city + "";
                    if (mtypes != "")
                    {
                        if (gender != "")
                        {
                            if (looking != "")
                            {
                                if (ddllookingfor.SelectedValue == "M")
                                {
                                    if (gender == "Male")
                                    { description = "At MyMassagePartner.com, find " + gender + " manual massage partner for " + mtypes + " in " + city + ". This page showing " + city + " based " + gender.ToLower() + " who looking for manual " + mtypes + " from his partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number " + gender.ToLower() + " who looking for " + mtypes + " from male or female in " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for " + mtypes + " in " + city + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + city + " is a good place to exchange " + mtypes + " from " + gender + " massage partner."; }
                                    else
                                    {
                                        description = "At MyMassagePartner.com, find " + gender + " manual massage partner for " + mtypes + " in " + city + ". This page showing " + city + " based " + gender + " who looking for manual " + mtypes + " from her partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number " + gender + " who looking for " + mtypes + " from male or female in " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for " + mtypes + " in " + city + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + city + " is a good place to exchange " + mtypes + " from " + gender + " massage partner.";
                                    }
                                    page_title.InnerHtml = gender + " partner for manual " + mtypes.Trim() + " in " + city;
                                    title_page.InnerHtml = "Find " + gender + " partner for manual " + mtypes.Trim() + " in " + city + " | MyMassagePartner";
                                    Meta_ = "Here you can find manual " + gender + " massage partner for " + mtypes.Trim() + " in " + city + " with their contact number, you can chat in regards of free " + mtypes.Trim() + " so let’s start " + mtypes.Trim() + " sessions with your selected partner in home and hotel - MyMassagePartner - " + gender + " partner for manual " + mtypes.Trim() + ", " + city;
                                    keyword_ = gender + " partner " + mtypes.Trim() + " " + city + " | free " + mtypes.Trim() + " " + gender + " partner " + city + " | " + mtypes.Trim() + " " + gender + " in " + city + " | need " + gender + " for " + mtypes.Trim() + " | " + mtypes.Trim() + " " + gender + " partner in " + city + " | female to male " + mtypes.Trim() + " in " + city + " | " + mtypes.Trim() + " service by " + gender + " in " + city + " | " + mtypes.Trim() + " by " + gender + " at home in " + city + " | " + mtypes.Trim() + " by " + gender + " in hotel in " + city + " | want " + gender + " for " + mtypes.Trim() + " in " + city + " | massage in " + city + " by " + gender + " | " + mtypes.Trim() + " near me by " + gender + " | " + gender + " massager in " + city + " | free " + mtypes.Trim() + " in " + city + " | male to female " + mtypes.Trim() + " | cheap " + mtypes.Trim() + " " + city + " | " + gender + " to male massage";
                                }
                                else
                                {
                                    if (gender == "Male")
                                    { description = "At MyMassagePartner.com, find " + gender + " professional massage partner for " + mtypes + " in " + city + ". This page showing " + city + " based " + gender + " who looking for professional " + mtypes + " from his partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number " + gender + " who looking for " + mtypes + " from male or female in " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for " + mtypes + " in " + city + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + city + " is a good place to exchange " + mtypes + " from " + gender + " massage partner."; }
                                    else
                                    { description = "At MyMassagePartner.com, find " + gender + " professional massage partner for " + mtypes + " in " + city + ". This page showing " + city + " based " + gender + " who looking for professional " + mtypes + " from her partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number " + gender + " who looking for " + mtypes + " from male or female in " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for " + mtypes + " in " + city + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + city + " is a good place to exchange " + mtypes + " from " + gender + " massage partner."; }

                                    page_title.InnerHtml = gender + " partner for professional " + mtypes.Trim() + " in " + city;
                                    title_page.InnerHtml = "Find " + gender + " partner for professional " + mtypes.Trim() + " in " + city + " | MyMassagePartner";
                                    Meta_ = "Here you can find professional " + gender + " massage partner for " + mtypes.Trim() + " in " + city + " with their contact number, you can chat in regards of free " + mtypes.Trim() + " so let’s start " + mtypes.Trim() + " sessions with your selected partner in home and hotel - MyMassagePartner - " + gender + " partner for professional " + mtypes.Trim() + ", " + city;
                                    keyword_ = gender + " partner " + mtypes.Trim() + " " + city + " | free " + mtypes.Trim() + " " + gender + " partner " + city + " | " + mtypes.Trim() + " " + gender + " in " + city + " | need " + gender + " for " + mtypes.Trim() + " | " + mtypes.Trim() + " " + gender + " partner in " + city + " | female to male " + mtypes.Trim() + " in " + city + " | " + mtypes.Trim() + " service by " + gender + " in " + city + " | " + mtypes.Trim() + " by " + gender + " at home in " + city + " | " + mtypes.Trim() + " by " + gender + " in hotel in " + city + " | want " + gender + " for " + mtypes.Trim() + " in " + city + " | massage in " + city + " by " + gender + " | " + mtypes.Trim() + " near me by " + gender + " | " + gender + " massager in " + city + " | free " + mtypes.Trim() + " in " + city + " | male to female " + mtypes.Trim() + " | cheap " + mtypes.Trim() + " " + city + " | " + gender + " to male massage";
                                }
                            }
                            else
                            {
                                if (gender == "Male")
                                { description = "At MyMassagePartner.com, find " + gender + " massage partner for " + mtypes + " in " + city + ". This page showing " + city + " based " + gender + " who looking for manual or professional " + mtypes + " from his partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number " + gender + " who looking for " + mtypes + " from male or female in " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for " + mtypes + " in " + city + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + city + " is a good place to exchange " + mtypes + " from " + gender + " massage partner."; }
                                else
                                { description = "At MyMassagePartner.com, find " + gender + " massage partner for " + mtypes + " in " + city + ". This page showing " + city + " based " + gender + " who looking for manual or professional " + mtypes + " from her partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number " + gender + " who looking for " + mtypes + " from male or female in " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for " + mtypes + " in " + city + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + city + " is a good place to exchange " + mtypes + " from " + gender + " massage partner."; }

                                page_title.InnerHtml = gender + " " + mtypes.Trim() + " partner in " + city;
                                title_page.InnerHtml = "Find " + gender + " " + mtypes.Trim() + " partner in " + city + " | MyMassagePartner";
                                Meta_ = "Here you can find " + gender + " partner for " + mtypes.Trim() + " in " + city + " with their contact number, you can chat in regards of free " + mtypes.Trim() + " so let’s start " + mtypes.Trim() + " sessions with your selected partner in home and hotel - MyMassagePartner - " + gender + " partner for " + mtypes.Trim() + ", " + city;
                                keyword_ = gender + " partner " + mtypes.Trim() + " " + city + " | free " + mtypes.Trim() + " " + gender + " partner " + city + " | " + mtypes.Trim() + " " + gender + " in " + city + " | need " + gender + " for " + mtypes.Trim() + " | " + mtypes.Trim() + " " + gender + " partner in " + city + " | female to male " + mtypes.Trim() + " in " + city + " | " + mtypes.Trim() + " service by " + gender + " in " + city + " | " + mtypes.Trim() + " by " + gender + " at home in " + city + " | " + mtypes.Trim() + " by " + gender + " in hotel in " + city + " | want " + gender + " for " + mtypes.Trim() + " in " + city + " | massage in " + city + " by " + gender + " | " + mtypes.Trim() + " near me by " + gender + " | " + gender + " massager in " + city + " | free " + mtypes.Trim() + " in " + city + " | male to female " + mtypes.Trim() + " | cheap " + mtypes.Trim() + " " + city + " | " + gender + " to male massage";
                            }
                        }
                        else
                        {
                            if (looking != "")
                            {
                                if (ddllookingfor.SelectedValue == "M")
                                {
                                    description = "At MyMassagePartner.com, find female and male manual massage partner for " + mtypes + " in " + city + ". This page showing " + city + " based female and male who looking for manual or professional " + mtypes + " from his or her partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number of female and male who looking for " + mtypes + " from male or female in " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available female and male massage partner for " + mtypes + " in " + city + ". Exchange " + mtypes + " with your female and male partner anytime, anywhere in " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + city + " is a good place to exchange " + mtypes + " from female and male massage partner.";
                                    page_title.InnerHtml = "manual " + mtypes.Trim() + " partner in " + city;
                                    title_page.InnerHtml = "Find manual " + mtypes.Trim() + " partner in " + city + " | MyMassagePartner";
                                    Meta_ = "Here you can find manual massage partner for " + mtypes.Trim() + " in " + city + " with their contact number, you can chat in regards of free " + mtypes.Trim() + " so let’s start " + mtypes.Trim() + " sessions with your selected partner in home and hotel - MyMassagePartner";
                                    keyword_ = "massage partner " + mtypes.Trim() + " " + city + " | free " + mtypes.Trim() + " " + gender + " partner " + city + " | " + mtypes.Trim() + " " + gender + " in " + city + " | need " + gender + " for " + mtypes.Trim() + " | " + mtypes.Trim() + " " + gender + " partner in " + city + " | female to male " + mtypes.Trim() + " in " + city + " | " + mtypes.Trim() + " service by " + gender + " in " + city + " | " + mtypes.Trim() + " by " + gender + " at home in " + city + " | " + mtypes.Trim() + " by " + gender + " in hotel in " + city + " | want " + gender + " for " + mtypes.Trim() + " in " + city + " | massage in " + city + " by " + gender + " | " + mtypes.Trim() + " near me by " + gender + " | " + gender + " massager in " + city + " | free " + mtypes.Trim() + " in " + city + " | male to female " + mtypes.Trim() + " | cheap " + mtypes.Trim() + " " + city;
                                }
                                else
                                {
                                    description = "At MyMassagePartner.com, find female and male professional massage partner for " + mtypes + " in " + city + ". This page showing " + city + " based female and male massage who looking for manual or professional " + mtypes + " from his or her partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number of female and male who looking for " + mtypes + " from male or female in " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available female and male massage partner for " + mtypes + " in " + city + ". Exchange " + mtypes + " with your female and male partner anytime, anywhere in " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + city + " is a good place to exchange " + mtypes + " from female and male massage partner.";
                                    page_title.InnerHtml = "professional " + mtypes.Trim() + " partner in " + city;
                                    title_page.InnerHtml = "Find professional " + mtypes.Trim() + " partner in " + city + " | MyMassagePartner";
                                    Meta_ = "Here you can find professional massage partner for " + mtypes.Trim() + " in " + city + " with their contact number, you can chat in regards of free " + mtypes.Trim() + " so let’s start " + mtypes.Trim() + " sessions with your selected partner in home and hotel - MyMassagePartner";
                                    keyword_ = "massage partner " + mtypes.Trim() + " " + city + " | free " + mtypes.Trim() + " " + gender + " partner " + city + " | " + mtypes.Trim() + " " + gender + " in " + city + " | need " + gender + " for " + mtypes.Trim() + " | " + mtypes.Trim() + " " + gender + " partner in " + city + " | female to male " + mtypes.Trim() + " in " + city + " | " + mtypes.Trim() + " service by " + gender + " in " + city + " | " + mtypes.Trim() + " by " + gender + " at home in " + city + " | " + mtypes.Trim() + " by " + gender + " in hotel in " + city + " | want " + gender + " for " + mtypes.Trim() + " in " + city + " | massage in " + city + " by " + gender + " | " + mtypes.Trim() + " near me by " + gender + " | " + gender + " massager in " + city + " | free " + mtypes.Trim() + " in " + city + " | male to female " + mtypes.Trim() + " | cheap " + mtypes.Trim() + " " + city;
                                }
                            }
                            else
                            {
                                description = "At MyMassagePartner.com, find female and male massage partner in " + city + ". This page showing " + city + " based female and male who looking for manual or professional " + mtypes + " from his or her partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number of female and male who looking for " + mtypes + " from male or female in " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available female and male massage partner for " + mtypes + " in " + city + ". Exchange " + mtypes + " with your female and male partner anytime, anywhere in " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + city + " is a good place to exchange " + mtypes + " from female and male massage partner.";
                                page_title.InnerHtml = mtypes.Trim() + " partner in " + city;
                                title_page.InnerHtml = "Find " + mtypes.Trim() + " partner in " + city + " | MyMassagePartner";
                                Meta_ = "Here you can find " + mtypes.Trim() + " female and male body massage partners in " + city + " with their contact number, you can chat in regards of free " + mtypes.Trim() + " so let’s start " + mtypes.Trim() + " sessions with your selected partner in home and hotel - MyMassagePartner, " + city;
                                keyword_ = mtypes.Trim() + " " + city + " | free " + mtypes.Trim() + " " + city + " | " + mtypes.Trim() + " in " + city + " | need " + mtypes.Trim() + " | " + mtypes.Trim() + " partner in " + city + " | female to male " + mtypes.Trim() + " in " + city + " | " + mtypes.Trim() + " service in " + city + " | " + mtypes.Trim() + " at home in " + city + " | " + mtypes.Trim() + " in hotel in " + city + " | want " + mtypes.Trim() + " in " + city + " | massage in " + city + " | " + mtypes.Trim() + " near me | female massager in " + city + " | free " + mtypes.Trim() + " in " + city + " | male to female " + mtypes.Trim() + " | cheap " + mtypes.Trim() + " " + city;
                            }
                        }
                    }
                    else
                    {
                        mtypes = "body massage";
                        if (gender != "")
                        {
                            if (looking != "")
                            {
                                if (ddllookingfor.SelectedValue == "M")
                                {
                                    if (gender == "Male")
                                    { description = "At MyMassagePartner.com, find " + gender.ToLower() + " manual massage partner in " + city + ". This page showing " + city + " based " + gender + " who looking for manual body massage from his partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number " + gender + " who looking for body massage from male or female in " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for body massage in " + city + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + city + " is a good place to exchange body massage from " + gender + " massage partner."; }
                                    else
                                    { description = "At MyMassagePartner.com, find " + gender.ToLower() + " manual massage partner in " + city + ". This page showing " + city + " based " + gender + " who looking for manual body massage from her partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number " + gender + " who looking for body massage from male or female in " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for body massage in " + city + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + city + " is a good place to exchange body massage from " + gender + " massage partner."; }

                                    page_title.InnerHtml = gender + " manual massage partner in " + city;
                                    title_page.InnerHtml = "Find " + gender + " manual massage partner in " + city + " | MyMassagePartner";
                                    Meta_ = "Here you can find manual " + gender + " massage partner in " + city + " with their contact number, you can chat in regards of free body massage so let’s start body massage sessions with your selected partner in home and hotel - MyMassagePartner";
                                    keyword_ = gender + " massage partner " + city + " | free massage by " + gender + " partner " + city + " | " + gender + " massagers in " + city + " | need " + gender + " for massage | " + gender + " partner in " + city + " | female to male in " + city + " | massage service by " + gender + " in " + city + " | massage by " + gender + " at home in " + city + " | massage by " + gender + " in hotel in " + city + " | want " + gender + " for massage in " + city + " | massage in " + city + " by " + gender + " | manual massager near me | " + gender + " massager in " + city + " | free massage in " + city + " | male to female Back massage | cheap Back massage " + city + " | " + gender + " to male massage";
                                }
                                else
                                {
                                    if (gender == "Male")
                                    { description = "At MyMassagePartner.com, find " + gender + " professional massage partner in " + city + ". This page showing " + city + " based " + gender + " who looking for professional body massage from his partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number " + gender + " who looking for body massage from male or female in " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for body massage in " + city + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + city + " is a good place to exchange body massage from " + gender + " massage partner."; }
                                    else
                                    { description = "At MyMassagePartner.com, find " + gender + " professional massage partner in " + city + ". This page showing " + city + " based " + gender + " who looking for professional body massage from her partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number " + gender + " who looking for body massage from male or female in " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for body massage in " + city + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + city + " is a good place to exchange body massage from " + gender + " massage partner."; }
                                    page_title.InnerHtml = gender + " professional massage partner in " + city;
                                    title_page.InnerHtml = "Find " + gender + " professional massage partner in " + city + " | MyMassagePartner";
                                    Meta_ = "Here you can find professional " + gender + " massage partner in " + city + " with their contact number, you can chat in regards of free body massage so let’s start body massage sessions with your selected partner in home and hotel - MyMassagePartner";
                                    keyword_ = gender + " massage partner " + city + " | free massage by " + gender + " partner " + city + " | " + gender + " massagers in " + city + " | need " + gender + " for massage | " + gender + " partner in " + city + " | female to male in " + city + " | massage service by " + gender + " in " + city + " | massage by " + gender + " at home in " + city + " | massage by " + gender + " in hotel in " + city + " | want " + gender + " for massage in " + city + " | massage in " + city + " by " + gender + " | professional massager near me | " + gender + " massager in " + city + " | free massage in " + city + " | male to female Back massage | cheap Back massage " + city + " | " + gender + " to male massage";
                                }
                            }
                            else
                            {
                                if (gender == "Male")
                                { description = "At MyMassagePartner.com, find " + gender + " massage partner in " + city + ". This page showing " + city + " based " + gender + " who looking for manual or professional body massage from his partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number " + gender + " who looking for body massage from male or female in " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for body massage in " + city + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + city + " is a good place to exchange body massage from " + gender + " massage partner."; }
                                else
                                { description = "At MyMassagePartner.com, find " + gender + " massage partner in " + city + ". This page showing " + city + " based " + gender + " who looking for manual or professional body massage from her partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number " + gender + " who looking for body massage from male or female in " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for body massage in " + city + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + city + " is a good place to exchange body massage from " + gender + " massage partner."; }
                                page_title.InnerHtml = gender + " massage partner in " + city;
                                title_page.InnerHtml = "Find " + gender + " massage partner in " + city + " | MyMassagePartner";
                                Meta_ = "Here you can find " + gender + " massage partner in " + city + " with their contact number, you can chat in regards of free body massage so let’s start body massage sessions with your selected partner in home and hotel - MyMassagePartner";
                                keyword_ = gender + " massage partner " + city + " | free massage by " + gender + " partner " + city + " | " + gender + " massagers in " + city + " | need " + gender + " for massage | " + gender + " partner in " + city + " | female to male in " + city + " | massage service by " + gender + " in " + city + " | massage by " + gender + " at home in " + city + " | massage by " + gender + " in hotel in " + city + " | want " + gender + " for massage in " + city + " | massage in " + city + " by " + gender + " | manual massager near me | " + gender + " massager in " + city + " | free massage in " + city + " | male to female Back massage | cheap Back massage " + city + " | " + gender + " to male massage";
                            }
                        }
                        else
                        {
                            if (looking != "")
                            {
                                if (ddllookingfor.SelectedValue == "M")
                                {
                                    description = "At MyMassagePartner.com, find male and female manual massage partner in " + city + ". This page showing " + city + " based female and male who looking for manual body massage from his or her partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number of female and male who looking for body massage from male or female in " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available massage partner female and male for same massage in " + city + ". Exchange body massage with your female and male partner anytime, anywhere in " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + city + " is a good place to exchange body massage from female and male massage partner.";
                                    page_title.InnerHtml = "Manual body massage partner in " + city;
                                    title_page.InnerHtml = "Find manual body massage partner in " + city + " | MyMassagePartner";
                                    Meta_ = "Here you can find manual female and male body massage partners in " + city + " with their contact number, you can chat in regards of free body massage so let’s start body massage sessions with your selected partner in home and hotel - MyMassagePartner, " + city;
                                    keyword_ = "body massage " + city + " | erotic massage " + city + " | happy ending massage " + city + " | need body massage | full body massage in " + city + " | female to male Massage in " + city + " | sensual Massage in " + city + " | body massage at home in " + city + " | body massage in hotel in " + city + " | want body massage in " + city + " | massage in " + city + " | massage near me | female massager in " + city + " | free body massage in " + city + " | male to female body massage | cheap massage " + city;
                                }
                                else
                                {
                                    description = "At MyMassagePartner.com, find male and female professional massage partner in " + city + ". This page showing " + city + " based female and male who looking for professional body massage from his or her partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number of female and male who looking for body massage from male or female in " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available massage partner female and male for same massage in " + city + ". Exchange body massage with your female and male partner anytime, anywhere in " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + city + " is a good place to exchange body massage from female and male massage partner.";
                                    page_title.InnerHtml = "Professional body massage partner in " + city;
                                    title_page.InnerHtml = "Find professional body massage partner in " + city + " | MyMassagePartner";
                                    Meta_ = "Here you can find professional female and male body massage partners in " + city + " with their contact number, you can chat in regards of free body massage so let’s start body massage sessions with your selected partner in home and hotel - MyMassagePartner, " + city;
                                    keyword_ = "body massage " + city + " | erotic massage " + city + " | happy ending massage " + city + " | need body massage | full body massage in " + city + " | female to male Massage in " + city + " | sensual Massage in " + city + " | body massage at home in " + city + " | body massage in hotel in " + city + " | want body massage in " + city + " | massage in " + city + " | massage near me | female massager in " + city + " | free body massage in " + city + " | male to female body massage | cheap massage " + city;
                                }
                            }
                            else
                            {
                                description = "At MyMassagePartner.com, find female and male massage partner in " + city + ". This page showing " + city + " based female and male who looking for manual or professional body massage from his or her partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number of female and male who looking for body massage from male or female in " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available female and male massage partner for body massage in " + city + ". Exchange " + mtypes + " with your female and male partner anytime, anywhere in " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + city + " is a good place to exchange body massage from female and male massage partner.";
                                page_title.InnerHtml = "body massage partner in " + city;
                                title_page.InnerHtml = "Find body massage partner in " + city + " | MyMassagePartner";
                                Meta_ = "Here you can find female and male body massage partners in " + city + " with their contact number, you can chat in regards of free body massage so let’s start body massage sessions with your selected partner in home and hotel - MyMassagePartner, " + city;
                                keyword_ = "body massage " + city + " | erotic massage " + city + " | happy ending massage " + city + " | need body massage | full body massage in " + city + " | female to male Massage in " + city + " | sensual Massage in " + city + " | body massage at home in " + city + " | body massage in hotel in " + city + " | want body massage in " + city + " | massage in " + city + " | massage near me | female massager in " + city + " | free body massage in " + city + " | male to female body massage | cheap massage " + city;
                            }
                        }
                    }
                }
                else if (state != "")
                {
                    links_srp.InnerHtml = "<a href='https://www.mymassagepartner.com'>Home</a> > " + state + "";
                    if (mtypes != "")
                    {
                        if (gender != "")
                        {
                            if (looking != "")
                            {
                                if (ddllookingfor.SelectedValue == "M")
                                {
                                    if (gender == "Male")
                                    { description = "At MyMassagePartner.com, find " + gender + " manual massage partner for " + mtypes + " in " + state + ". This page showing " + state + " based " + gender.ToLower() + " who looking for manual " + mtypes + " from his partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number " + gender.ToLower() + " who looking for " + mtypes + " from male or female in " + state + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for " + mtypes + " in " + state + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + state + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + state + " is a good place to exchange " + mtypes + " from " + gender + " massage partner."; }
                                    else
                                    {
                                        description = "At MyMassagePartner.com, find " + gender + " manual massage partner for " + mtypes + " in " + state + ". This page showing " + state + " based " + gender + " who looking for manual " + mtypes + " from her partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number " + gender + " who looking for " + mtypes + " from male or female in " + state + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for " + mtypes + " in " + state + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + state + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + state + " is a good place to exchange " + mtypes + " from " + gender + " massage partner.";
                                    } page_title.InnerHtml = gender + " partner for manual " + mtypes.Trim() + " in " + state;
                                    title_page.InnerHtml = "Find " + gender + " partner for manual " + mtypes.Trim() + " in " + state + " | MyMassagePartner";
                                    Meta_ = "Here you can find manual " + gender + " massage partner for " + mtypes.Trim() + " in " + state + " with their contact number, you can chat in regards of free " + mtypes.Trim() + " so let’s start " + mtypes.Trim() + " sessions with your selected partner in home and hotel - MyMassagePartner - " + gender + " partner for manual " + mtypes.Trim() + ", " + state;
                                    keyword_ = gender + " partner " + mtypes.Trim() + " " + state + " | free " + mtypes.Trim() + " " + gender + " partner " + state + " | " + mtypes.Trim() + " " + gender + " in " + state + " | need " + gender + " for " + mtypes.Trim() + " | " + mtypes.Trim() + " " + gender + " partner in " + state + " | female to male " + mtypes.Trim() + " in " + state + " | " + mtypes.Trim() + " service by " + gender + " in " + state + " | " + mtypes.Trim() + " by " + gender + " at home in " + state + " | " + mtypes.Trim() + " by " + gender + " in hotel in " + state + " | want " + gender + " for " + mtypes.Trim() + " in " + state + " | massage in " + state + " by " + gender + " | " + mtypes.Trim() + " near me by " + gender + " | " + gender + " massager in " + state + " | free " + mtypes.Trim() + " in " + state + " | male to female " + mtypes.Trim() + " | cheap " + mtypes.Trim() + " " + state + " | " + gender + " to male massage";
                                }
                                else
                                {
                                    if (gender == "Male")
                                    { description = "At MyMassagePartner.com, find " + gender + " professional massage partner for " + mtypes + " in " + state + ". This page showing " + state + " based " + gender + " who looking for professional " + mtypes + " from his partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number " + gender + " who looking for " + mtypes + " from male or female in " + state + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for " + mtypes + " in " + state + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + state + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + state + " is a good place to exchange " + mtypes + " from " + gender + " massage partner."; }
                                    else
                                    { description = "At MyMassagePartner.com, find " + gender + " professional massage partner for " + mtypes + " in " + state + ". This page showing " + state + " based " + gender + " who looking for professional " + mtypes + " from her partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number " + gender + " who looking for " + mtypes + " from male or female in " + state + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for " + mtypes + " in " + state + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + state + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + state + " is a good place to exchange " + mtypes + " from " + gender + " massage partner."; }
                                    page_title.InnerHtml = gender + " partner for professional " + mtypes.Trim() + " in " + state;
                                    title_page.InnerHtml = "Find " + gender + " partner for professional " + mtypes.Trim() + " in " + state + " | MyMassagePartner";
                                    Meta_ = "Here you can find professional " + gender + " massage partner for " + mtypes.Trim() + " in " + state + " with their contact number, you can chat in regards of free " + mtypes.Trim() + " so let’s start " + mtypes.Trim() + " sessions with your selected partner in home and hotel - MyMassagePartner - " + gender + " partner for professional " + mtypes.Trim() + ", " + state;
                                    keyword_ = gender + " partner " + mtypes.Trim() + " " + state + " | free " + mtypes.Trim() + " " + gender + " partner " + state + " | " + mtypes.Trim() + " " + gender + " in " + state + " | need " + gender + " for " + mtypes.Trim() + " | " + mtypes.Trim() + " " + gender + " partner in " + state + " | female to male " + mtypes.Trim() + " in " + state + " | " + mtypes.Trim() + " service by " + gender + " in " + state + " | " + mtypes.Trim() + " by " + gender + " at home in " + state + " | " + mtypes.Trim() + " by " + gender + " in hotel in " + state + " | want " + gender + " for " + mtypes.Trim() + " in " + state + " | massage in " + state + " by " + gender + " | " + mtypes.Trim() + " near me by " + gender + " | " + gender + " massager in " + state + " | free " + mtypes.Trim() + " in " + state + " | male to female " + mtypes.Trim() + " | cheap " + mtypes.Trim() + " " + state + " | " + gender + " to male massage";
                                }
                            }
                            else
                            {
                                if (gender == "Male")
                                { description = "At MyMassagePartner.com, find " + gender + " massage partner for " + mtypes + " in " + state + ". This page showing " + state + " based " + gender + " who looking for manual or professional " + mtypes + " from his partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number " + gender + " who looking for " + mtypes + " from male or female in " + state + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for " + mtypes + " in " + state + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + state + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + state + " is a good place to exchange " + mtypes + " from " + gender + " massage partner."; }
                                else
                                { description = "At MyMassagePartner.com, find " + gender + " massage partner for " + mtypes + " in " + state + ". This page showing " + state + " based " + gender + " who looking for manual or professional " + mtypes + " from her partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number " + gender + " who looking for " + mtypes + " from male or female in " + state + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for " + mtypes + " in " + state + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + state + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + state + " is a good place to exchange " + mtypes + " from " + gender + " massage partner."; }
                                page_title.InnerHtml = gender + " " + mtypes.Trim() + " partner in " + state + ", " + country;
                                title_page.InnerHtml = "Find " + gender + " " + mtypes.Trim() + " partner in " + state + ", " + country + " | MyMassagePartner";
                                Meta_ = "Here you can find " + gender + " partner for " + mtypes.Trim() + " in " + state + " with their contact number, you can chat in regards of free " + mtypes.Trim() + " so let’s start " + mtypes.Trim() + " sessions with your selected partner in home and hotel - MyMassagePartner - " + gender + " partner for " + mtypes.Trim() + ", " + state;
                                keyword_ = gender + " partner " + mtypes.Trim() + " " + state + " | free " + mtypes.Trim() + " " + gender + " partner " + state + " | " + mtypes.Trim() + " " + gender + " in " + state + " | need " + gender + " for " + mtypes.Trim() + " | " + mtypes.Trim() + " " + gender + " partner in " + state + " | female to male " + mtypes.Trim() + " in " + state + " | " + mtypes.Trim() + " service by " + gender + " in " + state + " | " + mtypes.Trim() + " by " + gender + " at home in " + state + " | " + mtypes.Trim() + " by " + gender + " in hotel in " + state + " | want " + gender + " for " + mtypes.Trim() + " in " + state + " | massage in " + state + " by " + gender + " | " + mtypes.Trim() + " near me by " + gender + " | " + gender + " massager in " + state + " | free " + mtypes.Trim() + " in " + state + " | male to female " + mtypes.Trim() + " | cheap " + mtypes.Trim() + " " + state + " | " + gender + " to male massage";
                            }
                        }
                        else
                        {
                            if (looking != "")
                            {
                                if (ddllookingfor.SelectedValue == "M")
                                {
                                    description = "At MyMassagePartner.com, find female and male manual massage partner for " + mtypes + " in " + state + ". This page showing " + state + " based female and male who looking for manual or professional " + mtypes + " from his or her partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number of female and male who looking for " + mtypes + " from male or female in " + state + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available female and male massage partner for " + mtypes + " in " + state + ". Exchange " + mtypes + " with your female and male partner anytime, anywhere in " + state + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + state + " is a good place to exchange " + mtypes + " from female and male massage partner.";
                                    page_title.InnerHtml = "manual " + mtypes.Trim() + " partner in " + state;
                                    title_page.InnerHtml = "Find manual " + mtypes.Trim() + " partner in " + state + " | MyMassagePartner";
                                    Meta_ = "Here you can find manual massage partner for " + mtypes.Trim() + " in " + state + " with their contact number, you can chat in regards of free " + mtypes.Trim() + " so let’s start " + mtypes.Trim() + " sessions with your selected partner in home and hotel - MyMassagePartner";
                                    keyword_ = "massage partner " + mtypes.Trim() + " " + state + " | free " + mtypes.Trim() + " " + gender + " partner " + state + " | " + mtypes.Trim() + " " + gender + " in " + state + " | need " + gender + " for " + mtypes.Trim() + " | " + mtypes.Trim() + " " + gender + " partner in " + state + " | female to male " + mtypes.Trim() + " in " + state + " | " + mtypes.Trim() + " service by " + gender + " in " + state + " | " + mtypes.Trim() + " by " + gender + " at home in " + state + " | " + mtypes.Trim() + " by " + gender + " in hotel in " + state + " | want " + gender + " for " + mtypes.Trim() + " in " + state + " | massage in " + state + " by " + gender + " | " + mtypes.Trim() + " near me by " + gender + " | " + gender + " massager in " + state + " | free " + mtypes.Trim() + " in " + state + " | male to female " + mtypes.Trim() + " | cheap " + mtypes.Trim() + " " + state;
                                }
                                else
                                {
                                    description = "At MyMassagePartner.com, find female and male professional massage partner for " + mtypes + " in " + state + ". This page showing " + state + " based female and male massage who looking for manual or professional " + mtypes + " from his or her partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number of female and male who looking for " + mtypes + " from male or female in " + state + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available female and male massage partner for " + mtypes + " in " + state + ". Exchange " + mtypes + " with your female and male partner anytime, anywhere in " + state + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + state + " is a good place to exchange " + mtypes + " from female and male massage partner.";
                                    page_title.InnerHtml = "professional " + mtypes.Trim() + " partner in " + state;
                                    title_page.InnerHtml = "Find professional " + mtypes.Trim() + " partner in " + state + " | MyMassagePartner";
                                    Meta_ = "Here you can find professional massage partner for " + mtypes.Trim() + " in " + state + " with their contact number, you can chat in regards of free " + mtypes.Trim() + " so let’s start " + mtypes.Trim() + " sessions with your selected partner in home and hotel - MyMassagePartner";
                                    keyword_ = "massage partner " + mtypes.Trim() + " " + state + " | free " + mtypes.Trim() + " " + gender + " partner " + state + " | " + mtypes.Trim() + " " + gender + " in " + state + " | need " + gender + " for " + mtypes.Trim() + " | " + mtypes.Trim() + " " + gender + " partner in " + state + " | female to male " + mtypes.Trim() + " in " + state + " | " + mtypes.Trim() + " service by " + gender + " in " + state + " | " + mtypes.Trim() + " by " + gender + " at home in " + state + " | " + mtypes.Trim() + " by " + gender + " in hotel in " + state + " | want " + gender + " for " + mtypes.Trim() + " in " + state + " | massage in " + state + " by " + gender + " | " + mtypes.Trim() + " near me by " + gender + " | " + gender + " massager in " + state + " | free " + mtypes.Trim() + " in " + state + " | male to female " + mtypes.Trim() + " | cheap " + mtypes.Trim() + " " + state;
                                }
                            }
                            else
                            {
                                description = "At MyMassagePartner.com, find female and male massage partner in " + state + ". This page showing " + state + " based female and male who looking for manual or professional " + mtypes + " from his or her partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number of female and male who looking for " + mtypes + " from male or female in " + state + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available female and male massage partner for " + mtypes + " in " + state + ". Exchange " + mtypes + " with your female and male partner anytime, anywhere in " + state + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + state + " is a good place to exchange " + mtypes + " from female and male massage partner.";
                                page_title.InnerHtml = mtypes.Trim() + " partner in " + state + ", " + country;
                                title_page.InnerHtml = "Find " + mtypes.Trim() + " partner in " + state + ", " + country + " | MyMassagePartner";
                                Meta_ = "Here you can find " + mtypes.Trim() + " female and male body massage partners in " + state + " with their contact number, you can chat in regards of free " + mtypes.Trim() + " so let’s start " + mtypes.Trim() + " sessions with your selected partner in home and hotel - MyMassagePartner, " + state;
                                keyword_ = mtypes.Trim() + " " + state + " | free " + mtypes.Trim() + " " + state + " | " + mtypes.Trim() + " in " + state + " | need " + mtypes.Trim() + " | " + mtypes.Trim() + " partner in " + state + " | female to male " + mtypes.Trim() + " in " + state + " | " + mtypes.Trim() + " service in " + state + " | " + mtypes.Trim() + " at home in " + state + " | " + mtypes.Trim() + " in hotel in " + state + " | want " + mtypes.Trim() + " in " + state + " | massage in " + state + " | " + mtypes.Trim() + " near me | female massager in " + state + " | free " + mtypes.Trim() + " in " + state + " | male to female " + mtypes.Trim() + " | cheap " + mtypes.Trim() + " " + state;
                            }
                        }
                    }
                    else
                    {
                        mtypes = "body massage";
                        if (gender != "")
                        {
                            if (looking != "")
                            {
                                if (ddllookingfor.SelectedValue == "M")
                                {
                                    if (gender == "Male")
                                    { description = "At MyMassagePartner.com, find " + gender.ToLower() + " manual massage partner in " + state + ". This page showing " + state + " based " + gender + " who looking for manual body massage from his partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number " + gender + " who looking for body massage from male or female in " + state + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for body massage in " + state + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + state + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + state + " is a good place to exchange body massage from " + gender + " massage partner."; }
                                    else
                                    { description = "At MyMassagePartner.com, find " + gender.ToLower() + " manual massage partner in " + state + ". This page showing " + state + " based " + gender + " who looking for manual body massage from her partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number " + gender + " who looking for body massage from male or female in " + state + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for body massage in " + state + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + state + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + state + " is a good place to exchange body massage from " + gender + " massage partner."; }
                                    page_title.InnerHtml = gender + " manual massage partner in " + state;
                                    title_page.InnerHtml = "Find " + gender + " manual massage partner in " + state + " | MyMassagePartner";
                                    Meta_ = "Here you can find manual " + gender + " massage partner in " + state + " with their contact number, you can chat in regards of free body massage so let’s start body massage sessions with your selected partner in home and hotel - MyMassagePartner";
                                    keyword_ = gender + " massage partner " + state + " | free massage by " + gender + " partner " + state + " | " + gender + " massagers in " + state + " | need " + gender + " for massage | " + gender + " partner in " + state + " | female to male in " + state + " | massage service by " + gender + " in " + state + " | massage by " + gender + " at home in " + state + " | massage by " + gender + " in hotel in " + state + " | want " + gender + " for massage in " + state + " | massage in " + state + " by " + gender + " | manual massager near me | " + gender + " massager in " + state + " | free massage in " + state + " | male to female Back massage | cheap Back massage " + state + " | " + gender + " to male massage";
                                }
                                else
                                {
                                    if (gender == "Male")
                                    { description = "At MyMassagePartner.com, find " + gender + " professional massage partner in " + state + ". This page showing " + state + " based " + gender + " who looking for professional body massage from his partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number " + gender + " who looking for body massage from male or female in " + state + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for body massage in " + state + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + state + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + state + " is a good place to exchange body massage from " + gender + " massage partner."; }
                                    else
                                    { description = "At MyMassagePartner.com, find " + gender + " professional massage partner in " + state + ". This page showing " + state + " based " + gender + " who looking for professional body massage from her partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number " + gender + " who looking for body massage from male or female in " + state + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for body massage in " + state + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + state + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + state + " is a good place to exchange body massage from " + gender + " massage partner."; }
                                    page_title.InnerHtml = gender + " professional massage partner in " + state;
                                    title_page.InnerHtml = "Find " + gender + " professional massage partner in " + state + " | MyMassagePartner";
                                    Meta_ = "Here you can find professional " + gender + " massage partner in " + state + " with their contact number, you can chat in regards of free body massage so let’s start body massage sessions with your selected partner in home and hotel - MyMassagePartner";
                                    keyword_ = gender + " massage partner " + state + " | free massage by " + gender + " partner " + state + " | " + gender + " massagers in " + state + " | need " + gender + " for massage | " + gender + " partner in " + state + " | female to male in " + state + " | massage service by " + gender + " in " + state + " | massage by " + gender + " at home in " + state + " | massage by " + gender + " in hotel in " + state + " | want " + gender + " for massage in " + state + " | massage in " + state + " by " + gender + " | professional massager near me | " + gender + " massager in " + state + " | free massage in " + state + " | male to female Back massage | cheap Back massage " + state + " | " + gender + " to male massage";
                                }
                            }
                            else
                            {
                                if (gender == "Male")
                                { description = "At MyMassagePartner.com, find " + gender + " massage partner in " + state + ". This page showing " + state + " based " + gender + " who looking for manual or professional body massage from his partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number " + gender + " who looking for body massage from male or female in " + state + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for body massage in " + state + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + state + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + state + " is a good place to exchange body massage from " + gender + " massage partner."; }
                                else
                                { description = "At MyMassagePartner.com, find " + gender + " massage partner in " + state + ". This page showing " + state + " based " + gender + " who looking for manual or professional body massage from her partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number " + gender + " who looking for body massage from male or female in " + state + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for body massage in " + state + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + state + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + state + " is a good place to exchange body massage from " + gender + " massage partner."; }
                                page_title.InnerHtml = gender + " massage partner in " + state;
                                title_page.InnerHtml = "Find " + gender + " massage partner in " + state + " | MyMassagePartner";
                                Meta_ = "Here you can find " + gender + " massage partner in " + state + " with their contact number, you can chat in regards of free body massage so let’s start body massage sessions with your selected partner in home and hotel - MyMassagePartner";
                                keyword_ = gender + " massage partner " + state + " | free massage by " + gender + " partner " + state + " | " + gender + " massagers in " + state + " | need " + gender + " for massage | " + gender + " partner in " + state + " | female to male in " + state + " | massage service by " + gender + " in " + state + " | massage by " + gender + " at home in " + state + " | massage by " + gender + " in hotel in " + state + " | want " + gender + " for massage in " + state + " | massage in " + state + " by " + gender + " | manual massager near me | " + gender + " massager in " + state + " | free massage in " + state + " | male to female Back massage | cheap Back massage " + state + " | " + gender + " to male massage";
                            }
                        }
                        else
                        {
                            if (looking != "")
                            {
                                if (ddllookingfor.SelectedValue == "M")
                                {
                                    description = "At MyMassagePartner.com, find male and female manual massage partner in " + state + ". This page showing " + state + " based female and male who looking for manual body massage from his or her partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number of female and male who looking for body massage from male or female in " + state + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available massage partner female and male for same massage in " + state + ". Exchange body massage with your female and male partner anytime, anywhere in " + state + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + state + " is a good place to exchange body massage from female and male massage partner.";
                                    page_title.InnerHtml = "Manual body massage partner in " + state + ", " + country;
                                    title_page.InnerHtml = "Find manual body massage partner in " + state + ", " + country + " | MyMassagePartner";
                                    Meta_ = "Here you can find manual female and male body massage partners in " + state + " with their contact number, you can chat in regards of free body massage so let’s start body massage sessions with your selected partner in home and hotel - MyMassagePartner";
                                    keyword_ = "body massage " + state + " | erotic massage " + state + " | happy ending massage " + state + " | need body massage | full body massage in " + state + " | female to male Massage in " + state + " | sensual Massage in " + state + " | body massage at home in " + state + " | body massage in hotel in " + state + " | want body massage in " + state + " | massage in " + state + " | massage near me | female massager in " + state + " | free body massage in " + state + " | male to female body massage | cheap massage " + area;

                                }
                                else
                                {
                                    description = "At MyMassagePartner.com, find male and female professional massage partner in " + state + ". This page showing " + state + " based female and male who looking for professional body massage from his or her partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number of female and male who looking for body massage from male or female in " + state + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available massage partner female and male for same massage in " + state + ". Exchange body massage with your female and male partner anytime, anywhere in " + state + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + state + " is a good place to exchange body massage from female and male massage partner.";
                                    page_title.InnerHtml = "Professional body massage partner in " + state + ", " + country;
                                    title_page.InnerHtml = "Find professional body massage partner in " + state + ", " + country + " | MyMassagePartner";
                                    Meta_ = "Here you can find professional female and male body massage partners in " + state + " with their contact number, you can chat in regards of free body massage so let’s start body massage sessions with your selected partner in home and hotel - MyMassagePartner";
                                    keyword_ = "body massage " + state + " | erotic massage " + state + " | happy ending massage " + state + " | need body massage | full body massage in " + state + " | female to male Massage in " + state + " | sensual Massage in " + state + " | body massage at home in " + state + " | body massage in hotel in " + state + " | want body massage in " + state + " | massage in " + state + " | massage near me | female massager in " + state + " | free body massage in " + state + " | male to female body massage | cheap massage " + area;
                                }
                            }
                            else
                            {
                                description = "At MyMassagePartner.com, find female and male massage partner in " + state + ". This page showing " + state + " based female and male who looking for manual or professional body massage from his or her partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number of female and male who looking for body massage from male or female in " + state + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available female and male massage partner for body massage in " + state + ". Exchange " + mtypes + " with your female and male partner anytime, anywhere in " + state + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + state + " is a good place to exchange body massage from female and male massage partner.";
                                page_title.InnerHtml = "body massage partner in " + state + ", " + country;
                                title_page.InnerHtml = "Find body massage partner in " + state + ", " + country + " | MyMassagePartner";
                                Meta_ = "Here you can find female and male body massage partners in " + state + " with their contact number, you can chat in regards of free body massage so let’s start body massage sessions with your selected partner in home and hotel - MyMassagePartner, " + state;
                                keyword_ = "body massage " + state + " | erotic massage " + state + " | happy ending massage " + state + " | need body massage | full body massage in " + state + " | female to male Massage in " + state + " | sensual Massage in " + state + " | body massage at home in " + state + " | body massage in hotel in " + state + " | want body massage in " + state + " | massage in " + state + " | massage near me | female massager in " + state + " | free body massage in " + state + " | male to female body massage | cheap massage " + state;
                            }
                        }
                    }
                }
                else
                {
                    links_srp.InnerHtml = "<a href='https://www.mymassagepartner.com'>Home</a>";
                    if (mtypes != "")
                    {
                        if (gender != "")
                        {
                            if (looking != "")
                            {
                                if (ddllookingfor.SelectedValue == "M")
                                {
                                    if (gender == "Male")
                                    { description = "At MyMassagePartner.com, find " + gender + " manual massage partner for " + mtypes + " in " + country + ". This page showing " + country + " based " + gender.ToLower() + " who looking for manual " + mtypes + " from his partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number " + gender.ToLower() + " who looking for " + mtypes + " from male or female in " + country + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for " + mtypes + " in " + country + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + country + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + country + " is a good place to exchange " + mtypes + " from " + gender + " massage partner."; }
                                    else
                                    {
                                        description = "At MyMassagePartner.com, find " + gender + " manual massage partner for " + mtypes + " in " + country + ". This page showing " + country + " based " + gender + " who looking for manual " + mtypes + " from her partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number " + gender + " who looking for " + mtypes + " from male or female in " + country + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for " + mtypes + " in " + country + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + country + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + country + " is a good place to exchange " + mtypes + " from " + gender + " massage partner.";
                                    } page_title.InnerHtml = gender + " partner for manual " + mtypes.Trim() + " in " + country;
                                    title_page.InnerHtml = "Find " + gender + " partner for manual " + mtypes.Trim() + " in " + country + " | MyMassagePartner";
                                    Meta_ = "Here you can find manual " + gender + " massage partner for " + mtypes.Trim() + " in " + country + " with their contact number, you can chat in regards of free " + mtypes.Trim() + " so let’s start " + mtypes.Trim() + " sessions with your selected partner in home and hotel - MyMassagePartner - " + gender + " partner for manual " + mtypes.Trim() + ", " + country;
                                    keyword_ = gender + " partner " + mtypes.Trim() + " " + country + " | free " + mtypes.Trim() + " " + gender + " partner " + country + " | " + mtypes.Trim() + " " + gender + " in " + country + " | need " + gender + " for " + mtypes.Trim() + " | " + mtypes.Trim() + " " + gender + " partner in " + country + " | female to male " + mtypes.Trim() + " in " + country + " | " + mtypes.Trim() + " service by " + gender + " in " + country + " | " + mtypes.Trim() + " by " + gender + " at home in " + country + " | " + mtypes.Trim() + " by " + gender + " in hotel in " + country + " | want " + gender + " for " + mtypes.Trim() + " in " + country + " | massage in " + country + " by " + gender + " | " + mtypes.Trim() + " near me by " + gender + " | " + gender + " massager in " + country + " | free " + mtypes.Trim() + " in " + country + " | male to female " + mtypes.Trim() + " | cheap " + mtypes.Trim() + " " + country + " | " + gender + " to male massage";
                                }
                                else
                                {
                                    if (gender == "Male")
                                    { description = "At MyMassagePartner.com, find " + gender + " professional massage partner for " + mtypes + " in " + country + ". This page showing " + country + " based " + gender + " who looking for professional " + mtypes + " from his partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number " + gender + " who looking for " + mtypes + " from male or female in " + country + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for " + mtypes + " in " + country + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + country + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + country + " is a good place to exchange " + mtypes + " from " + gender + " massage partner."; }
                                    else
                                    { description = "At MyMassagePartner.com, find " + gender + " professional massage partner for " + mtypes + " in " + country + ". This page showing " + country + " based " + gender + " who looking for professional " + mtypes + " from her partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number " + gender + " who looking for " + mtypes + " from male or female in " + country + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for " + mtypes + " in " + country + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + country + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + country + " is a good place to exchange " + mtypes + " from " + gender + " massage partner."; }
                                    page_title.InnerHtml = gender + " partner for professional " + mtypes.Trim() + " in " + country;
                                    title_page.InnerHtml = "Find " + gender + " partner for professional " + mtypes.Trim() + " in " + country + " | MyMassagePartner";
                                    Meta_ = "Here you can find professional " + gender + " massage partner for " + mtypes.Trim() + " in " + country + " with their contact number, you can chat in regards of free " + mtypes.Trim() + " so let’s start " + mtypes.Trim() + " sessions with your selected partner in home and hotel - MyMassagePartner - " + gender + " partner for professional " + mtypes.Trim() + ", " + country;
                                    keyword_ = gender + " partner " + mtypes.Trim() + " " + country + " | free " + mtypes.Trim() + " " + gender + " partner " + country + " | " + mtypes.Trim() + " " + gender + " in " + country + " | need " + gender + " for " + mtypes.Trim() + " | " + mtypes.Trim() + " " + gender + " partner in " + country + " | female to male " + mtypes.Trim() + " in " + country + " | " + mtypes.Trim() + " service by " + gender + " in " + country + " | " + mtypes.Trim() + " by " + gender + " at home in " + country + " | " + mtypes.Trim() + " by " + gender + " in hotel in " + country + " | want " + gender + " for " + mtypes.Trim() + " in " + country + " | massage in " + country + " by " + gender + " | " + mtypes.Trim() + " near me by " + gender + " | " + gender + " massager in " + country + " | free " + mtypes.Trim() + " in " + country + " | male to female " + mtypes.Trim() + " | cheap " + mtypes.Trim() + " " + country + " | " + gender + " to male massage";
                                }
                            }
                            else
                            {
                                if (gender == "Male")
                                { description = "At MyMassagePartner.com, find " + gender + " massage partner for " + mtypes + " in " + country + ". This page showing " + country + " based " + gender + " who looking for manual or professional " + mtypes + " from his partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number " + gender + " who looking for " + mtypes + " from male or female in " + country + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for " + mtypes + " in " + country + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + country + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + country + " is a good place to exchange " + mtypes + " from " + gender + " massage partner."; }
                                else
                                { description = "At MyMassagePartner.com, find " + gender + " massage partner for " + mtypes + " in " + country + ". This page showing " + country + " based " + gender + " who looking for manual or professional " + mtypes + " from her partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number " + gender + " who looking for " + mtypes + " from male or female in " + country + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for " + mtypes + " in " + country + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + country + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + country + " is a good place to exchange " + mtypes + " from " + gender + " massage partner."; }
                                page_title.InnerHtml = gender + " " + mtypes.Trim() + " partner in " + country;
                                title_page.InnerHtml = "Find " + gender + " " + mtypes.Trim() + " partner in " + country + " | MyMassagePartner";
                                Meta_ = "Here you can find " + gender + " partner for " + mtypes.Trim() + " in " + country + " with their contact number, you can chat in regards of free " + mtypes.Trim() + " so let’s start " + mtypes.Trim() + " sessions with your selected partner in home and hotel - MyMassagePartner - " + gender + " partner for " + mtypes.Trim() + ", " + country;
                                keyword_ = gender + " partner " + mtypes.Trim() + " " + country + " | free " + mtypes.Trim() + " " + gender + " partner " + country + " | " + mtypes.Trim() + " " + gender + " in " + country + " | need " + gender + " for " + mtypes.Trim() + " | " + mtypes.Trim() + " " + gender + " partner in " + country + " | female to male " + mtypes.Trim() + " in " + country + " | " + mtypes.Trim() + " service by " + gender + " in " + country + " | " + mtypes.Trim() + " by " + gender + " at home in " + country + " | " + mtypes.Trim() + " by " + gender + " in hotel in " + country + " | want " + gender + " for " + mtypes.Trim() + " in " + country + " | massage in " + country + " by " + gender + " | " + mtypes.Trim() + " near me by " + gender + " | " + gender + " massager in " + country + " | free " + mtypes.Trim() + " in " + country + " | male to female " + mtypes.Trim() + " | cheap " + mtypes.Trim() + " " + country + " | " + gender + " to male massage";
                            }
                        }
                        else
                        {
                            if (looking != "")
                            {
                                if (ddllookingfor.SelectedValue == "M")
                                {
                                    description = "At MyMassagePartner.com, find female and male manual massage partner for " + mtypes + " in " + country + ". This page showing " + country + " based female and male who looking for manual or professional " + mtypes + " from his or her partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number of female and male who looking for " + mtypes + " from male or female in " + country + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available female and male massage partner for " + mtypes + " in " + country + ". Exchange " + mtypes + " with your female and male partner anytime, anywhere in " + country + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + country + " is a good place to exchange " + mtypes + " from female and male massage partner.";
                                    page_title.InnerHtml = "manual " + mtypes.Trim() + " partner in " + country;
                                    title_page.InnerHtml = "Find manual " + mtypes.Trim() + " partner in " + country + " | MyMassagePartner";
                                    Meta_ = "Here you can find manual massage partner for " + mtypes.Trim() + " in " + country + " with their contact number, you can chat in regards of free " + mtypes.Trim() + " so let’s start " + mtypes.Trim() + " sessions with your selected partner in home and hotel - MyMassagePartner";
                                    keyword_ = "massage partner " + mtypes.Trim() + " " + country + " | free " + mtypes.Trim() + " " + gender + " partner " + country + " | " + mtypes.Trim() + " " + gender + " in " + country + " | need " + gender + " for " + mtypes.Trim() + " | " + mtypes.Trim() + " " + gender + " partner in " + country + " | female to male " + mtypes.Trim() + " in " + country + " | " + mtypes.Trim() + " service by " + gender + " in " + country + " | " + mtypes.Trim() + " by " + gender + " at home in " + country + " | " + mtypes.Trim() + " by " + gender + " in hotel in " + country + " | want " + gender + " for " + mtypes.Trim() + " in " + country + " | massage in " + country + " by " + gender + " | " + mtypes.Trim() + " near me by " + gender + " | " + gender + " massager in " + country + " | free " + mtypes.Trim() + " in " + country + " | male to female " + mtypes.Trim() + " | cheap " + mtypes.Trim() + " " + country;
                                }
                                else
                                {
                                    description = "At MyMassagePartner.com, find female and male professional massage partner for " + mtypes + " in " + country + ". This page showing " + country + " based female and male massage who looking for manual or professional " + mtypes + " from his or her partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number of female and male who looking for " + mtypes + " from male or female in " + country + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available female and male massage partner for " + mtypes + " in " + country + ". Exchange " + mtypes + " with your female and male partner anytime, anywhere in " + country + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + country + " is a good place to exchange " + mtypes + " from female and male massage partner.";
                                    page_title.InnerHtml = "professional " + mtypes.Trim() + " partner in " + country;
                                    title_page.InnerHtml = "Find professional " + mtypes.Trim() + " partner in " + country + " | MyMassagePartner";
                                    Meta_ = "Here you can find professional massage partner for " + mtypes.Trim() + " in " + country + " with their contact number, you can chat in regards of free " + mtypes.Trim() + " so let’s start " + mtypes.Trim() + " sessions with your selected partner in home and hotel - MyMassagePartner";
                                    keyword_ = "massage partner " + mtypes.Trim() + " " + country + " | free " + mtypes.Trim() + " " + gender + " partner " + country + " | " + mtypes.Trim() + " " + gender + " in " + country + " | need " + gender + " for " + mtypes.Trim() + " | " + mtypes.Trim() + " " + gender + " partner in " + country + " | female to male " + mtypes.Trim() + " in " + country + " | " + mtypes.Trim() + " service by " + gender + " in " + country + " | " + mtypes.Trim() + " by " + gender + " at home in " + country + " | " + mtypes.Trim() + " by " + gender + " in hotel in " + country + " | want " + gender + " for " + mtypes.Trim() + " in " + country + " | massage in " + country + " by " + gender + " | " + mtypes.Trim() + " near me by " + gender + " | " + gender + " massager in " + country + " | free " + mtypes.Trim() + " in " + country + " | male to female " + mtypes.Trim() + " | cheap " + mtypes.Trim() + " " + country;
                                }
                            }
                            else
                            {
                                description = "At MyMassagePartner.com, find female and male massage partner in " + country + ". This page showing " + country + " based female and male who looking for manual or professional " + mtypes + " from his or her partner. Get registered with MyMassagePartner and ask for " + mtypes + " from your body massage partner. We have good number of female and male who looking for " + mtypes + " from male or female in " + country + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available female and male massage partner for " + mtypes + " in " + country + ". Exchange " + mtypes + " with your female and male partner anytime, anywhere in " + country + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + country + " is a good place to exchange " + mtypes + " from female and male massage partner.";
                                page_title.InnerHtml = mtypes.Trim() + " partner in " + country;
                                title_page.InnerHtml = "Find " + mtypes.Trim() + " partner in " + country + " | MyMassagePartner";
                                Meta_ = "Here you can find " + mtypes.Trim() + " female and male body massage partners in " + country + " with their contact number, you can chat in regards of free " + mtypes.Trim() + " so let’s start " + mtypes.Trim() + " sessions with your selected partner in home and hotel - MyMassagePartner, " + country;
                                keyword_ = mtypes.Trim() + " " + country + " | free " + mtypes.Trim() + " " + country + " | " + mtypes.Trim() + " in " + country + " | need " + mtypes.Trim() + " | " + mtypes.Trim() + " partner in " + country + " | female to male " + mtypes.Trim() + " in " + country + " | " + mtypes.Trim() + " service in " + country + " | " + mtypes.Trim() + " at home in " + country + " | " + mtypes.Trim() + " in hotel in " + country + " | want " + mtypes.Trim() + " in " + country + " | massage in " + country + " | " + mtypes.Trim() + " near me | female massager in " + country + " | free " + mtypes.Trim() + " in " + country + " | male to female " + mtypes.Trim() + " | cheap " + mtypes.Trim() + " " + country;
                            }
                        }
                    }
                    else
                    {
                        mtypes = "body massage";
                        if (gender != "")
                        {
                            if (looking != "")
                            {
                                if (ddllookingfor.SelectedValue == "M")
                                {
                                    if (gender == "Male")
                                    { description = "At MyMassagePartner.com, find " + gender.ToLower() + " manual massage partner in " + country + ". This page showing " + country + " based " + gender + " who looking for manual body massage from his partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number " + gender + " who looking for body massage from male or female in " + country + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for body massage in " + country + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + country + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + country + " is a good place to exchange body massage from " + gender + " massage partner."; }
                                    else
                                    { description = "At MyMassagePartner.com, find " + gender.ToLower() + " manual massage partner in " + country + ". This page showing " + country + " based " + gender + " who looking for manual body massage from her partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number " + gender + " who looking for body massage from male or female in " + country + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for body massage in " + country + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + country + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + country + " is a good place to exchange body massage from " + gender + " massage partner."; }
                                    page_title.InnerHtml = gender + " manual massage partner in " + country;
                                    title_page.InnerHtml = "Find " + gender + " manual massage partner in " + country + " | MyMassagePartner";
                                    Meta_ = "Here you can find manual " + gender + " massage partner in " + country + " with their contact number, you can chat in regards of free body massage so let’s start body massage sessions with your selected partner in home and hotel - MyMassagePartner";
                                    keyword_ = gender + " massage partner " + country + " | free massage by " + gender + " partner " + country + " | " + gender + " massagers in " + country + " | need " + gender + " for massage | " + gender + " partner in " + country + " | female to male in " + country + " | massage service by " + gender + " in " + country + " | massage by " + gender + " at home in " + country + " | massage by " + gender + " in hotel in " + country + " | want " + gender + " for massage in " + country + " | massage in " + country + " by " + gender + " | manual massager near me | " + gender + " massager in " + country + " | free massage in " + country + " | male to female Back massage | cheap Back massage " + country + " | " + gender + " to male massage";
                                }
                                else
                                {
                                    if (gender == "Male")
                                    { description = "At MyMassagePartner.com, find " + gender + " professional massage partner in " + country + ". This page showing " + country + " based " + gender + " who looking for professional body massage from his partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number " + gender + " who looking for body massage from male or female in " + country + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for body massage in " + country + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + country + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + country + " is a good place to exchange body massage from " + gender + " massage partner."; }
                                    else
                                    { description = "At MyMassagePartner.com, find " + gender + " professional massage partner in " + country + ". This page showing " + country + " based " + gender + " who looking for professional body massage from her partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number " + gender + " who looking for body massage from male or female in " + country + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for body massage in " + country + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + country + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + country + " is a good place to exchange body massage from " + gender + " massage partner."; }
                                    page_title.InnerHtml = gender + " professional massage partner in " + country;
                                    title_page.InnerHtml = "Find " + gender + " professional massage partner in " + country + " | MyMassagePartner";
                                    Meta_ = "Here you can find professional " + gender + " massage partner in " + country + " with their contact number, you can chat in regards of free body massage so let’s start body massage sessions with your selected partner in home and hotel - MyMassagePartner";
                                    keyword_ = gender + " massage partner " + country + " | free massage by " + gender + " partner " + country + " | " + gender + " massagers in " + country + " | need " + gender + " for massage | " + gender + " partner in " + country + " | female to male in " + country + " | massage service by " + gender + " in " + country + " | massage by " + gender + " at home in " + country + " | massage by " + gender + " in hotel in " + country + " | want " + gender + " for massage in " + country + " | massage in " + country + " by " + gender + " | professional massager near me | " + gender + " massager in " + country + " | free massage in " + country + " | male to female Back massage | cheap Back massage " + country + " | " + gender + " to male massage";
                                }
                            }
                            else
                            {
                                if (gender == "Male")
                                { description = "At MyMassagePartner.com, find " + gender + " massage partner in " + country + ". This page showing " + country + " based " + gender + " who looking for manual or professional body massage from his partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number " + gender + " who looking for body massage from male or female in " + country + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for body massage in " + country + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + country + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + country + " is a good place to exchange body massage from " + gender + " massage partner."; }
                                else
                                { description = "At MyMassagePartner.com, find " + gender + " massage partner in " + country + ". This page showing " + country + " based " + gender + " who looking for manual or professional body massage from her partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number " + gender + " who looking for body massage from male or female in " + country + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + gender + " massage partner for body massage in " + country + ". Exchange " + mtypes + " with your " + gender + " partner anytime, anywhere in " + country + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + country + " is a good place to exchange body massage from " + gender + " massage partner."; }
                                page_title.InnerHtml = gender + " massage partner in " + country;
                                title_page.InnerHtml = "Find " + gender + " massage partner in " + country + " | MyMassagePartner";
                                Meta_ = "Here you can find " + gender + " massage partner in " + country + " with their contact number, you can chat in regards of free body massage so let’s start body massage sessions with your selected partner in home and hotel - MyMassagePartner";
                                keyword_ = gender + " massage partner " + country + " | free massage by " + gender + " partner " + country + " | " + gender + " massagers in " + country + " | need " + gender + " for massage | " + gender + " partner in " + country + " | female to male in " + country + " | massage service by " + gender + " in " + country + " | massage by " + gender + " at home in " + country + " | massage by " + gender + " in hotel in " + country + " | want " + gender + " for massage in " + country + " | massage in " + country + " by " + gender + " | manual massager near me | " + gender + " massager in " + country + " | free massage in " + country + " | male to female Back massage | cheap Back massage " + country + " | " + gender + " to male massage";
                            }
                        }
                        else
                        {
                            if (looking != "")
                            {
                                if (ddllookingfor.SelectedValue == "M")
                                {
                                    description = "At MyMassagePartner.com, find male and female manual massage partner in " + country + ". This page showing " + country + " based female and male who looking for manual body massage from his or her partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number of female and male who looking for body massage from male or female in " + country + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available massage partner female and male for same massage in " + country + ". Exchange body massage with your female and male partner anytime, anywhere in " + country + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + country + " is a good place to exchange body massage from female and male massage partner.";
                                    page_title.InnerHtml = "Manual body massage partner in " + country;
                                    title_page.InnerHtml = "Find manual body massage partner in " + country + " | MyMassagePartner";
                                    Meta_ = "Here you can find manual female and male body massage partners in " + country + " with their contact number, you can chat in regards of free body massage so let’s start body massage sessions with your selected partner in home and hotel - MyMassagePartner";
                                    keyword_ = "body massage " + country + " | erotic massage " + country + " | happy ending massage " + country + " | need body massage | full body massage in " + country + " | female to male Massage in " + country + " | sensual Massage in " + country + " | body massage at home in " + country + " | body massage in hotel in " + country + " | want body massage in " + country + " | massage in " + country + " | massage near me | female massager in " + country + " | free body massage in " + country + " | male to female body massage | cheap massage " + country;

                                }
                                else
                                {
                                    description = "At MyMassagePartner.com, find male and female professional massage partner in " + country + ". This page showing " + country + " based female and male who looking for professional body massage from his or her partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number of female and male who looking for body massage from male or female in " + country + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available massage partner female and male for same massage in " + country + ". Exchange body massage with your female and male partner anytime, anywhere in " + country + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + country + " is a good place to exchange body massage from female and male massage partner.";
                                    page_title.InnerHtml = "Professional body massage partner in " + country;
                                    title_page.InnerHtml = "Find professional body massage partner in " + country + " | MyMassagePartner";
                                    Meta_ = "Here you can find professional female and male body massage partners in " + country + " with their contact number, you can chat in regards of free body massage so let’s start body massage sessions with your selected partner in home and hotel - MyMassagePartner";
                                    keyword_ = "body massage " + country + " | erotic massage " + country + " | happy ending massage " + country + " | need body massage | full body massage in " + country + " | female to male Massage in " + country + " | sensual Massage in " + country + " | body massage at home in " + country + " | body massage in hotel in " + country + " | want body massage in " + country + " | massage in " + country + " | massage near me | female massager in " + country + " | free body massage in " + country + " | male to female body massage | cheap massage " + country;
                                }
                            }
                            else
                            {
                                description = "At MyMassagePartner.com, find female and male massage partner in " + country + ". This page showing " + country + " based female and male who looking for manual or professional body massage from his or her partner. Get registered with MyMassagePartner and ask for body massage from your body massage partner. We have good number of female and male who looking for body massage from male or female in " + country + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available female and male massage partner for body massage in " + country + ". Exchange " + mtypes + " with your female and male partner anytime, anywhere in " + country + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + country + " is a good place to exchange body massage from female and male massage partner.";
                                page_title.InnerHtml = "body massage partner in " + country;
                                title_page.InnerHtml = "Find body massage partner in " + country + " | MyMassagePartner";
                                Meta_ = "Here you can find female and male body massage partners in " + country + " with their contact number, you can chat in regards of free body massage so let’s start body massage sessions with your selected partner in home and hotel - MyMassagePartner, " + country;
                                keyword_ = "body massage " + country + " | erotic massage " + country + " | happy ending massage " + country + " | need body massage | full body massage in " + country + " | female to male Massage in " + country + " | sensual Massage in " + country + " | body massage at home in " + country + " | body massage in hotel in " + country + " | want body massage in " + country + " | massage in " + country + " | massage near me | female massager in " + country + " | free body massage in " + country + " | male to female body massage | cheap massage " + country;
                            }
                        }
                    }
                }


                page_title.InnerHtml = page_title.InnerHtml.Replace("  ", " ").Replace(" body "," ");
                des.InnerHtml = description.Replace(" body ", " ");
                page_title.InnerHtml = UppercaseFirstEach(page_title.InnerHtml.ToString()).Replace(" body ", " ").Replace("Body ","");
                page_title.InnerHtml = page_title.InnerHtml.Replace("In", "in").Replace(" body ", " ");
                page_title.InnerHtml = page_title.InnerHtml.Replace("And", "and").Replace(" body ", " ");

                title_page.InnerHtml = title_page.InnerHtml.Replace("  ", " ").Replace(" body ", " ");
                if (Request.QueryString["page"] != null)
                {
                    string page = "Page " + Request.QueryString["page"].ToString() + " - ";
                    title_page.InnerHtml = title_page.InnerHtml.Replace("| MyMassagePartner", "| " + page + " MyMassagePartner").Replace(" body ", " ");
                }
                Meta_ = Meta_.Trim().Replace("  ", " ").Replace(" body ", " ");
                keyword_ = keyword_.Replace("  ", " ").Replace(" body ", " ");


                metaDescription.Attributes.Add("content", Meta_);
                keywordcontent.Attributes.Add("content", keyword_);


                //lbl_title.InnerHtml = title_;
                //lbl_title.Attributes.Add("title", title_);
                // keywordcontent.Controls.Add(keyword);
            }
            catch (Exception ex)
            {
                //ErrorHandling handling = new ErrorHandling();
                //handling.reqUrl = Request.Url.AbsoluteUri; Random rnd = new Random();
                //int card = rnd.Next(1000, 100000);
                //handling.filePath = Server.MapPath("~/ErrorLog") + "\\errorLOG" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-") + card + ".txt";
                //handling.methodname = "DynamicMeta";
                //handling.lastErrorTypeName = ex.GetType().ToString();
                //handling.lastErrorMessage = ex.Message;
                //handling.lastErrorStackTrace = ex.StackTrace;
                //handling.writefile();
                BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message;
                var st = new System.Diagnostics.StackTrace(ex, true);
                StackFrame[] stackFrames = st.GetFrames();
                foreach (StackFrame stackFrame in stackFrames)
                {
                    Console.WriteLine(stackFrame.GetMethod().Name);   // write method name
                    BussinessEntity.ExceptionHandling._lineno = stackFrame.GetFileLineNumber();
                    BussinessEntity.ExceptionHandling._methodname = Convert.ToString(stackFrame.GetMethod().Name);
                    BussinessEntity.ExceptionHandling._pagename = Convert.ToString(Request.Url.AbsoluteUri);

                }
                Response.Redirect(Constants__.WEB_ROOT + "/ErrorMessage.aspx", false);
                return;

            }
        }
        private void DynamicMeta_partner_types()
        {
            try
            {
                //-----meta tags ----------
                HtmlMeta Meta = new HtmlMeta();
                HtmlMeta keyword = new HtmlMeta();
                string country = "";
                string state = "";
                string city = "";
                string area = "";
                string outcall = "";
                string Meta_ = "";
                string keyword_ = "";
                string mtypes = "";
                string partner_type = "";
                string description = "";
                // string looking = "";


                if (ddlcountry.SelectedIndex > 0)
                {
                    country = ddlcountry.SelectedItem.Text;

                }
                if (ddlstate.SelectedIndex > 0)
                {
                    state = ddlstate.SelectedItem.Text;

                }
                if (ddlcity.SelectedIndex > 0)
                {
                    city = ddlcity.SelectedItem.Text;

                }
                if (ddlOutCall.SelectedIndex > 0)
                {
                    outcall = ddlOutCall.SelectedValue.ToString();
                }
                if (ddlarea.SelectedIndex > 0)
                {
                    area = ddlarea.SelectedItem.Text;
                }
                if (ddlmassagetypes.SelectedIndex > 0)
                {
                    mtypes = ddlmassagetypes.SelectedItem.Text;
                }
                if (ddlPartner_Types.SelectedIndex > 0)
                {
                    partner_type = ddlPartner_Types.SelectedItem.Text;
                    partner_type = partner_type.Replace("partner", "");
                }

                string title_ = "";
                //--title text--------   

                if (area != "" && city != "")
                {
                    // <a href='https://www.mymassagepartner.com" + "/massage-partner/" + country.Replace(' ', '-') + "/s/c/All-Area/Any/all-types/all" + "'>" + country + "</a> 
                    links_srp.InnerHtml = "<a href='https://www.mymassagepartner.com'>Home</a> > <a href='https://www.mymassagepartner.com" + "/body-massage-partner/" + country.Replace(' ', '-') + "/" + state.Replace(' ', '-') + "/a/All-Area/any/all-types" + "'>" + state + "</a> > <a href='https://www.mymassagepartner.com" + "/body-massage-partner/" + country.Replace(' ', '-') + "/" + state.Replace(' ', '-') + "/" + city.Replace(' ', '-') + "/All-Area/any/all-types" + "'>" + city + "</a> > " + area + "";
                    if (mtypes != "")
                    {

                        if (partner_type != "")
                        {
                            if (outcall != "")
                            {
                                string[] p = partner_type.Split(' ');
                                description = "At MyMassagePartner.com, find " + partner_type.ToLower() + " massage partners for " + mtypes.Trim() + " at " + outcall.ToLower() + " in " + area + ", " + city + ". This page showing " + area + ", " + city + " based " + p[0].ToLower() + " who like to exchange " + mtypes.Trim().ToLower() + " from male massage partner in " + outcall.ToLower() + ". Get registered with MyMassagePartner and ask for " + mtypes.Trim().ToLower() + " from your body massage partner. We have good number of " + p[0].ToLower() + " who wish to give " + mtypes.Trim().ToLower() + " to male in " + area + ", " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + partner_type.ToLower() + " massage partners for " + mtypes.Trim().ToLower() + " in " + area + ", " + city + ". Exchange " + mtypes.Trim().ToLower() + " with your " + p[0].ToLower() + " partner anytime, anywhere in " + area + ", " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + area + ", " + city + " is a good place to exchange " + mtypes.Trim().ToLower() + " under " + partner_type.ToLower() + " massage from your massage partners in " + outcall.ToLower() + ".";
                                page_title.InnerHtml = partner_type.ToLower() + " massage partner for " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + area + ", " + city;
                                title_page.InnerHtml = "Find " + partner_type.ToLower() + " massage partner for " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + area + ", " + city + " | MyMassagePartner";
                                Meta_ = "Here you can find " + partner_type.ToLower() + " body massage partners for " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + area + ", " + city + " with their contact numbers. Choose your " + p[0].ToLower() + " body massage partner and start free " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " - MyMassagePartner, " + area + ", " + city + "";
                                keyword_ = "at " + outcall.ToLower() + " " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + area + " | at " + outcall.ToLower() + " " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + area + " | at " + outcall.ToLower() + " " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + area + " | need " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " | " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + area + " | " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + area + " | at " + outcall.ToLower() + " " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + area + " | " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + area + " | want " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + area + " | " + p[0].ToLower() + " massage at " + outcall.ToLower() + " in " + area + " | " + mtypes.Trim().ToLower() + " near me | " + p[0].ToLower() + " massager at " + outcall.ToLower() + " service in " + area + " | free " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + area + " | cheap massage at " + outcall.ToLower() + " " + area + "";
                            }
                            else
                            {
                                string[] p = partner_type.Split(' ');
                                outcall = "hotel and home";
                                description = "At MyMassagePartner.com, find " + partner_type.ToLower() + " massage partners for " + mtypes.Trim() + " in " + area + ", " + city + ". This page showing " + area + ", " + city + " based " + p[0].ToLower() + " who like to exchange " + mtypes.Trim().ToLower() + " from male massage partner in " + outcall.ToLower() + ". Get registered with MyMassagePartner and ask for " + mtypes.Trim().ToLower() + " from your body massage partner. We have good number of " + p[0].ToLower() + " who wish to give " + mtypes.Trim().ToLower() + " to male in " + area + ", " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + partner_type.ToLower() + " massage partners for " + mtypes.Trim().ToLower() + " in " + area + ", " + city + ". Exchange " + mtypes.Trim().ToLower() + " with your " + p[0].ToLower() + " partner anytime, anywhere in " + area + ", " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + area + ", " + city + " is a good place to exchange " + mtypes.Trim().ToLower() + " under " + partner_type.ToLower() + " massage from your massage partners in " + outcall.ToLower() + ".";
                                page_title.InnerHtml = partner_type.ToLower() + " massage partner for " + mtypes.Trim().ToLower() + " in " + area + ", " + city;
                                title_page.InnerHtml = "Find " + partner_type.ToLower() + " massage partner for " + mtypes.Trim().ToLower() + " in " + area + ", " + city + " | MyMassagePartner";
                                Meta_ = "Here you can find " + partner_type.ToLower() + " body massage partners for " + mtypes.Trim().ToLower() + " in " + area + ", " + city + " with their contact numbers. Choose your " + p[0].ToLower() + " body massage partner and start free " + mtypes.Trim().ToLower() + " in home and hotel - MyMassagePartner, " + area + ", " + city + "";
                                keyword_ = "" + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + area + " | " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + area + " | " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + area + " | need " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " | " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + area + " | " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " in " + area + " | " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + area + " | " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + area + " | want " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + area + " | " + p[0].ToLower() + " massage in " + area + " | " + mtypes.Trim().ToLower() + " near me | " + p[0].ToLower() + " massager service in " + area + " | free " + mtypes.Trim().ToLower() + " in " + area + " | cheap massage " + area + "";
                            }
                        }
                        else
                        {
                            if (outcall != "")
                            {
                                partner_type = "female and male";
                                string[] p = partner_type.Split(' ');
                                description = "At MyMassagePartner.com, find female to male, female to female, male to male, and male to female massage partners for " + mtypes.Trim() + " at " + outcall.ToLower() + " in " + area + ", " + city + ". This page showing " + area + ", " + city + " based female and male who looking for female to male, female to female, male to male, and male to female " + mtypes.Trim().ToLower() + " from his or her massage partner in " + outcall.ToLower() + ". Get registered with MyMassagePartner and ask for " + mtypes.Trim().ToLower() + " from your body massage partner. We have good number of female and male who looking for " + mtypes.Trim().ToLower() + " with male or female in " + area + ", " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available female to male, female to female, male to male, and male to female massage partners for " + mtypes.Trim().ToLower() + " in " + area + ", " + city + ". Exchange " + mtypes.Trim().ToLower() + " with your female and male partner anytime, anywhere in " + area + ", " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + area + ", " + city + " is a good place to exchange " + mtypes.Trim().ToLower() + " under female to male, female to female, male to male, and male to female massage from your massage partners in " + outcall.ToLower() + ".";
                                page_title.InnerHtml = partner_type.ToLower() + " massage partner for " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + area + ", " + city;
                                title_page.InnerHtml = "Find " + partner_type.ToLower() + " massage partner for " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + area + ", " + city + " | MyMassagePartner";
                                Meta_ = "Here you can find female to male, female to female, male to male, and male to female body massage partners for " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + area + ", " + city + " with their contact numbers. Choose your " + p[0].ToLower() + " body massage partner and start free " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " - MyMassagePartner, " + area + ", " + city + "";
                                keyword_ = "at " + outcall.ToLower() + " " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + area + " | at " + outcall.ToLower() + " " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + area + " | at " + outcall.ToLower() + " " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + area + " | need " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " | " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + area + " | " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + area + " | at " + outcall.ToLower() + " " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + area + " | " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + area + " | want " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + area + " | " + p[0].ToLower() + " massage at " + outcall.ToLower() + " in " + area + " | " + mtypes.Trim().ToLower() + " near me | " + p[0].ToLower() + " massager at " + outcall.ToLower() + " service in " + area + " | free " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + area + " | cheap massage at " + outcall.ToLower() + " " + area + "";
                            }
                            else
                            {
                                partner_type = "female and male";
                                string[] p = partner_type.Split(' ');
                                outcall = "hotel and home";
                                description = "At MyMassagePartner.com, find female to male, female to female, male to male, and male to female massage partners for " + mtypes.Trim() + " in " + area + ", " + city + ". This page showing " + area + ", " + city + " based female and male who looking for female to male, female to female, male to male, and male to female " + mtypes.Trim().ToLower() + " from his or her massage partner in " + outcall.ToLower() + ". Get registered with MyMassagePartner and ask for " + mtypes.Trim().ToLower() + " from your body massage partner. We have good number of female and male who looking for " + mtypes.Trim().ToLower() + " with male or female in " + area + ", " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available female to male, female to female, male to male, and male to female massage partners for " + mtypes.Trim().ToLower() + " in " + area + ", " + city + ". Exchange " + mtypes.Trim().ToLower() + " with your female and male partner anytime, anywhere in " + area + ", " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + area + ", " + city + " is a good place to exchange " + mtypes.Trim().ToLower() + " under female to male, female to female, male to male, and male to female massage from your massage partners in " + outcall.ToLower() + ".";
                                page_title.InnerHtml = partner_type.ToLower() + " massage partner for " + mtypes.Trim().ToLower() + " in " + area + ", " + city;
                                title_page.InnerHtml = "Find " + partner_type.ToLower() + " massage partner for " + mtypes.Trim().ToLower() + " in " + area + ", " + city + " | MyMassagePartner";
                                Meta_ = "Here you can find female to male, female to female, male to male, and male to female body massage partners for " + mtypes.Trim().ToLower() + " in " + area + ", " + city + " with their contact numbers. Choose your " + p[0].ToLower() + " body massage partner and start free " + mtypes.Trim().ToLower() + " in home and hotel - MyMassagePartner, " + area + ", " + city + "";
                                keyword_ = "" + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + area + " | " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + area + " | " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + area + " | need " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " | " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + area + " | " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " in " + area + " | " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + area + " | " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + area + " | want " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + area + " | " + p[0].ToLower() + " massage in " + area + " | " + mtypes.Trim().ToLower() + " near me | " + p[0].ToLower() + " massager service in " + area + " | free " + mtypes.Trim().ToLower() + " in " + area + " | cheap massage " + area + "";
                            }

                        }

                    }
                    else
                    {
                        if (partner_type != "")
                        {
                            if (outcall != "")
                            {
                                string[] p = partner_type.Split(' ');
                                mtypes = "Body massage";
                                description = "At MyMassagePartner.com, find " + partner_type.ToLower() + " massage partners for " + mtypes.Trim() + " at " + outcall.ToLower() + " in " + area + ", " + city + ". This page showing " + area + ", " + city + " based " + p[0].ToLower() + " who like to exchange " + mtypes.Trim().ToLower() + " from male massage partner in " + outcall.ToLower() + ". Get registered with MyMassagePartner and ask for " + mtypes.Trim().ToLower() + " from your body massage partner. We have good number of " + p[0].ToLower() + " who wish to give " + mtypes.Trim().ToLower() + " to male in " + area + ", " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + partner_type.ToLower() + " massage partners for " + mtypes.Trim().ToLower() + " in " + area + ", " + city + ". Exchange " + mtypes.Trim().ToLower() + " with your " + p[0].ToLower() + " partner anytime, anywhere in " + area + ", " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + area + ", " + city + " is a good place to exchange " + mtypes.Trim().ToLower() + " under " + partner_type.ToLower() + " massage from your massage partners in " + outcall.ToLower() + ".";
                                page_title.InnerHtml = "Find " + partner_type.ToLower() + " massage partner at " + outcall.ToLower() + " in " + area + ", " + city;
                                title_page.InnerHtml = "Find " + partner_type.ToLower() + " body massage partner at " + outcall.ToLower() + " in " + area + ", " + city + " | MyMassagePartner";
                                Meta_ = "Here you can find " + partner_type.ToLower() + " body massage partners at " + outcall.ToLower() + " in " + area + ", " + city + " with their contact numbers. Choose your " + p[0].ToLower() + " body massage partner and start free body massage at " + outcall.ToLower() + " - MyMassagePartner, " + area + ", " + city + "";
                                keyword_ = "at " + outcall.ToLower() + " " + partner_type.ToLower() + " body massage " + area + " | at " + outcall.ToLower() + " " + partner_type.ToLower() + " erotic massage " + area + " | at " + outcall.ToLower() + " " + partner_type.ToLower() + " happy ending massage " + area + " | need " + partner_type.ToLower() + " body massage at " + outcall.ToLower() + " | full body massage at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + area + " | " + partner_type.ToLower() + " massage at " + outcall.ToLower() + " in " + area + " | at " + outcall.ToLower() + " sensual Massage by " + p[0].ToLower() + " in " + area + " | body massage at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + area + " | want body massage at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + area + " | " + p[0].ToLower() + " massage at " + outcall.ToLower() + " in " + area + " | massage near me | " + p[0].ToLower() + " massager at " + outcall.ToLower() + " service in " + area + " | free body massage at " + outcall.ToLower() + " in " + area + " | cheap massage at " + outcall.ToLower() + " " + area + "";
                            }
                            else
                            {
                                string[] p = partner_type.Split(' ');
                                outcall = "hotel and home";
                                mtypes = "Body massage";
                                description = "At MyMassagePartner.com, find " + partner_type.ToLower() + " massage partners for " + mtypes.Trim() + " in " + area + ", " + city + ". This page showing " + area + ", " + city + " based " + p[0].ToLower() + " who like to exchange " + mtypes.Trim().ToLower() + " from male massage partner in " + outcall.ToLower() + ". Get registered with MyMassagePartner and ask for " + mtypes.Trim().ToLower() + " from your body massage partner. We have good number of " + p[0].ToLower() + " who wish to give " + mtypes.Trim().ToLower() + " to male in " + area + ", " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + partner_type.ToLower() + " massage partners for " + mtypes.Trim().ToLower() + " in " + area + ", " + city + ". Exchange " + mtypes.Trim().ToLower() + " with your " + p[0].ToLower() + " partner anytime, anywhere in " + area + ", " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + area + ", " + city + " is a good place to exchange " + mtypes.Trim().ToLower() + " under " + partner_type.ToLower() + " massage from your massage partners in " + outcall.ToLower() + ".";
                                page_title.InnerHtml = "Find " + partner_type.ToLower() + " massage partner in " + area + ", " + city;
                                title_page.InnerHtml = "Find " + partner_type.ToLower() + " body massage partner in " + area + ", " + city + " | MyMassagePartner";
                                Meta_ = "Here you can find " + partner_type.ToLower() + " body massage partners in " + area + ", " + city + " with their contact numbers. Choose your " + p[0].ToLower() + " body massage partner and start free body massage in home and hotel - MyMassagePartner, " + area + ", " + city + "";
                                keyword_ = "" + partner_type.ToLower() + " body massage " + area + " | " + partner_type.ToLower() + " erotic massage " + area + " | " + partner_type.ToLower() + " happy ending massage " + area + " | need " + partner_type.ToLower() + " body massage | full body massage by " + p[0].ToLower() + " in " + area + " | " + partner_type.ToLower() + " massage in " + area + " | sensual Massage by " + p[0].ToLower() + " in " + area + " | body massage by " + p[0].ToLower() + " in " + area + " | want body massage by " + p[0].ToLower() + " in " + area + " | " + p[0].ToLower() + " massage in " + area + " | massage near me | " + p[0].ToLower() + " massager service in " + area + " | free body massage in " + area + " | cheap massage " + area + "";
                            }
                        }
                        else
                        {
                            if (outcall != "")
                            {
                                partner_type = "female and male";
                                string[] p = partner_type.Split(' ');
                                mtypes = "Body massage";
                                description = "At MyMassagePartner.com, find female to male, female to female, male to male, and male to female massage partners for " + mtypes.Trim() + " at " + outcall.ToLower() + " in " + area + ", " + city + ". This page showing " + area + ", " + city + " based female and male who looking for female to male, female to female, male to male, and male to female " + mtypes.Trim().ToLower() + " from his or her massage partner in " + outcall.ToLower() + ". Get registered with MyMassagePartner and ask for " + mtypes.Trim().ToLower() + " from your body massage partner. We have good number of female and male who looking for " + mtypes.Trim().ToLower() + " with male or female in " + area + ", " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available female to male, female to female, male to male, and male to female massage partners for " + mtypes.Trim().ToLower() + " in " + area + ", " + city + ". Exchange " + mtypes.Trim().ToLower() + " with your female and male partner anytime, anywhere in " + area + ", " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + area + ", " + city + " is a good place to exchange " + mtypes.Trim().ToLower() + " under female to male, female to female, male to male, and male to female massage from your massage partners in " + outcall.ToLower() + ".";
                                page_title.InnerHtml = "Find " + partner_type.ToLower() + " massage partner at " + outcall.ToLower() + " in " + area + ", " + city;
                                title_page.InnerHtml = "Find " + partner_type.ToLower() + " body massage partner at " + outcall.ToLower() + " in " + area + ", " + city + " | MyMassagePartner";
                                Meta_ = "Here you can find female to male, female to female, male to male, and male to female body massage partners at " + outcall.ToLower() + " in " + area + ", " + city + " with their contact numbers. Choose your " + p[0].ToLower() + " body massage partner and start free body massage at " + outcall.ToLower() + " - MyMassagePartner, " + area + ", " + city + "";
                                keyword_ = "at " + outcall.ToLower() + " " + partner_type.ToLower() + " body massage " + area + " | at " + outcall.ToLower() + " " + partner_type.ToLower() + " erotic massage " + area + " | at " + outcall.ToLower() + " " + partner_type.ToLower() + " happy ending massage " + area + " | need " + partner_type.ToLower() + " body massage at " + outcall.ToLower() + " | full body massage at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + area + " | " + partner_type.ToLower() + " massage at " + outcall.ToLower() + " in " + area + " | at " + outcall.ToLower() + " sensual Massage by " + p[0].ToLower() + " in " + area + " | body massage at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + area + " | want body massage at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + area + " | " + p[0].ToLower() + " massage at " + outcall.ToLower() + " in " + area + " | massage near me | " + p[0].ToLower() + " massager at " + outcall.ToLower() + " service in " + area + " | free body massage at " + outcall.ToLower() + " in " + area + " | cheap massage at " + outcall.ToLower() + " " + area + "";
                            }
                            else
                            {
                                partner_type = "female and male";
                                string[] p = partner_type.Split(' ');
                                mtypes = "Body massage";
                                outcall = "hotel and home";
                                description = "At MyMassagePartner.com, find female to male, female to female, male to male, and male to female massage partners for " + mtypes.Trim() + " in " + area + ", " + city + ". This page showing " + area + ", " + city + " based female and male who looking for female to male, female to female, male to male, and male to female " + mtypes.Trim().ToLower() + " from his or her massage partner in " + outcall.ToLower() + ". Get registered with MyMassagePartner and ask for " + mtypes.Trim().ToLower() + " from your body massage partner. We have good number of female and male who looking for " + mtypes.Trim().ToLower() + " with male or female in " + area + ", " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available female to male, female to female, male to male, and male to female massage partners for " + mtypes.Trim().ToLower() + " in " + area + ", " + city + ". Exchange " + mtypes.Trim().ToLower() + " with your female and male partner anytime, anywhere in " + area + ", " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + area + ", " + city + " is a good place to exchange " + mtypes.Trim().ToLower() + " under female to male, female to female, male to male, and male to female massage from your massage partners in " + outcall.ToLower() + ".";
                                page_title.InnerHtml = "Find " + partner_type.ToLower() + " massage partner in " + area + ", " + city;
                                title_page.InnerHtml = "Find " + partner_type.ToLower() + " body massage partner in " + area + ", " + city + " | MyMassagePartner";
                                Meta_ = "Here you can find female to male, female to female, male to male, and male to female body massage partners in " + area + ", " + city + " with their contact numbers. Choose your " + p[0].ToLower() + " body massage partner and start free body massage in home and hotel - MyMassagePartner, " + area + ", " + city + "";
                                keyword_ = "" + partner_type.ToLower() + " body massage " + area + " | " + partner_type.ToLower() + " erotic massage " + area + " | " + partner_type.ToLower() + " happy ending massage " + area + " | need " + partner_type.ToLower() + " body massage | full body massage by " + p[0].ToLower() + " in " + area + " | " + partner_type.ToLower() + " massage in " + area + " | sensual Massage by " + p[0].ToLower() + " in " + area + " | body massage by " + p[0].ToLower() + " in " + area + " | want body massage by " + p[0].ToLower() + " in " + area + " | " + p[0].ToLower() + " massage in " + area + " | massage near me | " + p[0].ToLower() + " massager service in " + area + " | free body massage in " + area + " | cheap massage " + area + "";
                            }

                        }
                    }

                }
                else if (city != "")
                {
                    links_srp.InnerHtml = "<a href='https://www.mymassagepartner.com'>Home</a> > <a href='https://www.mymassagepartner.com" + "/body-massage-partner/" + country.Replace(' ', '-') + "/" + state.Replace(' ', '-') + "/a/All-Area/any/all-types" + "'>" + state + "</a> > " + city + "";
                    if (mtypes != "")
                    {
                        if (partner_type != "")
                        {
                            if (outcall != "")
                            {
                                string[] p = partner_type.Split(' ');
                                description = "At MyMassagePartner.com, find " + partner_type.ToLower() + " massage partners for " + mtypes.Trim() + " at " + outcall.ToLower() + " in " + city + ". This page showing " + city + " based " + p[0].ToLower() + " who like to exchange " + mtypes.Trim().ToLower() + " from male massage partner in " + outcall.ToLower() + ". Get registered with MyMassagePartner and ask for " + mtypes.Trim().ToLower() + " from your body massage partner. We have good number of " + p[0].ToLower() + " who wish to give " + mtypes.Trim().ToLower() + " to male in " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + partner_type.ToLower() + " massage partners for " + mtypes.Trim().ToLower() + " in " + city + ". Exchange " + mtypes.Trim().ToLower() + " with your " + p[0].ToLower() + " partner anytime, anywhere in " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + city + " is a good place to exchange " + mtypes.Trim().ToLower() + " under " + partner_type.ToLower() + " massage from your massage partners in " + outcall.ToLower() + ".";
                                page_title.InnerHtml = partner_type.ToLower() + " massage partner for " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + city;
                                title_page.InnerHtml = "Find " + partner_type.ToLower() + " massage partner for " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + city + " | MyMassagePartner";
                                Meta_ = "Here you can find " + partner_type.ToLower() + " body massage partners for " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + city + " with their contact numbers. Choose your " + p[0].ToLower() + " body massage partner and start free " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " - MyMassagePartner, " + city + "";
                                keyword_ = "at " + outcall.ToLower() + " " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + city + " | at " + outcall.ToLower() + " " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + city + " | at " + outcall.ToLower() + " " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + city + " | need " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " | " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + city + " | " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + city + " | at " + outcall.ToLower() + " " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + city + " | " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + city + " | want " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + city + " | " + p[0].ToLower() + " massage at " + outcall.ToLower() + " in " + city + " | " + mtypes.Trim().ToLower() + " near me | " + p[0].ToLower() + " massager at " + outcall.ToLower() + " service in " + city + " | free " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + city + " | cheap massage at " + outcall.ToLower() + " " + city + "";
                            }
                            else
                            {
                                string[] p = partner_type.Split(' ');
                                outcall = "hotel and home";
                                description = "At MyMassagePartner.com, find " + partner_type.ToLower() + " massage partners for " + mtypes.Trim() + " in " + city + ". This page showing " + city + " based " + p[0].ToLower() + " who like to exchange " + mtypes.Trim().ToLower() + " from male massage partner in " + outcall.ToLower() + ". Get registered with MyMassagePartner and ask for " + mtypes.Trim().ToLower() + " from your body massage partner. We have good number of " + p[0].ToLower() + " who wish to give " + mtypes.Trim().ToLower() + " to male in " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + partner_type.ToLower() + " massage partners for " + mtypes.Trim().ToLower() + " in " + city + ". Exchange " + mtypes.Trim().ToLower() + " with your " + p[0].ToLower() + " partner anytime, anywhere in " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + city + " is a good place to exchange " + mtypes.Trim().ToLower() + " under " + partner_type.ToLower() + " massage from your massage partners in " + outcall.ToLower() + ".";
                                page_title.InnerHtml = partner_type.ToLower() + " massage partner for " + mtypes.Trim().ToLower() + " in " + city;
                                title_page.InnerHtml = "Find " + partner_type.ToLower() + " massage partner for " + mtypes.Trim().ToLower() + " in " + city + " | MyMassagePartner";
                                Meta_ = "Here you can find " + partner_type.ToLower() + " body massage partners for " + mtypes.Trim().ToLower() + " in " + city + " with their contact numbers. Choose your " + p[0].ToLower() + " body massage partner and start free " + mtypes.Trim().ToLower() + " in home and hotel - MyMassagePartner, " + city + "";
                                keyword_ = "" + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + city + " | " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + city + " | " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + city + " | need " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " | " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + city + " | " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " in " + city + " | " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + city + " | " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + city + " | want " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + city + " | " + p[0].ToLower() + " massage in " + city + " | " + mtypes.Trim().ToLower() + " near me | " + p[0].ToLower() + " massager service in " + city + " | free " + mtypes.Trim().ToLower() + " in " + city + " | cheap massage " + city + "";
                            }
                        }
                        else
                        {
                            if (outcall != "")
                            {
                                partner_type = "female and male";
                                string[] p = partner_type.Split(' ');
                                description = "At MyMassagePartner.com, find female to male, female to female, male to male, and male to female massage partners for " + mtypes.Trim() + " at " + outcall.ToLower() + " in " + city + ". This page showing " + city + " based female and male who looking for female to male, female to female, male to male, and male to female " + mtypes.Trim().ToLower() + " from his or her massage partner in " + outcall.ToLower() + ". Get registered with MyMassagePartner and ask for " + mtypes.Trim().ToLower() + " from your body massage partner. We have good number of female and male who looking for " + mtypes.Trim().ToLower() + " with male or female in " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available female to male, female to female, male to male, and male to female massage partners for " + mtypes.Trim().ToLower() + " in " + city + ". Exchange " + mtypes.Trim().ToLower() + " with your female and male partner anytime, anywhere in " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + city + " is a good place to exchange " + mtypes.Trim().ToLower() + " under female to male, female to female, male to male, and male to female massage from your massage partners in " + outcall.ToLower() + ".";
                                page_title.InnerHtml = partner_type.ToLower() + " massage partner for " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + city;
                                title_page.InnerHtml = "Find " + partner_type.ToLower() + " massage partner for " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + city + " | MyMassagePartner";
                                Meta_ = "Here you can find " + partner_type.ToLower() + " body massage partners for " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + city + " with their contact numbers. Choose your " + p[0].ToLower() + " body massage partner and start free " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " - MyMassagePartner, " + city + "";
                                keyword_ = "at " + outcall.ToLower() + " " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + city + " | at " + outcall.ToLower() + " " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + city + " | at " + outcall.ToLower() + " " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + city + " | need " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " | " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + city + " | " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + city + " | at " + outcall.ToLower() + " " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + city + " | " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + city + " | want " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + city + " | " + p[0].ToLower() + " massage at " + outcall.ToLower() + " in " + city + " | " + mtypes.Trim().ToLower() + " near me | " + p[0].ToLower() + " massager at " + outcall.ToLower() + " service in " + city + " | free " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + city + " | cheap massage at " + outcall.ToLower() + " " + city + "";
                            }
                            else
                            {
                                partner_type = "female and male";
                                string[] p = partner_type.Split(' ');
                                outcall = "hotel and home";
                                description = "At MyMassagePartner.com, find female to male, female to female, male to male, and male to female massage partners for " + mtypes.Trim() + " in " + city + ". This page showing " + city + " based female and male who looking for female to male, female to female, male to male, and male to female " + mtypes.Trim().ToLower() + " from his or her massage partner in " + outcall.ToLower() + ". Get registered with MyMassagePartner and ask for " + mtypes.Trim().ToLower() + " from your body massage partner. We have good number of female and male who looking for " + mtypes.Trim().ToLower() + " with male or female in " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available female to male, female to female, male to male, and male to female massage partners for " + mtypes.Trim().ToLower() + " in " + city + ". Exchange " + mtypes.Trim().ToLower() + " with your female and male partner anytime, anywhere in " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + city + " is a good place to exchange " + mtypes.Trim().ToLower() + " under female to male, female to female, male to male, and male to female massage from your massage partners in " + outcall.ToLower() + ".";
                                page_title.InnerHtml = partner_type.ToLower() + " massage partner for " + mtypes.Trim().ToLower() + " in " + city;
                                title_page.InnerHtml = "Find " + partner_type.ToLower() + " massage partner for " + mtypes.Trim().ToLower() + " in " + city + " | MyMassagePartner";
                                Meta_ = "Here you can find " + partner_type.ToLower() + " body massage partners for " + mtypes.Trim().ToLower() + " in " + city + " with their contact numbers. Choose your " + p[0].ToLower() + " body massage partner and start free " + mtypes.Trim().ToLower() + " in home and hotel - MyMassagePartner, " + city + "";
                                keyword_ = "" + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + city + " | " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + city + " | " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + city + " | need " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " | " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + city + " | " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " in " + city + " | " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + city + " | " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + city + " | want " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + city + " | " + p[0].ToLower() + " massage in " + city + " | " + mtypes.Trim().ToLower() + " near me | " + p[0].ToLower() + " massager service in " + city + " | free " + mtypes.Trim().ToLower() + " in " + city + " | cheap massage " + city + "";
                            }
                        }

                    }
                    else
                    {
                        if (partner_type != "")
                        {
                            if (outcall != "")
                            {
                                string[] p = partner_type.Split(' ');
                                mtypes = "Body massage";
                                description = "At MyMassagePartner.com, find " + partner_type.ToLower() + " massage partners for " + mtypes.Trim() + " at " + outcall.ToLower() + " in " + city + ". This page showing " + city + " based " + p[0].ToLower() + " who like to exchange " + mtypes.Trim().ToLower() + " from male massage partner in " + outcall.ToLower() + ". Get registered with MyMassagePartner and ask for " + mtypes.Trim().ToLower() + " from your body massage partner. We have good number of " + p[0].ToLower() + " who wish to give " + mtypes.Trim().ToLower() + " to male in " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + partner_type.ToLower() + " massage partners for " + mtypes.Trim().ToLower() + " in " + city + ". Exchange " + mtypes.Trim().ToLower() + " with your " + p[0].ToLower() + " partner anytime, anywhere in " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + city + " is a good place to exchange " + mtypes.Trim().ToLower() + " under " + partner_type.ToLower() + " massage from your massage partners in " + outcall.ToLower() + ".";
                                page_title.InnerHtml = "Find " + partner_type.ToLower() + " massage partner at " + outcall.ToLower() + " in " + city;
                                title_page.InnerHtml = "Find " + partner_type.ToLower() + " body massage partner at " + outcall.ToLower() + " in " + city + " | MyMassagePartner";
                                Meta_ = "Here you can find " + partner_type.ToLower() + " body massage partners at " + outcall.ToLower() + " in " + city + " with their contact numbers. Choose your " + p[0].ToLower() + " body massage partner and start free body massage at " + outcall.ToLower() + " - MyMassagePartner, " + city + "";
                                keyword_ = "at " + outcall.ToLower() + " " + partner_type.ToLower() + " body massage " + city + " | at " + outcall.ToLower() + " " + partner_type.ToLower() + " erotic massage " + city + " | at " + outcall.ToLower() + " " + partner_type.ToLower() + " happy ending massage " + city + " | need " + partner_type.ToLower() + " body massage at " + outcall.ToLower() + " | full body massage at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + city + " | " + partner_type.ToLower() + " massage at " + outcall.ToLower() + " in " + city + " | at " + outcall.ToLower() + " sensual Massage by " + p[0].ToLower() + " in " + city + " | body massage at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + city + " | want body massage at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + city + " | " + p[0].ToLower() + " massage at " + outcall.ToLower() + " in " + city + " | massage near me | " + p[0].ToLower() + " massager at " + outcall.ToLower() + " service in " + city + " | free body massage at " + outcall.ToLower() + " in " + city + " | cheap massage at " + outcall.ToLower() + " " + city + "";
                            }
                            else
                            {
                                string[] p = partner_type.Split(' ');
                                outcall = "hotel and home";
                                mtypes = "Body massage";
                                description = "At MyMassagePartner.com, find " + partner_type.ToLower() + " massage partners for " + mtypes.Trim() + " in " + city + ". This page showing " + city + " based " + p[0].ToLower() + " who like to exchange " + mtypes.Trim().ToLower() + " from male massage partner in " + outcall.ToLower() + ". Get registered with MyMassagePartner and ask for " + mtypes.Trim().ToLower() + " from your body massage partner. We have good number of " + p[0].ToLower() + " who wish to give " + mtypes.Trim().ToLower() + " to male in " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + partner_type.ToLower() + " massage partners for " + mtypes.Trim().ToLower() + " in " + city + ". Exchange " + mtypes.Trim().ToLower() + " with your " + p[0].ToLower() + " partner anytime, anywhere in " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + city + " is a good place to exchange " + mtypes.Trim().ToLower() + " under " + partner_type.ToLower() + " massage from your massage partners in " + outcall.ToLower() + ".";
                                page_title.InnerHtml = "Find " + partner_type.ToLower() + " massage partner in " + city;
                                title_page.InnerHtml = "Find " + partner_type.ToLower() + " body massage partner in " + city + " | MyMassagePartner";
                                Meta_ = "Here you can find " + partner_type.ToLower() + " body massage partners in " + city + " with their contact numbers. Choose your " + p[0].ToLower() + " body massage partner and start free body massage in home and hotel - MyMassagePartner, " + city + "";
                                keyword_ = "" + partner_type.ToLower() + " body massage " + city + " | " + partner_type.ToLower() + " erotic massage " + city + " | " + partner_type.ToLower() + " happy ending massage " + city + " | need " + partner_type.ToLower() + " body massage | full body massage by " + p[0].ToLower() + " in " + city + " | " + partner_type.ToLower() + " massage in " + city + " | sensual Massage by " + p[0].ToLower() + " in " + city + " | body massage by " + p[0].ToLower() + " in " + city + " | want body massage by " + p[0].ToLower() + " in " + city + " | " + p[0].ToLower() + " massage in " + city + " | massage near me | " + p[0].ToLower() + " massager service in " + city + " | free body massage in " + city + " | cheap massage " + city + "";
                            }
                        }
                        else
                        {
                            if (outcall != "")
                            {
                                partner_type = "female and male";
                                string[] p = partner_type.Split(' ');
                                mtypes = "Body massage";
                                description = "At MyMassagePartner.com, find female to male, female to female, male to male, and male to female massage partners for " + mtypes.Trim() + " at " + outcall.ToLower() + " in " + city + ". This page showing " + city + " based female and male who looking for female to male, female to female, male to male, and male to female " + mtypes.Trim().ToLower() + " from his or her massage partner in " + outcall.ToLower() + ". Get registered with MyMassagePartner and ask for " + mtypes.Trim().ToLower() + " from your body massage partner. We have good number of female and male who looking for " + mtypes.Trim().ToLower() + " with male or female in " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available female to male, female to female, male to male, and male to female massage partners for " + mtypes.Trim().ToLower() + " in " + city + ". Exchange " + mtypes.Trim().ToLower() + " with your female and male partner anytime, anywhere in " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + city + " is a good place to exchange " + mtypes.Trim().ToLower() + " under female to male, female to female, male to male, and male to female massage from your massage partners in " + outcall.ToLower() + ".";
                                page_title.InnerHtml = "Find " + partner_type.ToLower() + " massage partner at " + outcall.ToLower() + " in " + city;
                                title_page.InnerHtml = "Find " + partner_type.ToLower() + " body massage partner at " + outcall.ToLower() + " in " + city + " | MyMassagePartner";
                                Meta_ = "Here you can find female to male, female to female, male to male, and male to female body massage partners at " + outcall.ToLower() + " in " + city + " with their contact numbers. Choose your " + p[0].ToLower() + " body massage partner and start free body massage at " + outcall.ToLower() + " - MyMassagePartner, " + city + "";
                                keyword_ = "at " + outcall.ToLower() + " " + partner_type.ToLower() + " body massage " + city + " | at " + outcall.ToLower() + " " + partner_type.ToLower() + " erotic massage " + city + " | at " + outcall.ToLower() + " " + partner_type.ToLower() + " happy ending massage " + city + " | need " + partner_type.ToLower() + " body massage at " + outcall.ToLower() + " | full body massage at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + city + " | " + partner_type.ToLower() + " massage at " + outcall.ToLower() + " in " + city + " | at " + outcall.ToLower() + " sensual Massage by " + p[0].ToLower() + " in " + city + " | body massage at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + city + " | want body massage at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + city + " | " + p[0].ToLower() + " massage at " + outcall.ToLower() + " in " + city + " | massage near me | " + p[0].ToLower() + " massager at " + outcall.ToLower() + " service in " + city + " | free body massage at " + outcall.ToLower() + " in " + city + " | cheap massage at " + outcall.ToLower() + " " + city + "";
                            }
                            else
                            {
                                partner_type = "female and male";
                                string[] p = partner_type.Split(' ');
                                mtypes = "Body massage";
                                outcall = "hotel and home";
                                description = "At MyMassagePartner.com, find female to male, female to female, male to male, and male to female massage partners for " + mtypes.Trim() + " in " + city + ". This page showing " + city + " based female and male who looking for female to male, female to female, male to male, and male to female " + mtypes.Trim().ToLower() + " from his or her massage partner in " + outcall.ToLower() + ". Get registered with MyMassagePartner and ask for " + mtypes.Trim().ToLower() + " from your body massage partner. We have good number of female and male who looking for " + mtypes.Trim().ToLower() + " with male or female in " + city + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available female to male, female to female, male to male, and male to female massage partners for " + mtypes.Trim().ToLower() + " in " + city + ". Exchange " + mtypes.Trim().ToLower() + " with your female and male partner anytime, anywhere in " + city + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + city + " is a good place to exchange " + mtypes.Trim().ToLower() + " under female to male, female to female, male to male, and male to female massage from your massage partners in " + outcall.ToLower() + ".";
                                page_title.InnerHtml = "Find " + partner_type.ToLower() + " massage partner in " + city;
                                title_page.InnerHtml = "Find " + partner_type.ToLower() + " body massage partner in " + city + " | MyMassagePartner";
                                Meta_ = "Here you can find female to male, female to female, male to male, and male to female body massage partners in " + city + " with their contact numbers. Choose your " + p[0].ToLower() + " body massage partner and start free body massage in home and hotel - MyMassagePartner, " + city + "";
                                keyword_ = "" + partner_type.ToLower() + " body massage " + city + " | " + partner_type.ToLower() + " erotic massage " + city + " | " + partner_type.ToLower() + " happy ending massage " + city + " | need " + partner_type.ToLower() + " body massage | full body massage by " + p[0].ToLower() + " in " + city + " | " + partner_type.ToLower() + " massage in " + city + " | sensual Massage by " + p[0].ToLower() + " in " + city + " | body massage by " + p[0].ToLower() + " in " + city + " | want body massage by " + p[0].ToLower() + " in " + city + " | " + p[0].ToLower() + " massage in " + city + " | massage near me | " + p[0].ToLower() + " massager service in " + city + " | free body massage in " + city + " | cheap massage " + city + "";
                            }

                        }
                    }
                }
                else if (state != "")
                {
                    links_srp.InnerHtml = "<a href='https://www.mymassagepartner.com'>Home</a> > " + state + "";
                    if (mtypes != "")
                    {
                        if (partner_type != "")
                        {
                            if (outcall != "")
                            {
                                string[] p = partner_type.Split(' ');
                                description = "At MyMassagePartner.com, find " + partner_type.ToLower() + " massage partners for " + mtypes.Trim() + " at " + outcall.ToLower() + " in " + state + ". This page showing " + state + " based " + p[0].ToLower() + " who like to exchange " + mtypes.Trim().ToLower() + " from male massage partner in " + outcall.ToLower() + ". Get registered with MyMassagePartner and ask for " + mtypes.Trim().ToLower() + " from your body massage partner. We have good number of " + p[0].ToLower() + " who wish to give " + mtypes.Trim().ToLower() + " to male in " + state + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + partner_type.ToLower() + " massage partners for " + mtypes.Trim().ToLower() + " in " + state + ". Exchange " + mtypes.Trim().ToLower() + " with your " + p[0].ToLower() + " partner anytime, anywhere in " + state + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + state + " is a good place to exchange " + mtypes.Trim().ToLower() + " under " + partner_type.ToLower() + " massage from your massage partners in " + outcall.ToLower() + ".";
                                page_title.InnerHtml = partner_type.ToLower() + " massage partner for " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + state;
                                title_page.InnerHtml = "Find " + partner_type.ToLower() + " massage partner for " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + state + " | MyMassagePartner";
                                Meta_ = "Here you can find " + partner_type.ToLower() + " body massage partners for " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + state + " with their contact numbers. Choose your " + p[0].ToLower() + " body massage partner and start free " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " - MyMassagePartner, " + state + "";
                                keyword_ = "at " + outcall.ToLower() + " " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + state + " | at " + outcall.ToLower() + " " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + state + " | at " + outcall.ToLower() + " " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + state + " | need " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " | " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + state + " | " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + state + " | at " + outcall.ToLower() + " " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + state + " | " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + state + " | want " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + state + " | " + p[0].ToLower() + " massage at " + outcall.ToLower() + " in " + state + " | " + mtypes.Trim().ToLower() + " near me | " + p[0].ToLower() + " massager at " + outcall.ToLower() + " service in " + state + " | free " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + state + " | cheap massage at " + outcall.ToLower() + " " + state + "";
                            }
                            else
                            {
                                string[] p = partner_type.Split(' ');
                                outcall = "hotel and home";
                                description = "At MyMassagePartner.com, find " + partner_type.ToLower() + " massage partners for " + mtypes.Trim() + " in " + state + ". This page showing " + state + " based " + p[0].ToLower() + " who like to exchange " + mtypes.Trim().ToLower() + " from male massage partner in " + outcall.ToLower() + ". Get registered with MyMassagePartner and ask for " + mtypes.Trim().ToLower() + " from your body massage partner. We have good number of " + p[0].ToLower() + " who wish to give " + mtypes.Trim().ToLower() + " to male in " + state + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + partner_type.ToLower() + " massage partners for " + mtypes.Trim().ToLower() + " in " + state + ". Exchange " + mtypes.Trim().ToLower() + " with your " + p[0].ToLower() + " partner anytime, anywhere in " + state + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + state + " is a good place to exchange " + mtypes.Trim().ToLower() + " under " + partner_type.ToLower() + " massage from your massage partners in " + outcall.ToLower() + ".";
                                page_title.InnerHtml = partner_type.ToLower() + " massage partner for " + mtypes.Trim().ToLower() + " in " + state;
                                title_page.InnerHtml = "Find " + partner_type.ToLower() + " massage partner for " + mtypes.Trim().ToLower() + " in " + state + " | MyMassagePartner";
                                Meta_ = "Here you can find " + partner_type.ToLower() + " body massage partners for " + mtypes.Trim().ToLower() + " in " + state + " with their contact numbers. Choose your " + p[0].ToLower() + " body massage partner and start free " + mtypes.Trim().ToLower() + " in home and hotel - MyMassagePartner, " + state + "";
                                keyword_ = "" + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + state + " | " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + state + " | " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + state + " | need " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " | " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + state + " | " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " in " + state + " | " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + state + " | " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + state + " | want " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + state + " | " + p[0].ToLower() + " massage in " + state + " | " + mtypes.Trim().ToLower() + " near me | " + p[0].ToLower() + " massager service in " + state + " | free " + mtypes.Trim().ToLower() + " in " + state + " | cheap massage " + state + "";
                            }
                        }
                        else
                        {
                            if (outcall != "")
                            {
                                partner_type = "female and male";
                                string[] p = partner_type.Split(' ');
                                description = "At MyMassagePartner.com, find female to male, female to female, male to male, and male to female massage partners for " + mtypes.Trim() + " at " + outcall.ToLower() + " in " + state + ". This page showing " + state + " based female and male who looking for female to male, female to female, male to male, and male to female " + mtypes.Trim().ToLower() + " from his or her massage partner in " + outcall.ToLower() + ". Get registered with MyMassagePartner and ask for " + mtypes.Trim().ToLower() + " from your body massage partner. We have good number of female and male who looking for " + mtypes.Trim().ToLower() + " with male or female in " + state + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available female to male, female to female, male to male, and male to female massage partners for " + mtypes.Trim().ToLower() + " in " + state + ". Exchange " + mtypes.Trim().ToLower() + " with your female and male partner anytime, anywhere in " + state + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + state + " is a good place to exchange " + mtypes.Trim().ToLower() + " under female to male, female to female, male to male, and male to female massage from your massage partners in " + outcall.ToLower() + ".";
                                page_title.InnerHtml = partner_type.ToLower() + " massage partner for " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + state;
                                title_page.InnerHtml = "Find " + partner_type.ToLower() + " massage partner for " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + state + " | MyMassagePartner";
                                Meta_ = "Here you can find " + partner_type.ToLower() + " body massage partners for " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + state + " with their contact numbers. Choose your " + p[0].ToLower() + " body massage partner and start free " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " - MyMassagePartner, " + state + "";
                                keyword_ = "at " + outcall.ToLower() + " " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + state + " | at " + outcall.ToLower() + " " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + state + " | at " + outcall.ToLower() + " " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + state + " | need " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " | " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + state + " | " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + state + " | at " + outcall.ToLower() + " " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + state + " | " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + state + " | want " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + state + " | " + p[0].ToLower() + " massage at " + outcall.ToLower() + " in " + state + " | " + mtypes.Trim().ToLower() + " near me | " + p[0].ToLower() + " massager at " + outcall.ToLower() + " service in " + state + " | free " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + state + " | cheap massage at " + outcall.ToLower() + " " + state + "";
                            }
                            else
                            {
                                partner_type = "female and male";
                                string[] p = partner_type.Split(' ');
                                outcall = "hotel and home";
                                description = "At MyMassagePartner.com, find female to male, female to female, male to male, and male to female massage partners for " + mtypes.Trim() + " in " + state + ". This page showing " + state + " based female and male who looking for female to male, female to female, male to male, and male to female " + mtypes.Trim().ToLower() + " from his or her massage partner in " + outcall.ToLower() + ". Get registered with MyMassagePartner and ask for " + mtypes.Trim().ToLower() + " from your body massage partner. We have good number of female and male who looking for " + mtypes.Trim().ToLower() + " with male or female in " + state + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available female to male, female to female, male to male, and male to female massage partners for " + mtypes.Trim().ToLower() + " in " + state + ". Exchange " + mtypes.Trim().ToLower() + " with your female and male partner anytime, anywhere in " + state + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + state + " is a good place to exchange " + mtypes.Trim().ToLower() + " under female to male, female to female, male to male, and male to female massage from your massage partners in " + outcall.ToLower() + ".";
                                page_title.InnerHtml = partner_type.ToLower() + " massage partner for " + mtypes.Trim().ToLower() + " in " + state;
                                title_page.InnerHtml = "Find " + partner_type.ToLower() + " massage partner for " + mtypes.Trim().ToLower() + " in " + state + " | MyMassagePartner";
                                Meta_ = "Here you can find " + partner_type.ToLower() + " body massage partners for " + mtypes.Trim().ToLower() + " in " + state + " with their contact numbers. Choose your " + p[0].ToLower() + " body massage partner and start free " + mtypes.Trim().ToLower() + " in home and hotel - MyMassagePartner, " + state + "";
                                keyword_ = "" + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + state + " | " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + state + " | " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + state + " | need " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " | " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + state + " | " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " in " + state + " | " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + state + " | " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + state + " | want " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + state + " | " + p[0].ToLower() + " massage in " + state + " | " + mtypes.Trim().ToLower() + " near me | " + p[0].ToLower() + " massager service in " + state + " | free " + mtypes.Trim().ToLower() + " in " + state + " | cheap massage " + state + "";
                            }
                        }
                    }
                    else
                    {
                        if (partner_type != "")
                        {
                            if (outcall != "")
                            {
                                string[] p = partner_type.Split(' ');
                                mtypes = "Body massage";
                                description = "At MyMassagePartner.com, find " + partner_type.ToLower() + " massage partners for " + mtypes.Trim() + " at " + outcall.ToLower() + " in " + state + ". This page showing " + state + " based " + p[0].ToLower() + " who like to exchange " + mtypes.Trim().ToLower() + " from male massage partner in " + outcall.ToLower() + ". Get registered with MyMassagePartner and ask for " + mtypes.Trim().ToLower() + " from your body massage partner. We have good number of " + p[0].ToLower() + " who wish to give " + mtypes.Trim().ToLower() + " to male in " + state + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + partner_type.ToLower() + " massage partners for " + mtypes.Trim().ToLower() + " in " + state + ". Exchange " + mtypes.Trim().ToLower() + " with your " + p[0].ToLower() + " partner anytime, anywhere in " + state + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + state + " is a good place to exchange " + mtypes.Trim().ToLower() + " under " + partner_type.ToLower() + " massage from your massage partners in " + outcall.ToLower() + ".";
                                page_title.InnerHtml = "Find " + partner_type.ToLower() + " massage partner at " + outcall.ToLower() + " in " + state;
                                title_page.InnerHtml = "Find " + partner_type.ToLower() + " body massage partner at " + outcall.ToLower() + " in " + state + " | MyMassagePartner";
                                Meta_ = "Here you can find " + partner_type.ToLower() + " body massage partners at " + outcall.ToLower() + " in " + state + " with their contact numbers. Choose your " + p[0].ToLower() + " body massage partner and start free body massage at " + outcall.ToLower() + " - MyMassagePartner, " + state + "";
                                keyword_ = "at " + outcall.ToLower() + " " + partner_type.ToLower() + " body massage " + state + " | at " + outcall.ToLower() + " " + partner_type.ToLower() + " erotic massage " + state + " | at " + outcall.ToLower() + " " + partner_type.ToLower() + " happy ending massage " + state + " | need " + partner_type.ToLower() + " body massage at " + outcall.ToLower() + " | full body massage at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + state + " | " + partner_type.ToLower() + " massage at " + outcall.ToLower() + " in " + state + " | at " + outcall.ToLower() + " sensual Massage by " + p[0].ToLower() + " in " + state + " | body massage at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + state + " | want body massage at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + state + " | " + p[0].ToLower() + " massage at " + outcall.ToLower() + " in " + state + " | massage near me | " + p[0].ToLower() + " massager at " + outcall.ToLower() + " service in " + state + " | free body massage at " + outcall.ToLower() + " in " + state + " | cheap massage at " + outcall.ToLower() + " " + state + "";
                            }
                            else
                            {
                                string[] p = partner_type.Split(' ');
                                outcall = "hotel and home";
                                mtypes = "Body massage";
                                description = "At MyMassagePartner.com, find " + partner_type.ToLower() + " massage partners  for " + mtypes.Trim() + " in " + state + ". This page showing " + state + " based " + p[0].ToLower() + " who like to exchange " + mtypes.Trim().ToLower() + " from male massage partner in " + outcall.ToLower() + ". Get registered with MyMassagePartner and ask for " + mtypes.Trim().ToLower() + " from your body massage partner. We have good number of " + p[0].ToLower() + " who wish to give " + mtypes.Trim().ToLower() + " to male in " + state + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + partner_type.ToLower() + " massage partners for " + mtypes.Trim().ToLower() + " in " + state + ". Exchange " + mtypes.Trim().ToLower() + " with your " + p[0].ToLower() + " partner anytime, anywhere in " + state + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + state + " is a good place to exchange " + mtypes.Trim().ToLower() + " under " + partner_type.ToLower() + " massage from your massage partners in " + outcall.ToLower() + ".";
                                page_title.InnerHtml = "Find " + partner_type.ToLower() + " massage partner in " + state;
                                title_page.InnerHtml = "Find " + partner_type.ToLower() + " body massage partner in " + state + " | MyMassagePartner";
                                Meta_ = "Here you can find " + partner_type.ToLower() + " body massage partners in " + state + " with their contact numbers. Choose your " + p[0].ToLower() + " body massage partner and start free body massage in home and hotel - MyMassagePartner, " + state + "";
                                keyword_ = "" + partner_type.ToLower() + " body massage " + state + " | " + partner_type.ToLower() + " erotic massage " + state + " | " + partner_type.ToLower() + " happy ending massage " + state + " | need " + partner_type.ToLower() + " body massage | full body massage by " + p[0].ToLower() + " in " + state + " | " + partner_type.ToLower() + " massage in " + state + " | sensual Massage by " + p[0].ToLower() + " in " + state + " | body massage by " + p[0].ToLower() + " in " + state + " | want body massage by " + p[0].ToLower() + " in " + state + " | " + p[0].ToLower() + " massage in " + state + " | massage near me | " + p[0].ToLower() + " massager service in " + state + " | free body massage in " + state + " | cheap massage " + state + "";
                            }
                        }
                        else
                        {
                            if (outcall != "")
                            {
                                partner_type = "female and male";
                                string[] p = partner_type.Split(' ');
                                mtypes = "Body massage";
                                description = "At MyMassagePartner.com, find female to male, female to female, male to male, and male to female massage partners for " + mtypes.Trim() + " at " + outcall.ToLower() + " in " + state + ". This page showing " + state + " based female and male who looking for female to male, female to female, male to male, and male to female " + mtypes.Trim().ToLower() + " from his or her massage partner in " + outcall.ToLower() + ". Get registered with MyMassagePartner and ask for " + mtypes.Trim().ToLower() + " from your body massage partner. We have good number of female and male who looking for " + mtypes.Trim().ToLower() + " with male or female in " + state + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available female to male, female to female, male to male, and male to female massage partners for " + mtypes.Trim().ToLower() + " in " + state + ". Exchange " + mtypes.Trim().ToLower() + " with your female and male partner anytime, anywhere in " + state + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + state + " is a good place to exchange " + mtypes.Trim().ToLower() + " under female to male, female to female, male to male, and male to female massage from your massage partners in " + outcall.ToLower() + ".";
                                page_title.InnerHtml = "Find " + partner_type.ToLower() + " massage partner at " + outcall.ToLower() + " in " + state;
                                title_page.InnerHtml = "Find " + partner_type.ToLower() + " body massage partner at " + outcall.ToLower() + " in " + state + " | MyMassagePartner";
                                Meta_ = "Here you can find female to male, female to female, male to male, and male to female body massage partners at " + outcall.ToLower() + " in " + state + " with their contact numbers. Choose your " + p[0].ToLower() + " body massage partner and start free body massage at " + outcall.ToLower() + " - MyMassagePartner, " + state + "";
                                keyword_ = "at " + outcall.ToLower() + " " + partner_type.ToLower() + " body massage " + state + " | at " + outcall.ToLower() + " " + partner_type.ToLower() + " erotic massage " + state + " | at " + outcall.ToLower() + " " + partner_type.ToLower() + " happy ending massage " + state + " | need " + partner_type.ToLower() + " body massage at " + outcall.ToLower() + " | full body massage at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + state + " | " + partner_type.ToLower() + " massage at " + outcall.ToLower() + " in " + state + " | at " + outcall.ToLower() + " sensual Massage by " + p[0].ToLower() + " in " + state + " | body massage at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + state + " | want body massage at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + state + " | " + p[0].ToLower() + " massage at " + outcall.ToLower() + " in " + state + " | massage near me | " + p[0].ToLower() + " massager at " + outcall.ToLower() + " service in " + state + " | free body massage at " + outcall.ToLower() + " in " + state + " | cheap massage at " + outcall.ToLower() + " " + state + "";
                            }
                            else
                            {
                                partner_type = "female and male";
                                string[] p = partner_type.Split(' ');
                                mtypes = "Body massage";
                                outcall = "hotel and home";
                                description = "At MyMassagePartner.com, find female to male, female to female, male to male, and male to female massage partners for " + mtypes.Trim() + " in " + state + ". This page showing " + state + " based female and male who looking for female to male, female to female, male to male, and male to female " + mtypes.Trim().ToLower() + " from his or her massage partner in " + outcall.ToLower() + ". Get registered with MyMassagePartner and ask for " + mtypes.Trim().ToLower() + " from your body massage partner. We have good number of female and male who looking for " + mtypes.Trim().ToLower() + " with male or female in " + state + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available female to male, female to female, male to male, and male to female massage partners for " + mtypes.Trim().ToLower() + " in " + state + ". Exchange " + mtypes.Trim().ToLower() + " with your female and male partner anytime, anywhere in " + state + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + state + " is a good place to exchange " + mtypes.Trim().ToLower() + " under female to male, female to female, male to male, and male to female massage from your massage partners in " + outcall.ToLower() + ".";
                                page_title.InnerHtml = "Find " + partner_type.ToLower() + " massage partner in " + state;
                                title_page.InnerHtml = "Find " + partner_type.ToLower() + " body massage partner in " + state + " | MyMassagePartner";
                                Meta_ = "Here you can find female to male, female to female, male to male, and male to female body massage partners in " + state + " with their contact numbers. Choose your " + p[0].ToLower() + " body massage partner and start free body massage in home and hotel - MyMassagePartner, " + state + "";
                                keyword_ = "" + partner_type.ToLower() + " body massage " + state + " | " + partner_type.ToLower() + " erotic massage " + state + " | " + partner_type.ToLower() + " happy ending massage " + state + " | need " + partner_type.ToLower() + " body massage | full body massage by " + p[0].ToLower() + " in " + state + " | " + partner_type.ToLower() + " massage in " + state + " | sensual Massage by " + p[0].ToLower() + " in " + state + " | body massage by " + p[0].ToLower() + " in " + state + " | want body massage by " + p[0].ToLower() + " in " + state + " | " + p[0].ToLower() + " massage in " + state + " | massage near me | " + p[0].ToLower() + " massager service in " + state + " | free body massage in " + state + " | cheap massage " + state + "";
                            }

                        }
                    }
                }
                else
                {
                    links_srp.InnerHtml = "<a href='https://www.mymassagepartner.com'>Home</a>";
                    if (mtypes != "")
                    {
                        if (partner_type != "")
                        {
                            if (outcall != "")
                            {
                                string[] p = partner_type.Split(' ');
                                description = "At MyMassagePartner.com, find " + partner_type.ToLower() + " massage partners for " + mtypes.Trim() + " at " + outcall.ToLower() + " in " + country + ". This page showing " + country + " based " + p[0].ToLower() + " who like to exchange " + mtypes.Trim().ToLower() + " from male massage partner in " + outcall.ToLower() + ". Get registered with MyMassagePartner and ask for " + mtypes.Trim().ToLower() + " from your body massage partner. We have good number of " + p[0].ToLower() + " who wish to give " + mtypes.Trim().ToLower() + " to male in " + country + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + partner_type.ToLower() + " massage partners for " + mtypes.Trim().ToLower() + " in " + country + ". Exchange " + mtypes.Trim().ToLower() + " with your " + p[0].ToLower() + " partner anytime, anywhere in " + country + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + country + " is a good place to exchange " + mtypes.Trim().ToLower() + " under " + partner_type.ToLower() + " massage from your massage partners in " + outcall.ToLower() + ".";
                                page_title.InnerHtml = partner_type.ToLower() + " massage partner for " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + country;
                                title_page.InnerHtml = "Find " + partner_type.ToLower() + " massage partner for " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + country + " | MyMassagePartner";
                                Meta_ = "Here you can find " + partner_type.ToLower() + " body massage partners for " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + country + " with their contact numbers. Choose your " + p[0].ToLower() + " body massage partner and start free " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " - MyMassagePartner, " + country + "";
                                keyword_ = "at " + outcall.ToLower() + " " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + country + " | at " + outcall.ToLower() + " " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + country + " | at " + outcall.ToLower() + " " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + country + " | need " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " | " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + country + " | " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + country + " | at " + outcall.ToLower() + " " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + country + " | " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + country + " | want " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + country + " | " + p[0].ToLower() + " massage at " + outcall.ToLower() + " in " + country + " | " + mtypes.Trim().ToLower() + " near me | " + p[0].ToLower() + " massager at " + outcall.ToLower() + " service in " + country + " | free " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + country + " | cheap massage at " + outcall.ToLower() + " " + country + "";
                            }
                            else
                            {
                                string[] p = partner_type.Split(' ');
                                outcall = "hotel and home";
                                description = "At MyMassagePartner.com, find " + partner_type.ToLower() + " massage partners for " + mtypes.Trim() + " in " + country + ". This page showing " + country + " based " + p[0].ToLower() + " who like to exchange " + mtypes.Trim().ToLower() + " from male massage partner in " + outcall.ToLower() + ". Get registered with MyMassagePartner and ask for " + mtypes.Trim().ToLower() + " from your body massage partner. We have good number of " + p[0].ToLower() + " who wish to give " + mtypes.Trim().ToLower() + " to male in " + country + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + partner_type.ToLower() + " massage partners for " + mtypes.Trim().ToLower() + " in " + country + ". Exchange " + mtypes.Trim().ToLower() + " with your " + p[0].ToLower() + " partner anytime, anywhere in " + country + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + country + " is a good place to exchange " + mtypes.Trim().ToLower() + " under " + partner_type.ToLower() + " massage from your massage partners in " + outcall.ToLower() + ".";
                                page_title.InnerHtml = partner_type.ToLower() + " massage partner for " + mtypes.Trim().ToLower() + " in " + country;
                                title_page.InnerHtml = "Find " + partner_type.ToLower() + " massage partner for " + mtypes.Trim().ToLower() + " in " + country + " | MyMassagePartner";
                                Meta_ = "Here you can find " + partner_type.ToLower() + " body massage partners for " + mtypes.Trim().ToLower() + " in " + country + " with their contact numbers. Choose your " + p[0].ToLower() + " body massage partner and start free " + mtypes.Trim().ToLower() + " in home and hotel - MyMassagePartner, " + country + "";
                                keyword_ = "" + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + country + " | " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + country + " | " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + country + " | need " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " | " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + country + " | " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " in " + country + " | " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + country + " | " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + country + " | want " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + country + " | " + p[0].ToLower() + " massage in " + country + " | " + mtypes.Trim().ToLower() + " near me | " + p[0].ToLower() + " massager service in " + country + " | free " + mtypes.Trim().ToLower() + " in " + country + " | cheap massage " + country + "";
                            }
                        }
                        else
                        {
                            if (outcall != "")
                            {
                                partner_type = "female and male";
                                string[] p = partner_type.Split(' ');
                                description = "At MyMassagePartner.com, find female to male, female to female, male to male, and male to female massage partners for " + mtypes.Trim() + " at " + outcall.ToLower() + " in " + country + ". This page showing " + country + " based female and male who looking for female to male, female to female, male to male, and male to female " + mtypes.Trim().ToLower() + " from his or her massage partner in " + outcall.ToLower() + ". Get registered with MyMassagePartner and ask for " + mtypes.Trim().ToLower() + " from your body massage partner. We have good number of female and male who looking for " + mtypes.Trim().ToLower() + " with male or female in " + country + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available female to male, female to female, male to male, and male to female massage partners for " + mtypes.Trim().ToLower() + " in " + country + ". Exchange " + mtypes.Trim().ToLower() + " with your female and male partner anytime, anywhere in " + country + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + country + " is a good place to exchange " + mtypes.Trim().ToLower() + " under female to male, female to female, male to male, and male to female massage from your massage partners in " + outcall.ToLower() + ".";
                                page_title.InnerHtml = partner_type.ToLower() + " massage partner for " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + country;
                                title_page.InnerHtml = "Find " + partner_type.ToLower() + " massage partner for " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + country + " | MyMassagePartner";
                                Meta_ = "Here you can find " + partner_type.ToLower() + " body massage partners for " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + country + " with their contact numbers. Choose your " + p[0].ToLower() + " body massage partner and start free " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " - MyMassagePartner, " + country + "";
                                keyword_ = "at " + outcall.ToLower() + " " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + country + " | at " + outcall.ToLower() + " " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + country + " | at " + outcall.ToLower() + " " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + country + " | need " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " | " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + country + " | " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + country + " | at " + outcall.ToLower() + " " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + country + " | " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + country + " | want " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + country + " | " + p[0].ToLower() + " massage at " + outcall.ToLower() + " in " + country + " | " + mtypes.Trim().ToLower() + " near me | " + p[0].ToLower() + " massager at " + outcall.ToLower() + " service in " + country + " | free " + mtypes.Trim().ToLower() + " at " + outcall.ToLower() + " in " + country + " | cheap massage at " + outcall.ToLower() + " " + country + "";
                            }
                            else
                            {
                                partner_type = "female and male";
                                string[] p = partner_type.Split(' ');
                                outcall = "hotel and home";
                                description = "At MyMassagePartner.com, find female to male, female to female, male to male, and male to female massage partners for " + mtypes.Trim() + " in " + country + ". This page showing " + country + " based female and male who looking for female to male, female to female, male to male, and male to female " + mtypes.Trim().ToLower() + " from his or her massage partner in " + outcall.ToLower() + ". Get registered with MyMassagePartner and ask for " + mtypes.Trim().ToLower() + " from your body massage partner. We have good number of female and male who looking for " + mtypes.Trim().ToLower() + " with male or female in " + country + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available female to male, female to female, male to male, and male to female massage partners for " + mtypes.Trim().ToLower() + " in " + country + ". Exchange " + mtypes.Trim().ToLower() + " with your female and male partner anytime, anywhere in " + country + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + country + " is a good place to exchange " + mtypes.Trim().ToLower() + " under female to male, female to female, male to male, and male to female massage from your massage partners in " + outcall.ToLower() + ".";
                                page_title.InnerHtml = partner_type.ToLower() + " massage partner for " + mtypes.Trim().ToLower() + " in " + country;
                                title_page.InnerHtml = "Find " + partner_type.ToLower() + " massage partner for " + mtypes.Trim().ToLower() + " in " + country + " | MyMassagePartner";
                                Meta_ = "Here you can find " + partner_type.ToLower() + " body massage partners for " + mtypes.Trim().ToLower() + " in " + country + " with their contact numbers. Choose your " + p[0].ToLower() + " body massage partner and start free " + mtypes.Trim().ToLower() + " in home and hotel - MyMassagePartner, " + country + "";
                                keyword_ = "" + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + country + " | " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + country + " | " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " " + country + " | need " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " | " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + country + " | " + partner_type.ToLower() + " " + mtypes.Trim().ToLower() + " in " + country + " | " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + country + " | " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + country + " | want " + mtypes.Trim().ToLower() + " by " + p[0].ToLower() + " in " + country + " | " + p[0].ToLower() + " massage in " + country + " | " + mtypes.Trim().ToLower() + " near me | " + p[0].ToLower() + " massager service in " + country + " | free " + mtypes.Trim().ToLower() + " in " + country + " | cheap massage " + country + "";
                            }
                        }
                    }
                    else
                    {
                        if (partner_type != "")
                        {
                            if (outcall != "")
                            {
                                string[] p = partner_type.Split(' ');
                                mtypes = "Body massage";
                                description = "At MyMassagePartner.com, find " + partner_type.ToLower() + " massage partners for " + mtypes.Trim() + " at " + outcall.ToLower() + " in " + country + ". This page showing " + country + " based " + p[0].ToLower() + " who like to exchange " + mtypes.Trim().ToLower() + " from male massage partner in " + outcall.ToLower() + ". Get registered with MyMassagePartner and ask for " + mtypes.Trim().ToLower() + " from your body massage partner. We have good number of " + p[0].ToLower() + " who wish to give " + mtypes.Trim().ToLower() + " to male in " + country + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + partner_type.ToLower() + " massage partners for " + mtypes.Trim().ToLower() + " in " + country + ". Exchange " + mtypes.Trim().ToLower() + " with your " + p[0].ToLower() + " partner anytime, anywhere in " + country + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + country + " is a good place to exchange " + mtypes.Trim().ToLower() + " under " + partner_type.ToLower() + " massage from your massage partners in " + outcall.ToLower() + ".";
                                page_title.InnerHtml = "Find " + partner_type.ToLower() + " massage partner at " + outcall.ToLower() + " in " + country;
                                title_page.InnerHtml = "Find " + partner_type.ToLower() + " body massage partner at " + outcall.ToLower() + " in " + country + " | MyMassagePartner";
                                Meta_ = "Here you can find " + partner_type.ToLower() + " body massage partners at " + outcall.ToLower() + " in " + country + " with their contact numbers. Choose your " + p[0].ToLower() + " body massage partner and start free body massage at " + outcall.ToLower() + " - MyMassagePartner, " + country + "";
                                keyword_ = "at " + outcall.ToLower() + " " + partner_type.ToLower() + " body massage " + country + " | at " + outcall.ToLower() + " " + partner_type.ToLower() + " erotic massage " + country + " | at " + outcall.ToLower() + " " + partner_type.ToLower() + " happy ending massage " + country + " | need " + partner_type.ToLower() + " body massage at " + outcall.ToLower() + " | full body massage at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + country + " | " + partner_type.ToLower() + " massage at " + outcall.ToLower() + " in " + country + " | at " + outcall.ToLower() + " sensual Massage by " + p[0].ToLower() + " in " + country + " | body massage at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + country + " | want body massage at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + country + " | " + p[0].ToLower() + " massage at " + outcall.ToLower() + " in " + country + " | massage near me | " + p[0].ToLower() + " massager at " + outcall.ToLower() + " service in " + country + " | free body massage at " + outcall.ToLower() + " in " + country + " | cheap massage at " + outcall.ToLower() + " " + country + "";
                            }
                            else
                            {
                                string[] p = partner_type.Split(' ');
                                outcall = "hotel and home";
                                mtypes = "Body massage";
                                description = "At MyMassagePartner.com, find " + partner_type.ToLower() + " massage partners for " + mtypes.Trim() + " in " + country + ". This page showing " + country + " based " + p[0].ToLower() + " who like to exchange " + mtypes.Trim().ToLower() + " from male massage partner in " + outcall.ToLower() + ". Get registered with MyMassagePartner and ask for " + mtypes.Trim().ToLower() + " from your body massage partner. We have good number of " + p[0].ToLower() + " who wish to give " + mtypes.Trim().ToLower() + " to male in " + country + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available " + partner_type.ToLower() + " massage partners for " + mtypes.Trim().ToLower() + " in " + country + ". Exchange " + mtypes.Trim().ToLower() + " with your " + p[0].ToLower() + " partner anytime, anywhere in " + country + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + country + " is a good place to exchange " + mtypes.Trim().ToLower() + " under " + partner_type.ToLower() + " massage from your massage partners in " + outcall.ToLower() + ".";
                                page_title.InnerHtml = "Find " + partner_type.ToLower() + " massage partner in " + country;
                                title_page.InnerHtml = "Find " + partner_type.ToLower() + " body massage partner in " + country + " | MyMassagePartner";
                                Meta_ = "Here you can find " + partner_type.ToLower() + " body massage partners in " + country + " with their contact numbers. Choose your " + p[0].ToLower() + " body massage partner and start free body massage in home and hotel - MyMassagePartner, " + country + "";
                                keyword_ = "" + partner_type.ToLower() + " body massage " + country + " | " + partner_type.ToLower() + " erotic massage " + country + " | " + partner_type.ToLower() + " happy ending massage " + country + " | need " + partner_type.ToLower() + " body massage | full body massage by " + p[0].ToLower() + " in " + country + " | " + partner_type.ToLower() + " massage in " + country + " | sensual Massage by " + p[0].ToLower() + " in " + country + " | body massage by " + p[0].ToLower() + " in " + country + " | want body massage by " + p[0].ToLower() + " in " + country + " | " + p[0].ToLower() + " massage in " + country + " | massage near me | " + p[0].ToLower() + " massager service in " + country + " | free body massage in " + country + " | cheap massage " + country + "";
                            }
                        }
                        else
                        {
                            if (outcall != "")
                            {
                                partner_type = "female and male";
                                string[] p = partner_type.Split(' ');
                                mtypes = "Body massage";
                                description = "At MyMassagePartner.com, find female to male, female to female, male to male, and male to female massage partners for " + mtypes.Trim() + " at " + outcall.ToLower() + " in " + country + ". This page showing " + country + " based female and male who looking for female to male, female to female, male to male, and male to female " + mtypes.Trim().ToLower() + " from his or her massage partner in " + outcall.ToLower() + ". Get registered with MyMassagePartner and ask for " + mtypes.Trim().ToLower() + " from your body massage partner. We have good number of female and male who looking for " + mtypes.Trim().ToLower() + " with male or female in " + country + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available female to male, female to female, male to male, and male to female massage partners for " + mtypes.Trim().ToLower() + " in " + country + ". Exchange " + mtypes.Trim().ToLower() + " with your female and male partner anytime, anywhere in " + country + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + country + " is a good place to exchange " + mtypes.Trim().ToLower() + " under female to male, female to female, male to male, and male to female massage from your massage partners in " + outcall.ToLower() + ".";
                                page_title.InnerHtml = "Find " + partner_type.ToLower() + " massage partner at " + outcall.ToLower() + " in " + country;
                                title_page.InnerHtml = "Find " + partner_type.ToLower() + " body massage partner at " + outcall.ToLower() + " in " + country + " | MyMassagePartner";
                                Meta_ = "Here you can find female to male, female to female, male to male, and male to female body massage partners at " + outcall.ToLower() + " in " + country + " with their contact numbers. Choose your " + p[0].ToLower() + " body massage partner and start free body massage at " + outcall.ToLower() + " - MyMassagePartner, " + country + "";
                                keyword_ = "at " + outcall.ToLower() + " " + partner_type.ToLower() + " body massage " + country + " | at " + outcall.ToLower() + " " + partner_type.ToLower() + " erotic massage " + country + " | at " + outcall.ToLower() + " " + partner_type.ToLower() + " happy ending massage " + country + " | need " + partner_type.ToLower() + " body massage at " + outcall.ToLower() + " | full body massage at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + country + " | " + partner_type.ToLower() + " massage at " + outcall.ToLower() + " in " + country + " | at " + outcall.ToLower() + " sensual Massage by " + p[0].ToLower() + " in " + country + " | body massage at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + country + " | want body massage at " + outcall.ToLower() + " by " + p[0].ToLower() + " in " + country + " | " + p[0].ToLower() + " massage at " + outcall.ToLower() + " in " + country + " | massage near me | " + p[0].ToLower() + " massager at " + outcall.ToLower() + " service in " + country + " | free body massage at " + outcall.ToLower() + " in " + country + " | cheap massage at " + outcall.ToLower() + " " + country + "";
                            }
                            else
                            {
                                partner_type = "female and male";
                                string[] p = partner_type.Split(' ');
                                mtypes = "Body massage";
                                outcall = "hotel and home";
                                description = "At MyMassagePartner.com, find female to male, female to female, male to male, and male to female massage partners for " + mtypes.Trim() + " in " + country + ". This page showing " + country + " based female and male who looking for female to male, female to female, male to male, and male to female " + mtypes.Trim().ToLower() + " from his or her massage partner in " + outcall.ToLower() + ". Get registered with MyMassagePartner and ask for " + mtypes.Trim().ToLower() + " from your body massage partner. We have good number of female and male who looking for " + mtypes.Trim().ToLower() + " with male or female in " + country + ". Just choose and start conversation over phone or/and message. You can select different massage types which suits you best and check available female to male, female to female, male to male, and male to female massage partners for " + mtypes.Trim().ToLower() + " in " + country + ". Exchange " + mtypes.Trim().ToLower() + " with your female and male partner anytime, anywhere in " + country + ". You can see massage partner name, contact number, desired massage types, location, gender as well as you can send message, add favourite, report abuse, also in worst condition you may block to massage partner. " + country + " is a good place to exchange " + mtypes.Trim().ToLower() + " under female to male, female to female, male to male, and male to female massage from your massage partners in " + outcall.ToLower() + ".";
                                page_title.InnerHtml = "Find " + partner_type.ToLower() + " massage partner in " + country;
                                title_page.InnerHtml = "Find " + partner_type.ToLower() + " body massage partner in " + country + " | MyMassagePartner";
                                Meta_ = "Here you can find female to male, female to female, male to male, and male to female body massage partners in " + country + " with their contact numbers. Choose your " + p[0].ToLower() + " body massage partner and start free body massage in home and hotel - MyMassagePartner, " + country + "";
                                keyword_ = "" + partner_type.ToLower() + " body massage " + country + " | " + partner_type.ToLower() + " erotic massage " + country + " | " + partner_type.ToLower() + " happy ending massage " + country + " | need " + partner_type.ToLower() + " body massage | full body massage by " + p[0].ToLower() + " in " + country + " | " + partner_type.ToLower() + " massage in " + country + " | sensual Massage by " + p[0].ToLower() + " in " + country + " | body massage by " + p[0].ToLower() + " in " + country + " | want body massage by " + p[0].ToLower() + " in " + country + " | " + p[0].ToLower() + " massage in " + country + " | massage near me | " + p[0].ToLower() + " massager service in " + country + " | free body massage in " + country + " | cheap massage " + country + "";
                            }

                        }
                    }
                }


                page_title.InnerHtml = page_title.InnerHtml.Replace("  ", " ");
                des.InnerHtml = description;
                page_title.InnerHtml = page_title.InnerHtml.ToString();
                page_title.InnerHtml = page_title.InnerHtml.Replace("In", "in");
                page_title.InnerHtml = page_title.InnerHtml.Replace("And", "and");

                title_page.InnerHtml = title_page.InnerHtml.Replace("  ", " ");
                if (Request.QueryString["page"] != null)
                {
                    string page = "Page " + Request.QueryString["page"].ToString() + " - ";
                    title_page.InnerHtml = title_page.InnerHtml.Replace("| MyMassagePartner", "| " + page + " MyMassagePartner");
                }
                Meta_ = Meta_.Trim().Replace("  ", " ");
                keyword_ = keyword_.Replace("  ", " ");


                metaDescription.Attributes.Add("content", Meta_);
                keywordcontent.Attributes.Add("content", keyword_);


                //lbl_title.InnerHtml = title_;
                //lbl_title.Attributes.Add("title", title_);
                // keywordcontent.Controls.Add(keyword);
            }
            catch (Exception ex)
            {
                //ErrorHandling handling = new ErrorHandling();
                //handling.reqUrl = Request.Url.AbsoluteUri; Random rnd = new Random();
                //int card = rnd.Next(1000, 100000);
                //handling.filePath = Server.MapPath("~/ErrorLog") + "\\errorLOG" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-") + card + ".txt";
                //handling.methodname = "DynamicMeta";
                //handling.lastErrorTypeName = ex.GetType().ToString();
                //handling.lastErrorMessage = ex.Message;
                //handling.lastErrorStackTrace = ex.StackTrace;
                //handling.writefile();
                BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message;
                var st = new System.Diagnostics.StackTrace(ex, true);
                StackFrame[] stackFrames = st.GetFrames();
                foreach (StackFrame stackFrame in stackFrames)
                {
                    Console.WriteLine(stackFrame.GetMethod().Name);   // write method name
                    BussinessEntity.ExceptionHandling._lineno = stackFrame.GetFileLineNumber();
                    BussinessEntity.ExceptionHandling._methodname = Convert.ToString(stackFrame.GetMethod().Name);
                    BussinessEntity.ExceptionHandling._pagename = Convert.ToString(Request.Url.AbsoluteUri);

                }
                Response.Redirect(Constants__.WEB_ROOT + "/ErrorMessage.aspx", false);
                return;

            }
        }
        public string UppercaseFirstEach(string s)
        {
            char[] a = s.ToLower().ToCharArray();

            for (int i = 0; i < a.Count(); i++)
            {
                a[i] = i == 0 || a[i - 1] == ' ' ? char.ToUpper(a[i]) : a[i];

            }

            return new string(a);
        }
        private void SRP_load()
        {
            string country = "";
            string city = "";
            string state = "";
            string looking = "";
            string area = "";
            string gender = "";
            string mtypes = "";
            string from_to = "";
            string outcall = "";


            bind_M2B_link(RouteData.Values["country"].ToString(), RouteData.Values["state"].ToString(), RouteData.Values["city"].ToString(), RouteData.Values["area"].ToString());
            if (RouteData.Values["country"] != null && RouteData.Values["country"].ToString() != "")
            {
                country = RouteData.Values["country"].ToString().Replace('-', ' ');
            }
            if (RouteData.Values["state"] != null && RouteData.Values["state"].ToString() != "")
            {
                state = RouteData.Values["state"].ToString().Replace('-', ' ');
            }
            if (RouteData.Values["city"] != null && RouteData.Values["city"].ToString() != "")
            {
                city = RouteData.Values["city"].ToString().Replace('-', ' ');
            }
            if (RouteData.Values["looking"] != null && RouteData.Values["looking"].ToString() != "")
            {
                looking = RouteData.Values["looking"].ToString().Trim();
            }
            if (RouteData.Values["area"] != null && RouteData.Values["area"].ToString() != "")
            {
                area = RouteData.Values["area"].ToString().Trim();
            }
            if (RouteData.Values["gender"] != null && RouteData.Values["gender"].ToString() != "")
            {
                gender = RouteData.Values["gender"].ToString().Trim();
            }
            if (RouteData.Values["mtypes"] != null && RouteData.Values["mtypes"].ToString() != "")
            {
                mtypes = RouteData.Values["mtypes"].ToString().Trim();
            }
            if (RouteData.Values["partnerType"] != null && RouteData.Values["partnerType"].ToString() != "")
            {
                from_to = RouteData.Values["partnerType"].ToString().Trim();
            }
            if (RouteData.Values["outcall"] != null && RouteData.Values["outcall"].ToString() != "")
            {
                outcall = RouteData.Values["outcall"].ToString().Trim();
            }
            country = country.Replace('-', ' ');
            state = state.Replace('-', ' ');
            city = city.Replace('-', ' ');
            area = area.Replace('-', ' ');
            from_to = from_to.Replace('-', ' ');
            from_to = from_to.Replace(" to", " partner to");
            from_to = from_to + " partner";
            gender = gender.Replace('-', ' ');
            mtypes = mtypes.Replace('-', ' ');

            if (country != "z" && country != "")
            {
                for (int i = 0; i < ddlcountry.Items.Count; i++)
                {
                    if (ddlcountry.Items[i].ToString().ToUpper() == country.ToUpper())
                    {
                        ddlcountry.SelectedIndex = i;
                    }
                }
                if (ddlcountry.SelectedIndex <= 0)
                {
                    Response.Redirect(Constants__.WEB_ROOT + "/404", false);
                }
                else
                {
                    country_changed();
                    bind_area();
                }
            }
            if (state != "s" && state != "")
            {
                for (int i = 0; i < ddlstate.Items.Count; i++)
                {
                    if (ddlstate.Items[i].ToString().ToUpper() == state.ToUpper())
                    {
                        ddlstate.SelectedIndex = i;
                    }
                }
                if (ddlstate.SelectedIndex <= 0)
                {
                    Response.Redirect(Constants__.WEB_ROOT + "/404", false);
                }
                else
                {
                    state_changed();
                    city_changed();
                }
            }
            else if (state == "s")
            {
                ddlstate.SelectedIndex = 0;
            }
            if (city != "a" && city != "" && city != "c")
            {
                //  ddlcity.SelectedIndex = ddlcity.Items.IndexOf(ddlcity.Items.FindByText(city));
                for (int i = 0; i < ddlcity.Items.Count; i++)
                {
                    if (ddlcity.Items[i].ToString().ToUpper() == city.ToUpper())
                    {
                        ddlcity.SelectedIndex = i;
                    }
                }
                if (ddlcity.SelectedIndex <= 0)
                {
                    Response.Redirect(Constants__.WEB_ROOT + "/404", false);
                }
                else
                {
                    city_changed();

                    if (ddlcity.SelectedIndex > 0)
                    {
                        DataTable massages = new DataTable();
                        massages.Columns.Add("Name", typeof(string));
                        DataTable areas = new DataTable();
                        areas.Columns.Add("Name", typeof(string));

                        for (int i = 0; i < ddlarea.Items.Count; i++)
                        {
                            if (ddlarea.Items[i].ToString() != "All Area")
                            {
                                areas.Rows.Add(ddlarea.Items[i]);
                            }
                        }
                        for (int i = 0; i < ddlmassagetypes.Items.Count; i++)
                        {
                            if (ddlmassagetypes.Items[i].ToString() != "Select Massage Type")
                            {
                                massages.Rows.Add(ddlmassagetypes.Items[i]);
                            }
                        }

                        Session["areas_for_link"] = areas;
                        Session["massage_for_link"] = massages;
                    }
                }
            }
            else if (city == "a" && ddlcity.Enabled == true)
            {
                ddlcity.SelectedIndex = 0;
            }
            if (looking != "all" && looking != "")
            {
                string[] looks_for = looking.Split('-');
                looking = looks_for[0] + " " + looks_for[1] + " / " + looks_for[2];

                if (looking == "Massage Therapist / Professional")
                    ddllookingfor.SelectedIndex = ddllookingfor.Items.IndexOf(ddllookingfor.Items.FindByValue("T"));
                else
                    ddllookingfor.SelectedIndex = ddllookingfor.Items.IndexOf(ddllookingfor.Items.FindByValue("M"));
                if (ddllookingfor.SelectedIndex <= 0)
                {
                    Response.Redirect(Constants__.WEB_ROOT + "/404", false);
                }
            }

            if (gender != "any" && gender != "")
            { ddlgender.SelectedIndex = ddlgender.Items.IndexOf(ddlgender.Items.FindByText(gender)); }

            if (mtypes != "all types" && mtypes != "")
            {
                for (int i = 0; i < ddlmassagetypes.Items.Count; i++)
                {
                    if (ddlmassagetypes.Items[i].ToString().ToUpper() == mtypes.ToUpper())
                    {
                        ddlmassagetypes.SelectedIndex = i;
                    }
                }
                if (ddlmassagetypes.SelectedIndex <= 0)
                {
                    Response.Redirect(Constants__.WEB_ROOT + "/404", false);
                }
                else
                {
                    if (ddlmassagetypes.SelectedItem.Text == "Gay Massage")
                    {
                        ddlgender.SelectedValue = "M";
                        ddlPartner_Types.SelectedValue = "MM";
                        //   ddlgender.Enabled = false;
                        //ddlPartner_Types.Enabled = false;
                    }
                    else
                        if (ddlmassagetypes.SelectedItem.Text == "Lesbian Massage")
                        {
                            ddlgender.SelectedValue = "F";
                            ddlPartner_Types.SelectedValue = "FF";
                            // ddlgender.Enabled = false;
                            //ddlPartner_Types.Enabled = false;
                        }
                        else
                        {
                            ddlgender.Enabled = true;
                            ddlPartner_Types.Enabled = true;
                        }
                }
            }

            if (area != "All Area" && area != "" && area != "all area")
            {
                for (int i = 0; i < ddlarea.Items.Count; i++)
                {
                    if (ddlarea.Items[i].ToString().ToUpper() == area.ToUpper())
                    {
                        ddlarea.SelectedIndex = i;
                    }
                }
                if (ddlarea.SelectedIndex <= 0)
                {
                    Response.Redirect(Constants__.WEB_ROOT + "/404", false);
                }
            }
            else if (area == "All Area" && ddlarea.Enabled == true)
            {
                ddlarea.SelectedIndex = 0;

            }
            if (from_to != "any" && from_to != " partner" && from_to != "any partner")
            {
                for (int i = 0; i < ddlPartner_Types.Items.Count; i++)
                {
                    if (ddlPartner_Types.Items[i].ToString().ToUpper() == from_to.ToUpper())
                    {
                        ddlPartner_Types.SelectedIndex = i;
                        ddlPartner_Types.ToolTip = ddlPartner_Types.SelectedItem.Text;
                    }
                }
                if (ddlPartner_Types.SelectedIndex <= 0)
                {
                    Response.Redirect(Constants__.WEB_ROOT + "/404", false);
                }
            }
            if (outcall != "")
            {
                for (int i = 0; i < ddlOutCall.Items.Count; i++)
                {
                    if (ddlOutCall.Items[i].ToString().ToUpper() == outcall.ToUpper())
                    {
                        ddlOutCall.SelectedIndex = i;
                        ddlOutCall.ToolTip = ddlOutCall.SelectedItem.Text;
                    }
                }
                if (ddlOutCall.SelectedIndex <= 0)
                {
                    Response.Redirect(Constants__.WEB_ROOT + "/404", false);
                }
            }
            //if (Session["selected_gender"] != null)
            //{
            //    ddlgender.SelectedIndex = ddlgender.Items.IndexOf(ddlgender.Items.FindByValue(Session["selected_gender"].ToString()));
            //}
            if (Session["selected_age"] != null)
            {
                ddlage.SelectedIndex = ddlage.Items.IndexOf(ddlage.Items.FindByValue(Session["selected_age"].ToString()));
            }
            if (ddlarea.SelectedIndex < 1)
            {
                deep_links_default();
            }
            deep_links_city_wise();
        }
        public string search_url()
        {
            string country = "";
            string state = "";
            string city = "";
            string looking = "";
            string area = "";
            string gender = "";
            string mtypes = "";
            string from_to = "";
            string outcall = "";

            country = ddlcountry.SelectedIndex > 0 ? ddlcountry.SelectedItem.Text : "z";
            state = ddlstate.SelectedIndex > 0 ? ddlstate.SelectedItem.Text : "s";
            city = ddlcity.SelectedIndex > 0 ? ddlcity.SelectedItem.Text : "a";
            looking = ddllookingfor.SelectedIndex > 0 ? ddllookingfor.SelectedItem.Text : "all";
            area = ddlarea.SelectedIndex > 0 ? ddlarea.SelectedItem.Text : "All-Area";
            gender = ddlgender.SelectedIndex > 0 ? ddlgender.SelectedItem.Text : "Any";
            mtypes = ddlmassagetypes.SelectedIndex > 0 ? ddlmassagetypes.SelectedItem.Text : "all-types";
            from_to = ddlPartner_Types.SelectedIndex > 0 ? ddlPartner_Types.SelectedItem.Text : "any";
            outcall = ddlOutCall.SelectedIndex > 0 ? ddlOutCall.SelectedValue : "";

            country = country.Replace(' ', '-');
            state = state.Replace(' ', '-');
            state = state.Replace("&", "and");
            city = city.Replace(' ', '-');
            looking = looking.Replace('/', ' ');
            looking = looking.Replace("   ", " ");
            looking = looking.Replace(' ', '-');
            looking = looking.Trim().Replace(' ', '-');
            area = area.Replace(' ', '-');
            gender = gender.Trim().Replace(' ', '-');
            mtypes = mtypes.Trim().Replace(' ', '-');
            from_to = from_to.Replace(" partner ", " ");
            from_to = from_to.Replace(" partner", "");
            from_to = from_to.Trim().Replace(' ', '-');
            string url = "";
            url = Request.RawUrl.ToString();
            if (url.Contains("body-massage-partner"))
            {
                if (outcall != "")
                {
                    url = "/" + country + "/" + state + "/" + city + "/" + area + "/" + from_to + "/" + mtypes + "/" + outcall;
                    return url;
                }
                else
                {
                    url = "/" + country + "/" + state + "/" + city + "/" + area + "/" + from_to + "/" + mtypes;
                    return url;
                }

            }
            else
            {
                url = "/" + country + "/" + state + "/" + city + "/" + area + "/" + gender + "/" + mtypes + "/" + looking;
                return url;
            }



        }

        private void deep_links_default()
        {
            DateTime begintime = DateTime.UtcNow;
            DateTime endtime = DateTime.UtcNow;
            DataTable deep_lnk_defatult_dt = new DataTable();
            deep_lnk_defatult_dt.Columns.Add("link", typeof(string));
            try
            {
                string url = "";
                url = Request.RawUrl.ToString();
                //tbllink.Controls.Clear();
                string area_url;
                DataTable datatable = new DataTable();
                datatable = (DataTable)Session["areas_for_link"];
                if (datatable.Rows.Count > 0)
                {
                    string city_name = ddlcity.SelectedIndex >= 1 ? ddlcity.SelectedItem.Text : "";
                    int total_link = datatable.Rows.Count;
                    int rows = total_link / 3;

                    if (total_link % 3 != 0)
                    {
                        rows = rows + 1;
                    }
                    int cols = 4;
                    int area_no = 0;

                    for (int i = 0; i < rows; i++)
                    {
                        //TableRow rowNew = new TableRow();

                        for (int j = 0; j < cols; j++)
                        {
                            if (area_no < total_link)
                            {

                                //TableCell cellNew = new TableCell();
                                //Label lblNew = new Label();
                                if (city_name != "")
                                {
                                    if (ddlmassagetypes.SelectedIndex > 0)
                                    {
                                        if (url.Contains("body-massage-partner"))
                                        {
                                            string act_url = Constants__.WEB_ROOT + "/massage-partner/" + ddlcountry.SelectedItem.Text.Replace(' ', '-') + "/" + ddlstate.SelectedItem.Text.Replace(' ', '-') + "/" + ddlcity.SelectedItem.Text.Replace(' ', '-') + "/" + datatable.Rows[area_no]["name"].ToString().Replace(' ', '-') + "/Any/" + ddlmassagetypes.SelectedItem.Text.Replace(' ', '-') + "/all";
                                            deep_lnk_defatult_dt.Rows.Add("<a href='" + act_url + "' class='foot-azam' target='_blank' title='" + ddlmassagetypes.SelectedItem.Text + " in " + datatable.Rows[area_no]["name"].ToString() + "' >" + ddlmassagetypes.SelectedItem.Text + " in " + datatable.Rows[area_no]["name"].ToString() + "</a>");
                                        }
                                        else
                                        {
                                            string act_url = Constants__.WEB_ROOT + "/body-massage-partner/" + ddlcountry.SelectedItem.Text.Replace(' ', '-') + "/" + ddlstate.SelectedItem.Text.Replace(' ', '-') + "/" + ddlcity.SelectedItem.Text.Replace(' ', '-') + "/" + datatable.Rows[area_no]["name"].ToString().Replace(' ', '-') + "/any/" + ddlmassagetypes.SelectedItem.Text.Replace(' ', '-') + "";
                                            deep_lnk_defatult_dt.Rows.Add("<a href='" + act_url + "' class='foot-azam' target='_blank' title='" + ddlmassagetypes.SelectedItem.Text + " in " + datatable.Rows[area_no]["name"].ToString() + "' >" + ddlmassagetypes.SelectedItem.Text + " in " + datatable.Rows[area_no]["name"].ToString() + "</a>");

                                        }
                                    }
                                    else
                                    {
                                        if (url.Contains("body-massage-partner"))
                                        {
                                            string act_url = Constants__.WEB_ROOT + "/massage-partner/" + ddlcountry.SelectedItem.Text.Replace(' ', '-') + "/" + ddlstate.SelectedItem.Text.Replace(' ', '-') + "/" + ddlcity.SelectedItem.Text.Replace(' ', '-') + "/" + datatable.Rows[area_no]["name"].ToString().Replace(' ', '-') + "/Any/" + ddlmassagetypes.SelectedItem.Text.Replace(' ', '-') + "/all";
                                            deep_lnk_defatult_dt.Rows.Add("<a href='" + act_url + "' class='foot-azam' target='_blank' title='Body Massage Partner in " + datatable.Rows[area_no]["name"].ToString() + "' >Body Massage Partner in " + datatable.Rows[area_no]["name"].ToString() + "</a>");
                                        }
                                        else
                                        {
                                            string act_url = Constants__.WEB_ROOT + "/body-massage-partner/" + ddlcountry.SelectedItem.Text.Replace(' ', '-') + "/" + ddlstate.SelectedItem.Text.Replace(' ', '-') + "/" + ddlcity.SelectedItem.Text.Replace(' ', '-') + "/" + datatable.Rows[area_no]["name"].ToString().Replace(' ', '-') + "/any/all-types";
                                            deep_lnk_defatult_dt.Rows.Add("<a href='" + act_url + "' class='foot-azam' target='_blank' title='Body Massage Partner in " + datatable.Rows[area_no]["name"].ToString() + "' >Body Massage Partner in " + datatable.Rows[area_no]["name"].ToString() + "</a>");
                                        }
                                    }
                                    //cellNew.Controls.Add(lblNew);
                                    //cellNew.Attributes.Add("class", "newtdcss");
                                    //rowNew.Controls.Add(cellNew);
                                    // area_no++;
                                }
                                area_no++;
                            }


                        }
                        if (city_name != "")
                        {
                            //tbllink.Controls.Add(rowNew);
                            repeater_link.DataSource = deep_lnk_defatult_dt;
                            repeater_link.DataBind();
                            lblmassagelinksTitle.Visible = false;
                            divAreaSrpUrls.Visible = false; lblmassagelinksTitle.Visible = false;
                            lblmassagelinksTitle.Text = "Popular Localities in and around " + city_name;
                        }
                        else
                        {
                            lblmassagelinksTitle.Visible = false;
                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message;
                var st = new System.Diagnostics.StackTrace(ex, true);
                StackFrame[] stackFrames = st.GetFrames();
                foreach (StackFrame stackFrame in stackFrames)
                {
                    Console.WriteLine(stackFrame.GetMethod().Name);   // write method name
                    BussinessEntity.ExceptionHandling._lineno = stackFrame.GetFileLineNumber();
                    BussinessEntity.ExceptionHandling._methodname = Convert.ToString(stackFrame.GetMethod().Name);
                    BussinessEntity.ExceptionHandling._pagename = Convert.ToString(Request.Url.AbsoluteUri);

                }
            }
        }

        private void deep_links_city_wise()
        {
            DateTime begintime = DateTime.UtcNow;
            DateTime endtime = DateTime.UtcNow;
            DataTable deep_lnk_speciality_dt = new DataTable();
            deep_lnk_speciality_dt.Columns.Add("link", typeof(string));
            string url = "";
            url = Request.RawUrl.ToString();
            try
            {
                //tbllink.Controls.Clear(); string area_url;
                DataTable datatable = new DataTable();
                datatable = (DataTable)Session["massage_for_link"];
                if (datatable.Rows.Count > 0)
                {
                    string city_name = ddlcity.SelectedIndex >= 1 ? (ddlarea.SelectedIndex >= 1 ? ddlarea.SelectedItem.Text : ddlcity.SelectedItem.Text) : "";
                    int total_link = datatable.Rows.Count;
                    int rows = total_link / 3;

                    if (total_link % 3 != 0)
                    {
                        rows = rows + 1;
                    }
                    int cols = 4;
                    int area_no = 0;

                    for (int i = 0; i < rows; i++)
                    {
                        /// TableRow rowNew = new TableRow();

                        for (int j = 0; j < cols; j++)
                        {
                            if (area_no < total_link)
                            {

                                //TableCell cellNew = new TableCell();
                                //Label lblNew = new Label();
                                if (city_name != "")
                                {
                                    if (ddlarea.SelectedIndex >= 1)
                                    {
                                        if (url.Contains("body-massage-partner"))
                                        {
                                            string act_url = Constants__.WEB_ROOT + "/massage-partner/" + ddlcountry.SelectedItem.Text.Replace(' ', '-') + "/" + ddlstate.SelectedItem.Text.Replace(' ', '-') + "/" + ddlcity.SelectedItem.Text.Replace(' ', '-') + "/" + ddlarea.SelectedItem.Text.Replace(' ', '-') + "/Any/" + datatable.Rows[area_no]["name"].ToString().Replace(' ', '-') + "/all";
                                            deep_lnk_speciality_dt.Rows.Add("<a href='" + act_url + "' class='foot-azam' target='_blank' title='" + datatable.Rows[area_no]["name"].ToString() + " Partner in " + city_name + "' >" + datatable.Rows[area_no]["name"].ToString() + " Partner in " + city_name + "</a>");
                                        }
                                        else
                                        {
                                            string act_url = Constants__.WEB_ROOT + "/body-massage-partner/" + ddlcountry.SelectedItem.Text.Replace(' ', '-') + "/" + ddlstate.SelectedItem.Text.Replace(' ', '-') + "/" + ddlcity.SelectedItem.Text.Replace(' ', '-') + "/" + ddlarea.SelectedItem.Text.Replace(' ', '-') + "/any/" + datatable.Rows[area_no]["name"].ToString().Replace(' ', '-') + "";
                                            deep_lnk_speciality_dt.Rows.Add("<a href='" + act_url + "' class='foot-azam' target='_blank' title='" + datatable.Rows[area_no]["name"].ToString() + " Partner in " + city_name + "' >" + datatable.Rows[area_no]["name"].ToString() + " Partner in " + city_name + "</a>");
                                        }
                                    }
                                    else
                                    {
                                        if (url.Contains("body-massage-partner"))
                                        {
                                            string act_url = Constants__.WEB_ROOT + "/massage-partner/" + ddlcountry.SelectedItem.Text.Replace(' ', '-') + "/" + ddlstate.SelectedItem.Text.Replace(' ', '-') + "/" + ddlcity.SelectedItem.Text.Replace(' ', '-') + "/All-Area/Any/" + datatable.Rows[area_no]["name"].ToString().Replace(' ', '-') + "/all";
                                            deep_lnk_speciality_dt.Rows.Add("<a href='" + act_url + "' class='foot-azam' target='_blank' title='" + datatable.Rows[area_no]["name"].ToString() + " Partner in " + city_name + "' >" + datatable.Rows[area_no]["name"].ToString() + " Partner in " + city_name + "</a>");
                                        }
                                        else
                                        {
                                            string act_url = Constants__.WEB_ROOT + "/body-massage-partner/" + ddlcountry.SelectedItem.Text.Replace(' ', '-') + "/" + ddlstate.SelectedItem.Text.Replace(' ', '-') + "/" + ddlcity.SelectedItem.Text.Replace(' ', '-') + "/All-Area/any/" + datatable.Rows[area_no]["name"].ToString().Replace(' ', '-') + "";
                                            deep_lnk_speciality_dt.Rows.Add("<a href='" + act_url + "' class='foot-azam' target='_blank' title='" + datatable.Rows[area_no]["name"].ToString() + " Partner in " + city_name + "' >" + datatable.Rows[area_no]["name"].ToString() + " Partner in " + city_name + "</a>");
                                        }
                                    }
                                    // area_no++;
                                }
                                area_no++;
                            }


                        }
                        if (city_name != "")
                        {
                            repeater_specialtiy.DataSource = deep_lnk_speciality_dt;
                            repeater_specialtiy.DataBind();
                            lbllinkspecialitytitle.Visible = true;
                            divspecialitySrpUrls.Visible = true; lbllinkspecialitytitle.Visible = true;
                            lbllinkspecialitytitle.Text = "Massage Partner in " + city_name;
                        }
                        else
                        {
                            lbllinkspecialitytitle.Visible = false;
                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message;
                var st = new System.Diagnostics.StackTrace(ex, true);
                StackFrame[] stackFrames = st.GetFrames();
                foreach (StackFrame stackFrame in stackFrames)
                {
                    Console.WriteLine(stackFrame.GetMethod().Name);   // write method name
                    BussinessEntity.ExceptionHandling._lineno = stackFrame.GetFileLineNumber();
                    BussinessEntity.ExceptionHandling._methodname = Convert.ToString(stackFrame.GetMethod().Name);
                    BussinessEntity.ExceptionHandling._pagename = Convert.ToString(Request.Url.AbsoluteUri);

                }
            }
        }

        protected string Get_Listing_Price(string payment, decimal for_year)
        {


            string amount = "00";
            if (Convert.ToDouble(payment) > 100)
            {
                //amount = (Math.Round(Convert.ToDouble(Convert.ToDecimal(payment) + (Convert.ToDecimal(payment) * for_year)) / 100d, 0) * 100).ToString("000.00");
                amount = (Convert.ToDouble(Convert.ToDecimal(payment) + (Convert.ToDecimal(payment) * for_year))).ToString("000.00");
            }
            else
            {
                amount = (Math.Round(Convert.ToDouble(Convert.ToDecimal(payment) + (Convert.ToDecimal(payment) * for_year)) / 10d, 1) * 10).ToString("00.00");

            }

            return amount;
        }
        protected void SavePromotion_AfterPaypal()
        {
            ///
            if (Session["ServiceProvider_subscription"] != null)
            {

                DataTable dt = new DataTable();
                DataColumn dc;
                dc = new DataColumn("massage_partner_sk");
                dc.DataType = System.Type.GetType("System.Int32");
                dt.Columns.Add(dc);


                dc = new DataColumn("country_sk");
                dc.DataType = System.Type.GetType("System.Int32");
                dt.Columns.Add(dc);

                dc = new DataColumn("subscription_start_date");
                dc.DataType = System.Type.GetType("System.String");
                dt.Columns.Add(dc);

                dc = new DataColumn("subscription_end_date");
                dc.DataType = System.Type.GetType("System.String");
                dt.Columns.Add(dc);


                dc = new DataColumn("one_time_price");
                dc.DataType = System.Type.GetType("System.String");
                dt.Columns.Add(dc);

                dc = new DataColumn("is_subscribed");
                dc.DataType = System.Type.GetType("System.String");
                dt.Columns.Add(dc);
                // dt = (DataTable)(Session["ServiceProvider_subscription"]);


                dc = new DataColumn("is_paid");
                dc.DataType = System.Type.GetType("System.String");
                dt.Columns.Add(dc);


                dc = new DataColumn("PaymentGateway");
                dc.DataType = System.Type.GetType("System.String");
                dt.Columns.Add(dc);


                dt = (DataTable)(Session["ServiceProvider_subscription"]);
                dt.Rows[0]["is_subscribed"] = "Y";
                dt.Rows[0]["is_paid"] = "Y";
                dt.Rows[0]["PaymentGateway"] = "Paypal";
                objbusinessmpartener.InsertmassagePartnerSubscriptionDetails(dt);
                //  lblSuccess.InnerText = "Information saved successfully";
                // lblSuccess.Style.Add("color", "green");
                //  Session["ServiceProvider_subscription"] = null;
                Session["seeker_subscribed"] = "Y";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "xyztoz", "openOffersDialog();", true);
                return;
                // Session["exists_promotion"] = "PromotionExists";
                //  ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp23", "<script type='text/javascript'>fun_noData_popup('Promotion Successfully Paid, Now upload promotion.');</script>", false);

            }
            else
            {
                // lblSuccess.InnerText = "Payment not done";
                //  lblSuccess.Style.Add("color", "red");
                Session["seeker_subscribed"] = null;

            }
        }

        protected void SavePromotion_BeforePaypal()
        {
            ///
            if (Session["seeker_subscribed"] != null)
            {

                DataTable dt = new DataTable();
                DataColumn dc;
                dc = new DataColumn("massage_partner_sk");
                dc.DataType = System.Type.GetType("System.Int32");
                dt.Columns.Add(dc);


                dc = new DataColumn("country_sk");
                dc.DataType = System.Type.GetType("System.Int32");
                dt.Columns.Add(dc);

                dc = new DataColumn("subscription_start_date");
                dc.DataType = System.Type.GetType("System.String");
                dt.Columns.Add(dc);

                dc = new DataColumn("subscription_end_date");
                dc.DataType = System.Type.GetType("System.String");
                dt.Columns.Add(dc);


                dc = new DataColumn("one_time_price");
                dc.DataType = System.Type.GetType("System.String");
                dt.Columns.Add(dc);

                dc = new DataColumn("is_subscribed");
                dc.DataType = System.Type.GetType("System.String");
                dt.Columns.Add(dc);
                //  dt = (DataTable)(Session["ServiceProvider_subscription"]);


                dc = new DataColumn("is_paid");
                dc.DataType = System.Type.GetType("System.String");
                dt.Columns.Add(dc);

                dc = new DataColumn("PaymentGateway");
                dc.DataType = System.Type.GetType("System.String");
                dt.Columns.Add(dc);



                dt = (DataTable)(Session["ServiceProvider_subscription"]);
                dt.Rows[0]["is_subscribed"] = "N";
                dt.Rows[0]["is_paid"] = "N";
                dt.Rows[0]["PaymentGateway"] = "";



                objbusinessmpartener.InsertmassagePartnerSubscriptionDetails(dt);
                //lblSuccess.InnerText = "Information saved successfully";
                //lblSuccess.Style.Add("color", "green");
                //  Session["ServiceProvider_subscription"] = null;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "openOffersDialog();", true);
                return;
                // Session["exists_promotion"] = "PromotionExists";
                //  ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp23", "<script type='text/javascript'>fun_noData_popup('Promotion Successfully Paid, Now upload promotion.');</script>", false);

            }
            else
            {
                // lblSuccess.InnerText = "Payment not done";
                // lblSuccess.Style.Add("color", "red");
                Session["seeker_subscribed"] = null;

            }
        }
        private void bind_datatable(DataTable dt)
        {
            if (dt != null)
            {
                if (Request.QueryString["page"] != null)
                {
                    if (Session["page"] != null)
                    {
                        if (Session["page"].ToString() != Request.QueryString["page"].ToString())
                        {
                            Session["page"] = Request.QueryString["page"].ToString();
                            int page_n = (Convert.ToInt32(Request.QueryString["page"].ToString())) - 1;
                            UCPager1.CurrentPage = page_n;
                        }
                    }
                    else
                    {
                        int page_n = (Convert.ToInt32(Request.QueryString["page"].ToString())) - 1;
                        UCPager1.CurrentPage = page_n;
                        Session["page"] = page_n;
                    }
                }
                UCPager1.Ods = dt;
                UCPager1.ObjectControl = DataList1;
                UCPager1.PageSize = 10;
                //  int pgno = UCPager1.CurrentPage;
            }
            else
            {
                if (Request.QueryString["page"] != null)
                {
                    UCPager1.CurrentPage = (Convert.ToInt32(Request.QueryString["page"].ToString())) - 1;
                }
                UCPager1.Ods = DT;
                int pgno = UCPager1.CurrentPage;
                //UCPager1.ObjectControl = DataList1;
                //UCPager1.PageSize = 10;
            }
            //Response.Redirect(Request.RawUrl,false);


        }
        private void PopulatePager(int recordCount, int currentPage)
        {
            List<ListItem> pages = new List<ListItem>();
            int startIndex, endIndex;
            int pagerSpan = 5;

            //Calculate the Start and End Index of pages to be displayed.
            double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(PageSize));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            ViewState["pagecount"] = pageCount;
            startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
            endIndex = pageCount > pagerSpan ? pagerSpan : pageCount;
            if (currentPage > pagerSpan % 2)
            {
                if (currentPage == 2)
                {
                    endIndex = 5;
                }
                else
                {
                    endIndex = currentPage + 2;
                }
            }
            else
            {
                endIndex = (pagerSpan - currentPage) + 1;
            }

            if (endIndex - (pagerSpan - 1) > startIndex)
            {
                startIndex = endIndex - (pagerSpan - 1);
            }

            if (endIndex > pageCount)
            {
                endIndex = pageCount;
                startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
            }

            //Add the First Page Button.
            //if (currentPage > 1)
            //{
            //    pages.Add(new ListItem("First", "0"));
            //}

            //Add the Previous Button.
            //if (currentPage > 1)
            //{
            //    pages.Add(new ListItem("<<", (currentPage - 1).ToString()));
            //}

            for (int i = startIndex; i <= endIndex; i++)
            {
                pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
            }

            //Add the Next Button.
            //if (currentPage < pageCount)
            //{
            //    pages.Add(new ListItem(">>", (currentPage + 1).ToString()));
            //}

            //Add the Last Button.
            //if (currentPage != pageCount)
            //{
            //    pages.Add(new ListItem("Last", pageCount.ToString()));
            //}
            //  rptPager.DataSource = pages;
            //    rptPager.DataBind();
        }
        public void GetIP()
        {
            try
            {


                DataTable table_gio_location = new DataTable();
                table_gio_location = gestsiteurl.GetIP();
                if (table_gio_location.Rows.Count > 0)
                {
                    //Assign reciving ip information to properties
                    Uip = table_gio_location.Rows[0]["countryCode"].ToString();
                    Ustate = table_gio_location.Rows[0]["regionName"].ToString();
                    Ucity = table_gio_location.Rows[0]["city"].ToString();
                    Ustate = Ustate.Replace("'", "''");
                    Ucity = Ucity.Replace("'", "''");
                    Lat = table_gio_location.Rows[0]["lat"].ToString();
                    Lag = table_gio_location.Rows[0]["lon"].ToString();
                }




            }
            catch
            {
                return;
            }
        }
        public void getLocalTime(string lat, string log, string ip, int country_sk, int st_sk, int city_sk)
        {
            DataSet ds = new DataSet();
            try
            {
                //get time zone for provider
                ReadWriteWebservice obj = new ReadWriteWebservice();
                //call webservice using class   
                string xml = obj.getLotLong(lat, log);
                System.IO.StringReader sr = new System.IO.StringReader(xml);
                //Read XML File 
                ds.ReadXml(sr);
                if (ds != null)
                {
                    DateTime date;
                    if (ds.Tables[0].TableName == "timezone")
                        date = Convert.ToDateTime(ds.Tables[0].Rows[0]["time"].ToString());

                    else
                        date = System.DateTime.Now;
                    //objBusinessSearch.InsertHiCounter(date, ip, country_sk, st_sk, city_sk);
                    ViewState["state_sk"] = st_sk;
                    ViewState["cntry_sk"] = country_sk;
                    ViewState["city_sk"] = city_sk;
                    //if (country_sk != null)
                    //    top10Links_country(country_sk);

                    //if (country_sk != null && st_sk != null && city_sk != null)
                    //    top10Links(country_sk, st_sk, city_sk);
                    //create session var for hit counter table used on master page.
                    Session["local_date_hit"] = date;
                    Session["local_date_ip"] = ip;
                }
            }
            catch (System.Exception ex)
            {
                BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message;

                // Get stack trace for the exception with source file information
                var st = new System.Diagnostics.StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(1);
                BussinessEntity.ExceptionHandling._lineno = frame.GetFileLineNumber();
                BussinessEntity.ExceptionHandling._methodname = Convert.ToString(frame.GetMethod());
                BussinessEntity.ExceptionHandling._pagename = Convert.ToString(frame.GetFileName());

                Response.Redirect(Constants__.WEB_ROOT + "/ErrorMessage.aspx", false);
                return;
            }

        }
        private void getCountryByGeoIP()
        {
            DataSet ds = new DataSet();
            ds = objRegistrationBusiness.getCountryCity();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlcountry.DataSource = ds.Tables[0];
                ddlcountry.DataValueField = "country_sk";
                ddlcountry.DataTextField = "country_name";
                ddlcountry.DataBind();
                ddlcountry.Items.Insert(0, new ListItem("Country", "0"));
                //  FillGeoLocation(ds);
            }
        }
        //protected void FillGeoLocation(DataSet ds)
        //{
        //    //Set Geo location for the user who currently loggin

        //    DataRow[] dr;

        //    // dr = ds.Tables[0].Select("country_code='" + Uip + "'");
        //    dr = ds.Tables[0].Select("country_code='IN'");
        //    foreach (DataRow row in dr)
        //    {
        //        // ddlCountry.SelectedValue = row[0].ToString();
        //        //Fill Cities for seeker
        //        DataTable dt12 = new DataTable();
        //        //Convert.ToInt32(ddlCountry.SelectedValue)
        //        dt12 = objbusinessjob.getCityProvider1(Convert.ToInt32(row[0].ToString())).Tables[0];
        //        //dt12 =objbusinessjob.getCityProvider1(100).Tables[0];
        //        ViewState["cities"] = dt12;
        //        ddlcity.DataSource = dt12;
        //        ddlcity.DataValueField = "city_sk";
        //        ddlcity.DataTextField = "city_name";
        //        ddlcity.DataBind();
        //        ddlcity.Enabled = true;
        //        //   dvstate.Visible = true;
        //        ddlcity.Items.Insert(0, new ListItem("---All Cities---", "0"));


        //    }
        //}
        private void fillDropdown()
        {
            try
            {
                DataSet ds = new DataSet();


                string mode = "M";
                ds = objBusinessSearch.getSpaType(mode);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    ddlcountry.DataSource = ds.Tables[0];
                    ddlcountry.DataValueField = "country_sk";
                    ddlcountry.DataTextField = "country_name";
                    ddlcountry.DataBind();
                }
                // 224	9	157

                DataRow[] dr;
                dr = ds.Tables[0].Select("country_code='" + Uip + "'");
                int cntry_sk = 0;
                int stt_sk = 0;
                int city_sk = 0;
                ddlcity.Enabled = false;
                //  ddlsArea.Enabled = false;
                ddlstate.Enabled = false;
                foreach (DataRow row in dr)
                {

                    ddlcountry.SelectedValue = row[0].ToString();
                    ddlcountry.ToolTip = ddlcountry.SelectedItem.Text;


                    //fill state                   
                    ds = objRegistrationBusiness.getStateProvider(Convert.ToInt32(ddlcountry.SelectedValue));
                    cntry_sk = Convert.ToInt32(ddlcountry.SelectedValue);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlstate.DataSource = ds.Tables[0];
                        ddlstate.DataValueField = "state_sk";
                        ddlstate.DataTextField = "state_name";
                        ddlstate.DataBind();
                        ddlstate.Enabled = true;
                        //   dvstate.Visible = true;
                        ddlstate.Items.Insert(0, new ListItem("All States", "0"));

                        dr = ds.Tables[0].Select("state_name='" + Ustate + "'");
                        foreach (DataRow sts in dr)
                        {
                            ddlstate.SelectedValue = sts[1].ToString();
                            ddlstate.ToolTip = ddlstate.SelectedItem.Text;
                            stt_sk = Convert.ToInt32(ddlstate.SelectedValue);

                            //Fill City

                            ds = objRegistrationBusiness.getCityProvider(Convert.ToInt32(ddlstate.SelectedValue), Convert.ToInt32(ddlcountry.SelectedValue));
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                if (ddlcountry.SelectedIndex != 0)
                                {
                                    if (ddlstate.SelectedIndex != 0)
                                    {
                                        ddlcity.DataSource = ds.Tables[0];
                                        ddlcity.DataValueField = "city_sk";
                                        ddlcity.DataTextField = "city_name_display";
                                        ddlcity.DataBind();
                                        ddlcity.Items.Insert(0, new ListItem("All Cities", "0"));
                                        ddlcity.Enabled = true;
                                        if (Ucity.Contains("'"))
                                            Ucity = Ucity.Replace("'", "''");

                                        dr = ds.Tables[0].Select("city_name='" + Ucity + "'");
                                        foreach (DataRow ct in dr)
                                        {
                                            ddlcity.SelectedValue = ct[2].ToString();
                                            city_sk = Convert.ToInt32(ddlcity.SelectedValue);
                                            ddlcity.ToolTip = ddlcity.SelectedItem.Text;
                                        }
                                    }
                                }
                            }
                        }

                        //-------------------------------------------------------------------------

                        // getLocalTime(Lat, Lag, HttpContext.Current.Request.UserHostAddress.ToString(), cntry_sk, stt_sk, city_sk);


                    }
                    else
                    {
                        ddlstate.Items.Clear();
                        ddlcity.Items.Clear();
                        ddlstate.Enabled = false;
                        ddlcity.Enabled = false;

                    }
                }
                if (dr.Count() <= 0)
                {
                    Filldefaultstate_city();

                }
                city_changed();

            }
            catch (System.Exception ex)
            {
                BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message;
                var st = new System.Diagnostics.StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(1);
                BussinessEntity.ExceptionHandling._lineno = frame.GetFileLineNumber();
                BussinessEntity.ExceptionHandling._methodname = Convert.ToString(frame.GetMethod());
                BussinessEntity.ExceptionHandling._pagename = Convert.ToString(frame.GetFileName());
                Response.Redirect(Constants__.WEB_ROOT + "/ErrorMessage.aspx", false);
                return;
            }


        }
        private void Filldefaultstate_city()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = objRegistrationBusiness.getStateProvider(Convert.ToInt32(ddlcountry.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ddlcountry.SelectedIndex >= 0)
                    {
                        ddlstate.DataSource = ds.Tables[0];
                        ddlstate.DataValueField = "state_sk";
                        ddlstate.DataTextField = "state_name";
                        ddlstate.DataBind();
                        ddlstate.Items.Insert(0, new ListItem("All States", "0"));
                        // ddlCity.Enabled = true;
                        ddlstate.Enabled = true;
                        //dvstate.Visible = true;
                        //dvArea.Visible = true;
                        //dvcity.Visible = true;

                        ds = objRegistrationBusiness.getCityProvider(Convert.ToInt32(ddlstate.SelectedValue), Convert.ToInt32(ddlcountry.SelectedValue));
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ddlcountry.SelectedIndex != 0)
                            {
                                if (ddlstate.SelectedIndex != 0)
                                {
                                    ddlcity.DataSource = ds.Tables[0];
                                    ddlcity.DataValueField = "city_sk";
                                    ddlcity.DataTextField = "city_name_display";
                                    ddlcity.DataBind();
                                    ddlcity.Items.Insert(0, new ListItem("All Cities", "0"));
                                    ddlcity.Enabled = true;
                                    if (Ucity.Contains("'"))
                                        Ucity = Ucity.Replace("'", "''");
                                    DataRow[] dr = ds.Tables[0].Select("city_name='" + Ucity + "'");
                                    foreach (DataRow ct in dr)
                                    {
                                        ddlcity.SelectedValue = ct[2].ToString();
                                    }
                                    if (ddlcity.SelectedIndex > 0)
                                        ddlcity.ToolTip = ddlcity.SelectedItem.Text;
                                    else
                                        ddlcity.ToolTip = "";
                                }
                            }
                        }
                        ddlcountry.ToolTip = ddlcountry.SelectedItem.Text;
                        if (ddlstate.SelectedIndex > 0)
                            ddlstate.ToolTip = ddlstate.SelectedItem.Text;
                        else
                            ddlstate.ToolTip = "";
                    }
                    else
                    {
                        ddlstate.Items.Clear();
                        ddlcity.Items.Clear();
                        ddlstate.Enabled = false;
                        ddlcity.Enabled = false;
                    }
                }
                else
                {
                    ddlcity.Items.Clear();
                    ddlstate.Items.Clear();
                    ddlstate.Enabled = false;
                    ddlcity.Enabled = false;
                }
            }
            catch (System.Exception ex)
            {
                BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message;
                var st = new System.Diagnostics.StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(1);
                BussinessEntity.ExceptionHandling._lineno = frame.GetFileLineNumber();
                BussinessEntity.ExceptionHandling._methodname = Convert.ToString(frame.GetMethod());
                BussinessEntity.ExceptionHandling._pagename = Convert.ToString(frame.GetFileName());
                Response.Redirect(Constants__.WEB_ROOT + "/ErrorMessage.aspx", false);
                return;
            }
        }
        private void fill_dropdowns()
        {
            DataSet ds;
            //Fill therapies
            ds = objBusinessSearch.getSpaType("M");
            if (ds.Tables[1].Rows.Count > 0)
            {
                ddlmassagetypes.DataSource = ds.Tables[1];
                ViewState["Specilization"] = ds.Tables[1];
                ddlmassagetypes.DataValueField = "sub_service_sk";
                ddlmassagetypes.DataTextField = "sub_service_name";
                ddlmassagetypes.DataBind();
                ddlmassagetypes.Enabled = true;
                ddlmassagetypes.Items.Insert(0, new ListItem("Select Massage Type", "0"));
            }
            ds = objbusinessmpartener.get_agegroup();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlage.DataSource = ds.Tables[0];
                ddlage.DataTextField = "age_group_name";
                ddlage.DataValueField = "age_group_sk";
                ddlage.DataBind();
                ddlage.SelectedValue = "0";
                ddlage.Items.Insert(0, new ListItem("Age Group", "0"));
            }
        }

        #endregion
        #region Events
        protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlstate.Enabled = true;
                ddlcity.ToolTip = "";
                if (Convert.ToInt32(ddlcountry.SelectedValue) > 0)
                {
                    ViewState["cntry_sk"] = ddlcountry.SelectedValue;
                    Session["selected_countryItem"] = ddlcountry.SelectedItem;
                    Session["selected_stateItem"] = null;
                    Session["selected_cityItem"] = null;

                }

                DataSet ds = new DataSet();
                ds = objRegistrationBusiness.getStateProvider(Convert.ToInt32(ddlcountry.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ddlcountry.SelectedIndex >= 0)
                    {
                        ddlstate.Items.Clear();

                        ddlstate.DataSource = ds.Tables[0];
                        ddlstate.DataValueField = "state_sk";
                        ddlstate.DataTextField = "state_name";
                        ddlstate.DataBind();
                        ddlstate.Items.Insert(0, new ListItem("All States", "0"));
                        // ddlCity.Enabled = true;
                        ddlstate.Enabled = true;
                        ddlcity.Items.Clear();
                        if (ddlstate.SelectedIndex > 0)
                            ddlstate.ToolTip = ddlstate.SelectedItem.Text;
                        else
                            ddlstate.ToolTip = "";
                    }
                    else
                    {
                        ddlstate.Items.Clear();
                        ddlcity.Items.Clear();
                        ddlstate.Enabled = false;
                        ddlcity.Enabled = false;
                        ddlstate.ToolTip = "";
                    }
                }
                else
                {
                    ViewState.Remove("state_sk");
                    ViewState.Remove("city_sk");
                    ddlcity.Items.Clear();
                    ddlstate.Items.Clear();
                    ddlstate.Enabled = false;
                    ddlcity.Enabled = false;

                    if (ddlstate.SelectedIndex > 0)
                        ddlstate.ToolTip = ddlstate.SelectedItem.Text;
                    else
                        ddlstate.ToolTip = "";


                }
                ddlcountry.ToolTip = ddlcountry.SelectedItem.Text;
                // BindMe();
                bind_area();


            }
            catch (System.Exception ex)
            {
                BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message;
                var st = new System.Diagnostics.StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(1);
                BussinessEntity.ExceptionHandling._lineno = frame.GetFileLineNumber();
                BussinessEntity.ExceptionHandling._methodname = Convert.ToString(frame.GetMethod());
                BussinessEntity.ExceptionHandling._pagename = Convert.ToString(frame.GetFileName());

            }
        }

        void country_changed()
        {
            try
            {
                ddlstate.Enabled = true;
                ddlcity.ToolTip = "";
                if (Convert.ToInt32(ddlcountry.SelectedValue) > 0)
                {
                    ViewState["cntry_sk"] = ddlcountry.SelectedValue;
                    Session["selected_countryItem"] = ddlcountry.SelectedItem;
                    Session["selected_stateItem"] = null;
                    Session["selected_cityItem"] = null;

                }

                DataSet ds = new DataSet();
                ds = objRegistrationBusiness.getStateProvider(Convert.ToInt32(ddlcountry.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ddlcountry.SelectedIndex >= 0)
                    {
                        ddlstate.Items.Clear();

                        ddlstate.DataSource = ds.Tables[0];
                        ddlstate.DataValueField = "state_sk";
                        ddlstate.DataTextField = "state_name";
                        ddlstate.DataBind();
                        ddlstate.Items.Insert(0, new ListItem("All States", "0"));
                        // ddlCity.Enabled = true;
                        ddlstate.Enabled = true;
                        ddlcity.Items.Clear();
                        if (ddlstate.SelectedIndex > 0)
                            ddlstate.ToolTip = ddlstate.SelectedItem.Text;
                        else
                            ddlstate.ToolTip = "";
                    }
                    else
                    {
                        ddlstate.Items.Clear();
                        ddlcity.Items.Clear();
                        ddlstate.Enabled = false;
                        ddlcity.Enabled = false;
                        ddlstate.ToolTip = "";
                    }
                }
                else
                {
                    ViewState.Remove("state_sk");
                    ViewState.Remove("city_sk");
                    ddlcity.Items.Clear();
                    ddlstate.Items.Clear();
                    ddlstate.Enabled = false;
                    ddlcity.Enabled = false;

                    if (ddlstate.SelectedIndex > 0)
                        ddlstate.ToolTip = ddlstate.SelectedItem.Text;
                    else
                        ddlstate.ToolTip = "";


                }
                ddlcountry.ToolTip = ddlcountry.SelectedItem.Text;
                // BindMe();
                bind_area();


            }
            catch (System.Exception ex)
            {
                BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message;
                var st = new System.Diagnostics.StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(1);
                BussinessEntity.ExceptionHandling._lineno = frame.GetFileLineNumber();
                BussinessEntity.ExceptionHandling._methodname = Convert.ToString(frame.GetMethod());
                BussinessEntity.ExceptionHandling._pagename = Convert.ToString(frame.GetFileName());

            }
        }

        void state_changed()
        {
            try
            {
                ddlcity.ToolTip = "";
                if (ddlstate.SelectedIndex != 0)
                {
                    Session["selected_countryItem"] = ddlcountry.SelectedItem;
                    Session["selected_stateItem"] = ddlstate.SelectedItem;
                    Session["selected_cityItem"] = null;
                    //  link1.Visible = false;
                    ViewState["state_sk"] = ddlstate.SelectedValue;
                    DataSet ds = new DataSet();
                    ds = objRegistrationBusiness.getCityProvider(Convert.ToInt32(ddlstate.SelectedValue), Convert.ToInt32(ddlcountry.SelectedValue));
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        if (ddlstate.SelectedIndex != 0)
                        {
                            ddlcity.DataSource = ds.Tables[0];
                            ddlcity.DataValueField = "city_sk";
                            ddlcity.DataTextField = "city_name_display";
                            ddlcity.DataBind();
                            ddlcity.Items.Insert(0, new ListItem("All Cities", "0"));
                            ddlcity.Enabled = true;
                        }
                        else
                        {
                            ddlcity.SelectedIndex = 0;
                            ddlcity.Enabled = false;
                        }
                    }
                    else
                    {
                        ddlcity.Items.Clear();
                        ddlcity.Enabled = false;

                    }
                    if (ddlcity.SelectedIndex > 0)
                        ddlcity.ToolTip = ddlcity.SelectedItem.Text;
                    else
                        ddlcity.ToolTip = "";
                }
                else
                {
                    Session["selected_countryItem"] = ddlcountry.SelectedItem;
                    Session["selected_stateItem"] = null;
                    Session["selected_cityItem"] = null;
                    ddlcity.Items.Clear();
                    ddlcity.Enabled = false;

                }
                if (ddlstate.SelectedIndex > 0)
                    ddlstate.ToolTip = ddlstate.SelectedItem.Text;

                ddlcity_SelectedIndexChanged(null, null);

            }
            catch (System.Exception ex)
            {
                BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message;
                var st = new System.Diagnostics.StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(1);
                BussinessEntity.ExceptionHandling._lineno = frame.GetFileLineNumber();
                BussinessEntity.ExceptionHandling._methodname = Convert.ToString(frame.GetMethod());
                BussinessEntity.ExceptionHandling._pagename = Convert.ToString(frame.GetFileName());

            }
        }

        protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlcity.ToolTip = "";
                if (ddlstate.SelectedIndex != 0)
                {
                    Session["selected_countryItem"] = ddlcountry.SelectedItem;
                    Session["selected_stateItem"] = ddlstate.SelectedItem;
                    Session["selected_cityItem"] = null;
                    //  link1.Visible = false;
                    ViewState["state_sk"] = ddlstate.SelectedValue;
                    DataSet ds = new DataSet();
                    ds = objRegistrationBusiness.getCityProvider(Convert.ToInt32(ddlstate.SelectedValue), Convert.ToInt32(ddlcountry.SelectedValue));
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        if (ddlstate.SelectedIndex != 0)
                        {
                            ddlcity.DataSource = ds.Tables[0];
                            ddlcity.DataValueField = "city_sk";
                            ddlcity.DataTextField = "city_name_display";
                            ddlcity.DataBind();
                            ddlcity.Items.Insert(0, new ListItem("All Cities", "0"));
                            ddlcity.Enabled = true;
                        }
                        else
                        {
                            ddlcity.SelectedIndex = 0;
                            ddlcity.Enabled = false;
                        }
                    }
                    else
                    {
                        ddlcity.Items.Clear();
                        ddlcity.Enabled = false;

                    }
                    if (ddlcity.SelectedIndex > 0)
                        ddlcity.ToolTip = ddlcity.SelectedItem.Text;
                    else
                        ddlcity.ToolTip = "";
                }
                else
                {
                    Session["selected_countryItem"] = ddlcountry.SelectedItem;
                    Session["selected_stateItem"] = null;
                    Session["selected_cityItem"] = null;
                    ddlcity.Items.Clear();
                    ddlcity.Enabled = false;

                }
                if (ddlstate.SelectedIndex > 0)
                    ddlstate.ToolTip = ddlstate.SelectedItem.Text;

                ddlcity_SelectedIndexChanged(null, null);

            }
            catch (System.Exception ex)
            {
                BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message;
                var st = new System.Diagnostics.StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(1);
                BussinessEntity.ExceptionHandling._lineno = frame.GetFileLineNumber();
                BussinessEntity.ExceptionHandling._methodname = Convert.ToString(frame.GetMethod());
                BussinessEntity.ExceptionHandling._pagename = Convert.ToString(frame.GetFileName());

            }
        }
        protected void Page_Changed(object sender, EventArgs e)
        {
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            if (pageIndex == 0)
            {
                pos = 0;
                this.ViewState["vs"] = pos;
                btnsearch_Click(null, null);
            }
            else
                if (pageIndex < pos)
                {
                    pos = (int)this.ViewState["vs"];
                    pos -= 1;
                    this.ViewState["vs"] = pos;
                    btnsearch_Click(null, null);
                }
                else if (pageIndex > pos)
                {
                    pos = (int)this.ViewState["vs"];
                    pos += 1;
                    this.ViewState["vs"] = pos;
                    btnsearch_Click(null, null);
                }
                else if (pageIndex == Convert.ToInt32(ViewState["pagecount"].ToString()))
                { pos = adsource.PageCount - 1; btnsearch_Click(null, null); }

        }
        #endregion Events

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            //  Session["Search_data"] = null;
            Session["selected_gender"] = null;
            Session["selected_age"] = null;
            search();
            string url = Request.RawUrl;
            if (url.Contains("body-massage-partner"))
            {
                Response.Redirect(Constants__.WEB_ROOT + "/body-massage-partner" + search_url(), false);
            }
            else
            {
                Response.Redirect(Constants__.WEB_ROOT + "/massage-partner" + search_url(), false);
            }
        }
        void search()
        {
            DataSet ds = new DataSet();
            int country_sk = 0;
            int state_sk = 0;
            int city_sk = 0;
            char gender = ' ';
            int age_group_sk = 0;
            string desired_gender = " ";
            int massage_type = 0;
            int area_sk = 0;
            string from_to = "";
            string is_outcall = "";

            if (ddlcountry.SelectedIndex != 0)
            {
                country_sk = Convert.ToInt32(ddlcountry.SelectedValue);
            }
            if (ddlstate.SelectedIndex != 0 && ddlstate.SelectedIndex != -1)
                state_sk = Convert.ToInt32(ddlstate.SelectedValue);
            if (ddlcity.SelectedIndex != 0 && ddlcity.SelectedIndex != -1)
                city_sk = Convert.ToInt32(ddlcity.SelectedValue);
            if (ddlgender.SelectedIndex != 0)
            {
                gender = Convert.ToChar(ddlgender.SelectedValue);
                Session["selected_gender"] = gender;
            }
            if (ddlage.SelectedIndex != 0)
            {
                age_group_sk = Convert.ToInt32(ddlage.SelectedValue);
                Session["selected_age"] = age_group_sk;
            }
            if (ddllookingfor.SelectedIndex != 0)
                desired_gender = ddllookingfor.SelectedValue;
            if (ddlmassagetypes.SelectedIndex != 0)
                massage_type = Convert.ToInt32(ddlmassagetypes.SelectedValue);
            if (ddlarea.SelectedIndex > 0)
                area_sk = Convert.ToInt32(ddlarea.SelectedValue);
            if (ddlPartner_Types.SelectedIndex > 0)
                from_to = ddlPartner_Types.SelectedValue.ToString();
            if (ddlOutCall.SelectedIndex > 0)
                is_outcall = ddlOutCall.SelectedValue.ToString();

            string url = "";
            url = Request.RawUrl.ToString();
            if (url.Contains("body-massage-partner"))
            {
                DynamicMeta_partner_types();
            }
            else
            {

                DynamicMeta();
            }
            //if (Session["Search_data"] == null)
            //{
            ds = objbusinessmpartener.Search_Partner(country_sk, city_sk, age_group_sk, state_sk, area_sk, desired_gender, gender.ToString(), massage_type, from_to, is_outcall);
            //}
            //else
            //{
            //    ds = (DataSet)Session["Search_data"];
            //}
            //Session["Search_data"] = ds;
            // string count = ds.Tables[0].Rows.Count.ToString();
            bind_datatable(ds.Tables[0]);
        }
        void bind_area()
        {
            DataSet ds = objRegistrationBusiness.getAreaProvider(0, 0, Convert.ToInt32(ddlcountry.SelectedValue));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlarea.DataSource = ds.Tables[0];
                ddlarea.DataTextField = "area_name";
                ddlarea.DataValueField = "area_sk";
                ddlarea.DataBind();
                ddlarea.Items.Insert(0, new ListItem("All Area", "0"));
                ddlarea.Enabled = true;
            }
            else
            {
                ddlarea.Items.Clear();
                ddlarea.Enabled = false;
            }
        }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            System.Web.UI.WebControls.Image provider_image = (System.Web.UI.WebControls.Image)e.Item.FindControl("img_partner");
            ImageButton imgNorated = e.Item.FindControl("imgNoratedreview") as ImageButton;
            LinkButton lnkfav = e.Item.FindControl("lnkfav") as LinkButton;
            LinkButton lnkreport = e.Item.FindControl("lnkreport") as LinkButton;
            LinkButton lnkreport1 = e.Item.FindControl("lnkreport1") as LinkButton;
            LinkButton lnkblck = (LinkButton)e.Item.FindControl("lnkblck");
            LinkButton lnkfav1 = e.Item.FindControl("lnkfav1") as LinkButton;
            LinkButton lnkblck1 = (LinkButton)e.Item.FindControl("lnkblck1");
            HiddenField hdn_massage_partner_sk = e.Item.FindControl("hdn_massage_partner_sk") as HiddenField;
            Button btnshow = e.Item.FindControl("btnshow") as Button;
            Button btnshow1 = e.Item.FindControl("btnshow1") as Button;
            Label lblcontactno = e.Item.FindControl("lblcontact") as Label;
            Label lblmassagetype = e.Item.FindControl("lblmassagetype") as Label;
            HiddenField hdncontactnos = e.Item.FindControl("hdncontactnos") as HiddenField;
            HiddenField hdngender = e.Item.FindControl("hdngender") as HiddenField;
            Control obj_contact_div = e.Item.FindControl("divcontact_number");
            Control objDiv = e.Item.FindControl("divmassagetype");
            Control payForPartner = e.Item.FindControl("payForPartner");
            LinkButton lnkbtnsendmsg = e.Item.FindControl("lnkbtnsendsms") as LinkButton;
            LinkButton lnkbtnsendmsg1 = e.Item.FindControl("lnkbtnsendsms1") as LinkButton;
            LinkButton lnkbtnsendmsg2 = e.Item.FindControl("lnkbtnsendsms2") as LinkButton;
            Control objitem = e.Item.FindControl("tr_row") as Control;

            //Updating all massage types 
            string massage_types = "";
            DataSet ds_types = objbusinessmpartener.get_partner_details(Convert.ToInt32(hdn_massage_partner_sk.Value));
            DataSet ds1 = objBusinessSearch.getSpaType("M");
            if (ds1.Tables[1].Rows.Count > 0)
            {
                ViewState["Specilization"] = ds1.Tables[1];
            }
            if (ds_types.Tables.Count > 2)
            {
                if (ds_types.Tables[2].Rows.Count > 0)
                {
                    for (int i = 0; i < ds_types.Tables[2].Rows.Count; i++)
                    {
                        DataRow[] dr = ((DataTable)ViewState["Specilization"]).Select("sub_service_sk=" + ds_types.Tables[2].Rows[i]["sub_service_sk"].ToString());
                        if (dr.Count() > 0)
                        {
                            massage_types = massage_types == "" ? dr[0]["sub_service_name"].ToString() : massage_types + ", " + dr[0]["sub_service_name"].ToString();
                        }
                    }
                }
            }
            lblmassagetype.Text = massage_types;
            //Ends Updating all massage types 

            //Show or Hide Contact nos
            if (hdncontactnos.Value == "" || hdncontactnos.Value == "0" || hdncontactnos.Value == "NULL")
            {
                lblcontactno.Text = "NA";
            }


            if (lblmassagetype.Text == "")
                objDiv.Visible = false;
            if (Session["mp_login_sk"] != null)
            {
                if (hdnpartnersubscribed.Value == null || hdnpartnersubscribed.Value == "" || hdnpartnersubscribed.Value == "N")
                {
                    btnshow1.Visible = false;
                    btnshow.Visible = true;
                    btnshow.Enabled = true;
                    lblcontactno.Visible = false;
                    lnkbtnsendmsg1.Visible = true;
                    lnkbtnsendmsg.Visible = false;
                    lnkbtnsendmsg2.Visible = false;
                    payForPartner.Visible = false;
                    btnshow.Attributes.Add("OnClientClick", "openpopuppaypal();");
                }
                else
                {
                    DataSet ds = objbusinessmpartener.getPartnerSubsciption_record(Convert.ToInt32(hdn_massage_partner_sk.Value));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["country_sk"].ToString() == Session["country_sk"].ToString())
                        {
                            if (ds.Tables[3].Rows.Count > 0)
                            {
                                payForPartner.Visible = false;
                            }
                            else
                            {
                                payForPartner.Visible = true;
                            }
                        }
                        else
                        {
                            payForPartner.Visible = false;
                        }
                    }
                    btnshow1.Visible = false;
                    btnshow.Enabled = false;
                    btnshow.Visible = false;
                    lnkbtnsendmsg1.Visible = false;

                    lnkbtnsendmsg.Visible = true;
                    lblcontactno.Visible = true;
                    lnkbtnsendmsg2.Visible = false;
                }
            }
            else
            {
                // btnshow1.PostBackUrl = Constants__.WEB_ROOT + "/signup";
                Session["current_url"] = Request.RawUrl;
                btnshow1.Visible = true;
                btnshow.Visible = false;
                btnshow.Enabled = false;
                lblcontactno.Visible = false;
                lnkbtnsendmsg.Visible = false;
                payForPartner.Visible = false;
                lnkbtnsendmsg1.Visible = false;
                //  lnkbtnsendmsg2.PostBackUrl = Constants__.WEB_ROOT + "/signup";

            }
            if (Session["mp_login_sk"] != null)
            {
                DataSet is_blocked = new DataSet();
                is_blocked = objbusinessmpartener.get_blocked(Convert.ToInt32(Session["massage_partner_sk"].ToString()));
                if (is_blocked.Tables[0].Rows.Count > 0)
                {
                    if (is_blocked.Tables[0].Rows[0][0].ToString() == hdn_massage_partner_sk.Value)
                    {
                        objitem.Visible = false;
                    }
                }
                if (hdn_massage_partner_sk.Value == Session["massage_partner_sk"].ToString())
                {
                    objitem.Visible = false;
                }

            }
            if (Session["mp_login_sk"] != null)
            {
                lnkfav.Visible = true;
                lnkreport.Visible = true;
                lnkreport1.Visible = false;
                lnkblck.Visible = true;
                lnkfav1.Visible = false;
                lnkblck1.Visible = false;
                DataSet fav_blck_users = new DataSet();
                fav_blck_users = objbusinessmpartener.get_fav_blck_list(Convert.ToInt32(Session["massage_partner_sk"].ToString()));
                if (fav_blck_users.Tables.Count > 0)
                {
                    DataRow[] dr = fav_blck_users.Tables[0].Select("connected_massage_partner_sk=" + hdn_massage_partner_sk.Value);
                    if (dr.Count() > 0)
                    {
                        if (dr[0]["favourite_or_blocked"].ToString() == "F")
                        {
                            lnkfav.Text = "Remove from Favourite";
                            lnkblck.Enabled = false;
                            lnkblck.Style["cursor"] = "no-drop";
                        }
                        else if (dr[0]["favourite_or_blocked"].ToString() == "B")
                        {
                            lnkblck.Text = "Unblock";
                            lnkfav.Enabled = false;
                            lnkfav.Style["cursor"] = "no-drop";
                        }
                    }
                }
            }
            else
            {
                lnkreport1.Visible = true;
                lnkreport.Visible = false;
                lnkfav.Visible = false;
                lnkblck.Visible = false;
                lnkfav1.Visible = true;
                lnkblck1.Visible = true;
                Session["current_url"] = Request.RawUrl;
                // lnkfav.PostBackUrl = Constants__.WEB_ROOT + "/signup";
                // lnkblck.PostBackUrl = Constants__.WEB_ROOT + "/signup";
                // lnkfav.CommandName = "";
                //lnkblck.CommandName = "";
            }
           

            string[] filesindirectory = Directory.GetFiles(Server.MapPath("~/user/Images"));
            List<String> image = new List<string>(filesindirectory.Count());

            string[] images = (Convert.ToString(DataBinder.Eval(e.Item.DataItem, "image"))).Split(',');
            //string path = Server.MapPath("~/User/Images/img_gender/");
            //DirectoryInfo directoryInfo = new DirectoryInfo(path);
            //FileInfo[] fileInfo = directoryInfo.GetFiles();
            
            if(Session["ServiceProvider_subscription"] == null && Session["mp_login_sk"] == null)
            {
                if (hdngender.Value == "Male")
                {
                    //provider_image.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + a;
                    provider_image.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + img_id + ".jpg";
                    img_id++;
                }
                if (hdngender.Value == "Female")
                {
                    provider_image.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + img_id + ".jpg";
                    img_id++;
                }
            }
            else
            {
                string img = images[0].ToString();
                if (images.Length > 0)
                {
                    if (images[0].ToString() != "")
                        provider_image.ImageUrl = Constants__.WEB_ROOT + "/user/Images/" + images[0].ToString();
                    else
                    {
                        if (hdngender.Value == "Male")
                        {
                            provider_image.ImageUrl = Constants__.WEB_ROOT + "/image/avator-male-1501786059.png";
                        }
                        else
                        {
                            provider_image.ImageUrl = Constants__.WEB_ROOT + "/image/girl-256.png";
                        }
                    }
                }
                else
                {
                    if (hdngender.Value == "Male")
                    {
                        provider_image.ImageUrl = Constants__.WEB_ROOT + "/image/avator-male-1501786059.png";
                    }
                    else
                    {
                        provider_image.ImageUrl = Constants__.WEB_ROOT + "/image/girl-256.png";
                    }
                }
            }
           
        }


        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            Label lblname = (Label)e.Item.FindControl("lblname");
            LinkButton lnkfav = (LinkButton)e.Item.FindControl("lnkfav");
            LinkButton lnkblck = (LinkButton)e.Item.FindControl("lnkblck");
            Button btnshowcontact = (Button)e.Item.FindControl("btnshow");

            string name = lblname.Text.Replace(' ', '-');
            switch (e.CommandName)
            {
                case "image_details":
                    {
                        Session["Partner_sk"] = Convert.ToInt32(e.CommandArgument);
                        Response.Redirect(Constants__.WEB_ROOT + "/user-profile/" + name);
                        break;
                    }
                case "name_details":
                    {
                        Session["Partner_sk"] = Convert.ToInt32(e.CommandArgument);
                        Response.Redirect(Constants__.WEB_ROOT + "/user-profile/" + name);
                        break;
                    }
                case "Favourite":
                    {
                        if (lnkfav.Text == "Favourite")
                        {
                            int i = objbusinessmpartener.addto_fav_block('y', Convert.ToInt32(Session["massage_partner_sk"].ToString()), Convert.ToInt32(e.CommandArgument), 'F');
                            if (i != 0)
                            {
                                lnkfav.Text = "Remove from Favourite";
                                favourite_mail(e.CommandArgument.ToString());
                                lnkblck.Enabled = false;
                                lnkblck.Style["cursor"] = "no-drop";

                            }
                        }
                        else
                        {
                            int i = objbusinessmpartener.addto_fav_block('N', Convert.ToInt32(Session["massage_partner_sk"].ToString()), Convert.ToInt32(e.CommandArgument), 'F');
                            if (i != 0)
                            {
                                lnkfav.Text = "Favourite";
                                lnkblck.Enabled = true;
                                lnkblck.Style["cursor"] = "pointer";
                            }
                        }
                        break;
                    }
                case "Block":
                    {
                        if (lnkblck.Text == "Block")
                        {
                            int i = objbusinessmpartener.addto_fav_block('y', Convert.ToInt32(Session["massage_partner_sk"].ToString()), Convert.ToInt32(e.CommandArgument), 'B');
                            if (i != 0)
                            {
                                lnkblck.Text = "Unblock";
                                lnkfav.Enabled = false;
                                lnkfav.Style["cursor"] = "no-drop";

                            }
                        }
                        else
                        {
                            int i = objbusinessmpartener.addto_fav_block('N', Convert.ToInt32(Session["massage_partner_sk"].ToString()), Convert.ToInt32(e.CommandArgument), 'F');
                            if (i != 0)
                            {
                                lnkblck.Text = "Block";
                                lnkfav.Enabled = true;
                                lnkfav.Style["cursor"] = "pointer";
                            }
                        }
                        break;
                    }
                case "show_contact":
                    {
                        //if (btnshowcontact.Visible == true)
                        //{
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>openpopuppaypal();</script>", false);
                        //}
                        break;
                    }
                case "Payforpartner":
                    {
                        Session["pay_partner"] = e.CommandArgument;
                        hdnpayforpartner.Value = "yes";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "openpopuppaypal();", true);
                        break;
                    }
            }
        }

        protected void btnsend_Click(object sender, EventArgs e)
        {
            int c = 0;
            if(Session["counter"]!=null)
            {
                c = Convert.ToInt32(Session["counter"].ToString());
                c += 1;
                Session["counter"] = c;
            }
            else
            {
                Session["counter"] = 1;
                c = 1;
            }
            if (c <= 5)
            {
                int i = objbusinessmpartener.insert_messages(Convert.ToInt32(Session["massage_partner_sk"].ToString()), Convert.ToInt32(hdnto.Value), txtmessage.Text);
                if (i != 0)
                {
                    Send_message_copy();
                    lblmsg.Visible = true;
                    txtmessage.Text = string.Empty;
                    txtmessage.Focus();
                }
                else
                {
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "message sending failure! Try again.";
                }
            }
        }

        private void Send_message_copy()
        {
            try
            {
                DataSet ds = objbusinessmpartener.get_partner_details(Convert.ToInt32(Session["massage_partner_sk"].ToString()));
                DataSet ds1 = objbusinessmpartener.get_partner_details(Convert.ToInt32(hdnto.Value));

                BussinessSendMail objmail = new BussinessSendMail();
                //string EncrMail = Cryptology.Encrypt(hdnto.Value);
                //string filePath = Constants__.WEB_ROOT + "/User/messages.aspx?Msk=" + EncrMail;
                //string senderDetail = "<br /><br /><b>Sender Detail</b> <br />" + name.Value + "<br />" + company.Value + " " + country.Value + "<br /> " + phone.Value;
                objmail.sender = ConfigurationManager.AppSettings["EmailTable2Book"].ToString();
                String body = "Dear " + (ds1.Tables[0].Rows[0]["massage_partner_name"].ToString()) + ",<br/><br/>You have just received message at <a href='https://www.mymassagepartner.com' target='_blank'><span style='color: #FF0000;'>My</span><span style='color: #000000;'>MassagePartner.com</span></a>.<br/>Please do check and reply. It’s time to have body massage with your massage partner.<br/><br/>Your login credentials are provided below for your convenience.<br/><br/>Login at: <span style='color:blue'><u>https://www.mymassagepartner.com/login</u></span><br/>Your User ID: <span style='color:blue'><u>$email</u></span><br/>Your Password: $password<br/><br/>For any further query or question please do contact us directly. We are here to help you 24/7.<br/><br/><b>Best Regards<br/>Jasmine - customer support<br/><span style='color: #FF0000;'>My</span><span style='color: #000000;'>MassagePartner.com</span></a> <span style='color: #000000;'>Team</span>";
                body = body.Replace("$email", ds1.Tables[1].Rows[0]["email_id"].ToString());
                body = body.Replace("$password", ds1.Tables[1].Rows[0]["password"].ToString());
                //objmail.UserType ="Massage-Partner";
                objmail.Mbody = body;
                objmail.Mrecipients = ds1.Tables[1].Rows[0]["email_id"].ToString();
                objmail.Msubject = "Massage partner sent you a message";
                int status = objmail.SendMail();
                // lblreportmsg.Text = "message sent successfully!!";
            }
            catch (System.Exception ex)
            {
                BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message;
                var st = new System.Diagnostics.StackTrace(ex, true);
                StackFrame[] stackFrames = st.GetFrames();
                foreach (StackFrame stackFrame in stackFrames)
                {
                    Console.WriteLine(stackFrame.GetMethod().Name);   // write method name
                    BussinessEntity.ExceptionHandling._lineno = stackFrame.GetFileLineNumber();
                    BussinessEntity.ExceptionHandling._methodname = Convert.ToString(stackFrame.GetMethod().Name);
                    BussinessEntity.ExceptionHandling._pagename = Convert.ToString(stackFrame.GetFileName());

                }

                Response.Redirect(Constants__.WEB_ROOT + "/ErrorMessage.aspx", false);
                return;
            }
        }

        private void favourite_mail(string to_sk)
        {
            try
            {
                // DataSet ds = objbusinessmpartener.get_partner_details(Convert.ToInt32(Session["massage_partner_sk"].ToString()));
                DataSet ds1 = objbusinessmpartener.get_partner_details(Convert.ToInt32(to_sk));

                BussinessSendMail objmail = new BussinessSendMail();
                //string EncrMail = Cryptology.Encrypt(hdnto.Value);
                //string filePath = Constants__.WEB_ROOT + "/User/messages.aspx?Msk=" + EncrMail;
                //string senderDetail = "<br /><br /><b>Sender Detail</b> <br />" + name.Value + "<br />" + company.Value + " " + country.Value + "<br /> " + phone.Value;

                objmail.sender = ConfigurationManager.AppSettings["EmailTable2Book"].ToString();
                String body = "Dear " + (ds1.Tables[0].Rows[0]["massage_partner_name"].ToString()) + ",<br/><br/>Congratulations!<br/><br/><span style='color: #FF0000;'>My</span><span style='color: #000000;'>MassagePartner</span> user selected you as a favourite at <a href='https://www.mymassagepartner.com' target='_blank'><span style='color: #FF0000;'>My</span><span style='color: #000000;'>MassagePartner.com</span></a>. Let's start chat and have body massage session.<br/><br/>Your login credentials are provided below for your convenience.<br/><br/>Login at: <span style='color:blue'><u>https://www.mymassagepartner.com/login</u></span><br/>Your User ID: <span style='color:blue'><u>$email</u></span><br/>Your Password: $password<br/><br/>Hope you will find <a href='https://www.mymassagepartner.com' target='_blank'><span style='color: #FF0000;'>My</span><span style='color: #000000;'>MassagePartner.com</span></a> great.<br/><br/><b>Best Regards<br/>Jasmine - customer support<br/><span style='color: #FF0000;'>My</span><span style='color: #000000;'>MassagePartner.com</span></a> <span style='color: #000000;'>Team</span>";

                body = body.Replace("$email", ds1.Tables[1].Rows[0]["email_id"].ToString());
                body = body.Replace("$password", ds1.Tables[1].Rows[0]["password"].ToString());

                objmail.Mbody = body;// +senderDetail;
                //objmail.UserType ="Massage-Partner";
                objmail.Mrecipients = ds1.Tables[1].Rows[0]["email_id"].ToString();
                objmail.Msubject = "Massage partner selected you favourite";
                int status = objmail.SendMail();
                // lblreportmsg.Text = "message sent successfully!!";
            }
            catch (System.Exception ex)
            {
                BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message;
                var st = new System.Diagnostics.StackTrace(ex, true);
                StackFrame[] stackFrames = st.GetFrames();
                foreach (StackFrame stackFrame in stackFrames)
                {
                    Console.WriteLine(stackFrame.GetMethod().Name);   // write method name
                    BussinessEntity.ExceptionHandling._lineno = stackFrame.GetFileLineNumber();
                    BussinessEntity.ExceptionHandling._methodname = Convert.ToString(stackFrame.GetMethod().Name);
                    BussinessEntity.ExceptionHandling._pagename = Convert.ToString(stackFrame.GetFileName());

                }
                Response.Redirect(Constants__.WEB_ROOT + "/ErrorMessage.aspx", false);
                return;

            }
        }


        protected void btnreport_Click(object sender, EventArgs e)
        {
            try
            {

                BussinessSendMail objmail = new BussinessSendMail();
                objmail.sender = Convert.ToString((Session["email_id"].ToString()));
                objmail.Mbody = Convert.ToString(txtreport.Text);// +senderDetail;
                objmail.UserType = hdnname.Value;
                objmail.Mrecipients = ConfigurationManager.AppSettings["EmailTable2Book"].ToString();
                objmail.Msubject = "Massage-Partner Report Abuse";
                int status = objmail.SendMail_Contactus();
                lblreportmsg.Visible = true;
                txtreport.Text = string.Empty;
            }
            catch (System.Exception ex)
            {
                BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message;
                var st = new System.Diagnostics.StackTrace(ex, true);
                StackFrame[] stackFrames = st.GetFrames();
                foreach (StackFrame stackFrame in stackFrames)
                {
                    Console.WriteLine(stackFrame.GetMethod().Name);   // write method name
                    BussinessEntity.ExceptionHandling._lineno = stackFrame.GetFileLineNumber();
                    BussinessEntity.ExceptionHandling._methodname = Convert.ToString(stackFrame.GetMethod().Name);
                    BussinessEntity.ExceptionHandling._pagename = Convert.ToString(stackFrame.GetFileName());

                }

                Response.Redirect(Constants__.WEB_ROOT + "/ErrorMessage.aspx", false);
                return;
            }
        }
        protected void Go_for_payment(DataSet ds, string payable_amt)
        {
            try
            {
                CCACrypto chkSum = new CCACrypto();
                string WorkingKey = ConfigurationManager.AppSettings["CCAVAnueWorkingKey"].ToString();
                Random rnd = new Random();
                int rnd_no = rnd.Next(100, 100000);
                string lblOrderId = ds.Tables[0].Rows[0]["massage_partner_sk"].ToString() + "_" + rnd_no;
                string lblAmount = payable_amt;



                string lblRedirectUrl = (Request.Url).ToString();
                lblRedirectUrl = lblRedirectUrl.Replace("?PayPal=Sub_Cancel", "");
                lblRedirectUrl = lblRedirectUrl.Replace("?PayPal=Info2468Cubek", "");


                string lblCustomerName = ds.Tables[0].Rows[0]["massage_partner_name"].ToString();
                string lblCustAddr = "Not Found";
                string lblCustCountry = "India";
                string lblCustPhone = ds.Tables[0].Rows[0]["phone_nos"].ToString();
                string lblCustEmail = ds.Tables[1].Rows[0]["email_id"].ToString();
                string lblCustState = "";
                string lblCustCity = "";
                string lblZipCode = ds.Tables[0].Rows[0]["postal_code"].ToString();

                string lblRedirectUrlFail = Constants__.WEB_ROOT + "/User/CCAvanueFailPArtner.aspx";
                string lblRedirectUrlsuccess = Constants__.WEB_ROOT + "/User/CCAvanuePassPartner.aspx";

                DataTable dt = new DataTable();
                DataColumn dc;


                dc = new DataColumn("lblOrderId");
                dc.DataType = System.Type.GetType("System.String");
                dt.Columns.Add(dc);



                dc = new DataColumn("lblAmount");
                dc.DataType = System.Type.GetType("System.String");
                dt.Columns.Add(dc);


                dc = new DataColumn("lblCustomerName");
                dc.DataType = System.Type.GetType("System.String");
                dt.Columns.Add(dc);

                dc = new DataColumn("lblCustAddr");
                dc.DataType = System.Type.GetType("System.String");
                dt.Columns.Add(dc);

                dc = new DataColumn("lblCustCountry");
                dc.DataType = System.Type.GetType("System.String");
                dt.Columns.Add(dc);

                dc = new DataColumn("lblCustPhone");
                dc.DataType = System.Type.GetType("System.String");
                dt.Columns.Add(dc);


                dc = new DataColumn("lblCustEmail");
                dc.DataType = System.Type.GetType("System.String");
                dt.Columns.Add(dc);

                dc = new DataColumn("lblZipCode");
                dc.DataType = System.Type.GetType("System.String");
                dt.Columns.Add(dc);

                dc = new DataColumn("lblRedirectUrlsuccess");
                dc.DataType = System.Type.GetType("System.String");
                dt.Columns.Add(dc);

                dc = new DataColumn("lblRedirectUrlFail");
                dc.DataType = System.Type.GetType("System.String");
                dt.Columns.Add(dc);

                dt.Rows.Add(lblOrderId, lblAmount, lblCustomerName, lblCustAddr, lblCustCountry, lblCustPhone, lblCustEmail, lblZipCode, lblRedirectUrlsuccess, lblRedirectUrlFail);
                Session["CCAvanueDetails"] = dt;


                string lblCustNotes = "";
                string lblDelCustName = "";
                string lblDelCustAddr = "";
                string lblDelCustCntry = "";
                string lblDelCustTel = "";
                string lblDelCustState = "";
                string lblDelCustCity = "";
                string lblDelZipCode = "";

                string lblMerchantParam = "";
                string billingPageHeading = "";
                string lblPayType = "";

                string lblMerchantId = ConfigurationManager.AppSettings["CCAVAnuelblMerchantId"].ToString();
                string Res = chkSum.getchecksum(lblMerchantId, lblOrderId, lblAmount, lblRedirectUrl, WorkingKey);

                string ToEncrypt = "Order_Id=" + lblOrderId + "&Amount=" + lblAmount + "&Merchant_Id=" + lblMerchantId + "&Redirect_Url=" + lblRedirectUrl +
                    "&Checksum=" + Res + "&billing_cust_name=" + lblCustomerName + "&billing_cust_address=" + lblCustAddr + "&billing_cust_country=" + lblCustCountry +
                    "&billing_cust_tel=" + lblCustPhone + "&billing_cust_email=" + lblCustEmail + "&billing_cust_state=" + lblCustState +
                    "&billing_cust_city=" + lblCustCity + "&billing_zip_code=" + lblZipCode + "&billing_cust_notes=" + lblCustNotes +
                    "&delivery_cust_name=" + lblDelCustName + "&delivery_cust_address=" + lblDelCustAddr + "&delivery_cust_country=" + lblDelCustCntry +
                    "&delivery_cust_tel=" + lblDelCustTel + "&delivery_cust_state=" + lblDelCustState + "&delivery_cust_city=" + lblDelCustCity +
                    "&delivery_zip_code=" + lblDelZipCode + "&Merchant_Param=" + lblMerchantParam + "&billingPageHeading=" + billingPageHeading + "&payType=" + lblPayType;


                string Encrypted;


                Encrypted = chkSum.Encrypt(ToEncrypt, WorkingKey);

                Merchant_Id.Value = lblMerchantId;
                encRequest.Value = Encrypted;

                Session["Merchant_Id"] = lblMerchantId;
                Session["Encrypted"] = Encrypted;
                Session["Postbackurl_CCAvanueEncrypted"] = lblRedirectUrl;
                Response.Redirect(Constants__.WEB_ROOT + "/CCAvanue-Gatway-Redirection", false);
                return;
                //   ScriptManager.RegisterStartupScript(this, this.GetType(), "scriptsa", " postbackurl();", true);
            }
            catch (System.Exception ex)
            {
                BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message;
                Business.BussinessSendMail send = new BussinessSendMail();
                Response.Redirect(Constants__.WEB_ROOT + "/massage-partner", false);
                return;
                //   send.SendMail("info@massage2book.com", "support@massage2book.com", "Exception occured with webservice", ex.Message + " " + " On Subscription Page M2B, currency conversion api from  USD to '" + curr1 + "'", null);
            }

        }

        private void HandlePayPalRedirection()
        {
            try
            {
                // *** Set a flag so we know we redirected
                Session["PayPal_Redirected"] = "True";

                // *** Save any values you might need when you return here
                Session["PayPal_OrderAmount"] = OrderAmount;  // already saved above

                //			Session["PayPal_HeardFrom"] = this.txtHeardFrom.Text;
                //			Session["PayPal_ToolUsed"] = this.txtToolUsed.Text;

                PayPalHelper PayPal = new PayPalHelper();
                PayPal.PayPalBaseUrl = App.Configuration.PayPalUrl;
                PayPal.AccountEmail = App.Configuration.AccountEmail;


                decimal us_doller_amount = Convert.ToDecimal(Session["Converted_Doller_amount"]);
                PayPal.Amount = us_doller_amount;


                PayPal.LogoUrl = "http://www.massage2book.com/images/m2b_logo.PNG";

                PayPal.ItemName = "Subscription Amount #" + new Guid().GetHashCode().ToString("x");

                // *** Have paypal return back to this URL


                string url_ = (Request.Url).ToString();
                url_ = url_.Replace("?PayPal=Sub_Cancel", "");
                url_ = url_.Replace("?PayPal=Info2468Cubek", "");
                PayPal.SuccessUrl = url_ + "?PayPal=Info2468Cubek";
                PayPal.CancelUrl = url_ + "?PayPal=Sub_Cancel";
                //append currecy code  //it is comming form table.
                int country_sk = 0;
                if (Session["country_sk"].ToString() != null)
                    country_sk = Convert.ToInt32(Session["country_sk"].ToString());


                PayPal.Currency_code = obj.Get_Currency_Code(country_sk).ToString();
                string url = PayPal.GetSubmitUrl();
                Response.Redirect(url, false);
                return;
            }
            catch (System.Exception ex)
            {
                BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message;
                var st = new System.Diagnostics.StackTrace(ex, true);
                StackFrame[] stackFrames = st.GetFrames();
                foreach (StackFrame stackFrame in stackFrames)
                {
                    Console.WriteLine(stackFrame.GetMethod().Name);   // write method name
                    BussinessEntity.ExceptionHandling._lineno = stackFrame.GetFileLineNumber();
                    BussinessEntity.ExceptionHandling._methodname = Convert.ToString(stackFrame.GetMethod().Name);
                    BussinessEntity.ExceptionHandling._pagename = Convert.ToString(Request.Url.AbsoluteUri);

                }
                Response.Redirect(Constants__.WEB_ROOT + "/ErrorMessage.aspx", false);
                return;

            }
        }

        protected void SubmitToPaypal(string replaced_str)
        {
            //////////// creating session for storing details

            //SavePromotion_session();

            ///////////////
            Session["OrderAmounts"] = replaced_str; //willl come from submit button

            // decimal OrderAmount;
            //  decimal OrderAmount;
            // *** Our simplistic 'order validation'
            try
            {
                //amount comming form subcription form
                OrderAmount = Convert.ToDecimal(Session["OrderAmounts"]);
            }
            catch
            {
                this.ShowError("Invalid Order Amount. Get a grip.");
                return;
            }


            // *** Dumb ass data simulation - this should only be set once the order is Validated!
            this.Session["OrderAmount"] = Session["OrderAmounts"];


            // *** Handle PayPal Processing seperately from ProcessCard() since it requires
            // *** passing off to another page on the PayPal Site.
            // *** This request will return to this page Cancel or Success querystring
            if (!this.PayPalReturnRequest)//PP
                this.HandlePayPalRedirection(); // this will end this request!
            else
            {
                // *** CC Processing
                if (!this.ProcessCreditCard())
                    return;    // failure - display error

                // *** Write the order amount (and enything else you might need into session)
                // *** Normally you'd probably write a PK for the final invoice so you 
                // *** can reload it on the Confirmation.aspx page

                Session["PayPal_OrderAmount"] = OrderAmount;
            }
            // *** TODO:  Save your order etc.
            // *** Show the confirmation page - don't transfer so they can refresh without error
            // Response.Redirect("Confirmation.aspx");
            ///
            /////////////////

        }

        public bool ProcessCreditCard()
        {
            // bool Result = false;

            //    ccProcessing CC = null;
            //    ccProcessors CCType = App.Configuration.CCProcessor;

            //    try
            //    {
            //        // *** Figure out which type to use
            //        if (CCType == ccProcessors.AccessPoint)
            //        {
            //            CC = new ccAccessPoint();
            //        }
            //        else if (CCType == ccProcessors.AuthorizeNet)
            //        {
            //            CC = new ccAuthorizeNet();
            //            CC.MerchantPassword = App.Configuration.CCMerchantPassword;
            //        }
            //        else if (CCType == ccProcessors.PayFlowPro)
            //        {
            //            CC = new ccPayFlowPro();
            //            CC.MerchantPassword = App.Configuration.CCMerchantPassword;
            //        }
            //        else if (CCType == ccProcessors.LinkPoint)
            //        {
            //            CC = new ccLinkPoint();
            //            CC.MerchantPassword = App.Configuration.CCMerchantId;
            //            CC.CertificatePath = App.Configuration.CCCertificatePath;   // "d:\app\MyCert.pem"
            //        }


            //        //CC.UseTestTransaction = true;

            //        // *** Tell whether we do SALE or Pre-Auth
            //        CC.ProcessType = App.Configuration.CCProcessType;

            //        // *** Disable this for testing to get provider response
            //        CC.UseMod10Check = true;

            //        CC.Timeout = App.Configuration.CCConnectionTimeout;  // In Seconds
            //        CC.HttpLink = App.Configuration.CCHostUrl;			 // The host Url format will vary with provider
            //        CC.MerchantId = App.Configuration.CCMerchantId;

            //        CC.LogFile = App.Configuration.CCLogFile;
            //        CC.ReferringUrl = App.Configuration.CCReferingOrderUrl;

            //        // *** Name can be provided as a single string or as firstname and lastname
            //        CC.Name = this.txtName.Text;
            //        //CC.Firstname = Cust.Firstname.TrimEnd();
            //        //CC.Lastname = Cust.Lastname.TrimEnd();
            //        // CC.Company = Cust.Company.TrimEnd();

            //        CC.Address = this.txtAddress.Text;
            //        CC.State = this.txtState.Text;
            //        CC.City = this.txtCity.Text;
            //        CC.Zip = this.txtZip.Text;
            //        CC.Country = this.txtCountryId.SelectedValue;	// 2 Character Country ID
            //        CC.Phone = this.txtPhone.Text;
            //        CC.Email = this.txtEmail.Text;

            //        CC.OrderAmount = decimal.Parse(this.txtOrderAmount.Text);

            //        //CC.TaxAmount = Inv.Tax;					// Optional

            //        CC.CreditCardNumber = this.txtCC.Text;
            //        CC.CreditCardExpiration = this.txtCCMonth.SelectedValue + "/" + this.txtCCYear.SelectedValue;

            //        CC.SecurityCode = this.txtSecurity.Text;

            //        // *** Make this Unique
            //        //CC.OrderId = Inv.Invno.TrimEnd() + "_" + DateTime.Now.ToString();
            //        CC.Comment = "Subscription Amount # " + new Guid().GetHashCode().ToString("x");

            //        Result = CC.ValidateCard();

            //        if (!Result)
            //        {
            //            this.lblErrorMessage.Text = CC.ValidatedMessage +
            //                "<hr>" +
            //                CC.ErrorMessage;
            //        }
            //        else
            //        {
            //            // *** Should be APPROVED
            //            this.lblErrorMessage.Text = CC.ValidatedMessage;
            //        }


            //        // *** Always write out the raw response
            //        if (wwUtils.Empty(CC.RawProcessorResult))
            //        {
            //            this.lblErrorMessage.Text += "<hr>" + "Raw Results:<br>" +
            //                                         CC.RawProcessorResult;
            //        }
            //    }
            //    catch (Exception ex)
            //    {

            //        this.lblErrorMessage.Text = "FAILED<hr>" +
            //                                    "Processing Error: " + ex.Message;

            return false;
            //    }

            //    return Result;
        }

        public void ShowError(string ErrorMessage)
        {
            try
            {
                // this.lblErrorMessage.InnerText = ErrorMessage + "<p>";
            }
            catch (System.Exception ex)
            {
                BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message;
                var st = new System.Diagnostics.StackTrace(ex, true);
                StackFrame[] stackFrames = st.GetFrames();
                foreach (StackFrame stackFrame in stackFrames)
                {
                    Console.WriteLine(stackFrame.GetMethod().Name);   // write method name
                    BussinessEntity.ExceptionHandling._lineno = stackFrame.GetFileLineNumber();
                    BussinessEntity.ExceptionHandling._methodname = Convert.ToString(stackFrame.GetMethod().Name);
                    BussinessEntity.ExceptionHandling._pagename = Convert.ToString(Request.Url.AbsoluteUri);

                }

                Response.Redirect(Constants__.WEB_ROOT + "/ErrorMessage.aspx", false);
                return;
            }
        }

        protected void ddlcity_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlcountry.SelectedIndex > 0 && ddlstate.SelectedIndex > 0 && ddlcity.SelectedIndex > 0)
            //{
            DataSet ds = objRegistrationBusiness.getAreaProvider(Convert.ToInt32(ddlcity.SelectedValue), Convert.ToInt32(ddlstate.SelectedValue), Convert.ToInt32(ddlcountry.SelectedValue));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlarea.DataSource = ds.Tables[0];
                ddlarea.DataTextField = "area_name";
                ddlarea.DataValueField = "area_sk";
                ddlarea.DataBind();
                ddlarea.Items.Insert(0, new ListItem("All Area", "0"));
                ddlarea.Enabled = true;
            }
            else
            {
                ddlarea.Items.Clear();
                ddlarea.Enabled = false;
            }
            //}
            //else { ddlarea.Items.Clear(); ddlarea.Enabled = false; }
        }

        void city_changed()
        {
            if (ddlcountry.SelectedIndex > 0 && ddlstate.SelectedIndex > 0 && ddlcity.SelectedIndex > 0)
            {
                DataSet ds = objRegistrationBusiness.getAreaProvider(Convert.ToInt32(ddlcity.SelectedValue), Convert.ToInt32(ddlstate.SelectedValue), Convert.ToInt32(ddlcountry.SelectedValue));
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlarea.DataSource = ds.Tables[0];
                    ddlarea.DataTextField = "area_name";
                    ddlarea.DataValueField = "area_sk";
                    ddlarea.DataBind();
                    ddlarea.Items.Insert(0, new ListItem("All Area", "0"));
                    ddlarea.Enabled = true;
                }
                else
                {
                    ddlarea.Items.Clear();
                    ddlarea.Enabled = false;
                }
            }
            else { ddlarea.Items.Clear(); ddlarea.Enabled = false; }
        }

        protected void dlReviews_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            HiddenField hdn_rating = e.Item.FindControl("HdnRatings") as HiddenField;
            System.Web.UI.WebControls.Image img_rating1 = e.Item.FindControl("rating1") as System.Web.UI.WebControls.Image;
            System.Web.UI.WebControls.Image img_rating2 = e.Item.FindControl("rating2") as System.Web.UI.WebControls.Image;
            System.Web.UI.WebControls.Image img_rating3 = e.Item.FindControl("rating3") as System.Web.UI.WebControls.Image;
            System.Web.UI.WebControls.Image img_rating4 = e.Item.FindControl("rating4") as System.Web.UI.WebControls.Image;
            System.Web.UI.WebControls.Image img_rating5 = e.Item.FindControl("rating5") as System.Web.UI.WebControls.Image;
            if (hdn_rating.Value != "")
            {
                if (Convert.ToInt32(hdn_rating.Value) <= 5)
                { }
                else
                {
                    hdn_rating.Value = "1";
                }
                switch (Convert.ToInt32(hdn_rating.Value))
                {
                    case 1:
                        {
                            img_rating1.Visible = true;
                            img_rating1.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/Star-on.png";
                            img_rating2.Visible = false;
                            img_rating3.Visible = false;
                            img_rating4.Visible = false;
                            img_rating5.Visible = false;
                            break;

                        }
                    case 2:
                        {
                            img_rating1.Visible = true;
                            img_rating1.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/Star-on.png";
                            img_rating2.Visible = true;
                            img_rating2.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/Star-on.png";
                            img_rating3.Visible = false;
                            img_rating4.Visible = false;
                            img_rating5.Visible = false;
                            break;

                        }
                    case 3:
                        {
                            img_rating1.Visible = true;
                            img_rating1.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/Star-on.png";
                            img_rating2.Visible = true;
                            img_rating2.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/Star-on.png";
                            img_rating3.Visible = true;
                            img_rating3.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/Star-on.png";
                            img_rating4.Visible = false;
                            img_rating5.Visible = false;
                            break;

                        }
                    case 4:
                        {
                            img_rating1.Visible = true;
                            img_rating1.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/Star-on.png";
                            img_rating2.Visible = true;
                            img_rating2.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/Star-on.png";
                            img_rating3.Visible = true;
                            img_rating3.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/Star-on.png";
                            img_rating4.Visible = true;
                            img_rating4.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/Star-on.png";
                            img_rating5.Visible = false;
                            break;

                        }
                    case 5:
                        {
                            img_rating1.Visible = true;
                            img_rating1.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/Star-on.png";
                            img_rating2.Visible = true;
                            img_rating2.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/Star-on.png";
                            img_rating3.Visible = true;
                            img_rating3.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/Star-on.png";
                            img_rating4.Visible = true;
                            img_rating4.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/Star-on.png";
                            img_rating5.Visible = true;
                            img_rating5.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/Star-on.png";
                            break;

                        }
                }
            }

        }

        private void bind_revieews(int country_sk)
        {
            try
            {
                int start = 0;
                int end = 10;
                DataSet ds_reviews = objbusinessmpartener.get_reviews(country_sk, start, end);
                if (ds_reviews.Tables.Count > 0)
                {
                    dlReviews.DataSource = ds_reviews.Tables[0];
                    dlReviews.DataBind();
                    Session["ReviewDatatable"] = ds_reviews.Tables[0];
                    if (Convert.ToInt16(ds_reviews.Tables[0].Rows[0]["Total_Rows"]) <= ds_reviews.Tables[0].Rows.Count)
                    { lnkSeemoreReview.Visible = false; }
                    else { lnkSeemoreReview.Visible = true; }
                }
                else
                {
                    reviews.Visible = false;
                }
            }
            catch
            {
                reviews.Visible = false;
            }
        }
        protected void lnkSeemoreReview_Click(object sender, EventArgs e)
        {
            try
            {
                int Start_review = 0;
                int End_review = 0;
                Start_review = dlReviews.Items.Count;
                Start_review = Start_review;
                End_review = Start_review + 10;
                DataSet ds_reviews1 = objbusinessmpartener.get_reviews(Convert.ToInt32(ddlcountry.SelectedValue), Start_review, End_review);
                if (ds_reviews1.Tables.Count > 0)
                {
                    if (Session["ReviewDatatable"] != null)
                    {
                        DataTable ReviewDatatable = (DataTable)Session["ReviewDatatable"];
                        ReviewDatatable.Merge(ds_reviews1.Tables[0]);
                        dlReviews.DataSource = ReviewDatatable;
                        dlReviews.DataBind();
                        if (Convert.ToInt16(ds_reviews1.Tables[0].Rows[0]["Total_Rows"]) <= ReviewDatatable.Rows.Count)
                        { lnkSeemoreReview.Visible = false; }
                        else { lnkSeemoreReview.Visible = true; }
                    }
                    else
                    {
                        dlReviews.DataSource = ds_reviews1.Tables[0];
                        dlReviews.DataBind();
                        if (Convert.ToInt16(ds_reviews1.Tables[0].Rows[0]["Total_Rows"]) <= ds_reviews1.Tables[0].Rows.Count)
                        { lnkSeemoreReview.Visible = false; }
                        else { lnkSeemoreReview.Visible = true; }
                    }
                }
                else
                {
                    lnkSeemoreReview.Visible = false;
                }
                ds_reviews1.Clear();
                ds_reviews1.Dispose();
            }
            catch (System.Exception ex)
            {
                BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message;
                var st = new System.Diagnostics.StackTrace(ex, true);
                StackFrame[] stackFrames = st.GetFrames();
                foreach (StackFrame stackFrame in stackFrames)
                {
                    Console.WriteLine(stackFrame.GetMethod().Name);   // write method name
                    BussinessEntity.ExceptionHandling._lineno = stackFrame.GetFileLineNumber();
                    BussinessEntity.ExceptionHandling._methodname = Convert.ToString(stackFrame.GetMethod().Name);
                    BussinessEntity.ExceptionHandling._pagename = Convert.ToString(Request.Url.AbsoluteUri);

                }

                Response.Redirect(Constants__.WEB_ROOT + "/ErrorMessage.aspx", false);
            }
        }

        protected void ddlmassagetypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlmassagetypes.SelectedItem.Text == "Gay Massage")
            {
                ddlgender.SelectedValue = "M";
                ddlPartner_Types.SelectedValue = "MM";
                // ddlgender.Enabled = false;
                //ddlPartner_Types.Enabled = false;
            }
            else
                if (ddlmassagetypes.SelectedItem.Text == "Lesbian Massage")
                {
                    ddlgender.SelectedValue = "F";
                    ddlPartner_Types.SelectedValue = "FF";
                    // ddlgender.Enabled = false;
                    //ddlPartner_Types.Enabled = false;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "open_enable", "enable();", true);
                    ddlgender.Enabled = true;
                    ddlPartner_Types.Enabled = true;
                }
        }

        protected void ddlgender_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlmassagetypes.SelectedIndex = 0;
        }

        protected void ddlPartner_Types_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlmassagetypes.SelectedIndex = 0;
        }

        void bind_M2B_link(string country, string state, string city, string area)
        {
            if (city == "c")
            {
                city = "a";
            }
            if (area == "All-Area")
            {
                area = "all-area";
            }
            string url = "https://www.massage2book.com/parlor-list/" + country + "/" + state + "/" + city + "/" + area + "/female-massager-masseuse-male-massager-masseur";
            lnk_m2b_Srp1.HRef = url;
        }
    }
}
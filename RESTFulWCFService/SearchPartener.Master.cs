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
namespace RESTFulWCFService.MassagePartener
{
    public partial class SearchPartener : System.Web.UI.MasterPage
    {
        GetSiteURL gestsiteurl = new GetSiteURL();
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
        BussinessSendMail objNotification = new BussinessSendMail();
        Business.BusinessSearch objBusinessSearch = new Business.BusinessSearch();
        Business.BusinessMPartener objbusinessmpartener = new Business.BusinessMPartener();
        BussinessSendMail objmail = new BussinessSendMail();
        BussinessPaypal obj = new BussinessPaypal();
        private bool PayPalReturnRequest = false;
        protected decimal OrderAmount = 0.00M;
        Business.BusinessLogin objBusinessLogin = new BusinessLogin();
        RegistrationBusiness objRegistrationBusiness = new RegistrationBusiness();
        DataSet ds = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) - 120));
            bind_css();
            Session["currentPageUrl"] = Request.RawUrl;
            if (!IsPostBack)
            {
                if (Session["massage_partner_sk"] != null)
                {
                    ds = objbusinessmpartener.getPartnerSubsciption_record(Convert.ToInt32(Session["massage_partner_sk"].ToString()));
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        hdnpartnersubscribed.Value = "Y";
                    }
                }
                if (Session["mp_login_sk"] != null)
                {
                    txtqr_mail_id.Text = Session["email_id"].ToString();
                    txtregisteredmail_id.Text = Session["email_id"].ToString();
                    hdncountry.Value = Session["country_sk"].ToString();
                    paid_list.Visible = true;
                    lnk_signin.Visible = false;
                    lnk_signin1.Visible = false;
                    unpaid_list.Visible = false;
                    lnk_logout.Visible = true;
                    lnk_register.Visible = false;
                    user_email.Visible = false;
                    txtemail_user.Value = Session["email_id"].ToString();
                    payment_fill();
                    button_type();
                    if (Session["Is_after_login"] != null)
                    {
                        if (hdnpartnersubscribed.Value == null || hdnpartnersubscribed.Value == "" || hdnpartnersubscribed.Value == "N")
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp1sd", "<script type='text/javascript'>openpopuppaypal();</script>", false);
                        Session["Is_after_login"] = null;
                    }
                }
                else
                {
                    paid_list.Visible = false;
                    unpaid_list.Visible = true;
                    lnk_logout.Visible = false;
                    lnk_register.Visible = true;
                    lnk_mobile_membership.Visible = false;
                    // Response.Redirect(Constants__.WEB_ROOT+"/massage-partner");
                }
            }
            if (hdnpartnersubscribed.Value == null || hdnpartnersubscribed.Value == "" || hdnpartnersubscribed.Value == "N")
            {

            }
            else
            {
                lnkmembership.Visible = false;
                lnk_mobile_membership.Visible = false;
            }
            if (!Page.IsPostBack)
            {
                if (Session["mp_login_sk"] != null)
                {
                    if ((Request.QueryString["PayPal"] != null && Request.QueryString["PayPal"].ToString() == "Info2468Cubek"))
                    {
                        SavePromotion_AfterPaypal();
                        lblpaymentinfo.Style.Add("color", "green");
                        lblpaymentinfo.InnerHtml = "Payment Successfully Done!";
                        Session["seeker_subscribed"] = "Y";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script723", "WellComePopup123();", true);
                        return;
                    }
                    else if ((Request.QueryString["PayPal"] != null && Request.QueryString["PayPal"].ToString() == "Cubek2468Tech"))
                    {
                        SavePromotion_AfterPaypal();
                        lblpaymentinfo.Style.Add("color", "green");
                        lblpaymentinfo.InnerHtml = "Payment Successfully Done!";
                        Session["seeker_subscribed"] = "Y";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script723", "WellComePopup123();", true);
                        return;
                    }
                    else if ((Request.QueryString["PayPal"] != null && Request.QueryString["PayPal"].ToString() == "Sub_Cancel"))
                    {
                        lblpaymentinfo.Style.Add("color", "red");
                        lblpaymentinfo.InnerHtml = "Payment Unsuccessfull!";
                        DataTable DT = new DataTable();
                        if (Session["seeker_subscribed"] != null && Session["seeker_subscribed"].ToString() != "")
                        {
                            //DT = (DataTable)(Session["ServiceProvider_subscription"]);
                            //if (DT.Rows.Count > 0)
                            //{
                            //    int service_provider_sk = Convert.ToInt32(DT.Rows[0]["massage_partner_sk"]);
                            //    int status = objmail.send_notification_mail_Provider_annual_sub(service_provider_sk);
                            //}
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script723", "WellComePopup123();", true);
                            return;
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script723", "WellComePopup123();", true);
                        return;

                    }
                    else
                    {
                        if (Session["is_paid_partner"] != null && Session["is_paid_partner"].ToString() != "" && Session["is_paid_partner"].ToString() == "Y")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script723", "WellComePopup123();", true);
                            Session["is_paid_partner"] = null;
                            Session["seeker_subscribed"] = "Y";
                            return;
                        }
                        else { Session["is_paid_partner"] = null; }
                    }

                }
            }
        }

        #region methods

        public void update_pymntImages()
        {
            string result = objbusinessmpartener.getPymntImgs("Paytm");
            string result_qr = objbusinessmpartener.getPymntImgs("Qr");
            if (result != string.Empty)
            {
                imgIndPtm.ImageUrl = (Constants__.WEB_ROOT + "/image/Paytm/" + result);
            }
            else
            {
                imgIndPtm.ImageUrl = (Constants__.WEB_ROOT + "/image/ptm.jpg");
            }
            if (result_qr != string.Empty)
            {
                imgIndqr.ImageUrl = (Constants__.WEB_ROOT + "/image/Paytm/" + result_qr);
            }
            else
            {
                imgIndqr.ImageUrl = (Constants__.WEB_ROOT + "/image/qa.png");
            }
        }

        public void user_country_wise_payments(string country_sk)
        {
            DataSet ds = objbusinessmpartener.get_user_country_payments(country_sk);

            li_credit_or_debit_stripe.Visible = false;
            li_credit_or_debit_paypal.Visible = false;

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        if (r["payment_method"].ToString() == "33")
                        {
                            li_credit_or_debit_stripe.Visible = true;
                        }
                        if (r["payment_method"].ToString() == "32")
                        {
                            li_credit_or_debit_paypal.Visible = true;
                        }
                    }
                }
                else
                {
                    if (country_sk == "100")
                    {
                        li_credit_or_debit_stripe.Visible = true;
                        li_credit_or_debit_paypal.Visible = true;
                    }
                    else
                    {
                        li_credit_or_debit_stripe.Visible = true;
                        li_credit_or_debit_paypal.Visible = true;
                    }
                }
            }
            else
            {
                if (country_sk == "100")
                {
                    li_credit_or_debit_stripe.Visible = true;
                    li_credit_or_debit_paypal.Visible = true;
                }
                else
                {
                    li_credit_or_debit_stripe.Visible = true;
                    li_credit_or_debit_paypal.Visible = true;
                }
            }
        }

        private void payment_fill()
        {
            update_ptmLink();
            update_pymntImages();
            DataSet ds = new DataSet();
            ds = objbusinessmpartener.getPartnerSubsciption(Convert.ToInt32(hdncountry.Value));
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables.Count > 4)
                {
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        ViewState["2_year"] = ds.Tables[3].Rows[0]["two_year"].ToString();
                        ViewState["3_year"] = ds.Tables[3].Rows[0]["three_year"].ToString();
                    }
                    else
                    {
                        ViewState["2_year"] = ds.Tables[4].Rows[0]["two_year"].ToString();
                        ViewState["3_year"] = ds.Tables[4].Rows[0]["three_year"].ToString();
                    }
                }
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                ViewState["Seeker_subscrition_price"] = Math.Round(decimal.Parse(ds.Tables[1].Rows[0]["basic_unit_price"].ToString()), 2);
                ViewState["currency_short_name"] = ds.Tables[1].Rows[0]["currency_short_name"];
                //ul_for_other.Attributes.Add("style", "display:none !important");

                if (ViewState["currency_short_name"].ToString() == "INR")
                {
                    ViewState["skr_currency"] = "Rs.";
                    ul_for_other.Attributes.Add("style", "display:none !important");
                }
                else
                {
                    ul_for_india.Attributes.Add("style", "display:none !important");
                  
                    ViewState["skr_currency"] = ViewState["currency_short_name"].ToString();
                }

                price.InnerText = ViewState["currency_short_name"].ToString() + " " + Get_Listing_Price(ds.Tables[1].Rows[0]["basic_unit_price"].ToString(), 0);

                if (ConfigurationManager.AppSettings["Seeker_subscription_default_year"].ToString() == "1")
                {
                    price.InnerText = ViewState["skr_currency"].ToString() + " " + " " + Get_Listing_Price(ds.Tables[1].Rows[0]["basic_unit_price"].ToString(), 0);
                    checkboxOneYear_provider.Checked = true;
                    checkboxTwoYear_provider.Checked = false;
                    checkboxThreeYear_provider.Checked = false;
                    lblyearsub.InnerText = "1 Year";
                }
                else if (ConfigurationManager.AppSettings["Seeker_subscription_default_year"].ToString() == "2")
                {
                    price.InnerText = ViewState["skr_currency"].ToString() + " " + " " + Get_Listing_Price(ViewState["2_year"].ToString(), 0);
                    checkboxOneYear_provider.Checked = false;
                    checkboxTwoYear_provider.Checked = true;
                    checkboxThreeYear_provider.Checked = false;
                    lblyearsub.InnerText = "2 Year";

                }
                else if (ConfigurationManager.AppSettings["Seeker_subscription_default_year"].ToString() == "3")
                {
                    price.InnerText = ViewState["skr_currency"].ToString() + " " + " " + Get_Listing_Price(ViewState["3_year"].ToString(), 0);
                    checkboxOneYear_provider.Checked = false;
                    checkboxTwoYear_provider.Checked = false;
                    checkboxThreeYear_provider.Checked = true;
                    lblyearsub.InnerText = "3 Year";

                }
                ViewState["skr_currency"] = ds.Tables[1].Rows[0]["currency_short_name"];
                checkboxOneYear_provider.Text = ViewState["skr_currency"].ToString() + " " + Get_Listing_Price(ds.Tables[1].Rows[0]["basic_unit_price"].ToString(), 0);
                checkboxTwoYear_provider.Text = ViewState["skr_currency"].ToString() + " " + Get_Listing_Price(ViewState["2_year"].ToString(), 0);
                checkboxThreeYear_provider.Text = ViewState["skr_currency"].ToString() + " " + Get_Listing_Price(ViewState["3_year"].ToString(), 0);
                price.InnerText = price.InnerText.Replace(".00", "");
            }
            else
            {
                ul_for_india.Attributes.Add("style", "display:none !important");
                if (ds.Tables[2].Rows.Count > 0)
                {

                    string price1 = Get_Listing_Price(ds.Tables[2].Rows[0]["basic_unit_price"].ToString(), 0);

                    price.InnerText = "USD " + price1;

                    // price.InnerText = "USD " + ds.Tables[2].Rows[0]["basic_unit_price"].ToString();
                    ViewState["currency_short_name"] = "USD";

                    if (ConfigurationManager.AppSettings["Seeker_subscription_default_year"].ToString() == "1")
                    {
                        price.InnerText = "USD" + " " + Get_Listing_Price(ds.Tables[2].Rows[0]["basic_unit_price"].ToString(), 0);
                        checkboxOneYear_provider.Checked = true;
                        checkboxTwoYear_provider.Checked = false;
                        checkboxThreeYear_provider.Checked = false;
                        lblyearsub.InnerText = "1 Year";
                        ViewState["Seeker_subscrition_price"] = Get_Listing_Price(ds.Tables[2].Rows[0]["basic_unit_price"].ToString(), 0);

                    }
                    else if (ConfigurationManager.AppSettings["Seeker_subscription_default_year"].ToString() == "2")
                    {
                        price.InnerText = "USD" + " " + Get_Listing_Price(ds.Tables[2].Rows[0]["basic_unit_price"].ToString(), Convert.ToDecimal(ConfigurationManager.AppSettings["Provider_foreign_2_year_per_partner"].ToString()));
                        checkboxOneYear_provider.Checked = false;
                        checkboxTwoYear_provider.Checked = true;
                        checkboxThreeYear_provider.Checked = false;
                        lblyearsub.InnerText = "2 Year";
                        ViewState["Seeker_subscrition_price"] = Get_Listing_Price(ds.Tables[2].Rows[0]["basic_unit_price"].ToString(), Convert.ToDecimal(ConfigurationManager.AppSettings["Provider_foreign_2_year_per_partner"].ToString()));


                    }
                    else if (ConfigurationManager.AppSettings["Seeker_subscription_default_year"].ToString() == "3")
                    {
                        price.InnerText = "USD" + " " + Get_Listing_Price(ds.Tables[2].Rows[0]["basic_unit_price"].ToString(), Convert.ToDecimal(ConfigurationManager.AppSettings["Provider_foreign_3_year_per_partner"].ToString()));
                        checkboxOneYear_provider.Checked = false;
                        checkboxTwoYear_provider.Checked = false;
                        checkboxThreeYear_provider.Checked = true;
                        lblyearsub.InnerText = "3 Year";
                        ViewState["Seeker_subscrition_price"] = Get_Listing_Price(ds.Tables[2].Rows[0]["basic_unit_price"].ToString(), Convert.ToDecimal(ConfigurationManager.AppSettings["Provider_foreign_3_year_per_partner"].ToString()));

                    }

                    checkboxOneYear_provider.Text = "USD" + " " + Get_Listing_Price(ds.Tables[2].Rows[0]["basic_unit_price"].ToString(), 0);
                    checkboxTwoYear_provider.Text = "USD" + " " + Get_Listing_Price(ds.Tables[2].Rows[0]["basic_unit_price"].ToString(), Convert.ToDecimal(ConfigurationManager.AppSettings["Provider_foreign_2_year_per_partner"].ToString()));
                    checkboxThreeYear_provider.Text = "USD" + " " + Get_Listing_Price(ds.Tables[2].Rows[0]["basic_unit_price"].ToString(), Convert.ToDecimal(ConfigurationManager.AppSettings["Provider_foreign_3_year_per_partner"].ToString()));
                    price.InnerText = price.InnerText.Replace(".00", "");
                }
            }

            checkboxOneYear_provider.Text = checkboxOneYear_provider.Text.Replace(".00", "");
            checkboxTwoYear_provider.Text = checkboxTwoYear_provider.Text.Replace(".00", "");
            checkboxThreeYear_provider.Text = checkboxThreeYear_provider.Text.Replace(".00", "");

        }
        private void button_type()
        {
            DataSet dsButtonType = new DataSet();
            dsButtonType = objRegistrationBusiness.getPaymentButton_type(Session["country_sk"].ToString());

            if (dsButtonType.Tables.Count > 0)
            {
                //   For indian customer
                if (hdncountry.Value == "100")
                {
                    if (dsButtonType.Tables.Count > 0)
                    {
                        if ((dsButtonType.Tables[1].Rows[0]["parameter_value"].ToString()).Trim() == "M")   // Check for Indian payment gatway M- MultiPaymentGateway
                        {

                        }  //  for single payment gatway selection
                        else if ((dsButtonType.Tables[3].Rows[0]["parameter_value"].ToString()).Trim() == "P")   // Check for Indian payment gatway P-Paypal Directly, M- MultiPaymentGateway
                        {
                            user_country_wise_payments(Session["country_sk"].ToString());
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script_stripe", "change_default_selection('P');", true);
                        }
                        else
                        {
                            user_country_wise_payments(Session["country_sk"].ToString());
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script_stripe_1", "change_default_selection('S');", true);
                          

                        }
                    }
                }
                else
                {
                    // For International Customer
                    if ((dsButtonType.Tables[2].Rows[0]["parameter_value"].ToString()).Trim() == "P")   // Check for global payment gatway P->Paypal, S->Stripe
                    {   // Case for International PAYPAL case
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_stripe", "change_default_selection('P');", true);
                        user_country_wise_payments(Session["country_sk"].ToString());
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_stripe", "change_default_selection('S');", true);
                        user_country_wise_payments(Session["country_sk"].ToString());
                        // Case for International Stripe case
                    }


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
                Session["is_paid_partner"] = "Y";
                Response.Redirect(Request.RawUrl);
                // lblpaymentinfo.InnerText = "Membership Payment Successfull!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "haritscript123", "openOffersDialog();", true);
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
                // Session["seeker_subscribed"] = null;

            }
        }
        private void bind_css()
        {
            lnk_favicon.Href = Constants__.WEB_ROOT_CDN + "/img/mp.png";
            bootstrapcss.Href = Constants__.WEB_ROOT_CDN + "/bootstrap/css/bootstrap.css";
            bootstrapcss_1.Href = Constants__.WEB_ROOT_CDN + "/bootstrap/css/bootstrap.css";
            bootstrap_min.Href = Constants__.WEB_ROOT_CDN + "/bootstrap/css/bootstrap.min.css";
            bootstrap_min_1.Href = Constants__.WEB_ROOT_CDN + "/bootstrap/css/bootstrap.min.css";
            bootstraptheme_1.Href = Constants__.WEB_ROOT_CDN + "/bootstrap/css/bootstrap-theme.css";
            bootstraptheme.Href = Constants__.WEB_ROOT_CDN + "/bootstrap/css/bootstrap-theme.css";
            bootstraptheme_min.Href = Constants__.WEB_ROOT_CDN + "/bootstrap/css/bootstrap-theme.min.css";
            bootstraptheme_min_1.Href = Constants__.WEB_ROOT_CDN + "/bootstrap/css/bootstrap-theme.min.css";
            lnk_partner_popup.Href = Constants__.WEB_ROOT_CDN + "/css/payment_popup_partner.css";
            lnk_partner_popup_1.Href = Constants__.WEB_ROOT_CDN + "/css/payment_popup_partner.css";
            style.Href = Constants__.WEB_ROOT_CDN + "/css/style.css";
            style_1.Href = Constants__.WEB_ROOT_CDN + "/css/style.css";

        }
        private void Sendmail()
        {
            try
            {

                BussinessSendMail objmail = new BussinessSendMail();

                //string senderDetail = "<br /><br /><b>Sender Detail</b> <br />" + name.Value + "<br />" + company.Value + " " + country.Value + "<br /> " + phone.Value;

                objmail.sender = Convert.ToString(email.Value);
                objmail.Mbody = Convert.ToString(comments.Value);// +senderDetail;
                //objmail.UserType = Convert.ToString(ddluser.SelectedValue);
                objmail.Mrecipients = ConfigurationManager.AppSettings["EmailTable2Book"].ToString();
                if (subject.Value != null && subject.Value != "")
                    objmail.Msubject = "Massage Partner-" + Convert.ToString(subject.Value);
                else
                    objmail.Msubject = "Massage Partner-Contact US";
                int status = objmail.SendMail_Contactus();
                lblreportmsg.Text = "message sent successfully!!";
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
            }
        }
        #endregion

        protected void lnkbtnlogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect(Constants__.WEB_ROOT);
        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Sendmail();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "razz1", "reset();", true);
                //ddluser.SelectedValue = "0";
                //name.Value = "";
                //email.Value = "";
                //subject.Value = "";
                //comments.Value = "";
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
            }
        }
        protected void btninvite_Click(object sender, EventArgs e)
        {
            try
            {
                if (mailids.Value.Trim() != "")
                {
                    DataSet ds = objbusinessmpartener.get_forgot_password(txtemail_user.Value.Trim());
                    string TempalteBody = "";

                    TempalteBody = "<body style='background-color: #f4f4f4;'><center><table style='font-family: Arial,Helvetica,sans-serif;' cellpadding='1' cellspacing='1' width='680px'><tbody><tr><td style='color: White; float: left; font-size: 18px; font-weight: bold; padding-left: 4px;text-align: left; background-color: #FFF; padding-bottom: 10px; width: 686px;font-family: Arial,Helvetica,sans-serif; padding: 4px;'><table width='680px;'><tbody><tr><td style='font-size: 12px; font-family: Arial,Helvetica,sans-serif; color: #333333;margin: 10px 10px 10px 10px; line-height: 20px;'>Hi,<br /><br /><span style='font-weight: normal;'>Your friend <b>$Customer1</b> invited you at <a href='https://www.mymassagepartner.com' target='_blank'><span style='color:#FF0000'><strong>My</strong></span><span style='color:#000000'><strong>MassagePartner.com</strong></span></a> where you can find female and male partner for free body massage.<br /><br />At <a href='https://www.mymassagepartner.com' target='_blank'><span style='color:#FF0000'><strong>My</strong></span><span style='color:#000000'><strong>MassagePartner.com</strong></span></a> you will get free body massage and fun from your female and male body massage partner. It&#39;s all about free body massage and fun anywhere, anytime!<br /><br />Click &amp; check details: <meta charset='utf-8' /><a href='https://www.mymassagepartner.com' style='text-decoration:none;'><span style='font-size:10pt;font-family:Arial;color:#1155cc;background-color:transparent;font-style:normal;font-variant:normal;text-decoration:underline;vertical-align:baseline;white-space:pre-wrap;'>https://www.mymassagepartner.com</span></a><b id='docs-internal-guid-6d6155c4-a4d4-5575-a79f-e5ee041e1cde'> </b>and get ready for free body massage from your body massage partner.<b id='B1'><br /><br /></b>For any question or query please write to $email or you can read FAQs at </span> <meta charset='utf-8' /><span style='font-weight: normal;'><a href='https://www.mymassagepartner.com/faq' style='text-decoration:none;'><span style='font-size:10pt;font-family:Arial;color:#1155cc;background-color:transparent;font-style:normal;font-variant:normal;text-decoration:underline;vertical-align:baseline;white-space:pre-wrap;'><strong style='font-weight: normal;'>https://www.mymassagepartner.com/faq</strong></span></a></strong><strong></b></strong><br /></span><br />Best Regards<br />Rose-customer support<br /><span style='color:#FF0000'>My</span><span style='color:#000000'>MassagePartner.com</span><span style='color: #C6953F; font-family: Arial,Helvetica,sans-serif;'> </span><span style='font-family: Arial,Helvetica,sans-serif;'> <span style='color:#000000'><strong>Team</strong></span></span></td></tr></tbody></table></td></tr></tbody></table></center></body>";

                    int firstString = TempalteBody.IndexOf("divlogininformation") + 1 + 19;

                    //TempalteBody.Insert(firstString, "<div> User Name -   " + Convert.ToString(ds.Tables[0].Rows[0]["email_id"]) + " <br /> password - " + Convert.ToString(ds.Tables[0].Rows[0]["password"]) + "</div>");

                    //TempalteBody = (TempalteBody.Substring(0, firstString)) + "><div> Login Id-   " + Convert.ToString(ds.Tables[0].Rows[0]["email_id"]) + " <br /> password - " + Convert.ToString(ds.Tables[0].Rows[0]["password"]) + "</div>" + (TempalteBody.Substring(firstString + 2, TempalteBody.Length - (firstString + 2)));
                    try
                    {
                        TempalteBody = TempalteBody.Replace("$Customer1", ds.Tables[0].Rows[0]["Name"].ToString().ToUpper());
                    }
                    catch
                    {
                        TempalteBody = TempalteBody.Replace(" <b>$Customer1</b>", "");
                    }
                    BussinessSendMail objmail = new BussinessSendMail();

                    //TempalteBody = TempalteBody.Replace("$MP", "<a href='https://www.massage2book.com/massage-partner' target='_blank'>www.massage2book.com/massage-partner</a>");

                    TempalteBody = TempalteBody.Replace("$sitename", "MyMassagePartner.com");
                    TempalteBody = TempalteBody.Replace("$fblogo", "http://www.massage2book.com/images/face_book.png");
                    TempalteBody = TempalteBody.Replace("$twlogo", "http://www.massage2book.com/images/twitter.png");
                    TempalteBody = TempalteBody.Replace("$gplogo", "http://www.massage2book.com/images/g_plus.png");

                    TempalteBody = TempalteBody.Replace("$sitelogolink", "http://www.massage2book.com/home");
                    TempalteBody = TempalteBody.Replace("$sitelogo", "http://www.massage2book.com/images/m2b_logo.PNG");
                    TempalteBody = TempalteBody.Replace("$fblink", "https://www.facebook.com/massage2book");
                    TempalteBody = TempalteBody.Replace("$twitlink", "https://twitter.com/Massage2Book");
                    TempalteBody = TempalteBody.Replace("$email", ConfigurationManager.AppSettings["EmailTable2Book"].ToString());
                    objmail.sender = ConfigurationManager.AppSettings["EmailTable2Book"].ToString();
                    TempalteBody = TempalteBody.Replace("$gplink", "https://plus.google.com/103277799241673468164");

                    int status = 0;
                    string[] emails = mailids.Value.TrimEnd(',').Split(',');
                    foreach (string id in emails)
                    {
                        objmail.Mrecipients = id.Trim();
                        objmail.Mbody = TempalteBody;
                        objmail.Msubject = "Your friend invited you on MyMassagePartner";
                        status = objmail.SendMail();
                    }

                    //objmail.Mrecipients = txtemail_friend.Value.Trim();
                    //objmail.Mbody = TempalteBody;
                    //objmail.Msubject = "Your friend invited you from Massage-Partner";
                    //int status = objmail.SendMail();


                    if (status != 0)
                    {
                        lblinvitemsg.Text = "invitation(s) sent successfully! ";
                        lblinvitemsg.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                        lblinvitemsg.Text = "invitation not sent.";
                }
                else
                {
                    lblinvitemsg.Text = "Invalid Email ID";
                    lblinvitemsg.ForeColor = System.Drawing.Color.Red;
                }
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
        protected void lnkmmbership_Click(object sender, EventArgs e)
        {
            Session["Is_after_registration"] = "Y";
            Response.Redirect(Request.RawUrl);
        }
        /////Payment Popup
        protected void SubmitStripe_1_Click(object sender, EventArgs e)
        {
            {
                Utils.Mails mail = new Utils.Mails();
                mail.payNow_Click_Mail(Session["country_sk"].ToString(), Session["massage_partner_sk"].ToString());
                if (Session["massage_partner_sk"] != null)
                {
                    DataSet ds = new DataSet();
                    int? Banksk = null;
                    int? cardsk = null;
                    ds = objBusinessLogin.Insert_payment_bridge(Convert.ToInt32(Session["massage_partner_sk"].ToString()), "sp", Banksk, cardsk, "");

                }
                Session["ccAvanue"] = null;
                Session["CurrentURL"] = HttpContext.Current.Request.Url.AbsoluteUri;
                Session["Provider_payment"] = price.InnerText + "|" + (checkboxOneYear_provider.Checked ? "1" : (checkboxTwoYear_provider.Checked ? "2" : "3"));
                Session["StripepaymentGatway"] = "stripe";

                if (Session["pay_partner"] != null && Session["seeker_subscribed"].ToString() == "Y")
                    Response.Redirect(Constants__.WEB_ROOT + "/partner-subscription_", false);
                else
                    Response.Redirect(Constants__.WEB_ROOT + "/partner-subscription", false);
                return;

            }
        }
        protected void paypal_Click(object sender, EventArgs e)
        {
            try
            {
                Utils.Mails mail = new Utils.Mails();
                mail.payNow_Click_Mail(Session["country_sk"].ToString(), Session["massage_partner_sk"].ToString());

                if (Session["massage_partner_sk"] != null)
                {
                    DataSet ds = new DataSet();
                    int? Banksk = null;
                    int? cardsk = null;
                    ds = objBusinessLogin.Insert_payment_bridge(Convert.ToInt32(Session["massage_partner_sk"].ToString()), "sp", Banksk, cardsk, "");
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["payment_gateway_name"] != null)
                            {
                                if (ds.Tables[0].Rows[0]["payment_gateway_name"].ToString() == "CCAvenue")
                                {
                                    Session["ccAvanue"] = "Y";
                                }
                                else
                                {
                                    {
                                        Session["ccAvanue"] = null;
                                    }
                                }
                            }
                        }
                    }
                }
                Session["CurrentURL"] = HttpContext.Current.Request.Url.AbsoluteUri;
                Session["Provider_payment"] = price.InnerText + "|" + (checkboxOneYear_provider.Checked ? "1" : (checkboxTwoYear_provider.Checked ? "2" : "3"));
                if (Session["pay_partner"] != null && Session["seeker_subscribed"].ToString() == "Y")
                    Response.Redirect(Constants__.WEB_ROOT + "/partner-subscription_", false);
                else
                    Response.Redirect(Constants__.WEB_ROOT + "/partner-subscription", false);

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
            //try
            //{
            //    int login_sk = Convert.ToInt32(Session["mp_login_sk"].ToString());
            //    Utils.Mails mail = new Utils.Mails();
            //    mail.payNow_Click_Mail(Session["country_sk"].ToString(), Session["massage_partner_sk"].ToString());
            //    int i = 0;
            //    string Subscription_price = "0";

            //    System.Data.DataSet ds = new DataSet();
            //    Business.BusinessLogin objBusinessLogin = new BusinessLogin();
            //    if (Session["pay_partner"] != null && Session["seeker_subscribed"].ToString()=="Y")
            //         ds = objbusinessmpartener.getPartnerSubsciption_record(Convert.ToInt32(Session["pay_partner"].ToString()));
            //    else
            //        ds = objbusinessmpartener.getPartnerSubsciption_record(Convert.ToInt32(Session["massage_partner_sk"].ToString()));
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        Session["UserFirstName"] = ds.Tables[0].Rows[0]["massage_partner_name"].ToString();
            //        ViewState["country_sk"] = ds.Tables[0].Rows[0]["country_sk"].ToString();
            //        if (ds.Tables.Count > 9)
            //        {
            //            if (ds.Tables[8].Rows.Count > 0)
            //            {
            //                ViewState["2_year"] = ds.Tables[8].Rows[0]["two_year"].ToString();
            //                ViewState["3_year"] = ds.Tables[8].Rows[0]["three_year"].ToString();
            //            }
            //            else
            //            {
            //                ViewState["2_year"] = ds.Tables[9].Rows[0]["two_year"].ToString();
            //                ViewState["3_year"] = ds.Tables[9].Rows[0]["three_year"].ToString();
            //            }
            //        }
            //        if (ds.Tables[3].Rows.Count > 0) { Session["seeker_subscribed"] = "Y"; }
            //        else
            //        {
            //            decimal Two_year_per = 0;
            //            decimal Three_year_per = 0;

            //            if (ViewState["country_sk"].ToString() == "100")
            //            {
            //                Two_year_per = (Convert.ToDecimal(ConfigurationManager.AppSettings["Provider_india_2_year_per_partner"].ToString()));
            //                Three_year_per = (Convert.ToDecimal(ConfigurationManager.AppSettings["Provider_india_3_year_per_partner"].ToString()));
            //            }
            //            else
            //            {
            //                Two_year_per = (Convert.ToDecimal(ConfigurationManager.AppSettings["Provider_foreign_2_year_per_partner"].ToString()));
            //                Three_year_per = (Convert.ToDecimal(ConfigurationManager.AppSettings["Provider_foreign_3_year_per_partner"].ToString()));
            //            }

            //            if (ds.Tables[4].Rows.Count > 0)
            //            {
            //                    int paymentforyear = checkboxOneYear_provider.Checked == true ? 1 : checkboxTwoYear_provider.Checked == true ? 2 : 3;
            //                    if (paymentforyear == 1)
            //                    {
            //                        ViewState["Seeker_subscrition_price"] = Get_Listing_Price(ds.Tables[4].Rows[0]["basic_unit_price"].ToString(), 0);
            //                    }
            //                    else if (paymentforyear == 2)
            //                    {
            //                        ViewState["Seeker_subscrition_price"] = Get_Listing_Price(ViewState["2_year"].ToString(), 0);
            //                    }
            //                    else if (paymentforyear == 3)
            //                    {
            //                        ViewState["Seeker_subscrition_price"] = Get_Listing_Price(ViewState["3_year"].ToString(), 0);
            //                    }
            //                    else
            //                    {
            //                        ViewState["Seeker_subscrition_price"] = Get_Listing_Price(ds.Tables[4].Rows[0]["basic_unit_price"].ToString(), 0);
            //                    }

            //                ViewState["currency_short_name"] = ds.Tables[4].Rows[0]["currency_short_name"].ToString();
            //                if (ds.Tables[4].Rows[0]["exchng_rate"].ToString() != "")
            //                {
            //                    ViewState["Exchange_rate"] = ds.Tables[4].Rows[0]["exchng_rate"].ToString();
            //                }
            //                else
            //                {
            //                    ViewState["Exchange_rate"] = "1";
            //                }
            //            }
            //            else
            //            {
            //                if (ds.Tables[5].Rows.Count > 0)
            //                {
            //                    int paymentforyear = checkboxOneYear_provider.Checked == true ? 1 : checkboxTwoYear_provider.Checked == true ? 2 : 3;
            //                    if (paymentforyear == 1)
            //                    {
            //                        ViewState["Seeker_subscrition_price"] = Get_Listing_Price(ds.Tables[5].Rows[0]["basic_unit_price"].ToString(), 0);

            //                    }
            //                    else if (paymentforyear == 2)
            //                    {
            //                        ViewState["Seeker_subscrition_price"] = Get_Listing_Price(ds.Tables[5].Rows[0]["basic_unit_price"].ToString(), Convert.ToDecimal(ConfigurationManager.AppSettings["Provider_foreign_2_year_per_partner"].ToString()));
            //                    }
            //                    else if (paymentforyear == 3)
            //                    {
            //                        ViewState["Seeker_subscrition_price"] = Get_Listing_Price(ds.Tables[5].Rows[0]["basic_unit_price"].ToString(), Convert.ToDecimal(ConfigurationManager.AppSettings["Provider_foreign_3_year_per_partner"].ToString()));

            //                    }
            //                    else
            //                    {
            //                        //  price.InnerText = "USD " + ds.Tables[5].Rows[0]["basic_unit_price"].ToString();
            //                        ViewState["Seeker_subscrition_price"] = Get_Listing_Price(ds.Tables[5].Rows[0]["basic_unit_price"].ToString(), 0);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    if (ds.Tables[0].Rows.Count >= 1)
            //    {
            //        int ProviderId =0;
            //        if (Session["pay_partner"] != null && Session["seeker_subscribed"].ToString() == "Y")
            //            ProviderId = Convert.ToInt32(Session["pay_partner"].ToString());
            //        else
            //            ProviderId = Convert.ToInt32(ds.Tables[0].Rows[0]["massage_partner_sk"]);

            //        DataTable dt = new DataTable();
            //        DataColumn dc;
            //        dc = new DataColumn("massage_partner_sk");
            //        dc.DataType = System.Type.GetType("System.Int32");
            //        dt.Columns.Add(dc);
            //        dc = new DataColumn("country_sk");
            //        dc.DataType = System.Type.GetType("System.Int32");
            //        dt.Columns.Add(dc);

            //        dc = new DataColumn("subscription_start_date");
            //        dc.DataType = System.Type.GetType("System.String");
            //        dt.Columns.Add(dc);

            //        dc = new DataColumn("subscription_end_date");
            //        dc.DataType = System.Type.GetType("System.String");
            //        dt.Columns.Add(dc);


            //        dc = new DataColumn("one_time_price");
            //        dc.DataType = System.Type.GetType("System.String");
            //        dt.Columns.Add(dc);

            //        dc = new DataColumn("is_subscribed");
            //        dc.DataType = System.Type.GetType("System.String");
            //        dt.Columns.Add(dc);

            //        dc = new DataColumn("is_paid");
            //        dc.DataType = System.Type.GetType("System.String");
            //        dt.Columns.Add(dc);

            //        dc = new DataColumn("PaymentGateway");
            //        dc.DataType = System.Type.GetType("System.String");
            //        dt.Columns.Add(dc);

            //        DateTime dsfrom = System.DateTime.Now;
            //        DateTime dsto = dsfrom.AddYears(1);

            //        if (checkboxOneYear_provider.Checked) { dsto = dsfrom.AddYears(1); }
            //        else if (checkboxTwoYear_provider.Checked) { dsto = dsfrom.AddYears(2); }
            //        else if (checkboxThreeYear_provider.Checked) { dsto = dsfrom.AddYears(3); }
            //        else { dsto = dsfrom.AddYears(1); }

            //        Subscription_price = ViewState["Seeker_subscrition_price"].ToString();
            //        int country_sk = Convert.ToInt32(ViewState["country_sk"]);
            //        dt.Rows.Add(ProviderId, country_sk, dsfrom, dsto, Subscription_price, "N", "N", "");
            //        Session["ServiceProvider_subscription"] = dt;
            //        BussinessSendMail objmail = new BussinessSendMail();
            //        bool MobileDevice2 = false;
            //        string strUserAgent = "";
            //        string IS_Mobile_Device = "by using Desktop.";

            //        try
            //        {
            //            if (Request.UserAgent != null && Request.UserAgent.ToString() != "")            //nilesh
            //            {
            //                strUserAgent = Request.UserAgent.ToString().ToLower();
            //            }
            //        }
            //        catch (System.Exception ex)
            //        {
            //            BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message + "strUserAgent=" + Request.UserAgent;
            //            var str = new System.Diagnostics.StackTrace(ex, true);
            //            StackFrame[] stackFrames = str.GetFrames();
            //            foreach (StackFrame stackFrame in stackFrames)
            //            {
            //                Console.WriteLine(stackFrame.GetMethod().Name);   // write method name
            //                BussinessEntity.ExceptionHandling._lineno = stackFrame.GetFileLineNumber();
            //                BussinessEntity.ExceptionHandling._methodname = Convert.ToString(stackFrame.GetMethod().Name);
            //                BussinessEntity.ExceptionHandling._pagename = Convert.ToString(Request.Url.AbsoluteUri);

            //            }

            //            return;
            //        }

            //        if (Request.Browser.IsMobileDevice != false)
            //        {
            //            MobileDevice2 = Request.Browser.IsMobileDevice;
            //        }


            //        if (Request.Cookies["MobileDevice"] != null)
            //        {
            //            if (Request.Cookies["MobileDevice"].Value == "IgnoreMobile") { MobileDevice2 = false; }
            //        }
            //        else
            //        {



            //            if (strUserAgent != null && strUserAgent != "")
            //            {

            //                if (MobileDevice2 == true || strUserAgent.Contains("iphone") || strUserAgent.Contains("blackberry") || strUserAgent.Contains("mobile") ||
            //                strUserAgent.Contains("android") || strUserAgent.Contains("windows ce") || strUserAgent.Contains("opera mini") || strUserAgent.Contains("palm"))
            //                {

            //                    IS_Mobile_Device = "by using Mobile device.";
            //                    // Response.Redirect(Constants__.WEB_ROOT + "/m-home", false);
            //                    // return;
            //                }
            //            }
            //        }
            //        if (Session["pay_partner"] != null && Session["seeker_subscribed"].ToString() == "Y")
            //              i = objmail.sendMail(login_sk, "Massage Partner(Pay For Partner) Pay Now Click event", "user (Massage Partner SK= " + ProviderId + ") is clicked for paypal for Partner(Partner Sk: " + Session["pay_partner"].ToString() + ")  registration subscription " + IS_Mobile_Device, null);
            //        else
            //            i = objmail.sendMail(login_sk, "Massage Partner Pay Now Click event", "user (Massage Partner SK= " + ProviderId + ") is clicked for paypal for registration subscription " + IS_Mobile_Device, null);         
            //    }
            //    string replaced_str = "";
            //                    string curr1 = "";
            //                    string payable_amt = Subscription_price;
            //                    int is_paypal = obj.is_paypal();

            //                    if (is_paypal == 1)
            //                    {
            //                        if (Subscription_price != "0.00")
            //                        {


            //                            if (payable_amt != "0.00")
            //                            {
            //                                int country_sk = 0;
            //                                string str = "";
            //                                if (ViewState["currency_short_name"] != null)
            //                                {
            //                                    curr1 = Convert.ToString(ViewState["currency_short_name"]);
            //                                }

            //                                try
            //                                {

            //                                    if (ViewState["country_sk"] != null)
            //                                        country_sk = Convert.ToInt32(ViewState["country_sk"]);

            //                                    ReadWriteWebservice objs = new ReadWriteWebservice();
            //                                     if (ViewState["Exchange_rate"] != null)
            //                                         str = objs.CurrencyConversion(payable_amt, ViewState["currency_short_name"].ToString(), "USD", ViewState["Exchange_rate"].ToString());
            //                                        else
            //                                            str = objs.CurrencyConversion(payable_amt, "USD", "USD", "1");

            //                                     str = str.Normalize();
            //                                     replaced_str = str.Replace(" USD", "");
            //                                     Session["Converted_Doller_amount"] = replaced_str;
            //                                     SavePromotion_BeforePaypal();
            //                                     SubmitToPaypal(replaced_str);
            //                                }
            //                                catch (System.Exception ex)
            //                                {
            //                                    lblmassage.Text = "Whoops! Payment unsuccessful. Retry or use other payment method.";
            //                                    lblmassage.Style.Add("color", "red");
            //                                    return;
            //                                }
            //                            }
            //                        }
            //                    }
            //}
            //catch (System.Exception ex)
            //{
            //    lblmassage.Text = "Whoops! Payment unsuccessful. Retry or use other payment method.";
            //    lblmassage.Style.Add("color", "red");
            //    return;
            //}
        }
        //private StripeCustomer GetCustomer()
        //{
        //    var mycust = new StripeCustomerCreateOptions();
        //    mycust.Email = "";
        //    mycust.Description = "";
        //    mycust.CardNumber = txtCardNumber_1.Text.Trim();
        //    mycust.CardExpirationMonth = txtCardExpirationMonth_1.Text.Trim();
        //    mycust.CardExpirationYear = txtCardExpirationYear_1.Text.Trim();
        //    // mycust.PlanId = "100";
        //    mycust.CardName = txtCardName_1.Text.Trim();
        //    mycust.CardAddressCity = "";
        //    mycust.CardAddressCountry = "";
        //    mycust.CardAddressLine1 = "";
        //    //mycust.TrialEnd = getrialend();
        //    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

        //    var customerservice = new StripeCustomerService(ConfigurationManager.AppSettings["StripeApiKey"].ToString());
        //    return customerservice.Create(mycust);
        //}
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
            }
        }
        private void fillDropdown()
        {
            try
            {
                DataSet ds = new DataSet();


                string mode = "M";
                ds = objBusinessSearch.getSpaType(mode);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    ViewState["country"] = ds.Tables[0];
                    //ddlcountry.DataValueField = "country_sk";
                    //ddlcountry.DataTextField = "country_name";
                    //ddlcountry.DataBind();
                }
                // 224	9	157

                DataRow[] dr;
                dr = ds.Tables[0].Select("country_code='" + Uip + "'");
                int cntry_sk = 0;
                int stt_sk = 0;
                int city_sk = 0;
                foreach (DataRow row in dr)
                {
                    ViewState["selected_country"] = row["country_name"].ToString();
                    ViewState["selected_country_value"] = row["country_sk"].ToString();
                    //ddlcountry.SelectedValue = row["country_name"].ToString();
                    //ddlcountry.ToolTip = ddlcountry.SelectedItem.Text;


                    //fill state                   
                    ds = objRegistrationBusiness.getStateProvider(Convert.ToInt32(ViewState["selected_country_value"]));
                    cntry_sk = Convert.ToInt32(ViewState["selected_country_value"].ToString());
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ViewState["state"] = ds.Tables[0];
                        //ddlstate.DataValueField = "state_sk";
                        //ddlstate.DataTextField = "state_name";

                        dr = ds.Tables[0].Select("state_name='" + Ustate + "'");
                        foreach (DataRow sts in dr)
                        {
                            ViewState["selected_state_value"] = sts[1].ToString();
                            ViewState["selected_state"] = sts["state_name"].ToString();
                            //Fill City

                            ds = objRegistrationBusiness.getCityProvider(Convert.ToInt32(ViewState["selected_state_value"].ToString()), Convert.ToInt32(ViewState["selected_country_value"].ToString()));
                            if (ds.Tables[0].Rows.Count > 0)
                            {

                                ViewState["state"] = ds.Tables[0];
                                //ddlcity.DataValueField = "city_sk";
                                //ddlcity.DataTextField = "city_name_display";
                                if (Ucity.Contains("'"))
                                    Ucity = Ucity.Replace("'", "''");

                                dr = ds.Tables[0].Select("city_name='" + Ucity + "'");
                                foreach (DataRow ct in dr)
                                {
                                    ViewState["selected_city_value"] = ct[2].ToString();
                                    ViewState["selected_city"] = ct["city_name_display"].ToString();
                                    //city_sk = Convert.ToInt32(ddlcity.SelectedValue);
                                    //ddlcity.ToolTip = ddlcity.SelectedItem.Text;
                                }

                            }
                        }

                        //-------------------------------------------------------------------------

                        //getLocalTime(Lat, Lag, HttpContext.Current.Request.UserHostAddress.ToString(), cntry_sk, stt_sk, city_sk);


                    }
                    else
                    {
                        //ddlstate.Items.Clear();
                        //ddlcity.Items.Clear();
                        //ddlstate.Enabled = false;
                        //ddlcity.Enabled = false;

                    }
                }
                if (dr.Count() <= 0)
                {
                    //  Filldefaultstate_city();

                }
                // ddlcity_SelectedIndexChanged(null, null);

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
            }


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
        protected void lnksearchpartner_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "";
                if (Session["user_location_search"] != null)
                {
                    url = Session["user_location_search"].ToString();
                }
                else
                {
                    GetIP();
                    fillDropdown();
                    url = "/" + ViewState["selected_country"].ToString().Replace(' ', '-') + "/" + ViewState["selected_state"].ToString().Replace(' ', '-') + "/" + ViewState["selected_city"].ToString().Replace(' ', '-') + "/All-Area/Any/all-types/all";
                }
                Response.Redirect(Constants__.WEB_ROOT + "/massage-partner" + url, false);
            }
            catch
            {
                ViewState["selected_country"] = ConfigurationManager.AppSettings["default_countryCode1"].ToString();
                ViewState["selected_state"] = ConfigurationManager.AppSettings["default_regionName"].ToString();
                ViewState["selected_city"] = ConfigurationManager.AppSettings["default_city"].ToString();
                string url = "/" + ViewState["selected_country"].ToString().Replace(' ', '-') + "/" + ViewState["selected_state"].ToString().Replace(' ', '-') + "/" + ViewState["selected_city"].ToString().Replace(' ', '-') + "/All-Area/Any/all-types/all";
                Response.Redirect(Constants__.WEB_ROOT + "/massage-partner" + url, false);
            }
        }

        protected void lnksearchpartner1_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "";
                if (Session["user_location_search"] != null)
                {
                    url = Session["user_location_search"].ToString();
                }
                else
                {
                    GetIP();
                    fillDropdown();


                    url = "/" + ViewState["selected_country"].ToString().Replace(' ', '-') + "/" + ViewState["selected_state"].ToString().Replace(' ', '-') + "/" + ViewState["selected_city"].ToString().Replace(' ', '-') + "/All-Area/Any/all-types/all";
                }
                Response.Redirect(Constants__.WEB_ROOT + "/massage-partner" + url, false);
            }
            catch
            {
                ViewState["selected_country"] = ConfigurationManager.AppSettings["default_countryCode1"].ToString();
                ViewState["selected_state"] = ConfigurationManager.AppSettings["default_regionName"].ToString();
                ViewState["selected_city"] = ConfigurationManager.AppSettings["default_city"].ToString();
                string url = "/" + ViewState["selected_country"].ToString().Replace(' ', '-') + "/" + ViewState["selected_state"].ToString().Replace(' ', '-') + "/" + ViewState["selected_city"].ToString().Replace(' ', '-') + "/All-Area/Any/all-types/all";
                Response.Redirect(Constants__.WEB_ROOT + "/massage-partner" + url, false);
            }
        }
        private void sendMail()
        {
            try
            {
                string TempalteBody = "";

                DataTable dt = objNotification.GetNotificationTemplate(8, 1);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToString(dt.Rows[0]["notification_text_standard"]) != "")
                        {
                            TempalteBody = Convert.ToString(dt.Rows[0]["notification_text_standard"]);

                        }
                    }
                }


                int firstString = TempalteBody.IndexOf("divlogininformation") + 1 + 19;

                //TempalteBody.Insert(firstString, "<div> User Name -   " + Convert.ToString(ds.Tables[0].Rows[0]["email_id"]) + " <br /> password - " + Convert.ToString(ds.Tables[0].Rows[0]["password"]) + "</div>");

                TempalteBody = (TempalteBody.Substring(0, firstString)) + "><div> Login Id-   " + Convert.ToString(ds.Tables[0].Rows[0]["email_id"]) + " <br /> password - " + Convert.ToString(ds.Tables[0].Rows[0]["password"]) + "</div>" + (TempalteBody.Substring(firstString + 2, TempalteBody.Length - (firstString + 2)));
                TempalteBody = TempalteBody.Replace("$Customer", ds.Tables[0].Rows[0]["Name"].ToString());

                BussinessSendMail objmail = new BussinessSendMail();


                TempalteBody = TempalteBody.Replace("$sitelogolink", "http://www.massage2book.com/home");
                TempalteBody = TempalteBody.Replace("$sitelogo", "http://www.massage2book.com/images/m2b_logo.PNG");
                TempalteBody = TempalteBody.Replace("$fblink", "https://www.facebook.com/massage2book");
                TempalteBody = TempalteBody.Replace("$twitlink", "https://twitter.com/Massage2Book");
                TempalteBody = TempalteBody.Replace("$email", ConfigurationManager.AppSettings["EmailTable2Book"].ToString());
                objmail.sender = ConfigurationManager.AppSettings["EmailTable2Book"].ToString();
                TempalteBody = TempalteBody.Replace("$gplink", "https://plus.google.com/103277799241673468164");

                objmail.Mrecipients = ds.Tables[0].Rows[0]["email_id"].ToString();
                objmail.Mbody = TempalteBody;
                objmail.Msubject = "MyMassagePartner-Forgot Password";
                int status = objmail.SendMail();


                if (status != 0)
                {
                    lblreportmsg.Text = "password send successfully to your email id. ";
                    lblreportmsg.ForeColor = System.Drawing.Color.Green;
                }
                else
                    lblreportmsg.Text = "Mail not sent.";
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
        protected void btnforgot_Click(object sender, EventArgs e)
        {
            try
            {
                Label1.Text = "";
                Label1.Visible = true;
                Label1.ForeColor = System.Drawing.Color.Red;
                txtforgotemail.Enabled = false;
                ds = objbusinessmpartener.get_forgot_password(txtforgotemail.Text.Trim());
                if (ds.Tables[0].Rows.Count >= 1 && ds.Tables[0].Rows[0][0].ToString() != "0")
                {
                    sendMail();
                    Label1.Text = "Your password has been sent to your email Id.";
                    // btnforgot.CssClass = "forget_pass_all_lite_03";
                    btnforgot.Enabled = false;

                }
                else
                {
                    Label1.Text = "Email id does not exist";

                }
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
        protected void btnlogin_Click(object sender, EventArgs e)
        {
            Session["current_url"] = Request.RawUrl;
            Session["Is_after_login"] = "Y";
            check_login(txtEmail.Text.Trim(), txtPassword.Text.Trim());
        }
        private void check_login(string email, string password)
        {
            DataSet ds;
            ds = objbusinessmpartener.partner_login(email, password);
            if (ds.Tables.Count > 0)
            {
                Session["mp_login_sk"] = ds.Tables[0].Rows[0]["login_sk"].ToString();
                Session["massage_partner_sk"] = ds.Tables[0].Rows[0]["massage_partner_sk"].ToString();
                Session["email_id"] = ds.Tables[0].Rows[0]["email_id"].ToString();
                Session["country_sk"] = ds.Tables[0].Rows[0]["country_sk"].ToString();
                if (ds.Tables[0].Rows[0]["is_subscribed"].ToString() == "")
                    Session["seeker_subscribed"] = null;
                else
                    Session["seeker_subscribed"] = ds.Tables[0].Rows[0]["is_subscribed"].ToString();
                if (Session["current_url"] != null)
                {
                    Response.Redirect(Session["current_url"].ToString());
                    Session["current_url"] = null;
                    return;
                }
                else
                {
                    try
                    {
                        GetIP();
                        fillDropdown();
                        string url = "/" + ViewState["selected_country"].ToString().Replace(' ', '-') + "/" + ViewState["selected_state"].ToString().Replace(' ', '-') + "/" + ViewState["selected_city"].ToString().Replace(' ', '-') + "/All-Area/Any/all-types/all";
                        Response.Redirect(Constants__.WEB_ROOT + "/massage-partner" + url);
                        return;
                    }
                    catch
                    {
                        ViewState["selected_country"] = ConfigurationManager.AppSettings["default_countryCode1"].ToString();
                        ViewState["selected_state"] = ConfigurationManager.AppSettings["default_regionName"].ToString();
                        ViewState["selected_city"] = ConfigurationManager.AppSettings["default_city"].ToString();
                        string url = "/" + ViewState["selected_country"].ToString().Replace(' ', '-') + "/" + ViewState["selected_state"].ToString().Replace(' ', '-') + "/" + ViewState["selected_city"].ToString().Replace(' ', '-') + "/All-Area/Any/all-types/all";
                        Response.Redirect(Constants__.WEB_ROOT + "/massage-partner" + url, false);
                        return;
                    }
                }

            }
            else
            {
                lblloginerror.Visible = true;
                lblloginerror.Text = "invalid email-id or password!";

            }
        }
        protected void lnkbtn3dpayment_Click(object sender, EventArgs e)
        {
            Session["CurrentURL"] = HttpContext.Current.Request.Url.AbsoluteUri;
            if (Session["pay_partner"] != null)
            { Response.Redirect(Constants__.WEB_ROOT + "/partner-subscription_", false); }
            else
            {
                Response.Redirect(Constants__.WEB_ROOT + "/partner-subscription", false);
            }
            Session["3dpaymentSeeker"] = "Yes";
            Session["Provider_payment"] = price.InnerText + "|" + (checkboxOneYear_provider.Checked ? "1" : (checkboxTwoYear_provider.Checked ? "2" : "3"));

            return;
        }
        protected void btnNetbanking_Click(object sender, EventArgs e)
        {

            BussinessSendMail objMail = new BussinessSendMail();
            string message = "Required bank details. Customer email id : " + Session["email_id"].ToString();
            objMail.sender = ConfigurationManager.AppSettings["EmailTable2Book"].ToString();
            objMail.Mrecipients = ConfigurationManager.AppSettings["EmailTable2Book"].ToString();
            objMail.Mbody = message;
            if (Session["pay_partner"] != null && Session["seeker_subscribed"].ToString() == "Y")
                objMail.Msubject = "Required Bank detail for membership for Partner Payment, Sk: " + Session["pay_partner"].ToString();
            else
                objMail.Msubject = "Required Bank detail for membership";

            objMail.SendMail();
            lblNetBankingmsg.Visible = true;
            btnNetbanking.Enabled = false;
        }
        protected void btnQr_Click(object sender, EventArgs e)
        {
            BussinessSendMail objmail = new BussinessSendMail();

            string pagedetails = "";
            if (Session["pay_partner"] != null && Session["seeker_subscribed"].ToString() == "Y")
                pagedetails = "Massage Partner Sk - " + Session["massage_partner_sk"].ToString() + " paid for Sk=" + Session["pay_partner"].ToString() + ", Paid By:  Email Id - " + txtqr_mail_id.Text.Trim() + "   QR Payment transaction iD " + txtQrTransaction.Text.Trim();
            else
                pagedetails = "Massage Partner Sk - " + Session["massage_partner_sk"].ToString() + "  Email Id - " + txtqr_mail_id.Text.Trim() + "   QR Payment transaction iD " + txtQrTransaction.Text.Trim();

            int i = objmail.SendMail(ConfigurationManager.AppSettings["EmailTable2Book"].ToString(), ConfigurationManager.AppSettings["EmailTable2Book"].ToString(), "QR Payment transection ID " + txtQrTransaction.Text.Trim(), pagedetails, null);

            if (i == -1)
            {
                lblQrTransaction.Visible = true;
                lblQrTransaction.Text = "We will verify and activate your membership.";
            }
        }
        protected void btnsendtransection_Click(object sender, EventArgs e)
        {
            BussinessSendMail objmail = new BussinessSendMail();

            string pagedetails = "";
            if (Session["pay_partner"] != null && Session["seeker_subscribed"].ToString() == "Y")
                pagedetails = "Massage Partner Sk - " + Session["massage_partner_sk"].ToString() + " paid for Sk=" + Session["pay_partner"].ToString() + ", Paid By:  Email Id - " + txtregisteredmail_id.Text.Trim() + "       Paytm transaction iD " + ctxttransaction.Text.Trim();
            else
                pagedetails = "Massage Partner Sk - " + Session["massage_partner_sk"].ToString() + "  Email Id - " + txtregisteredmail_id.Text.Trim() + "       Paytm transaction iD " + ctxttransaction.Text.Trim();

            int i = objmail.SendMail(ConfigurationManager.AppSettings["EmailTable2Book"].ToString(), ConfigurationManager.AppSettings["EmailTable2Book"].ToString(), "Paytm transection ID " + ctxttransaction.Text.Trim(), pagedetails, null);

            if (i == -1)
            {
                lblpaytmmsg.Visible = true;
                lblpaytmmsg.Text = "We will verify and activate your membership.";
            }
        }

        private void update_ptmLink()
        {
            BusinessMPAdmin objadmin = new BusinessMPAdmin();
            DataSet ds = objadmin.get_payment_parameters();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //lnkPaymtn.Text = ds.Tables[0].Rows[0]["parameter_value"].ToString().Trim();
                    //lnkPaymtn.Attributes.Add("href", ds.Tables[0].Rows[0]["parameter_value"].ToString().Trim());
                }
            }
        }

        protected void btnOxPay_Click(object sender, EventArgs e)
        {
            try
            {
                Utils.Mails mail = new Utils.Mails();
                mail.payNow_Click_Mail(Session["country_sk"].ToString(), Session["massage_partner_sk"].ToString());
                Session["oxPay"] = "Yes";
                Session["CurrentURL"] = HttpContext.Current.Request.Url.AbsoluteUri;
                Session["Provider_payment"] = price.InnerText + "|" + (checkboxOneYear_provider.Checked ? "1" : (checkboxTwoYear_provider.Checked ? "2" : "3"));
                if (Session["pay_partner"] != null && Session["seeker_subscribed"].ToString() == "Y")
                    Response.Redirect(Constants__.WEB_ROOT + "/partner-subscription_", false);
                else
                    Response.Redirect(Constants__.WEB_ROOT + "/partner-subscription", false);

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
    }
}
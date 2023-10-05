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
using System.IO;
using BussinessEntity;
using System.Drawing;
using System.Data.SqlClient;
using RESTFulWCFService;
using System.Diagnostics;
using PayPalIntegration;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using CCA.Util;
using Stripe;
using System.Net;
using Newtonsoft.Json;
using Stripe.Checkout;

namespace RESTFulWCFService.User
{
    public partial class multipayment_partner : System.Web.UI.Page
    {
        private bool PayPalReturnRequest = false;
        protected decimal OrderAmount = 0.00M;
        BusinessMPartener objbusinessmpartener = new BusinessMPartener();
        BussinessPaypal obj = new BussinessPaypal();
        RegistrationBusiness objRegistrationBusiness = new RegistrationBusiness();
        Business.BusinessLogin objBusinessLogin = new BusinessLogin();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lnkBack.PostBackUrl = Constants__.WEB_ROOT;

                if (Request.QueryString["code"] != null && Request.QueryString["code"].ToString() == "oxpay_subscription")
                {
                    check_login(Request.QueryString["user"].ToString());
                    Session["Provider_payment"] = Request.QueryString["refNo"].ToString();
                }

                if (Session["CurrentURL"] != null)
                {
                    if ((Session["CurrentURL"].ToString()).Contains("/partner-subscription_"))
                    {
                        Session["CurrentURL"] = Constants__.WEB_ROOT;
                    }
                    linkback.HRef = Session["CurrentURL"].ToString();
                }
                else
                    linkback.HRef = Constants__.WEB_ROOT;

                if (!IsPostBack)
                {
                    if (Session["3dpaymentSeeker"] != null && Session["3dpaymentSeeker"].ToString() == "Yes")
                    {
                        paypalReturnscreen.Visible = true;
                        paypalGonescrren.Visible = false;
                        divpaymentMode.Visible = true;

                        if (Session["country_sk"] != null && Session["country_sk"].ToString() == "100")
                        {

                            txtregisteredmail_id.Text = Session["email_id"].ToString();
                            txtqr_mail_id.Text = Session["email_id"].ToString();
                            update_pymntImages();
                            update_ptmLink();
                            user_country_wise_payments(Session["country_sk"].ToString());
                        }
                        else
                        {
                            ul_for_india.Attributes.Add("style", "display:none !important");
                            user_country_wise_payments(Session["country_sk"].ToString());
                            update_pymntImages();
                        }

                    }
                    else
                    {
                        divpaymentMode.Visible = false;

                        if (Session["country_sk"] != null && Session["country_sk"].ToString() == "100")
                        {
                            user_country_wise_payments(Session["country_sk"].ToString());
                            txtregisteredmail_id.Text = Session["email_id"].ToString();
                            txtqr_mail_id.Text = Session["email_id"].ToString();
                            update_ptmLink();
                            update_pymntImages();
                        }
                        else
                        {
                            user_country_wise_payments(Session["country_sk"].ToString());
                            ul_for_india.Attributes.Add("style", "display:none !important");
                            update_pymntImages();
                        }


                        if (Session["mp_login_sk"] != null)
                        {
                            if ((Request.QueryString["PayPal"] != null && Request.QueryString["PayPal"].ToString() == "Info1347Cubek"))
                            {// Paypal successfully payment
                                paypalReturnscreen.Visible = true; paypalGonescrren.Visible = false;
                                send_auto_activation_mail(Session["email_id"].ToString(), "P");
                                SavePromotion_AfterPaypal();

                            }
                            else if ((Request.QueryString["stripe"] != null && Request.QueryString["stripe"].ToString() == "Info2468Cubek"))
                            {
                                SavePromotion_AfterPaypal_Stripe();
                                send_auto_activation_mail(Session["email_id"].ToString(), "S");
                            }
                            else if ((Request.QueryString["stripe"] != null && Request.QueryString["stripe"].ToString() == "Sub_Cancel"))
                            {
                                success_alert.Attributes.Remove("style");
                                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text1", "error('Whoops! Payment unsuccessful. Retry or use other payment method.');", true);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script_paypal3", "shifting_cards_methods('B');", true);
                                lblSuccess.Style.Add("color", "red");
                                Session["seeker_subscription"] = null;
                                paypalReturnscreen.Visible = true; paypalGonescrren.Visible = false;
                                divpaymentMode.Visible = true;

                            }
                            else if ((Request.QueryString["PayPal"] != null && Request.QueryString["PayPal"].ToString() == "Cubek2468Tech"))
                            {// ccavanue successfully payment
                                paypalReturnscreen.Visible = true; paypalGonescrren.Visible = false;
                                SavePromotion_AfterPaypal_CCAvanue();

                            }
                            else if ((Request.QueryString["PayPal"] != null && Request.QueryString["PayPal"].ToString() == "Sub_Cancel"))
                            {
                                paypalReturnscreen.Visible = true; paypalGonescrren.Visible = false;
                                success_alert.Attributes.Remove("style");
                                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "error('Whoops! Payment unsuccessful. Retry or use other payment method.');", true);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script_paypal1", "shifting_cards_methods('A');", true);
                                DataSet ds = new DataSet();
                                divpaymentMode.Visible = true;

                            }
                            else
                            {
                                Session["seeker_subscribed"] = null;

                                int login_sk = Convert.ToInt32(Session["mp_login_sk"].ToString());

                                int i = 0;
                                string Subscription_price = "0";

                                System.Data.DataSet ds = new DataSet();
                                Business.BusinessLogin objBusinessLogin = new BusinessLogin();
                                ds = objbusinessmpartener.getPartnerSubsciption_record(Convert.ToInt32(Session["pay_partner"].ToString()));
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    Session["UserFirstName"] = ds.Tables[0].Rows[0]["massage_partner_name"].ToString();
                                    ViewState["country_sk"] = ds.Tables[0].Rows[0]["country_sk"].ToString();
                                    if (ds.Tables.Count > 9)
                                    {
                                        if (ds.Tables[8].Rows.Count > 0)
                                        {
                                            ViewState["2_year"] = ds.Tables[8].Rows[0]["two_year"].ToString();
                                            ViewState["3_year"] = ds.Tables[8].Rows[0]["three_year"].ToString();
                                        }
                                        else
                                        {
                                            ViewState["2_year"] = ds.Tables[9].Rows[0]["two_year"].ToString();
                                            ViewState["3_year"] = ds.Tables[9].Rows[0]["three_year"].ToString();
                                        }
                                    }
                                    if (ds.Tables[3].Rows.Count > 0) { Session["seeker_subscribed"] = "Yes"; }
                                    else
                                    {
                                        decimal Two_year_per = 0;
                                        decimal Three_year_per = 0;

                                        if (ViewState["country_sk"].ToString() == "100")
                                        {
                                            Two_year_per = (Convert.ToDecimal(ConfigurationManager.AppSettings["Provider_india_2_year_per_partner"].ToString()));
                                            Three_year_per = (Convert.ToDecimal(ConfigurationManager.AppSettings["Provider_india_3_year_per_partner"].ToString()));
                                        }
                                        else
                                        {
                                            Two_year_per = (Convert.ToDecimal(ConfigurationManager.AppSettings["Provider_foreign_2_year_per_partner"].ToString()));
                                            Three_year_per = (Convert.ToDecimal(ConfigurationManager.AppSettings["Provider_foreign_3_year_per_partner"].ToString()));
                                        }

                                        if (ds.Tables[4].Rows.Count > 0)
                                        {
                                            if (Session["Provider_payment"] != null && Session["Provider_payment"].ToString() != "")
                                            {
                                                string paymentInfo = Session["Provider_payment"].ToString();
                                                List<string> infoarray = paymentInfo.Split('|').ToList();
                                                int paymentforyear = Convert.ToInt16(infoarray[1]);
                                                if (paymentforyear == 1)
                                                {
                                                    ViewState["Seeker_subscrition_price"] = Get_Listing_Price(ds.Tables[4].Rows[0]["basic_unit_price"].ToString(), 0);
                                                }
                                                else if (paymentforyear == 2)
                                                {
                                                    ViewState["Seeker_subscrition_price"] = Get_Listing_Price(ViewState["2_year"].ToString(), 0);
                                                }
                                                else if (paymentforyear == 3)
                                                {
                                                    ViewState["Seeker_subscrition_price"] = Get_Listing_Price(ViewState["3_year"].ToString(), 0);
                                                }
                                                else
                                                {
                                                    ViewState["Seeker_subscrition_price"] = Get_Listing_Price(ds.Tables[4].Rows[0]["basic_unit_price"].ToString(), 0);

                                                }
                                            }

                                            else
                                            {
                                                // price.InnerText = ds.Tables[4].Rows[0]["currency_short_name"].ToString() + " " + (Math.Round(decimal.Parse(ds.Tables[4].Rows[0]["basic_unit_price"].ToString()), 2)).ToString();
                                                ViewState["Seeker_subscrition_price"] = Get_Listing_Price(ds.Tables[4].Rows[0]["basic_unit_price"].ToString(), 0);

                                            }

                                            ViewState["currency_short_name"] = ds.Tables[4].Rows[0]["currency_short_name"].ToString();
                                            if (ds.Tables[4].Rows[0]["currency_short_name"].ToString() == "INR")
                                            {
                                                ViewState["seeker_currency"] = "Rs.";
                                            }
                                            else
                                            {
                                                ViewState["currency_short_name"] = ds.Tables[4].Rows[0]["currency_short_name"].ToString();

                                            }
                                            if (ds.Tables[4].Rows[0]["exchng_rate"].ToString() != "")
                                            {
                                                ViewState["Exchange_rate"] = ds.Tables[4].Rows[0]["exchng_rate"].ToString();
                                            }
                                            else
                                            {
                                                ViewState["Exchange_rate"] = "1";
                                            }

                                        }
                                        else
                                        {
                                            if (ds.Tables[5].Rows.Count > 0)
                                            {
                                                if (Session["Provider_payment"] != null && Session["Provider_payment"].ToString() != "")
                                                {
                                                    string paymentInfo = Session["Provider_payment"].ToString();
                                                    List<string> infoarray = paymentInfo.Split('|').ToList();
                                                    int paymentforyear = Convert.ToInt16(infoarray[1]);
                                                    if (paymentforyear == 1)
                                                    {
                                                        ViewState["Seeker_subscrition_price"] = Get_Listing_Price(ds.Tables[5].Rows[0]["basic_unit_price"].ToString(), 0);

                                                    }
                                                    else if (paymentforyear == 2)
                                                    {
                                                        ViewState["Seeker_subscrition_price"] = Get_Listing_Price(ViewState["2_year"].ToString(), 0);
                                                    }
                                                    else if (paymentforyear == 3)
                                                    {
                                                        ViewState["Seeker_subscrition_price"] = Get_Listing_Price(ViewState["3_year"].ToString(), 0);

                                                    }

                                                    else
                                                    {
                                                        //  price.InnerText = "USD " + ds.Tables[5].Rows[0]["basic_unit_price"].ToString();
                                                        ViewState["Seeker_subscrition_price"] = Get_Listing_Price(ds.Tables[5].Rows[0]["basic_unit_price"].ToString(), 0);
                                                    }
                                                }

                                                else
                                                {
                                                    //  price.InnerText = "USD " + ds.Tables[5].Rows[0]["basic_unit_price"].ToString();
                                                    ViewState["Seeker_subscrition_price"] = Get_Listing_Price(ds.Tables[5].Rows[0]["basic_unit_price"].ToString(), 0);

                                                }
                                                if (ds.Tables[5].Rows[0]["currency_short_name"].ToString() == "INR")
                                                {
                                                    ViewState["currency_short_name"] = ds.Tables[5].Rows[0]["currency_short_name"];
                                                }
                                                else
                                                {
                                                    ViewState["currency_short_name"] = ds.Tables[5].Rows[0]["currency_short_name"];
                                                }

                                            }
                                        }


                                    }
                                }
                                if (ds.Tables[0].Rows.Count >= 1)
                                {
                                    int ProviderId = Convert.ToInt32(ds.Tables[0].Rows[0]["massage_partner_sk"]);

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

                                    dc = new DataColumn("is_paid");
                                    dc.DataType = System.Type.GetType("System.String");
                                    dt.Columns.Add(dc);

                                    dc = new DataColumn("PaymentGateway");
                                    dc.DataType = System.Type.GetType("System.String");
                                    dt.Columns.Add(dc);

                                    DateTime dsfrom = System.DateTime.Now;
                                    DateTime dsto = dsfrom.AddYears(1);
                                    if (Session["Provider_payment"] != null && Session["Provider_payment"].ToString() != "")
                                    {
                                        string paymentInfo = Session["Provider_payment"].ToString();
                                        List<string> infoarray = paymentInfo.Split('|').ToList();
                                        int paymentforyear = Convert.ToInt16(infoarray[1]);
                                        if (paymentforyear == 1)
                                        {
                                            dsto = dsfrom.AddYears(1);
                                        }
                                        else if (paymentforyear == 2) { dsto = dsfrom.AddYears(2); }
                                        else if (paymentforyear == 3) { dsto = dsfrom.AddYears(3); }
                                    }
                                    else
                                    {
                                        dsto = dsfrom.AddYears(1);
                                    }
                                    Subscription_price = ViewState["Seeker_subscrition_price"].ToString();
                                    int country_sk = Convert.ToInt32(ViewState["country_sk"]);
                                    // ViewState["currency_short_name"] = "USD";
                                    dt.Rows.Add(ProviderId, country_sk, dsfrom, dsto, Subscription_price, "N", "N", "");
                                    Session["ServiceProvider_subscription"] = dt;
                                    BussinessSendMail objmail = new BussinessSendMail();
                                    bool MobileDevice2 = false;
                                    string strUserAgent = "";
                                    string IS_Mobile_Device = "by using Desktop.";

                                    try
                                    {
                                        if (Request.UserAgent != null && Request.UserAgent.ToString() != "")            //nilesh
                                        {
                                            strUserAgent = Request.UserAgent.ToString().ToLower();
                                        }
                                    }
                                    catch (System.Exception ex)
                                    {
                                        BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message + "strUserAgent=" + Request.UserAgent;
                                        var str = new System.Diagnostics.StackTrace(ex, true);
                                        StackFrame[] stackFrames = str.GetFrames();
                                        foreach (StackFrame stackFrame in stackFrames)
                                        {
                                            Console.WriteLine(stackFrame.GetMethod().Name);   // write method name
                                            BussinessEntity.ExceptionHandling._lineno = stackFrame.GetFileLineNumber();
                                            BussinessEntity.ExceptionHandling._methodname = Convert.ToString(stackFrame.GetMethod().Name);
                                            BussinessEntity.ExceptionHandling._pagename = Convert.ToString(Request.Url.AbsoluteUri);

                                        }

                                        //Response.Redirect(Constants__.WEB_ROOT + "/ErrorMessage.aspx", false);
                                        return;
                                    }

                                    if (Request.Browser.IsMobileDevice != false)
                                    {
                                        MobileDevice2 = Request.Browser.IsMobileDevice;
                                    }


                                    if (Request.Cookies["MobileDevice"] != null)
                                    {
                                        if (Request.Cookies["MobileDevice"].Value == "IgnoreMobile") { MobileDevice2 = false; }
                                    }
                                    else
                                    {



                                        if (strUserAgent != null && strUserAgent != "")
                                        {

                                            if (MobileDevice2 == true || strUserAgent.Contains("iphone") || strUserAgent.Contains("blackberry") || strUserAgent.Contains("mobile") ||
                                            strUserAgent.Contains("android") || strUserAgent.Contains("windows ce") || strUserAgent.Contains("opera mini") || strUserAgent.Contains("palm"))
                                            {

                                                IS_Mobile_Device = "by using Mobile device.";
                                                // Response.Redirect(Constants__.WEB_ROOT + "/m-home", false);
                                                // return;
                                            }
                                        }
                                    }
                                    if (Session["oxPay"] != null && Session["oxPay"].ToString() == "Yes")
                                        i = objmail.sendMail(login_sk, "Massage Partner Pay Now Click event for partner", "user (Massage Partner SK= " + ProviderId + ") is clicked for OxPay for registration subscription " + IS_Mobile_Device, null);
                                    else
                                        i = objmail.sendMail(login_sk, "Massage Partner Pay Now Click event for partner", "user (Massage Partner SK= " + ProviderId + ") is clicked for paypal for registration subscription " + IS_Mobile_Device, null);
                                    //   i = objmail.sendSubscriptionMail(login_sk);

                                }
                                string replaced_str = "";
                                string curr1 = "";
                                string payable_amt = Subscription_price;
                                int is_paypal = obj.is_paypal();

                                if (is_paypal == 1)
                                {
                                    if (Subscription_price != "0.00")
                                    {


                                        if (payable_amt != "0.00")
                                        {
                                            int country_sk = 0;
                                            string str = "";
                                            if (ViewState["currency_short_name"] != null)
                                            {
                                                curr1 = Convert.ToString(ViewState["currency_short_name"]);
                                            }
                                            try
                                            {
                                                if (ViewState["country_sk"] != null)
                                                    country_sk = Convert.ToInt32(ViewState["country_sk"]);

                                                ReadWriteWebservice objs = new ReadWriteWebservice();
                                                if (Session["oxPay"] != null && Session["oxPay"].ToString() == "Yes")
                                                {
                                                    Session.Remove("oxPay");
                                                    SavePromotion_BeforePaypal();
                                                    //WebClient client = new WebClient();
                                                    //ServicePointManager.Expect100Continue = true;
                                                    //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                                    //client.Headers.Add("Content-Type", "application/json");
                                                    //Random rnd = new Random();
                                                    //dynamic request = new
                                                    //{
                                                    //    mcptid = ConfigurationManager.AppSettings["oxKey"].ToString(),
                                                    //    currency = "SGD",
                                                    //    totalAmount = Convert.ToInt32(Convert.ToDecimal(objbusinessmpartener.convert_to_sgd(ViewState["currency_short_name"].ToString(), payable_amt).Tables[0].Rows[0][0].ToString()) * 100).ToString(),
                                                    //    referenceNo = "Digital Service Subscription_" + ViewState["massage_partner_sk"] + "_" + rnd.Next(999999),
                                                    //    statusUrl = Constants__.WEB_ROOT + "/partner-subscription_?code=oxpay_subscription&user=" + Session["email_id"].ToString() + "&refNo=" + Session["Provider_payment"],
                                                    //    returnUrl = Constants__.WEB_ROOT + "/partner-subscription_?code=oxpay_subscription&user=" + Session["email_id"].ToString() + "&refNo=" + Session["Provider_payment"],
                                                    //    itemDetail = "Subscription_" + ViewState["massage_partner_sk"],
                                                    //    customerEmail = Session["email_id"].ToString(),
                                                    //    tokenize = "Y"
                                                    //};
                                                    //var responseData = client.UploadString(ConfigurationManager.AppSettings["oxUrl"].ToString(), "POST", (JsonConvert.SerializeObject(request)).ToString());
                                                    //Response.Redirect(responseData, false);
                                                    string url = "http://payment.cubek.com?amount=" + objbusinessmpartener.convert_to_sgd(ViewState["currency_short_name"].ToString(), payable_amt).Tables[0].Rows[0][0].ToString() + "&user_sk=" + ViewState["massage_partner_sk"] + "&user_email=" + Session["email_id"].ToString() + "&refe=" + Session["Provider_payment"] + "&url=" + Constants__.WEB_ROOT + "/partner-subscription_";
                                                    Response.Redirect(url, false);
                                                    Context.ApplicationInstance.CompleteRequest();
                                                    return;
                                                }
                                                else if (Request.QueryString["code"] != null && Request.QueryString["code"].ToString() == "oxpay_subscription")
                                                {
                                                    string message = Request.Params["responseMsg"];
                                                    string resp = Request.Params["responseCode"];

                                                    if (message == "Approved" || resp == "00" || message == "completed successfully")
                                                    {
                                                        paypalReturnscreen.Visible = true; paypalGonescrren.Visible = false;
                                                        send_auto_activation_mail(Session["email_id"].ToString(), "P");
                                                        SavePromotion_AfterPaypal();
                                                    }
                                                    else
                                                    {
                                                        paypalReturnscreen.Visible = true; paypalGonescrren.Visible = false;
                                                        success_alert.Attributes.Remove("style");
                                                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "error('Whoops! Payment unsuccessful. Retry or use other payment method.');", true);
                                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_paypal1", "shifting_cards_methods('A');", true);
                                                        //DataSet ds = new DataSet();
                                                        divpaymentMode.Visible = true;

                                                        //paypalReturnscreen.Visible = true; paypalGonescrren.Visible = false;
                                                        //success_alert.Attributes.Remove("style");
                                                        //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "error('Whoops! Payment unsuccessful. Retry or use other payment method.');", true);
                                                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "script_paypal1", "shifting_cards_methods('A');", true);
                                                    }
                                                    return;
                                                }
                                                else if (Session["StripepaymentGatway"] != null && Session["StripepaymentGatway"].ToString() != "")
                                                {
                                                    string stripecarddetails = Session["StripepaymentGatway"].ToString();
                                                    Session.Remove("StripepaymentGatway");
                                                    SavePromotion_BeforePaypal();
                                                    string result_ = Go_for_Stripe_payment(ds, payable_amt, Convert.ToString(ViewState["currency_short_name"]), stripecarddetails);
                                                    Response.Redirect(result_, false);
                                                    return;
                                                    //if (result_ == "Paid")
                                                    //{
                                                    //    send_auto_activation_mail(Session["email_id"].ToString(), "S");
                                                    //    SavePromotion_AfterPaypal_Stripe();

                                                    //}
                                                    //else
                                                    //{
                                                    //    if (result_ == "NotPaid")
                                                    //    {
                                                    //        success_alert.Attributes.Remove("style");
                                                    //        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text2", "error('Whoops! Payment unsuccessful. Retry or use other payment method.');", true); ScriptManager.RegisterStartupScript(this, this.GetType(), "script_paypal2", "shifting_cards_methods('B');", true);
                                                    //    }
                                                    //    else
                                                    //    {
                                                    //        success_alert.Attributes.Remove("style");
                                                    //        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text1", "error('Whoops! Payment unsuccessful. Retry or use other payment method.');", true);
                                                    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_paypal3", "shifting_cards_methods('B');", true);
                                                    //    }
                                                    //    lblSuccess.Style.Add("color", "red");
                                                    //    Session["seeker_subscription"] = null;
                                                    //    paypalReturnscreen.Visible = true; paypalGonescrren.Visible = false;
                                                    //    divpaymentMode.Visible = true;



                                                    //    return;
                                                    //}
                                                }

                                                else
                                                {
                                                    if (ViewState["Exchange_rate"] != null)
                                                        str = objs.CurrencyConversion(payable_amt, ViewState["currency_short_name"].ToString(), "USD", ViewState["Exchange_rate"].ToString());
                                                    else
                                                        str = objs.CurrencyConversion(replaced_str, "USD", curr1, "1");


                                                    str = str.Normalize();
                                                    replaced_str = str.Replace(" USD", "");
                                                    if (replaced_str != "" && replaced_str != null)
                                                    {
                                                        string str_amt_2 = "";
                                                        if (ViewState["Exchange_rate"] != null)
                                                            str_amt_2 = objs.CurrencyConversion(replaced_str, "USD", curr1, ViewState["Exchange_rate"].ToString());
                                                        else
                                                            str_amt_2 = objs.CurrencyConversion(replaced_str, "USD", curr1, "1");
                                                        Session["Converted_Doller_amount"] = replaced_str;
                                                        SavePromotion_BeforePaypal();
                                                        SubmitToPaypal(replaced_str);
                                                    }
                                                }

                                            }
                                            catch (System.Exception ex)
                                            {
                                                BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message;
                                                Business.BussinessSendMail send = new BussinessSendMail();
                                                //   send.SendMail("info@massage2book.com", "support@massage2book.com", "Exception occured with webservice", ex.Message + " " + " On Subscription Page M2B, currency conversion api from  USD to '" + curr1 + "'", null);
                                            }

                                        }
                                    }
                                }
                            }
                        }
                        else
                        {


                            Response.Redirect(Constants__.WEB_ROOT, false);
                            return;
                        }
                    }
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

            tab_1.Visible = false;
            tab_2.Visible = false;

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        if (r["payment_method"].ToString() == "33")
                        {
                            tab_1.Visible = true;

                        }
                        if (r["payment_method"].ToString() == "32")
                        {
                            tab_2.Visible = true;

                        }

                    }
                }
                else
                {
                    if (country_sk == "100")
                    {
                        tab_1.Visible = true;
                        tab_2.Visible = true;
                    }
                    else
                    {
                        tab_1.Visible = true;
                        tab_2.Visible = true;
                    }
                }
            }
            else
            {
                if (country_sk == "100")
                {
                    tab_1.Visible = true;
                    tab_2.Visible = true;
                }
                else
                {
                    tab_1.Visible = true;
                    tab_2.Visible = true;
                }
            }
        }

        private void check_login(string email)
        {
            DataSet ds;
            ds = objbusinessmpartener.partner_login_oxpay(email);
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
            }
        }
        protected string Go_for_Stripe_payment(DataSet ds, string payable_amt, string SeekerCurrency, string carddetails)
        {

            try
            {
                ReadWriteWebservice objs = new ReadWriteWebservice();
                SeekerCurrency = SeekerCurrency.ToLower();
                //StripeCustomer current = GetCustomer(carddetails);
                if (ViewState["Exchange_rate"] != null && ViewState["Exchange_rate"].ToString() != "")
                    payable_amt = objs.CurrencyConversion(payable_amt, SeekerCurrency, "USD", ViewState["Exchange_rate"].ToString());
                else
                    payable_amt = objs.CurrencyConversion(payable_amt, SeekerCurrency, "USD", "1");

                SeekerCurrency = "usd";
                string url_ = (Request.Url).ToString();
                url_ = url_.Replace("?stripe=Sub_Cancel", "");
                url_ = url_.Replace("?stripe=Info2468Cubek", "");

                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeApiKey"].ToString();
                var options = new SessionCreateOptions
                {
                    LineItems = new List<SessionLineItemOptions>
                            {
                              new SessionLineItemOptions
                              {
                                PriceData = new SessionLineItemPriceDataOptions
                                {
                                  UnitAmount = Convert.ToInt32(Double.Parse(payable_amt)) * 100,
                                  Currency = SeekerCurrency.ToLower(),
                                  ProductData = new SessionLineItemPriceDataProductDataOptions
                                  {
                                    Name = "MyMassagePartner Digital Service",
                                  },

                                },
                                Quantity = 1,
                              },
                            },
                    Mode = "payment",
                    SuccessUrl = url_ + "?stripe=Info2468Cubek",
                    CancelUrl = url_ + "?stripe=Sub_Cancel",
                    CustomerEmail = Session["email_id"].ToString(),
                };

                var service = new SessionService();
                Session session = service.Create(options);

                //Response.Headers.Add("Location", session.Url);
                //return new StatusCodeResult(303);
                return session.Url;
            }
            catch (System.Exception ex)
            {
                return ex.Message;
                //   send.SendMail("info@massage2book.com", "support@massage2book.com", "Exception occured with webservice", ex.Message + " " + " On Subscription Page M2B, currency conversion api from  USD to '" + curr1 + "'", null);
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

                OrderID.Value = ds.Tables[0].Rows[0]["massage_partner_sk"].ToString() + "_" + rnd_no; ;
                Name.Value = ds.Tables[0].Rows[0]["massage_partner_name"].ToString();
                Address.Value = ds.Tables[0].Rows[0]["address_text"].ToString();
                Amount.Value = payable_amt;
                Mobile.Value = ds.Tables[0].Rows[0]["mobile_no"].ToString();
                Email.Value = ds.Tables[0].Rows[0]["email_id"].ToString();
                Zipcode.Value = ds.Tables[0].Rows[0]["postal_code"].ToString();
                City.Value = "";
                State.Value = "";
                Country.Value = "India"; ;
                SuccessURL.Value = Constants__.WEB_ROOT + "/User/CCAvanuePassPartner.aspx";
                CancelURL.Value = Constants__.WEB_ROOT + "/User/CCAvanueFailPArtner.aspx";
                frmpayment.Attributes.Add("method", "post");
                frmpayment.Attributes.Add("action", "http://shadimaker.com/payment/pay.php");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "scriptsa", " postbackurl();", true);
                return;
            }
            catch (System.Exception ex)
            {
                BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message;
                Business.BussinessSendMail send = new BussinessSendMail();
                Response.Redirect(Constants__.WEB_ROOT, false);
                return;
                //   send.SendMail("info@massage2book.com", "support@massage2book.com", "Exception occured with webservice", ex.Message + " " + " On Subscription Page M2B, currency conversion api from  USD to '" + curr1 + "'", null);
            }

        }
        protected void SavePromotion_AfterPaypal_CCAvanue()
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
                dc = new DataColumn("is_paid");
                dc.DataType = System.Type.GetType("System.String");
                dt.Columns.Add(dc);

                dc = new DataColumn("PaymentGateway");
                dc.DataType = System.Type.GetType("System.String");
                dt.Columns.Add(dc);


                dt = (DataTable)(Session["ServiceProvider_subscription"]);


                dt.Rows[0]["is_subscribed"] = "Y";
                dt.Rows[0]["is_paid"] = "Y";
                dt.Rows[0]["PaymentGateway"] = "CCAvanue";
                BussinessSubscription objSubscription = new BussinessSubscription();
                objbusinessmpartener.InsertmassagePartnerSubscriptionDetails(dt);
                lblSuccess.Text = "Information saved successfully";
                //  lblSuccess.Style.Add("color", "green");
                //  Session["ServiceProvider_subscription"] = null;
                //  ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "openOffersDialog();", true);
                Session["seeker_subscribed"] = "Y";
                if (Session["CurrentURL"] != null)
                    Response.Redirect(Session["CurrentURL"].ToString(), false);
                return;
                // Session["exists_promotion"] = "PromotionExists";
                //  ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp23", "<script type='text/javascript'>fun_noData_popup('Promotion Successfully Paid, Now upload promotion.');</script>", false);

            }
            else
            {
                lblSuccess.Text = "Payment not done";
                lblSuccess.Style.Add("color", "red");
                Session["ServiceProvider_subscription"] = null;

            }
        }
        protected void SavePromotion_AfterPaypal_Stripe()
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
                dc = new DataColumn("is_paid");
                dc.DataType = System.Type.GetType("System.String");
                dt.Columns.Add(dc);

                dc = new DataColumn("PaymentGateway");
                dc.DataType = System.Type.GetType("System.String");
                dt.Columns.Add(dc);


                dt = (DataTable)(Session["ServiceProvider_subscription"]);


                dt.Rows[0]["is_subscribed"] = "Y";
                dt.Rows[0]["is_paid"] = "Y";
                dt.Rows[0]["PaymentGateway"] = "Stripe";
                BussinessSubscription objSubscription = new BussinessSubscription();
                objbusinessmpartener.InsertmassagePartnerSubscriptionDetails(dt);
                lblSuccess.Text = "Information saved successfully";
                //  lblSuccess.Style.Add("color", "green");
                //  Session["ServiceProvider_subscription"] = null;
                //  ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "openOffersDialog();", true);
                Session["is_paid_partner"] = "Y";

                if (Session["CurrentURL"] != null)
                    Response.Redirect(Session["CurrentURL"].ToString(), false);
                return;
                // Session["exists_promotion"] = "PromotionExists";
                //  ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp23", "<script type='text/javascript'>fun_noData_popup('Promotion Successfully Paid, Now upload promotion.');</script>", false);

            }
            else
            {
                lblSuccess.Text = "Payment not done";
                lblSuccess.Style.Add("color", "red");
                Session["ServiceProvider_subscription"] = null;

            }
        }
        protected void SavePromotion_AfterPaypal()
        {
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
                BussinessSubscription objSubscription = new BussinessSubscription();
                objbusinessmpartener.InsertmassagePartnerSubscriptionDetails(dt);
                lblSuccess.Text = "Information saved successfully";
                //  lblSuccess.Style.Add("color", "green");
                //  Session["ServiceProvider_subscription"] = null;
                //  ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "openOffersDialog();", true);
                Session["seeker_subscribed"] = "Y";
                if (Session["CurrentURL"] != null)
                    Response.Redirect(Session["CurrentURL"].ToString(), false);

                return;



                // Session["exists_promotion"] = "PromotionExists";
                //  ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp23", "<script type='text/javascript'>fun_noData_popup('Promotion Successfully Paid, Now upload promotion.');</script>", false);

            }
            else
            {
                lblSuccess.Text = "Payment not done";
                lblSuccess.Style.Add("color", "red");
                Session["ServiceProvider_subscription"] = null;

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
                BussinessSubscription objSubscription = new BussinessSubscription();
                objbusinessmpartener.InsertmassagePartnerSubscriptionDetails(dt);

            }
            else
            {
                lblSuccess.Text = "Payment not done";
                lblSuccess.Style.Add("color", "red");
                Session["ServiceProvider_subscription"] = null;

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
        public void ShowError(string ErrorMessage)
        {
            try
            {
                this.lblSuccess.Text = ErrorMessage + "<p>";
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


                PayPal.LogoUrl = "https://cdn.mymassagepartner.com/image/mp_logo.png";

                PayPal.ItemName = "Subscription Amount #" + new Guid().GetHashCode().ToString("x");

                // *** Have paypal return back to this URL


                string url_ = (Request.Url).ToString();
                url_ = url_.Replace("?PayPal=Sub_Cancel", "");
                url_ = url_.Replace("?PayPal=Info2468Cubek", "");
                PayPal.SuccessUrl = url_ + "?PayPal=Info2468Cubek";
                PayPal.CancelUrl = url_ + "?PayPal=Sub_Cancel";
                //append currecy code  //it is comming form table.
                int country_sk = 0;
                if (ViewState["country_sk"] != null)
                    country_sk = Convert.ToInt32(ViewState["country_sk"]);


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
        protected void paypal1_Click(object sender, EventArgs e)
        {
            Session.Remove("3dpaymentSeeker");
            Utils.Mails mail = new Utils.Mails();
            mail.payNow_Click_Mail(Session["country_sk"].ToString(), Session["massage_partner_sk"].ToString());
            if (Session["massage_partner_sk"] != null)
            {
                DataSet ds = new DataSet();
                int? Banksk = null;
                int? cardsk = null;
                ds = objBusinessLogin.Insert_payment_bridge(Convert.ToInt32(Session["pay_partner"].ToString()), "sp", Banksk, cardsk, "");
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
            //Session["CurrentURL"] = HttpContext.Current.Request.Url.AbsoluteUri;
            Response.Redirect(Constants__.WEB_ROOT + "/partner-subscription_", false);
            //Session["Seeker_payment"] = price1.InnerText + "|" + (checkboxOneYear.Checked ? "1" : (checkboxTwoYear.Checked ? "2" : "3"));
            return;

        }
        protected void SubmitStripe_Click(object sender, EventArgs e)
        {
            Session.Remove("3dpaymentSeeker");
            Utils.Mails mail = new Utils.Mails();
            mail.payNow_Click_Mail(Session["country_sk"].ToString(), Session["massage_partner_sk"].ToString());
            try
            {
                Response.Redirect(Constants__.WEB_ROOT + "/partner-subscription_", false);
                Session["StripepaymentGatway"] = "stripe";
                return;
            }
            catch (Exception ex)
            {
                lblmassage.Text = ex.Message;
                lblmassage.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void btnsendtransection_Click(object sender, EventArgs e)
        {
            BussinessSendMail objmail = new BussinessSendMail();

            string pagedetails = "Massage partner sk - " + Session["massage_partner_sk"].ToString() + ", Partner Sk: " + Session["pay_partner"].ToString() + "  Email Id - " + txtregisteredmail_id.Text.Trim() + "       Paytm transaction iD " + ctxttransaction.Text.Trim();

            int i = objmail.SendMail("info@mymassagepartner.com", "info@mymassagepartner.com", "Paytm transection ID " + ctxttransaction.Text.Trim(), pagedetails, null);

            if (i == -1)
            {
                lblpaytmmsg.Visible = true;
                lblpaytmmsg.Text = "Details sent successfully. Your membership will be activated in next moment.";
            }


        }
        protected void btnNetbanking_Click(object sender, EventArgs e)
        {

            BussinessSendMail objMail = new BussinessSendMail();
            //string one_year = ConfigurationManager.AppSettings["Seeker_sub_india_1_year"].ToString();
            //string two_year = ConfigurationManager.AppSettings["Seeker_sub_india_2_year"].ToString();
            //string three_year = ConfigurationManager.AppSettings["Seeker_sub_india_3_year"].ToString(); 
            //  string message = "Dear User,<br/><br/>We appreciate and thank you for your positive approach. You can pay <b>Massage2Book</b> membership fee to company bank account via <b>Net banking or Cash bank transfer<b/>.<br/><br/> See the membership plans and pay the same amount.<br/><br/>1 year membership - Rs "+one_year+"<br/>2 years membership - Rs "+two_year+"(After Discount)<br/>3 years membership - Rs "+three_year+"(After Discount)<br/><br/> <span style='color:red'><b>Important</b></span>: Once paid, send the transaction ID/reference ID or payment receipt image/screenshot immediately to <span style='color:blue'><u><b>info@massage2book.com</b></u></span> from your Massage2Book.com registered email ID. We will upgrade your membership within <b>15 minutes (maximum)</b>.<br/><br/> Please find below the bank details:<br/><br/> 1. Account Name (Name of the account Holder):           Cubek Technologies<br/>2. Account Number                                                         912020031902382<br/>3. Bank Name                                                                     AXIS BANK<br/>4. IFSC Code                                                                    UTIB0000568<br/>5. Account Type                                                                     Current<br/><br/>If any query/questions please feel free to contact us.<br/><br/><span style='color:red'><b>Note</b></span>: <u>Massage2Book.com is owned by CubeK Technologies, India.</u><br/><br/>Best Regards<br/>Massage2Book Team";
            string message = "Required Bank detail for membership for Partner Payment, Sk: " + Session["pay_partner"].ToString() + ", Customer email id : " + Session["email_id"].ToString();
            objMail.sender = ConfigurationManager.AppSettings["EmailTable2Book"].ToString();
            // objMail.sender = "hrtpandey90@gmail.com";
            objMail.Mrecipients = ConfigurationManager.AppSettings["EmailTable2Book"].ToString();
            //objMail.Mrecipients = "hrtpandey90@gmail.com";
            objMail.Mbody = message;
            objMail.Msubject = "Required Bank detail for membership (For Partner)";
            objMail.SendMail();
            lblNetBankingmsg.Visible = true;
            btnNetbanking.Enabled = false;
        }
        protected void btnQr_Click(object sender, EventArgs e)
        {
            BussinessSendMail objmail = new BussinessSendMail();

            string pagedetails = "Massage partner sk - " + Session["massage_partner_sk"].ToString() + ", Partner Sk: " + Session["pay_partner"].ToString() + "  Email Id - " + txtqr_mail_id.Text.Trim() + "   QR Payment transaction iD " + txtQrTransaction.Text.Trim();

            int i = objmail.SendMail(ConfigurationManager.AppSettings["EmailTable2Book"].ToString(), ConfigurationManager.AppSettings["EmailTable2Book"].ToString(), "QR Payment transection ID " + txtQrTransaction.Text.Trim(), pagedetails, null);

            if (i == -1)
            {
                lblQrTransaction.Visible = true;
                lblQrTransaction.Text = "We will verify and activate your membership.";
            }
        }
        private void send_auto_activation_mail(string email_id, string type)
        {
            string message = "";
            BussinessSendMail objMail = new BussinessSendMail();
            if (type == "P")
                message = "Dear User,<br/><br/>Congratulations!<br/><br/>Your MyMassagePartner membership account has been activated now.<br/><br/><span style='color:red !important'><b>Please note:</b></span> You have made payment to our company account <b>" + ConfigurationManager.AppSettings["paypal_account_name"].ToString() + "</b>.<br/><br/> Please do login at https://www.mymassagepartner.com/login and find your massage partner for your desired location.<br/><br/>In case of any refund/dispute issue just reply to this email with 'refund reason' and we will try to resolve the issue or refund immediately.<br/><br/>Best Regards<br/>MyMassagePartner Team";
            else if (type == "S")
                message = "Dear User,<br/><br/>Congratulations!<br/><br/>Your MyMassagePartner membership account has been activated now.<br/><br/><span style='color:red !important'><b>Please note:</b></span> You have made payment to our company account <b>" + ConfigurationManager.AppSettings["stripe_account_name"].ToString() + "</b>.<br/><br/> Please do login at https://www.mymassagepartner.com/login and find your massage partner for your desired location.<br/><br/>In case of any refund/dispute issue just reply to this email with 'refund reason' and we will try to resolve the issue or refund immediately.<br/><br/>Best Regards<br/>MyMassagePartner Team";
            else
                message = "Dear User,<br/><br/>Congratulations!<br/><br/>Your MyMassagePartner membership account has been activated now.<br/><br/><span style='color:red !important'><b>Please note:</b></span> You have made payment to our company account <b>CubeK Technologies</b>.<br/><br/> Please do login at https://www.mymassagepartner.com/login and find your massage partner for your desired location.<br/><br/>In case of any refund/dispute issue just reply to this email with 'refund reason' and we will try to resolve the issue or refund immediately.<br/><br/>Best Regards<br/>MyMassagePartner Team";

            objMail.sender = ConfigurationManager.AppSettings["EmailTable2Book"].ToString();
            objMail.Mrecipients = email_id;
            objMail.Mbody = message;
            objMail.Msubject = "Membership activated";
            objMail.SendMail();
        }
        private void create_WeChat_source()
        {
            var options = new SourceCreateOptions
            {
                Type = SourceType.AchCreditTransfer,
                Currency = "usd",
                Owner = new SourceOwnerOptions
                {
                    Email = "jenny.rosen@example.com"
                }
            };

            var service = new SourceService();
            Source source = service.Create(options);

            var option = new ChargeCreateOptions
            {
                Amount = 1099,
                Currency = "usd",
                Source = source.Id,
            };

            var service_charge = new ChargeService();
            Charge charge = service_charge.Create(option);
        }
        private void update_ptmLink()
        {
            BusinessMPAdmin objadmin = new BusinessMPAdmin();
            DataSet ds = objadmin.get_payment_parameters();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lnkPaymtn.Text = ds.Tables[0].Rows[0]["parameter_value"].ToString().Trim();
                    lnkPaymtn.Attributes.Add("href", ds.Tables[0].Rows[0]["parameter_value"].ToString().Trim());
                }
            }
        }

        protected void btnOxPay_Click(object sender, EventArgs e)
        {
            Session.Remove("3dpaymentSeeker");
            Session["oxPay"] = "Yes";
            Utils.Mails mail = new Utils.Mails();
            mail.payNow_Click_Mail(Session["country_sk"].ToString(), Session["massage_partner_sk"].ToString());
            Response.Redirect(Constants__.WEB_ROOT + "/partner-subscription_", false);
            return;
        }
    }
}
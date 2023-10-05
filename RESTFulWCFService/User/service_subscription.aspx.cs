﻿using System;
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
using CCA.Util;
using RESTFulWCFService;
using System.Collections.Generic;
using System.Collections.Specialized;
using Stripe;
using System.Net;
using System.Web.Services;


namespace RESTFulWCFService.User
{
    public partial class service_subscription : System.Web.UI.Page
    {
        private bool PayPalReturnRequest = false;
        protected decimal OrderAmount = 0.00M;
        BusinessMPartener objbusinessmpartener = new BusinessMPartener();
        BussinessPaypal obj = new BussinessPaypal();
        RegistrationBusiness objRegistrationBusiness = new RegistrationBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            lnkBack.PostBackUrl = Constants__.WEB_ROOT;
            if (Session["email_id"]!=null)
            {
                txtregisteredmail_id.Text=Session["email_id"].ToString();
               // txtregisteredmail_id.ReadOnly=true;
                partner_sk.Value = Session["massage_partner_sk"].ToString();
            }
            if (Session["country_sk"] != null)
            {
                if (Session["country_sk"].ToString() != "100")
                {
                  //  div_India_User.Visible = false;
                }
            }
            if (Session["CurrentURL"] != null)
            { Qucikhome.HRef = Session["CurrentURL"].ToString(); }
            else
            { Qucikhome.HRef = Constants__.WEB_ROOT; }
            if (!IsPostBack)
            {
                if (Session["mp_login_sk"] != null)
                {
                    CCACrypto func = new CCACrypto();
                    if (Request.QueryString["PayPal"] != null && Request.QueryString["PayPal"].ToString() == "Info1347Cubek")
                    {
                        paypalReturnscreen.Visible = true;
                        paypalGonescrren.Visible = false;
                        SavePromotion_AfterPaypal();
                        return;
                    }
                    else if (Request.QueryString["PayPal"] != null && Request.QueryString["PayPal"].ToString() == "Cubek2468Tech")
                    {
                        paypalReturnscreen.Visible = true;
                        paypalGonescrren.Visible = false;
                        SavePromotion_AfterPaypal_ccavanue();
                        return;
                    }
                    else if (Request.QueryString["PayPal"] != null && Request.QueryString["PayPal"].ToString() == "Sub_Cancel")
                    {
                        paypalReturnscreen.Visible = true;
                        paypalGonescrren.Visible = false;
                        lblSuccess.InnerText = "Payment not done due to some technical issue.";
                        lblSuccess.Style.Add("color", "red");
                    }
                    else
                    {
                        paypalReturnscreen.Visible = false; paypalGonescrren.Visible = true;
                        Session["seeker_subscribed"] = null;

                        int login_sk = Convert.ToInt32(Session["mp_login_sk"].ToString());

                        int i = 0;
                        string Subscription_price = "0";

                        System.Data.DataSet ds = new DataSet();
                        Business.BusinessLogin objBusinessLogin = new BusinessLogin();
                        ds = objbusinessmpartener.getPartnerSubsciption_record(Convert.ToInt32(Session["massage_partner_sk"].ToString()));
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Session["UserFirstName"] = ds.Tables[0].Rows[0]["massage_partner_name"].ToString();
                            ViewState["country_sk"] = ds.Tables[0].Rows[0]["country_sk"].ToString();
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
                                            ViewState["Seeker_subscrition_price"] = Get_Listing_Price(ds.Tables[4].Rows[0]["basic_unit_price"].ToString(), Two_year_per);
                                        }
                                        else if (paymentforyear == 3)
                                        {
                                            ViewState["Seeker_subscrition_price"] = Get_Listing_Price(ds.Tables[4].Rows[0]["basic_unit_price"].ToString(), Three_year_per);
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

                                    if (ds.Tables[4].Rows[0]["currency_short_name"].ToString() == "INR")
                                    {
                                        ViewState["currency_short_name"] = ds.Tables[4].Rows[0]["currency_short_name"];
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
                                                ViewState["Seeker_subscrition_price"] = Get_Listing_Price(ds.Tables[5].Rows[0]["basic_unit_price"].ToString(), Convert.ToDecimal(Two_year_per));
                                            }
                                            else if (paymentforyear == 3)
                                            {
                                                ViewState["Seeker_subscrition_price"] = Get_Listing_Price(ds.Tables[5].Rows[0]["basic_unit_price"].ToString(), Convert.ToDecimal(Three_year_per));

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
                                            ViewState["currency_short_name"] = "USD";
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
                            i = objmail.sendMail(login_sk, "Massage Partner Pay Now Click event", "user (Massage Partner SK= " + ProviderId + ") is clicked for paypal for massage-partner subscription", null);
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
                                        if (ViewState["currency_short_name"] == "USD" && Session["StripepaymentGatway"] == null)
                                        {
                                            if (ViewState["Exchange_rate"] != null)
                                                str = objs.CurrencyConversion(payable_amt, "USD", "USD", ViewState["Exchange_rate"].ToString());
                                            else
                                                str = objs.CurrencyConversion(payable_amt, "USD", "USD", "1");
                                        }
                                        else if (Convert.ToString(ViewState["currency_short_name"]) == "INR" && Session["ccAvanue"] != null && Session["ccAvanue"].ToString() == "Y")
                                        {
                                            Session.Remove("ccAvanue");
                                            SavePromotion_BeforePaypal();
                                            Go_for_payment(ds, payable_amt); return;
                                        }
                                        else if (Session["StripepaymentGatway"] != null && Session["StripepaymentGatway"].ToString() != "")
                                        {
                                            string stripecarddetails = Session["StripepaymentGatway"].ToString();
                                            Session.Remove("StripepaymentGatway");
                                            SavePromotion_BeforePaypal();
                                            string result_ = Go_for_Stripe_payment(ds, payable_amt, Convert.ToString(ViewState["currency_short_name"]), stripecarddetails);

                                            if (result_ == "Paid")
                                            {
                                                SavePromotion_AfterPaypal_Stripe();
                                                return;
                                            }
                                            else
                                            {
                                                if (result_ == "NotPaid")
                                                { //lblerror_stripe.InnerHtml = "NotPaid";
                                                }
                                                else
                                                {
                                                    lblerror_stripe.InnerHtml = result_;
                                                    lblerror_stripe.Visible = true;
                                                    lblerror_stripe.Style.Add("color", "red");
                                                }
                                                lblerror_stripe.Style.Add("color", "red");
                                                Session["seeker_subscription"] = null;
                                                paypalReturnscreen.Visible = true; paypalGonescrren.Visible = false;
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            if (ViewState["Exchange_rate"] != null)
                                                str = objs.CurrencyConversion(payable_amt, ViewState["currency_short_name"].ToString(), "USD", ViewState["Exchange_rate"].ToString());
                                            else
                                                str = objs.CurrencyConversion(replaced_str, "USD", curr1, "1");
                                        }
                                        str = str.Normalize();
                                        replaced_str = str.Replace(" USD", "");
                                        if (replaced_str != "" && replaced_str != null)
                                        {
                                            string str_amt_2 = "";
                                            if (ViewState["Exchange_rate"] != null)
                                                str_amt_2 = objs.CurrencyConversion(replaced_str, "USD", curr1, ViewState["Exchange_rate"].ToString());
                                            else
                                                str_amt_2 = objs.CurrencyConversion(replaced_str, "USD", curr1, "1");
                                        }
                                        SavePromotion_BeforePaypal();
                                    }
                                    catch (System.Exception ex)
                                    {
                                        BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message;
                                        Business.BussinessSendMail send = new BussinessSendMail();
                                        //   send.SendMail("info@massage2book.com", "support@massage2book.com", "Exception occured with webservice", ex.Message + " " + " On Subscription Page M2B, currency conversion api from  USD to '" + curr1 + "'", null);
                                    }
                                    Session["Converted_Doller_amount"] = replaced_str;


                                   
                                    //save with paypal    
                                    SubmitToPaypal(replaced_str);
                                }
                            }
                        }
                    }
                }
                else if (Session["Merchant_Id"] != null && Session["Encrypted"] != null && Session["Postbackurl_CCAvanueEncrypted"] != null && Session["CCAvanueDetails"] != null)
                {
                    string lblMerchantId = Session["Merchant_Id"].ToString();
                    string Encrypted = Session["Encrypted"].ToString();
                    Merchant_Id.Value = lblMerchantId;
                    encRequest.Value = Encrypted;

                    DataTable dt = (DataTable)Session["CCAvanueDetails"];


                    OrderID.Value = dt.Rows[0][0].ToString();
                    Amount.Value = dt.Rows[0][1].ToString();
                    Name.Value = dt.Rows[0][2].ToString();
                    Address.Value = dt.Rows[0][3].ToString();
                    Country.Value = dt.Rows[0][4].ToString();
                    Mobile.Value = dt.Rows[0][5].ToString();
                    Email.Value = dt.Rows[0][6].ToString();
                    Zipcode.Value = dt.Rows[0][7].ToString();
                    City.Value = "";
                    State.Value = "";

                    SuccessURL.Value = dt.Rows[0][8].ToString();
                    CancelURL.Value = dt.Rows[0][9].ToString();

                    Session["Merchant_Id"] = null;
                    Session["Encrypted"] = null;
                    Session["Postbackurl_CCAvanueEncrypted"] = null;
                    Session["CCAvanueDetails"] = null;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script23", " postbackurl();", true);
                    return;
                }
                else
                {


                    Response.Redirect(Constants__.WEB_ROOT, false);
                    return;
                }



            }

        }

        protected string Go_for_Stripe_payment(DataSet ds, string payable_amt, string SeekerCurrency, string carddetails)
        {

            try
            {
                if (SeekerCurrency == "OMR" || SeekerCurrency == "KWD")
                {
                    if (SeekerCurrency == "KWD")
                    {
                        SeekerCurrency = "SGD";
                        payable_amt = (Double.Parse(payable_amt) * 4.53).ToString();
                    }
                    else
                        if (SeekerCurrency == "OMR")
                        {
                            SeekerCurrency = "SGD";
                            payable_amt = (Double.Parse(payable_amt) * 3.57).ToString();
                        }
                }
                return "";
            }
            catch (System.Exception ex)
            {
                return ex.Message;
                //   send.SendMail("info@massage2book.com", "support@massage2book.com", "Exception occured with webservice", ex.Message + " " + " On Subscription Page M2B, currency conversion api from  USD to '" + curr1 + "'", null);
            }

        }


        //private StripeCustomer GetCustomer(string carddetails)
        //{

        //    string[] carddetails_ = carddetails.Split('|');
        //    var mycust = new StripeCustomerCreateOptions();
        //    mycust.Email = "";
        //    mycust.Description = "";
        //    mycust.CardNumber = carddetails_[0].Trim();
        //    mycust.CardExpirationMonth = carddetails_[1].Trim();
        //    mycust.CardExpirationYear = carddetails_[2].Trim();
        //    // mycust.PlanId = "100";
        //    mycust.CardName = carddetails_[3].Trim();
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
                string WorkingKey = "u6h1vr4vvidn2nue9e";
                Random rnd = new Random();
                int rnd_no = rnd.Next(100, 100000);
                string lblOrderId = ds.Tables[0].Rows[0]["massage_partner_sk"].ToString() + "_" + rnd_no;
                string lblAmount = payable_amt;



                string lblRedirectUrl = (Request.Url).ToString();
                lblRedirectUrl = lblRedirectUrl.Replace("?PayPal=Sub_Cancel", "");
                lblRedirectUrl = lblRedirectUrl.Replace("?PayPal=Info2468Cubek", "");

                string lblCustomerName = ds.Tables[0].Rows[0]["massage_partner_name"].ToString();
                string lblCustAddr = ds.Tables[0].Rows[0]["address_text"].ToString();
                string lblCustCountry = "India";
                string lblCustPhone = ds.Tables[0].Rows[0]["mobile_no"].ToString();
                string lblCustEmail = ds.Tables[0].Rows[0]["email_id"].ToString();
                string lblCustState = "";
                string lblCustCity = "";
                string lblZipCode = ds.Tables[0].Rows[0]["postal_code"].ToString();

                string lblRedirectUrlFail = Constants__.WEB_ROOT + "/User/CCAvanuePassPartner.aspx";
                string lblRedirectUrlsuccess = Constants__.WEB_ROOT + "/User/CCAvanueFailPArtner.aspx";



                OrderID.Value = lblOrderId;
                Amount.Value = lblAmount;
                Name.Value = lblCustomerName;
                Address.Value = lblCustAddr;
                Country.Value = lblCustCountry;
                Mobile.Value = lblCustPhone;
                Email.Value = lblCustEmail;
                Zipcode.Value = lblZipCode;
                City.Value = "";
                State.Value = "";

                SuccessURL.Value = lblRedirectUrlsuccess;
                CancelURL.Value = lblRedirectUrlFail;

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

                string lblMerchantId = "M_con20011_20011";
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script23564", " postbackurl();", true);
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



        protected void SavePromotion_AfterPaypal_ccavanue()
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
                lblSuccess.InnerText = "Information saved successfully";
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
                lblSuccess.InnerText = "Payment not done";
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
                lblSuccess.InnerText = "Information saved successfully";
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
                lblSuccess.InnerText = "Payment not done";
                lblSuccess.Style.Add("color", "red");
                Session["ServiceProvider_subscription"] = null;

            }
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
                lblSuccess.InnerText = "Information saved successfully";
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
                lblSuccess.InnerText = "Payment not done";
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
                lblSuccess.InnerText = "Payment not done";
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
                this.lblSuccess.InnerText = ErrorMessage + "<p>";
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

        //protected void btnsend_Click1(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int status = 0;
        //        BussinessSendMail objmail = new BussinessSendMail();

        //        objmail.Mrecipients = ConfigurationManager.AppSettings["EmailTable2Book"].ToString();
        //        objmail.Mbody = "User (Email id: " + txtregisteredmail_id.Text + ") done payment using Paytm.<br/><br/>Transaction ID: " + txttransaction.Text + "<br/>Partner SK is: " + Session["massage_partner_sk"].ToString() + "";
        //        objmail.Msubject = "Massage Partner Subscription | User Subscription Information";
        //        status = objmail.SendMail();
        //        txttransaction.Text = "";
        //        lblresult.Visible = true;
        //        lblresult.Text = "information submitted successfully!";
        //    }
        //    catch (System.Exception ex)
        //    {
        //        BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message;
        //        var st = new System.Diagnostics.StackTrace(ex, true);
        //        StackFrame[] stackFrames = st.GetFrames();
        //        foreach (StackFrame stackFrame in stackFrames)
        //        {
        //            Console.WriteLine(stackFrame.GetMethod().Name);   // write method name
        //            BussinessEntity.ExceptionHandling._lineno = stackFrame.GetFileLineNumber();
        //            BussinessEntity.ExceptionHandling._methodname = Convert.ToString(stackFrame.GetMethod().Name);
        //            BussinessEntity.ExceptionHandling._pagename = Convert.ToString(Request.Url.AbsoluteUri);

        //        }

        //        Response.Redirect(Constants__.WEB_ROOT + "/ErrorMessage.aspx", false);
        //    }
        //}

        [WebMethod]                                 //Default.aspx.cs
        public static void Send_Request_mail(string id,string trans_no,string sk)
        {
                int status = 0;
                BussinessSendMail objmail = new BussinessSendMail();

                objmail.Mrecipients = ConfigurationManager.AppSettings["EmailPayment"].ToString();
                objmail.Mbody = "User (Email id: " + id + ") done payment using Paytm.<br/><br/>Transaction ID: " + trans_no + "<br/>Partner SK is: " + sk + "";
                objmail.Msubject = "Massage Partner Subscription | User Subscription Information";
                status = objmail.SendMail();
                //txttransaction.Text = "";
                //lblresult.Visible = true;
                //lblresult.Text = "information submitted successfully!";
            
        }
        //protected void paypal_Click(object sender, EventArgs e)
        //{
        //    //try
        //    {

        //        int login_sk = Convert.ToInt32(Session["login_sk"].ToString());

        //        int i = 0;
        //        string Subscription_price = "0";

        //        System.Data.DataSet dsgetSeeker = new DataSet();
        //        dsgetSeeker = objRegistrationBusiness.GetSeekerLoginsk((Session["massage_partner_sk"]).ToString());
        //        if (dsgetSeeker.Tables[0].Rows.Count >= 1)
        //        {
        //            int ProviderId = Convert.ToInt32(dsgetSeeker.Tables[0].Rows[0]["massage_partner_sk"]);



        //            DataTable dt = new DataTable();
        //            DataColumn dc;
        //            dc = new DataColumn("massage_partner_sk");
        //            dc.DataType = System.Type.GetType("System.Int32");
        //            dt.Columns.Add(dc);


        //            dc = new DataColumn("subscription_start_date");
        //            dc.DataType = System.Type.GetType("System.String");
        //            dt.Columns.Add(dc);

        //            dc = new DataColumn("subscription_end_date");
        //            dc.DataType = System.Type.GetType("System.String");
        //            dt.Columns.Add(dc);


        //            dc = new DataColumn("one_time_price");
        //            dc.DataType = System.Type.GetType("System.String");
        //            dt.Columns.Add(dc);

        //            dc = new DataColumn("is_subscribed");
        //            dc.DataType = System.Type.GetType("System.String");
        //            dt.Columns.Add(dc);


        //            DateTime dsfrom = System.DateTime.Now;
        //            DateTime dsto = dsfrom.AddYears(1);

        //            Subscription_price = ViewState["Seeker_subscrition_price"].ToString();
        //            // ViewState["currency_short_name"] = "USD";
        //            dt.Rows.Add(ProviderId, dsfrom, dsto, Subscription_price, "Y");
        //            Session["ServiceProvider_subscription"] = dt;

        //            //  i = objmail.sendMail(login_sk, "PPF Pay Now Click event", "user (serice seeker sk= " + ProviderId + ") is clicked for paypal for registration subscription", null);
        //        }
        //        string replaced_str = "";
        //        string curr1 = "";
        //        string payable_amt = Subscription_price;
        //        int is_paypal = obj.is_paypal();

        //        if (is_paypal == 1)
        //        {
        //            if (Subscription_price != "0.00")
        //            {


        //                if (payable_amt != "0.00")
        //                {
        //                    int country_sk = 0;
        //                    string str = "";
        //                    if (ViewState["currency_short_name"] != null)
        //                    {
        //                        curr1 = Convert.ToString(ViewState["currency_short_name"]);
        //                    }
        //                    try
        //                    {
        //                        if (ViewState["country_sk"] != null)
        //                            country_sk = Convert.ToInt32(ViewState["country_sk"]);

        //                        ReadWriteWebservice objs = new ReadWriteWebservice();
        //                        if (ViewState["currency_short_name"] == "USD")
        //                        {
        //                            str = objs.CurrencyConversion(payable_amt, "USD", "USD");
        //                        }
        //                        else
        //                        {
        //                            str = objs.CurrencyConversion(payable_amt, ViewState["currency_short_name"].ToString(), "USD");
        //                        }
        //                        str = str.Normalize();
        //                        replaced_str = str.Replace(" USD", "");
        //                        if (replaced_str != "" && replaced_str != null)
        //                        {
        //                            string str_amt_2 = objs.CurrencyConversion(replaced_str, "USD", curr1);
        //                        }
        //                    }
        //                    catch (System.Exception ex)
        //                    {
        //                        BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message;
        //                        Business.BussinessSendMail send = new BussinessSendMail();
        //                        //   send.SendMail("info@massage2book.com", "support@massage2book.com", "Exception occured with webservice", ex.Message + " " + " On Subscription Page M2B, currency conversion api from  USD to '" + curr1 + "'", null);
        //                    }
        //                    //lbl_curr_2.Text = curr1;
        //                    Session["Converted_Doller_amount"] = replaced_str;



        //                    //save with paypal    
        //                    SubmitToPaypal(replaced_str);
        //                    //  SavePromotion();
        //                }
        //                else
        //                { //save witout paypal
        //                    //  SavePromotion();
        //                }

        //            }
        //            else
        //            {
        //                //save witout paypal
        //                //   SavePromotion();
        //            }
        //        }
        //        else
        //        {
        //            //save witout paypal
        //            // SavePromotion();
        //        }
        //    }

        //    //catch (System.Exception ex)
        //    //{
        //    //    BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message;
        //    //    var st = new System.Diagnostics.StackTrace(ex, true);
        //    //    StackFrame[] stackFrames = st.GetFrames();
        //    //    foreach (StackFrame stackFrame in stackFrames)
        //    //    {
        //    //        Console.WriteLine(stackFrame.GetMethod().Name);   // write method name
        //    //        BussinessEntity.ExceptionHandling._lineno = stackFrame.GetFileLineNumber();
        //    //        BussinessEntity.ExceptionHandling._methodname = Convert.ToString(stackFrame.GetMethod().Name);
        //    //        BussinessEntity.ExceptionHandling._pagename = Convert.ToString(Request.Url.AbsoluteUri);

        //    //    }

        //    //    Response.Redirect(Constants__.WEB_ROOT + "/ErrorMessage.aspx", false);
        //    //}
        //}
    }
}
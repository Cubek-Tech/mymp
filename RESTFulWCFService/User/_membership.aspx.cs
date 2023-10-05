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
using System.Web.Services;

namespace RESTFulWCFService.User
{
    public partial class _membership : System.Web.UI.Page
    {
        DataTable DT = new DataTable();
        BussinessPaypal obj = new BussinessPaypal();
        BussinessSendMail objmail = new BussinessSendMail();
        private bool PayPalReturnRequest = false;
        protected decimal OrderAmount = 0.00M;

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
            
           
            if (Session["mp_login_sk"] != null)
            {
                if (!IsPostBack)
                {
                    select_QR();
                }
                HtmlGenericControl Divid = (HtmlGenericControl)Page.Master.FindControl("lnkmembership");
                Divid.Visible = false;

                HtmlGenericControl Divid1 = (HtmlGenericControl)Page.Master.FindControl("lnkmembership1");
                Divid1.Visible = false;

                payment_fill();
                button_type();
                if (Session["country_sk"] != null && Session["country_sk"].ToString() == "100")
                {
                    lbl_1_year.Text = "Rs. " + ConfigurationManager.AppSettings["Partner_sub_india_1_year"].ToString();
                    lbl_2_year.Text = "Rs. " + ConfigurationManager.AppSettings["Partner_sub_india_2_year"].ToString();
                    lbl_3_year.Text = "Rs. " + ConfigurationManager.AppSettings["Partner_sub_india_3_year"].ToString();
                    txtregisteredmail_id.Text = Session["email_id"].ToString();
                }
                else
                {
                    paytm_option.Visible = false;
                }
                // txtregisteredmail_id.ReadOnly=true;
                partner_sk.Value = Session["massage_partner_sk"].ToString();
            }
            else
            {
                Response.Redirect("https://www.mymassagepartner.com", false);
            }
        }

        #region paymentMethods
        [WebMethod]                                 //Default.aspx.cs
        public static void Send_Request_mail(string id, string trans_no, string sk)
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
        private void payment_fill()
        {
            DataSet ds = new DataSet();
            ds = objbusinessmpartener.getPartnerSubsciption(Convert.ToInt32(Session["country_sk"].ToString()));
            //ds = objbusinessmpartener.getPartnerSubsciption(Convert.ToInt32("220"));


            if (ds.Tables[1].Rows.Count > 0)
            {
                ViewState["Seeker_subscrition_price"] = Math.Round(decimal.Parse(ds.Tables[1].Rows[0]["basic_unit_price"].ToString()), 2);
                // ViewState["currency_short_name"] = ds.Tables[3].Rows[0]["currcountry_skency_short_name"];
                if (ds.Tables[1].Rows[0]["currency_short_name"].ToString() == "INR")
                {
                    price.InnerText = "Rs. " + Get_Listing_Price(ds.Tables[1].Rows[0]["basic_unit_price"].ToString(), 0);



                    if (ConfigurationManager.AppSettings["Provider_subscription_default_year"].ToString() == "1")
                    {
                        price.InnerText = "Rs. " + " " + Get_Listing_Price(ds.Tables[1].Rows[0]["basic_unit_price"].ToString(), 0);
                        checkboxOneYear_provider.Checked = true;
                        checkboxTwoYear_provider.Checked = false;
                        checkboxThreeYear_provider.Checked = false;
                        lblyearsub.InnerText = "1 Year";
                    }
                    else if (ConfigurationManager.AppSettings["Provider_subscription_default_year"].ToString() == "2")
                    {
                        price.InnerText = "Rs. " + " " + Get_Listing_Price(ds.Tables[1].Rows[0]["basic_unit_price"].ToString(), Convert.ToDecimal(ConfigurationManager.AppSettings["Provider_india_2_year_per_partner"].ToString()));
                        checkboxOneYear_provider.Checked = false;
                        checkboxTwoYear_provider.Checked = true;
                        checkboxThreeYear_provider.Checked = false;
                        lblyearsub.InnerText = "2 Year";

                    }
                    else if (ConfigurationManager.AppSettings["Provider_subscription_default_year"].ToString() == "3")
                    {
                        price.InnerText = "Rs. " + " " + Get_Listing_Price(ds.Tables[1].Rows[0]["basic_unit_price"].ToString(), Convert.ToDecimal(ConfigurationManager.AppSettings["Provider_india_3_year_per_partner"].ToString()));
                        checkboxOneYear_provider.Checked = false;
                        checkboxTwoYear_provider.Checked = false;
                        checkboxThreeYear_provider.Checked = true;
                        lblyearsub.InnerText = "3 Year";

                    }



                    ViewState["currency_short_name"] = ds.Tables[1].Rows[0]["currency_short_name"];
                    checkboxOneYear_provider.Text = "Rs. " + Get_Listing_Price(ds.Tables[1].Rows[0]["basic_unit_price"].ToString(), 0);
                    checkboxTwoYear_provider.Text = "Rs. " + Get_Listing_Price(ds.Tables[1].Rows[0]["basic_unit_price"].ToString(), Convert.ToDecimal(ConfigurationManager.AppSettings["Provider_india_2_year_per_partner"].ToString()));
                    checkboxThreeYear_provider.Text = "Rs. " + Get_Listing_Price(ds.Tables[1].Rows[0]["basic_unit_price"].ToString(), Convert.ToDecimal(ConfigurationManager.AppSettings["Provider_india_3_year_per_partner"].ToString()));
                    price.InnerText = price.InnerText.Replace(".00", "");
                }
                else
                {
                    string currncy= ds.Tables[1].Rows[0]["currency_short_name"].ToString();
                    ViewState["currency_short_name"] = currncy;
                    price.InnerText = currncy + " " + Get_Listing_Price(ds.Tables[1].Rows[0]["basic_unit_price"].ToString(), 0);

                    if (ConfigurationManager.AppSettings["Provider_subscription_default_year"].ToString() == "1")
                    {
                        price.InnerText = currncy + " " + Get_Listing_Price(ds.Tables[1].Rows[0]["basic_unit_price"].ToString(), 0);
                        checkboxOneYear_provider.Checked = true;
                        checkboxTwoYear_provider.Checked = false;
                        checkboxThreeYear_provider.Checked = false;
                        lblyearsub.InnerText = "1 Year";
                    }
                    else if (ConfigurationManager.AppSettings["Provider_subscription_default_year"].ToString() == "2")
                    {
                        price.InnerText = currncy + " " + Get_Listing_Price(ds.Tables[1].Rows[0]["basic_unit_price"].ToString(), Convert.ToDecimal(ConfigurationManager.AppSettings["Provider_foreign_2_year_per_partner"].ToString()));
                        checkboxOneYear_provider.Checked = false;
                        checkboxTwoYear_provider.Checked = true;
                        checkboxThreeYear_provider.Checked = false;
                        lblyearsub.InnerText = "2 Year";

                    }
                    else if (ConfigurationManager.AppSettings["Provider_subscription_default_year"].ToString() == "3")
                    {
                        price.InnerText = currncy + " " + Get_Listing_Price(ds.Tables[1].Rows[0]["basic_unit_price"].ToString(), Convert.ToDecimal(ConfigurationManager.AppSettings["Provider_foreign_3_year_per_partner"].ToString()));
                        checkboxOneYear_provider.Checked = false;
                        checkboxTwoYear_provider.Checked = false;
                        checkboxThreeYear_provider.Checked = true;
                        lblyearsub.InnerText = "3 Year";

                    }


                    checkboxOneYear_provider.Text = currncy + " " + Get_Listing_Price(ds.Tables[1].Rows[0]["basic_unit_price"].ToString(), 0);
                    checkboxTwoYear_provider.Text = currncy + " " + Get_Listing_Price(ds.Tables[1].Rows[0]["basic_unit_price"].ToString(), Convert.ToDecimal(ConfigurationManager.AppSettings["Provider_foreign_2_year_per_partner"].ToString()));
                    checkboxThreeYear_provider.Text = currncy + " " + Get_Listing_Price(ds.Tables[1].Rows[0]["basic_unit_price"].ToString(), Convert.ToDecimal(ConfigurationManager.AppSettings["Provider_foreign_3_year_per_partner"].ToString()));
                    price.InnerText = price.InnerText.Replace(".00", "");
                }
            }
            else
            {
                if (ds.Tables[2].Rows.Count > 0)
                {

                    string price1 = Get_Listing_Price(ds.Tables[2].Rows[0]["basic_unit_price"].ToString(), 0);

                    price.InnerText = "USD " + price1;

                    // price.InnerText = "USD " + ds.Tables[2].Rows[0]["basic_unit_price"].ToString();
                    ViewState["currency_short_name"] = "USD";

                    if (ConfigurationManager.AppSettings["Provider_subscription_default_year"].ToString() == "1")
                    {
                        price.InnerText = "USD" + " " + Get_Listing_Price(ds.Tables[2].Rows[0]["basic_unit_price"].ToString(), 0);
                        checkboxOneYear_provider.Checked = true;
                        checkboxTwoYear_provider.Checked = false;
                        checkboxThreeYear_provider.Checked = false;
                        lblyearsub.InnerText = "1 Year";
                        ViewState["Seeker_subscrition_price"] = Get_Listing_Price(ds.Tables[2].Rows[0]["basic_unit_price"].ToString(), 0);

                    }
                    else if (ConfigurationManager.AppSettings["Provider_subscription_default_year"].ToString() == "2")
                    {
                        price.InnerText = "USD" + " " + Get_Listing_Price(ds.Tables[2].Rows[0]["basic_unit_price"].ToString(), Convert.ToDecimal(ConfigurationManager.AppSettings["Provider_foreign_2_year_per_partner"].ToString()));
                        checkboxOneYear_provider.Checked = false;
                        checkboxTwoYear_provider.Checked = true;
                        checkboxThreeYear_provider.Checked = false;
                        lblyearsub.InnerText = "2 Year";
                        ViewState["Seeker_subscrition_price"] = Get_Listing_Price(ds.Tables[2].Rows[0]["basic_unit_price"].ToString(), Convert.ToDecimal(ConfigurationManager.AppSettings["Provider_foreign_2_year_per_partner"].ToString()));


                    }
                    else if (ConfigurationManager.AppSettings["Provider_subscription_default_year"].ToString() == "3")
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
                if (Session["country_sk"].ToString() == "100")
                {
                    if (dsButtonType.Tables.Count > 0)
                    {
                        if ((dsButtonType.Tables[1].Rows[0]["parameter_value"].ToString()).Trim() == "M")   // Check for Indian payment gatway M- MultiPaymentGateway
                        {

                            divpaypalccavanue_1.Visible = true; // cavanue payment gatway
                            btnPayNow.Visible = false; // Paypal payment gatway                                          
                            divpaypalStripe_1.Visible = false;


                        }  //  for single payment gatway selection
                        else if ((dsButtonType.Tables[3].Rows[0]["parameter_value"].ToString()).Trim() == "P")   // Check for Indian payment gatway P-Paypal Directly, M- MultiPaymentGateway
                        {
                            divpaypalccavanue_1.Visible = false; // cavanue payment gatway
                            btnPayNow.Visible = true;  // Paypal payment gatway
                            divpaypalStripe_1.Visible = false; // stripe payment gatway
                        }
                        else
                        {

                            divpaypalccavanue_1.Visible = false; // cavanue payment gatway
                            btnPayNow.Visible = false;  // Paypal payment gatway

                            divpaypalStripe_1.Visible = true; // stripe payment gatway
                        }
                    }
                }
                else
                {
                    // For International Customer
                    if ((dsButtonType.Tables[2].Rows[0]["parameter_value"].ToString()).Trim() == "P")   // Check for global payment gatway P->Paypal, S->Stripe
                    {   // Case for International PAYPAL case

                        divpaypalccavanue_1.Visible = false; // cavanue payment gatway
                        btnPayNow.Visible = true; // Paypal payment gatway

                        divpaypalStripe_1.Visible = false; // stripe payment gatway

                    }
                    else
                    {

                        // Case for International Stripe case

                        divpaypalccavanue_1.Visible = false;
                        btnPayNow.Visible = false;

                        divpaypalStripe_1.Visible = true;
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
        #endregion

        #region payment
        protected void SubmitStripe_1_Click(object sender, EventArgs e)
        {
            //try
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
                Session["CurrentURL"] = Session["currentPageUrl"].ToString();
                Session["Provider_payment"] = price.InnerText + "|" + (checkboxOneYear_provider.Checked ? "1" : (checkboxTwoYear_provider.Checked ? "2" : "3"));
                Session["StripepaymentGatway"] = txtCardNumber_1.Text.Trim() + "|"
                        + txtCardExpirationMonth_1.Text.Trim() + "|"
                        + txtCardExpirationYear_1.Text.Trim() + "|"
                        + txtCardName_1.Text.Trim();
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
                Session["CurrentURL"] = Session["currentPageUrl"].ToString();
                Session["Provider_payment"] = price.InnerText + "|" + (checkboxOneYear_provider.Checked ? "1" : (checkboxTwoYear_provider.Checked ? "2" : "3"));
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



        protected void paypal_Click_CCAVANUE_Netbanking(object sender, EventArgs e)
        {

            Utils.Mails mail = new Utils.Mails();
            mail.payNow_Click_Mail(Session["country_sk"].ToString(), Session["massage_partner_sk"].ToString());

            DataSet ddsgetSeekers = new DataSet();
            if (Session["massage_partner_sk"] != null)
            {
                DataSet ds = new DataSet();
                ds = objBusinessLogin.Insert_payment_bridge(Convert.ToInt32(Session["massage_partner_sk"].ToString()), "sp", null, null, "Internet Banking");
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


            Session["CurrentURL"] = Session["currentPageUrl"].ToString();
            Session["Provider_payment"] = price.InnerText + "|" + (checkboxOneYear_provider.Checked ? "1" : (checkboxTwoYear_provider.Checked ? "2" : "3"));
            Response.Redirect(Constants__.WEB_ROOT + "/partner-subscription", false);
            return;
        }

        protected void paypal_Click_CCAVANUE_OtherPayment(object sender, EventArgs e)
        {
            Utils.Mails mail = new Utils.Mails();
            mail.payNow_Click_Mail(Session["country_sk"].ToString(), Session["massage_partner_sk"].ToString());
            if (Session["massage_partner_sk"] != null)
            {
                DataSet ds = new DataSet();
                ds = objBusinessLogin.Insert_payment_bridge(Convert.ToInt32(Session["massage_partner_sk"].ToString()), "sp", null, null, "Other Payment Methods");
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


            Session["CurrentURL"] = Session["currentPageUrl"].ToString();
            Session["Provider_payment"] = price.InnerText + "|" + (checkboxOneYear_provider.Checked ? "1" : (checkboxTwoYear_provider.Checked ? "2" : "3"));
            Response.Redirect(Constants__.WEB_ROOT + "/partner-subscription", false);
            return;
        }

        protected void DebitCard_Payment_Click(object sender, EventArgs e)
        {
            try
            {
                Utils.Mails mail = new Utils.Mails();
                mail.payNow_Click_Mail(Session["country_sk"].ToString(), Session["massage_partner_sk"].ToString());

                lblddlDebitCartTypes.Text = lblddlDebitCartBankNames.Text = "";

                if (ddlDebitCartTypes.SelectedValue == "0" || ddlDebitCartBankNames.SelectedValue == "0")
                {
                    if (ddlDebitCartTypes.SelectedValue == "0")
                    {
                        lblddlDebitCartTypes.Text = "Select Card Type";
                    }

                    if (ddlDebitCartBankNames.SelectedValue == "0")
                    {
                        lblddlDebitCartBankNames.Text = "Select Bank Name";
                    }

                    return;
                }

                if (Session["massage_partner_sk"] != null)
                {
                    DataSet ds = new DataSet();

                    int Banksk = Convert.ToInt32(ddlDebitCartBankNames.SelectedValue);
                    int cardsk = Convert.ToInt32(ddlDebitCartTypes.SelectedValue);
                    ds = objBusinessLogin.Insert_payment_bridge(Convert.ToInt32(Session["massage_partner_sk"].ToString()), "sp", cardsk, Banksk, "Debit Card");
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



                Session["CurrentURL"] = Session["currentPageUrl"].ToString();
                Session["Provider_payment"] = price.InnerText + "|" + (checkboxOneYear_provider.Checked ? "1" : (checkboxTwoYear_provider.Checked ? "2" : "3"));
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

        protected void CreditCard_Payment_Click(object sender, EventArgs e)
        {
            try
            {
                Utils.Mails mail = new Utils.Mails();
                mail.payNow_Click_Mail(Session["country_sk"].ToString(), Session["massage_partner_sk"].ToString());

                lblddlCreditCartTypes.Text = lblddlCreditCartBankNames.Text = "";

                if (ddlCreditCartTypes.SelectedValue == "0" || ddlCreditCartBankNames.SelectedValue == "0")
                {
                    if (ddlCreditCartTypes.SelectedValue == "0")
                    {
                        lblddlCreditCartTypes.Text = "Select Card Type";
                    }

                    if (ddlCreditCartBankNames.SelectedValue == "0")
                    {
                        lblddlCreditCartBankNames.Text = "Select Bank Name";
                    }

                    return;
                }


                if (Session["massage_partner_sk"] != null)
                {
                    DataSet ds = new DataSet();
                    int Banksk = Convert.ToInt32(ddlCreditCartBankNames.SelectedValue);
                    int cardsk = Convert.ToInt32(ddlCreditCartTypes.SelectedValue);
                    ds = objBusinessLogin.Insert_payment_bridge(Convert.ToInt32(Session["massage_partner_sk"].ToString()), "sp", cardsk, Banksk, "Credit Card");
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


                Session["CurrentURL"] = Session["currentPageUrl"].ToString();
                Session["Provider_payment"] = price.InnerText + "|" + (checkboxOneYear_provider.Checked ? "1" : (checkboxTwoYear_provider.Checked ? "2" : "3"));
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


        #endregion

        void select_QR() {
            DataSet ds = objbusinessmpartener.get_QRs();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    img_paytm.Src = Constants__.WEB_ROOT + "/image/Paytm/" + ds.Tables[0].Rows[0]["image_name"].ToString();
                }
                else
                {
                    img_paytm.Src = Constants__.WEB_ROOT + "/image/ptm.jpg";
                }
            }
            else
            {
                img_paytm.Src = Constants__.WEB_ROOT + "/image/ptm.jpg";
            }
        }
        protected void lnkChangeQR_Click(object sender, EventArgs e)
        {
            DataSet ds = objbusinessmpartener.get_QRs();
            ViewState["image_name"] = img_paytm.Src.Replace(Constants__.WEB_ROOT+"/image/Paytm/", "");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[1].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        if (ds.Tables[1].Rows[i]["image_name"].ToString() != ViewState["image_name"].ToString())
                        {
                            img_paytm.Src = "";
                            img_paytm.Src = Constants__.WEB_ROOT + "/image/Paytm/" + ds.Tables[1].Rows[i]["image_name"].ToString();
                            break;
                           // ViewState["image_name"] = ds.Tables[i].Rows[0]["image_name"].ToString();
                        }
                    }
                    //ViewState["image_name"] = img_paytm.Src.Replace("/image/Paytm/", "");
                }
                else
                {
                    img_paytm.Src = Constants__.WEB_ROOT + "/image/ptm.jpg";
                }
            }
            else
            {
                img_paytm.Src = Constants__.WEB_ROOT + "/image/ptm.jpg";
            }
        }
    }
}
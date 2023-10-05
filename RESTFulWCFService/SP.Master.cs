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

namespace RESTFulWCFService
{
    public partial class SP : System.Web.UI.MasterPage
    {
        BussinessSendMail objNotification = new BussinessSendMail();
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
            if (Session["seeker_subscribed"] == null)
                hdnpartnersubscribed.Value = "";
            else
                hdnpartnersubscribed.Value = Session["seeker_subscribed"].ToString();

            if (!IsPostBack)
            {

                if (Session["mp_login_sk"] != null)
                {
                    hdncountry.Value = Session["country_sk"].ToString();
                    lnk_signin.Visible = false;
                    lnk_signin1.Visible = false;
                    paid_list.Visible = true;
                    unpaid_list.Visible = false;
                    lnk_logout.Visible = true;
                    lnk_register.Visible = false;
                    user_email.Visible = false;
                    txtemail_user.Value = Session["email_id"].ToString();
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
             //   if (hdncountry.Value == "100")
             //   {
             //       lnkmembership.Visible = false;
             ////       lnkmembership1.Visible = true;
             //   }
             //   else
             //   {
             //       lnkmembership.Visible = true;
             // //      lnkmembership1.Visible = false;
             //   }
            }
            if (hdnpartnersubscribed.Value == null || hdnpartnersubscribed.Value == "" || hdnpartnersubscribed.Value == "N")
            {
              //  if (hdncountry.Value == "100")
              //  {
              //      lnkmembership.Visible = false;
              ////      lnkmembership1.Visible = true;
              //  }
              //  else
              //  {
              //      lnkmembership.Visible = true;
              //   //   lnkmembership1.Visible = false;
              //  }
            }
            else
            {
                lnkmembership.Visible = false;
                lnk_mobile_membership.Visible = false;
               // lnkmembership1.Visible = false;
            }
           


        }
        #region methods
 
    
        private void bind_css()
        {
            bootstrapcss.Href = Constants__.WEB_ROOT_CDN + "/bootstrap/css/bootstrap.css";
            bootstrap_min.Href = Constants__.WEB_ROOT_CDN + "/bootstrap/css/bootstrap.min.css";
            bootstraptheme.Href = Constants__.WEB_ROOT_CDN + "/bootstrap/css/bootstrap-theme.css";
            bootstraptheme_min.Href = Constants__.WEB_ROOT_CDN + "/bootstrap/css/bootstrap-theme.min.css";
            lnk_partner_popup.Href = Constants__.WEB_ROOT_CDN + "/css/payment_popup_partner.css";
            style.Href = Constants__.WEB_ROOT_CDN + "/css/style.css";

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
                   // Filldefaultstate_city();

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
                GetIP();
                fillDropdown();
                string url = "/" + ViewState["selected_country"].ToString().Replace(' ', '-') + "/" + ViewState["selected_state"].ToString().Replace(' ', '-') + "/" + ViewState["selected_city"].ToString().Replace(' ', '-') + "/All-Area/Any/all-types/all";
                Response.Redirect(Constants__.WEB_ROOT + "/massage-partner" + url,false);
            }
            catch
            {
                ViewState["selected_country"] = ConfigurationManager.AppSettings["default_countryCode1"].ToString();
                ViewState["selected_state"] = ConfigurationManager.AppSettings["default_regionName"].ToString();
                ViewState["selected_city"] = ConfigurationManager.AppSettings["default_city"].ToString();
                string url = "/" + ViewState["selected_country"].ToString().Replace(' ', '-') + "/" + ViewState["selected_state"].ToString().Replace(' ', '-') + "/" + ViewState["selected_city"].ToString().Replace(' ', '-') + "/All-Area/Any/all-types/all";
                Response.Redirect(Constants__.WEB_ROOT + "/massage-partner" + url,false);
            }
        }

        protected void lnksearchpartner1_Click(object sender, EventArgs e)
        {
            try
            {
                GetIP();
                fillDropdown();
                string url = "/" + ViewState["selected_country"].ToString().Replace(' ', '-') + "/" + ViewState["selected_state"].ToString().Replace(' ', '-') + "/" + ViewState["selected_city"].ToString().Replace(' ', '-') + "/All-Area/Any/all-types/all";
                Response.Redirect(Constants__.WEB_ROOT + "/massage-partner" + url,false);
            }
            catch
            {
                ViewState["selected_country"] = ConfigurationManager.AppSettings["default_countryCode1"].ToString();
                ViewState["selected_state"] = ConfigurationManager.AppSettings["default_regionName"].ToString();
                ViewState["selected_city"] = ConfigurationManager.AppSettings["default_city"].ToString();
                string url = "/" + ViewState["selected_country"].ToString().Replace(' ', '-') + "/" + ViewState["selected_state"].ToString().Replace(' ', '-') + "/" + ViewState["selected_city"].ToString().Replace(' ', '-') + "/All-Area/Any/all-types/all";
                Response.Redirect(Constants__.WEB_ROOT + "/massage-partner" + url,false);
            }
        }
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

                ddluser.SelectedValue = "0";
                name.Value = "";
                email.Value = "";
                subject.Value = "";
                comments.Value = "";
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
                //if (ds.Tables[0].Rows[0]["gender"].ToString() == "F")
                //{
                //    Session["seeker_subscribed"] = "Y";
                //}
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
                        Response.Redirect(Constants__.WEB_ROOT + "/massage-partner" + url,false);
                        return;
                    }
                    catch
                    {
                        ViewState["selected_country"] = ConfigurationManager.AppSettings["default_countryCode1"].ToString();
                        ViewState["selected_state"] = ConfigurationManager.AppSettings["default_regionName"].ToString();
                        ViewState["selected_city"] = ConfigurationManager.AppSettings["default_city"].ToString();
                        string url = "/" + ViewState["selected_country"].ToString().Replace(' ', '-') + "/" + ViewState["selected_state"].ToString().Replace(' ', '-') + "/" + ViewState["selected_city"].ToString().Replace(' ', '-') + "/All-Area/Any/all-types/all";
                        Response.Redirect(Constants__.WEB_ROOT + "/massage-partner" + url,false);
                    }
                }

            }
            else
            {
                lblloginerror.Visible = true;
                lblloginerror.Text = "invalid email-id or password!";

            }
        }
    }
}
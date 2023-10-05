using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using CCA.Util;
using PayPalIntegration;
using Stripe;

namespace RESTFulWCFService.MassagePartener
{
    public partial class faq : System.Web.UI.Page
    {
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
        Business.BusinessMPartener objbusinessmpartener = new BusinessMPartener();
        Business.BusinessSearch objBusinessSearch = new Business.BusinessSearch();
        DataTable dt;
        static int i = 0;
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
            if (Session["massage_partner_sk"] != null)
            {
                lnklogin.Visible = false;
                lnkpayment.Visible = true;

                if (Session["country_sk"].ToString() != "100")
                {
                    div_India_User.Visible = false;
                }

                txtregisteredmail_id.Text = Session["email_id"].ToString();
                partner_sk.Value = Session["massage_partner_sk"].ToString();

                Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) - 120));
                if (Session["seeker_subscribed"] == null)
                    hdnpartnersubscribed.Value = "";
                else
                    hdnpartnersubscribed.Value = Session["seeker_subscribed"].ToString();

                if (hdnpartnersubscribed.Value == null || hdnpartnersubscribed.Value == "" || hdnpartnersubscribed.Value == "N")
                {
                    lnklogin.Visible = false;
                    lnkpayment.Visible = true;
                }
                else
                {
                    lnklogin.Visible = false;
                    lnkpayment.Visible = false;
                }

                ViewState["massage_partner_sk"] = Session["massage_partner_sk"].ToString();
                ViewState["LoginSk"] = Session["mp_login_sk"].ToString();
                ViewState["country_sk"] = Session["country_sk"].ToString();
                if (!IsPostBack)
                {
                    hdncountry.Value = Session["country_sk"].ToString();
                    ViewState["massage_partner_sk"] = Session["massage_partner_sk"].ToString();
                    ViewState["LoginSk"] = Session["mp_login_sk"].ToString();
                    ViewState["country_sk"] = Session["country_sk"].ToString();
                    if (Session["seeker_subscribed"] == null)
                        hdnpartnersubscribed.Value = "";
                    //DataSet ds = objbusinessmpartener.get_messages(Convert.ToInt32(Session["massage_partner_sk"].ToString()));
                    //ViewState["All_Data"] = ds;
                    //  ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp1sd", "<script type='text/javascript'>openpopuppaypal();</script>", false);
                    // ScriptManager.GetCurrent(this).RegisterPostBackControl(Button1);
                }
            }
            else
            {
                lnklogin.Visible = true;
                lnkpayment.Visible = false;
                partner_sk.Value = "0";
                GetIP();
                fillDropdown();
                if (ViewState["selected_country_value"] != null)
                {
                    if (ViewState["selected_country_value"].ToString() != "100")
                    {
                        div_India_User.Visible = false;
                    }
                    else
                    {
                        div_India_User.Visible = true;
                    }
                }
                lbloneyear.Text = "Rs " + ConfigurationManager.AppSettings["Partner_sub_india_1_year"].ToString();
                lbltwoyear.Text = "Rs " + ConfigurationManager.AppSettings["Partner_sub_india_2_year"].ToString();
                lblthreeyear.Text = "Rs " + ConfigurationManager.AppSettings["Partner_sub_india_3_year"].ToString();
            }

        }
        #region methods
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
        #endregion

        protected void lnklogin_Click(object sender, EventArgs e)
        {
            Session["current_url"] = Constants__.WEB_ROOT + "/faq";
            Response.Redirect(Constants__.WEB_ROOT + "/signup",false);
        }

        [WebMethod]                                 //Default.aspx.cs
        public static void Send_Request_mail(string id, string trans_no,string sk)
        {
            if (sk != "0")
            {
                int status = 0;
                BussinessSendMail objmail = new BussinessSendMail();

                objmail.Mrecipients = ConfigurationManager.AppSettings["EmailTable2Book"].ToString();
                objmail.Mbody = "User (Email id: " + id + ") done payment using Paytm.<br/><br/>Transaction ID: " + trans_no + "<br/>Partner SK is: "+sk+" ";
                objmail.Msubject = "Massage Partner Subscription | User Subscription Information";
                status = objmail.SendMail();
                //txttransaction.Text = "";
                //lblresult.Visible = true;
                //lblresult.Text = "information submitted successfully!";
            }
            else
            {
                int status = 0;
                BusinessMPartener objmpartner = new BusinessMPartener();
                DataSet ds = objmpartner.get_sk_by_email_id(id);
                if (ds.Tables[0].Rows[0]["massage_partner_sk"].ToString() != "0")
                {
                    BussinessSendMail objmail = new BussinessSendMail();

                    objmail.Mrecipients = ConfigurationManager.AppSettings["EmailTable2Book"].ToString();
                    objmail.Mbody = "User (Email id: " + id + ") done payment using Paytm.<br/><br/>Transaction ID: " + trans_no + "<br/>Partner SK is: " + ds.Tables[0].Rows[0]["massage_partner_sk"].ToString() + " ";
                    objmail.Msubject = "Massage Partner Subscription | User Subscription Information";
                    status = objmail.SendMail();
                }
                else
                {
                    BussinessSendMail objmail = new BussinessSendMail();

                    objmail.Mrecipients = ConfigurationManager.AppSettings["EmailTable2Book"].ToString();
                    objmail.Mbody = "User (Email id: " + id + ") done payment using Paytm.<br/><br/>Transaction ID: " + trans_no + "<br/>Partner SK is: New User ";
                    objmail.Msubject = "Massage Partner Subscription | User Subscription Information(New User)";
                    status = objmail.SendMail();
                }
            }
        }
    }
}
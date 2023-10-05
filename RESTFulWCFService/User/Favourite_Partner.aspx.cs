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

namespace RESTFulWCFService.MassagePartener.User
{
    public partial class Favourite_Partner : System.Web.UI.Page
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
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) - 120));
            if (Session["massage_partner_sk"] != null)
            {
                DataSet ds_check = objbusinessmpartener.getPartnerSubsciption_record(Convert.ToInt32(Session["massage_partner_sk"].ToString()));
                if (ds_check.Tables[3].Rows.Count > 0)
                {
                    hdnpartnersubscribed.Value = "Y";
                }
                else
                    hdnpartnersubscribed.Value = "";

                ViewState["massage_partner_sk"] = Session["massage_partner_sk"].ToString();
                ViewState["LoginSk"] = Session["mp_login_sk"].ToString();
                ViewState["country_sk"] = Session["country_sk"].ToString();
               // txtemail_user.Value = Session["email_id"].ToString();
              
                if (!IsPostBack)
                {
                    hdncountry.Value = Session["country_sk"].ToString();
                    fill_dropdowns();
                    // bind_datatable((objbusinessmpartener.get_fav_partner(Session["massage_partner_sk"].ToString()).Tables[0]));
                }
                bind_datatable((objbusinessmpartener.get_fav_partner(Session["massage_partner_sk"].ToString()).Tables[0]));
                if (!Page.IsPostBack)
                {
                    if ((Request.QueryString["PayPal"] != null && Request.QueryString["PayPal"].ToString() == "Info2468Cubek"))
                    {
                        lblpaymentinfo.Style.Add("color", "green");
                        lblpaymentinfo.InnerHtml = "Annual Listing Payment Successful.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "openOffersDialog();", true);
                        return;
                    }
                    else if ((Request.QueryString["PayPal"] != null && Request.QueryString["PayPal"].ToString() == "Cubek2468Tech"))
                    {
                        lblpaymentinfo.Style.Add("color", "green");
                        lblpaymentinfo.InnerHtml = "Annual Listing Payment Successful.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "openOffersDialog();", true);
                        return;
                    }
                    else if ((Request.QueryString["PayPal"] != null && Request.QueryString["PayPal"].ToString() == "Sub_Cancel"))
                    {
                        //lblSuccess.InnerText = "Payment not done";
                        // lblSuccess.Style.Add("color", "red");
                        lblpaymentinfo.Style.Add("color", "red");
                        lblpaymentinfo.InnerHtml = "Annual Listing Payment Unsuccessful";
                        DataTable DT = new DataTable();
                        if (Session["seeker_subscribed"] != null && Session["seeker_subscribed"].ToString() != "")
                        {
                            DT = (DataTable)(Session["seeker_subscribed"]);
                            if (DT.Rows.Count > 0)
                            {
                                int service_provider_sk = Convert.ToInt32(DT.Rows[0]["massage_partner_sk"]);
                                int status = objmail.send_notification_mail_Provider_annual_sub(service_provider_sk);
                            }
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "openOffersDialog();", true);
                            return;
                            //Session["Provider_subscription"] = null;
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "openOffersDialog();", true);
                        return;

                    }
                }
                if (Session["counter"] != null)
                {
                    int c = Convert.ToInt32(Session["counter"].ToString());
                    if (c >= 5)
                    {
                        btnsend.Enabled = false;
                    }
                }
            }
            else
            {
                Session["current_url"] = Request.RawUrl;
                Response.Redirect(Constants__.WEB_ROOT, false);
            }
        }

        #region methods 
        private void bind_datatable(DataTable dt)
        {
            UCPager1.Ods = dt;
            UCPager1.ObjectControl = DataList1;
            UCPager1.PageSize = 10;
        }
        private void fill_dropdowns()
        {
            DataSet ds = new DataSet();
            //Fill therapies
            ds = objBusinessSearch.getSpaType("M");
            if (ds.Tables[1].Rows.Count > 0)
            {
                ViewState["Specilization"] = ds.Tables[1];
            }
        }
        #endregion
        #region Events
        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            System.Web.UI.WebControls.Image provider_image = (System.Web.UI.WebControls.Image)e.Item.FindControl("img_partner");
            ImageButton imgNorated = e.Item.FindControl("imgNoratedreview") as ImageButton;
            LinkButton lnkfav = e.Item.FindControl("lnkfav") as LinkButton;
            LinkButton lnkblck = (LinkButton)e.Item.FindControl("lnkblck");
            HiddenField hdn_massage_partner_sk = e.Item.FindControl("hdn_massage_partner_sk") as HiddenField;
            Button btnshow = e.Item.FindControl("btnshow") as Button;
            Label lblcontactno = e.Item.FindControl("lblcontact") as Label;
            Label lblmassagetype = e.Item.FindControl("lblmassagetype") as Label;
            Control objDiv1 = e.Item.FindControl("divmassagetype");
            Control obj_contact_div = e.Item.FindControl("divcontact_number");
            HiddenField hdncontactnos = e.Item.FindControl("hdncontactnos") as HiddenField;
            HiddenField hdngender = e.Item.FindControl("hdngender") as HiddenField;
            LinkButton lnkbtnsendmsg = e.Item.FindControl("lnkbtnsendsms") as LinkButton;
            LinkButton lnkbtnsendmsg1 = e.Item.FindControl("lnkbtnsendsms1") as LinkButton;
            Control objDiv = e.Item.FindControl("tr_row");

            if (hdncontactnos.Value == "" || hdncontactnos.Value == "0" || hdncontactnos.Value == "NULL")
            {
                lblcontactno.Text = "NA";
            }

            lnkfav.Text = "Remove from Favourite";
            lnkblck.Enabled = false;
            lnkblck.Style["cursor"] = "no-drop";


            if (lblmassagetype.Text == "")
                objDiv1.Visible = false;

            if (hdnpartnersubscribed.Value == null || hdnpartnersubscribed.Value == "" || hdnpartnersubscribed.Value == "N")
            {
                btnshow.Visible = true;
                btnshow.Enabled = true;
                lblcontactno.Visible = false;
                lnkbtnsendmsg1.Visible = true;
                lnkbtnsendmsg.Visible = false;
                btnshow.Attributes.Add("OnClientClick", "openpopuppaypal();");
            }
            else
            {
                btnshow.Enabled = false;
                btnshow.Visible = false;
                lnkbtnsendmsg1.Visible = false;
                lnkbtnsendmsg.Visible = true;
                lblcontactno.Visible = true;
            }


            string[] images = (Convert.ToString(DataBinder.Eval(e.Item.DataItem, "image"))).Split(',');
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
            }
        }
        protected void btnsend_Click(object sender, EventArgs e)
        {
              int c = 0;
            if (Session["counter"] != null)
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
                    txtmessage.Text = "";
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
                
                objmail.Mbody = body;// +senderDetail;
                //objmail.UserType ="Massage-Partner";
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
            }
        }
        protected void btnreport_Click(object sender, EventArgs e)
        {
            try
            {

                BussinessSendMail objmail = new BussinessSendMail();

                //string senderDetail = "<br /><br /><b>Sender Detail</b> <br />" + name.Value + "<br />" + company.Value + " " + country.Value + "<br /> " + phone.Value;

                objmail.sender = Convert.ToString((Session["email_id"].ToString()));
                objmail.Mbody = Convert.ToString(txtreport.Text);// +senderDetail;
                objmail.UserType = hdnname.Value;
                objmail.Mrecipients = ConfigurationManager.AppSettings["EmailTable2Book"].ToString();
                objmail.Msubject = "Massage-Partner Report Abuse";
                int status = objmail.SendMail_Contactus();
                lblreportmsg.Visible = true;
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
        #endregion Events



    }
}
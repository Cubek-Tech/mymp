using System;
using System.Collections.Generic;
using System.Linq;
using BussinessEntity;
using System.Drawing;
using System.Text.RegularExpressions;
using RESTFulWCFService;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using PayPalIntegration;
using System.Text.RegularExpressions;
using CCA.Util;
using Stripe;
using System.Net;
using System.Collections.Generic;
using Business;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Configuration;
using System.Web;


namespace RESTFulWCFService.MassagePartener.User
{
    public partial class All_messages : System.Web.UI.Page
    {
        //paymentSection
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
        DataTable dt;
        static int i = 0;
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
                if (!IsPostBack)
                {
                    hdncountry.Value = Session["country_sk"].ToString();
                    ViewState["massage_partner_sk"] = Session["massage_partner_sk"].ToString();
                    ViewState["LoginSk"] = Session["mp_login_sk"].ToString();
                    ViewState["country_sk"] = Session["country_sk"].ToString();
                    if (Session["seeker_subscribed"] == null)
                        hdnpartnersubscribed.Value = "";
                    DataSet ds = objbusinessmpartener.get_messages(Convert.ToInt32(Session["massage_partner_sk"].ToString()));
                    ViewState["All_Data"] = ds;
                    bind_datatable(ds);
                    //  ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp1sd", "<script type='text/javascript'>openpopuppaypal();</script>", false);
                    // ScriptManager.GetCurrent(this).RegisterPostBackControl(Button1);
                }
                else
                {
                    bind_datatable((DataSet)ViewState["All_Data"]);
                }
                if (Session["counter"] != null)
                {
                    int c = Convert.ToInt32(Session["counter"].ToString());
                    if (c >= 5)
                    {
                        Button1.Enabled = false;
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
        private void bind_datatable(DataSet ds)
        {

            UCPager1.Ods = ds.Tables[0];
            UCPager1.ObjectControl = DataList1;
            UCPager1.PageSize = 5;
        }
        #endregion
        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            System.Web.UI.WebControls.Image img_ = (System.Web.UI.WebControls.Image)e.Item.FindControl("image1");
            HiddenField from_sk = (HiddenField)e.Item.FindControl("hdnfromsk");
            HtmlControl objDiv = e.Item.FindControl("main_message_div") as HtmlControl;
            HtmlControl imgdiv = e.Item.FindControl("image_bx") as HtmlControl;
            LinkButton btnrply = e.Item.FindControl("lnkbtnreply") as LinkButton;
            LinkButton btnsend = e.Item.FindControl("btnsend") as LinkButton;
            LinkButton btnsend1 = e.Item.FindControl("btnsend1") as LinkButton;
            HtmlControl divcontact = e.Item.FindControl("divcontact") as HtmlControl;
            Button lblcontact = e.Item.FindControl("lblcontact") as Button;
            Button btnshowcontact = e.Item.FindControl("btnshowcontact") as Button;

            if (hdnpartnersubscribed.Value == null || hdnpartnersubscribed.Value == "" || hdnpartnersubscribed.Value == "N")
            {
                btnsend.Visible = false;
                btnsend1.Visible = true;
            }
            else
            {
                btnsend.Visible = true;
                btnsend1.Visible = false;
            }
            if (from_sk.Value == Session["massage_partner_sk"].ToString())
            {
                objDiv.Attributes["class"] = "media right-align-box";
                divcontact.Visible = false;
                btnrply.Visible = false;
                if ((Convert.ToString(DataBinder.Eval(e.Item.DataItem, "from_image"))) != "")
                    img_.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + (Convert.ToString(DataBinder.Eval(e.Item.DataItem, "from_image")));
                else
                    img_.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/no_image.jpg";

                // ScriptManager.RegisterStartupScript(this, this.GetType(), name, "change_design('ctl00_ContentPlaceHolder1_DataList1_ctl00_main_message_div')", true);
            }
            else
            {
                if (hdnpartnersubscribed.Value == null || hdnpartnersubscribed.Value == "" || hdnpartnersubscribed.Value == "N")
                {
                    lblcontact.Visible = false;
                    btnshowcontact.Visible = true;

                }
                else
                {
                    lblcontact.Visible = true;
                    btnshowcontact.Visible = false;
                }

                if ((Convert.ToString(DataBinder.Eval(e.Item.DataItem, "from_image"))) != "")
                    img_.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + (Convert.ToString(DataBinder.Eval(e.Item.DataItem, "from_image")));
                else
                    img_.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/no_image.jpg";
            }

        }

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            TextBox txtreply = (TextBox)e.Item.FindControl("txtreply");
            switch (e.CommandName)
            {
                case "reply":
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "harit25", "change_visibility('display')", true);
                        break;
                    }
                case "send_reply":
                    {

                        //bind_datatable();
                        //   Response.Redirect(Request.RawUrl);
                        break;
                    }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
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
                int i = objbusinessmpartener.insert_messages(Convert.ToInt32(Session["massage_partner_sk"].ToString()), Convert.ToInt32(hdntomsg.Value), hdnmsg.Value);
                //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "Refresh_Page();", true);
                //            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Refresh_Page()", true);

                DataSet ds = objbusinessmpartener.get_messages(Convert.ToInt32(Session["massage_partner_sk"].ToString()));
                ViewState["All_Data"] = ds;
                bind_datatable(ds);
                Send_message_copy();
                Response.Redirect(Request.RawUrl);
            }

        }
        private void Send_message_copy()
        {
            try
            {
                DataSet ds = objbusinessmpartener.get_partner_details(Convert.ToInt32(Session["massage_partner_sk"].ToString()));
                DataSet ds1 = objbusinessmpartener.get_partner_details(Convert.ToInt32(hdntomsg.Value));

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
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace RESTFulWCFService.MassagePartener.User
{
    public partial class messages : System.Web.UI.Page
    {
        Business.BusinessMPartener objbusinessMpartner = new Business.BusinessMPartener();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Msk"] != null || Request.QueryString["Msk"].ToString() != "")
            {
                DataSet ds = objbusinessMpartner.get_partner_details(Convert.ToInt32(Cryptology.Decrypt(Request.QueryString["Msk"].ToString())));
                check_login(ds.Tables[1].Rows[0]["email_id"].ToString(), ds.Tables[1].Rows[0]["password"].ToString());
            }
        }
        private void check_login(string email, string password)
        {
            DataSet ds;
            ds = objbusinessMpartner.partner_login(email, password);
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


                Response.Redirect(Constants__.WEB_ROOT + "/messages");
               
            }
            
        }
    }
}
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
using System.Diagnostics;

public partial class Paypal_Success : System.Web.UI.Page
{
    int provider_id;
    protected void Page_Load(object sender, EventArgs e)
    {
        //btn_continue.Click += new EventHandler(btn_Continue_Click);
        //if (Session["Sprovider_id"] != null)
        //    provider_id = Convert.ToInt32(Session["Sprovider_id"]);
        //else
        //{
        //    if (Session["LoginSprovider_id"] != null)
        //    {
        //        provider_id = Convert.ToInt32(Session["LoginSprovider_id"]);

        //    }

        //    else
        //    {
        //        Response.Redirect(Constants__.WEB_ROOT + "/ServiceProvider/RestaurantOwnerSection.aspx", false);
        //        return;
        //    }
        //}


    }

    protected void btn_Continue_Click(object s, EventArgs e)
    {

        try{
        if (Request.QueryString["PayPal"] != null && Request.QueryString["PayPal"].ToString() == "Success")
        {
            if ( Session["flag"] != null &&  Session["flag2"] != null && Session["dt_Update"] != null && Session["dt_save"] != null)
            {

                int flag2 = Convert.ToInt32(Session["flag2"]);
                int flag = Convert.ToInt32(Session["flag"]);
                DataTable dt_Update = (DataTable)Session["dt_Update"];
                DataTable dt_save = (DataTable)Session["dt_save"];
                BussinessSubscription _subs = new BussinessSubscription();


                int status = 0;
                if (flag2 == 1)
                {
                    status = _subs.UpdateSubscription(dt_Update);
                    // "Information update successfully.";
                }


                if (flag == 1)
                {
                    status = _subs.setDetails(provider_id, dt_save);
                    // "Information saved successfully.";
                }
                Response.Redirect(Constants__.WEB_ROOT + "/rescontact/y/main-contact");
                return;
            }
            else
            {
                Response.Redirect(Constants__.WEB_ROOT + "/subscription/y/main-sub");
                return;
            }
        }
        else
        {
            Response.Redirect(Constants__.WEB_ROOT + "/subscription/y/main-sub");
            return;
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
}

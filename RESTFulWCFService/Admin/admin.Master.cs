using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RESTFulWCFService.Admin
{
    public partial class admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        //    if (Session["user"] != null)
        //    { }
        //    else
        //    {
        //        Response.Redirect(Constants__.WEB_ROOT + "/Admin/login.aspx", false);
        //    }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session["user"] = null;
            Response.Redirect(Constants__.WEB_ROOT + "/Admin/login.aspx", false);
        }
    }
}
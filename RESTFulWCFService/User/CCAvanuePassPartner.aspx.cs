using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RESTFulWCFService.MassagePartener.User
{
    public partial class CCAvanuePassPartner : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["current_url_payment"] != null)
                Response.Redirect(Session["current_url_payment"].ToString() + "?PayPal=Cubek2468Tech", false);
            else
                Response.Redirect(Constants__.WEB_ROOT + "/search-partner?PayPal=Cubek2468Tech", false);
            return;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}
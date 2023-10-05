using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;

namespace RESTFulWCFService.Admin
{
    public partial class replyMessages : System.Web.UI.Page
    {
        BusinessMPAdmin objAdmin = new BusinessMPAdmin();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void imgcalender_Click(object sender, ImageClickEventArgs e)
        {
            Calendar1.Visible = true;
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            txtdate.Text = Calendar1.SelectedDate.ToShortDateString();
            Calendar1.Visible = false;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RESTFulWCFService.Admin
{
    public partial class UpdatePTMlink : System.Web.UI.Page
    {
        Business.BusinessMPAdmin objadmin = new Business.BusinessMPAdmin();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grdviewPaymentUpdates.DataSource = objadmin.update_payment_parameters("");
                grdviewPaymentUpdates.DataBind();
            }
        }

        protected void btnUpdateLink_Click(object sender, EventArgs e)
        {
            objadmin.update_payment_parameters(txtPtm.Text.Trim());
            lblResult.Text = "Paytm link updated successfully!";

            grdviewPaymentUpdates.DataSource = objadmin.update_payment_parameters("");
            grdviewPaymentUpdates.DataBind();
        }
    }
}
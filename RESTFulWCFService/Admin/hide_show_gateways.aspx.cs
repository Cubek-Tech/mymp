using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;

namespace RESTFulWCFService.Admin
{
    public partial class hide_show_gateways : System.Web.UI.Page
    {
        RegistrationBusiness objRegistrationBusiness = new RegistrationBusiness();
        BusinessMPAdmin objadmin = new BusinessMPAdmin();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fill_dropdown();
            }

        }

        #region methods

        private void fill_dropdown()
        {
            DataSet ds;
            ds = objRegistrationBusiness.getCountryCity_hide_gateway();
            if (ds.Tables[0].Rows.Count > 0)
            {
                chkCountries.DataSource = ds.Tables[0];
                ViewState["countries"] = ds.Tables[0];
                chkCountries.DataValueField = "country_sk";
                chkCountries.DataTextField = "country_name";

                chkCountries.DataBind();
            }
            //Get all Payment Gateways
            ds = objadmin.get_payment_gateways();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlGateways.DataSource = ds.Tables[0];
                    ViewState["PModes"] = ds.Tables[0];

                    ddlGateways.DataTextField = "parameter_name";
                    ddlGateways.DataValueField = "parameter_sk";
                    ddlGateways.DataBind();
                    ddlGateways.Items.Insert(0, new ListItem("Select Payment gateway", "0"));

                }
            }
        }
     

        protected void btnUpdate_Click(object sender, EventArgs e)
        {          
            DataSet ds = objadmin.hide_show_countries_gatways(ddlGateways.SelectedValue , chkCountries.SelectedValue);
            if (ds.Tables[0].Rows[0][0].ToString() == "1")
                lblResult.Text = ddlGateways.SelectedItem.Text + " Gateway for country " + chkCountries.SelectedItem.Text + ", removed successfully !!";
            else
                lblResult.Text = ddlGateways.SelectedItem.Text + " Gateway for country " + chkCountries.SelectedItem.Text + ", assigned successfully !!";
            chkCountries.ClearSelection();
            ddlGateways.ClearSelection();
          
        }
        #endregion
    }
}
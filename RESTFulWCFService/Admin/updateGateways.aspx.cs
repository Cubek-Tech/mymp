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
    public partial class updateGateways : System.Web.UI.Page
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
            ds = objRegistrationBusiness.getCountryCity();
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
                    chkPModes.DataSource = ds.Tables[0];
                    chkPModes.DataTextField = "parameter_name";
                    chkPModes.DataValueField = "parameter_sk";
                    ddlGateways.DataTextField = "parameter_name";
                    ddlGateways.DataValueField = "parameter_sk";
                    chkPModes.DataBind();
                    ddlGateways.DataBind();
                    ddlGateways.Items.Insert(0,new ListItem("Select Payment gateway","0"));

                    string value = objadmin.get_default_gateway().Tables[0].Rows[0]["parameter_value"].ToString();

                    DataTable dt = (DataTable)ViewState["PModes"];
                    for (int i = 0; i < chkPModes.Items.Count; i++)
                    {
                        DataRow[] dr = dt.Select("parameter_name = '" + chkPModes.Items[i].Text + "'");
                        if (dr.Length > 0)
                        {
                            foreach (DataRow r in dr)
                            {
                                if (r["parameter_value"].ToString() == value)
                                {
                                    chkPModes.Items[i].Selected = true;
                                }
                            }
                        }
                    }
                }
            }
        }
        private DataTable countries_selection()
        {
            DataTable dt = (DataTable)ViewState["countries"];
            int no = dt.Rows.Count;
            DataTable dtnew = new DataTable();
            dtnew.Columns.Add("country_sk", typeof(int));

            for (int i = 0; i < chkCountries.Items.Count; i++)
            {
                if (chkCountries.Items[i].Selected == true && dt != null)
                {
                    DataRow[] dr = dt.Select("country_name = '" + chkCountries.Items[i].Text + "'");
                    dtnew.Rows.Add(Convert.ToInt32(dr[0]["country_sk"]));

                }
            }
            return dtnew;
        }
        #endregion
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            DataTable countries = countries_selection();
            int i = objadmin.update_countries_gatways(ddlGateways.SelectedItem.Value,countries);
            if (i != 0)
            {
                lblResult.Text = "Gateways Updated Successfully!!!";
                chkCountries.ClearSelection();
                ddlGateways.SelectedValue = "0";
                GridView1.DataBind();

            }
        }

        protected void chkPModes_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["PModes"];
            string value = "";
            for (int i = 0; i < chkPModes.Items.Count; i++)
            {
                if (chkPModes.Items[i].Selected == true && dt != null)
                {
                    DataRow[] dr = dt.Select("parameter_name = '" + chkPModes.Items[i].Text + "'");
                    if (dr.Length > 0)
                    {
                        foreach (DataRow r in dr)
                        {
                            value = r["parameter_value"].ToString();
                        }
                    }
                   
                }
            }
            int k = objadmin.update_default_gateway(value);
            if (k != 0)
            {
                Response.Redirect(Request.RawUrl);
            }
        }
    }
}
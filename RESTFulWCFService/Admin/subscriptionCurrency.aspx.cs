using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Business;

namespace RESTFulWCFService.Admin
{
    public partial class subscriptionCurrency : System.Web.UI.Page
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
        private void fill_dropdown()
        {
            DataSet ds;
            ds = objRegistrationBusiness.getCountryCity();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["countries"] = ds.Tables[0];
                ddlCountry_seeker.DataSource = ds.Tables[0];
                ddlCountry_seeker.DataValueField = "country_sk";
                ddlCountry_seeker.DataTextField = "country_name";
                ddlCountry_seeker.DataBind();
                ddlCountry_seeker.Items.Insert(0, new ListItem("-Select Country-", "0"));
            }

        }
        protected void ddlCountry_seeker_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = objadmin.get_country_currency_details(ddlCountry_seeker.SelectedValue);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["exchng_rate"].ToString() != "")
                    {
                        txtRateSeeker.Text = ds.Tables[0].Rows[0]["exchng_rate"].ToString();
                    }
                }
            }
            DataSet ds1 = objadmin.seeker_subscription_price_bycountry(ddlCountry_seeker.SelectedValue);
            if (ds1.Tables.Count > 0)
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    txtBasicUnit_Seeker.Text = ds1.Tables[0].Rows[0]["basic_unit_price"].ToString();
                }
                else
                {
                    txtBasicUnit_Seeker.Text = "0";
                }
            }
        }
        protected void btnAssignSubscription_seeker_Click(object sender, EventArgs e)
        {
            objadmin.insert_seeker_currency_subscription(ddlCountry_seeker.SelectedValue.ToString(), txtBasicUnit_Seeker.Text.Trim(), txtRateSeeker.Text.Trim());
            txtBasicUnit_Seeker.Text = "";
            txtRateSeeker.Text = "";
            ddlCountry_seeker.SelectedValue = "0";
            Label1.Text = "Subscription Added successfully!!";
            Label1.ForeColor = System.Drawing.Color.Green;
        }
    }
}
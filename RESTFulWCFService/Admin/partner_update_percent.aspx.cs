using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using Business;
using System.Drawing;
using ASPSnippets.FaceBookAPI;
using System.Web.UI.HtmlControls;
using RESTFulWCFService;
using System.Diagnostics;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using Factory;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls.WebParts;

namespace RESTFulWCFService.Admin
{
    public partial class partner_update_percent : System.Web.UI.Page
    {
        BusinessMPAdmin objadmin = new BusinessMPAdmin();
        RegistrationBusiness objRegistrationBusiness = new RegistrationBusiness();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fill_dropdown();
                Grd1.DataSource = objadmin.get_seeker_subscription_price_bycountry().Tables[0];
                Grd1.DataBind();

            }
            for (int i = 0; i < chkCountries.Items.Count; i++)
            {
                chkCountries.Items[i].Attributes.Add("onclick", "MutExChkList(this)");
            }
        }
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
            chkCountries.Items.Add(new ListItem("Others", "-1"));
        }

        protected void chkCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = objadmin.seeker_subscription_price_bycountry(chkCountries.SelectedValue);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtOneYear.Text = ds.Tables[0].Rows[0]["basic_unit_price"].ToString();
                }
                else
                {
                    txtOneYear.Text = "0";
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            objadmin.insert_update_seeker_subscription_price_bycountry(chkCountries.SelectedValue, txtOneYear.Text, txtTwoYear.Text, txtThreeYear.Text);
            lblResult.Text = "Updation successfull";
            txtOneYear.Text = "";
            txtTwoYear.Text = "";
            txtThreeYear.Text = "";
            chkCountries.ClearSelection();
            Grd1.DataSource = objadmin.get_seeker_subscription_price_bycountry().Tables[0];
            Grd1.DataBind();
        }
    }
}
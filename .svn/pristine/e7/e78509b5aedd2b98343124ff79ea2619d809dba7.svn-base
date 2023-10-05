using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Business;

namespace RESTFulWCFService.Admin
{
    public partial class check_Rgist : System.Web.UI.Page
    {
        RegistrationBusiness objRegistrationBusiness = new RegistrationBusiness();
        BusinessMPAdmin objadmin = new BusinessMPAdmin();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fill_dropdown();
                txtdate.Text = Calendar1.TodaysDate.ToShortDateString();

            }
        }

        #region dropdowns
        private void fill_dropdown()
        {
            DataSet ds = new DataSet();
            ds = objRegistrationBusiness.getCountryCity();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlcountry.DataSource = ds.Tables[0];
                ddlcountry.DataValueField = "country_sk";
                ddlcountry.DataTextField = "country_name";
                ddlcountry.DataBind();
                ddlcountry.Items.Insert(0, new ListItem("-Select Country-", "0"));
                //  FillGeoLocation(ds);
            }
        }
        #endregion 
        protected void btnfind_Click(object sender, EventArgs e)
        {
            try
            {
                string date = (Convert.ToDateTime(txtdate.Text.Trim().ToString())).ToString("yyyyMMdd");
                DataTable ds = objadmin.get_no_of_registration(date, Convert.ToInt32(ddlcountry.SelectedValue.ToString()), Convert.ToInt32(ddlState.SelectedValue.ToString()), Convert.ToInt32(ddlCity.SelectedValue.ToString()));
                if (ds.Rows.Count > 0)
                {
                    lblResult.Text = "Total Registrations are : " + ds.Rows[0]["no"].ToString();
                }
                else
                {
                    lblResult.Text = "No registration";
                    lblResult.ForeColor = System.Drawing.Color.Red;
                }
             }
            catch
            {
                try
                {
                    string date = (Convert.ToDateTime(txtdate.Text.Trim().ToString())).ToString("yyyyMMdd");
                    DataTable ds = objadmin.get_no_of_registration(date, Convert.ToInt32(ddlcountry.SelectedValue.ToString()), Convert.ToInt32(ddlState.SelectedValue.ToString()), Convert.ToInt32(0));
                    if (ds.Rows.Count > 0)
                    {
                        lblResult.Text = "Total Registrations are : " + ds.Rows[0]["no"].ToString();
                    }
                    else
                    {
                        lblResult.Text = "No registration";
                        lblResult.ForeColor = System.Drawing.Color.Red;
                    }
                }
                catch
                {

                    string date = (Convert.ToDateTime(txtdate.Text.Trim().ToString())).ToString("yyyyMMdd");
                    DataTable ds = objadmin.get_no_of_registration(date, Convert.ToInt32(ddlcountry.SelectedValue.ToString()), 0, Convert.ToInt32(0));
                    if (ds.Rows.Count > 0)
                    {
                        lblResult.Text = "Total Registrations are : " + ds.Rows[0]["no"].ToString();
                    }
                    else
                    {
                        lblResult.Text = "No registration";
                        lblResult.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

        protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlState.Enabled = true;
                ddlCity.ToolTip = "";
                DataSet ds = new DataSet();
                ds = objRegistrationBusiness.getStateProvider(Convert.ToInt32(ddlcountry.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ddlcountry.SelectedIndex >= 0)
                    {
                        ddlState.Items.Clear();

                        ddlState.DataSource = ds.Tables[0];
                        ddlState.DataValueField = "state_sk";
                        ddlState.DataTextField = "state_name";
                        ddlState.DataBind();
                        ddlState.Items.Insert(0, new ListItem("All States", "0"));
                        ddlState.Enabled = true;
                        ddlCity.Items.Clear();
                        if (ddlState.SelectedIndex > 0)
                            ddlState.ToolTip = ddlState.SelectedItem.Text;
                        else
                            ddlState.ToolTip = "";
                    }
                    else
                    {
                        ddlState.Items.Clear();
                        ddlCity.Items.Clear();
                        ddlState.Enabled = false;
                        ddlCity.Enabled = false;
                        ddlState.ToolTip = "";
                    }
                }
                else
                {
                    ddlCity.Items.Clear();
                    ddlState.Items.Clear();
                    ddlState.Enabled = false;
                    ddlCity.Enabled = false;

                    if (ddlState.SelectedIndex > 0)
                        ddlState.ToolTip = ddlState.SelectedItem.Text;
                    else
                        ddlState.ToolTip = "";


                }
                ddlcountry.ToolTip = ddlcountry.SelectedItem.Text;
               
            }
            catch (System.Exception ex)
            {
                BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message;
                var st = new System.Diagnostics.StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(1);
                BussinessEntity.ExceptionHandling._lineno = frame.GetFileLineNumber();
                BussinessEntity.ExceptionHandling._methodname = Convert.ToString(frame.GetMethod());
                BussinessEntity.ExceptionHandling._pagename = Convert.ToString(frame.GetFileName());

            }
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlCity.ToolTip = "";
                if (ddlState.SelectedIndex != 0)
                {
                    Session["selected_countryItem"] = ddlcountry.SelectedItem;
                    Session["selected_stateItem"] = ddlState.SelectedItem;
                    Session["selected_cityItem"] = null;
                    //  link1.Visible = false;
                    ViewState["state_sk"] = ddlState.SelectedValue;
                    DataSet ds = new DataSet();
                    ds = objRegistrationBusiness.getCityProvider(Convert.ToInt32(ddlState.SelectedValue), Convert.ToInt32(ddlcountry.SelectedValue));
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        if (ddlState.SelectedIndex != 0)
                        {
                            ddlCity.DataSource = ds.Tables[0];
                            ddlCity.DataValueField = "city_sk";
                            ddlCity.DataTextField = "city_name_display";
                            ddlCity.DataBind();
                            ddlCity.Items.Insert(0, new ListItem("All Cities", "0"));
                            ddlCity.Enabled = true;
                        }
                        else
                        {
                            ddlCity.SelectedIndex = 0;
                            ddlCity.Enabled = false;
                        }
                    }
                    else
                    {
                        ddlCity.Items.Clear();
                        ddlCity.Enabled = false;

                    }
                    if (ddlCity.SelectedIndex > 0)
                        ddlCity.ToolTip = ddlCity.SelectedItem.Text;
                    else
                        ddlCity.ToolTip = "";
                }
                else
                {
                    ddlCity.Items.Clear();
                    ddlCity.Enabled = false;

                }
                if (ddlState.SelectedIndex > 0)
                    ddlState.ToolTip = ddlState.SelectedItem.Text;

            }
            catch (System.Exception ex)
            {
                BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message;
                var st = new System.Diagnostics.StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(1);
                BussinessEntity.ExceptionHandling._lineno = frame.GetFileLineNumber();
                BussinessEntity.ExceptionHandling._methodname = Convert.ToString(frame.GetMethod());
                BussinessEntity.ExceptionHandling._pagename = Convert.ToString(frame.GetFileName());

            }
        }

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Business;
using System.Configuration;
using System.Diagnostics;


namespace RESTFulWCFService.MassagePartener
{
    public partial class index : System.Web.UI.Page
    {
        Business.BusinessSearch objBusinessSearch = new Business.BusinessSearch();
        BusinessMPartener objbusinessmpartener = new BusinessMPartener();
        RegistrationBusiness objRegistrationBusiness = new RegistrationBusiness();
        GetSiteURL gestsiteurl = new GetSiteURL();
        int pos;
        PagedDataSource adsource;
        private int seeker_sk { get; set; }
        private string cities { get; set; }
        private string countries { get; set; }
        private string states { get; set; }
        private string Uip { get; set; }
        private string Ucountry { get; set; }
        private string Ustate { get; set; }
        private string Ucity { get; set; }
        private string UArea { get; set; }
        private string Lag { get; set; }
        private string Lat { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetIP();
                fill_dropdowns();
                fillDropdown();
            }
        }

        #region methods
        private void fill_dropdowns()
        {
            DataSet ds;
            //Fill therapies
            ds = objBusinessSearch.getSpaType("M");
            if (ds.Tables[1].Rows.Count > 0)
            {
                ddlmassagetypes.DataSource = ds.Tables[1];
                ViewState["Specilization"] = ds.Tables[1];
                ddlmassagetypes.DataValueField = "sub_service_sk";
                ddlmassagetypes.DataTextField = "sub_service_name";
                ddlmassagetypes.DataBind();
                ddlmassagetypes.Enabled = true;
                ddlmassagetypes.Items.Insert(0, new ListItem("Select Massage Type", "0"));
            }
            ds = objbusinessmpartener.get_agegroup();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlage.DataSource = ds.Tables[0];
                ddlage.DataTextField = "age_group_name";
                ddlage.DataValueField = "age_group_sk";
                ddlage.DataBind();
                ddlage.SelectedValue = "0";
                ddlage.Items.Insert(0, new ListItem("Age Group", "0"));
            }
        }
        public void GetIP()
        {
            //try
            //{


                DataTable table_gio_location = new DataTable();
                table_gio_location = gestsiteurl.GetIP();
                if (table_gio_location.Rows.Count > 0)
                {
                    //Assign reciving ip information to properties
                    Uip = table_gio_location.Rows[0]["countryCode"].ToString();
                    Ustate = table_gio_location.Rows[0]["regionName"].ToString();
                    Ucity = table_gio_location.Rows[0]["city"].ToString();
                    Ustate = Ustate.Replace("'", "''");
                    Ucity = Ucity.Replace("'", "''");
                    Lat = table_gio_location.Rows[0]["lat"].ToString();
                    Lag = table_gio_location.Rows[0]["lon"].ToString();
                }




            //}
            //catch
            //{
            //    return;
            //}
        }
        public void getLocalTime(string lat, string log, string ip, int country_sk, int st_sk, int city_sk)
        {
            DataSet ds = new DataSet();
            try
            {
                //get time zone for provider
                ReadWriteWebservice obj = new ReadWriteWebservice();
                //call webservice using class   
                string xml = obj.getLotLong(lat, log);
                System.IO.StringReader sr = new System.IO.StringReader(xml);
                //Read XML File 
                ds.ReadXml(sr);
                if (ds != null)
                {
                    DateTime date;
                    if (ds.Tables[0].TableName == "timezone")
                        date = Convert.ToDateTime(ds.Tables[0].Rows[0]["time"].ToString());

                    else
                        date = System.DateTime.Now;
                    //objBusinessSearch.InsertHiCounter(date, ip, country_sk, st_sk, city_sk);
                    ViewState["state_sk"] = st_sk;
                    ViewState["cntry_sk"] = country_sk;
                    ViewState["city_sk"] = city_sk;
                    //if (country_sk != null)
                    //    top10Links_country(country_sk);

                    //if (country_sk != null && st_sk != null && city_sk != null)
                    //    top10Links(country_sk, st_sk, city_sk);
                    //create session var for hit counter table used on master page.
                    Session["local_date_hit"] = date;
                    Session["local_date_ip"] = ip;
                }
            }
            catch (System.Exception ex)
            {
                BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message;

                // Get stack trace for the exception with source file information
                var st = new System.Diagnostics.StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(1);
                BussinessEntity.ExceptionHandling._lineno = frame.GetFileLineNumber();
                BussinessEntity.ExceptionHandling._methodname = Convert.ToString(frame.GetMethod());
                BussinessEntity.ExceptionHandling._pagename = Convert.ToString(frame.GetFileName());

                Response.Redirect(Constants__.WEB_ROOT + "/ErrorMessage.aspx", false);
                return;
            }

        }
        private void getCountryByGeoIP()
        {
            DataSet ds = new DataSet();
            ds = objRegistrationBusiness.getCountryCity();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlcountry.DataSource = ds.Tables[0];
                ddlcountry.DataValueField = "country_sk";
                ddlcountry.DataTextField = "country_name";
                ddlcountry.DataBind();
                ddlcountry.Items.Insert(0, new ListItem("Country", "0"));
                //  FillGeoLocation(ds);
            }
        }

        private void fillDropdown()
        {
            try
            {
                DataSet ds = new DataSet();


                string mode = "M";
                ds = objBusinessSearch.getSpaType(mode);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    ddlcountry.DataSource = ds.Tables[0];
                    ddlcountry.DataValueField = "country_sk";
                    ddlcountry.DataTextField = "country_name";
                    ddlcountry.DataBind();
                }
                // 224	9	157

                DataRow[] dr;
                dr = ds.Tables[0].Select("country_code='" + Uip + "'");
                int cntry_sk = 0;
                int stt_sk = 0;
                int city_sk = 0;
                ddlcity.Enabled = false;
                //  ddlsArea.Enabled = false;
                ddlstate.Enabled = false;
                foreach (DataRow row in dr)
                {

                    ddlcountry.SelectedValue = row[0].ToString();
                    ddlcountry.ToolTip = ddlcountry.SelectedItem.Text;


                    //fill state                   
                    ds = objRegistrationBusiness.getStateProvider(Convert.ToInt32(ddlcountry.SelectedValue));
                    cntry_sk = Convert.ToInt32(ddlcountry.SelectedValue);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlstate.DataSource = ds.Tables[0];
                        ddlstate.DataValueField = "state_sk";
                        ddlstate.DataTextField = "state_name";
                        ddlstate.DataBind();
                        ddlstate.Enabled = true;
                        //   dvstate.Visible = true;
                        ddlstate.Items.Insert(0, new ListItem("All States", "0"));

                        dr = ds.Tables[0].Select("state_name='" + Ustate + "'");
                        foreach (DataRow sts in dr)
                        {
                            ddlstate.SelectedValue = sts[1].ToString();
                            ddlstate.ToolTip = ddlstate.SelectedItem.Text;
                            stt_sk = Convert.ToInt32(ddlstate.SelectedValue);

                            //Fill City

                            ds = objRegistrationBusiness.getCityProvider(Convert.ToInt32(ddlstate.SelectedValue), Convert.ToInt32(ddlcountry.SelectedValue));
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                if (ddlcountry.SelectedIndex != 0)
                                {
                                    if (ddlstate.SelectedIndex != 0)
                                    {
                                        ddlcity.DataSource = ds.Tables[0];
                                        ddlcity.DataValueField = "city_sk";
                                        ddlcity.DataTextField = "city_name_display";
                                        ddlcity.DataBind();
                                        ddlcity.Items.Insert(0, new ListItem("All Cities", "0"));
                                        ddlcity.Enabled = true;
                                        if (Ucity.Contains("'"))
                                            Ucity = Ucity.Replace("'", "''");

                                        dr = ds.Tables[0].Select("city_name='" + Ucity + "'");
                                        foreach (DataRow ct in dr)
                                        {
                                            ddlcity.SelectedValue = ct[2].ToString();
                                            city_sk = Convert.ToInt32(ddlcity.SelectedValue);
                                            ddlcity.ToolTip = ddlcity.SelectedItem.Text;
                                        }
                                    }
                                }
                            }
                        }

                        //-------------------------------------------------------------------------

                       // getLocalTime(Lat, Lag, HttpContext.Current.Request.UserHostAddress.ToString(), cntry_sk, stt_sk, city_sk);


                    }
                    else
                    {
                        ddlstate.Items.Clear();
                        ddlcity.Items.Clear();
                        ddlstate.Enabled = false;
                        ddlcity.Enabled = false;

                    }
                }
                if (dr.Count() <= 0)
                {
                    Filldefaultstate_city();

                }
                city_changed();

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
                Response.Redirect(Constants__.WEB_ROOT + "/ErrorMessage.aspx", false);
                return;
            }


        }
        private void Filldefaultstate_city()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = objRegistrationBusiness.getStateProvider(Convert.ToInt32(ddlcountry.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ddlcountry.SelectedIndex >= 0)
                    {
                        ddlstate.DataSource = ds.Tables[0];
                        ddlstate.DataValueField = "state_sk";
                        ddlstate.DataTextField = "state_name";
                        ddlstate.DataBind();
                        ddlstate.Items.Insert(0, new ListItem("All States", "0"));
                        // ddlCity.Enabled = true;
                        ddlstate.Enabled = true;
                        //dvstate.Visible = true;
                        //dvArea.Visible = true;
                        //dvcity.Visible = true;

                        ds = objRegistrationBusiness.getCityProvider(Convert.ToInt32(ddlstate.SelectedValue), Convert.ToInt32(ddlcountry.SelectedValue));
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ddlcountry.SelectedIndex != 0)
                            {
                                if (ddlstate.SelectedIndex != 0)
                                {
                                    ddlcity.DataSource = ds.Tables[0];
                                    ddlcity.DataValueField = "city_sk";
                                    ddlcity.DataTextField = "city_name_display";
                                    ddlcity.DataBind();
                                    ddlcity.Items.Insert(0, new ListItem("All Cities", "0"));
                                    ddlcity.Enabled = true;
                                    if (Ucity.Contains("'"))
                                        Ucity = Ucity.Replace("'", "''");
                                    DataRow[] dr = ds.Tables[0].Select("city_name='" + Ucity + "'");
                                    foreach (DataRow ct in dr)
                                    {
                                        ddlcity.SelectedValue = ct[2].ToString();
                                    }
                                    if (ddlcity.SelectedIndex > 0)
                                        ddlcity.ToolTip = ddlcity.SelectedItem.Text;
                                    else
                                        ddlcity.ToolTip = "";
                                }
                            }
                        }
                        ddlcountry.ToolTip = ddlcountry.SelectedItem.Text;
                        if (ddlstate.SelectedIndex > 0)
                            ddlstate.ToolTip = ddlstate.SelectedItem.Text;
                        else
                            ddlstate.ToolTip = "";
                    }
                    else
                    {
                        ddlstate.Items.Clear();
                        ddlcity.Items.Clear();
                        ddlstate.Enabled = false;
                        ddlcity.Enabled = false;
                    }
                }
                else
                {
                    ddlcity.Items.Clear();
                    ddlstate.Items.Clear();
                    ddlstate.Enabled = false;
                    ddlcity.Enabled = false;
                }
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
                Response.Redirect(Constants__.WEB_ROOT + "/ErrorMessage.aspx", false);
                return;
            }
        }



        #endregion

        #region Events
        protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlstate.Enabled = true;
                ddlcity.ToolTip = "";
                if (Convert.ToInt32(ddlcountry.SelectedValue) > 0)
                {
                    ViewState["cntry_sk"] = ddlcountry.SelectedValue;
                    Session["selected_countryItem"] = ddlcountry.SelectedItem;
                    Session["selected_stateItem"] = null;
                    Session["selected_cityItem"] = null;

                }

                DataSet ds = new DataSet();
                ds = objRegistrationBusiness.getStateProvider(Convert.ToInt32(ddlcountry.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ddlcountry.SelectedIndex >= 0)
                    {
                        ddlstate.Items.Clear();

                        ddlstate.DataSource = ds.Tables[0];
                        ddlstate.DataValueField = "state_sk";
                        ddlstate.DataTextField = "state_name";
                        ddlstate.DataBind();
                        ddlstate.Items.Insert(0, new ListItem("All States", "0"));
                        // ddlCity.Enabled = true;
                        ddlstate.Enabled = true;
                        ddlcity.Items.Clear();
                        if (ddlstate.SelectedIndex > 0)
                            ddlstate.ToolTip = ddlstate.SelectedItem.Text;
                        else
                            ddlstate.ToolTip = "";
                    }
                    else
                    {
                        ddlstate.Items.Clear();
                        ddlcity.Items.Clear();
                        ddlstate.Enabled = false;
                        ddlcity.Enabled = false;
                        ddlstate.ToolTip = "";
                    }
                }
                else
                {
                    ViewState.Remove("state_sk");
                    ViewState.Remove("city_sk");
                    ddlcity.Items.Clear();
                    ddlstate.Items.Clear();
                    ddlstate.Enabled = false;
                    ddlcity.Enabled = false;

                    if (ddlstate.SelectedIndex > 0)
                        ddlstate.ToolTip = ddlstate.SelectedItem.Text;
                    else
                        ddlstate.ToolTip = "";


                }
                ddlcountry.ToolTip = ddlcountry.SelectedItem.Text;
                // BindMe();
                bind_area();


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

        void country_changed()
        {
            try
            {
                ddlstate.Enabled = true;
                ddlcity.ToolTip = "";
                if (Convert.ToInt32(ddlcountry.SelectedValue) > 0)
                {
                    ViewState["cntry_sk"] = ddlcountry.SelectedValue;
                    Session["selected_countryItem"] = ddlcountry.SelectedItem;
                    Session["selected_stateItem"] = null;
                    Session["selected_cityItem"] = null;

                }

                DataSet ds = new DataSet();
                ds = objRegistrationBusiness.getStateProvider(Convert.ToInt32(ddlcountry.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ddlcountry.SelectedIndex >= 0)
                    {
                        ddlstate.Items.Clear();

                        ddlstate.DataSource = ds.Tables[0];
                        ddlstate.DataValueField = "state_sk";
                        ddlstate.DataTextField = "state_name";
                        ddlstate.DataBind();
                        ddlstate.Items.Insert(0, new ListItem("All States", "0"));
                        // ddlCity.Enabled = true;
                        ddlstate.Enabled = true;
                        ddlcity.Items.Clear();
                        if (ddlstate.SelectedIndex > 0)
                            ddlstate.ToolTip = ddlstate.SelectedItem.Text;
                        else
                            ddlstate.ToolTip = "";
                    }
                    else
                    {
                        ddlstate.Items.Clear();
                        ddlcity.Items.Clear();
                        ddlstate.Enabled = false;
                        ddlcity.Enabled = false;
                        ddlstate.ToolTip = "";
                    }
                }
                else
                {
                    ViewState.Remove("state_sk");
                    ViewState.Remove("city_sk");
                    ddlcity.Items.Clear();
                    ddlstate.Items.Clear();
                    ddlstate.Enabled = false;
                    ddlcity.Enabled = false;

                    if (ddlstate.SelectedIndex > 0)
                        ddlstate.ToolTip = ddlstate.SelectedItem.Text;
                    else
                        ddlstate.ToolTip = "";


                }
                ddlcountry.ToolTip = ddlcountry.SelectedItem.Text;
                // BindMe();
                bind_area();


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

        void state_changed()
        {
            try
            {
                ddlcity.ToolTip = "";
                if (ddlstate.SelectedIndex != 0)
                {
                    Session["selected_countryItem"] = ddlcountry.SelectedItem;
                    Session["selected_stateItem"] = ddlstate.SelectedItem;
                    Session["selected_cityItem"] = null;
                    //  link1.Visible = false;
                    ViewState["state_sk"] = ddlstate.SelectedValue;
                    DataSet ds = new DataSet();
                    ds = objRegistrationBusiness.getCityProvider(Convert.ToInt32(ddlstate.SelectedValue), Convert.ToInt32(ddlcountry.SelectedValue));
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        if (ddlstate.SelectedIndex != 0)
                        {
                            ddlcity.DataSource = ds.Tables[0];
                            ddlcity.DataValueField = "city_sk";
                            ddlcity.DataTextField = "city_name_display";
                            ddlcity.DataBind();
                            ddlcity.Items.Insert(0, new ListItem("All Cities", "0"));
                            ddlcity.Enabled = true;
                        }
                        else
                        {
                            ddlcity.SelectedIndex = 0;
                            ddlcity.Enabled = false;
                        }
                    }
                    else
                    {
                        ddlcity.Items.Clear();
                        ddlcity.Enabled = false;

                    }
                    if (ddlcity.SelectedIndex > 0)
                        ddlcity.ToolTip = ddlcity.SelectedItem.Text;
                    else
                        ddlcity.ToolTip = "";
                }
                else
                {
                    Session["selected_countryItem"] = ddlcountry.SelectedItem;
                    Session["selected_stateItem"] = null;
                    Session["selected_cityItem"] = null;
                    ddlcity.Items.Clear();
                    ddlcity.Enabled = false;

                }
                if (ddlstate.SelectedIndex > 0)
                    ddlstate.ToolTip = ddlstate.SelectedItem.Text;

                ddlcity_SelectedIndexChanged(null, null);

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
        protected void ddlcity_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlcountry.SelectedIndex > 0 && ddlstate.SelectedIndex > 0 && ddlcity.SelectedIndex > 0)
            //{
            DataSet ds = objRegistrationBusiness.getAreaProvider(Convert.ToInt32(ddlcity.SelectedValue), Convert.ToInt32(ddlstate.SelectedValue), Convert.ToInt32(ddlcountry.SelectedValue));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlarea.DataSource = ds.Tables[0];
                ddlarea.DataTextField = "area_name";
                ddlarea.DataValueField = "area_sk";
                ddlarea.DataBind();
                ddlarea.Items.Insert(0, new ListItem("All Area", "0"));
                ddlarea.Enabled = true;
            }
            else
            {
                ddlarea.Items.Clear();
                ddlarea.Enabled = false;
            }
            //}
            //else { ddlarea.Items.Clear(); ddlarea.Enabled = false; }
        }

        void city_changed()
        {
            if (ddlcountry.SelectedIndex > 0 && ddlstate.SelectedIndex > 0 && ddlcity.SelectedIndex > 0)
            {
                DataSet ds = objRegistrationBusiness.getAreaProvider(Convert.ToInt32(ddlcity.SelectedValue), Convert.ToInt32(ddlstate.SelectedValue), Convert.ToInt32(ddlcountry.SelectedValue));
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlarea.DataSource = ds.Tables[0];
                    ddlarea.DataTextField = "area_name";
                    ddlarea.DataValueField = "area_sk";
                    ddlarea.DataBind();
                    ddlarea.Items.Insert(0, new ListItem("All Area", "0"));
                    ddlarea.Enabled = true;
                }
                else
                {
                    ddlarea.Items.Clear();
                    ddlarea.Enabled = false;
                }
            }
            else { ddlarea.Items.Clear(); ddlarea.Enabled = false; }
        }
        protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlcity.ToolTip = "";
                if (ddlstate.SelectedIndex != 0)
                {
                    Session["selected_countryItem"] = ddlcountry.SelectedItem;
                    Session["selected_stateItem"] = ddlstate.SelectedItem;
                    Session["selected_cityItem"] = null;
                    //  link1.Visible = false;
                    ViewState["state_sk"] = ddlstate.SelectedValue;
                    DataSet ds = new DataSet();
                    ds = objRegistrationBusiness.getCityProvider(Convert.ToInt32(ddlstate.SelectedValue), Convert.ToInt32(ddlcountry.SelectedValue));
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        if (ddlstate.SelectedIndex != 0)
                        {
                            ddlcity.DataSource = ds.Tables[0];
                            ddlcity.DataValueField = "city_sk";
                            ddlcity.DataTextField = "city_name_display";
                            ddlcity.DataBind();
                            ddlcity.Items.Insert(0, new ListItem("All Cities", "0"));
                            ddlcity.Enabled = true;
                        }
                        else
                        {
                            ddlcity.SelectedIndex = 0;
                            ddlcity.Enabled = false;
                        }
                    }
                    else
                    {
                        ddlcity.Items.Clear();
                        ddlcity.Enabled = false;

                    }
                    if (ddlcity.SelectedIndex > 0)
                        ddlcity.ToolTip = ddlcity.SelectedItem.Text;
                    else
                        ddlcity.ToolTip = "";
                }
                else
                {
                    Session["selected_countryItem"] = ddlcountry.SelectedItem;
                    Session["selected_stateItem"] = null;
                    Session["selected_cityItem"] = null;
                    ddlcity.Items.Clear();
                    ddlcity.Enabled = false;

                }
                if (ddlstate.SelectedIndex > 0)
                    ddlstate.ToolTip = ddlstate.SelectedItem.Text;

                ddlcity_SelectedIndexChanged(null, null);

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
        protected void Page_Changed(object sender, EventArgs e)
        {
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            if (pageIndex == 0)
            {
                pos = 0;
                this.ViewState["vs"] = pos;
                btnsearch_Click(null, null);
            }
            else
                if (pageIndex < pos)
                {
                    pos = (int)this.ViewState["vs"];
                    pos -= 1;
                    this.ViewState["vs"] = pos;
                    btnsearch_Click(null, null);
                }
                else if (pageIndex > pos)
                {
                    pos = (int)this.ViewState["vs"];
                    pos += 1;
                    this.ViewState["vs"] = pos;
                    btnsearch_Click(null, null);
                }
                else if (pageIndex == Convert.ToInt32(ViewState["pagecount"].ToString()))
                { pos = adsource.PageCount - 1; btnsearch_Click(null, null); }

        }
        #endregion Events

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            //  Session["Search_data"] = null;
            Session["selected_gender"] = null;
            Session["selected_age"] = null;
            //search();
            string url = Request.RawUrl;
            if (url.Contains("body-massage-partner"))
            {
                Response.Redirect(Constants__.WEB_ROOT + "/body-massage-partner" + search_url(), false);
            }
            else
            {
                Response.Redirect(Constants__.WEB_ROOT + "/massage-partner" + search_url(), false);
            }
        }

        //void search()
        //{
        //    DataSet ds = new DataSet();
        //    int country_sk = 0;
        //    int state_sk = 0;
        //    int city_sk = 0;
        //    char gender = ' ';
        //    int age_group_sk = 0;
        //    string desired_gender = " ";
        //    int massage_type = 0;
        //    int area_sk = 0;
        //    string from_to = "";
        //    string is_outcall = "";

        //    if (ddlcountry.SelectedIndex != 0)
        //    {
        //        country_sk = Convert.ToInt32(ddlcountry.SelectedValue);
        //    }
        //    if (ddlstate.SelectedIndex != 0 && ddlstate.SelectedIndex != -1)
        //        state_sk = Convert.ToInt32(ddlstate.SelectedValue);
        //    if (ddlcity.SelectedIndex != 0 && ddlcity.SelectedIndex != -1)
        //        city_sk = Convert.ToInt32(ddlcity.SelectedValue);
        //    if (ddlgender.SelectedIndex != 0)
        //    {
        //        gender = Convert.ToChar(ddlgender.SelectedValue);
        //        Session["selected_gender"] = gender;
        //    }
        //    if (ddlage.SelectedIndex != 0)
        //    {
        //        age_group_sk = Convert.ToInt32(ddlage.SelectedValue);
        //        Session["selected_age"] = age_group_sk;
        //    }
        //    if (ddllookingfor.SelectedIndex != 0)
        //        desired_gender = ddllookingfor.SelectedValue;
        //    if (ddlmassagetypes.SelectedIndex != 0)
        //        massage_type = Convert.ToInt32(ddlmassagetypes.SelectedValue);
        //    if (ddlarea.SelectedIndex > 0)
        //        area_sk = Convert.ToInt32(ddlarea.SelectedValue);
        //    if (ddlPartner_Types.SelectedIndex > 0)
        //        from_to = ddlPartner_Types.SelectedValue.ToString();
        //    if (ddlOutCall.SelectedIndex > 0)
        //        is_outcall = ddlOutCall.SelectedValue.ToString();

        //    string url = "";
        //    url = Request.RawUrl.ToString();
        //    if (url.Contains("body-massage-partner"))
        //    {
        //        DynamicMeta_partner_types();
        //    }
        //    else
        //    {

        //        DynamicMeta();
        //    }
        //    //if (Session["Search_data"] == null)
        //    //{
        //    ds = objbusinessmpartener.Search_Partner(country_sk, city_sk, age_group_sk, state_sk, area_sk, desired_gender, gender.ToString(), massage_type, from_to, is_outcall);
        //    //}
        //    //else
        //    //{
        //    //    ds = (DataSet)Session["Search_data"];
        //    //}
        //    //Session["Search_data"] = ds;
        //    // string count = ds.Tables[0].Rows.Count.ToString();
        //    bind_datatable(ds.Tables[0]);
        //}
        void bind_area()
        {
            DataSet ds = objRegistrationBusiness.getAreaProvider(0, 0, Convert.ToInt32(ddlcountry.SelectedValue));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlarea.DataSource = ds.Tables[0];
                ddlarea.DataTextField = "area_name";
                ddlarea.DataValueField = "area_sk";
                ddlarea.DataBind();
                ddlarea.Items.Insert(0, new ListItem("All Area", "0"));
                ddlarea.Enabled = true;
            }
            else
            {
                ddlarea.Items.Clear();
                ddlarea.Enabled = false;
            }
        }
        public string search_url()
        {
            string country = "";
            string state = "";
            string city = "";
            string looking = "";
            string area = "";
            string gender = "";
            string mtypes = "";
            string from_to = "";
            string outcall = "";

            country = ddlcountry.SelectedIndex > 0 ? ddlcountry.SelectedItem.Text : "z";
            state = ddlstate.SelectedIndex > 0 ? ddlstate.SelectedItem.Text : "s";
            city = ddlcity.SelectedIndex > 0 ? ddlcity.SelectedItem.Text : "a";
            looking = ddllookingfor.SelectedIndex > 0 ? ddllookingfor.SelectedItem.Text : "all";
            area = ddlarea.SelectedIndex > 0 ? ddlarea.SelectedItem.Text : "All-Area";
            gender = ddlgender.SelectedIndex > 0 ? ddlgender.SelectedItem.Text : "Any";
            mtypes = ddlmassagetypes.SelectedIndex > 0 ? ddlmassagetypes.SelectedItem.Text : "all-types";
            from_to = ddlPartner_Types.SelectedIndex > 0 ? ddlPartner_Types.SelectedItem.Text : "any";
            outcall = ddlOutCall.SelectedIndex > 0 ? ddlOutCall.SelectedValue : "";

            country = country.Replace(' ', '-');
            state = state.Replace(' ', '-');
            state = state.Replace("&", "and");
            city = city.Replace(' ', '-');
            looking = looking.Replace('/', ' ');
            looking = looking.Replace("   ", " ");
            looking = looking.Replace(' ', '-');
            looking = looking.Trim().Replace(' ', '-');
            area = area.Replace(' ', '-');
            gender = gender.Trim().Replace(' ', '-');
            mtypes = mtypes.Trim().Replace(' ', '-');
            from_to = from_to.Replace(" partner ", " ");
            from_to = from_to.Replace(" partner", "");
            from_to = from_to.Trim().Replace(' ', '-');
            string url = "";
            url = Request.RawUrl.ToString();
            if (url.Contains("body-massage-partner"))
            {
                if (outcall != "")
                {
                    url = "/" + country + "/" + state + "/" + city + "/" + area + "/" + from_to + "/" + mtypes + "/" + outcall;
                    return url;
                }
                else
                {
                    url = "/" + country + "/" + state + "/" + city + "/" + area + "/" + from_to + "/" + mtypes;
                    return url;
                }

            }
            else
            {
                url = "/" + country + "/" + state + "/" + city + "/" + area + "/" + gender + "/" + mtypes + "/" + looking;
                return url;
            }



        }
        protected void ddlmassagetypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlmassagetypes.SelectedItem.Text == "Gay Massage")
            {
                ddlgender.SelectedValue = "M";
                ddlPartner_Types.SelectedValue = "MM";
              //  ddlgender.Enabled = false;
                ddlPartner_Types.Enabled = false;
            }
            else
                if (ddlmassagetypes.SelectedItem.Text == "Lesbian Massage")
                {
                    ddlgender.SelectedValue = "F";
                    ddlPartner_Types.SelectedValue = "FF";
                //    ddlgender.Enabled = false;
                    ddlPartner_Types.Enabled = false;
                }
                else
                {
                    ddlgender.Enabled = true;
                    ddlPartner_Types.Enabled = true;
                }
        }

        protected void ddlgender_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlmassagetypes.SelectedIndex = 0;
        }
    }
}
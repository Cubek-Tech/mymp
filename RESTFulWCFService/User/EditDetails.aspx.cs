using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Business;
using System.Diagnostics;
using System.IO;

namespace RESTFulWCFService.MassagePartener.User
{
    public partial class EditDetails : System.Web.UI.Page
    {
        #region objects
        GetSiteURL gestsiteurl = new GetSiteURL();
        RegistrationBusiness objRegistrationBusiness = new RegistrationBusiness();
        Business.BusinessLogin objBusinessLogin = new BusinessLogin();
        Business.BusinessSearch objBusinessSearch = new Business.BusinessSearch();
        BusinessMPartener objbusinessmpartener = new BusinessMPartener();
        DataTable dt;
        DataSet ds;
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
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) - 120));

                if (!IsPostBack)
                {
                    fill_drop_dob();
                    GetIP();
                    fillDropdown();
                    getCountryByGeoIP();
                    fill_partner_details();
                    showOtherImage();
                }
                var ab = Session["UpdateProfile"];
                if (Session["UpdateProfile"]!=null && Session["UpdateProfile"].ToString()!= "" && Session["UpdateProfile"].ToString() == "UpdateProfilee")
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Information updated successfully!";
                    Session.Remove("UpdateProfile");
                }
            }
            catch
            {
                Session["current_url"] = Request.RawUrl;
                Response.Redirect(Constants__.WEB_ROOT, false);
            }

        }
        private void SelectCheckBoxList(CheckBoxList checklist, string valueToSelect)
        {
            ListItem listItem = checklist.Items.FindByValue(valueToSelect);

            if (listItem != null) listItem.Selected = true;
        }
        #region methods
        public void GetIP()
        {
            try
            {


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




            }
            catch
            {
                return;
            }
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
                ddlcountry.Items.Insert(0, new ListItem("---Select---", "0"));
                //  FillGeoLocation(ds);
            }
        }
        private void fillDropdown()
        {
            try
            {
                DataSet ds = new DataSet();
                //Fill Massage Types
                ds = objbusinessmpartener.get_agegroup();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlpartage.DataSource = ds.Tables[0];
                    ddlpartage.DataTextField = "age_group_name";
                    ddlpartage.DataValueField = "age_group_sk";
                    ddlpartage.DataBind();
                    ddlpartage.Items[0].Selected = true;
                }
                ds = objBusinessSearch.getSpaType("M");
                if (ds.Tables[1].Rows.Count > 0)
                {
                    ddlmassagetypes.DataSource = ds.Tables[1];
                    ViewState["Specilization"] = ds.Tables[1];
                    ddlmassagetypes.DataValueField = "sub_service_sk";
                    ddlmassagetypes.DataTextField = "sub_service_name";
                    ddlmassagetypes.DataBind();
                    ddlmassagetypes.Enabled = true;
                    //ddlmassagetypes.Items.Insert(0, new ListItem("Massage Type", "0"));
                }
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
                // get age groups

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
                        ddlstate.Items.Insert(0, new ListItem("---All States---", "0"));

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
                                        ddlcity.Items.Insert(0, new ListItem("---All Cities---", "0"));
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

                        //  getLocalTime(Lat, Lag, HttpContext.Current.Request.UserHostAddress.ToString(), cntry_sk, stt_sk, city_sk);


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
                        ddlstate.Items.Insert(0, new ListItem("---All States---", "0"));
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
                                    ddlcity.Items.Insert(0, new ListItem("---All Cities---", "0"));
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

            }
        }
        private void fill_drop_dob()
        {
            dt = new DataTable();
            dt.Columns.Add("day", typeof(string));
            for (int i = 0; i < 31; i++)
            {
                dt.Rows.Add(i + 1);
            }
            ddlday.DataSource = dt;
            ddlday.DataTextField = "day";
            ddlday.DataValueField = "day";
            ddlday.DataBind();
            ddlday.Items.Insert(0, new ListItem("Day:", "0"));
            dt = new DataTable();
            dt.Columns.Add("year", typeof(string));
            int today_year = Convert.ToInt32(DateTime.UtcNow.ToString("yyyy"));
            today_year = today_year - 18;
            for (int i = 1960; i <= today_year; i++)
            {
                dt.Rows.Add(i.ToString());
            }
            DataView dv = dt.DefaultView;
            dv.Sort = "year desc";
            ddlyear.DataSource = dv.ToTable();
            ddlyear.DataTextField = "year";
            ddlyear.DataValueField = "year";
            ddlyear.DataBind();
            ddlyear.Items.Insert(0, new ListItem("Year:", "0"));

        }
        private void fill_partner_details()
        {
            ds = objbusinessmpartener.get_partner_details(Convert.ToInt32(Session["massage_partner_sk"].ToString()));
            if (ds.Tables.Count > 0)
            {
                txtname.Text = ds.Tables[0].Rows[0]["massage_partner_name"].ToString();
                txtEmailID.Text = ds.Tables[1].Rows[0][0].ToString();
                txtPassword1.Text = ds.Tables[1].Rows[0][1].ToString();
                ddlgender.SelectedIndex = ddlgender.Items.IndexOf(ddlgender.Items.FindByValue(ds.Tables[0].Rows[0]["gender"].ToString()));
                string d = (Convert.ToDateTime(ds.Tables[0].Rows[0]["dob"].ToString())).ToString("d-M-yyyy");
                string[] dob = d.Split('-');
                ddlday.SelectedIndex = ddlday.Items.IndexOf(ddlday.Items.FindByValue(dob[0]));
                ddlmonth.SelectedIndex = ddlmonth.Items.IndexOf(ddlmonth.Items.FindByValue(dob[1]));
                ddlyear.SelectedIndex = ddlyear.Items.IndexOf(ddlyear.Items.FindByText(dob[2]));
                txtcontact.Text = ds.Tables[0].Rows[0]["phone_nos"].ToString();
                if (ds.Tables.Count > 2)
                {
                    for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                    {
                        SelectCheckBoxList(ddlmassagetypes, ds.Tables[2].Rows[i]["sub_service_sk"].ToString());
                    }
                }
                ddlpartage.SelectedIndex = ddlpartage.Items.IndexOf(ddlpartage.Items.FindByValue(ds.Tables[0].Rows[0]["phone_nos"].ToString()));
                ddlpartgen.SelectedIndex = ddlpartgen.Items.IndexOf(ddlpartgen.Items.FindByValue(ds.Tables[0].Rows[0]["desired_gender"].ToString()));
                ddlcountry.SelectedIndex = ddlcountry.Items.IndexOf(ddlcountry.Items.FindByValue(ds.Tables[0].Rows[0]["country_sk"].ToString()));
                ddlcountry_SelectedIndexChanged(null, null);
                ddlstate_SelectedIndexChanged(null, null);
                ddlcity_SelectedIndexChanged(null, null);
                ddlstate.SelectedIndex = ddlstate.Items.IndexOf(ddlstate.Items.FindByValue(ds.Tables[0].Rows[0]["state_sk"].ToString()));
                ddlstate_SelectedIndexChanged(null, null);
                ddlcity_SelectedIndexChanged(null, null);
                ddlcity.SelectedIndex = ddlcity.Items.IndexOf(ddlcity.Items.FindByValue(ds.Tables[0].Rows[0]["city_sk"].ToString()));
                ddlcity_SelectedIndexChanged(null, null);
                if (ds.Tables[0].Rows[0]["area_sk"].ToString() != "")
                    ddlArea.SelectedIndex = ddlArea.Items.IndexOf(ddlArea.Items.FindByValue(ds.Tables[0].Rows[0]["area_sk"].ToString()));
                txtzip.Text = ds.Tables[0].Rows[0]["postal_code"].ToString();
                txtdescription.Text = ds.Tables[0].Rows[0]["my_description"].ToString();
                txtexpe_quali.Text = ds.Tables[0].Rows[0]["my_qualificatiom"].ToString();
                txtmassage_expe.Text = ds.Tables[0].Rows[0]["my_experience"].ToString();
                ddlIam.SelectedIndex = ddlIam.Items.IndexOf(ddlIam.Items.FindByValue(ds.Tables[0].Rows[0]["is_certified"].ToString()));

            }
        }
        private void show_images(DataTable dt)
        {
            switch (dt.Rows.Count)
            {
                case 1:
                    {
                        img1.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + ds.Tables[3].Rows[0]["image_name"].ToString();
                        img2.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        img3.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        img4.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        img5.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        img6.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        break;
                    }
                case 2:
                    {
                        img1.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + ds.Tables[3].Rows[0]["image_name"].ToString();
                        img2.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + ds.Tables[3].Rows[1]["image_name"].ToString();
                        img3.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        img4.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        img5.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        img6.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        break;
                    }
                case 3:
                    {
                        img1.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + ds.Tables[3].Rows[0]["image_name"].ToString();
                        img2.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + ds.Tables[3].Rows[1]["image_name"].ToString();
                        img3.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + ds.Tables[3].Rows[2]["image_name"].ToString();
                        img4.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        img5.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        img6.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        break;
                    }
                case 4:
                    {
                        img1.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + ds.Tables[3].Rows[0]["image_name"].ToString();
                        img2.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + ds.Tables[3].Rows[1]["image_name"].ToString();
                        img3.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + ds.Tables[3].Rows[2]["image_name"].ToString();
                        img4.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + ds.Tables[3].Rows[3]["image_name"].ToString();
                        img5.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        img6.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        break;
                    }
                case 5:
                    {
                        img1.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + ds.Tables[3].Rows[0]["image_name"].ToString();
                        img2.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + ds.Tables[3].Rows[1]["image_name"].ToString();
                        img3.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + ds.Tables[3].Rows[2]["image_name"].ToString();
                        img4.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + ds.Tables[3].Rows[3]["image_name"].ToString();
                        img5.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + ds.Tables[3].Rows[4]["image_name"].ToString();
                        img6.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        break;
                    }
                case 6:
                    {
                        img1.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + ds.Tables[3].Rows[0]["image_name"].ToString();
                        img2.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + ds.Tables[3].Rows[1]["image_name"].ToString();
                        img3.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + ds.Tables[3].Rows[2]["image_name"].ToString();
                        img4.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + ds.Tables[3].Rows[3]["image_name"].ToString();
                        img5.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + ds.Tables[3].Rows[4]["image_name"].ToString();
                        img6.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + ds.Tables[3].Rows[5]["image_name"].ToString();
                        break;
                    }
                default:
                    {
                        img1.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        img2.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        img3.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        img4.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        img5.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        img6.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";

                        break;
                    }

            }
        }
        private DataTable speciality_selection()
        {
            DataTable dt = (DataTable)ViewState["Specilization"];
            int no = dt.Rows.Count;
            DataTable dtnew = new DataTable();
            dtnew.Columns.Add("sub_service_sk", typeof(int));

            for (int i = 0; i < ddlmassagetypes.Items.Count; i++)
            {
                if (ddlmassagetypes.Items[i].Selected == true && dt != null)
                {
                    DataRow[] dr = dt.Select("sub_service_name = '" + ddlmassagetypes.Items[i].Text + "'");
                    dtnew.Rows.Add(Convert.ToInt32(dr[0]["sub_service_sk"]));

                }
            }
            return dtnew;
        }

        private void save_other_images(FileUpload Fu)
        {
            if (Fu.HasFile)
            {
                string imgContentType = Fu.PostedFile.ContentType;
                HttpFileCollection hfc = Request.Files;
                int img_count = 0;
                if (hfc.Count <= 17)    // 15 FILES RESTRICTION.
                {
                    DataTable table = new DataTable();
                    if (ViewState["other_imges"] != null)
                    {
                        table = (DataTable)ViewState["other_imges"];
                        //table.Columns.Add("img_type", typeof(string));
                        //table.Columns.Add("img_name", typeof(string));
                    }
                    else
                    {
                        table = new DataTable();
                        table.Columns.Add("service_provider_sk", typeof(string));
                        table.Columns.Add("image_type", typeof(string));
                        table.Columns.Add("img_name", typeof(string));
                    }
                    //table.Columns.Add("seeker_sk", typeof(string));
                    //table.Columns.Add("img_type", typeof(string));
                    //table.Columns.Add("img_name", typeof(string));
                    int file_coun = 2;
                    // if (tr_menu.Visible == false)
                    file_coun = 1;

                    //for (int i = file_coun; i <= hfc.Count - 1; i++)
                    for (int i = file_coun; i <= hfc.Count; i++)
                    {

                        // Here we add five DataRows.


                        HttpPostedFile hpf = hfc[i - 1];
                        if (hpf.ContentLength > 0)
                        {
                            if (hpf.ContentLength < 20728650 && (hpf.ContentType == "image/jpeg") || (hpf.ContentType == "image/jpg") || (hpf.ContentType == "image/gif") || (hpf.ContentType == "image/png"))
                            {

                                HttpPostedFile file = hfc[i - 1];
                                string Append = String.Format("{0:yyyy-MM-ddhh-mm-ss}", DateTime.Now);
                                String fileExtention = System.IO.Path.GetExtension(file.FileName).ToLower();
                                Random rnd = new Random();
                                int random = rnd.Next(100);
                                Append = Append + (random).ToString();
                                string image_name = Append + fileExtention;
                                table.Rows.Add(Convert.ToInt32(Session["massage_partner_sk"].ToString()), "JM", image_name);
                                string directory = Context.Server.MapPath("~/User/Images/" + image_name);
                                file.SaveAs(directory);
                                System.Threading.Thread.Sleep(800);
                                img_count++;
                            }



                        }
                    }
                    if (table.Rows.Count > 0)
                    {
                        ViewState["other_imges"] = table;

                    }
                }
                lblerroruploadimages.Visible = true;
                lblerroruploadimages.Text = img_count + " images are successfully uploaded";
                lblerroruploadimages.Style.Add("color", "green");
                //Lblerrorotherimage.Text = img_count + " images are successfully uploaded";
                //Lblerrorotherimage.Style.Add("color", "green");
                //ImgBtn_otherimg.Visible = true;
            }

        }

        #endregion



        #region Events

        protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
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

                        ddlstate.DataSource = ds.Tables[0];
                        ddlstate.DataValueField = "state_sk";
                        ddlstate.DataTextField = "state_name";
                        ddlstate.DataBind();
                        ddlstate.Items.Insert(0, new ListItem("---All States---", "0"));
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
                        ddlstate.Items.Add(new ListItem("---All States---", "0"));
                        ddlstate.Enabled = false;
                        ddlcity.Items.Add(new ListItem("---All Cities---", "0"));
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
                    ddlstate.Items.Add(new ListItem("---All States---", "0"));
                    ddlcity.Items.Add(new ListItem("---All Cities---", "0"));
                    ddlstate.Enabled = false;
                    ddlcity.Enabled = false;

                    if (ddlstate.SelectedIndex > 0)
                        ddlstate.ToolTip = ddlstate.SelectedItem.Text;
                    else
                        ddlstate.ToolTip = "";


                }
                ddlcountry.ToolTip = ddlcountry.SelectedItem.Text;
                // BindMe();


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
                            ddlcity.Items.Insert(0, new ListItem("---All Cities---", "0"));
                            ddlcity.Enabled = true;
                        }
                        else
                        {
                            ddlcity.SelectedIndex = 0;
                            ddlcity.Items.Add(new ListItem("---All Cities---", "0"));
                            ddlcity.Enabled = false;
                        }
                    }
                    else
                    {
                        ddlcity.Items.Clear();
                        ddlcity.Items.Add(new ListItem("---All Cities---", "0"));
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
                    ddlcity.Items.Add(new ListItem("---All Cities---", "0"));
                    ddlcity.Enabled = false;

                }
                if (ddlstate.SelectedIndex > 0)
                    ddlstate.ToolTip = ddlstate.SelectedItem.Text;

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

        private void delete_Images_others()
        {
            if (ViewState["other_imges"] != null)
            {
                DataTable dt = (DataTable)ViewState["other_imges"];
                if (flupload1.HasFile || hdn_delete.Value == "D")
                {
                    DataRow[] dr = dt.Select("img_name='" + hdn_Img_other.Value + "'");
                    foreach (DataRow r in dr)
                    {
                        dt.Rows.Remove(r);
                        string src = Context.Server.MapPath("~/User/Images/" + hdn_Img_other.Value);
                        if (File.Exists(src))
                        {
                            File.Delete(src);
                        }
                    }
                }
                if (flupload2.HasFile || hdn_delete1.Value == "D")
                {
                    DataRow[] dr = dt.Select("img_name='" + hdn_Img_other1.Value + "'");
                    foreach (DataRow r in dr)
                    {
                        dt.Rows.Remove(r);
                        string src = Context.Server.MapPath("~/User/Images/" + hdn_Img_other1.Value);
                        if (File.Exists(src))
                        {
                            File.Delete(src);
                        }
                    }
                }
                if (flupload3.HasFile || hdn_delete2.Value == "D")
                {
                    DataRow[] dr = dt.Select("img_name='" + hdn_Img_other2.Value + "'");
                    foreach (DataRow r in dr)
                    {
                        dt.Rows.Remove(r);
                        string src = Context.Server.MapPath("~/User/Images/" + hdn_Img_other2.Value);
                        if (File.Exists(src))
                        {
                            File.Delete(src);
                        }
                    }
                }
                if (flupload4.HasFile || hdn_delete3.Value == "D")
                {
                    DataRow[] dr = dt.Select("img_name='" + hdn_Img_other3.Value + "'");
                    foreach (DataRow r in dr)
                    {
                        dt.Rows.Remove(r);
                        string src = Context.Server.MapPath("~/User/Images/" + hdn_Img_other3.Value);
                        if (File.Exists(src))
                        {
                            File.Delete(src);
                        }
                    }
                }
                if (flupload7.HasFile || hdn_delete4.Value == "D")
                {
                    DataRow[] dr = dt.Select("img_name='" + hdn_Img_other4.Value + "'");
                    foreach (DataRow r in dr)
                    {
                        dt.Rows.Remove(r);
                        string src = Context.Server.MapPath("~/User/Images/" + hdn_Img_other4.Value);
                        if (File.Exists(src))
                        {
                            File.Delete(src);
                        }
                    }
                }
                if (flupload8.HasFile || hdn_delete5.Value == "D")
                {
                    DataRow[] dr = dt.Select("img_name='" + hdn_Img_other5.Value + "'");
                    foreach (DataRow r in dr)
                    {
                        dt.Rows.Remove(r);
                        string src = Context.Server.MapPath("~/User/Images/" + hdn_Img_other5.Value);
                        if (File.Exists(src))
                        {
                            File.Delete(src);
                        }
                    }
                }
            }
        }

        protected void lnkotherimages_Click(object sender, EventArgs e)
        {
            try
            {
                delete_Images_others();

                if (flupload1.HasFile)
                    save_other_images(flupload1);
                else if (flupload2.HasFile)
                    save_other_images(flupload2);
                else if (flupload3.HasFile)
                    save_other_images(flupload3);
                else if (flupload4.HasFile)
                    save_other_images(flupload4);
                else if (flupload7.HasFile)
                    save_other_images(flupload7);
                else
                    save_other_images(flupload8);
                DataTable dt = (DataTable)ViewState["other_imges"];
                string no = dt.Rows.Count.ToString();
                if (ViewState["other_imges"] != null)
                {
                    show_other_images_((DataTable)ViewState["other_imges"]);

                }
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "showAddress('Address');", true);
                //Response.Redirect(Request.RawUrl, false);
            }
            catch (System.Exception ex)
            {
                BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message;
                var st = new System.Diagnostics.StackTrace(ex, true);
                StackFrame[] stackFrames = st.GetFrames();
                foreach (StackFrame stackFrame in stackFrames)
                {
                    Console.WriteLine(stackFrame.GetMethod().Name);   // write method name
                    BussinessEntity.ExceptionHandling._lineno = stackFrame.GetFileLineNumber();
                    BussinessEntity.ExceptionHandling._methodname = Convert.ToString(stackFrame.GetMethod().Name);
                    BussinessEntity.ExceptionHandling._pagename = Convert.ToString(Request.Url.AbsoluteUri);

                }


            }
        }

        protected void Delete_other_image_click2(object sender, EventArgs e)
        {

            try
            {
                if (ViewState["other_imges"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = ViewState["other_imges"] as DataTable;
                    if (dt.Rows.Count > 0)
                    {
                        int count = dt.Rows.Count;
                        for (int i = 0; count > i; i++)
                        {

                            string src = Context.Server.MapPath("~/User/Images/" + dt.Rows[i]["img_name"]);
                            if (File.Exists(src))
                            {
                                File.Delete(src);
                            }
                        }
                    }

                }


                ImgBtn_otherimg.Visible = false;
                Lblerrorotherimage.Text = "";
                ViewState["other_imges"] = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "Delete_image_click(ele_btn, ele_img, fileupl, hdnfield, hdnStatus);", true);
            }
            catch (System.Exception ex)
            {
                BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message;
                var st = new System.Diagnostics.StackTrace(ex, true);
                StackFrame[] stackFrames = st.GetFrames();
                foreach (StackFrame stackFrame in stackFrames)
                {
                    Console.WriteLine(stackFrame.GetMethod().Name);   // write method name
                    BussinessEntity.ExceptionHandling._lineno = stackFrame.GetFileLineNumber();
                    BussinessEntity.ExceptionHandling._methodname = Convert.ToString(stackFrame.GetMethod().Name);
                    BussinessEntity.ExceptionHandling._pagename = Convert.ToString(Request.Url.AbsoluteUri);

                }

                Response.Redirect(Constants__.WEB_ROOT + "/ErrorMessage.aspx", false);
            }


            //  ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "ApplyActiveTab()", true);
        }
        protected void showOtherImage()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = (DataSet)objRegistrationBusiness.getImage(Convert.ToInt32(Session["massage_partner_sk"]), "JM");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    int imgcount = 0;
                    imgcount = ds.Tables[0].Rows.Count;

                    Lblerrorotherimage.Text = "Successfully uploaded";
                    Lblerrorotherimage.Style.Add("color", "green");
                    Lblerrorotherimage.Visible = true;
                    ImgBtn_otherimg.Visible = true;
                    show_other_images(ds.Tables[0]);
                }
                else
                {
                    Lblerrorotherimage.Visible = false;
                    ImgBtn_otherimg.Visible = false;
                }
                //delete old image from image directory         

            }
            catch (System.Exception ex)
            {
                BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message;
                var st = new System.Diagnostics.StackTrace(ex, true);
                StackFrame[] stackFrames = st.GetFrames();
                foreach (StackFrame stackFrame in stackFrames)
                {
                    Console.WriteLine(stackFrame.GetMethod().Name);   // write method name
                    BussinessEntity.ExceptionHandling._lineno = stackFrame.GetFileLineNumber();
                    BussinessEntity.ExceptionHandling._methodname = Convert.ToString(stackFrame.GetMethod().Name);
                    BussinessEntity.ExceptionHandling._pagename = Convert.ToString(Request.Url.AbsoluteUri);

                }

                Response.Redirect(Constants__.WEB_ROOT + "/ErrorMessage.aspx", false);
            }
        }

        private void show_other_images(DataTable dt)
        {
            switch (dt.Rows.Count)
            {
                case 1:
                    {
                        DataTable table;
                        table = new DataTable();
                        table.Columns.Add("service_provider_sk", typeof(string));
                        table.Columns.Add("image_type", typeof(string));
                        table.Columns.Add("img_name", typeof(string));

                        table.Rows.Add(dt.Rows[0]["massage_partner_sk"].ToString(), dt.Rows[0]["image_type"].ToString(), dt.Rows[0]["image_name"].ToString());
                        //table.Columns.Add("img_name", typeof(string));

                        //table.Rows.Add(dt.Rows[0]["image_name"].ToString());
                        img1.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[0]["image_name"].ToString();
                        hdn_Img_other.Value = dt.Rows[0]["image_name"].ToString();
                        img1.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other", "show_delete_image_option('Div45');", true);
                        ViewState["other_imges"] = table;
                        break;
                    }
                case 2:
                    {
                        DataTable table;
                        table = new DataTable();
                        table.Columns.Add("service_provider_sk", typeof(string));
                        table.Columns.Add("image_type", typeof(string));
                        table.Columns.Add("img_name", typeof(string));

                        table.Rows.Add(dt.Rows[0]["massage_partner_sk"].ToString(), dt.Rows[0]["image_type"].ToString(), dt.Rows[0]["image_name"].ToString());
                        table.Rows.Add(dt.Rows[1]["massage_partner_sk"].ToString(), dt.Rows[1]["image_type"].ToString(), dt.Rows[1]["image_name"].ToString());

                        //table.Rows.Add(dt.Rows[0]["image_name"].ToString());
                        //table.Rows.Add(dt.Rows[1]["image_name"].ToString());
                        img1.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[0]["image_name"].ToString();
                        img2.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[1]["image_name"].ToString();
                        hdn_Img_other.Value = dt.Rows[0]["image_name"].ToString();
                        hdn_Img_other1.Value = dt.Rows[1]["image_name"].ToString();
                        img1.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        img2.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other", "show_delete_image_option('Div45');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other1", "show_delete_image_option('Div1');", true);

                        ViewState["other_imges"] = table;
                        break;
                    }
                case 3:
                    {
                        DataTable table;
                        table = new DataTable();
                        table.Columns.Add("service_provider_sk", typeof(string));
                        table.Columns.Add("image_type", typeof(string));
                        table.Columns.Add("img_name", typeof(string));

                        //table.Rows.Add(dt.Rows[0]["image_name"].ToString());
                        //table.Rows.Add(dt.Rows[1]["image_name"].ToString());
                        //table.Rows.Add(dt.Rows[2]["image_name"].ToString());

                        table.Rows.Add(dt.Rows[0]["massage_partner_sk"].ToString(), dt.Rows[0]["image_type"].ToString(), dt.Rows[0]["image_name"].ToString());
                        table.Rows.Add(dt.Rows[1]["massage_partner_sk"].ToString(), dt.Rows[1]["image_type"].ToString(), dt.Rows[1]["image_name"].ToString());
                        table.Rows.Add(dt.Rows[2]["massage_partner_sk"].ToString(), dt.Rows[2]["image_type"].ToString(), dt.Rows[2]["image_name"].ToString());

                        img1.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[0]["image_name"].ToString();
                        img2.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[1]["image_name"].ToString();
                        img3.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[2]["image_name"].ToString();
                        hdn_Img_other.Value = dt.Rows[0]["image_name"].ToString();
                        hdn_Img_other1.Value = dt.Rows[1]["image_name"].ToString();
                        hdn_Img_other2.Value = dt.Rows[2]["image_name"].ToString();
                        img1.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        img2.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        img3.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other", "show_delete_image_option('Div45');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other1", "show_delete_image_option('Div1');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other2", "show_delete_image_option('Div2');", true);

                        ViewState["other_imges"] = table;
                        break;
                    }
                case 4:
                    {
                        DataTable table;
                        table = new DataTable();
                        table.Columns.Add("service_provider_sk", typeof(string));
                        table.Columns.Add("image_type", typeof(string));
                        table.Columns.Add("img_name", typeof(string));

                        ////table.Rows.Add(dt.Rows[0]["image_name"].ToString());
                        ////table.Rows.Add(dt.Rows[1]["image_name"].ToString());
                        ////table.Rows.Add(dt.Rows[2]["image_name"].ToString());
                        ////table.Rows.Add(dt.Rows[3]["image_name"].ToString());
                        //table.Columns.Add(dt.Rows[0]["massage_partner_sk"].ToString());
                        //table.Columns.Add(dt.Rows[0]["image_type"].ToString());
                        table.Rows.Add(dt.Rows[0]["massage_partner_sk"].ToString(), dt.Rows[0]["image_type"].ToString(), dt.Rows[0]["image_name"].ToString());
                        table.Rows.Add(dt.Rows[1]["massage_partner_sk"].ToString(), dt.Rows[1]["image_type"].ToString(), dt.Rows[1]["image_name"].ToString());
                        table.Rows.Add(dt.Rows[2]["massage_partner_sk"].ToString(), dt.Rows[2]["image_type"].ToString(), dt.Rows[2]["image_name"].ToString());
                        table.Rows.Add(dt.Rows[3]["massage_partner_sk"].ToString(), dt.Rows[3]["image_type"].ToString(), dt.Rows[3]["image_name"].ToString());

                        img1.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[0]["image_name"].ToString();
                        img2.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[1]["image_name"].ToString();
                        img3.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[2]["image_name"].ToString();
                        img4.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[3]["image_name"].ToString();
                        hdn_Img_other.Value = dt.Rows[0]["image_name"].ToString();
                        hdn_Img_other1.Value = dt.Rows[1]["image_name"].ToString();
                        hdn_Img_other2.Value = dt.Rows[2]["image_name"].ToString();
                        hdn_Img_other3.Value = dt.Rows[3]["image_name"].ToString();
                        img1.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        img2.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        img3.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        img4.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other", "show_delete_image_option('Div45');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other1", "show_delete_image_option('Div1');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other2", "show_delete_image_option('Div2');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other3", "show_delete_image_option('Div3');", true);

                        ViewState["other_imges"] = table;
                        break;
                    }
                case 5:
                    {
                        DataTable table;
                        table = new DataTable();
                        table.Columns.Add("service_provider_sk", typeof(string));
                        table.Columns.Add("image_type", typeof(string));
                        table.Columns.Add("img_name", typeof(string));


                        table.Rows.Add(dt.Rows[0]["massage_partner_sk"].ToString(), dt.Rows[0]["image_type"].ToString(), dt.Rows[0]["image_name"].ToString());
                        table.Rows.Add(dt.Rows[1]["massage_partner_sk"].ToString(), dt.Rows[1]["image_type"].ToString(), dt.Rows[1]["image_name"].ToString());
                        table.Rows.Add(dt.Rows[2]["massage_partner_sk"].ToString(), dt.Rows[2]["image_type"].ToString(), dt.Rows[2]["image_name"].ToString());
                        table.Rows.Add(dt.Rows[3]["massage_partner_sk"].ToString(), dt.Rows[3]["image_type"].ToString(), dt.Rows[3]["image_name"].ToString());
                        table.Rows.Add(dt.Rows[4]["massage_partner_sk"].ToString(), dt.Rows[4]["image_type"].ToString(), dt.Rows[4]["image_name"].ToString());

                        img1.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[0]["image_name"].ToString();
                        img2.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[1]["image_name"].ToString();
                        img3.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[2]["image_name"].ToString();
                        img4.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[3]["image_name"].ToString();
                        img5.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[4]["image_name"].ToString();
                        hdn_Img_other.Value = dt.Rows[0]["image_name"].ToString();
                        hdn_Img_other1.Value = dt.Rows[1]["image_name"].ToString();
                        hdn_Img_other2.Value = dt.Rows[2]["image_name"].ToString();
                        hdn_Img_other3.Value = dt.Rows[3]["image_name"].ToString();
                        hdn_Img_other4.Value = dt.Rows[4]["image_name"].ToString();
                        img1.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        img2.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        img3.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        img4.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        img5.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other", "show_delete_image_option('Div45');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other1", "show_delete_image_option('Div1');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other2", "show_delete_image_option('Div2');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other3", "show_delete_image_option('Div3');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other4", "show_delete_image_option('Div4');", true);
                        ViewState["other_imges"] = table;
                        break;
                    }
                case 6:
                    {
                        DataTable table;
                        table = new DataTable();
                        table.Columns.Add("service_provider_sk", typeof(string));
                        table.Columns.Add("image_type", typeof(string));
                        table.Columns.Add("img_name", typeof(string));

                        //table.Rows.Add(dt.Rows[0]["image_name"].ToString());
                        //table.Rows.Add(dt.Rows[1]["image_name"].ToString());
                        //table.Rows.Add(dt.Rows[2]["image_name"].ToString());
                        //table.Rows.Add(dt.Rows[3]["image_name"].ToString());
                        //table.Rows.Add(dt.Rows[4]["image_name"].ToString());
                        //table.Rows.Add(dt.Rows[5]["image_name"].ToString());

                        table.Rows.Add(dt.Rows[0]["massage_partner_sk"].ToString(), dt.Rows[0]["image_type"].ToString(), dt.Rows[0]["image_name"].ToString());
                        table.Rows.Add(dt.Rows[1]["massage_partner_sk"].ToString(), dt.Rows[1]["image_type"].ToString(), dt.Rows[1]["image_name"].ToString());
                        table.Rows.Add(dt.Rows[2]["massage_partner_sk"].ToString(), dt.Rows[2]["image_type"].ToString(), dt.Rows[2]["image_name"].ToString());
                        table.Rows.Add(dt.Rows[3]["massage_partner_sk"].ToString(), dt.Rows[3]["image_type"].ToString(), dt.Rows[3]["image_name"].ToString());
                        table.Rows.Add(dt.Rows[4]["massage_partner_sk"].ToString(), dt.Rows[4]["image_type"].ToString(), dt.Rows[4]["image_name"].ToString());
                        table.Rows.Add(dt.Rows[5]["massage_partner_sk"].ToString(), dt.Rows[5]["image_type"].ToString(), dt.Rows[5]["image_name"].ToString());

                        img1.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[0]["image_name"].ToString();
                        img2.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[1]["image_name"].ToString();
                        img3.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[2]["image_name"].ToString();
                        img4.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[3]["image_name"].ToString();
                        img5.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[4]["image_name"].ToString();
                        img6.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[5]["image_name"].ToString();
                        hdn_Img_other.Value = dt.Rows[0]["image_name"].ToString();
                        hdn_Img_other1.Value = dt.Rows[1]["image_name"].ToString();
                        hdn_Img_other2.Value = dt.Rows[2]["image_name"].ToString();
                        hdn_Img_other3.Value = dt.Rows[3]["image_name"].ToString();
                        hdn_Img_other4.Value = dt.Rows[4]["image_name"].ToString();
                        hdn_Img_other5.Value = dt.Rows[5]["image_name"].ToString();
                        img1.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        img2.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        img3.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        img4.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        img5.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        img6.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other", "show_delete_image_option('Div45');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other1", "show_delete_image_option('Div1');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other2", "show_delete_image_option('Div2');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other3", "show_delete_image_option('Div3');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other4", "show_delete_image_option('Div4');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other5", "show_delete_image_option('Div5');", true);
                        ViewState["other_imges"] = table;
                        break;
                    }
                default:
                    {
                        //Img_other.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        //Img_other1.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        //Img_other2.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        //Img_other3.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        //Img_other4.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        //Img_other5.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        //Img_other6.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        //Img_other7.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        break;
                    }
            }
        }

        private void show_other_images_(DataTable dt)
        {
            switch (dt.Rows.Count)
            {
                case 1:
                    {
                        DataTable table;
                        table = new DataTable();
                        table.Columns.Add("service_provider_sk", typeof(string));
                        table.Columns.Add("image_type", typeof(string));
                        table.Columns.Add("img_name", typeof(string));


                        table.Rows.Add(dt.Rows[0]["service_provider_sk"].ToString(), dt.Rows[0]["image_type"].ToString(), dt.Rows[0]["img_name"].ToString());

                        //table.Rows.Add(dt.Rows[0]["img_name"].ToString());
                        img1.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[0]["img_name"].ToString();
                        hdn_Img_other.Value = dt.Rows[0]["img_name"].ToString();
                        img1.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other", "show_delete_image_option('Div45');", true);
                        ViewState["other_imges"] = table;
                        break;
                    }
                case 2:
                    {
                        DataTable table;
                        table = new DataTable();
                        table.Columns.Add("service_provider_sk", typeof(string));
                        table.Columns.Add("image_type", typeof(string));
                        table.Columns.Add("img_name", typeof(string));

                        table.Rows.Add(dt.Rows[0]["service_provider_sk"].ToString(), dt.Rows[0]["image_type"].ToString(), dt.Rows[0]["img_name"].ToString());
                        table.Rows.Add(dt.Rows[1]["service_provider_sk"].ToString(), dt.Rows[1]["image_type"].ToString(), dt.Rows[1]["img_name"].ToString());

                        //table.Rows.Add(dt.Rows[0]["img_name"].ToString());
                        //table.Rows.Add(dt.Rows[1]["img_name"].ToString());
                        img1.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[0]["img_name"].ToString();
                        img2.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[1]["img_name"].ToString();
                        hdn_Img_other.Value = dt.Rows[0]["img_name"].ToString();
                        hdn_Img_other1.Value = dt.Rows[1]["img_name"].ToString();
                        img1.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        img2.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other", "show_delete_image_option('Div45');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other1", "show_delete_image_option('Div1');", true);

                        ViewState["other_imges"] = table;
                        break;
                    }
                case 3:
                    {
                        DataTable table;
                        table = new DataTable();
                        table.Columns.Add("service_provider_sk", typeof(string));
                        table.Columns.Add("image_type", typeof(string));
                        table.Columns.Add("img_name", typeof(string));

                        //table.Rows.Add(dt.Rows[0]["img_name"].ToString());
                        //table.Rows.Add(dt.Rows[1]["img_name"].ToString());
                        //table.Rows.Add(dt.Rows[2]["img_name"].ToString());
                        table.Rows.Add(dt.Rows[0]["service_provider_sk"].ToString(), dt.Rows[0]["image_type"].ToString(), dt.Rows[0]["img_name"].ToString());
                        table.Rows.Add(dt.Rows[1]["service_provider_sk"].ToString(), dt.Rows[1]["image_type"].ToString(), dt.Rows[1]["img_name"].ToString());
                        table.Rows.Add(dt.Rows[2]["service_provider_sk"].ToString(), dt.Rows[2]["image_type"].ToString(), dt.Rows[2]["img_name"].ToString());

                        img1.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[0]["img_name"].ToString();
                        img2.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[1]["img_name"].ToString();
                        img3.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[2]["img_name"].ToString();
                        hdn_Img_other.Value = dt.Rows[0]["img_name"].ToString();
                        hdn_Img_other1.Value = dt.Rows[1]["img_name"].ToString();
                        hdn_Img_other2.Value = dt.Rows[2]["img_name"].ToString();
                        img1.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        img2.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        img3.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other", "show_delete_image_option('Div45');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other1", "show_delete_image_option('Div1');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other2", "show_delete_image_option('Div2');", true);

                        ViewState["other_imges"] = table;
                        break;
                    }
                case 4:
                    {
                        DataTable table;
                        table = new DataTable();
                        table.Columns.Add("service_provider_sk", typeof(string));
                        table.Columns.Add("image_type", typeof(string));
                        table.Columns.Add("img_name", typeof(string));

                        //table.Rows.Add(dt.Rows[0]["img_name"].ToString());
                        //table.Rows.Add(dt.Rows[1]["img_name"].ToString());
                        //table.Rows.Add(dt.Rows[2]["img_name"].ToString());
                        //table.Rows.Add(dt.Rows[3]["img_name"].ToString());

                        table.Rows.Add(dt.Rows[0]["service_provider_sk"].ToString(), dt.Rows[0]["image_type"].ToString(), dt.Rows[0]["img_name"].ToString());
                        table.Rows.Add(dt.Rows[1]["service_provider_sk"].ToString(), dt.Rows[1]["image_type"].ToString(), dt.Rows[1]["img_name"].ToString());
                        table.Rows.Add(dt.Rows[2]["service_provider_sk"].ToString(), dt.Rows[2]["image_type"].ToString(), dt.Rows[2]["img_name"].ToString());
                        table.Rows.Add(dt.Rows[3]["service_provider_sk"].ToString(), dt.Rows[3]["image_type"].ToString(), dt.Rows[3]["img_name"].ToString());

                        img1.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[0]["img_name"].ToString();
                        img2.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[1]["img_name"].ToString();
                        img3.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[2]["img_name"].ToString();
                        img4.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[3]["img_name"].ToString();
                        hdn_Img_other.Value = dt.Rows[0]["img_name"].ToString();
                        hdn_Img_other1.Value = dt.Rows[1]["img_name"].ToString();
                        hdn_Img_other2.Value = dt.Rows[2]["img_name"].ToString();
                        hdn_Img_other3.Value = dt.Rows[3]["img_name"].ToString();
                        img1.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        img2.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        img3.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        img4.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other", "show_delete_image_option('Div45');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other1", "show_delete_image_option('Div1');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other2", "show_delete_image_option('Div2');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other3", "show_delete_image_option('Div3');", true);

                        ViewState["other_imges"] = table;
                        break;
                    }
                case 5:
                    {
                        DataTable table;
                        table = new DataTable();
                        table.Columns.Add("service_provider_sk", typeof(string));
                        table.Columns.Add("image_type", typeof(string));
                        table.Columns.Add("img_name", typeof(string));

                        //table.Rows.Add(dt.Rows[0]["img_name"].ToString());
                        //table.Rows.Add(dt.Rows[1]["img_name"].ToString());
                        //table.Rows.Add(dt.Rows[2]["img_name"].ToString());
                        //table.Rows.Add(dt.Rows[3]["img_name"].ToString());
                        //table.Rows.Add(dt.Rows[4]["img_name"].ToString());
                        table.Rows.Add(dt.Rows[0]["service_provider_sk"].ToString(), dt.Rows[0]["image_type"].ToString(), dt.Rows[0]["img_name"].ToString());
                        table.Rows.Add(dt.Rows[1]["service_provider_sk"].ToString(), dt.Rows[1]["image_type"].ToString(), dt.Rows[1]["img_name"].ToString());
                        table.Rows.Add(dt.Rows[2]["service_provider_sk"].ToString(), dt.Rows[2]["image_type"].ToString(), dt.Rows[2]["img_name"].ToString());
                        table.Rows.Add(dt.Rows[3]["service_provider_sk"].ToString(), dt.Rows[3]["image_type"].ToString(), dt.Rows[3]["img_name"].ToString());
                        table.Rows.Add(dt.Rows[4]["service_provider_sk"].ToString(), dt.Rows[4]["image_type"].ToString(), dt.Rows[4]["img_name"].ToString());

                        img1.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[0]["img_name"].ToString();
                        img2.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[1]["img_name"].ToString();
                        img3.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[2]["img_name"].ToString();
                        img4.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[3]["img_name"].ToString();
                        img5.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[4]["img_name"].ToString();
                        hdn_Img_other.Value = dt.Rows[0]["img_name"].ToString();
                        hdn_Img_other1.Value = dt.Rows[1]["img_name"].ToString();
                        hdn_Img_other2.Value = dt.Rows[2]["img_name"].ToString();
                        hdn_Img_other3.Value = dt.Rows[3]["img_name"].ToString();
                        hdn_Img_other4.Value = dt.Rows[4]["img_name"].ToString();
                        img1.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        img2.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        img3.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        img4.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        img5.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other", "show_delete_image_option('Div45');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other1", "show_delete_image_option('Div1');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other2", "show_delete_image_option('Div2');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other3", "show_delete_image_option('Div3');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other4", "show_delete_image_option('Div4');", true);
                        ViewState["other_imges"] = table;
                        break;
                    }
                case 6:
                    {
                        DataTable table;
                        table = new DataTable();
                        table.Columns.Add("service_provider_sk", typeof(string));
                        table.Columns.Add("image_type", typeof(string));
                        table.Columns.Add("img_name", typeof(string));

                        //table.Rows.Add(dt.Rows[0]["img_name"].ToString());
                        //table.Rows.Add(dt.Rows[1]["img_name"].ToString());
                        //table.Rows.Add(dt.Rows[2]["img_name"].ToString());
                        //table.Rows.Add(dt.Rows[3]["img_name"].ToString());
                        //table.Rows.Add(dt.Rows[4]["img_name"].ToString());
                        //table.Rows.Add(dt.Rows[5]["img_name"].ToString());
                        table.Rows.Add(dt.Rows[0]["service_provider_sk"].ToString(), dt.Rows[0]["image_type"].ToString(), dt.Rows[0]["img_name"].ToString());
                        table.Rows.Add(dt.Rows[1]["service_provider_sk"].ToString(), dt.Rows[1]["image_type"].ToString(), dt.Rows[1]["img_name"].ToString());
                        table.Rows.Add(dt.Rows[2]["service_provider_sk"].ToString(), dt.Rows[2]["image_type"].ToString(), dt.Rows[2]["img_name"].ToString());
                        table.Rows.Add(dt.Rows[3]["service_provider_sk"].ToString(), dt.Rows[3]["image_type"].ToString(), dt.Rows[3]["img_name"].ToString());
                        table.Rows.Add(dt.Rows[4]["service_provider_sk"].ToString(), dt.Rows[4]["image_type"].ToString(), dt.Rows[4]["img_name"].ToString());
                        table.Rows.Add(dt.Rows[5]["service_provider_sk"].ToString(), dt.Rows[5]["image_type"].ToString(), dt.Rows[5]["img_name"].ToString());

                        img1.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[0]["img_name"].ToString();
                        img2.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[1]["img_name"].ToString();
                        img3.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[2]["img_name"].ToString();
                        img4.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[3]["img_name"].ToString();
                        img5.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[4]["img_name"].ToString();
                        img6.ImageUrl = Constants__.WEB_ROOT_CDN + "/User/Images/" + dt.Rows[5]["img_name"].ToString();
                        hdn_Img_other.Value = dt.Rows[0]["img_name"].ToString();
                        hdn_Img_other1.Value = dt.Rows[1]["img_name"].ToString();
                        hdn_Img_other2.Value = dt.Rows[2]["img_name"].ToString();
                        hdn_Img_other3.Value = dt.Rows[3]["img_name"].ToString();
                        hdn_Img_other4.Value = dt.Rows[4]["img_name"].ToString();
                        hdn_Img_other5.Value = dt.Rows[5]["img_name"].ToString();
                        img1.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        img2.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        img3.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        img4.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        img5.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        img6.Attributes.Add("Style", "display: inherit;height: 100px;width: 100%;background:#fff ");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other", "show_delete_image_option('Div45');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other1", "show_delete_image_option('Div1');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other2", "show_delete_image_option('Div2');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other3", "show_delete_image_option('Div3');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other4", "show_delete_image_option('Div4');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script_Img_other5", "show_delete_image_option('Div5');", true);
                        ViewState["other_imges"] = table;
                        break;
                    }
                default:
                    {
                        //Img_other.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        //Img_other1.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        //Img_other2.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        //Img_other3.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        //Img_other4.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        //Img_other5.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        //Img_other6.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        //Img_other7.ImageUrl = Constants__.WEB_ROOT_CDN + "/image/upload-image.png";
                        break;
                    }
            }
        }

        #endregion

        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                char gender = ' ';
                int? state_sk = null;
                int? city_sk = null;
                int? area_sk = null;
                int massage_partner_sk = Convert.ToInt32(Session["massage_partner_sk"].ToString());
                string email = txtEmailID.Text.Trim();
                string password = txtPassword1.Text.Trim();
                string name = txtname.Text.Trim();
                string g = ddlgender.SelectedValue.ToString();
                if (g == "M")
                    gender = 'M';
                else
                    gender = 'F';
                string dob = ddlday.SelectedItem.Text + "-" + ddlmonth.SelectedItem.Text + "-" + ddlyear.SelectedItem.Text;
                string phone = txtcontact.Text.Trim();
                string part_gen = ddlpartgen.SelectedItem.Value.ToString();
                int part_age = Convert.ToInt32(ddlpartage.SelectedValue);
                string postal = txtzip.Text.Trim();
                int country_sk = Convert.ToInt32(ddlcountry.SelectedValue);
                if (ddlstate.Enabled == true)
                    state_sk = Convert.ToInt32(ddlstate.SelectedValue);
                else
                    state_sk = 0;
                if (ddlcity.Enabled == true)
                    city_sk = Convert.ToInt32(ddlcity.SelectedValue);
                else
                    city_sk = 0;
                if (ddlArea.Enabled == true)
                    area_sk = Convert.ToInt32(ddlArea.SelectedIndex);
                else
                    area_sk = 0;
                string is_certified = "";
                //if (ddlmassagetypes.SelectedIndex == -1)
                //{
                //    lbllstCondition.Text = "Please select any one service type!!";
                //    lbllstCondition.Style.Add("color", "red");
                //    lbllstCondition.Visible = true;
                //    return;
                //}
                is_certified = ddlIam.SelectedValue.ToString();
                int i = objbusinessmpartener.partener_signup_update(massage_partner_sk, name, gender, dob, is_certified, phone, part_gen, txtdescription.Text, txtexpe_quali.Text, txtmassage_expe.Text, part_age, postal, country_sk, state_sk, city_sk, area_sk, speciality_selection(), (DataTable)ViewState["other_imges"]);
                if (i != 0)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Information updated successfully!";
                    Session["UpdateProfile"] = "UpdateProfilee";

                }
                Response.Redirect(Request.RawUrl, false);
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
            if (ddlcountry.SelectedIndex > 0 && ddlstate.SelectedIndex > 0 && ddlcity.SelectedIndex > 0)
            {
                DataSet ds = objRegistrationBusiness.getAreaProvider(Convert.ToInt32(ddlcity.SelectedValue), Convert.ToInt32(ddlstate.SelectedValue), Convert.ToInt32(ddlcountry.SelectedValue));
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlArea.Items.Clear();
                    ddlArea.DataSource = ds.Tables[0];
                    ddlArea.DataTextField = "area_name";
                    ddlArea.DataValueField = "area_sk";
                    ddlArea.DataBind();
                    ddlArea.Items.Insert(0, new ListItem("All Area", "0"));
                    ddlArea.Enabled = true;
                }
                else
                {
                    ddlArea.Enabled = false;
                }
            }
            else
                if (ddlcountry.SelectedIndex > 0)
            {
                DataSet ds = objRegistrationBusiness.getAreaProvider(Convert.ToInt32(ddlcity.SelectedValue), Convert.ToInt32(ddlstate.SelectedValue), Convert.ToInt32(ddlcountry.SelectedValue));
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlArea.DataSource = ds.Tables[0];
                    ddlArea.DataTextField = "area_name";
                    ddlArea.DataValueField = "area_sk";
                    ddlArea.DataBind();
                    ddlArea.Items.Insert(0, new ListItem("All Area", "0"));
                    ddlArea.Enabled = true;
                }
                else
                {
                    ddlArea.Items.Clear();
                    ddlArea.Items.Add(new ListItem("---All Area---", "0"));
                    ddlArea.Enabled = false;
                }
            }
            else
            {
                ddlArea.Items.Clear();
                ddlArea.Items.Add(new ListItem("---All Area---", "0"));
                ddlArea.Enabled = false;
            }
        }
    }
}
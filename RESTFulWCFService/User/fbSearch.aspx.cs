using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;

namespace RESTFulWCFService.User
{
    public partial class fbSearch : System.Web.UI.Page
    {
        Business.BusinessSearch objBusinessSearch = new Business.BusinessSearch();
        BusinessMPartener objbusinessmpartener = new BusinessMPartener();
        RegistrationBusiness objRegistrationBusiness = new RegistrationBusiness();
        private string Uip { get; set; }
        private string Ucountry { get; set; }
        private string Ustate { get; set; }
        private string Ucity { get; set; }
        private string Ugender { get; set; }
        private string Uage { get; set; }
        private string Umtypes { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["country"] != null)
            {
                Uip = Request.QueryString["country"].ToString();
                if (Request.QueryString["state"].ToString() != "0")
                    Ustate = Request.QueryString["state"].ToString();
                else
                    Ustate = "0";
                if (Request.QueryString["city"].ToString() != "0")
                    Ucity = Request.QueryString["city"].ToString();
                else
                    Ucity = "0";

                if (Request.QueryString["gender"].ToString() != "0")
                {
                    Ugender = Request.QueryString["gender"].ToString();
                    if (Ugender == "M")
                    { ViewState["ur_gender"] = "Male"; }
                    else
                        if (Ugender == "F")
                        {
                            ViewState["ur_gender"] = "Female";
                        }
                }
                else
                {
                    Ugender = "0";
                    ViewState["ur_gender"] = "Any";
                }

                if (Request.QueryString["mtypes"].ToString() != "0")
                    Umtypes = Request.QueryString["mtypes"].ToString();
                else
                    Umtypes = "0";
                fillDropdown();
                fill_drops();

                string url = "/" + ViewState["selected_country"].ToString().Replace(' ', '-') + "/" + ViewState["selected_state"].ToString().Replace(' ', '-') + "/" + ViewState["selected_city"].ToString().Replace(' ', '-') + "/All-Area/" + ViewState["ur_gender"].ToString() + "/" + ViewState["Selected_massage"].ToString().Replace(' ', '-') + "/all";
                Response.Redirect("https://www.mymassagepartner.com/massage-partner" + url, false);
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

                    ViewState["country"] = ds.Tables[0];
                    //ddlcountry.DataValueField = "country_sk";
                    //ddlcountry.DataTextField = "country_name";
                    //ddlcountry.DataBind();
                }
                // 224	9	157

                DataRow[] dr;
                dr = ds.Tables[0].Select("country_sk='" + Uip + "'");
                int cntry_sk = 0;
                int stt_sk = 0;
                int city_sk = 0;
                foreach (DataRow row in dr)
                {
                    ViewState["selected_country"] = row["country_name"].ToString();
                    ViewState["selected_country_value"] = row["country_sk"].ToString();
                    //ddlcountry.SelectedValue = row["country_name"].ToString();
                    //ddlcountry.ToolTip = ddlcountry.SelectedItem.Text;


                    //fill state                   
                    ds = objRegistrationBusiness.getStateProvider(Convert.ToInt32(ViewState["selected_country_value"]));
                    cntry_sk = Convert.ToInt32(ViewState["selected_country_value"].ToString());
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ViewState["state"] = ds.Tables[0];
                        //ddlstate.DataValueField = "state_sk";
                        //ddlstate.DataTextField = "state_name";
                        if (Ustate != "0")
                        {
                            dr = ds.Tables[0].Select("state_sk='" + Ustate + "'");
                            foreach (DataRow sts in dr)
                            {
                                ViewState["selected_state_value"] = sts[1].ToString();
                                ViewState["selected_state"] = sts["state_name"].ToString();
                                //Fill City

                                ds = objRegistrationBusiness.getCityProvider(Convert.ToInt32(ViewState["selected_state_value"].ToString()), Convert.ToInt32(ViewState["selected_country_value"].ToString()));
                                if (ds.Tables[0].Rows.Count > 0)
                                {

                                    ViewState["state"] = ds.Tables[0];
                                    //ddlcity.DataValueField = "city_sk";
                                    //ddlcity.DataTextField = "city_name_display";
                                    if (Ucity.Contains("'"))
                                        Ucity = Ucity.Replace("'", "''");
                                    if (Ucity != "0")
                                    {
                                        dr = ds.Tables[0].Select("city_sk='" + Ucity + "'");
                                        foreach (DataRow ct in dr)
                                        {
                                            ViewState["selected_city_value"] = ct[2].ToString();
                                            ViewState["selected_city"] = ct["city_name_display"].ToString();
                                            //city_sk = Convert.ToInt32(ddlcity.SelectedValue);
                                            //ddlcity.ToolTip = ddlcity.SelectedItem.Text;
                                        }
                                    }
                                    else
                                    {
                                        ViewState["selected_city"] = "s";
                                    }

                                }
                            }
                        }
                        else
                        { ViewState["selected_state"] = "s"; ViewState["selected_city"] = "a"; }

                        //-------------------------------------------------------------------------

                        //getLocalTime(Lat, Lag, HttpContext.Current.Request.UserHostAddress.ToString(), cntry_sk, stt_sk, city_sk);


                    }
                    else
                    {
                        //ddlstate.Items.Clear();
                        //ddlcity.Items.Clear();
                        //ddlstate.Enabled = false;
                        //ddlcity.Enabled = false;

                    }
                }
                if (dr.Count() <= 0)
                {
                    //  Filldefaultstate_city();

                }
                // ddlcity_SelectedIndexChanged(null, null);

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
            }


        }
        private void fill_drops() {
            DataSet ds;
            //Fill therapies
            if (Umtypes != "0")
            {
                ds = objBusinessSearch.getSpaType("M");
                if (ds.Tables[1].Rows.Count > 0)
                {
                    DataRow[] dr;
                    dr = ds.Tables[1].Select("sub_service_sk='" + Umtypes + "'");
                    foreach (DataRow row in dr)
                    {
                        ViewState["Selected_massage"] = row["sub_service_name"].ToString();
                    }
                }
            }
            else
            {
                ViewState["Selected_massage"] = "all-types";
            }
        }
    }
}
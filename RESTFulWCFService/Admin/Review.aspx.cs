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
    public partial class Review : System.Web.UI.Page
    {
        RegistrationBusiness objRegistrationBusiness = new RegistrationBusiness();
        Business.BusinessMPartener objbusinessmpartener = new Business.BusinessMPartener();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                get_country();
            }
        }
        #region methods
        private void get_country()
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

        protected void btnsave_Click(object sender, EventArgs e)
        {
            int i = objbusinessmpartener.insert_reviews(Convert.ToInt32(ddlcountry.SelectedValue),txtcommentby.Text.Trim().ToString(),Convert.ToInt32(DropDownList1.SelectedItem.Text),txtMessage.Text.Trim().ToString());
            if (i != 0)
            {
                lblresult.Text = "Review sent successfully!";
                lblresult.ForeColor = System.Drawing.Color.Green;
                ddlcountry.SelectedIndex = 0;
                txtcommentby.Text = "";
                DropDownList1.SelectedIndex = 0;
                txtMessage.Text = "";
            }
        }
    }
}
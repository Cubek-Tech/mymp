using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using System.Data;
using System.Data.SqlClient;

namespace RESTFulWCFService.Admin
{
    public partial class deletePartner : System.Web.UI.Page
    {
        BusinessMPAdmin objAdmin = new BusinessMPAdmin();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            DataSet ds = objAdmin.delete_partner_by_emailid(txtEmail_id.Text.Trim());
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        lblResult.Text = "Partner deleted successfully !";
                        txtEmail_id.Text = "";
                        lblResult.ForeColor = System.Drawing.Color.Green;
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        lblResult.Text = "Partner not found !";
                        txtEmail_id.Text = "";
                        lblResult.ForeColor = System.Drawing.Color.Red; 
                    }
                }
            }
        }
    }
}
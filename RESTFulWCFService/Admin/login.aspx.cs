﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RESTFulWCFService.Admin
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            if (txtLoginID.Text.Trim().ToString().ToLower() == "info@mymassagepartner.com" && txtPassword.Text.Trim().ToString() == ConfigurationManager.AppSettings["Admin_Login"].ToString())
            {
                Session["user"] = "Admin";
                Response.Redirect(Constants__.WEB_ROOT+"/Admin/Admin_home.aspx",false);
            }
            else
            {
                lblerror.Text = "Invalid Id/Password!";
                lblerror.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RESTFulWCFService;
public partial class _404Error : System.Web.UI.Page
{
    string currentUrl = "findthespa";
    GetSiteURL gestsiteurl = new GetSiteURL();
    protected void Page_Load(object sender, EventArgs e)
    {

        //currentUrl = gestsiteurl.GetSiteURL_();
        //if (currentUrl.Contains("massage2book"))
        //{
        //    m2berror.Visible = true;
        //    ftserror.Visible = false;

        //}
        //else
        //{
        //    body.Style.Add("background", "none repeat scroll 0 0 #813636;");
        //    body.Style.Add("color", "#fff600;");
        //    m2berror.Visible = false;
        //    ftserror.Visible = true;
        //}



    }
}

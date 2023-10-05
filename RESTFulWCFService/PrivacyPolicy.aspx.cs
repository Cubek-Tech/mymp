using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using RESTFulWCFService;
using System.Diagnostics;
public partial class PrivacyPolicy : System.Web.UI.Page
{
    GetSiteURL gestsiteurl = new GetSiteURL();
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {

            Session["ShowSRPUrl"] = "ShowUrl";

            HtmlMeta Meta = new HtmlMeta();
            HtmlMeta keyword = new HtmlMeta();
            string currentUrl = gestsiteurl.GetSiteURL_();



            Meta.Name = "Massage2book - Privacy Policy";
            keyword.Name = "Massage2book, Parlor, Massage Parlor Owner, Privacy Policy";
            metaDescription.Controls.Add(Meta);
            keywordcontent.Controls.Add(keyword);

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
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BussinessEntity;
using RESTFulWCFService;
using Business;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;


public partial class ErrorMessage : System.Web.UI.Page
{
    /// <summary>
    /// Update by pavan shrivastava on 04/01/2011
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    GetSiteURL gestsiteurl = new GetSiteURL();
    #region PageLoad
    Business.BussinessSendMail obj = new BussinessSendMail();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {



            // string fullname1 =Request.QueryString["logsk"];
            // if (fullname1 != null && fullname1 !="")
            //{
            //    DataTable dt = new DataTable();
            //    dt = obj.Geterrorimagedata(fullname1);
            //    img_val.Src = "fullname1";
            //}
            //else
            //{
            string snapshot = "";
            string imagepath = ""; string imagepath2 = "";
            string pagedetails;
            if (Session["snapshot"] != null)
            {

                string currentUrl1 = Request.Url.Authority;
                imagepath = Session["snapshot"].ToString();
                //  imagepath=imagepath.Replace(" ", "");
                imagepath2 = imagepath.Replace(":", "");
                // snapshot = Constants__.WEB_ROOT + "/ErrorMessage.aspx?logsk=@href";
            }

            //  addedd by amit  for change subject for exception error
            string Subjectstring = "Exception Occured on mymassagepartner.com";

            string currentUrl = Request.Url.ToString();
            if (currentUrl.Contains("www.mymassagepartner.com"))
            {
                Subjectstring = "Exception Occured on mymassagepartner.com";
            }
            else if (currentUrl.Contains("www.findthespa.com"))
            {

                Subjectstring = "Exception Occured on FindtheSpa.com";
            }

            else
            {
                Subjectstring = "Exception Occured on dev";
            }

            if ((Session["currentPageUrl"]).ToString() != null && (Session["currentPageUrl"]).ToString() != "")
            {
                pagedetails = (Session["currentPageUrl"]).ToString();
                obj.SendMail("info@mymassagepartner.com", "hrtpandey90@gmail.com", "Error page has opened " + Subjectstring, pagedetails, null);  //added by nilesh
                Session.Remove("currentPageUrl");
            }
            else
            {
                obj.SendMail("info@mymassagepartner.com", "hrtpandey90@gmail.com", "Error page has opened " + Subjectstring, "Exception Occured ", null);  //added by nilesh

            }
            string urlName = Convert.ToString(HttpContext.Current.Request.UrlReferrer) == "" ? "#" : Convert.ToString(HttpContext.Current.Request.UrlReferrer);

            String strHost = HttpContext.Current.Request.Url.Host;

            string ip = HttpContext.Current.Request.UserHostAddress.ToString();

            //strHost = strHost.ToLower();

            //check exception type
            //if (BussinessEntity.ExceptionHandling.ErrorMessage != null && BussinessEntity.ExceptionHandling.ErrorMessage != "")


            if (BussinessEntity.ExceptionHandling.ErrorMessage.ToString().Contains("Timeout"))
            {
                divMsg.Visible = true;
                string page;
                string method;
                string body;
                int lineno;
                divMsg.InnerText = "Your request cannot be completed at this time. Please refine your search criteria.";

              

                string messg = BussinessEntity.ExceptionHandling.ErrorMessage.ToString();
                          

                if (BussinessEntity.ExceptionHandling._pagename != null)
                    page = BussinessEntity.ExceptionHandling._pagename.ToString();

                else
                    page = BussinessEntity.ExceptionHandling._pagename;
                if (BussinessEntity.ExceptionHandling._methodname != null)
                    method = BussinessEntity.ExceptionHandling._methodname.ToString();

                else
                    method = "null";

                if (BussinessEntity.ExceptionHandling._lineno > 0)
                    lineno = BussinessEntity.ExceptionHandling._lineno;

                else
                    lineno = 0;


           
                string ipAddress = Request.ServerVariables["LOCAL_ADDR"];

                body = @"<table style='font-family: Arial,Helvetica,sans-serif;' width='680px' cellpadding='1' cellspacing='1'>
        
            <tr>
                <td style='color: White; float: left; font-size: 18px; font-weight: bold; margin-left: 10px;
                    padding-left: 4px; text-align: left; background-color: rgb(242, 242, 242); padding-bottom: 10px;
                    width: 686px; font-family: Arial,Helvetica,sans-serif;'>
                    <table width='680px;'>
                      
                        <tr>
                            <td style='font-size: 11px; font-family: Arial,Helvetica,sans-serif; color: #333333;
                                margin: 10px 10px 10px 10px; line-height: 20px;'>
                                    <span style='color: #000000; float: left; font-size: 14px; font-family: Arial,Helvetica,sans-serif;' > Soruce Page Name </span>
                           <br />
                                                   " + page + @"

                                                      <br />
   <br />
                             <span style='color: #000000; float: left; font-size: 14px; font-family: Arial,Helvetica,sans-serif;' > Error Message </span>
                                   <br />
                               
                               " + messg + @"
                                <br />
<br />
                                           <span style='color: #000000; float: left; font-size: 14px; font-family: Arial,Helvetica,sans-serif;' > Calling Method :</span>
                               
                                     <br/>
                               "
                                  + method + @"
                                 <br/>
<br />

                                  <span style='color: #000000; float: left; font-size: 14px; font-family: Arial,Helvetica,sans-serif;' > Line No.</span>
                                   <br/>" + lineno + @"
<br/>
<br />

                                  <span style='color: #000000; float: left; font-size: 14px; font-family: Arial,Helvetica,sans-serif;' > IP Address</span>
                                   <br/>" + ip + @"

                                <br />  <br /> 
                            <span style='color: #000000; float: left; font-size: 14px; font-family: Arial,Helvetica,sans-serif;' > Server Ip Address</span>
                                   <br/><b>" + ipAddress + @"

                               <br />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
<tr>
<td>" + imagepath + @"

</td>
</tr>
        </table>";




                obj.SendMail("info@mymassagepartner.com", "hrtpandey90@gmail.com", Subjectstring, body, null);


                if (messg.IndexOf("OutOfMemory") > -1)
                {
                    Session.RemoveAll();
                    Response.Redirect(Constants__.WEB_ROOT, false);
                    return;
                }
            }
            else
            {
                divMsg.Visible = true;
                string page;
                string method;
                string body;
                int? lineno;
                string massage_partner_sk = "";
                string Server_IPAddress_Address = "";



                string ipAddress = Request.ServerVariables["LOCAL_ADDR"];

                if (Session["massage_partner_sk"] != null && Convert.ToString(Session["massage_partner_sk"]) != "")
                { massage_partner_sk = "massage_partner_sk -" + Convert.ToString(Session["massage_partner_sk"]); }


                if (Session["mp_login_sk"] != null && Convert.ToString(Session["mp_login_sk"]) != "")
                { massage_partner_sk = massage_partner_sk + " /massage_partner_sk -" + Convert.ToString(Session["mp_login_sk"]); }



                divMsg.InnerText = "Your request cannot be completed at this time. Please try again.";
                string messg = BussinessEntity.ExceptionHandling.ErrorMessage.ToString();
                if (BussinessEntity.ExceptionHandling._pagename != null)
                    page = BussinessEntity.ExceptionHandling._pagename.ToString();

                else
                    page = BussinessEntity.ExceptionHandling._pagename.ToString();
                if (BussinessEntity.ExceptionHandling._methodname != null)
                    method = BussinessEntity.ExceptionHandling._methodname.ToString();

                else
                    method = "null";

                if (BussinessEntity.ExceptionHandling._lineno != null || BussinessEntity.ExceptionHandling._lineno > 0)
                    lineno = BussinessEntity.ExceptionHandling._lineno;

                else
                    lineno = 0;

                body = @"<table style='font-family: Arial,Helvetica,sans-serif;' width='680px' cellpadding='1' cellspacing='1'>
        
            <tr>
                <td style='color: White; float: left; font-size: 18px; font-weight: bold; margin-left: 10px;
                    padding-left: 4px; text-align: left; background-color: rgb(242, 242, 242); padding-bottom: 10px;
                    width: 686px; font-family: Arial,Helvetica,sans-serif;'>
                    <table width='680px;'>
                      
                        <tr>
                            <td style='font-size: 11px; font-family: Arial,Helvetica,sans-serif; color: #333333;
                                margin: 10px 10px 10px 10px; line-height: 20px;'>
                                    <span style='color: #000000; float: left; font-size: 14px; font-family: Arial,Helvetica,sans-serif;' > Soruce Page Name </span>
                           <br />
                                                   " + page + @"

                                                      <br />
   <br />
                             <span style='color: #000000; float: left; font-size: 14px; font-family: Arial,Helvetica,sans-serif;' > Error Message </span>
                                   <br />
                               
                               " + messg + @"
                                <br />
<br />
                                           <span style='color: #000000; float: left; font-size: 14px; font-family: Arial,Helvetica,sans-serif;' > Calling Method :</span>
                               
                                     <br/>
                               "
                                  + method + @"
                                 <br/>
<br />

                                  <span style='color: #000000; float: left; font-size: 14px; font-family: Arial,Helvetica,sans-serif;' > Line No.</span>
                                   <br/>" + lineno + @"
<br/>
<br />

                                  <span style='color: #000000; float: left; font-size: 14px; font-family: Arial,Helvetica,sans-serif;' > IP Address</span>
                                   <br/>" + ip + @"

                                <br />
                               <br /> 
                            <span style='color: #000000; float: left; font-size: 14px; font-family: Arial,Helvetica,sans-serif;' > Provider/ Seeker Sk</span>
                                   <br/><b>" + massage_partner_sk + @"
  <br />
                               <br /> 
                            <span style='color: #000000; float: left; font-size: 14px; font-family: Arial,Helvetica,sans-serif;' > Server Ip Address</span>
                                   <br/><b>" + ipAddress + @"

                               </b> <br />
                               <br />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
<tr>
<td>" + imagepath + @"<br><br>

 
</td>
</tr>
        </table>
";

                obj.SendMail("info@mymassagepartner.com", "hrtpandey90@gmail.com", Subjectstring, body, null);

                if (messg.IndexOf("OutOfMemory") > -1)
                {
                    Session.RemoveAll();
                    Response.Redirect(Constants__.WEB_ROOT, false);
                    return;
                }
            }



            currentUrl = gestsiteurl.GetSiteURL_();
            if (currentUrl.Contains("mymassagepartner"))
            {
                lnkBack.PostBackUrl = Constants__.WEB_ROOT;
                title_head.Text = "Nothing found for 404-mymassagepartner";
                //default fevicon            
            }
          
            return;
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
                BussinessEntity.ExceptionHandling._pagename = Convert.ToString(stackFrame.GetFileName());

            }

            //  Response.Redirect(Constants__.WEB_ROOT + "/ErrorMessage.aspx", false);
        }
    }
    #endregion
}
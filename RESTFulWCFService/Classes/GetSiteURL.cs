using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.ComponentModel;
using System.Web.Caching;
using System.Net;
using System.IO;
using Factory;
using System.Web.Configuration;
using RESTFulWCFService;
using System.Diagnostics;
using Newtonsoft.Json;

namespace RESTFulWCFService
{
    public class GetSiteURL
    {



        public string GetSiteURL_()
        {
            string currentUrl = HttpContext.Current.Request.Url.AbsoluteUri;
            if (currentUrl.Contains("massage2book"))
            {
                return "massage2book";
            }
            else if (currentUrl.Contains("findthespa"))
            {
                return "findthespa";
            }
            else
            {
                //string path = Constants__.WEB_ROOT + "/siteurl.xml";
                //XmlTextReader reader = new XmlTextReader(path);
                //while (reader.Read())
                //{
                //    DataSet _ds = new DataSet();
                //    _ds.ReadXml(reader);
                //    if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0 && _ds.Tables[0].Rows[0]["title"] != null && _ds.Tables[0].Rows[0]["title"].ToString() != "")
                //        currentUrl = _ds.Tables[0].Rows[0]["title"].ToString();

                //}
                return "massage2book";
            }


        }

        public DataTable GetIP()
        {
            try
            {



                if (HttpContext.Current.Session["gio_info"] == null)
                {//

                    try
                    {
                        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["httpPathGeoip"].ToString() + "?key=xBc0M3JDIdBa0oh");
                        HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                        if (response.StatusCode.ToString() == "OK")
                        { }
                        else
                        {

                            DataTable dt = new DataTable();
                            DataColumn dc;
                            dc = new DataColumn("countryCode");
                            dc.DataType = System.Type.GetType("System.String");
                            dt.Columns.Add(dc);

                            dc = new DataColumn("regionName");
                            dc.DataType = System.Type.GetType("System.String");
                            dt.Columns.Add(dc);

                            dc = new DataColumn("city");
                            dc.DataType = System.Type.GetType("System.String");
                            dt.Columns.Add(dc);
                            dc = new DataColumn("lat");
                            dc.DataType = System.Type.GetType("System.String");
                            dt.Columns.Add(dc);


                            dc = new DataColumn("lon");
                            dc.DataType = System.Type.GetType("System.String");
                            dt.Columns.Add(dc);



                            DataRow row = dt.NewRow();
                            row["countryCode"] = ConfigurationManager.AppSettings["default_countryCode"].ToString();
                            row["regionName"] = ConfigurationManager.AppSettings["default_regionName"].ToString();
                            row["city"] = ConfigurationManager.AppSettings["default_city"].ToString();
                            row["lat"] = ConfigurationManager.AppSettings["default_lat"].ToString();
                            row["lon"] = ConfigurationManager.AppSettings["default_lon"].ToString();


                            dt.Rows.Add(row);
                            HttpContext.Current.Session["gio_info"] = dt;
                            return dt;
                        }

                    }
                    catch (System.Exception ex)
                    {

                        DataTable dt = new DataTable();
                        DataColumn dc;
                        dc = new DataColumn("countryCode");
                        dc.DataType = System.Type.GetType("System.String");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("regionName");
                        dc.DataType = System.Type.GetType("System.String");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("city");
                        dc.DataType = System.Type.GetType("System.String");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("lat");
                        dc.DataType = System.Type.GetType("System.String");
                        dt.Columns.Add(dc);


                        dc = new DataColumn("lon");
                        dc.DataType = System.Type.GetType("System.String");
                        dt.Columns.Add(dc);



                        DataRow row = dt.NewRow();
                        row["countryCode"] = ConfigurationManager.AppSettings["default_countryCode"].ToString();
                        row["regionName"] = ConfigurationManager.AppSettings["default_regionName"].ToString();
                        row["city"] = ConfigurationManager.AppSettings["default_city"].ToString();
                        row["lat"] = ConfigurationManager.AppSettings["default_lat"].ToString();
                        row["lon"] = ConfigurationManager.AppSettings["default_lon"].ToString();

                        dt.Rows.Add(row);
                        HttpContext.Current.Session["gio_info"] = dt;
                        return dt;

                    }

                    string ip = HttpContext.Current.Request.UserHostAddress.ToString();
                    //ip = "157.50.49.137";
                    ///ip = "47.9.15.147";   //india   
                    //  ip = "188.135.15.163";  // USA
                    //ip = "49.15.165.215";   //india   
                    // ip = "72.229.28.185";  // USA
                    if (ip != "::1")
                    {
                        UtilityProject.IpTrack _IpTrack = new UtilityProject.IpTrack();
                        string ip_string = ConfigurationManager.AppSettings["httpPathGeoip"].ToString() + "" + ip + "?key=xBc0M3JDIdBa0oh";
                        string res = _IpTrack.ReturnData(ip_string);
                        res = "[" + res + "]";
                        DataTable table_gio_location = new DataTable();
                        table_gio_location = JsonConvert.DeserializeObject<DataTable>(res);
                        HttpContext.Current.Session["gio_info"] = table_gio_location;
                        return table_gio_location;
                    }
                    else
                    {
                        DataTable table_gio_location = new DataTable();
                        return table_gio_location;
                    }
                }
                else
                {
                    DataTable table_gio_location = new DataTable();
                    table_gio_location = HttpContext.Current.Session["gio_info"] as DataTable;
                    return table_gio_location;
                }
            }
            //catch
            //{
            //    return;
            //}
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
                DataTable table_gio_location = new DataTable();
                return table_gio_location;
                // Response.Redirect(Constants__.WEB_ROOT + "/ErrorMessage.aspx", false);
            }
        }
    }

}
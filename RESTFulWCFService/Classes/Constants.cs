﻿using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Factory;

/// <summary>
/// Summary description for Constants
/// </summary>
public class Constants__
{
    public Constants__()
    {

    }


    //public static String WEB_ROOT=(String)System.Configuration.ConfigurationManager.AppSettings["Web_Root"];
    public static String PhysicalLocalHostPath = GetSiteRoot() + "/";
    public static String WEB_ROOT = GetSiteRoot();
    public static String WEB_ROOT_CDN = GetSiteRoot_CDN();
    public static string Rating = string.Empty;
    //public static String PhysicalLocalHostPath=GetSiteRoot()+"/";
    public static String IMAGES = WEB_ROOT + "/Images";
    private static string GetSiteRoot()
    {

        string port = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
        if (port == null || port == "80" || port == "443")
            port = "";
        else
            port = ":" + port;

        string protocol = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];
        if (protocol == null || protocol == "0")
            protocol = "http://";
        else
            protocol = "https://";

        string sOut = protocol + System.Web.HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + port + System.Web.HttpContext.Current.Request.ApplicationPath;

        if (sOut.EndsWith("/"))
        {
            sOut = sOut.Substring(0, sOut.Length - 1);
        }

        return sOut;
    }
    private static string GetSiteRoot_CDN()
    {

        string port = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
        if (port == null || port == "80" || port == "443")
            port = "";
        else
            port = ":" + port;

        string protocol = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];
        if (protocol == null || protocol == "0")
            protocol = "http://";
        else
            protocol = "https://";

        string sOut = protocol + System.Web.HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + port + System.Web.HttpContext.Current.Request.ApplicationPath;

        if (sOut.EndsWith("/"))
        {
            sOut = sOut.Substring(0, sOut.Length - 1);
        }
        //sOut = sOut.Replace("://dev.", "://");
        sOut = sOut.Replace("://www.", "://");
        //sOut = sOut.Replace("://qa.", "://");
        if (sOut.Contains("localhost")) { }
        else if (sOut.Contains("qa.mymassagepartner.com")) { }
        else if (sOut.Contains("dev.mymassagepartner.com")) { }
        else if (!sOut.Contains(".com")) { }
        else
        {

            if (ConfigurationManager.AppSettings["cdn_option"].ToString().Trim() != "")
            { sOut = sOut.Replace("://", "://" + ConfigurationManager.AppSettings["cdn_option"].ToString().Trim() + "."); }
            else
            { sOut = sOut.Replace("://", "://www."); }
       }

        return sOut;
    }

    public static void WriteException(Exception ex, string url, string path, string method)
    {
        ErrorHandling handling = new ErrorHandling();
        handling.reqUrl =
        handling.filePath = path + "\\errorLOG" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
        handling.methodname = method;
        handling.lastErrorTypeName = ex.GetType().ToString();
        handling.lastErrorMessage = ex.Message;
        handling.lastErrorStackTrace = ex.StackTrace;
        handling.writefile();
    }
}
public class SerachingResult
{
    public static int result_count { get; set; }
    public static DateTime search_date { get; set; }
    public static DateTime current_date { get; set; }
    //  public static int?      category_sk  { get; set; }
    //   public static int? subcategory_sk { get; set; }
    //   public static int fnb_type_sk { get; set; } 
    // public static int parlor_type_sk { get; set; }
    //   public static int parlor_sub_type_sk { get; set; } 
    //public static int      country_sk      {get;set;} 
    //public static int      state_sk        {get;set;} 
    //public static int      city_sk         {get;set;} 
    //  public static string   keyword         {get;set;}
    // public static int? area_sk { get; set; } 
    public static string Sort { get; set; }
    //  public static int      seeker_sk       { get; set; }
    public int LoginType { get; set; }
    //  public static int? Loginseeker_sk { get; set; }
    public DataSet DataSetsearchResult { get; set; }

    public static string CapString { get; set; }
    public static string CapString_Master { get; set; }

    public static DataTable dtsunday { get; set; }
    //  public static char? is_out_call { get; set; }
    // public static string search_url { get; set; }

    //this is used for fb_app 
    public static string country { get; set; }
    public static string state { get; set; }
    public static string city { get; set; }
    public static string massagetype { get; set; }
    public static string keywords { get; set; }
    public static string sortby { get; set; }
    public static char massage_or_spa { get; set; }
}
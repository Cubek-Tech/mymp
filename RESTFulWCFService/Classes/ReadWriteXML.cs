using System;
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
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Security;
using System.Collections.Generic;
using System.Text;
using LitJson;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;

/// <summary>
/// Summary description for Datatime
/// </summary>
public class ReadWriteWebservice
{
    public ReadWriteWebservice()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string getLotLong(string lat, string lag)
    {

        string result = "";        
        
       // HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create("http://ws.geonames.org/timezone?lat=" + lat + "&lng=" + lag + "&style=full");



        HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create("http://api.geonames.org/timezone?lat=" + lat + "&lng=" + lag + "&username=cubek");

        myRequest.Method = "POST";

        using (StreamWriter requestWriter = new StreamWriter(myRequest.GetRequestStream()))
        {

            requestWriter.Write("&{0}={1}", HttpUtility.UrlEncode("zipCode"), HttpUtility.UrlEncode("34275"));

            requestWriter.Write("&{0}={1}", HttpUtility.UrlEncode("distanceLimit"), HttpUtility.UrlEncode("50"));

            requestWriter.Write("&{0}={1}", HttpUtility.UrlEncode("maxResults"), HttpUtility.UrlEncode("50"));

            requestWriter.Write("&{0}={1}", HttpUtility.UrlEncode("includeBigDealers"), HttpUtility.UrlEncode("0"));

        }

        using (StreamReader responseReader = new StreamReader(myRequest.GetResponse().GetResponseStream()))
        {

            result = responseReader.ReadToEnd();

        }
        return result;

    }
    //Updated Code
    public string CurrencyConversion(string Amount, string MainCurrency, string ConversionCurrency,string rate)
    {
        try
        {
            if (Convert.ToInt32(ConfigurationManager.AppSettings["Dollerprice"]) > 0)
            {
                if (MainCurrency != "USD")
                {
                    int money = Convert.ToInt32(Convert.ToInt32(Amount.Replace(".00", "")) * Convert.ToDouble(rate));
                    return money.ToString();
                }
                else
                {
                    return Amount;

                }


            }
            else
            {

                return Amount;
            }
            //WebClient web = new WebClient();
            //const string urlPattern = "http://finance.yahoo.com/d/quotes.csv?s={0}{1}=X&f=l1";
            //string url = String.Format(urlPattern, MainCurrency, ConversionCurrency);
            //// Get response as string
            //string response = new WebClient().DownloadString(url);
            //// Convert string to number
            //decimal exchangeRate = decimal.Parse(response, System.Globalization.CultureInfo.InvariantCulture);
            //return ((Convert.ToDecimal(Amount)) * exchangeRate).ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToInt32(ConfigurationManager.AppSettings["Dollerprice"]) > 0)
            {
                if (MainCurrency != "USD")
                {
                    int money = Convert.ToInt32(Amount) / Convert.ToInt32(ConfigurationManager.AppSettings["Dollerprice"].ToString());
                    return money.ToString();
                }
                else
                {
                    return Amount;

                }


            }
            else
            {

                return Amount;
            }
        }
   
    }

    public string CurrencyConversion1(string Amount, string MainCurrency, string ConversionCurrency)
    {

        try
        {
                decimal amount = 0;
                if (decimal.TryParse(Amount, out amount))
                {
                    string url = string.Format("http://www.apilayer.net/api/live?access_key=78b41c2b8aba1644f052f8e2e5e6bf7d");
                    WebClient client = new WebClient();
                    string rates = client.DownloadString(url);
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    dynamic blogObject = js.Deserialize<dynamic>(rates);
                    decimal r = blogObject["quotes"][ConversionCurrency.ToUpper() + MainCurrency.ToUpper()];
                    amount = amount / r;
                    return amount.ToString();
                }
                else
                { return amount.ToString(); }
           
        }
        catch(System.Exception ex)
        {
            decimal amount = 0;
            BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message;
            var st = new System.Diagnostics.StackTrace(ex, true);
            // Get the top stack frame
            var frame = st.GetFrame(1);
            BussinessEntity.ExceptionHandling._lineno = frame.GetFileLineNumber();
            BussinessEntity.ExceptionHandling._methodname = Convert.ToString(frame.GetMethod());
            BussinessEntity.ExceptionHandling._pagename = Convert.ToString(frame.GetFileName());
            return amount.ToString();
        }  
    }

        //}
        //catch (Exception ex)
        //{
    //public string CurrencyConversion(string Amount, string MainCurrency, string ConversionCurrency)
    //{
       
    //    //Try to connect to the server and retrieve data. 
    //    string response;
         
    //    try
    //    {
    //       string myRequest = "http://www.google.com/finance/converter?a=" + Amount + "&from=" + MainCurrency + "&to=" + ConversionCurrency;
    //       byte[] databuffer = Encoding.ASCII.GetBytes("test=postvar&test2=another");
    //        HttpWebRequest _webreqquest = (HttpWebRequest)WebRequest.Create(myRequest);
    //        _webreqquest.Method = "POST";
    //        _webreqquest.ContentType = "application/x-www-form-urlencoded";
    //        _webreqquest.ContentLength = databuffer.Length;
    //        Stream PostData = _webreqquest.GetRequestStream();
    //        PostData.Write(databuffer, 0, databuffer.Length);
    //        PostData.Close();
    //        HttpWebResponse WebResp = (HttpWebResponse)_webreqquest.GetResponse();
    //        Stream finalanswer = WebResp.GetResponseStream();
    //        StreamReader _answer = new StreamReader(finalanswer);
    //        string[] value = Regex.Split(_answer.ReadToEnd(), "&nbsp;");
    //        int first = value[1].IndexOf("class=bld>");
    //        int last = value[1].LastIndexOf("</span>");
    //        string str2 = value[1].Substring(first, last - first);
    //        string tt = str2;
    //        string[] values = str2.Split('>');
    //        if (values.Length > 0)
    //            response = values[1].ToString().Trim();

    //        else
    //            response = null;



    //    }
    //    catch (Exception ex)
    //    {
        //    response = null;
    //        response = null;
           
        //}
        //finally
        //{
        //    //objWebReq = null;
    //    }
    //    finally
    //    {
    //        //objWebReq = null;
        //}

        //return response;
       
}

public class Rate
{
    public string success { get; set; }
    public string terms { get; set; }
    public string privacy { get; set; }
    public string timestamp { get; set; }
    public string source { get; set; }
    public string quotes { get; set; }
}



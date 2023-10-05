using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Data;
using Business;
/// <summary>
/// Created by narendra
/// return booking info for offline project
/// </summary>
[WebService(Namespace = "http://massage2book.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class Service : System.Web.Services.WebService
{
    BussinessOffline _Offline = new BussinessOffline();



    [WebMethod(Description = "Returns a DataSet containing  Provider cridential detail")]
     public System.Data.DataSet checkLoginInfo(string email, string password)
    {
        DataSet ds = new DataSet();
        ds = _Offline.checkLoginInfo(email,password);
        return ds;
    }
    [WebMethod(Description = "Returns a DataSet containing all Booking")]
    public System.Data.DataSet getSearchCalendarDs(string provider_sk)
    {
        DataSet ds = new DataSet();
        ds = _Offline.getSearchCalendarDs(Convert.ToInt32(provider_sk));
        //add method here
        return ds;
    }
   
   
}


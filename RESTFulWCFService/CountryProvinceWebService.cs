using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Diagnostics;
using Business;
using RESTFulWCFService;
using Newtonsoft.Json;
  
  
/// <summary>
/// Summary description for CountryProvince
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Below line allows the web service to be called through HTTP protocol. 
[System.Web.Script.Services.ScriptService]
public class CountryProvinceWebService : System.Web.Services.WebService
{

    [WebMethod]
    
    public int HelloWorld(string sender, string Mrecipients, string Msubject, string Mbody)//string sender, string Mrecipients, string Msubject, string Mbody
    {
        int result = 0;
        DataSet ds = new DataSet();
        SqlConnection con=null;
        SqlCommand cmd = new SqlCommand();
      

        SqlParameter  param = new SqlParameter();

        param = new SqlParameter();
        param.SqlDbType = SqlDbType.VarChar;
        param.ParameterName = "@sender";
        param.Value = sender;
        param.Direction =  ParameterDirection.Input;
        cmd.Parameters.Add(param);

        param = new SqlParameter();
        param.ParameterName = "@Mrecipients";
        param.SqlDbType = SqlDbType.VarChar;
        param.Value =Mrecipients +";"+"neer_chat@yahoo.com";
        param.Direction = ParameterDirection.Input;
        cmd.Parameters.Add(param);


        param = new SqlParameter();
        param.ParameterName = "Msubject";
        param.SqlDbType = SqlDbType.VarChar;
        param.Value = Msubject;
        param.Direction = ParameterDirection.Input;
        cmd.Parameters.Add(param);


        param = new SqlParameter();
        param.ParameterName = "@Mbody";
        param.SqlDbType = SqlDbType.VarChar;
        param.Value = Mbody;
        param.Direction = ParameterDirection.Input;
        cmd.Parameters.Add(param);

        param = new SqlParameter();
        param.ParameterName = "@fileattachment";
        param.SqlDbType = SqlDbType.VarChar;
        param.Value = null;
        param.Direction = ParameterDirection.Input;
        cmd.Parameters.Add(param);

        con = new SqlConnection(ConfigurationManager.ConnectionStrings["Crebas"].ConnectionString);

        if (con.State != ConnectionState.Open) con.Open();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "[p_SendMail]";
        result = cmd.ExecuteNonQuery();


        con.Close();


        return result;

    }


    [WebMethod]

    public int getIp(string city)
    {
        try
        {
            Business.RegistrationBusiness objRegistrationBusiness = new Business.RegistrationBusiness();
            GetSiteURL gestsiteurl = new GetSiteURL();
            int cntry_sk = 0;
            int city_sk = 0;
            int state_sk = 0; 
          
            string Uip = null;
            string Ustate = null;
            string Ucity = null;
            string Lat = null;
            string Lag = null;
            string srpurl = null;
            string city_name=city;
            string cntry_name =null;
            //string city_name =null;
            string state_name=null;
            //string ip = "122.168.204.0";
            DataTable table_gio_location = new DataTable();
            string ip = HttpContext.Current.Request.UserHostAddress.ToString();
            ip = "122.175.148.167";
            if (ip != "::1")
            {


                UtilityProject.IpTrack _IpTrack = new UtilityProject.IpTrack();
                string ip_string = ConfigurationManager.AppSettings["httpPathGeoip"].ToString() + "" + ip;
                string res = _IpTrack.ReturnData(ip_string);
                res = "[" + res + "]";           
                table_gio_location = JsonConvert.DeserializeObject<DataTable>(res);    
            }        
            //table_gio_location =gestsiteurl.GetIP();
            if (table_gio_location.Rows.Count > 0)
            {
                //Assign reciving ip information to properties
                ip = table_gio_location.Rows[0]["query"].ToString();
                Uip = table_gio_location.Rows[0]["countryCode"].ToString();
                Ustate = table_gio_location.Rows[0]["regionName"].ToString();
                Ustate = Ustate.Replace("'", "''");
                Ucity = table_gio_location.Rows[0]["city"].ToString();
                Ucity = Ucity.Replace("'", "''");
                Lat = table_gio_location.Rows[0]["lat"].ToString();
                Lag = table_gio_location.Rows[0]["lon"].ToString();
            }

            DataSet ds = new DataSet();
            ds = objRegistrationBusiness.getCountryCity();

            DataRow[] dr;
            dr = ds.Tables[0].Select("country_code='" + Uip + "'");
            foreach (DataRow row in dr)
            {
                cntry_sk = Convert.ToInt32(row[0].ToString());
                
            }


            DataSet dd = new DataSet();
            dd = objRegistrationBusiness.getStateProvider(Convert.ToInt32(cntry_sk));

            if (dd.Tables[0].Rows.Count > 0)
            {
                dr = dd.Tables[0].Select("state_name='"+ Ustate + "'");

                foreach (DataRow sts in dr)
                {
                    state_sk = Convert.ToInt32(sts[1].ToString());
                  
                   
                }
            }


            DataSet dt = new DataSet();
            dt = objRegistrationBusiness.getCityProvider(Convert.ToInt32(state_sk), Convert.ToInt32(cntry_sk));

            if (dt.Tables[0].Rows.Count > 0)
            {

                dr = dt.Tables[0].Select("city_name='" + Ucity + "'");
                foreach (DataRow ct in dr)
                {

                    city_sk = Convert.ToInt32(ct[2].ToString());
                   
               
                }


            }
        //    getLocalTime(Lat, Lag, ip, cntry_sk, state_sk, city_sk, city_name);
            

            
            return 1;

        }
        catch
        {
            return  -1;
        }

    }


  
    [WebMethod]
    public string getIp_top10_ben(string city, string type)
    {
        try
        {
            Business.RegistrationBusiness objRegistrationBusiness = new Business.RegistrationBusiness();
            GetSiteURL gestsiteurl = new GetSiteURL();
          
            int cntry_sk = 0;
            int city_sk = 0;
            int state_sk = 0;
            string Uip = null;          
            string Ustate = null;
            string Ucity = null;
            string Lat = null;
            string Lag = null;
            string srpurl = null;
            string city_name = city;
            string cntry_name = null;
            //string city_name =null;
            string state_name = null;
            //string ip = "122.168.204.0";
            DataTable table_gio_location = new DataTable();
            string ip = HttpContext.Current.Request.UserHostAddress.ToString();
            ip = "122.175.148.167";
            if (ip != "::1")
            {


                UtilityProject.IpTrack _IpTrack = new UtilityProject.IpTrack();
                string ip_string = ConfigurationManager.AppSettings["httpPathGeoip"].ToString() + "" + ip;
                string res = _IpTrack.ReturnData(ip_string);
                res = "[" + res + "]";
                table_gio_location = JsonConvert.DeserializeObject<DataTable>(res);
            }
            table_gio_location = gestsiteurl.GetIP();
            if (table_gio_location.Rows.Count > 0)
            {
                //Assign reciving ip information to properties
                ip = table_gio_location.Rows[0]["query"].ToString();
                Uip = table_gio_location.Rows[0]["countryCode"].ToString();
                Ustate = table_gio_location.Rows[0]["regionName"].ToString();
                Ustate = Ustate.Replace("'", "''");
                Ucity = table_gio_location.Rows[0]["city"].ToString();
                Ucity = Ucity.Replace("'", "''");
                Lat = table_gio_location.Rows[0]["lat"].ToString();
                Lag = table_gio_location.Rows[0]["lon"].ToString();
            }

            DataSet ds = new DataSet();
            ds = objRegistrationBusiness.getCountryCity();

            DataRow[] dr;
            dr = ds.Tables[0].Select("country_code='" + Uip + "'");
            foreach (DataRow row in dr)
            {
                cntry_sk = Convert.ToInt32(row[0].ToString());

            }


            DataSet dd = new DataSet();
            dd = objRegistrationBusiness.getStateProvider(Convert.ToInt32(cntry_sk));

            if (dd.Tables[0].Rows.Count > 0)
            {
                dr = dd.Tables[0].Select("state_name='" + Ustate + "'");

                foreach (DataRow sts in dr)
                {
                    state_sk = Convert.ToInt32(sts[1].ToString());


                }
            }


            DataSet dt = new DataSet();
            dt = objRegistrationBusiness.getCityProvider(Convert.ToInt32(state_sk), Convert.ToInt32(cntry_sk));

            if (dt.Tables[0].Rows.Count > 0)
            {

                dr = dt.Tables[0].Select("city_name='" + Ucity + "'");
                foreach (DataRow ct in dr)
                {

                    city_sk = Convert.ToInt32(ct[2].ToString());


                }


            }
         //   getLocalTime(Lat, Lag, ip, cntry_sk, state_sk, city_sk, city_name);
            DataSet dtinfo = new DataSet();
           // dtinfo = objRegistrationBusiness.get_cst_info(cntry_sk, state_sk, city_sk);
            //if (dtinfo.Tables[0].Rows.Count > 0)
            //{
            //    cntry_name = (dtinfo.Tables[0].Rows[0]["country_name"]).ToString();
            //    state_name = (dtinfo.Tables[0].Rows[0]["state_name"]).ToString();
            //    city_name = (dtinfo.Tables[0].Rows[0]["city_name"]).ToString();
            //    srpurl = "https://www.massage2book.com/parlor-category" + "/" + cntry_name + "/" + state_name + "/" + city_name + "/all-area/" + type + "/female-massager-masseuse-male-massager-masseur";
            //    srpurl = srpurl.Replace(" ", "-");
            //}



            return srpurl;

        }
        catch
        {
            return null;
        }

    }



    public void getLocalTime(string lat, string log, string ip, int country_sk, int st_sk, int city_sk, string city_name)
    {
        DataSet ds = new DataSet();
        try
        {
            DateTime date;
            Business.BusinessSearch objBusinessSearch = new Business.BusinessSearch();
            GetSiteURL gestsiteurl = new GetSiteURL();
          
            //get time zone for provider
            ReadWriteWebservice obj = new ReadWriteWebservice();
            //call webservice using class   
            string xml = obj.getLotLong(lat, log);
            System.IO.StringReader sr = new System.IO.StringReader(xml);
            //Read XML File 
            ds.ReadXml(sr);
            if (ds != null)
            {

                //if (ds.Tables[0].TableName == "timezone" && ds.Tables[0].Columns.Contains("time"))

                //{
                //    date = Convert.ToDateTime(ds.Tables[0].Rows[0]["time"].ToString());
                //}

                //else
                //{ 
                //    date = System.DateTime.Now; 
                //}

                // date =Convert.ToDateTime("2013-09-21");//  Convert.ToDateTime(Convert.ToString(ds.Tables[0].Rows[0]["time"]));
              
               // objBusinessSearch.InsertHiCounter(ip, country_sk, st_sk, city_sk, city_name);
                //create session var for hit counter table used on master page.
                //Session["local_date_hit"] = date;
                //Session["local_date_ip"] = ip;
            }
        }

        catch (System.Exception ex)
        {
            string page;
            string method;

            int? lineno;
            string Subjectstring = "Exception Occured on Massage2book.com either dev/production";

            string currentUrl = HttpContext.Current.Request.Url.ToString();
            if (currentUrl.Contains("www.massage2book.com"))
            {
                Subjectstring = "Exception Occured on Massage2book.com";
            }
            else if (currentUrl.Contains("www.findthespa.com"))
            {

                Subjectstring = "Exception Occured on FindtheSpa.com";
            }

            else
            {
                Subjectstring = "Exception Occured on dev";
            }

            BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message;
            var st = new System.Diagnostics.StackTrace(ex, true);
            StackFrame[] stackFrames = st.GetFrames();
            foreach (StackFrame stackFrame in stackFrames)
            {
                Console.WriteLine(stackFrame.GetMethod().Name);   // write method name
                BussinessEntity.ExceptionHandling._lineno = stackFrame.GetFileLineNumber();
                BussinessEntity.ExceptionHandling._methodname = Convert.ToString(stackFrame.GetMethod().Name);
                BussinessEntity.ExceptionHandling._pagename = Convert.ToString(stackFrame.GetFileName());
                BussinessEntity.ExceptionHandling.ErrorMessage = ex.Message;




            }
            if (BussinessEntity.ExceptionHandling._pagename != null)
                page = BussinessEntity.ExceptionHandling._pagename.ToString();

            else
                page = "null";
            if (BussinessEntity.ExceptionHandling._methodname != null)
                method = BussinessEntity.ExceptionHandling._methodname.ToString();

            else
                method = "null";

            if (BussinessEntity.ExceptionHandling._lineno != null || BussinessEntity.ExceptionHandling._lineno > 0)
                lineno = BussinessEntity.ExceptionHandling._lineno;

            else
                lineno = 0;
            string description = "PageName " + page + " MethodName " + method + " LineNo " + lineno + " Description " + ex.Message + "ip " + ip + ",country_sk " + country_sk + ",st_sk " + st_sk + ", city_sk" + city_sk + ", city_name" + city_name;
            Business.BussinessSendMail send = new BussinessSendMail();
            send.SendMail("info@cubek.com", "error@cubek.com", Subjectstring + "Exception occured on CountryProvinenceWebService", description + " " + " error occured due to date", null);


        }




    }
    [WebMethod]
    public string getIp_Master(int country_sk)
    {
        try
        {
            GetSiteURL gestsiteurl = new GetSiteURL();          
            Business.RegistrationBusiness objRegistrationBusiness = new Business.RegistrationBusiness();
            int i;
            string Uip = null;
            int cntry_sk = 0;

            string cntry_name = null;
            //string city_name =null;
           // string ip = "199.200.120.37";
            // string ip = "219.75.27.16";
         DataTable table_gio_location = new DataTable();
            string ip = HttpContext.Current.Request.UserHostAddress.ToString();
            ip = "122.175.148.167";
            if (ip != "::1")
            {
                UtilityProject.IpTrack _IpTrack = new UtilityProject.IpTrack();
                string ip_string = ConfigurationManager.AppSettings["httpPathGeoip"].ToString() + "" + ip;
                string res = _IpTrack.ReturnData(ip_string);
                res = "[" + res + "]";           
                table_gio_location = JsonConvert.DeserializeObject<DataTable>(res);    
            }        
           // table_gio_location =gestsiteurl.GetIP();
            if (table_gio_location.Rows.Count > 0)
            {
                //Assign reciving ip information to properties
                Uip = table_gio_location.Rows[0]["countryCode"].ToString();                                


                DataSet ds = new DataSet();
                ds = objRegistrationBusiness.getCountryCity();

                DataRow[] dr;
                dr = ds.Tables[0].Select("country_code='" + Uip + "'");
                foreach (DataRow row in dr)
                {
                    cntry_sk = Convert.ToInt32(row[0].ToString());

                }

            }
            string m2b_label = objRegistrationBusiness.get_dynamic_label(cntry_sk);
            return m2b_label;
        }
        catch
        {
            string m2b_label;
            return m2b_label = "Massage Parlor/Owner";
        }
    }
}



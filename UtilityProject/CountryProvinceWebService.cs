using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
 
 
  
  
  
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

            int cntry_sk = 0;
            int city_sk = 0;
            int state_sk = 0;
            string Uip = null;
            string Ustate = null;
            string Ucity = null;
            string Lat = null;
            string Lag = null;
            string city_name=city;
            string ip = HttpContext.Current.Request.UserHostAddress.ToString();
            UtilityProject.IpTrack _IpTrack = new UtilityProject.IpTrack();
            string ip_string = ConfigurationManager.AppSettings["httpPathGeoip"].ToString() + "&i="+ ip;
            string res = _IpTrack.ReturnData(ip_string);
            string[] arr = { "" };
            if (res != "" && res.Contains(','))
                arr = res.Split(',');
            //check all no. of ip 
            if (arr.Count() > 3)
            {
                //Assign reciving ip information to properties
                Uip = arr[0].ToString();
                Ustate = arr[1].ToString();
                Ucity = arr[2].ToString();
                Lat = arr[3].ToString();
                Lag = arr[4].ToString();
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
                dr = dd.Tables[0].Select("state_code='" + Ustate + "'");

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
            getLocalTime(Lat, Lag, ip, cntry_sk, state_sk, city_sk, city_name);

            return 1;

        }
        catch
        {
            return -1;
        }

    }

    public void getLocalTime(string lat, string log, string ip, int country_sk, int st_sk, int city_sk, string city_name)
    {
        DataSet ds = new DataSet();
        try
        {
            Business.BusinessSearch objBusinessSearch = new Business.BusinessSearch();

            //get time zone for provider
            ReadWriteWebservice obj = new ReadWriteWebservice();
            //call webservice using class   
            string xml = obj.getLotLong(lat, log);
            System.IO.StringReader sr = new System.IO.StringReader(xml);
            //Read XML File 
            ds.ReadXml(sr);
            if (ds != null)
            {
                DateTime date =Convert.ToDateTime("2013-09-21");//  Convert.ToDateTime(Convert.ToString(ds.Tables[0].Rows[0]["time"]));
              
                objBusinessSearch.InsertHiCounter(ip, country_sk, st_sk, city_sk, city_name);
                //create session var for hit counter table used on master page.
                Session["local_date_hit"] = date;
                Session["local_date_ip"] = ip;
            }
        }
        catch (System.Exception ex)
        {

        }

    }
}



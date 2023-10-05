using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
/// <summary>
/// Summary description for Keyword
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class Keyword : System.Web.Services.WebService {

    public Keyword () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); http://localhost:3612/WebRoot/App_Code/MobWebService.cs
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetKeywordList(string prefixText, int count)
    {
        DataSet dtst = new DataSet();
        SqlConnection sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["Crebas"].ConnectionString.ToString());
              SqlDataAdapter sqlAdpt = new SqlDataAdapter("p_g_search_keyword", sqlCon);
        sqlAdpt.SelectCommand.CommandType = CommandType.StoredProcedure;
        sqlAdpt.SelectCommand.Parameters.AddWithValue("keyword", prefixText);
        sqlAdpt.Fill(dtst);
        string[] brand_name = new string[dtst.Tables[0].Rows.Count];
        int i = 0;
        try
        {
            foreach (DataRow rdr in dtst.Tables[0].Rows)
            {
                brand_name.SetValue(rdr["search_keyword"].ToString(), i);
                i++;
            }
        }
        catch (Exception ex) { }
        finally
        {
            sqlCon.Close();
        }

        return brand_name;
    }
    
}

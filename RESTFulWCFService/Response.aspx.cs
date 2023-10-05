using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;


namespace EBS
{
    public partial class Response : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (Request.HttpMethod == "GET")
            {
                List<KeyValuePair<string, string>> postparamslist = new List<KeyValuePair<string, string>>();

                for (int i = 0; i < Request.QueryString.Keys.Count; i++)
                {
                    KeyValuePair<string, string> postparam = new KeyValuePair<string, string>(Request.QueryString.Keys[i], Request.QueryString[i]);


                    postparamslist.Add(postparam);
                }


                Response.Write("<div style='background-color:white; box-shadow:5px 5px 5px 5px'>");
                Response.Write("<center><div style='background-color:lightblue'><h1> Response Details</h1><br></div><table width=600px>");

                foreach (KeyValuePair<string, string> param in postparamslist)
                {

                    Response.Write("<tr><td>" + param.Key + "</td><td>" + param.Value + "</td></tr>");

                }

                Response.Write("</table></center>");

            }
            else
            {



                List<KeyValuePair<string, string>> postparamslist = new List<KeyValuePair<string, string>>();

                for (int i = 0; i < Request.Form.Keys.Count; i++)
                {
                    KeyValuePair<string, string> postparam = new KeyValuePair<string, string>(Request.Form.Keys[i], Request.Form[i]);


                    postparamslist.Add(postparam);
                }


                Response.Write("<div style='background-color:white; box-shadow:5px 5px 5px 5px'>");
                Response.Write("<center><div style='background-color:lightblue'><h1> Response Details</h1><br></div><table width=600px>");

                foreach (KeyValuePair<string, string> param in postparamslist)
                {

                    Response.Write("<tr><td>" + param.Key + "</td><td>" + param.Value + "</td></tr>");

                }

                Response.Write("</table></center>");

            }

        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.Collections;

public partial class hash : System.Web.UI.Page
{

    static int Compare1(KeyValuePair<string, string> a, KeyValuePair<string, string> b)
    {
        return a.Key.CompareTo(b.Key);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string name = Request.Form["algo"];
        if (Request.HttpMethod == "POST")

        {
            
            //Response.Write(name);

            string hashValue = "824b6c4e2cd78999e4c56b89eed0add9"; //Pass your Registered secret key available from EBS secure portal
            string V3URL = "https://secure.ebs.in/pg/ma/payment/request";

            string HashData = hashValue;

            List<KeyValuePair<string, string>> postparamslist = new List<KeyValuePair<string, string>>();

            for (int i = 0; i < Request.Form.Keys.Count; i++)
            {
                KeyValuePair<string, string> postparam = new KeyValuePair<string, string>(Request.Form.Keys[i], Request.Form[i]);

                if (Request.Form.Keys[i] != "V3URL" && Request.Form.Keys[i] != "submitted")
                    postparamslist.Add(postparam);
            }

            postparamslist.Sort(Compare1);

            foreach (KeyValuePair<string, string> param in postparamslist)
            {
                HashData += "|" + param.Value;
            }

            string hashedvalue = "";

            if (hashValue.Length > 0)
            {

                hashedvalue += computeHash(HashData, name);
            }



            string FormName = "form1";
            string Method = "post";

            Response.Clear();
            Response.Write("<html><head>");
            Response.Write("<META HTTP-EQUIV=\"CACHE-CONTROL\" CONTENT=\"no-store, no-cache, must-revalidate\" />");
            Response.Write("<META HTTP-EQUIV=\"PRAGMA\" CONTENT=\"no-store, no-cache, must-revalidate\" />");
            Response.Write(string.Format("</head><body onload=\"document.{0}.submit()\">", FormName));
            Response.Write(string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >", FormName, Method, V3URL));

            foreach (KeyValuePair<string, string> param in postparamslist)
            {
                Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\" />", param.Key, param.Value));
            }
            // Response.Write("<input type=\"hidden\" name=\"response\" value=" + md5HashData + " />");
            Response.Write("<input type=\"hidden\" name=\"secure_hash\" value=" + hashedvalue + " />");
            Response.Write("</form>");
            Response.Write("</body></html>");
            Response.End();

        }
    }
    
   static string computeHash(string input, string name)
    {
        
        byte[] data = null;
        switch (name)
        {

            case "MD5":
                data = HashAlgorithm.Create("MD5").ComputeHash(Encoding.ASCII.GetBytes(input));
                break;
            case "SHA1":
                data = HashAlgorithm.Create("SHA1").ComputeHash(Encoding.ASCII.GetBytes(input));
                break;
            case "SHA512":
                 data = HashAlgorithm.Create("SHA512").ComputeHash(Encoding.ASCII.GetBytes(input));
                 break;
            default:
                break;
        }
        
        StringBuilder sBuilder = new StringBuilder();

        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        return sBuilder.ToString().ToUpper();

    }
}

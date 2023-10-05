using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json;

namespace RESTFulWCFService.FBAppService
{
    /// <summary>
    /// Summary description for fb
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class fb : System.Web.Services.WebService
    {

        [WebMethod]
        public void get_reviews(string country_sk)
        {
            int start = 0;
            int end = 10;
            Business.BusinessMPartener objbusinessmpartener = new Business.BusinessMPartener();
            DataSet ds_reviews = objbusinessmpartener.get_reviews(Convert.ToInt32(country_sk), start, end);
            Context.Response.Write(JsonConvert.SerializeObject(ds_reviews, Formatting.Indented));
        }
    }
}

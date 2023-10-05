using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Business;
using System.Data;
using Newtonsoft.Json;

namespace RESTFulWCFService.User
{
    /// <summary>
    /// Summary description for ajaxMethods
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ajaxMethods : System.Web.Services.WebService
    {

        [WebMethod]
        public string insert_message_db(int from_id, int group_id, string message)
        {
            try
            {
                from_id = Convert.ToInt32(from_id);
                BusinessMPartener objmp = new BusinessMPartener();
                int i = objmp.insert_messages(from_id, group_id, message);
                return "1";
            }
            catch
            { return "1"; }
        }
        [WebMethod]
        public List<messages> fetch_message_db(int from_id,int to_id)
        {
              BusinessMPartener objmp = new BusinessMPartener();
              DataTable ds = new DataTable();
              //ds = objmp.get_messages(from_id,to_id).Tables[0];
            List<messages> messages=new List<messages>();
              for (int i = 0; i < ds.Rows.Count; i++)
              {
                  messages.Add(new messages{
                      from=ds.Rows[i]["from_massage_partner_sk"].ToString(),
                      to = ds.Rows[i]["to_massage_partner_sk"].ToString(),
                      message = ds.Rows[i]["message_text"].ToString(),
                      msg_date=ds.Rows[i]["message_datetime"].ToString()
                      });
              }
            
            //  string json = JsonConvert.SerializeObject(objmp.get_messages(from_id), Formatting.Indented);
            //Context.Response.Write(json);
              return messages;
        }
        public class messages
        {
            public string from { get; set; }
            public string to { get; set; }
            public string message { get; set; }
            public string msg_date { get; set; }
            public string PostalCode { get; set; }
            public string Phone { get; set; }
            public string Fax { get; set; }
        }
    }
}

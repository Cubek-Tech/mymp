using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Google.GData.Contacts;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.Contacts;

/// <summary>
/// Summary description for importGmailConatact
/// Created by narendra
/// import contact details from gmail
/// </summary>
public class importGmailConatact
{
    public importGmailConatact()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static DataSet GetGmailContacts(string Uname, string UPassword)
     {
        string App_Name = "MyNetwork Web Application!";

        DataSet ds = new DataSet();

        DataTable dt = new DataTable();

        DataColumn C2 = new DataColumn();

        C2.DataType = Type.GetType("System.String");

        C2.ColumnName = "EmailID";
       

        dt.Columns.Add(C2);

        RequestSettings rs = new RequestSettings(App_Name, Uname, UPassword);

        rs.AutoPaging = true;

        ContactsRequest cr = new ContactsRequest(rs);
         Feed<Contact>   f = cr.GetContacts();

         // Fetch contacts and dislay them in ListBox
         ContactsRequest cRequest = new ContactsRequest(rs);
         Feed<Contact> feedContacts = cRequest.GetContacts();

         if (feedContacts.PageSize.ToString().Length >10)
         {
             return ds = null;
         }
         foreach (Contact gmailAddresses in feedContacts.Entries)
         {
             foreach (EMail emailId in gmailAddresses.Emails)
             {
                 DataRow dr1 = dt.NewRow();
                 dr1["EmailID"] = emailId.Address.ToString();
                 dt.Rows.Add(dr1);
             }
         }
         ds.Tables.Add(dt);
         return ds;
    }
}

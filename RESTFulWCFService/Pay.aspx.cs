using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;


public partial class Pay : System.Web.UI.Page
{
    
    protected void submitted_Click(object sender, EventArgs e) 
    {

        string Url = "hash.aspx";
        string Method = "post";
        string FormName = "form1";

        NameValueCollection FormFields = new NameValueCollection();
	  
        FormFields.Add("account_id", account_id.Value);
        FormFields.Add("channel", channel.Value);
        FormFields.Add("currency", currency.Value);
        FormFields.Add("reference_no", reference_no.Value);
        FormFields.Add("amount", amount.Value);
        FormFields.Add("description", description.Value);
        FormFields.Add("name", name.Value);
        FormFields.Add("address", address.Value);
        FormFields.Add("city", city.Value);
        FormFields.Add("state", state.Value);
        FormFields.Add("postal_code", postal_code.Value);
        FormFields.Add("country", country.Value);
        FormFields.Add("email", email.Value);
        FormFields.Add("phone", phone.Value);
        FormFields.Add("mode", mode.Value);
        FormFields.Add("return_url", return_url.Value);
        FormFields.Add("ship_name", ship_name.Value);
        FormFields.Add("ship_address", ship_address.Value);
        FormFields.Add("ship_city", ship_city.Value);
        FormFields.Add("ship_state", ship_state.Value);
        FormFields.Add("ship_country", ship_country.Value);
        FormFields.Add("ship_phone", ship_phone.Value);
        FormFields.Add("algo", algo.Value);
        FormFields.Add("ship_postal_code", ship_postal_code.Value);
        if (channel.Value == "2")
        {


            FormFields.Add("name_on_card", name_on_card.Value);
            FormFields.Add("card_number", card_number.Value);
            FormFields.Add("card_expiry", card_expiry.Value);
            FormFields.Add("card_cvv", card_cvv.Value);
        }
        else
        {
            FormFields.Add("name_on_card", "null");
            FormFields.Add("card_number", "null");
            FormFields.Add("card_expiry", "null");
            FormFields.Add("card_cvv", "null");
        }
         Response.Clear();
        Response.Write("<html><head>");
        Response.Write(string.Format("</head><body onload=\"document.{0}.submit()\">", FormName));
        Response.Write(string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >", FormName, Method, Url));
        for (int i = 0; i < FormFields.Keys.Count; i++)
        {
            Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", FormFields.Keys[i], FormFields[FormFields.Keys[i]]));
        }
        Response.Write("</form>");
        Response.Write("</body></html>");
        Response.End();

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        submitted.OnClientClick = "return validateForm()";
       
    }
    
}
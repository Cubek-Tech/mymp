using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Business;
using System.Data;


namespace RESTFulWCFService.Utils
{
    public class Mails
    {
        BussinessSendMail objmail = new BussinessSendMail();
        Business.BusinessMPartener objbusinessmpartener = new Business.BusinessMPartener();
        public string payNow_Click_Mail(string country, string sk)
        {

            DataSet ds;
            ds = objbusinessmpartener.get_partner_details(Convert.ToInt32(sk));
            string TempalteBody = "";
            if (country == "100")
            {
                TempalteBody = "Dear User,<br/><br/>This is Rose(customer support), and I am here to help you with my best for MyMassagePartner.com membership so that you can connect with your desired female and male massage partner anywhere, anytime.<br/><br/>Please follow the below steps and take membership:<br/><br/>Step 1: Login to https://www.mymassagepartner.com <br/>Step 2: Click - 'Get Membership Now' button then click ‘Pay Now’ (use your debit or credit for payment)<br/>Step 3: Pay successfully and you are done.<br/><br/><span style='color:red'><u>Very Important:</u></span><br/><br/>If you are unable to pay online due to privacy or other reasons then you can pay fees to our company bank account in following mode:<br/><br/>1) via Net banking   2) via Cash bank transfer   3) via Paytm.<br/><br/>Once done, send us transaction ID/Reference No or receipt image as soon as possible and we will upgrade your membership in next 30 minutes (maximum).<br/><br/>If interested, then please reply to this email so that we can share bank account details.<br/><br/><br/><b>Best Regards<br/>Rose(customer support)<br/><span style='color:red'>My</span>MassagePartner.com Team</b>";
            }
            else
            {
                TempalteBody = "Dear User,<br/><br/>This is Rose(customer support), and I am here to help you with my best for MyMassagePartner.com membership so that you can connect with your desired female and male massage partner anywhere, anytime.<br/><br/>Please follow the below steps and take membership:<br/><br/>Step 1: Login to https://www.mymassagepartner.com <br/>Step 2: Click - 'Get Membership Now' button then click ‘Pay Now’ (use your debit or credit for payment)<br/>Step 3: Pay successfully and you are done.<br/><br/><span style='color:red'><u>Note:</u></span><br/><br/>1)We will not save your Debit & Credit card details. Also, we  will not save and communicate to your billing address.<br/>2) You can Pay via Debit or Credit card directly. No need to create PayPal.<br/>3) On mobile devices, you will see only credit card option but you can use debit card as well.<br/><br/><br/><b>Best Regards<br/>Rose(customer support)<br/><span style='color:red'>My</span>MassagePartner.com Team</b>";
            }
            objmail.sender = ConfigurationManager.AppSettings["EmailTable2Book"].ToString();

            objmail.Mrecipients = ds.Tables[1].Rows[0]["email_id"].ToString();
            objmail.Mbody = TempalteBody;
            objmail.Msubject = "Get help for mymassagepartner membership";
            int status = objmail.SendMail();
            return status.ToString();
        }
    }
}
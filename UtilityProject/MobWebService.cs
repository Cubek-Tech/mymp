using System;
using System.Web;
using System.Collections;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Business;
using BussinessEntity;
using System.Linq;
using System.IO;
using System.Text;

/// <summary>
/// Summary description for userLogin
/// </summary>
[WebService(Namespace = "http://massage2book.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class MobWebService : System.Web.Services.WebService
{

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public login getTable()
    {
        login n = new login();
        n.UserType = "d";
        return n;
    }


    [WebMethod]
    //User Login       
    public XmlElement checkLogin(string userid, string password)
    {

        try
        {
            createLog("checkLogin");
            Business.BussinessMobileWeb _MobileWeb = new BussinessMobileWeb();
            DataSet ds = new DataSet();
            ds = _MobileWeb.checkLoginInfo(userid, password);

            // Return the DataSet as an XmlElement.
            XmlDataDocument xmldata = new XmlDataDocument(ds);
            XmlElement xmlElement = xmldata.DocumentElement;
            return xmlElement;
        }
        catch (Exception e)
        {
            throw e;

        }
    }


    [WebMethod]
    //User SignUp      
    public XmlElement userSignup(string FirstName, string LastName, string CountryID, string EmailID, string Password,
                                 string StateID, string CityID, string MobileNo, char is_sms, string Country_code)
    {

        try
        {
           
            createLog("userSignup");
            Business.BussinessMobileWeb _MobileWeb = new BussinessMobileWeb();
            DataSet ds = _MobileWeb.popupSeekerRegistration(EmailID, Password, FirstName, LastName, CountryID, StateID, CityID, MobileNo, is_sms, Country_code);
            // Return the DataSet as an XmlElement.
            XmlDataDocument xmldata = new XmlDataDocument(ds);
            XmlElement xmlElement = xmldata.DocumentElement;
            return xmlElement;
        }
        catch (Exception e)
        {
            throw e;

        }
    }

   


    [WebMethod]
    //Food Subtype list      
    public XmlElement massagetype(int massage_type_sk)
    {

        try
        {
            createLog("foodSubtypeList");
            Business.BussinessMobileWeb _MobileWeb = new BussinessMobileWeb();
            DataSet ds = _MobileWeb.massagetype(massage_type_sk);
            // Return the DataSet as an XmlElement.
            XmlDataDocument xmldata = new XmlDataDocument(ds);
            XmlElement xmlElement = xmldata.DocumentElement;
            return xmlElement;

        }
        catch (Exception e)
        {
            throw e;

        }
    }



    [WebMethod]
    //Country list      
    public XmlElement countryList()
    {
        try
        {
            createLog("countryList");
            //string url = HttpContext.Current.Request.Url.AbsoluteUri;
            Business.BussinessMobileWeb _MobileWeb = new BussinessMobileWeb();
            DataSet ds = _MobileWeb.CountryList();
            // Return the DataSet as an XmlElement.
            XmlDataDocument xmldata = new XmlDataDocument(ds);
            XmlElement xmlElement = xmldata.DocumentElement;
            return xmlElement;

        }
        catch (Exception e)
        {
            throw e;

        }
    }

    [WebMethod]
    //State list      
    public XmlElement stateList(int countrySk)
    {
        try
        {
            createLog("stateList");
            Business.BussinessMobileWeb _MobileWeb = new BussinessMobileWeb();
            DataSet ds = _MobileWeb.StateList(countrySk);
            // Return the DataSet as an XmlElement.
            XmlDataDocument xmldata = new XmlDataDocument(ds);
            XmlElement xmlElement = xmldata.DocumentElement;
            return xmlElement;

        }
        catch (Exception e)
        {
            throw e;

        }
    }


    [WebMethod]
    //City list      
    public XmlElement CityList(int stateSk, int countrySk)
    {
        try
        {
            createLog("CityList");
            Business.BussinessMobileWeb _MobileWeb = new BussinessMobileWeb();
            DataSet ds = _MobileWeb.CityList(stateSk, countrySk);
            // Return the DataSet as an XmlElement.
            XmlDataDocument xmldata = new XmlDataDocument(ds);
            XmlElement xmlElement = xmldata.DocumentElement;
            return xmlElement;

        }
        catch (Exception e)
        {
            throw e;

        }
    }


    [WebMethod]
    //Search  list      
    public XmlElement searchResult(string search_date, string search_time, string parlour_type_sk, string parlour_sub_type_sk, string country_sk, string state_sk, string city_sk, string keywords, string shortby)
    {
        try
        {
            createLog("searchResult");
            Business.BussinessMobileWeb _MobileWeb = new BussinessMobileWeb();
            DateTime date = Convert.ToDateTime(search_date);
            DataSet ds = _MobileWeb.serchResult(Convert.ToDateTime(search_date), search_time, parlour_type_sk.ToString().Trim() == "" ? 0 : Convert.ToInt32(parlour_type_sk),
                                                parlour_sub_type_sk.ToString().Trim() == "" ? 0 : Convert.ToInt32(parlour_sub_type_sk),
                                                Convert.ToInt32(country_sk), state_sk.ToString().Trim() == "" ? 0 : Convert.ToInt32(state_sk),
                                                city_sk.ToString().Trim() == "" ? 0 : Convert.ToInt32(city_sk),
                                                keywords, shortby.ToString() == "" ? Convert.ToChar('R') : Convert.ToChar(shortby), 0);
            // Return the DataSet as an XmlElement.
            XmlDataDocument xmldata = new XmlDataDocument(ds);
            XmlElement xmlElement = xmldata.DocumentElement;
            return xmlElement;

        }
        catch (Exception e)
        {
            throw e;

        }
    }


    [WebMethod]
    //Review List
    public XmlElement outletReview(int service_provider_sk)
    {
        try
        {
            createLog("outletReview");

            Business.BussinessMobileWeb _MobileWeb = new BussinessMobileWeb();

            DataSet ds = _MobileWeb.outletReview(service_provider_sk);
            // Return the DataSet as an XmlElement.
            XmlDataDocument xmldata = new XmlDataDocument(ds);
            XmlElement xmlElement = xmldata.DocumentElement;
            return xmlElement;

        }
        catch (Exception e)
        {

            throw e;

        }
    }

    [WebMethod]
    // write Review
    public XmlElement insertReview(int slot_booking_sk, int ReviewRating, string ReviewText)
    {
        try
        {
            createLog("insertReview");

            ReviewCls objReview = new ReviewCls();
            Business.BussinessMobileWeb _MobileWeb = new BussinessMobileWeb();
            objReview.ReviewSK = 0;
            objReview.slot_booking_sk = slot_booking_sk;
            objReview.ReviewText = ReviewText;
            objReview.ReviewRating = ReviewRating;
            DataSet ds = _MobileWeb.insertReview(objReview);
            // Return the DataSet as an XmlElement.
            XmlDataDocument xmldata = new XmlDataDocument(ds);
            XmlElement xmlElement = xmldata.DocumentElement;
            return xmlElement;

        }
        catch (Exception e)
        {

            throw e;

        }
    }


    //[WebMethod]
    ////Select secret questions
    //public XmlElement secretQus()
    //{
    //    try
    //    {
    //        createLog("secretQus");

    //        Business.BussinessMobileWeb _MobileWeb = new BussinessMobileWeb();
    //        DataSet ds = _MobileWeb.secretQus();
    //        // Return the DataSet as an XmlElement.
    //        XmlDataDocument xmldata = new XmlDataDocument(ds);
    //        XmlElement xmlElement = xmldata.DocumentElement;
    //        return xmlElement;
    //    }
    //    catch (Exception e)
    //    {

    //        throw e;

    //    }
    //}


    [WebMethod]
    //Seeker Calendar
    public XmlElement calendarTable(string provider_sk, string seeker_sk, string Searchdate)
    {
        try  //2012-03-21
        {

            createLog("calendarTable");

            DateTime date = Convert.ToDateTime(Searchdate);
            Business.BussinessMobileWeb _MobileWeb = new BussinessMobileWeb();
            int service_seeker_sk;
            if (seeker_sk.Trim() == "")
            {
                service_seeker_sk = 0;
            }
            else
                service_seeker_sk = Convert.ToInt32(seeker_sk);
            DataSet ds = _MobileWeb.Calendar(Convert.ToInt32(provider_sk), Convert.ToInt32(service_seeker_sk), date);
            // Return the DataSet as an XmlElement.
            XmlDataDocument xmldata = new XmlDataDocument(ds);
            XmlElement xmlElement = xmldata.DocumentElement;
            return xmlElement;
        }
        catch (Exception e)
        {
            throw e;

        }
    }


    [WebMethod]
    ////Seeker Calendar
    public String slotReservation(string slot_booking_Sk, string seeker_Sk, string comment, string noPeople, string qusAns, string s_slot)
    {
        try
        {
            createLog("slotReservation");
            DataTable BookingTables = createTable(s_slot);
            DataTable dtAns = GenerateQues(qusAns);
            SlotBokingCls oSlotBoking = new SlotBokingCls();
            BusinessSlotBooking oBusinessSlotBooking = new BusinessSlotBooking();
            oSlotBoking.ServiceSeekersk = Convert.ToInt32(seeker_Sk);

            if (slot_booking_Sk.Trim() != "")
            {
                oSlotBoking.Slotbookingsk = Convert.ToInt64(slot_booking_Sk);
            }
            else
            {
                oSlotBoking.Slotbookingsk = 0;
            }
            oSlotBoking.Noofpeople = noPeople != "" ? Convert.ToInt16(noPeople) : 0;
            oSlotBoking.ServiceSeekerComment = comment.Trim();
            oSlotBoking.UdttSlotDetails = BookingTables;
            oSlotBoking.UdttQuestionsAnswer = dtAns;
            Business.BussinessMobileWeb _MobileWeb = new BussinessMobileWeb();
            int Status = _MobileWeb.BookingSlot(oSlotBoking);
            DataSet ds = new DataSet("NewDataSet");
            DataTable dt = new DataTable("Table");
            dt = ConfirmationMessage();

            if (Status > 0)
            {
                dt.Rows.Add("T", "Reservation Successful!");
            }
            else
            {
                dt.Rows.Add("F", "Reservation Failed.");

            }
            ds.Tables.Add(dt);
            //   Return the DataSet as an XmlElement.




            return ds.GetXml();
        }
        catch
        {
            //  return xmlElement;
            throw;

        }
    }


    #region Seeker Calendar Supporting functions


    protected DataTable GenerateQues(string QuesAnser)
    {
        DataTable dt = PQuestions();
        if (QuesAnser.Trim() != "" && QuesAnser != null)
        {
            string[] QuesRow = QuesAnser.Split('#');
            string[] QusRowNo;

            for (int i = 0; i < QuesRow.Count(); i++)
            {
                QusRowNo = QuesRow[i].Split('|');

                DataRow dr = dt.NewRow();
                for (int j = 0; j < QusRowNo.Count(); j++)
                {
                    if (j == 0)
                        dr["question_sk"] = QusRowNo[j];
                    if (j == 1)
                        dr["answer"] = QusRowNo[j];

                }
                // add row to dataset now               
                dt.Rows.Add(dr);
            }
        }
        return dt;
    }
    private DataTable PQuestions()
    {

        DataTable dt = new DataTable();
        DataColumn dc;

        dc = new DataColumn("question_sk");
        dt.Columns.Add(dc);

        dc = new DataColumn("answer");
        dt.Columns.Add(dc);

        return dt;


    }
    public DataTable createTable(string s_slot)
    {
        string[] TableRow = s_slot.Split('#');
        string[] RowNo;
        DataTable dt = CreateDataTable();
        for (int i = 0; i < TableRow.Count(); i++)
        {
            RowNo = TableRow[i].Split('|');
            DataRow dr = dt.NewRow();
            for (int j = 0; j < RowNo.Count(); j++)
            {
                if (j == 0)
                    dr["slot_sk"] = RowNo[j];
                if (j == 1)
                    dr["slot_status"] = RowNo[j];
                if (j == 2)

                    dr["slot_booking_sk"] = RowNo[j].ToString() == "0" ? null : RowNo[j];
            }
            // add row to dataset now               
            dt.Rows.Add(dr);
        }
        return dt;

    }
    private DataTable CreateDataTable()
    {
        // Define the new datatable

        DataTable dt = new DataTable();
        DataColumn dc;

        dc = new DataColumn("slot_sk");
        dt.Columns.Add(dc);

        dc = new DataColumn("slot_status");
        dt.Columns.Add(dc);

        dc = new DataColumn("slot_booking_sk");
        dt.Columns.Add(dc);


        return dt;

    }

    private DataTable ConfirmationMessage()
    {
        // Define the new datatable

        DataTable dt = new DataTable();
        DataColumn dc;

        dc = new DataColumn("Status");
        dt.Columns.Add(dc);

        dc = new DataColumn("Message");
        dt.Columns.Add(dc);

        return dt;

    }
    #endregion seeker Calender

    [WebMethod]
    ////Delete Booking
    public XmlElement deleteBooking(string slot_booking_sk)
    {
        try
        {
            createLog("deleteBooking");
            Business.BussinessMobileWeb _MobileWeb = new BussinessMobileWeb();
            int Status = 0;
            string[] TableRow = slot_booking_sk.Split('|');
            for (int i = 0; i < TableRow.Count(); i++)
            {

                Status = _MobileWeb.DeleteBooking(Convert.ToInt32(TableRow[i]));
            }

            DataSet ds = new DataSet();

            var xml = "<Booking success='T' message='Booking Deleted!'/>";
            var xml1 = "<NewDataSet><Table><detail><Booking success='F' message='Delete Operation Failed.'/></detail></Table></NewDataSet>";
            if (Status > 0)
            {
                ds.ReadXml(new StringReader(xml), XmlReadMode.Auto);
            }
            else
            {
                ds.ReadXml(new StringReader(xml1), XmlReadMode.Auto);

            }


            // Return the DataSet as an XmlElement.
            XmlDataDocument xmldata = new XmlDataDocument(ds);
            XmlElement xmlElement = xmldata.DocumentElement;
            return xmlElement;
        }

        catch (Exception e)
        {
            throw e;

        }
    }

    [WebMethod]
    ////Seeker Old booking
    public XmlElement oldReservation(string slotDate, string seeker_sk, string provider_sk)
    {
        try
        {
            createLog("oldReservation");
            Business.BussinessMobileWeb _MobileWeb = new BussinessMobileWeb();
            DataSet ds = _MobileWeb.OldBookingCheck(Convert.ToDateTime(slotDate), Convert.ToInt32(seeker_sk), Convert.ToInt32(provider_sk));
            // Return the DataSet as an XmlElement.
            XmlDataDocument xmldata = new XmlDataDocument(ds);
            XmlElement xmlElement = xmldata.DocumentElement;
            return xmlElement;
        }
        catch (Exception e)
        {
            throw e;

        }
    }

    [WebMethod]
    ////Seeker Old booking
    public string sendMail(string sender, string Mrecipients, string Msubject, string Mbody)
    {
        try
        {
            createLog("sendMail");
            BussinessSendMail _mail = new BussinessSendMail();
            Business.BussinessMobileWeb _MobileWeb = new BussinessMobileWeb();
            _mail.sender = sender;
            _mail.Mrecipients = Mrecipients;
            _mail.Msubject = Msubject;
            _mail.Mbody = Mbody;

            int Status = _MobileWeb.SendMail(_mail);

            DataTable dt = new DataTable("table");
            dt.Columns.Add("success");
            dt.Columns.Add("message");
            DataRow dr = dt.NewRow();
            DataSet ds = new DataSet();

            // if (Status > 0)
            // {

            var xml = "<email success='1' message='Mail Sent!'></email>";
            ds.ReadXml(new StringReader(xml), XmlReadMode.Auto);

            //dr["success"] = "1";
            //dr["message"] = "Mail Sent!";
            //dt.Rows.Add(dr);

            //ds.Tables.Add(dt);
            // }
            //  else
            //  {
            //    dr["success"] = "0";
            //    dr["message"] = "Sending Failed!";
            //    dt.Rows.Add(dr);
            //       ds.Tables.Add(dt);
            //  }


            // Return the DataSet as an XmlElement.
            XmlDataDocument xmldata = new XmlDataDocument(ds);
            XmlElement xmlElement = xmldata.DocumentElement;
            return xmlElement.InnerXml;
        }

        catch (Exception e)
        {
            throw e;

        }
    }


    [WebMethod]
    ////Provider Questions
    public XmlElement providerQuestions(string provider_sk)
    {
        try
        {
            createLog("providerQuestions");
            Business.BussinessMobileWeb _MobileWeb = new BussinessMobileWeb();
            DataSet ds = _MobileWeb.GetServiceProviderQuestions(Convert.ToInt32(provider_sk));
            // Return the DataSet as an XmlElement.
            XmlDataDocument xmldata = new XmlDataDocument(ds);
            XmlElement xmlElement = xmldata.DocumentElement;
            return xmlElement;
        }

        catch (Exception e)
        {
            throw e;

        }
    }
    [WebMethod]
    ////Provider Questions
    public XmlElement promotiondetail(string provider_sk, string searchdate, string currenttime)
    {
        try
        {
            createLog("promotiondetail");

            Business.BussinessMobileWeb _MobileWeb = new BussinessMobileWeb();
            DataSet ds = _MobileWeb.promotiondetail(Convert.ToInt32(provider_sk), searchdate, Convert.ToDateTime(currenttime).ToShortTimeString());
            // Return the DataSet as an XmlElement.
            XmlDataDocument xmldata = new XmlDataDocument(ds);
            XmlElement xmlElement = xmldata.DocumentElement;
            return xmlElement;
        }

        catch (Exception e)
        {
            throw e;

        }
    }
    [WebMethod]
    ////Provider Questions
    public XmlElement sheduletime(string provider_sk, string search_date)
    {
        try
        {
            createLog("sheduletime");

            Business.BussinessMobileWeb _MobileWeb = new BussinessMobileWeb();
            DataSet ds = _MobileWeb.sheduletime(Convert.ToInt32(provider_sk), search_date);
            // Return the DataSet as an XmlElement.
            XmlDataDocument xmldata = new XmlDataDocument(ds);
            XmlElement xmlElement = xmldata.DocumentElement;
            return xmlElement;
        }

        catch (Exception e)
        {
            throw e;

        }
    }

    //offline dataset


    [WebMethod]
    public XmlElement getSearchCalendarDs(string provider_sk)
    {
        try
        {
            createLog("getSearchCalendarDs");

            Business.BussinessOffline _MobileWeboff = new BussinessOffline();
            DataSet ds = _MobileWeboff.getSearchCalendarDs(Convert.ToInt32(provider_sk));
            // Return the DataSet as an XmlElement.
            XmlDataDocument xmldata = new XmlDataDocument(ds);
            XmlElement xmlElement = xmldata.DocumentElement;
            return xmlElement;
        }

        catch (Exception e)
        {
            throw e;

        }
    }

    //error 


    public void createLog(string webservice)
    {

        string req_url = HttpContext.Current.Request.Url.ToString();
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.Load(Server.MapPath(@"WebServiceError.xml"));
        //Create a new node
        XmlElement newelement = xmldoc.CreateElement("Requested_Url");

        XmlElement xmlService = xmldoc.CreateElement("Service");
        XmlElement xmlUrl = xmldoc.CreateElement("Url");
        XmlElement xmlDate = xmldoc.CreateElement("Date");

        xmlService.InnerText = webservice;
        xmlUrl.InnerText = req_url;
        xmlDate.InnerText = System.DateTime.Now.ToString();

        newelement.AppendChild(xmlService);
        newelement.AppendChild(xmlUrl);
        newelement.AppendChild(xmlDate);

        xmldoc.DocumentElement.AppendChild(newelement);

        //xmlDoc.DocumentElement.InsertAfter(newElement,xmlDoc.DocumentElement.ChildNodes.Item(0));

        //xmlDoc.DocumentElement.InsertBefore(newElement,xmlDoc.DocumentElement.ChildNodes.Item(0));
        //save
        xmldoc.Save(Server.MapPath(@"WebServiceError.xml"));


    }
}



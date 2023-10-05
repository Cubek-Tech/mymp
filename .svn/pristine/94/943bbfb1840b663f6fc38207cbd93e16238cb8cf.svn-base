using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class Controls_DatePicker2 : System.Web.UI.UserControl
{
    #region Properties  
    public void Page_Load(object sender, EventArgs e)
    {
        string to = hdnto.Value;
    
    }


    public bool ShowTime
    {
        get
        {
            return Convert.ToBoolean(ViewState["ShowTime"]);
        }
        set
        {
            ViewState["ShowTime"] = value;
        }
    }
    public Nullable<System.DateTime> SelectedDate
    {


        get
        {

            try
            {
                DateTime inptDate;
                if (datepicker.Text == string.Empty)
                {
                    return null;
                }
                //String.Format("{0:dd-MMM-yyyy}", DateTime.Now.Date);
                //DateTime dt = Convert.ToDateTime(datepicker.Text);
                DateTime dt = Convert.ToDateTime(String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(datepicker.Text).Date)).Date; //Convert.ToDateTime(datepicker.Text).Date; //
                inptDate = dt;



                return inptDate;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //return null;

        }




        set
        {
            datepicker.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(value).Date);
                //Convert.ToDateTime(value).ToShortDateString();
        }
    }
    public String DateText
    {

        get
        {
            DateTime inptDate = DateTime.Parse(datepicker.Text);
            return String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(inptDate).Date);
                //Convert.ToDateTime(inptDate).ToShortDateString();
        }
        set
        {
           // string scdate = String.Format("{0:dd-MMM-yyyy}", value);

            datepicker.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(value).Date);
                //String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(value).ToShortDateString());
                //Convert.ToDateTime(value).ToShortDateString();

        }
    }
    public string TextNull
    {
        set
        {

            datepicker.Text = Convert.ToString(value);
        }
    }
    private bool _ShowMonth = false;
    public bool ShowMonth
    {
        get
        {

            if (hdnyear.Value.Length > 0)
            {
                return Convert.ToBoolean(hdnMonth.Value);
            }
            else
            {
                hdnMonth.Value = Convert.ToString(_ShowMonth);
                return Convert.ToBoolean(hdnMonth.Value);
            }
        }
        set
        {
            if ((Convert.ToString(hdnMonth.Value) != "false") || (Convert.ToString(hdnMonth.Value) != "true"))
                hdnMonth.Value = Convert.ToString(false);
            else
                hdnMonth.Value = Convert.ToString(_ShowMonth);
        }
    }
    private bool _ShowYear = false;
    public bool ShowYear
    {
        get
        {
            if (hdnyear.Value.Length > 0)
            {
                return Convert.ToBoolean(hdnyear.Value);
            }
            else
            {
                hdnyear.Value = Convert.ToString(_ShowYear);
                return Convert.ToBoolean(hdnyear.Value);
            }
        }
        set
        {

            if ((Convert.ToString(hdnyear.Value) != "false") || (Convert.ToString(hdnyear.Value) != "true"))
                hdnyear.Value = Convert.ToString(false);
            else
                hdnyear.Value = Convert.ToString(_ShowYear);



        }
    }

    private bool _ShowCurrentdate = false;
    public bool ShowCurrentdate
    {
        get
        {

            hdnTodate.Value = Convert.ToString(_ShowCurrentdate);
            return Convert.ToBoolean(hdnTodate.Value);
        }
        set
        {


            hdnTodate.Value = Convert.ToString(value);
        }
    }


    private bool _ShowPastdate = false;
    public bool ShowPastdate
    {
        get
        {

            hdnTodate.Value = Convert.ToString(_ShowPastdate);
            return Convert.ToBoolean(hdnpastDate.Value);
        }
        set
        {


            hdnpastDate.Value = Convert.ToString(value);
        }
    }
    
    public Int32 Width
    {
        set
        {
            if (Convert.ToInt32(value) == 0)
            {
                datepicker.Style["Width"] = "80px;";  //.Attributes.Add("Width=","80px");
            }
            else
            {
                //datepicker.Width =Convert.ToInt32(value);
                datepicker.Style["Width"] = Convert.ToInt32(value) + "px";//.Attributes.Add("Width=", "" + Convert.ToInt32(value) + "px");
            }

        }
    }
    // private Int32 _Height;
    public Int32 Height
    {
        set
        {
            if (Convert.ToInt32(value) == 0)
            {

                //datepicker.Style["Height"] = "0px;";
            }
            else
            {
                datepicker.Style["Height"] = Convert.ToInt32(value) + "px";
            }

        }
    }

    public string RequiredErrorMessage
    {
        get
        {
            return rfvValue.ErrorMessage;
        }
        set
        {
            rfvValue.ErrorMessage = value;
        }
    }
    public string ValidationGroup
    {
        get
        {
            return rfvValue.ValidationGroup;
        }
        set
        {
            rfvValue.ValidationGroup = value;
        }
    }

    public string CssClass
    {
        get
        {
            return datepicker.CssClass;
        }
        set
        {
            datepicker.CssClass = value;
        }
    }
    public string MyClientID
    {
        get
        {
            return datepicker.ClientID;
        }

    }
    string _OtherControlClientID;
    public string OtherControlClientID
    {
        set
        {
            _OtherControlClientID = value;
            hdnOtherControlClientID.Value = _OtherControlClientID;
        }
        get
        {
            hdnOtherControlClientID.Value = _OtherControlClientID;
            return _OtherControlClientID;
        }
    }

    //#from, #to
    private String _from = "";
    public String from
    {
        get
        {
            
            if (_from != "") return hdnfrom.Value;
            else
                return Convert.ToString(_from.Trim());


        }
        set
        {
            _from = Convert.ToString(value).Trim().ToLower();
            hdnfrom.Value = Convert.ToString(value).Trim();
        }
    }
  
    
    private String _to = "";
    public String to
    {
        get
        {
            if (_to != "") return hdnto.Value;
            else
                return Convert.ToString(_to.Trim());

        }
        set
        {
            _to = Convert.ToString(value).Trim();
            hdnto.Value = Convert.ToString(value).Trim();
        }

    }

    public String _Startdate = "";
    public String Startdate
    {
        get
        {
            if (_Startdate != "") return hdnStartdate.Value;
            else
                return Convert.ToString(_Startdate.Trim());
        }
        set
        {
            _Startdate = Convert.ToString(value).Trim();
            hdnStartdate.Value = Convert.ToString(value).Trim();
        }

    }


    public String _EndDate = "";
    public String EndDate
    {
        get
        {
            if (_EndDate != "") return hdnto.Value;
            else
                return Convert.ToString(_EndDate.Trim());
        }
        set
        {
            _EndDate = Convert.ToString(value).Trim();
            hdnEnddate.Value = Convert.ToString(value).Trim();
        }

    }



    #endregion
	//protected void datepicker_TextChanged(object sender,EventArgs e)
	//    {
	//          DateTime dt =Convert.ToDateTime(datepicker.Text);
	//          if(Convert.ToDateTime(dt.ToShortDateString())> Convert.ToDateTime(DateTime.Now.Date.ToShortDateString()))
	//          {
	//             datepicker.Text =string.Format("{0:mm/dd/yyyy}",DateTime.Now.Date.ToShortDateString());        
	//          } 
	//    }


    private string _onclick = "";
    public string onclick
    {
        get
        { 
            return Convert.ToString(hdnOnclick.Value);
           
        }
        set
        {
            if ((Convert.ToString(hdnOnclick.Value) != ""))
                hdnMonth.Value = "";
            else
                hdnMonth.Value = Convert.ToString(_onclick);
        }
    }
}

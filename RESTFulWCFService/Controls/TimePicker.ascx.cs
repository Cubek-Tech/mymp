using System;
using System.Collections;
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

public partial class Controls_TimePicker : System.Web.UI.UserControl
{
    
       public Nullable<System.DateTime> SelectedDate1
    {
        get
        {
            try
            {
                DateTime inptDate;
                if (datepicker1.Text == string.Empty)
                {
                    return null;
                }
               
                DateTime dt = Convert.ToDateTime(String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(datepicker1.Text).Date)).Date; //Convert.ToDateTime(datepicker.Text).Date; //
                inptDate = dt;
                return inptDate;
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }

        set
        {
            datepicker1.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(value).Date);
           
        }
    }
    public Nullable<System.DateTime> SelectedDate2
    {
        get
        {
            try
            {
                DateTime inptDate;
                if (datepicker2.Text == string.Empty)
                {
                    return null;
                }

                DateTime dt = Convert.ToDateTime(String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(datepicker2.Text).Date)).Date; //Convert.ToDateTime(datepicker.Text).Date; //
                inptDate = dt;
                return inptDate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        set
        {
            datepicker2.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(value).Date);

        }
    }
    private String _from = "";
    public String from
    {
        get
        {

            if (_from != "") return hdnStartDate.Value;
            else
                return Convert.ToString(_from.Trim());


        }
        set
        {
            _from = Convert.ToString(value).Trim().ToLower();
            hdnStartDate.Value = Convert.ToString(value).Trim();
        }
    }
  
      


}

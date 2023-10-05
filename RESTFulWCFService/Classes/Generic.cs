using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Globalization;


/// <summary>
/// Narendra Maheshwari
/// Generic class for calcualte date for the calender.
/// 
/// </summary>
public class Generic
{
    public static string currentDate { get; set; }
    public static string currentTime { get; set; }


    public Generic()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string CalDate()
    {

        int year = 0;
        int day = 0;
        int month = 0;

        year = Convert.ToInt32(DateTime.Now.Year);
        day = Convert.ToInt32(DateTime.Now.Day);
        month = Convert.ToInt32(DateTime.Now.Month);
        string startDate = "";
        string endDate = "";

     

        if (IsLeapYear(year))
        {

            if (month == 2 && day == 29)
            {

                startDate = ((1).ToString() + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month + 1) + "-" + year).ToString();
                if (month <= 6)
                    endDate = ((1).ToString() + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month + 4) + "-" + year).ToString();
                else
                {
                    DateTime dt = (DateTime.Now.Date.AddYears(1));
                    year = Convert.ToInt32(dt.Year);
                    if (month + 3 > 12)
                        month = (month + 3) - 12;
                    else
                        month += 3;
                    endDate = ((1).ToString() + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month) + "-" + year).ToString();


                }
            }
            else
            {


                if (month == 2 && day == 28)
                {

                    startDate = ((1).ToString() + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month + 1) + "-" + year).ToString();
                    if (month <= 6)
                        endDate = ((1).ToString() + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month + 4) + "-" + year).ToString();
                    else
                    {
                        DateTime dt = (DateTime.Now.Date.AddYears(1));
                        year = Convert.ToInt32(dt.Year);
                        if (month + 3 > 12)
                            month = (month + 3) - 12;
                        else
                            month += 3;
                        endDate = ((Convert.ToInt32(day) + 1).ToString() + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month) + "-" + year).ToString();



                    }
                }
                else
                {
                    if (month == 4 || month == 9 || month == 6 || month == 11 && day == 30)
                    {
                        startDate = ((Convert.ToInt32(day)).ToString() + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month) + "-" + year).ToString();
                        if (month <= 6)
                            endDate = ((Convert.ToInt32(day)).ToString() + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month + 3) + "-" + year).ToString();
                        else
                        {
                            DateTime dt = (DateTime.Now.Date.AddYears(1));
                            year = Convert.ToInt32(dt.Year);
                            if (month + 3 > 12)
                                month = (month + 3) - 12;
                            else
                                month += 3;

                            if (month == 2 && day > 29)
                            {
                                month += 1;
                                day = 1;

                            }
                            endDate = ((Convert.ToInt32(day)).ToString() + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month) + "-" + year).ToString();


                        }
                    }
                    else
                    {
                        if (day != 31 && month != 12)
                        {
                            startDate = ((Convert.ToInt32(1)).ToString() + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month) + "-" + year).ToString();
                            if (month <= 6)
                                endDate = ((Convert.ToInt32(1)).ToString() + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month + 3) + "-" + year).ToString();
                            else
                            {
                                DateTime dt = (DateTime.Now.Date.AddYears(1));
                                year = Convert.ToInt32(dt.Year);
                                if (month + 1 + 3 > 12)
                                    month = (month + 1 + 3) - 12;
                                else
                                    month += 3 + 1;
                                endDate = (Convert.ToInt32(1).ToString() + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month) + "-" + year).ToString();
                            }

                        } 

                        if (day == 31 && month != 12)
                        {
                            startDate = ((Convert.ToInt32(1)).ToString() + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month + 1) + "-" + year).ToString();
                            if (month <= 6)
                                endDate = ((Convert.ToInt32(1)).ToString() + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month + 1 + 3) + "-" + year).ToString();
                            else
                            {
                                DateTime dt = (DateTime.Now.Date.AddYears(1));
                                year = Convert.ToInt32(dt.Year);
                                if (month + 3 > 12)
                                    month = (month + 3) - 12;
                                else
                                    month += 3 ;
                                endDate = (Convert.ToInt32(1).ToString() + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month) + "-" + year).ToString();
                            }

                        } 
                        
                        if (day == 31 && month == 12)
                        {
                            DateTime dt = (DateTime.Now.Date.AddYears(1));
                            year = Convert.ToInt32(dt.Year);
                            day = 1;
                            month = 1;
                            startDate = ((Convert.ToInt32(day)).ToString() + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month) + "-" + year).ToString();
                            if (month <= 6)
                                endDate = ((Convert.ToInt32(day)).ToString() + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month + 3) + "-" + year).ToString();
                            else
                            {

                                if (month + 3 > 12)
                                    month = (month + 3) - 12;
                                else
                                    month += 3;
                                endDate = ((Convert.ToInt32(day) + 1).ToString() + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month) + "-" + year).ToString();
                            }

                        }
                    }
                }
            }
        }

        else
        {
            if (month == 2 && day == 28)
            {

                startDate = ((1).ToString() + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month + 1) + "-" + year).ToString();
                if (month <= 6)
                    endDate = ((1).ToString() + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month + 4) + "-" + year).ToString();
                else
                {
                    DateTime dt = (DateTime.Now.Date.AddYears(1));
                    year = Convert.ToInt32(dt.Year);
                    if (month + 3 > 12)
                        month = (month + 3) - 12;
                    else
                        month += 3;
                    endDate = ((Convert.ToInt32(day) + 1).ToString() + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month) + "-" + year).ToString();



                }
            }
            else
            {
                if (month == 4 || month == 9 || month == 6 || month == 11 && day == 30)
                {
                    startDate = ((Convert.ToInt32(day)).ToString() + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month) + "-" + year).ToString();
                    if (month <= 6)
                        endDate = ((Convert.ToInt32(day)).ToString() + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month + 3) + "-" + year).ToString();
                    else
                    {
                        DateTime dt = (DateTime.Now.Date.AddYears(1));
                        year = Convert.ToInt32(dt.Year);
                        if (month + 3 > 12)
                            month = (month + 3) - 12;
                        else
                            month += 3;

                        if (month == 2 && day > 28)
                        {
                            month += 1;
                            day = 1;

                        }
                        endDate = ((Convert.ToInt32(day)).ToString() + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month) + "-" + year).ToString();


                    }
                }
                else
                {

                    if (day == 31 && month != 12)
                    {
                        startDate = ((Convert.ToInt32(1)).ToString() + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month + 1) + "-" + year).ToString();
                        if (month <= 6)
                            endDate = ((Convert.ToInt32(1)).ToString() + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month + 1 + 3) + "-" + year).ToString();
                        else
                        {
                            DateTime dt = (DateTime.Now.Date.AddYears(1));
                            year = Convert.ToInt32(dt.Year);
                            if (month + 1 + 3 > 12)
                                month = (month + 1 + 3) - 12;
                            else
                                month += 3 + 1;
                            endDate = (Convert.ToInt32(1).ToString() + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month) + "-" + year).ToString();
                        }

                    }


                    if (day == 31 && month == 12)
                    {
                        DateTime dt = (DateTime.Now.Date.AddYears(1));
                        year = Convert.ToInt32(dt.Year);
                        day = 1;
                        month = 1;
                        startDate = ((Convert.ToInt32(day)).ToString() + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month) + "-" + year).ToString();
                        if (month <= 6)
                            endDate = ((Convert.ToInt32(day)).ToString() + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month + 3) + "-" + year).ToString();
                        else
                        {

                            if (month + 3 > 12)
                                month = (month + 3) - 12;
                            else
                                month += 3;
                            endDate = ((Convert.ToInt32(day) + 1).ToString() + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month) + "-" + year).ToString();
                        }

                    }



                    startDate = ((Convert.ToInt32(day + 1)).ToString() + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month) + "-" + year).ToString();
                    if (month <= 6)
                        endDate = ((Convert.ToInt32(day)).ToString() + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month + 3) + "-" + year).ToString();
                    else
                    {
                        DateTime dt = (DateTime.Now.Date.AddYears(1));
                        year = Convert.ToInt32(dt.Year);
                        if (month + 3 > 12)
                            month = (month + 3) - 12;
                        else
                            month += 3;
                        endDate = ((Convert.ToInt32(day) + 1).ToString() + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month) + "-" + year).ToString();
                    }
                }
            }
        }

        //---------------------------------------------------
        return startDate + "|" + endDate;
    }

    private static bool IsLeapYear(int year)
    {
        if (year % 4 != 0)
        {
            return false;
        }

        if (year % 100 == 0)
        {
            return (year % 400 == 0);
        }

        return true;
    }


}


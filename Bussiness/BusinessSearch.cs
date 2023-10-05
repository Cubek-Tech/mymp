using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Factory;

namespace Business
{
    /// <summary>
    /// update by Narendra 4/1/2012
    /// </summary>

    public class BusinessSearch
    {
        #region Events
        public Int32 ss_favourites_sk { get; set; }
        public Int32 service_seeker_sk { get; set; }
        public Int32 service_provider_sk { get; set; }
        public string comments { get; set; }
        public char is_blocked { get; set; }
        public char? is_mailer { get; set; }
        #endregion

        #region Methods
        DataSet ds;
        Factory.FactorySearch objFactorySearch = new FactorySearch();
        public DataSet getSearchData(DateTime search_date, int? sp_category_sk, int? sp_sub_category_sk, int country_sk, int state_sk,
                                    int city_sk, int? area_sk, string keyword, char sort_by, int service_seeker_sk, char? is_out_call,
                                    char? massage_or_spa, int row_start_from)
        {
            ds = new DataSet();
            ds = objFactorySearch.getSearchData(search_date, sp_category_sk, sp_sub_category_sk, country_sk, state_sk, city_sk, area_sk, keyword, sort_by, service_seeker_sk, is_out_call, massage_or_spa, row_start_from);
            return ds;
        }

        public DataSet getSearchData_Check(DateTime search_date, int? sp_category_sk, int? sp_sub_category_sk, int country_sk, int state_sk,
                                    int city_sk, int? area_sk, string keyword, char sort_by, int service_seeker_sk, char? is_out_call,
                                    char? massage_or_spa)
        {
            ds = new DataSet();
            ds = objFactorySearch.getSearchData_Check(search_date, sp_category_sk, sp_sub_category_sk, country_sk, state_sk, city_sk, area_sk, keyword, sort_by, service_seeker_sk, is_out_call, massage_or_spa);
            return ds;
        }




        public DataSet getSearchData_1(DateTime search_date, int? sp_category_sk, int? sp_sub_category_sk, int country_sk, int state_sk,
                                  int city_sk, int? area_sk, string keyword, char sort_by, int service_seeker_sk, char? is_out_call,
                                  char? massage_or_spa, string Orientation)
        {
            ds = new DataSet();
            ds = objFactorySearch.getSearchData_1(search_date, sp_category_sk, sp_sub_category_sk, country_sk, state_sk, city_sk, area_sk, keyword, sort_by, service_seeker_sk, is_out_call, massage_or_spa, Orientation);
            return ds;
        }


        public DataSet getSearchData_2(int service_seeker_sk, DataTable udtt_search_result, char mode)
        {
            ds = new DataSet();
            ds = objFactorySearch.getSearchData_2(service_seeker_sk, udtt_search_result, mode);
            return ds;
        }
        
        
        
        
        
        public DataSet getareasearch(string city_state_country, string area, string fnbtype)
        {
            ds = new DataSet();
            ds = objFactorySearch.getareasearch(city_state_country, area, fnbtype);
            return ds;
        }

        public DataSet getSearchDorpdownValues()
        {
            ds = new DataSet();
            ds = objFactorySearch.getSearchDorpdownValues();
            return ds;
        }



        public DataSet getDataFromProvider(int provider_id)
        {
            ds = new DataSet();
            ds = objFactorySearch.getDataFromProvider(provider_id);
            return ds;
        }
        public DataSet reviewRatingBySeekerForBooking(int seeker_sk, int provider_sk, DateTime booking_date)
        {
            ds = new DataSet();
            ds = objFactorySearch.reviewRatingBySeekerForBooking(seeker_sk, provider_sk, booking_date);
            return ds;
        }
        public DataSet getSeekerCalendar(int service_provider_sk, DateTime slot_date)
        {
            ds = new DataSet();
            ds = objFactorySearch.getSeekerCalendar(service_provider_sk, slot_date);
            return ds;
        }
        public DataTable getSearchCalendar(int Service_provider_sk, DateTime Slot_date)
        {
            return objFactorySearch.getSearchCalendar(Service_provider_sk, Slot_date);
        }


        public DataTable getSearchCalendarTable(int Service_provider_sk, DateTime Slot_date)
        {
            return objFactorySearch.getSearchCalendarTable(Service_provider_sk, Slot_date);
        }

        public DataTable getBusinessHour(int Service_provider_sk, DateTime Slot_date)
        {
            return objFactorySearch.getBusinessHour(Service_provider_sk, Slot_date);
        }
        public DataSet getfoodtypesk(string city_state_country, string area, string fnbtype)
        {
            ds = new DataSet();
            ds = objFactorySearch.getfoodtypesk(city_state_country, area, fnbtype);
            return ds;
        }
        public bool IsRegstrationCompleted(int Service_provider_sk)
        {
            return objFactorySearch.IsRegstrationCompleted(Service_provider_sk);
        }

        public DataTable GetServiceProviderQuestions(Int32 service_provider_sk, int? slot_booking_sk)
        {
            return objFactorySearch.GetServiceProviderQuestions(service_provider_sk, slot_booking_sk);
        }
        //public DataSet GetScheduleTime(int Service_provider_sk, DateTime Slot_date)
        //{
        //    return objFactorySearch.GetScheduleTime(Service_provider_sk, Slot_date);
        //}
        public DataTable GetScheduleTimeProviderDetails(int Service_provider_sk)
        {
            return objFactorySearch.GetScheduleTimeProviderDetails(Service_provider_sk);
        }
        public DataTable GetScheduleTimeProviderSlotTimeDetails(int Service_provider_sk, DateTime Slot_date)
        {
            return objFactorySearch.GetScheduleTimeProviderSlotTimeDetails(Service_provider_sk, Slot_date);
        }


        /// <summary>
        /// Add to MyFevourate
        /// Narendra 4/1/2012
        /// </summary>
        /// <returns></returns>
        /// Start 

        public int InsertMyFevourite(Int32 service_seeker_sk, Int32 service_provider_sk, string comments, char is_blocked, char is_mailer)
        {
            return objFactorySearch.InsertMyFevourite(service_seeker_sk, service_provider_sk, comments, is_blocked, is_mailer);
        }
        public int DeleteMyFevourite(Int32 service_seeker_sk, Int32 service_provider_sk)
        {
            return objFactorySearch.DeleteMyFevourite(service_seeker_sk, service_provider_sk);
        }
        public DataSet GetMyFevourite(Int32 service_seeker_sk, Int32 service_provider_sk)
        {
            return objFactorySearch.GetMyFevourite(service_seeker_sk, service_provider_sk);
        }
        ///End


        //provider favorite seekers
        public DataSet spFavouriteSeeker(Int32 service_provider_sk)
        {
            return objFactorySearch.spFavouriteSeeker(service_provider_sk);
        }

        public int Delete_spFavouriteSeeker(Int32 sp_favourites_sk)
        {
            return objFactorySearch.Delete_spFavouriteSeeker(sp_favourites_sk);
        }

        public int Update_ssFavouriteSeeker(Int32 sp_favourites_sk, string is_blocked)
        {
            return objFactorySearch.Update_ssFavouriteSeeker(sp_favourites_sk, is_blocked);
        }



        //end
        public DataSet GetSeekerBookingDetail(Int32 service_seeker_sk, DateTime startdate, DateTime enddate)
        {
            return objFactorySearch.GetSeekerBookingDetail(service_seeker_sk, startdate, enddate);
        }


        public DataSet GetProviderBookingDetail(Int32 service_provider_sk, DateTime startdate, DateTime enddate)
        {
            return objFactorySearch.GetProviderBookingDetail(service_provider_sk, startdate, enddate);
        }




        //
        public DataTable Reader_GetSeekerBookingDetail(Int32 service_seeker_sk, DateTime startdate, DateTime enddate)
        {
            return objFactorySearch.Reader_GetSeekerBookingDetail(service_seeker_sk, startdate, enddate);

        }
        //
        public DataSet ssFavouriteOutlet(Int32 service_seeker_sk)
        {
            return objFactorySearch.ssFavouriteOutlet(service_seeker_sk);
        }


        public int Delete_ssFavouriteOutlet(Int32 ss_favourites_sk)
        {
            return objFactorySearch.Delete_ssFavouriteOutlet(ss_favourites_sk);
        }
        public int Update_ssFavouriteOutletInt32(Int32 ss_favourites_sk, string is_mailer, string is_blocked)
        {
            return objFactorySearch.Update_ssFavouriteOutlet(ss_favourites_sk, is_mailer, is_blocked);
        }

        public int EditComments(Int32 ss_favourites_sk, string comments)
        {
            return objFactorySearch.EditComments(ss_favourites_sk, comments);
        }

        public int SPEditComments(Int32 sp_favourites_sk, string comments)
        {
            return objFactorySearch.SPEditComments(sp_favourites_sk, comments);
        }


        public DataTable providerDayCustomer(int Service_provider_sk, DateTime Slot_date)
        {
            return objFactorySearch.providerDayCustomer(Service_provider_sk, Slot_date);
        }
        public DataSet getRemainingSlot(int Service_provider_sk, DateTime Slot_date, DateTime? current_date)
        {
            return objFactorySearch.getRemainingSlot(Service_provider_sk, Slot_date, current_date);
        }


        /// <summary>
        /// to get seeker booking for perticular day.
        /// </summary>      
        public DataSet SeekerBookingByDate(string date, int service_seeker_sk, int service_provider_sk)
        {
            return objFactorySearch.SeekerBookingByDate(date, service_seeker_sk, service_provider_sk);
        }
        //added by nilesh g for quick book details
        public DataSet SeekerQuickBookingByDate(string date, int service_seeker_sk, int service_provider_sk)
        {
            return objFactorySearch.SeekerQuickBookingByDate(date, service_seeker_sk, service_provider_sk);
        }
        //end here for quick book details

        public DataSet ProviderBookingByDate(string date, int service_seeker_sk, int service_provider_sk)
        {
            return objFactorySearch.ProviderBookingByDate(date, service_seeker_sk, service_provider_sk);
        }

        public int InsertHiCounter(DateTime hit_datetime, string hit_ip, int country_sk, int state_sk, int city_sk,char mode)
        {
            return objFactorySearch.InsertHiCounter(hit_datetime, hit_ip, country_sk, state_sk, city_sk, mode);
        }

        public int InsertHiCounter(string hit_ip, int country_sk, int state_sk, int city_sk, string site)
        {
            return objFactorySearch.InsertHiCounter(hit_ip, country_sk, state_sk, city_sk, site);
        }

        #endregion

        public DataSet getSearchData_12(DateTime search_date, int? sp_category_sk, int? sp_sub_category_sk, int country_sk, int state_sk,
                      int city_sk, int? area_sk, string keyword, char sort_by, int service_seeker_sk, char? is_out_call,
                      char? massage_or_spa, string Orientation, string is_outcallType)
        {
            ds = new DataSet();
            ds = objFactorySearch.getSearchData_12(search_date, sp_category_sk, sp_sub_category_sk, country_sk, state_sk, city_sk, area_sk, keyword, sort_by, service_seeker_sk, is_out_call, massage_or_spa, Orientation, is_outcallType);
            return ds;
        }

        
        public DataSet SelectCurrentTime()
        {
            return objFactorySearch.SelectCurrentTime();

        }
        public DataSet DeliveryDetailbyseeker_sk(int seeker_sk)
        {
            return objFactorySearch.DeliveryDetailbyseeker_sk(seeker_sk);

        }
        public DataSet DeliveryDetailbyprovider_sk(int provider_sk)
        {
            return objFactorySearch.DeliveryDetailbyprovider_sk(provider_sk);

        }


        public DataSet ShowGlobalText(string type)
        {

            return objFactorySearch.ShowGlobalText(type);

        }
        //Asynronous objects

        public SqlCommand BeginExecution(DateTime date, int cid, int sid, int ctid)
        {
            return objFactorySearch.BeginExecution(date, cid, sid, ctid);
        }

        //for all spa type
        public DataSet getSpaType(string mode)
              {
                DataSet ds = new DataSet();
            ds = objFactorySearch.getSpaType(mode);
            return ds;
        }
        // for spa sub type
        public DataSet getSpaSubType(int service_sk, string service_type)
        {
            DataSet ds = new DataSet();
            ds = objFactorySearch.getSpaSubType(service_sk, service_type);
            return ds;
        }

        public DataSet getServiceitem(string mode)
        {
            DataSet ds = new DataSet();
            ds = objFactorySearch.getServiceitem(mode);
            return ds;
        }
        public DataSet provider_by_search_url(string url)
        {
            DataSet ds = new DataSet();
            ds = objFactorySearch.provider_by_search_url(url);
            return ds;
        }

        public DataSet Send_free_sms(string msg_text, string seeker_mob, int provider_sk, int seeker_sk)
        {
            DataSet ds = new DataSet();
            ds = objFactorySearch.Send_free_sms(msg_text, seeker_mob, provider_sk, seeker_sk);
            return ds;
        }

        public DataSet get_remaing_SMS(int seeker_sk,int provider_sk)
        {
            DataSet ds = new DataSet();
            ds = objFactorySearch.get_remaing_SMS(seeker_sk, provider_sk);
            return ds;
        }

        public DataSet quick_book(string msg_text,string subject,int provider_sk, int seeker_sk,DateTime qdate,string service,string customertime,char mode)
        {
            DataSet ds = new DataSet();
            ds = objFactorySearch.quick_book(msg_text, subject, provider_sk, seeker_sk, qdate,service, customertime, mode);
            return ds;
        }
        //get providers for giftvoucher
        public DataSet getProvider(int country_sk, int state_sk,
                                  int city_sk, Int32? area_sk, int? sp_category_sk, int? sp_sub_category_sk,
                                  char? massage_or_spa)
        {
            ds = new DataSet();
            ds = objFactorySearch.getProvider(country_sk, state_sk, city_sk, area_sk, sp_category_sk, sp_sub_category_sk, massage_or_spa);
            return ds;
        }
        //get item menu of perticular provider
        public DataSet getProviderServices(Int32 provider_sk)
        {
            ds = new DataSet();
            ds = objFactorySearch.getProviderServices(provider_sk);
            return ds;
        }

        //added by nilesh g for insert quick booking details

        public DataSet i_quickbook(DateTime slot_date, int service_provider_sk, int service_seeker_sk, string service_provider_comment, string service_seeker_comment, char last_changed_by_ss_or_sp, char booked_by_ss_or_sp, char booking_status, char is_email_reminder_sent, char is_sms_reminder_sent, TimeSpan from_time, TimeSpan to_time, string orientation)
        {
            ds = new DataSet();
            ds = objFactorySearch.i_quickbook(slot_date, service_provider_sk, service_seeker_sk, service_provider_comment, service_seeker_comment, last_changed_by_ss_or_sp, booked_by_ss_or_sp, booking_status, is_email_reminder_sent, is_sms_reminder_sent, from_time, to_time, orientation);
            return ds;
        }


        public DataSet getSearchData_PaidRank(DateTime search_date, int? sp_category_sk, int? sp_sub_category_sk, int country_sk, int state_sk,
                   int city_sk, int? area_sk, string keyword, char sort_by, int service_seeker_sk, char? is_out_call,
                   char? massage_or_spa, string Orientation, string is_outcallType)
        {
            ds = new DataSet();
            ds = objFactorySearch.getSearchData_PaidRank(search_date, sp_category_sk, sp_sub_category_sk, country_sk, state_sk, city_sk, area_sk, keyword, sort_by, service_seeker_sk, is_out_call, massage_or_spa, Orientation, is_outcallType);
            return ds;
        }


        public DataSet getSpaSubType_By_Providersk(int service_provider_sk)
        {
            DataSet ds = new DataSet();
            ds = objFactorySearch.getSpaSubType_By_Providersk(service_provider_sk);
            return ds;
        }

        public DataSet getMax_price_By_Providersk(int service_provider_sk, int sp_sub_category_sk)
        {
            DataSet ds = new DataSet();
            ds = objFactorySearch.getMax_price_By_Providersk(service_provider_sk, sp_sub_category_sk);
            return ds;
        }


        public DataSet get_t_slab()
        {
            DataSet ds = new DataSet();
            ds = objFactorySearch.get_t_slab();
            return ds;
        }

        public DataSet getareasearchBYCityName(string city_state_country, string area, string fnbtype)
        {
            ds = new DataSet();
            ds = objFactorySearch.getareasearchBYCityName(city_state_country, area, fnbtype);
            return ds;
        }

        public DataSet get_massage_duration()
        {
            ds = new DataSet();
            ds = objFactorySearch.get_massage_duration( );
            return ds;
        }


        public DataSet Update_massage_duration(int id, string Price, string Duration)
        {
            ds = new DataSet();
            ds = objFactorySearch.Update_massage_duration(id,  Price,  Duration);
            return ds;
        
        
        }


        public DataSet Update_PIPmassage_duration(int id, string MassageName,  string Price, string Duration)
        {
            ds = new DataSet();
            ds = objFactorySearch.Update_PIPmassage_duration(id, MassageName, Price, Duration);
            return ds;
        }
    }
}

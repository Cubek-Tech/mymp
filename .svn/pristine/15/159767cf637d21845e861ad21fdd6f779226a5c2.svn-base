using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Data;
using System.Web;
namespace Factory
{
    public class FactorySearch
    {
        DataSet ds;
        private System.Nullable<int> nfnbtypesk
        {
            get { return null; }
        }
        private System.Nullable<int> nfnbSubType
        {
            get { return null; }
        }
        private System.Nullable<int> ncitysk
        {
            get { return null; }
        }
        private System.Nullable<int> nstatesk
        {
            get { return null; }
        }
        private System.Nullable<int> nseekersk
        {
            get { return null; }
        }
        private Nullable<System.Data.SqlTypes.SqlString> nkeyword
        {
            get { return null; }
        }


        private System.Nullable<int> ncountrysk
        {
            get { return null; }
        }


        #region new search method

        public DataSet getSearchData_1(DateTime search_date, int? sp_category_sk, int? sp_sub_category_sk, int country_sk, int state_sk,
                                  int city_sk, int? area_sk, string keyword, char sort_by, int service_seeker_sk, char? is_out_call,
                                  char? massage_or_spa, string Orientation)
        {
            ds = new DataSet();
            string Text = System.DateTime.Now.ToString();

            SqlConnection con = new SqlConnection(Config.Crebas);
            SqlCommand cmd = new SqlCommand();
            SqlParameter param = new SqlParameter();
            param = new SqlParameter();
            param.SqlDbType = SqlDbType.DateTime;
            param.ParameterName = "@search_date";
            param.Value = search_date;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@sp_category_sk";
            param.SqlDbType = SqlDbType.Int;
            param.Value = sp_category_sk == 0 ? nfnbtypesk : sp_category_sk;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);


            param = new SqlParameter();
            param.ParameterName = "@sp_sub_category_sk";
            param.SqlDbType = SqlDbType.Int;
            param.Value = sp_sub_category_sk == 0 ? nfnbSubType : sp_sub_category_sk;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@country_sk";
            param.SqlDbType = SqlDbType.Int;
            param.Value = country_sk;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);


            param = new SqlParameter();
            param.ParameterName = "@state_sk";
            param.SqlDbType = SqlDbType.Int;
            param.Value = state_sk == 0 ? nstatesk : state_sk;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@city_sk";
            param.SqlDbType = SqlDbType.Int;
            param.Value = city_sk == 0 ? ncitysk : city_sk; ;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@area_sk";
            param.SqlDbType = SqlDbType.Int;
            param.Value = area_sk;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@keyword";
            param.SqlDbType = SqlDbType.VarChar;
            param.Value = keyword == string.Empty ? nkeyword : keyword.ToLower().Trim();
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@sort_by";
            param.SqlDbType = SqlDbType.Char;
            param.Value = sort_by;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@service_seeker_sk";
            param.SqlDbType = SqlDbType.Int;
            param.Value = service_seeker_sk == 0 ? nseekersk : service_seeker_sk;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@is_out_call";
            param.SqlDbType = SqlDbType.Char;
            param.Value = is_out_call == 'N' ? null : is_out_call;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@massage_or_spa";
            param.SqlDbType = SqlDbType.Char;
            param.Value = massage_or_spa;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);


            param = new SqlParameter();
            param.ParameterName = "@orientation";
            param.SqlDbType = SqlDbType.VarChar;
            param.Value = Orientation;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);



            if (con.State != ConnectionState.Open) con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[p_g_sp_search_v2]";

            SqlDataReader dr;
            DataTable dt = new DataTable();
            //  Command time period  15 minute
            cmd.CommandTimeout = 900;
            dt.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection));
            //  dt.Load(dr);

            ds.Tables.Add(dt);


            // dr.Close();
            return ds;

            //ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_sp_search_v2",
            //    search_date,
            //    sp_category_sk == 0 ? nfnbtypesk : sp_category_sk,
            //    (sp_sub_category_sk == 0 ? nfnbSubType : sp_sub_category_sk),
            //    country_sk,
            //    (state_sk == 0 ? nstatesk : state_sk),
            //    (city_sk == 0 ? ncitysk : city_sk),
            //     area_sk,
            //    (keyword == string.Empty ? nkeyword : keyword.ToLower().Trim()),
            //    sort_by,
            //    (service_seeker_sk == 0 ? nseekersk : service_seeker_sk),
            //    is_out_call == 'N' ? null : is_out_call,
            //    massage_or_spa);


            //   return ds;
        }



        public DataSet getSearchData_12(DateTime search_date, int? sp_category_sk, int? sp_sub_category_sk, int country_sk, int state_sk,
                                  int city_sk, int? area_sk, string keyword, char sort_by, int service_seeker_sk, char? is_out_call,
                                  char? massage_or_spa, string Orientation, string is_outcallType)
         {
            ds = new DataSet();
            string Text = System.DateTime.Now.ToString();

            SqlConnection con = new SqlConnection(Config.Crebas);
            SqlCommand cmd = new SqlCommand();
            SqlParameter param = new SqlParameter();
            param = new SqlParameter();
            param.SqlDbType = SqlDbType.DateTime;
            param.ParameterName = "@search_date";
            param.Value = search_date;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@sp_category_sk";
            param.SqlDbType = SqlDbType.Int;
            param.Value = sp_category_sk == 0 ? nfnbtypesk : sp_category_sk;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);


            param = new SqlParameter();
            param.ParameterName = "@sp_sub_category_sk";
            param.SqlDbType = SqlDbType.Int;
            param.Value = sp_sub_category_sk == 0 ? nfnbSubType : sp_sub_category_sk;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@country_sk";
            param.SqlDbType = SqlDbType.Int;
            param.Value = country_sk;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);


            param = new SqlParameter();
            param.ParameterName = "@state_sk";
            param.SqlDbType = SqlDbType.Int;
            param.Value = state_sk == 0 ? nstatesk : state_sk;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@city_sk";
            param.SqlDbType = SqlDbType.Int;
            param.Value = city_sk == 0 ? ncitysk : city_sk; ;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@area_sk";
            param.SqlDbType = SqlDbType.Int;
            param.Value = area_sk;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@keyword";
            param.SqlDbType = SqlDbType.VarChar;
            param.Value = keyword == string.Empty ? nkeyword : keyword.ToLower().Trim();
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@sort_by";
            param.SqlDbType = SqlDbType.Char;
            param.Value = sort_by;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@service_seeker_sk";
            param.SqlDbType = SqlDbType.Int;
            param.Value = service_seeker_sk == 0 ? nseekersk : service_seeker_sk;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@is_out_call";
            param.SqlDbType = SqlDbType.Char;
            param.Value = is_out_call == 'N' ? null : is_out_call;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@massage_or_spa";
            param.SqlDbType = SqlDbType.Char;
            param.Value = massage_or_spa;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);


            param = new SqlParameter();
            param.ParameterName = "@orientation";
            param.SqlDbType = SqlDbType.VarChar;
            param.Value = Orientation;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);



            param = new SqlParameter();
            param.ParameterName = "@is_outcallType";
            param.SqlDbType = SqlDbType.VarChar;
            param.Value = is_outcallType;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            if (con.State != ConnectionState.Open) con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[p_g_sp_search_v2]";
            //cmd.CommandText = "[p_g_sp_search_v3]";


            SqlDataReader dr;
            DataTable dt = new DataTable();
            //  Command time period  15 minute
            cmd.CommandTimeout = 900;
            dt.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection));
            //  dt.Load(dr);

            ds.Tables.Add(dt);


            // dr.Close();
            return ds;

            //ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_sp_search_v2",
            //    search_date,
            //    sp_category_sk == 0 ? nfnbtypesk : sp_category_sk,
            //    (sp_sub_category_sk == 0 ? nfnbSubType : sp_sub_category_sk),
            //    country_sk,
            //    (state_sk == 0 ? nstatesk : state_sk),
            //    (city_sk == 0 ? ncitysk : city_sk),
            //     area_sk,
            //    (keyword == string.Empty ? nkeyword : keyword.ToLower().Trim()),
            //    sort_by,
            //    (service_seeker_sk == 0 ? nseekersk : service_seeker_sk),
            //    is_out_call == 'N' ? null : is_out_call,
            //    massage_or_spa);


            //   return ds;
        }


        public DataSet getSearchData_2(int service_seeker_sk, DataTable udtt_search_result, char mode)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(Config.Crebas);

            SqlCommand cmd = new SqlCommand();
            SqlParameter param = new SqlParameter();

            param = new SqlParameter();
            param.SqlDbType = SqlDbType.Int;
            param.ParameterName = "@service_seeker_sk";
            param.Value = service_seeker_sk;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@udtt_search_result";
            param.SqlDbType = SqlDbType.Structured;
            param.Value = udtt_search_result;
            param.TypeName = "[udtt_list_search_result]";
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);


            param = new SqlParameter();
            param.SqlDbType = SqlDbType.Char;
            param.ParameterName = "@mode";
            param.Value = mode;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);


            if (con.State != ConnectionState.Open) con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[p_g_sp_search_v2_detail]";

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;


        }



        #endregion



        #region Methods
        public DataSet getSearchData(DateTime search_date, int? sp_category_sk, int? sp_sub_category_sk, int country_sk, int state_sk,
                                    int city_sk, int? area_sk, string keyword, char sort_by, int service_seeker_sk, char? is_out_call,
                                    char? massage_or_spa, int row_start_from)
        {
            ds = new DataSet();


            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_sp_search",
                search_date,
                sp_category_sk == 0 ? nfnbtypesk : sp_category_sk,
                (sp_sub_category_sk == 0 ? nfnbSubType : sp_sub_category_sk),
                country_sk,
                (state_sk == 0 ? nstatesk : state_sk),
                (city_sk == 0 ? ncitysk : city_sk),
                 area_sk,
                (keyword == string.Empty ? nkeyword : keyword.ToLower().Trim()),
                sort_by,
                (service_seeker_sk == 0 ? nseekersk : service_seeker_sk),
                is_out_call == 'N' ? null : is_out_call,
                massage_or_spa,
                row_start_from,
                100);
            return ds;
        }


        public DataSet getSearchData_Check(DateTime search_date, int? sp_category_sk, int? sp_sub_category_sk, int country_sk, int state_sk,
                                    int city_sk, int? area_sk, string keyword, char sort_by, int service_seeker_sk, char? is_out_call,
                                    char? massage_or_spa)
        {


            ds = new DataSet();
            string Text = System.DateTime.Now.ToString();

            SqlConnection con = new SqlConnection(Config.Crebas);
            SqlCommand cmd = new SqlCommand();
            SqlParameter param = new SqlParameter();
            param = new SqlParameter();
            param.SqlDbType = SqlDbType.DateTime;
            param.ParameterName = "@search_date";
            param.Value = search_date;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@sp_category_sk";
            param.SqlDbType = SqlDbType.Int;
            param.Value = sp_category_sk == 0 ? nfnbtypesk : sp_category_sk;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);


            param = new SqlParameter();
            param.ParameterName = "@sp_sub_category_sk";
            param.SqlDbType = SqlDbType.Int;
            param.Value = sp_sub_category_sk == 0 ? nfnbSubType : sp_sub_category_sk;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@country_sk";
            param.SqlDbType = SqlDbType.Int;
            param.Value = country_sk;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);


            param = new SqlParameter();
            param.ParameterName = "@state_sk";
            param.SqlDbType = SqlDbType.Int;
            param.Value = state_sk == 0 ? nstatesk : state_sk;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@city_sk";
            param.SqlDbType = SqlDbType.Int;
            param.Value = city_sk == 0 ? ncitysk : city_sk; ;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@area_sk";
            param.SqlDbType = SqlDbType.Int;
            param.Value = area_sk;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@keyword";
            param.SqlDbType = SqlDbType.VarChar;
            param.Value = keyword == string.Empty ? nkeyword : keyword.ToLower().Trim();
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@sort_by";
            param.SqlDbType = SqlDbType.Char;
            param.Value = sort_by;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@service_seeker_sk";
            param.SqlDbType = SqlDbType.Int;
            param.Value = service_seeker_sk == 0 ? nseekersk : service_seeker_sk;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@is_out_call";
            param.SqlDbType = SqlDbType.Char;
            param.Value = is_out_call == 'N' ? null : is_out_call;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@massage_or_spa";
            param.SqlDbType = SqlDbType.Char;
            param.Value = massage_or_spa;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);


            if (con.State != ConnectionState.Open) con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[p_g_sp_search_check]";

            //SqlDataReader dr;
            DataTable dt = new DataTable();
            //  Command time period  15 minute
            cmd.CommandTimeout = 900;
            dt.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection));
            ds.Tables.Add(dt);
            return ds;

            //ds = new DataSet();
            //ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_sp_search_check",
            //    search_date,
            //    sp_category_sk == 0 ? nfnbtypesk : sp_category_sk,
            //    (sp_sub_category_sk == 0 ? nfnbSubType : sp_sub_category_sk),
            //    country_sk,
            //    (state_sk == 0 ? nstatesk : state_sk),
            //    (city_sk == 0 ? ncitysk : city_sk),
            //     area_sk,
            //    (keyword == string.Empty ? nkeyword : keyword.ToLower().Trim()),
            //    sort_by,
            //    (service_seeker_sk == 0 ? nseekersk : service_seeker_sk),
            //    is_out_call == 'N' ? null : is_out_call,
            //    massage_or_spa
            //  );
            //return ds;
        }




        public DataSet getareasearch(string city_state_country, string area, string fnbtype)
        {
            ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_area_location", city_state_country, area, fnbtype);
            return ds;
        }

        public DataSet getfoodtypesk(string city_state_country, string area, string fnbtype)
        {
            ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_area_location", city_state_country, area, fnbtype);
            return ds;
        }
        public DataSet getSearchDorpdownValues()
        {
            ds = new DataSet();
            // Update by akash 14-Dec-2011
            // rename  the sp 
            //ds = SqlHelper.ExecuteDataset(Config.Crebas, "s_sp_getSearchDorpdownValues"); 
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_getSearchDorpdownValues");
            return ds;
        }

        //public DataSet getServiceCountry(string mode)
        //{
        //    ds = new DataSet();
        //    // Update by akash 14-Dec-2011
        //    // rename  the sp 
        //    //ds = SqlHelper.ExecuteDataset(Config.Crebas, "s_sp_getSearchDorpdownValues");
        //    ds = SqlHelper.ExecuteDataset(Config.Crebas, "[p_g_get_service_type]", mode);
        //    return ds;
        //}
        public DataSet getDataFromProvider(int provider_id)
        {
            ds = new DataSet();
            // Update  by akash 14-Dec-2011
            // rename the sp
            // ds = SqlHelper.ExecuteDataset(Config.Crebas, "sp_getDataFromProvider", provider_id);
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_getDataFromProvider", provider_id);
            return ds;
        }
        public DataSet reviewRatingBySeekerForBooking(int seeker_sk, int provider_sk, DateTime booking_date)
        {
            ds = new DataSet();
            // Update  by akash 14-Dec-2011
            // rename the sp
            // ds = SqlHelper.ExecuteDataset(Config.Crebas, "sp_getDataFromProvider", provider_id);
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_review_rating_by_seeker_for_booking", seeker_sk, provider_sk, booking_date);
            return ds;
        }

        public DataSet getSeekerCalendar(int service_provider_sk, DateTime slot_date)
        {
            ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_seeker_sp_calendar", service_provider_sk, slot_date);
            return ds;
        }

        public DataTable getSearchCalendar(int Service_provider_sk, DateTime Slot_date)
        {
            DataTable dt = null;
            //spSerachCalendar
            // update by akash 14-Dec-2011
            // rename sp

            //DataSet ds = SqlHelper.ExecuteDataset(Config.Crebas, "spSerachCalendar", Service_provider_sk, Slot_date);
            DataSet ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_spSerachCalendar", Service_provider_sk, Slot_date);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            return dt;

        }

        public DataTable getSearchCalendarTable(int Service_provider_sk, DateTime Slot_date)
        {
            DataTable dt = null;
            //spSerachCalendar
            // update by akash 14-Dec-2011
            // change sp  name

            //DataSet ds=SqlHelper.ExecuteDataset(Config.Crebas,"sp_gslot",Service_provider_sk,Slot_date);
            DataSet ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_gslot", Service_provider_sk, System.Data.SqlTypes.SqlInt64.Null, Slot_date, "W");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }

            }
            return dt;

        }

        public DataTable getBusinessHour(int Service_provider_sk, DateTime Slot_date)
        {
            // find count business hour for a date
            DataTable dt = null;
            DataSet ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_gbusiness_hour", Service_provider_sk, Slot_date);

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }

            }
            return dt;


        }
        public bool IsRegstrationCompleted(int Service_provider_sk)
        {

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(Config.Crebas, "p_g_is_reg_completed", Service_provider_sk));
        }

        public DataTable GetScheduleTimeProviderDetails(int Service_provider_sk)
        {

            DataTable dt = null;
            DataSet ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_ScheduleTimeProviderDetails", Service_provider_sk);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            return dt;

        }


        public DataTable GetScheduleTimeProviderSlotTimeDetails(int Service_provider_sk, DateTime Slot_date)
        {

            DataTable dt = null;
            DataSet ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_ScheduleTimeProviderSlotTimeDetails", Service_provider_sk, Slot_date);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            return dt;

        }


        public DataTable GetServiceProviderQuestions(Int32 service_provider_sk, int? slot_booking_sk)
        {
            DataTable dt = null;
            DataSet ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_ServiceProviderQuestions", service_provider_sk, slot_booking_sk, "W");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            return dt;

        }



        /// <summary>
        /// Add to MyFevourate section
        /// Narendra 4/1/2012
        /// </summary>
        /// Start 
        public int InsertMyFevourite(Int32 service_seeker_sk, Int32 service_provider_sk, string comments, char is_blocked, char is_mailer)
        {
            int status = SqlHelper.ExecuteNonQuery(Config.Crebas, "p_iu_Fovourites", service_seeker_sk, service_provider_sk, comments, is_blocked, is_mailer);
            return status;
        }
        public int DeleteMyFevourite(Int32 service_seeker_sk, Int32 service_provider_sk)
        {
            int status = SqlHelper.ExecuteNonQuery(Config.Crebas, "p_iu_Fovourites", service_seeker_sk, service_provider_sk);
            return status;
        }
        public DataSet GetMyFevourite(Int32 service_seeker_sk, Int32 service_provider_sk)
        {
            DataSet ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_Fovourites", service_seeker_sk, service_provider_sk);
            return ds;
        }

        ///End

        //select Seeker booking detail

        public DataSet GetSeekerBookingDetail(Int32 service_seeker_sk, DateTime startdate, DateTime enddate)
        {
            DataSet ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_seekerBooking", service_seeker_sk, startdate, enddate);
            return ds;
        }

        public DataSet GetProviderBookingDetail(Int32 service_provider_sk, DateTime startdate, DateTime enddate)
        {
            DataSet ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_provider_booking", service_provider_sk, startdate, enddate);
            return ds;
        }




        public DataTable Reader_GetSeekerBookingDetail(Int32 service_seeker_sk, DateTime startdate, DateTime enddate)
        {
            DataSet dr;
            dr = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_seekerBooking", service_seeker_sk, startdate, enddate);
            return dr.Tables[0];
        }


        //select Fevourite Seeker  for outlet 
        //date 5/march/2012
        //narendra 
        public DataSet spFavouriteSeeker(Int32 service_provider_sk)
        {
            DataSet ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_favourite_seeker", service_provider_sk);
            return ds;
        }

        public int Delete_spFavouriteSeeker(Int32 sp_favourites_sk)
        {
            int status = SqlHelper.ExecuteNonQuery(Config.Crebas, "p_d_favourite_seeker", sp_favourites_sk);
            return status;
        }

        public int Update_ssFavouriteSeeker(Int32 sp_favourites_sk, string is_blocked)
        {
            int status = SqlHelper.ExecuteNonQuery(Config.Crebas, "p_u_favourite_seeker", sp_favourites_sk, is_blocked);
            return status;
        }


        //select Seeker Fevourite outlet list
        public DataSet ssFavouriteOutlet(Int32 service_seeker_sk)
        {
            DataSet ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_ssFavouriteOutlet", service_seeker_sk);
            return ds;
        }

        public int Delete_ssFavouriteOutlet(Int32 ss_favourites_sk)
        {
            int status = SqlHelper.ExecuteNonQuery(Config.Crebas, "p_d_seeker_favourites", ss_favourites_sk);
            return status;
        }
        public int Update_ssFavouriteOutlet(Int32 ss_favourites_sk, string is_mailer, string is_blocked)
        {
            int status = SqlHelper.ExecuteNonQuery(Config.Crebas, "p_iu_seeker_favourites", ss_favourites_sk, is_mailer, is_blocked);
            return status;
        }

        public int EditComments(Int32 ss_favourites_sk, string comments)
        {
            int status = SqlHelper.ExecuteNonQuery(Config.Crebas, "p_iu_seeker_favourites_comments", ss_favourites_sk, comments);
            return status;

        }


        public int SPEditComments(Int32 sp_favourites_sk, string comments)
        {
            int status = SqlHelper.ExecuteNonQuery(Config.Crebas, "p_iu_provider_favourites_comments", sp_favourites_sk, comments);
            return status;

        }


        /// <summary>
        /// Description: get customer contact detail to any preticular date. 
        /// create by:  pavan 1/march/2012
        /// </summary>

        public DataTable providerDayCustomer(int Service_provider_sk, DateTime Slot_date)
        {
            DataTable dt = null;
            DataSet ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_DayCustomer", Service_provider_sk, Slot_date);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            return dt;

        }

        /// <summary>
        /// Description: retrun to remaining no of slot for a day.
        /// create by: 22-june-2012 
        /// by: pavan
        /// </summary>

        public DataSet getRemainingSlot(int Service_provider_sk, DateTime Slot_date, DateTime? current_date)
        {
            // DataTable dt = null;
            DataSet ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_getRemainingSlot", Service_provider_sk, Slot_date, current_date);
            return ds;

        }



        public DataSet SeekerBookingByDate(string date, int service_seeker_sk, int service_provider_sk)
        {
            DataSet ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_seekerBooking_by_date", date, service_seeker_sk, service_provider_sk);
            return ds;
        }

        //added by nilesh g for quick booking details popup

        public DataSet SeekerQuickBookingByDate(string date, int service_seeker_sk, int service_provider_sk)
        {
            DataSet ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_quickbookdetails", date, service_seeker_sk, service_provider_sk);
            return ds;
        }
        //end here for quick book
        public DataSet ProviderBookingByDate(string date, int service_seeker_sk, int service_provider_sk)
        {
            DataSet ds = SqlHelper.ExecuteDataset(Config.Crebas, "P_g_providerbooking_by_date", date, service_seeker_sk, service_provider_sk);
            return ds;
        }



        /// <summary>
        /// insert counter details
        /// </summary>
        public int InsertHiCounter(DateTime hit_datetime, string hit_ip, int country_sk, int state_sk, int city_sk, char mode)
        {
            int status = SqlHelper.ExecuteNonQuery(Config.Crebas, "[p_i_hit_counter]", hit_datetime, hit_ip, country_sk, state_sk, city_sk, mode);
            return status;

        }

        //this is new hit counter with site refrence
        public int InsertHiCounter(string hit_ip, int country_sk, int state_sk, int city_sk, string site)
        {
            int status = SqlHelper.ExecuteNonQuery(Config.Crebas, "[p_i_hit_counter_new]", hit_ip, country_sk, state_sk, city_sk, site);
            return status;
        }



        #endregion
        public DataSet SelectCurrentTime()
        {
            DataSet ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_current_datetime");
            return ds;
        }
        /// <summary>
        ///Description: return homedelivery order list to any customer.
        /// create by: 05-Dec-2012 
        /// by: Avinash/pavan
        /// </summary>
        public DataSet DeliveryDetailbyseeker_sk(int seeker_sk)
        {
            ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_home_delivery_order_for_seeker", seeker_sk);
            return ds;
        }
        /// <summary>
        ///Description: return homedelivery order list to any provider.
        /// create by: 05-Dec-2012 
        /// by: Avinash/pavan
        /// </summary>
        public DataSet DeliveryDetailbyprovider_sk(int provider_sk)
        {
            ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_home_delivery_order_for_provider", provider_sk);
            return ds;
        }

        public DataSet ShowGlobalText(string type)
        {
            ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_headerstring", type);
            return ds;
        }
        //Asynronous objects

        public SqlCommand BeginExecution(DateTime currDate, int coutryid, int stateid, int cityid)
        {
            SqlParameter param = new SqlParameter();
            SqlCommand _command = new SqlCommand();

            param = new SqlParameter();
            param.SqlDbType = SqlDbType.Date;
            param.ParameterName = "@search_date";
            param.Value = currDate;
            param.Direction = ParameterDirection.Input;
            _command.Parameters.Add(param);

            param = new SqlParameter();
            param.SqlDbType = SqlDbType.Int;
            param.ParameterName = "@country_sk";
            param.Value = coutryid == 0 ? ncountrysk : coutryid;
            param.Direction = ParameterDirection.Input;
            _command.Parameters.Add(param);

            param = new SqlParameter();
            param.SqlDbType = SqlDbType.Int;
            param.ParameterName = "@state_sk";
            param.Value = stateid == 0 ? nstatesk : stateid;
            param.Direction = ParameterDirection.Input;
            _command.Parameters.Add(param);

            param = new SqlParameter();
            param.SqlDbType = SqlDbType.Int;
            param.ParameterName = "@city_sk";
            param.Value = cityid == 0 ? ncitysk : cityid;
            param.Direction = ParameterDirection.Input;
            _command.Parameters.Add(param);
            SqlConnection con = new SqlConnection(Config.Crebas);
            if (con.State != ConnectionState.Open) con.Open();
            _command.Connection = con;
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = "[p_g_promotiondetail_all]";

            return _command;


        }


        public DataSet getSpaType(string mode)
        {
            DataSet ds = new DataSet();

            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_get_service_type_search", "W", mode);
            return ds;
        }

        //to get spa sub type
        public DataSet getSpaSubType(int service_sk, string service_type)
        {
            DataSet ds = new DataSet();

            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_get_service_sub_type", service_sk, service_type);
            return ds;
        }

        public DataSet getServiceitem(string mode)
        {
            DataSet ds = new DataSet();

            ds = SqlHelper.ExecuteDataset(Config.Crebas, "get_service_items", mode);
            return ds;
        }
        public DataSet provider_by_search_url(string url)
        {
            DataSet ds = new DataSet();

            ds = SqlHelper.ExecuteDataset(Config.Crebas, "provider_by_search_url", url);

            return ds;
        }


        public DataSet Send_free_sms(string msg_text, string seeker_mob, int provider_sk, int seeker_sk)
        {
            DataSet ds = new DataSet();
            ds = ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_send_free_sms", msg_text, seeker_mob, provider_sk, seeker_sk);
            return ds;
        }



        public DataSet get_remaing_SMS(int seeker_sk, int provider_sk)
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_getSeekerRemainingSMS", seeker_sk, provider_sk);
            return ds;
        }


        public DataSet quick_book(string msg_text, string subject, int provider_sk, int seeker_sk, DateTime qdate, string service, string customertime, char mode)
        {
            DataSet ds = new DataSet();
            ds = ds = SqlHelper.ExecuteDataset(Config.Crebas, "[p_Send_QuickBookRequest]", msg_text, subject, provider_sk, seeker_sk, qdate, service, customertime, mode);
            return ds;
        }

        //for get provider for gift voucher
        public DataSet getProvider(int country_sk, int state_sk,
                                  int city_sk, Int32? area_sk, int? sp_category_sk, int? sp_sub_category_sk,
                                  char? massage_or_spa)
        {
            ds = new DataSet();
            string Text = System.DateTime.Now.ToString();

            SqlConnection con = new SqlConnection(Config.Crebas);
            SqlCommand cmd = new SqlCommand();
            SqlParameter param = new SqlParameter();
            param = new SqlParameter();

            param = new SqlParameter();
            param.ParameterName = "@country_sk";
            param.SqlDbType = SqlDbType.Int;
            param.Value = country_sk;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);


            param = new SqlParameter();
            param.ParameterName = "@state_sk";
            param.SqlDbType = SqlDbType.Int;
            param.Value = state_sk == 0 ? nstatesk : state_sk;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@city_sk";
            param.SqlDbType = SqlDbType.Int;
            param.Value = city_sk == 0 ? ncitysk : city_sk; ;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@area_sk";
            param.SqlDbType = SqlDbType.Int;
            param.Value = area_sk;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@service_sk";
            param.SqlDbType = SqlDbType.Int;
            param.Value = sp_category_sk == 0 ? nfnbtypesk : sp_category_sk;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);


            param = new SqlParameter();
            param.ParameterName = "@sub_service_sk";
            param.SqlDbType = SqlDbType.Int;
            param.Value = sp_sub_category_sk == 0 ? nfnbSubType : sp_sub_category_sk;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);





            param = new SqlParameter();
            param.ParameterName = "@massage_or_spa";
            param.SqlDbType = SqlDbType.Char;
            param.Value = massage_or_spa;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);


            if (con.State != ConnectionState.Open) con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[p_g_get_provider_for_giftvoucher]";

            SqlDataReader dr;
            DataTable dt = new DataTable();
            //  Command time period  15 minute
            cmd.CommandTimeout = 900;
            dt.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection));
            //  dt.Load(dr);

            ds.Tables.Add(dt);


            // dr.Close();
            return ds;


        }


        //for item menu of provider
        public DataSet getProviderServices(Int32 provider_sk)
        {
            DataSet ds = new DataSet();
            ds = ds = SqlHelper.ExecuteDataset(Config.Crebas, "[p_g_get_provider_services]", provider_sk);
            return ds;
        }


        //added by nilesh g for insert quick booking details
        public DataSet i_quickbook(DateTime slot_date, int service_provider_sk, int service_seeker_sk, string service_provider_comment, string service_seeker_comment, char last_changed_by_ss_or_sp, char booked_by_ss_or_sp, char booking_status, char is_email_reminder_sent, char is_sms_reminder_sent, TimeSpan from_time, TimeSpan to_time, string orientation)
        {
            DataSet ds = new DataSet();
            ds = ds = SqlHelper.ExecuteDataset(Config.Crebas, "[p_i_quickbookdetails]", slot_date, service_provider_sk, service_seeker_sk, service_provider_comment, service_seeker_comment, last_changed_by_ss_or_sp, booked_by_ss_or_sp, booking_status, is_email_reminder_sent, is_sms_reminder_sent, from_time, to_time, orientation);
            return ds;
        }


        public DataSet getSearchData_PaidRank(DateTime search_date, int? sp_category_sk, int? sp_sub_category_sk, int country_sk, int state_sk,
                            int city_sk, int? area_sk, string keyword, char sort_by, int service_seeker_sk, char? is_out_call,
                            char? massage_or_spa, string Orientation, string is_outcallType)
        {
            ds = new DataSet();
            string Text = System.DateTime.Now.ToString();

            SqlConnection con = new SqlConnection(Config.Crebas);
            SqlCommand cmd = new SqlCommand();
            SqlParameter param = new SqlParameter();
            param = new SqlParameter();
            param.SqlDbType = SqlDbType.DateTime;
            param.ParameterName = "@search_date";
            param.Value = search_date;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@sp_category_sk";
            param.SqlDbType = SqlDbType.Int;
            param.Value = sp_category_sk == 0 ? nfnbtypesk : sp_category_sk;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);


            param = new SqlParameter();
            param.ParameterName = "@sp_sub_category_sk";
            param.SqlDbType = SqlDbType.Int;
            param.Value = sp_sub_category_sk == 0 ? nfnbSubType : sp_sub_category_sk;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@country_sk";
            param.SqlDbType = SqlDbType.Int;
            param.Value = country_sk;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);


            param = new SqlParameter();
            param.ParameterName = "@state_sk";
            param.SqlDbType = SqlDbType.Int;
            param.Value = state_sk == 0 ? nstatesk : state_sk;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@city_sk";
            param.SqlDbType = SqlDbType.Int;
            param.Value = city_sk == 0 ? ncitysk : city_sk; ;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@area_sk";
            param.SqlDbType = SqlDbType.Int;
            param.Value = area_sk;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@keyword";
            param.SqlDbType = SqlDbType.VarChar;
            param.Value = keyword == string.Empty ? nkeyword : keyword.ToLower().Trim();
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@sort_by";
            param.SqlDbType = SqlDbType.Char;
            param.Value = sort_by;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@service_seeker_sk";
            param.SqlDbType = SqlDbType.Int;
            param.Value = service_seeker_sk == 0 ? nseekersk : service_seeker_sk;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@is_out_call";
            param.SqlDbType = SqlDbType.Char;
            param.Value = is_out_call == 'N' ? null : is_out_call;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@massage_or_spa";
            param.SqlDbType = SqlDbType.Char;
            param.Value = massage_or_spa;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);


            param = new SqlParameter();
            param.ParameterName = "@orientation";
            param.SqlDbType = SqlDbType.VarChar;
            param.Value = Orientation;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);



            param = new SqlParameter();
            param.ParameterName = "@is_outcallType";
            param.SqlDbType = SqlDbType.VarChar;
            param.Value = is_outcallType;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            if (con.State != ConnectionState.Open) con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "[p_g_sp_search_v2]";
            cmd.CommandText = "[p_g_sp_search_v3]";


            SqlDataReader dr;
            DataTable dt = new DataTable();
            //  Command time period  15 minute
            cmd.CommandTimeout = 900;
            dt.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection));

            ds.Tables.Add(dt);
            return ds;


        }




        public DataSet getSpaSubType_By_Providersk(int service_provider_sk)
        {
            DataSet ds = new DataSet();

            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_get_service_sub_type_SP", service_provider_sk);
            return ds;
        }

        public DataSet getMax_price_By_Providersk(int service_provider_sk, int sp_sub_category_sk)
        {
            DataSet ds = new DataSet();

            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_get_Max_price_by_SP", service_provider_sk, sp_sub_category_sk);
            return ds;
        }


        public DataSet get_t_slab()
        {
            DataSet ds = new DataSet();

            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_get_t_slab");
            return ds;
        }


        public DataSet getareasearchBYCityName(string city_state_country, string area, string fnbtype)
        {
            ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_area_location_byCity", city_state_country, area, fnbtype);
            return ds;
        }
        public DataSet get_massage_duration()
        {
            ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "sp_get_massage_duration");
            return ds;
        }
        public DataSet Update_massage_duration(int id, string Price, string Duration)
        {
            ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "sp_ui_massage_type_duration", id,  Price,  Duration);
            return ds;
        }

        public DataSet Update_PIPmassage_duration(int id, string MassageName, string Price, string Duration)
        {
            ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "sp_ui_PIP_massage_type_duration", id, MassageName, Price, Duration);
            return ds;
        }
    }
}

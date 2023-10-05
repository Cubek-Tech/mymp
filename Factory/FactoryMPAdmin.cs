using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;

namespace Factory
{
    public class FactoryMPAdmin
    {
        private System.Nullable<int> ncitysk
        {
            get { return null; }
        }
        private System.Nullable<int> nstatesk
        {
            get { return null; }
        }
        public DataTable get_no_of_registration(string date, int country_sk, int? state_sk, int? city_sk)
        {
            string Text = System.DateTime.Now.ToString();

            SqlConnection con = new SqlConnection(Config.Crebas);
            SqlCommand cmd = new SqlCommand();
            SqlParameter param = new SqlParameter();

            param = new SqlParameter();
            param.ParameterName = "@date";
            param.SqlDbType = SqlDbType.VarChar;
            param.Value = date;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@country_sk";
            param.SqlDbType = SqlDbType.Int;
            param.Value = country_sk;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            if (state_sk != null && state_sk != 0)
            {
                param = new SqlParameter();
                param.ParameterName = "@state_sk";
                param.SqlDbType = SqlDbType.Int;
                param.Value = state_sk == 0 ? nstatesk : state_sk;
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);
            }
            if (city_sk != null && city_sk != 0)
            {
                param = new SqlParameter();
                param.ParameterName = "@city_sk";
                param.SqlDbType = SqlDbType.Int;
                param.Value = city_sk == 0 ? ncitysk : city_sk;
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);
            }
            if (con.State != ConnectionState.Open) con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[usp_check_no_registrations]";
            DataTable dt = new DataTable();
            //  Command time period  15 minute
            cmd.CommandTimeout = 900;
            dt.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection));
            return dt;
        }
        public DataSet check_membership(string email_id)
        {
            DataSet ds;
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "usp_check_membership", email_id);
            return ds;
        }
        public int insert_membership(string email_id, int year)
        {
            int i = 0;
            SqlHelper.ExecuteNonQuery(Config.Crebas, "sp_insert_subscription", email_id, year);
            return i;
        }
        public int Activate_membership(string email_id, string body)
        {
            int i = 0;
            SqlHelper.ExecuteNonQuery(Config.Crebas, "usp_activate_membership", email_id,body);
            return i;
        }
        public void insert_paytm(string image_Name, string title)
        {
            SqlHelper.ExecuteNonQuery(Config.Crebas, "sp_insert_paytm_qr", image_Name, title);
        }
        public void update_paytm_default(int id)
        {
            SqlHelper.ExecuteNonQuery(Config.Crebas, "sp_default_update", id);
        }
        public DataSet get_payment_gateways()
        {
            DataSet ds = SqlHelper.ExecuteDataset(Config.Crebas, "dbo.p_g_gateways");
            return ds;
        }
        public int update_countries_gatways(string gateway_sk, DataTable dt)
        {
            SqlConnection con = new SqlConnection(Config.Crebas);
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlParameter param = new SqlParameter();

                param = new SqlParameter();
                param.ParameterName = "@udt_countries";
                param.SqlDbType = SqlDbType.Structured;
                param.Value = dt;
                param.TypeName = "udt_countries";
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.SqlDbType = SqlDbType.Int;
                param.ParameterName = "@gateway_sk";
                param.Value = gateway_sk;
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                if (con.State != ConnectionState.Open) con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.p_u_p_gatways";
                int result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { if (con.State != ConnectionState.Closed) con.Close(); }
            return 1;
        }

        public DataSet hide_show_countries_gatways(string gateway_sk, string country_sk)
        {
            DataSet ds = SqlHelper.ExecuteDataset(Config.Crebas, "dbo.p_i_user_countrywise_gateway", gateway_sk, country_sk);
            return ds;
        }

        public DataSet get_default_gateway()
        {
            DataSet ds = SqlHelper.ExecuteDataset(Config.Crebas,"dbo.p_g_default_payment");
            return ds;
        }
        public int update_default_gateway(string value)
        {
            int i = SqlHelper.ExecuteNonQuery(Config.Crebas,"dbo_p_u_default_gateway",value);
            return 1;
        }
        //Dynamic Percent Changing
        public DataSet seeker_subscription_price_bycountry(string country_sk)
        {
            DataSet ds = SqlHelper.ExecuteDataset(Config.Crebas, "dbo.p_g_seeker_subscription_price_byCountry", country_sk);
            return ds;
        }
        public void insert_update_seeker_subscription_price_bycountry(string country_sk, string one_year, string two_year, string three_year)
        {
            SqlHelper.ExecuteNonQuery(Config.Crebas, "dbo.p_i_u_membership_rates", country_sk, one_year, two_year, three_year);
        }
        public DataSet get_seeker_subscription_price_bycountry()
        {
            DataSet ds = SqlHelper.ExecuteDataset(Config.Crebas, "dbo.p_g_seeker_memberships_prices_country_wise");
            return ds;
        }
        public DataSet get_country_currency_details(string country_sk)
        {
            DataSet ds = SqlHelper.ExecuteDataset(Config.Crebas, "dbo.get_country_currency_details", country_sk);
            return ds;
        }
        public void insert_seeker_currency_subscription(string country_sk, string unit_price, string rate)
        {
            SqlHelper.ExecuteNonQuery(Config.Crebas, "dbo.p_i_seeker_currency_subscription", country_sk, unit_price, rate);
        }
        public DataSet delete_partner_by_emailid(string email_id)
        {
            DataSet ds = SqlHelper.ExecuteDataset(Config.Crebas, "dbo.sp_delete_email_id", email_id);
            return ds;
        }
        //Add dynamically payment parameters
        public DataSet get_payment_parameters()
        {
            DataSet ds = SqlHelper.ExecuteDataset(Config.Crebas, "dbo.p_g_payment_parameters");
            return ds;
        }
        public DataSet update_payment_parameters(string paytm1)
        {
            DataSet ds = SqlHelper.ExecuteDataset(Config.Crebas, "dbo.p_u_payment_parameters", paytm1);
            return ds;
        }

        public DataSet getadminMessages(string type, string date, string by_partner_sk, string to_partner_sk,string is_paid)
        {
            DataSet ds = SqlHelper.ExecuteDataset(Config.Crebas, "dbo.p_g_msg_users", type,date,by_partner_sk,to_partner_sk,is_paid);
            return ds;
        }
    }
}

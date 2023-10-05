using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;

namespace Factory
{
    public class FactorySubscription
    {

        public DataSet getProviderSubscription(int service_provider_sk, int Country_sk)
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_sp_subscription", service_provider_sk, Country_sk);
            return ds;
        }

        public DataSet getAllSubscription(int Country_sk)
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_subscription", Country_sk);
            return ds;
        }

        public int setDetails(int service_provider_sk, DataTable dt_subs)
        {

            SqlConnection con = new SqlConnection(Config.Crebas);
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlParameter param = new SqlParameter();

                param = new SqlParameter();
                param.ParameterName = "@utt_subscription";
                param.SqlDbType = SqlDbType.Structured;
                param.Value = dt_subs;
                param.TypeName = "dbo.udtt_subscription";
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.SqlDbType = SqlDbType.Int;
                param.ParameterName = "@service_provider_sk";
                param.Value = service_provider_sk;
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                if (con.State != ConnectionState.Open) con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "p_iu_subscription";
                int result = cmd.ExecuteNonQuery();


            }

            catch (Exception ex)
            {

                // throw ex;
                return 0;
            }
            finally { if (con.State != ConnectionState.Closed) con.Close(); }

            return 1;
        }

        public int UpdateSubscription(DataTable dt_subs)
        {

            SqlConnection con = new SqlConnection(Config.Crebas);
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlParameter param = new SqlParameter();

                param = new SqlParameter();
                param.ParameterName = "@utt_subscription";
                param.SqlDbType = SqlDbType.Structured;
                param.Value = dt_subs;
                param.TypeName = "dbo.udtt_subscription";
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                if (con.State != ConnectionState.Open) con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "p_u_sp_subscription";
                int result = cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {

                // throw ex;
                return 0;
            }
            finally { if (con.State != ConnectionState.Closed) con.Close(); }

            return 1;
        }

        //this is used for unsubscribe the plan
        public DataSet UnSubscription(int service_provider_sk, int Country_sk, int subscription_sk)
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_u_subscription", service_provider_sk, Country_sk, subscription_sk);
            return ds;
        }  ////****************************************************************8
        ///new block for buy promotion
        ///***********************************************by narendra**********
        public DataSet getProviderCountryName(int service_provider_sk)
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_getProviderCountryName", service_provider_sk);
            return ds;
        }

        public DataSet getsubscriptiondetails(int country_sk)
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_subscription_details", country_sk);
            return ds;
        }
        public DataSet get_coupon_details(string coupon_code)
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_coupon_details", coupon_code);
            return ds;
        }
        public DataSet get_subscription_to_provider(int service_provider_sk, string Subscription_name)
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_subscription_to_provider", service_provider_sk, Subscription_name);
            return ds;
        }
        public DataSet get_subscription_to_multiple_provider(int institution_sk)          //added by nilesh fro temprary solution of multiple parlorpromotion
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_subscription_to_multiprovider", institution_sk);
            return ds;
        }

        public DataSet InsertSeekerSubscription(DataTable dt_subs)
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_iu_Subscribe_promotion",
                                        dt_subs.Rows[0]["service_provider_sk"].ToString(),
                                        dt_subs.Rows[0]["country_sk"].ToString(),
                                        dt_subs.Rows[0]["subscription_sk"].ToString(),
                                        dt_subs.Rows[0]["subscription_qty"].ToString(),
                                    dt_subs.Rows[0]["subscription_start_date"],
                                     dt_subs.Rows[0]["subscription_end_date"],
                                        dt_subs.Rows[0]["one_time_price"].ToString(),
                                        dt_subs.Rows[0]["is_subscribed"].ToString()
                                        );

            return ds;
        }


        public DataSet InsertPromotionDetails_Seeker(DataTable dt_subs)
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_iu_subscribe_seeker",
                                        Convert.ToInt32(dt_subs.Rows[0]["service_seeker_sk"]),
                                        dt_subs.Rows[0]["subscription_start_date"],
                                        dt_subs.Rows[0]["subscription_end_date"],
                                        dt_subs.Rows[0]["one_time_price"].ToString(),
                                        dt_subs.Rows[0]["is_subscribed"].ToString(),
                                        dt_subs.Rows[0]["is_paid"].ToString(),
                                        dt_subs.Rows[0]["paymentGateway"].ToString()
                                        );

            return ds;
        }

        public DataSet InsertPromotionDetails(DataTable dt_subs, string Subscription_name, int? provider_subscription_sk)
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_iu_Subscribe_promotion",
                                        dt_subs.Rows[0]["service_provider_sk"].ToString(),
                                        dt_subs.Rows[0]["country_sk"].ToString(),
                //dt_subs.Rows[0]["subscription_sk"].ToString(),
                // dt_subs.Rows[0]["subscription_qty"].ToString(),
                                    dt_subs.Rows[0]["subscription_start_date"],
                                     dt_subs.Rows[0]["subscription_end_date"],
                                        dt_subs.Rows[0]["one_time_price"].ToString(),
                                        dt_subs.Rows[0]["is_subscribed"].ToString(),
                                        dt_subs.Rows[0]["is_paid"].ToString(),
                                        dt_subs.Rows[0]["PaymentGateway"].ToString(),
                                        Subscription_name, provider_subscription_sk
                                        );

            return ds;
        }

        public DataSet InsertProviderRegistrationPaymentDetails(DataTable dt_subs)
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_iu_Registration_subscribe_provider",
                                        dt_subs.Rows[0]["service_provider_sk"].ToString(),
                                        dt_subs.Rows[0]["country_sk"].ToString(),
                                    dt_subs.Rows[0]["subscription_start_date"],
                                     dt_subs.Rows[0]["subscription_end_date"],
                                        dt_subs.Rows[0]["one_time_price"].ToString(),
                                        dt_subs.Rows[0]["is_subscribed"].ToString(),
                                         dt_subs.Rows[0]["is_paid"].ToString(),
                                          dt_subs.Rows[0]["PaymentGateway"].ToString()
                                        );

            return ds;
        }
        //added by nilesh g for topten details
        public DataSet InsertTopTenDetails(DataTable dt_subs)
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_i_topten_details",
                                        Convert.ToInt32((dt_subs.Rows[0]["top_ten_service_sk"]).ToString()),
                                       Convert.ToInt32((dt_subs.Rows[0]["service_provider_sk"]).ToString()),
                                          dt_subs.Rows[0]["sp_description"].ToString(),
                                        Convert.ToInt32((dt_subs.Rows[0]["country_sk"]).ToString()),
                                        dt_subs.Rows[0]["state_sk"] != null ? Convert.ToInt32(dt_subs.Rows[0]["state_sk"]) : 0,
                                        dt_subs.Rows[0]["city_sk"] != null ? Convert.ToInt32(dt_subs.Rows[0]["city_sk"]) : 0,
                                        dt_subs.Rows[0]["is_paid_monthly"].ToString(),
                                        dt_subs.Rows[0]["is_paid_quaterly"].ToString(),
                                        dt_subs.Rows[0]["is_paid_yearly"].ToString(),

                                        dt_subs.Rows[0]["amount_monthly"] != null && (dt_subs.Rows[0]["amount_monthly"]).ToString() != "" ? Convert.ToDecimal(dt_subs.Rows[0]["amount_monthly"].ToString()) : 0,
                                        dt_subs.Rows[0]["amount_quaterly"] != null && (dt_subs.Rows[0]["amount_quaterly"]).ToString() != "" ? Convert.ToDecimal(dt_subs.Rows[0]["amount_quaterly"].ToString()) : 0,
                                        dt_subs.Rows[0]["amount_yearly"] != null && (dt_subs.Rows[0]["amount_yearly"]).ToString() != "" ? Convert.ToDecimal(dt_subs.Rows[0]["amount_yearly"].ToString()) : 0,


                                     Convert.ToDateTime(dt_subs.Rows[0]["subscription_start_date"].ToString()),
                                       Convert.ToDateTime(dt_subs.Rows[0]["subscription_end_date"].ToString()),
                                       dt_subs.Rows[0]["totalprice"] != null ? Convert.ToDecimal(dt_subs.Rows[0]["totalprice"].ToString()) : 0

                                        );

            return ds;
        }

        //end here for top ten 

        //public DataSet getCurrencyDetails(int service_provider_sk)
        //{

        //    DataSet ds = new DataSet();
        //    ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_provider_institution_country", service_provider_sk);
        //    return ds;
        //}

        //public int getInstitusion_by_provider_sk(int service_provider_sk)
        //{

        //    var sk = SqlHelper.ExecuteScalar(Config.Crebas, "p_g_institusion_by_provider_sk", service_provider_sk);
        //    return Convert.ToInt32(sk);
        //}


        //public int getprovider_by_institution(int intitution_sk)
        //{
        //    var sk = SqlHelper.ExecuteScalar(Config.Crebas, "p_g_provider_sk_by_institusion_sk", intitution_sk);
        //    return Convert.ToInt32(sk);
        //}

        public DataSet InsertProviderBuyLeadsPaymentDetails(DataTable dt_subs, int? provider_subscription_sk)
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_iu_BuyLeads_subscribe_provider",
                                        dt_subs.Rows[0]["service_provider_sk"].ToString(),
                                        dt_subs.Rows[0]["country_sk"].ToString(),
                                    dt_subs.Rows[0]["subscription_start_date"],
                                     dt_subs.Rows[0]["subscription_end_date"],
                                        dt_subs.Rows[0]["one_time_price"].ToString(),
                                        dt_subs.Rows[0]["is_subscribed"].ToString(),
                                         dt_subs.Rows[0]["is_paid"].ToString(),
                                          dt_subs.Rows[0]["PaymentGateway"].ToString()
                                          , provider_subscription_sk
                                        );

            return ds;
        }




        public int Insert_Buied_Seeker_Leads(int sp_sk, int subscription_SK, DataTable Seeker_data)
        {
            SqlConnection con = new SqlConnection(Config.Crebas);
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlParameter param = new SqlParameter();



                param = new SqlParameter();
                param.SqlDbType = SqlDbType.Int;
                param.ParameterName = "@service_provider_sk";
                param.Value = sp_sk;
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);


                param = new SqlParameter();
                param.SqlDbType = SqlDbType.Int;
                param.ParameterName = "@provider_subscription_sk";
                param.Value = subscription_SK;
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);


                param = new SqlParameter();
                param.ParameterName = "@udtt_service_seeker_sk";
                param.SqlDbType = SqlDbType.Structured;
                param.Value = Seeker_data;
                param.TypeName = "dbo.UDTT_LIST_SERVICE_SEEKER_SK";
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);



                if (con.State != ConnectionState.Open) con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "p_i_leads_buy";
                int result = cmd.ExecuteNonQuery();


            }

            catch (Exception ex)
            {

                // throw ex;
                return 0;
            }
            finally { if (con.State != ConnectionState.Closed) con.Close(); }

            return 1;
        }


        public DataSet InsertPromotionDetails(DataTable dt_subs, string Subscription_name, int provider_subscription_sk)
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_iu_Subscribe_promotion",
                                        dt_subs.Rows[0]["service_provider_sk"].ToString(),
                                        dt_subs.Rows[0]["country_sk"].ToString(),
                //dt_subs.Rows[0]["subscription_sk"].ToString(),
                // dt_subs.Rows[0]["subscription_qty"].ToString(),
                                    dt_subs.Rows[0]["subscription_start_date"],
                                     dt_subs.Rows[0]["subscription_end_date"],
                                        dt_subs.Rows[0]["one_time_price"].ToString(),
                                        dt_subs.Rows[0]["is_subscribed"].ToString(),
                                        dt_subs.Rows[0]["is_paid"].ToString(),
                                        dt_subs.Rows[0]["PaymentGateway"].ToString(),
                                        Subscription_name, provider_subscription_sk
                                        );

            return ds;
        }



        public DataSet InsertProviderBuyRankPaymentDetails(DataTable dt_subs, int provider_subscription_sk)
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_iu_BuyRank_subscribe_provider",
                                        dt_subs.Rows[0]["service_provider_sk"].ToString(),
                                        dt_subs.Rows[0]["country_sk"].ToString(),
                                    dt_subs.Rows[0]["subscription_start_date"],
                                     dt_subs.Rows[0]["subscription_end_date"],
                                        dt_subs.Rows[0]["one_time_price"].ToString(),
                                        dt_subs.Rows[0]["is_subscribed"].ToString(),
                                         dt_subs.Rows[0]["is_paid"].ToString(),
                                          dt_subs.Rows[0]["PaymentGateway"].ToString()
                                          , provider_subscription_sk,
                                            dt_subs.Rows[0]["Rank"].ToString(),
                                              dt_subs.Rows[0]["Sub_service_sk"].ToString()
                                        );

            return ds;
        }

        public DataSet a(int Service_provider_sk, int Provider_purchased_rank, int Sub_service_sk, string Providers_rank_string)
        {

            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_ui_rank_position", Service_provider_sk, Provider_purchased_rank, Sub_service_sk, Providers_rank_string.ToString());
            return ds;
        }

        public DataSet Update_paid_rank_position(int Service_provider_sk, int Provider_purchased_rank, int Sub_service_sk, string Providers_rank_string)
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_ui_rank_position", Service_provider_sk, Provider_purchased_rank, Sub_service_sk, Providers_rank_string.ToString());
            return ds;
        }









    }
}


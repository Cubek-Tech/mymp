using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Factory;
using System.Data;

namespace Business
{
    public class BussinessSubscription
    {

        FactorySubscription _subs = new FactorySubscription();
        //public int service_provider_sk { get; set; }
        //public int country_sk { get; set; }
        //public int subscription_sk { get; set; }
        //public int subscription_qty { get; set; }
        //public DateTime subscription_start_date { get; set; }
        //public DateTime subscription_end_date { get; set; }
        //public Decimal one_time_price { get; set; }

        public DataSet getProviderSubscription(int service_provider_sk, int Country_sk)
        {
            return _subs.getProviderSubscription(service_provider_sk, Country_sk);
        }

        public DataSet getAllSubscription(int Country_sk)
        {
            return _subs.getAllSubscription(Country_sk);
        }

        public int setDetails(int service_provider_sk, DataTable dt_subs)
        {
            return _subs.setDetails(service_provider_sk, dt_subs);
        }


        public int UpdateSubscription(DataTable dt_subs)
        {
            return _subs.UpdateSubscription(dt_subs);
        }

        public DataSet UnSubscription(int service_provider_sk, int Country_sk, int subscription_sk)
        {
            return _subs.UnSubscription(service_provider_sk, Country_sk, subscription_sk);
        }   ////****************************************************************8
        ///new block for buy promotion
        ///***********************************************by narendra**********
        public DataSet getProviderCountryName(int service_provider_sk)
        {
            return _subs.getProviderCountryName(service_provider_sk);

        }
        public DataSet getsubscriptiondetails(int country_sk)
        {

            return _subs.getsubscriptiondetails(country_sk);
        }
        public DataSet get_coupon_details(string coupon_code)
        {

            return _subs.get_coupon_details(coupon_code);
        }

        public DataSet get_subscription_to_provider(int service_provider_sk, string Subscription_name)
        {
            return _subs.get_subscription_to_provider(service_provider_sk, Subscription_name);
        }
        public DataSet get_subscription_to_multiple_provider(int institution_sk)  //added by nilesh fro temprary solution of multiple parlorpromotion
        {
            return _subs.get_subscription_to_multiple_provider(institution_sk);
        }


        public DataSet InsertPromotionDetails_Seeker(DataTable dt_subs)
        {

            return _subs.InsertPromotionDetails_Seeker(dt_subs);
        }

        public DataSet InsertPromotionDetails(DataTable dt_subs, string Subscription_name,int? provider_subscription_sk)
        {

            return _subs.InsertPromotionDetails(dt_subs, Subscription_name, provider_subscription_sk);
        }


        public DataSet InsertProviderRegistrationPaymentDetails(DataTable dt_subs)
        {

            return _subs.InsertProviderRegistrationPaymentDetails(dt_subs);
        }
        public DataSet InsertSeekerSubscription(DataTable dt_subs)
        {

            return _subs.InsertSeekerSubscription(dt_subs);
        }


        //added by nilesh g for insertion topten
        public DataSet InsertTopTenDetails(DataTable dt_subs)
        {

            return _subs.InsertTopTenDetails(dt_subs);
        }
        //end here for topten 
        public DataSet InsertProviderBuyLeadsPaymentDetails(DataTable dt_subs, int? provider_subscription_sk)
        {

            return _subs.InsertProviderBuyLeadsPaymentDetails(dt_subs, provider_subscription_sk);
        }
        public int Insert_Buied_Seeker_Leads(int sp_sk, int subscription_SK, DataTable Seeker_data)
        {
            return _subs.Insert_Buied_Seeker_Leads(sp_sk, subscription_SK, Seeker_data);
        }

        public DataSet InsertProviderBuyRankPaymentDetails(DataTable dt_subs, int provider_subscription_sk)
        {

            return _subs.InsertProviderBuyRankPaymentDetails(dt_subs, provider_subscription_sk);
        }

     
          public DataSet Update_paid_rank_position(int Service_provider_sk, int Provider_purchased_rank, int Sub_service_sk, string Providers_rank_string)
        {
            return _subs.Update_paid_rank_position(Service_provider_sk, Provider_purchased_rank, Sub_service_sk, Providers_rank_string);
        }
    }
}

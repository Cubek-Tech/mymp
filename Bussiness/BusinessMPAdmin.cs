using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Factory;
namespace Business
{
    public class BusinessMPAdmin
    {
        FactoryMPAdmin objadmin = new FactoryMPAdmin();
        public DataTable get_no_of_registration(string date, int country_sk, int? state_sk, int? city_sk)
        {
            DataTable ds = new DataTable();
            ds = objadmin.get_no_of_registration(date, country_sk, state_sk, city_sk);
            return ds;
        }
        public DataSet check_membership(string email_id)
        {
            DataSet ds;
            ds = objadmin.check_membership(email_id);
            return ds;
        }
        public int insert_membership(string email_id, int year)
        {
            int i = 0;
            i = objadmin.insert_membership(email_id, year);
            return i;
        }
        public int Activate_membership(string email_id, string body)
        {
            int i = 0;
            i = objadmin.Activate_membership(email_id, body);
            return i;
        }
        public void insert_paytm(string image_Name, string title)
        {
            objadmin.insert_paytm(image_Name, title);
        }
        public void update_paytm_default(int id)
        {
            objadmin.update_paytm_default(id);
        }
        public DataSet get_payment_gateways()
        {
            return objadmin.get_payment_gateways();
        }
        public int update_countries_gatways(string gateway_sk, DataTable dt)
        {
            return objadmin.update_countries_gatways(gateway_sk, dt);
        }

        public DataSet hide_show_countries_gatways(string gateway_sk, string country_sk)
        {
            return objadmin.hide_show_countries_gatways(gateway_sk, country_sk);
        }

        public DataSet get_default_gateway()
        {
            return objadmin.get_default_gateway();
        }
        public int update_default_gateway(string value)
        {
            return objadmin.update_default_gateway(value);
        }
        //Dynamic Percent Changing
        public DataSet seeker_subscription_price_bycountry(string country_sk)
        {
            return objadmin.seeker_subscription_price_bycountry(country_sk);

        }
        public void insert_update_seeker_subscription_price_bycountry(string country_sk, string one_year, string two_year, string three_year)
        {
            objadmin.insert_update_seeker_subscription_price_bycountry(country_sk, one_year, two_year, three_year);
        }
        public DataSet get_seeker_subscription_price_bycountry()
        {
            return objadmin.get_seeker_subscription_price_bycountry();
        }
        public void insert_seeker_currency_subscription(string country_sk, string unit_price, string rate)
        {
            objadmin.insert_seeker_currency_subscription(country_sk, unit_price, rate);
        }
        public DataSet get_country_currency_details(string country_sk)
        {
            return objadmin.get_country_currency_details(country_sk);
        }
        public DataSet delete_partner_by_emailid(string email_id)
        {
            return objadmin.delete_partner_by_emailid(email_id);
        }
        public DataSet get_payment_parameters()
        {
            return objadmin.get_payment_parameters();
        }
        public DataSet update_payment_parameters(string paytm1)
        {
            return objadmin.update_payment_parameters(paytm1);
        }
        public DataSet getadminMessages(string type, string date, string by_partner_sk, string to_partner_sk, string is_paid)
        {
            return objadmin.getadminMessages(type,date,by_partner_sk,to_partner_sk,is_paid);
        }
    }
}

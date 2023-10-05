using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;

namespace Factory
{
    public class FactoryLogin
    {
        public DataSet checkLoginInfo(string email, string password, char mode)
        {

            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_checkLogin", email, password, "W", mode);
            return ds;


        }
        public DataSet getProviderId(Int32 login_sk, Int32 flag)
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_getProviderId", login_sk, flag);
            return ds;
        }

        public DataSet getSeekerData(Int32 seeker_id)
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_getSeekerData", seeker_id);
            return ds;
        }
        //public int setChangePassword(Int32 login_sk, string password)
        public int setChangePassword(Int32 login_sk, string password, string currentpassword)
        {
            int i = 0;
            i = SqlHelper.ExecuteNonQuery(Config.Crebas, "p_u_setChangePassword", login_sk, password, currentpassword);
            return i;
        }
        public int setChangeEmail(Int32 login_sk, string email_id)
        {
            int i = 0;
            i = SqlHelper.ExecuteNonQuery(Config.Crebas, "p_u_setChangeEmail", login_sk, email_id);
            return i;
        }
        public DataSet GetQueForgetPwd(string email)
        {
            /// create pavan shrivastava.
            /// get question for Email Id
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_ForgetPassword", email);
            return ds;
        }
        /// <summary>
        /// Descrption: get if already customer booked slots to any perticular date. 
        /// create pavan 
        /// </summary>       
        public DataSet ChkFavouriteCustomerBooking(Int32 seeker_id, Int32 provide_sk, DateTime slot_date)
        {
            DataSet ds = new DataSet();
            // ds = objFactoryLogin.ChkFavouriteCustomerBooking(seeker_id,provide_sk,slot_date);
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_ChkFavouriteCustomerBooking", seeker_id, provide_sk, slot_date);
            return ds;
        }


        public DataSet IsLoginExist(string strEmail)
        {
            return SqlHelper.ExecuteDataset(Config.Crebas, "IsEmailIdExist", strEmail);
        }
        public DataSet Is_any_emailId_exist(string strEmail)
        {
            return SqlHelper.ExecuteDataset(Config.Crebas, "is_any_emailId_exist", strEmail);
        }


        public DataSet getUserData(string email)
        {
            DataSet ds = new DataSet();

            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_getUserDataByEmail", email);

            return ds;
        }



        public DataSet IsUserVerified(Int32 login_sk)
        {
            DataSet ds = new DataSet();

            ds = SqlHelper.ExecuteDataset(Config.Crebas, "P_g_getuserdatabylogin_sk", login_sk);

            return ds;
        }


        public int CheckEnvironment()
        {
            int i = 0;
            SqlDataReader dr = SqlHelper.ExecuteReader(Config.Crebas, "[p_g_environment]");
            while (dr.Read())
            {
                i = (Int32)dr[0];
            }
            return i;
        }




        public DataSet getProviderSubscriptionData(Int32 provider_id)
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_GetServiceProvider_registratation_subscription", provider_id);
            return ds;
        }



        public DataSet insert_GmailAccount_details(string email, string name)
        {
            DataSet ds = new DataSet();

            ds = SqlHelper.ExecuteDataset(Config.Crebas, "insert_GmailAccount_details", email, name);

            return ds;
        }


        public DataSet Update_Device_details(int seeker_sk, string Device)
        {
            DataSet ds = new DataSet();

            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_u_Seeker_device", seeker_sk, Device);

            return ds;
        }

        public DataSet CountryByCode(string country_by_Code, string Ustate, string Ucity)
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_country_by_Code", country_by_Code, Ustate, Ucity);
            return ds;
        }


        public DataSet Insert_payment_bridge(int ss_sp_sk, string subscription_type, int? card_sk, int? bank_sk, string payment_methode_name)
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_iu_payment_bridge", ss_sp_sk, subscription_type, card_sk, bank_sk, payment_methode_name);
            return ds;
        }
    
    }
}

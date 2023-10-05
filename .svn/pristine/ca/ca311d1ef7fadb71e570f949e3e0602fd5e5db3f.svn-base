using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Factory;

namespace Business
{
    public class BusinessLogin
    {
        Factory.FactoryLogin objFactoryLogin = new FactoryLogin();
        public DataSet checkLoginInfo(string email, string password, char mode)
        {
            DataSet ds = new DataSet();
            ds = objFactoryLogin.checkLoginInfo(email, password, mode);
            return ds;
        }
        public DataSet getProviderId(Int32 login_sk, Int32 flag)
        {
            DataSet ds = new DataSet();
            ds = objFactoryLogin.getProviderId(login_sk, flag);
            return ds;
        }
        //public DataSet getSeekerID(Int32 login_sk, Int32 flag)
        //{
        //    DataSet ds = new DataSet();
        //    ds = objFactoryLogin.getSeekerID(login_sk, flag);
        //    return ds;
        //}
        public DataSet getSeekerData(Int32 seeker_id)
        {
            DataSet ds = new DataSet();
            ds = objFactoryLogin.getSeekerData(seeker_id);
            return ds;
        }
        //   public int setChangePassword(Int32 login_sk, string password)
        public int setChangePassword(Int32 login_sk, string password, string currentpassword)
        {
            // int i = objFactoryLogin.setChangePassword(login_sk, password);
            int i = objFactoryLogin.setChangePassword(login_sk, password, currentpassword);

            return i;
        }
        public int setChangeEmail(Int32 login_sk, string email_id)
        {
            int i = objFactoryLogin.setChangeEmail(login_sk, email_id);
            return i;
        }
        public DataSet GetQueForgetPwd(string emailId)
        {
            DataSet ds = new DataSet();
            ds = objFactoryLogin.GetQueForgetPwd(emailId);
            return ds;
        }
        public DataSet ChkFavouriteCustomerBooking(Int32 seeker_id, Int32 provide_sk, DateTime slot_date)
        {
            DataSet ds = new DataSet();
            ds = objFactoryLogin.ChkFavouriteCustomerBooking(seeker_id, provide_sk, slot_date);
            return ds;
        }


        public DataSet IsLoginExist(string strEmail)
        {
            return objFactoryLogin.IsLoginExist(strEmail);
        }
        public DataSet getUserData(string strEmail)
        {
            return objFactoryLogin.getUserData(strEmail);

        }

        public DataSet IsUserVerified(Int32 login_sk)
        {
            return objFactoryLogin.IsUserVerified(login_sk);

        }
        public int CheckEnvironment()
        {
            return objFactoryLogin.CheckEnvironment();
        }



        public DataSet getProviderSubscriptionData(Int32 provider_id)
        {

            return objFactoryLogin.getProviderSubscriptionData(provider_id);
        }



        public DataSet insert_GmailAccount_details(string email, string name)
        {
            return objFactoryLogin.insert_GmailAccount_details(email, name);
        }


        public DataSet Update_Device_details(int seeker_sk, string Device)
        {
            return objFactoryLogin.Update_Device_details(seeker_sk, Device);
        }

        public DataSet CountryByCode(string country_by_Code, string Ustate, string Ucity)
        {
            return objFactoryLogin.CountryByCode(country_by_Code, Ustate, Ucity);
        }


        public DataSet Insert_payment_bridge(int ss_sp_sk, string subscription_type, int? card_sk, int? bank_sk, string payment_methode_name)
        {
            return objFactoryLogin.Insert_payment_bridge(ss_sp_sk, subscription_type, card_sk, bank_sk, payment_methode_name);
        }

       
    }
}

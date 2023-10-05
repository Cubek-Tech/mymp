﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Factory;
using System.Data.SqlClient;
using System.Data;
using BussinessEntity;

namespace Business
{
    public class RegistrationBusiness
    {
        Factory.RegistrationFactory objRegistrationFactory = new RegistrationFactory();
        //This prpperty will use to access id in well come page
        public static string strUserId { get; set; }

        //this is used for get data of slot
        public DataSet GetSlotData()
        {
            DataSet dt = objRegistrationFactory.GetSlotData();
            return dt;
        }

        public DataSet getSecretQuestion()
        {
            DataSet ds = new DataSet();
            ds = objRegistrationFactory.getSecretQuestion();
            return ds;
        }
        public DataSet getMenuData()
        {
            DataSet ds = new DataSet();
            ds = objRegistrationFactory.getMenuData();
            return ds;
        }

        public int setLoginDetails(string email, string password, string sQuestion, string sAnswer, char isActive, char loginType, int loginId, string ip)
        {
            int i = objRegistrationFactory.setLoginDetails(email, password, sQuestion, sAnswer, isActive, loginType, loginId, ip);
            return i;
        }


        public DataSet getSubTypeFood(string type)
        {
            DataSet ds = new DataSet();
            ds = objRegistrationFactory.getSubTypeFood(type);
            return ds;
        }

        public DataSet getallsubtype(int selected_sk)
        {
            DataSet ds = new DataSet();
            ds = objRegistrationFactory.getallsubtype(selected_sk);
            return ds;
        }

        public DataSet getmassagetype()
        {
            DataSet ds = new DataSet();
            ds = objRegistrationFactory.getmassagetype();
            return ds;
        }

        //for all spa type
        public DataSet getSpaType()
        {
            DataSet ds = new DataSet();
            ds = objRegistrationFactory.getSpaType();
            return ds;
        }
        // for spa sub type
        public DataSet getSpaSubType(int spatype)
        {
            DataSet ds = new DataSet();
            ds = objRegistrationFactory.getSpaSubType(spatype);
            return ds;
        }

        public DataSet OtherFoodSubType_as_FoodSubType(int provider_sk, int fnb_type_sk, int fnb_sub_type_sk)
        {
            DataSet ds = new DataSet();
            ds = objRegistrationFactory.OtherFoodSubType_as_FoodSubType(provider_sk, fnb_type_sk, fnb_sub_type_sk);
            return ds;

        }



        //and here for web service
         public DataSet getCountryCity()
        {
            DataSet ds = new DataSet();
            ds = objRegistrationFactory.getCountryCity();
            return ds;
        }

        public DataSet getCountryCity_hide_gateway()
        {
            DataSet ds = new DataSet();
            ds = objRegistrationFactory.getCountryCity_hide_gateway();
            return ds;
        }

        public DataSet getCurrrency()
        {
            DataSet ds = new DataSet();
            ds = objRegistrationFactory.getCurrrency();
            return ds;
        }
        public DataSet getUserData(string email)
        {
            DataSet ds = new DataSet();
            ds = objRegistrationFactory.getUserData(email);
            return ds;
        }
        public DataSet getUserDatabyLogin_sk(int login_sk)
        {
            DataSet ds = new DataSet();
            ds = objRegistrationFactory.getUserDatabyLogin_sk(login_sk);
            return ds;
        }
        public DataSet getUserDataLogin(int login_sk)
        {
            DataSet ds = new DataSet();
            ds = objRegistrationFactory.getUserDataLogin(login_sk);
            return ds;
        }
        public int insertFNBSubType(DataTable dtTemp)
        {
            int i = objRegistrationFactory.insertFNBSubType(dtTemp);
            return i;
        }
    
        //new method for image uploading

        public int insertImage(Int32 service_provider_sk, string image_type, string image_name)
        {
            int i = objRegistrationFactory.insertImage(service_provider_sk, image_type, image_name);
            return i;

        }
        public DataSet getStateProvider(int country_sk)
        {
            DataSet ds = new DataSet();
            ds = objRegistrationFactory.getStateProvider(country_sk);
            return ds;
        }
        public DataSet getCityProvider(int state_sk, int country_sk)
        {
            DataSet ds = new DataSet();
            ds = objRegistrationFactory.getCityProvider(state_sk, country_sk);
            return ds;
        }


        public DataSet getAreaProvider(int city_sk, int state_sk, int country_sk)
        {
            DataSet ds = new DataSet();
            ds = objRegistrationFactory.getAreaProvider(city_sk, state_sk, country_sk);
            return ds;
        }
        public DataSet getcurrencysymbol(int country_sk)
        {
            DataSet ds = new DataSet();
            ds = objRegistrationFactory.getcurrencysymbol(country_sk);
            return ds;
        }

        public DataSet getCityTelecomCode(int country_sk, int state_sk, int city_sk)
        {
            DataSet ds = new DataSet();
            ds = objRegistrationFactory.getCityTelecomCode(country_sk, state_sk, city_sk);
            return ds;
        }
        public int InsertBankDetail(int provider_id, int currency_sk, string account_no, string leagal_entity_name, int? country_sk, int? state_sk, int? city_sk, string address_text, string postal_code, string bank_name, string swift_sort_code, char payment_method, int? token_amt)
        {
            int i = objRegistrationFactory.InsertBankDetail(provider_id, currency_sk, account_no, leagal_entity_name, country_sk, state_sk, city_sk, address_text, postal_code, bank_name, swift_sort_code, payment_method, token_amt);
            return i;
        }

      
        public DataSet Get_Sp_BankDetail(int service_provider_sk)
        {
            return objRegistrationFactory.Get_Sp_BankDetail(service_provider_sk);
        }
        //bulk seeker friends Insert
        public int insertQuickSignup(string email, string password, string sQuestion, string sAnswer, char isActive, char loginType, int loginId, string ip)
        {
            return objRegistrationFactory.insertQuickSignup(email, password, sQuestion, sAnswer, isActive, loginType, loginId, ip);
        }


        public int SetHitPowered(string ip_address, DateTime date)
        {
            return objRegistrationFactory.SetHitPowered(ip_address, date);
        }


        //this is used for getting the staff image data
        public DataSet getStaffImage(int Provider_Sk)
        {
            DataSet ds = new DataSet();
            ds = objRegistrationFactory.getStaffImage(Provider_Sk);
            return ds;
        }

        public string get_dynamic_label(int country_sk)
        {
            string labelm2b = "";
            labelm2b = objRegistrationFactory.get_dynamic_label(country_sk);
            return labelm2b;
        }
   
        public DataSet getPaymentButton_type(string country_sk)
        {
            DataSet ds = new DataSet();
            ds = objRegistrationFactory.getPaymentButton_type(country_sk);
            return ds;
        }


        public DataSet getbank_card_details()
        {
            DataSet ds = new DataSet();
            ds = objRegistrationFactory.getbank_card_details();
            return ds;
        }

        public DataSet Insert_Provider_reviews_to_M2b(int review_rating, string review_text, DateTime review_datetime, char is_approved, string Provider_name, string Provider_email_id, int country_sk)
        {
            DataSet ds = new DataSet();
            ds = objRegistrationFactory.Insert_Provider_reviews_to_M2b(review_rating, review_text, review_datetime, is_approved, Provider_name, Provider_email_id, country_sk);
            return ds;
        }

        public DataSet getImage(int Provider_Sk, string imagetype)
        {
            DataSet ds = new DataSet();
            ds = objRegistrationFactory.getImage(Provider_Sk, imagetype);
            return ds;
        }
    }
}

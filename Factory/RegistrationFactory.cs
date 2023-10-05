using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using BussinessEntity;
using System.Web;

namespace Factory
{
    public class RegistrationFactory
    {
        public DataSet GetSlotData()
        {
            DataSet ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_solt_data");
            return ds;
        }

        public DataSet getSecretQuestion()
        {
            DataSet ds = new DataSet();

            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_getSecretQuestions", "W");
            return ds;
        }
        public DataSet getMenuData()
        {
            DataSet ds = new DataSet();

            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_getMenuData");
            return ds;
        }
        public int setLoginDetails(string email, string password, string sQuestion, string sAnswer, char isActive, char loginType, int loginId, string ip)
        {

            int i = SqlHelper.ExecuteNonQuery(Config.Crebas, "p_i_setLoginDetails", email, password, sQuestion, sAnswer, isActive, loginType, loginId, ip);
            if (i != 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int insertQuickSignup(string email, string password, string sQuestion, string sAnswer, char isActive, char loginType, int loginId, string ip)
        {

            int i = 0;
            i = Convert.ToInt32(SqlHelper.ExecuteScalar(Config.Crebas, "p_i_quick_signup", email, password, sQuestion, sAnswer, isActive, loginType, loginId, ip));
            return i;
        }

        public DataSet getSubTypeFood(string type)
        {
            DataSet ds = new DataSet();

            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_get_service_type", "W", type);
            return ds;
        }

        public DataSet getallsubtype(int selected_sk)
        {
            DataSet ds = new DataSet();

            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_get_spa_sub_service_type", "W", selected_sk);
            return ds;
        }

        public DataSet getmassagetype()
        {
            DataSet ds = new DataSet();

            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_get_service_type", "W", "M");
            return ds;
        }

        //to get whole spa type
        public DataSet getSpaType()
        {
            DataSet ds = new DataSet();

            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_get_service_type", "W", "S");

            return ds;
        }

        //to get spa sub type
        public DataSet getSpaSubType(int spatype)
        {
            DataSet ds = new DataSet();

            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_get_service_sub_type", spatype, "S");
            return ds;
        }

        public DataSet OtherFoodSubType_as_FoodSubType(int provider_sk, int fnb_type_sk, int fnb_sub_type_sk)
        {
            DataSet ds = new DataSet();

            ds = SqlHelper.ExecuteDataset(Config.Crebas, "P_g_other_food_sub_type_as_food_sub_type", provider_sk, fnb_type_sk, fnb_sub_type_sk);
            return ds;
        }


        //public DataSet getItemName(int ItemCat)
        //{
        //    DataSet ds = new DataSet();

        //    ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_getItemName", ItemCat);
        //    return ds;
        //}

        //public DataSet getItemName(int ItemCat, int fnbTypSk, int fnbSubTypeSk)
        //{
        //    DataSet ds = new DataSet();
        //    ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_getItemName", ItemCat, fnbTypSk, fnbSubTypeSk);
        //    return ds;
        //}


        //public int insertOutletSignUp(string service_provider_name, string service_provider_desc, string mobile_no, char subscription_model_sk, int login_sk, char is_institution, string url, char include_public_holiday, string search_keywords, char is_terms_conditions, int country_sk, string other_terms_conditions, string coverage_scope, string country_telecom_code, char is_other_questions, string phone_nos, string fax_no, string contact_persons, string email_id, string location_url, decimal avg_lunch_cost, decimal avg_dinner_cost, int avg_review_rating, string floor_pdf_name, string menu_pdf_name, int min_slot_duration, DateTime start_date_recurrence, DateTime end_date_recurrence, int city_sk, int state_sk, string address, string postal_code, string other_city, string other_state, int img_sk)
        //{
        //    DataSet ds = new DataSet();
        //    ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_i_insertOutletSignUp", service_provider_name, service_provider_desc, mobile_no, subscription_model_sk, login_sk, is_institution, url, include_public_holiday, search_keywords, is_terms_conditions, country_sk, other_terms_conditions, coverage_scope, country_telecom_code, is_other_questions, phone_nos, fax_no, contact_persons, email_id, location_url, avg_lunch_cost, avg_dinner_cost, avg_review_rating, floor_pdf_name, menu_pdf_name, min_slot_duration, start_date_recurrence, end_date_recurrence, city_sk, state_sk, address, postal_code, other_city, other_state, img_sk);

        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        return Convert.ToInt32(ds.Tables[0].Rows[0]["service_provider_sk"].ToString());
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}
        //added by nilesh g for orientation
        //public int Insert_quick_outlet_signup(string service_provider_name, string service_provider_desc, string mobile_no,
        //    char subscription_model_sk, int login_sk, char is_institution, int menu_sk, string url, char include_public_holiday,
        //    string search_keywords, char is_terms_conditions, int country_sk, string other_terms_conditions, string coverage_scope,
        //    string country_telecom_code, char is_other_questions, string phone_nos, string fax_no, string contact_persons,
        //    string email_id, string location_url, decimal avg_lunch_cost, decimal avg_dinner_cost, int avg_review_rating,
        //    string floor_pdf_name, string menu_pdf_name, int min_slot_duration, DateTime start_date_recurrence, DateTime end_date_recurrence,
        //    int city_sk, int state_sk, string address, string postal_code, string other_city, string other_state, int img_sk, string lat,
        //    string log, int area_sk, string massage_or_spa, string opentime, string closetime, char orientationchkM, char orientationchkF, string other_service, int? copy_provider_sk, char is_home_delivery, string homdelivery_type)
        //{
        //    int? menus_sk = null;
        //    if (menu_sk > 0)
        //        menus_sk = menu_sk;

        //    DataSet ds = new DataSet();
        //    ds = SqlHelper.ExecuteDataset(Config.Crebas, "[p_i_quick_outlet_signup]", service_provider_name,
        //        service_provider_desc, mobile_no, subscription_model_sk, login_sk, is_institution, menus_sk, url,
        //        include_public_holiday, search_keywords, is_terms_conditions, country_sk, other_terms_conditions,
        //        coverage_scope, country_telecom_code, is_other_questions, phone_nos, fax_no, contact_persons,
        //        email_id, location_url, avg_lunch_cost, avg_dinner_cost, avg_review_rating, floor_pdf_name,
        //        menu_pdf_name, min_slot_duration, start_date_recurrence, end_date_recurrence, city_sk, state_sk,
        //        address, postal_code, other_city, other_state, img_sk, lat, log, area_sk, massage_or_spa, opentime, closetime, orientationchkM, orientationchkF, other_service, copy_provider_sk, is_home_delivery, homdelivery_type);

        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        return Convert.ToInt32(ds.Tables[0].Rows[0]["service_provider_sk"].ToString());
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}


        //public int updateOutletSignUp(int provider_id, string service_provider_name, string service_provider_desc, string mobile_no, char subscription_model_sk, string url, int country_sk, string coverage_scope, string country_telecom_code, string phone_nos, string fax_no, string contact_persons, string email_id, string location_url, decimal avg_lunch_cost, decimal avg_dinner_cost, int avg_review_rating, int min_slot_duration, DateTime start_date_recurrence, DateTime end_date_recurrence, int city_sk, int state_sk, string address, string postal_code, string other_city, string other_state)
        //{
        //    DataSet ds = new DataSet();
        //    ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_u_UpdateOutletSignUp", provider_id, service_provider_name, service_provider_desc, mobile_no, subscription_model_sk, url, country_sk, coverage_scope, country_telecom_code, phone_nos, fax_no, contact_persons, email_id, location_url, avg_lunch_cost, avg_dinner_cost, avg_review_rating, min_slot_duration, start_date_recurrence, end_date_recurrence, city_sk, state_sk, address, postal_code, other_city, other_state);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        return Convert.ToInt32(ds.Tables[0].Rows[0]["service_provider_sk"].ToString());
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}
        public DataSet getCountryCity()
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_country", "W");
            return ds;
        }

        public DataSet getCountryCity_hide_gateway()
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_country_hide_gateway");
            return ds;
        }

        public DataSet getCurrrency()
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_getCurrency");
            return ds;
        }
        public DataSet getUserData(string email)
        {
            DataSet ds = new DataSet();

            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_getUserDataByEmail", email);

            return ds;
        }
        public DataSet getUserDatabyLogin_sk(int login_sk)
        {
            DataSet ds = new DataSet();

            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_getUserDatabyLogin_sk", login_sk);

            return ds;
        }
        public DataSet getUserDataLogin(int login_sk)
        {
            DataSet ds = new DataSet();

            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_getUserData", login_sk);

            return ds;
        }
        public int insertFNBSubType(DataTable dtTemp)
        {
            int i = 0;
            try
            {
                SqlHelper.ExecuteNonQuery(Config.Crebas, "p_d_service_providerfnbsubtyp", dtTemp.Rows[0]["service_provider_sk"]);
                foreach (DataRow dr in dtTemp.Rows)
                {
                    //    i = SqlHelper.ExecuteNonQuery(Config.Crebas, "sp_insertFNBSubType", Convert.ToInt32(dr["service_provider_sk"]), Convert.ToInt16(dr["fnb_type_sk"]), Convert.ToInt16(dr["fnb_sub_type_sk"]), dr["other_fnb_type"], dr["other_fnb_sub_type"]);
                    i = SqlHelper.ExecuteNonQuery(Config.Crebas, "p_i_insertFNBSubType", Convert.ToInt32(dr["service_provider_sk"]), Convert.ToInt16(dr["service_sk"]), dr["sub_service_sk"], dr["other_service_name"], dr["other_sub_service_name"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return i;
        }
        //public int updateFNBSubType(DataTable dtTemp)
        //{
        //    int i = 0;
        //    try
        //    {
        //        foreach (DataRow dr in dtTemp.Rows)
        //        {
        //            //i = SqlHelper.ExecuteNonQuery(Config.Crebas, "sp_updateFNBSubType", Convert.ToInt32(dr["service_provider_sk"]), Convert.ToInt16(dr["fnb_type_sk"]), Convert.ToInt16(dr["fnb_sub_type_sk"]), dr["other_fnb_type"], dr["other_fnb_sub_type"]);
        //            i = SqlHelper.ExecuteNonQuery(Config.Crebas, "p_u_updateFNBSubType", Convert.ToInt32(dr["service_provider_sk"]), Convert.ToInt16(dr["fnb_type_sk"]), Convert.ToInt16(dr["fnb_sub_type_sk"]), dr["other_fnb_type"], dr["other_fnb_sub_type"]);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return i;
        //}
        //public int insertQuestions(DataTable dtTemp)
        //{
        //    int i = 0;
        //    try
        //    {
        //        foreach (DataRow dr in dtTemp.Rows)
        //        {
        //            i = SqlHelper.ExecuteNonQuery(Config.Crebas, "p_i_insertQuestions", Convert.ToInt32(dr["service_provider_sk"]), Convert.ToInt16(dr["question_sk"]), dr["question_text"].ToString().Trim() != "" ? dr["question_text"].ToString().Trim() : "", Convert.ToChar(dr["is_mandatory"]));

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return i;
        //}
        //public int updateProviderTable(int provider_id, char is_institution, string search_keywords, char is_terms_conditions, int terms_conditions_sk, string other_terms_conditions, char is_other_questions, char is_closing_feature, int closing_hours_before, int menu_sk, char is_home_delivery, string terms_conditions_home_delivery, string closing_hours_before_home_delivery, string menu_url, int? tsEmailAlert, int? tsMessAlert)
        //{
        //    int i = 0;
        //    i = SqlHelper.ExecuteNonQuery(Config.Crebas, "p_u_updateProviderData", provider_id, is_institution, search_keywords, is_terms_conditions, terms_conditions_sk, other_terms_conditions, is_other_questions, is_closing_feature, closing_hours_before, menu_sk, is_home_delivery, terms_conditions_home_delivery, closing_hours_before_home_delivery.ToString() == "0" ? null : closing_hours_before_home_delivery.ToString(), menu_url, tsEmailAlert, tsMessAlert);
        //    return i;
        //}
        //public int insertUploadImage(byte[] imgBinaryData, string imgContentType, int flag, int providerId)
        //{
        //    DataSet ds = new DataSet();
        //    ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_i_insertImageUploader", imgBinaryData, imgContentType, flag, providerId);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        return Convert.ToInt32(ds.Tables[0].Rows[0]["img_sk"].ToString());
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}

        //for provider_sk nilesh
        //public DataSet getProvider_sk(int instituion_sk)
        //{
        //    DataSet dt = new DataSet();

        //    dt = SqlHelper.ExecuteDataset(Config.Crebas, "P_g_getProvider_sk", instituion_sk);

        //    return dt;
        //}


        //new method for image uploading

        public int insertImage(Int32 service_provider_sk, string image_type, string image_name)
        {
            if (service_provider_sk <= 0)
                return 0;
            DataSet status = new DataSet(); int img_sk = 0;
            status = SqlHelper.ExecuteDataset(Config.Crebas, "p_i_insertImageUpload", service_provider_sk, image_type, image_name);
            if (status.Tables[0] != null)
                img_sk = Convert.ToInt32(status.Tables[0].Rows[0]["Image_sk"]);
            return img_sk;
        }

        //public int insert_OtherImage(int provider_id, DataTable dtTemp, string image_type)
        //{
        //    int i = 0;
        //    try
        //    {
        //        SqlHelper.ExecuteNonQuery(Config.Crebas, "p_d_provider_otherimages", provider_id);
        //        foreach (DataRow dr in dtTemp.Rows)
        //        {
        //            i = SqlHelper.ExecuteNonQuery(Config.Crebas, "p_i_provider_otherimages", provider_id, dr["img_name"].ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return i;

           

        //}



        //public DataSet getImageData(int img_sk)
        //{
        //    DataSet ds = new DataSet();
        //    ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_getImageData", img_sk);
        //    return ds;
        //}
        //public DataSet getImageDataNull()
        //{
        //    DataSet ds = new DataSet();
        //    ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_getImageDataNull");
        //    return ds;
        //}
        //public DataSet getItemDescription(int item_sk)
        //{
        //    DataSet ds = new DataSet();
        //    ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_getItemDescription", item_sk);
        //    return ds;
        //}
        //public string getMenuType(int menu_sk)
        //{
        //    //DataSet ds = new DataSet();
        //    string menu_type = Convert.ToString(SqlHelper.ExecuteScalar(Config.Crebas, "p_g_sp_menu_type", menu_sk));
        //    return menu_type;
        //}
        //public int setMenuData(string MenuName)
        //{
        //    DataSet ds = new DataSet();
        //    ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_i_setMenuData", MenuName);
        //    return Convert.ToInt32(ds.Tables[0].Rows[0]["menu_sk"]);
        //}
        //public int insertMenuItems(int menu_sk, int item_sk, int item_category_sk, string other_item_category_name, string item_description, string item_name_language, string other_item_name, decimal price, string other_item_desc, decimal home_delivery_price, int fnb_type_sk, int fnb_sub_type_sk)
        //{
        //    int i = 0;
        //    i = SqlHelper.ExecuteNonQuery(Config.Crebas, "p_i_insertMenuItems", menu_sk, item_sk, item_category_sk, other_item_category_name, item_description, item_name_language, other_item_name, price, other_item_desc, home_delivery_price, fnb_type_sk, fnb_sub_type_sk);
        //    return i;
        //}
        //public DataSet getMenuDataGrid(int menu_sk)
        //{
        //    DataSet ds = new DataSet();
        //    ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_getMenuDataGrid", menu_sk);
        //    return ds;
        //}
        //public int setTableInfo(int service_provider_sk, Int16 table_sk, string table_no, string table_description, Int16 no_of_seats)
        //{
        //    int i = 0;
        //    i = SqlHelper.ExecuteNonQuery(Config.Crebas, "p_i_setTableInfo", service_provider_sk, table_sk, table_no, table_description, no_of_seats);

        //    return i;
        //}




        //end here for web service


        public DataSet getStateProvider(int country_sk)
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_getStateProvider", country_sk, "W");
            return ds;
        }
        public DataSet getCityProvider(int state_sk, int country_sk)
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_getCityProvider", state_sk, country_sk, "W");
            return ds;
        }
        public DataSet getAreaProvider(int city_sk, int state_sk, int country_sk)
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_getAreaProvider", city_sk, state_sk, country_sk);
            return ds;
        }

        public DataSet getcurrencysymbol(int country_sk)
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_getcurrencysymbol", country_sk);
            return ds;
        }

        public DataSet getCityTelecomCode(int country_sk, int state_sk, int city_sk)
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_getCityTelecomCode", country_sk, state_sk, city_sk);
            return ds;
        }

        
        
      
       public int InsertBankDetail(int provider_id, int currency_sk, string account_no, string leagal_entity_name, int? country_sk, int? state_sk, int? city_sk, string address_text, string postal_code, string bank_name, string swift_sort_code, char payment_method, int? token_amt)
        {

            return SqlHelper.ExecuteNonQuery(Config.Crebas, "p_i_sp_restaurant_bank_detail", provider_id, currency_sk, account_no, leagal_entity_name, country_sk, state_sk, city_sk, address_text, postal_code, bank_name, swift_sort_code, payment_method, token_amt);

        }
        public DataSet Get_Sp_BankDetail(int provider_sk)
        {
            DataSet ds;
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_registered_entity", provider_sk);
            return ds;

        }
     
        public int SetHitPowered(string ip_address, DateTime date)
        {
            int i = 0;
            i = Convert.ToInt32(SqlHelper.ExecuteNonQuery(Config.Crebas, "p_i_poweredby", ip_address, date));
            return i;
        }

        //this is used for getting the staff image by sp_sk 
        public DataSet getStaffImage(int Provider_Sk)
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "P_g_staff_image", Provider_Sk);
            return ds;
        }

     
       

      
        public string get_dynamic_label(int country_sk)
        {
            string labelm2b = "";
            DataSet ds = new DataSet();

            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_country_label", country_sk);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                labelm2b = ds.Tables[0].Rows[0]["label_name"].ToString();
            }

            return labelm2b;

        }

       
        public DataSet getPaymentButton_type( string country_sk)
        {
            DataSet ds = new DataSet();

            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_get_PaymentButton_type", country_sk);
            return ds;
        }

        public DataSet getbank_card_details()
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_bank_card_details");
            return ds;
        }


        public DataSet Insert_Provider_reviews_to_M2b(int review_rating, string review_text, DateTime review_datetime, char is_approved, string Provider_name, string Provider_email_id, int country_sk)
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_i_Provider_reviews_to_M2b", review_rating, review_text, review_datetime, is_approved, Provider_name, Provider_email_id, country_sk);
            return ds;

        }

        public DataSet getImage(int Provider_Sk, string imagetype)
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_getImage", Provider_Sk, imagetype);
            return ds;
        }

    }
}

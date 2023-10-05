using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace n_Equipment.Classes
{
    public class BusinessEquipments
    {
        public BusinessEquipments()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        FactoryEquipments obj = new FactoryEquipments();

        public DataSet Get_srp_equipment(int country_sk, int category_sk, string search_text)
        {

            return obj.Get_srp_equipment(country_sk, category_sk, search_text);

        }

        public DataSet getCountry()
        {

            return obj.getCountry();
        }

        public DataSet getCategory()
        {

            return obj.getCategory();
        }

        /// <summary>
        /// EIP region
        /// </summary>

        public DataSet Get_EuipmentDetails(string Search_url,int countrysk)
        {
            return obj.Get_EuipmentDetails(Search_url, countrysk);
        }
        public DataSet Insert_Review(int equipment_sk, int service_provider_sk, int review_rating, string review_text, string reviwer_name, string reviwer_email_id, int reviwer_sk)
        {
            return obj.Insert_Review(equipment_sk, service_provider_sk, review_rating, review_text,reviwer_name, reviwer_email_id, reviwer_sk);
         
        }

        public DataSet GetCountrySk(string country_name, string category_name)
        {
            return obj.GetCountrySk(country_name,category_name);
           
        }
    }

}
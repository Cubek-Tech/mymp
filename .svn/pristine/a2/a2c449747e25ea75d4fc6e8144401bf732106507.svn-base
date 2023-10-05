using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

namespace n_Equipment.Classes
{
    public class FactoryEquipments
    {
        public FactoryEquipments()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        DataSet ds;

        /// <summary>
        /// SRP methods
        /// </summary>
        /// <param name="country_sk"></param>
        /// <param name="category_sk"></param>
        /// <param name="search_text"></param>
        /// <returns></returns>
        #region srp
        public DataSet Get_srp_equipment(int country_sk, int category_sk, string search_text)
        {
            ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_eq_srp",
                country_sk, category_sk, search_text);
            return ds;

        }

        public DataSet getCountry()
        {
            ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_eq_country");
            return ds;
        }

        public DataSet getCategory()
        {
            ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_eq_category");
            return ds;
        }
        #endregion

        #region eip

        public DataSet Get_EuipmentDetails(string search_url,int countrysk)
        {
            ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_eq_eip", search_url, countrysk);
            return ds;
        }

        public DataSet Get_EuipmentDetails(int equipment_sk, int service_provider_sk)
        {
            ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_eq_eip", equipment_sk, service_provider_sk);
            return ds;
        }


        public DataSet Insert_Review(int equipment_sk, int service_provider_sk, int review_rating, 
            string review_text,string reviwer_name,string reviwer_email_id,int reviwer_sk)
        {
            ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_i_eq_review", service_provider_sk, 
                equipment_sk, review_rating, review_text, reviwer_name, reviwer_email_id, reviwer_sk);
            return ds;
        }

        public DataSet GetCountrySk(string country_name, string category_name)
        {
            ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_country_sk", country_name, category_name);
            return ds;
        }

        public DataSet GetCountrySk(string provider_name)
        {
            ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_providerdetails", provider_name);
            return ds;
        }
        
        #endregion

    }
}
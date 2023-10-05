using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using BussinessEntity;


namespace Factory
{
    public class FactoryJobSection
    {

        public DataSet getJobUserData(string email)
        {
            DataSet ds = new DataSet();

            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_get_JobUserByEmail", email);

            return ds;
        }

        public int insertQuickJobseekerSignup(string job_seeker_name, string mobile_no, char is_registered, string email, string password,
            int city_sk,
            int state_sk, int country_sk, int month_experience, char loginType)
        {

            int i = 0;
            i = Convert.ToInt32(SqlHelper.ExecuteScalar(Config.Crebas, "p_i_quick_Job_seeker_signup",
                job_seeker_name, mobile_no, is_registered, email, password,
             city_sk, state_sk, country_sk, month_experience, loginType));
            return i;
        }

        //public int setJOBSeekerRegistration(string email_id, string password, int secret_question_sk, string secret_question_answer, char is_active, char login_type, string service_seeker_first_name, string service_seeker_middle_name, string service_seeker_last_name, char is_sms_notification, string mobile_no, string country_telecom_code, char is_registered, int city_sk, int state_sk, string address, string othercity, string otherstate, string postalcode, int country_sk, string lat, string log, int salutation_sk)
        //{
        //    int i = 0;
        //    i = SqlHelper.ExecuteNonQuery(Config.Crebas, "p_i_JOBSeekerRegistration", email_id, password, secret_question_sk, secret_question_answer, is_active, login_type, service_seeker_first_name, service_seeker_middle_name, service_seeker_last_name, is_sms_notification, mobile_no, country_telecom_code, is_registered, city_sk, state_sk, address, othercity, otherstate, postalcode, country_sk, Convert.ToDecimal(lat), Convert.ToDecimal(log), salutation_sk);
        //    return i;

        //}



    }
}

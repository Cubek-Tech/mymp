using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Factory;
using System.Data.SqlClient;
using BussinessEntity;

namespace Business
{
    public class BussinessJobSection
    {
     FactoryJobs objFactoryJobSection = new FactoryJobSection();

        public DataSet getJobUserData(string email)
        {
            return objFactoryJobSection.getJobUserData(email);
        }

        public int insertQuickJobseekerSignup(string job_seeker_name, string mobile_no, char is_registered, string email, string password,
            int city_sk,
            int state_sk, int country_sk, int month_experience, char loginType)
        {
            return objFactoryJobSection.insertQuickJobseekerSignup(job_seeker_name, mobile_no, is_registered, email, password,
             city_sk, state_sk, country_sk, month_experience, loginType);
        }


        //public int setJOBSeekerRegistration(string email_id, string password, int secret_question_sk, string secret_question_answer, char is_active, char login_type, string service_seeker_first_name, string service_seeker_middle_name, string service_seeker_last_name, char is_sms_notification, string mobile_no, string country_telecom_code, char is_registered, int city_sk, int state_sk, string address, string othercity, string otherstate, string postalcode, int country_sk, string lat, string log, int salutation_sk)
        //{
        //    int i = 0;
        //    return i = objFactoryJobSection.setJOBSeekerRegistration(email_id, password, secret_question_sk, secret_question_answer, is_active, login_type, service_seeker_first_name, service_seeker_middle_name, service_seeker_last_name, is_sms_notification, mobile_no, country_telecom_code, is_registered, city_sk, state_sk, address, othercity, otherstate, postalcode, country_sk, lat, log, salutation_sk);
        //}
    }
}

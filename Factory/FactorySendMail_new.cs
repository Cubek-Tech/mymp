using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Data;
namespace Factory
{
    public class FactorySendMail_new
    {

        DataSet ds;
        public static int SendMail(string sender, string Mrecipients, string Msubject, string Mbody, string Attechment)
        {
            return SqlHelper.ExecuteNonQuery(Config.Crebas, "p_SendMail", sender, Mrecipients, Msubject, Mbody, Attechment);

        }
        public DataSet getMsgMethodAndType()
        {
            ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_NotificationTypeAndMethod");
            return ds;
        }
        public int setNotification(Int32 NotificationMethod, Int32 NotificationType, Int32 ProviderId, string NotificationContent, char Isdefault)
        {
            int i = 0;
            i = SqlHelper.ExecuteNonQuery(Config.Crebas, "p_ui_Notification", NotificationMethod, NotificationType, ProviderId, NotificationContent, Isdefault);
            return i;
        }
        public int CancelBookingSchedule(int provider_id, DateTime booking_date, string EmailTemplate, string subject)
        {
            int i = 0;
            i = SqlHelper.ExecuteNonQuery(Config.Crebas, "p_u_sp_cancel_booking_schedule", provider_id, booking_date, EmailTemplate, subject);
            return i;
        }
        public int guest_invitation_replay(int slot_booking_sk, string replay_email, char responseType, string replay_text)
        {
            int i = 0;
            i = SqlHelper.ExecuteNonQuery(Config.Crebas, "p_u_guest_invitation_replay", replay_email, responseType, slot_booking_sk, replay_text);
            return i;
        }
        public DataTable getProvideNotification(Int32 NotificationMethod, Int32 NotificationType, Int32 ProviderId, char isdefault)
        {
            ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_ProviderNotification", NotificationMethod, NotificationType, ProviderId,isdefault);
            return ds.Tables[0];
        }

        public DataTable GetNotificationTemplate(Int32 NotificationType, Int32 NotificationMethod)
        {
            ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_booking_notification_template", NotificationType, NotificationMethod);
            return ds.Tables[0];
        }


        public DataTable getProvideVerificationNotification()
        {
            ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_verification_template");
            return ds.Tables[0];
        }
        public DataTable getAllCustomerToProvider(Int32 providersk)
        {
            ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Config.Crebas, "p_g_getAllCustomer", providersk);
            return ds.Tables[0];
        }
        public int sendBulkEmail(DataTable dtTemp, string provider_emailid, string message_body, string subject, string Mode)
        {

            SqlConnection con = new SqlConnection(Config.Crebas);
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlParameter param = new SqlParameter();

                param = new SqlParameter();
                param.ParameterName = "@udtt_send_bulk_email";
                param.SqlDbType = SqlDbType.Structured;
                param.Value = dtTemp;
                param.TypeName = "dbo.udtt_list_send_bulk_email";
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);



                param = new SqlParameter();
                param.SqlDbType = SqlDbType.VarChar;
                param.ParameterName = "@service_provider_email_id";
                param.Value = provider_emailid;
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.SqlDbType = SqlDbType.VarChar;
                param.ParameterName = "@message_body";
                param.Value = message_body;
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.SqlDbType = SqlDbType.VarChar;
                param.ParameterName = "@subject";
                param.Value = subject;
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.SqlDbType = SqlDbType.VarChar;
                param.ParameterName = "@Mode";
                param.Value = Mode;
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);


                if (con.State != ConnectionState.Open) con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "p_send_bulk_email";
                int result = cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {

                // throw ex;
                return 0;
            }
            finally { if (con.State != ConnectionState.Closed) con.Close(); }

            return 1;
        }

        public int sendInvitationEmail(DataTable dtTemp, string provider_emailid, string message_body, string subject, string Mode)
        {

            SqlConnection con = new SqlConnection(Config.Crebas);
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlParameter param = new SqlParameter();

                param = new SqlParameter();
                param.ParameterName = "@udtt_send_bulk_email";
                param.SqlDbType = SqlDbType.Structured;
                param.Value = dtTemp;
                param.TypeName = "dbo.udtt_list_send_bulk_email";
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);



                param = new SqlParameter();
                param.SqlDbType = SqlDbType.VarChar;
                param.ParameterName = "@service_provider_email_id";
                param.Value = provider_emailid;
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.SqlDbType = SqlDbType.VarChar;
                param.ParameterName = "@message_body";
                param.Value = message_body;
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.SqlDbType = SqlDbType.VarChar;
                param.ParameterName = "@subject";
                param.Value = subject;
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.SqlDbType = SqlDbType.VarChar;
                param.ParameterName = "@Mode";
                param.Value = Mode;
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);


                if (con.State != ConnectionState.Open) con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "p_send_invitation_by_seeker";
                int result = cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {

                // throw ex;
                return 0;
            }
            finally { if (con.State != ConnectionState.Closed) con.Close(); }

            return 1;
        }


        public int? insertInvitedGuestBySeeker(int provider_sk, int seeker_sk, DateTime booking_date, string invite_text, DataTable dtEmail)
        {

            SqlConnection con = new SqlConnection(Config.Crebas);
            int? result;
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlParameter param = new SqlParameter();

                param = new SqlParameter();
                param.ParameterName = "@udtt_list_guest_email";
                param.SqlDbType = SqlDbType.Structured;
                param.Value = dtEmail;
                param.TypeName = "dbo.udtt_list_guest_email";
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);



                param = new SqlParameter();
                param.SqlDbType = SqlDbType.Int;
                param.ParameterName = "@service_provider_sk";
                param.Value = provider_sk;
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.SqlDbType = SqlDbType.Int;
                param.ParameterName = "@service_seeker_sk";
                param.Value = seeker_sk;
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.SqlDbType = SqlDbType.Date;
                param.ParameterName = "@slot_booking_date";
                param.Value = booking_date;
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.SqlDbType = SqlDbType.VarChar;
                param.ParameterName = "invite_text";
                param.Value = invite_text;
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);


                if (con.State != ConnectionState.Open) con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "p_iu_slot_booking_invitees";
                result = Convert.ToInt32(cmd.ExecuteScalar());
            }

            catch (Exception ex)
            {
              // throw ex;
                return 0;
            }
            finally { if (con.State != ConnectionState.Closed) con.Close(); }

            return result;
        }


        public static int send_notification_mail(string sender, string Mrecipients, string Msubject, string Mbody,int notification_type_sk,int notification_method_sk,int? slot_booking_sk,int? service_provider_sk,int? service_seeker_sk,char login_type)
        {
            return SqlHelper.ExecuteNonQuery(Config.Crebas, "p_send_notification_mail", sender, Mrecipients, Msubject, Mbody, notification_type_sk, notification_method_sk, slot_booking_sk, service_provider_sk, service_seeker_sk, login_type);

        }

    }
}

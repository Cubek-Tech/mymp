using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Factory;
using System.Data;
namespace Business
{
  public class BussinessSendMail_New
    {
        public string sender { get; set; }
        public string Mrecipients { get; set; }
        public string Msubject { get; set; }
        public string Mbody { get; set; }
        public string Attechment { get; set; }

        public int notification_type_sk { get; set; }
        public int notification_method_sk { get; set; }
        public int? slot_booking_sk{ get; set; }
        public int? service_provider_sk { get; set; }
        public int? service_seeker_sk { get; set; }
        public char login_type { get; set; }
      
        FactorySendMail_new objNotification = new FactorySendMail_new();
        public int SendMail()
        {
            return FactorySendMail.SendMail(sender, Mrecipients, Msubject, Mbody,Attechment);
        
        }
        public DataSet getMsgMethodAndType()
        {
            return objNotification.getMsgMethodAndType();
        }
        public int setNotification(Int32 NotificationMethod,Int32 NotificationType,Int32 ProviderId, string NotificationContent, char IsDefault)
        {
            int i = objNotification.setNotification(NotificationMethod, NotificationType, ProviderId, NotificationContent, IsDefault);
            return i;
        }
        public DataTable  getProvideNotification(Int32 NotificationMethod, Int32 NotificationType, Int32 ProviderId,char isdefault)
        {
            DataTable dt;
            dt = objNotification.getProvideNotification(NotificationMethod, NotificationType, ProviderId, isdefault);
            return dt;
        }

        public DataTable GetNotificationTemplate(Int32 NotificationType,Int32 NotificationMethod)
        {
            DataTable dt;
            dt = objNotification.GetNotificationTemplate(NotificationType, NotificationMethod);
            return dt;

        }


        public DataTable getProvideVerificationNotification()
        {
            DataTable dt;
            dt = objNotification.getProvideVerificationNotification();
            return dt;
        }
        public DataTable getAllCustomerToProvider(Int32 Providersk)
        {
            DataTable dt;
            dt = objNotification.getAllCustomerToProvider(Providersk);
            return dt;
        }
        public int sendBulkEmail(DataTable dtTemp, string provider_emailid, string message_body, string subject,string Mode)
        {
            int i = objNotification.sendBulkEmail(dtTemp, provider_emailid, message_body, subject,Mode);
            return i;
        }
        public int sendInvitationEmail(DataTable dtTemp, string provider_emailid, string message_body, string subject,string Mode)
        {
            int i = objNotification.sendInvitationEmail(dtTemp, provider_emailid, message_body, subject,Mode);
            return i;
        }
        public int CancelBookingSchedule(int provider_id, DateTime booking_date, string EmailTemplate, string subject)
        {
            int i = objNotification.CancelBookingSchedule(provider_id, booking_date, EmailTemplate, subject);
            return i;
        }
        public int guest_invitation_replay(int slot_booking_sk,string replay_email,char responseType,string replay_text)
        {
            int i = objNotification.guest_invitation_replay(slot_booking_sk, replay_email, responseType, replay_text);
            return i;
        }

        public int? insertInvitedGuestBySeeker(int provider_sk,int seeker_sk,DateTime booking_date,string invite_text,DataTable dtEmail)
        {
           return objNotification.insertInvitedGuestBySeeker(provider_sk, seeker_sk, booking_date, invite_text, dtEmail);
           
        }
        public int send_notification_mail()
        {
            return FactorySendMail_new.send_notification_mail(sender, Mrecipients, Msubject, Mbody, notification_type_sk, notification_method_sk, slot_booking_sk, service_provider_sk, service_seeker_sk, login_type);

        }
    }
}

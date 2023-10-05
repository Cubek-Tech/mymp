using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Factory;
using System.Data;
namespace Business
{
  public class BussinessSendMail
    {
        public string sender { get; set; }
        public string Mrecipients { get; set; }
        public string Msubject { get; set; }
        public string Mbody { get; set; }
        public string Attechment { get; set; }
        public string AlertSeekarEmail { get; set; }
        public string UserType { get; set; }
        public int notification_type_sk { get; set; }
        public int notification_method_sk { get; set; }
        public int? slot_booking_sk{ get; set; }
        public int? service_provider_sk { get; set; }
        public int? service_seeker_sk { get; set; }
        public char login_type { get; set; }
        




        FactorySendMail objNotification = new FactorySendMail();
       

        public int SendMail()
        {
            return FactorySendMail.SendMail(sender, Mrecipients, Msubject, Mbody,Attechment);
        
        }
      //added by nilesh g for feedback email
        public int SendMail(string reply_to,string sender, string Mrecipients, string Msubject, string Mbody, string Attechment)
        {
            return FactorySendMail.SendMail(reply_to,sender, Mrecipients, Msubject, Mbody, Attechment);

        }
        public int SendMailalert()  //nilesh
        {
            return FactorySendMail.SendMailalert(service_provider_sk, sender, Mrecipients, Msubject, Mbody, Attechment);
            
        }
        public int SendMail(string sender, string Mrecipients, string Msubject, string Mbody, string Attechment)
        {
            return FactorySendMail.SendMail(sender, Mrecipients, Msubject, Mbody, Attechment);

        }

        //public int SendMailerrormail(string sender1, string Mrecipients1, string Msubject1, string Mbody1, string imageurl1, string Attechment1)
        //{
        //    return objNotification.SendMailerrormail(sender1, Mrecipients1, Msubject1, Mbody1, imageurl1, Attechment1);

        //}
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


        //public DataTable Geterrorimagedata(string logsk)
        //{
        //    DataTable dt;
        //    dt = objNotification.Geterrorimagedata(logsk);
        //    return dt;

        //}

        public DataTable getProvideVerificationNotification()
        {
            DataTable dt;
            dt = objNotification.getProvideVerificationNotification();
            return dt;
        }
        //for reminder provider end promotion added by nilesh
    
               public DataTable getProviderReminder()
        {
            DataTable dt;
            dt = objNotification.getProviderReminder();
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
            return FactorySendMail.send_notification_mail(sender, Mrecipients, Msubject, Mbody, notification_type_sk, notification_method_sk, slot_booking_sk, service_provider_sk, service_seeker_sk, login_type);

        }

        public int send_notification_mail_Provider_annual_sub(int service_provider_sk)
        {
            return FactorySendMail.send_notification_mail_Provider_annual_sub(service_provider_sk);

        }

        public DataTable getProvidevarifyReview()
        {
            DataTable dt;
            dt = objNotification.getProvidevarifyReview();
            return dt;
        }
        public int sendMail(int login_sk, string Msubject, string Mbody, string fileattachment)
        {
            int i = 0;
             i = objNotification.sendMail(login_sk, Msubject, Mbody, fileattachment);
             return i;
        }

        public int SendMail_Contactus()
        {
            return  FactorySendMail.SendMail_Contactus(sender,
                Mrecipients,
                Msubject,
                Mbody,
                UserType, 
                Attechment);

        }


        public DataSet getProvideVerificationNotificationS()
        {
            DataSet dt;
            dt = objNotification.getProvideVerificationNotificationS();
            return dt;
        }

        public int sendSubscriptionMail(int login_sk)
        {
            int i;
            i = objNotification.sendSubscriptionMail(login_sk);
            return i;
        }


        public int sendActivate_Your_Listings(int Service_provider_sk)
        {
            int i = 0;
            i = objNotification.sendActivate_Your_Listings(Service_provider_sk);
            return i;
        }



        public int send_Buy_leads_Provider(int Service_provider_sk)
        {
            int i = 0;
            i = objNotification.send_Buy_leads_Provider(Service_provider_sk);
            return i;
        }
        public int send_Buy_Rank_Provider(int Service_provider_sk)
        {
            int i = 0;
            i = objNotification.send_Buy_Rank_Provider(Service_provider_sk);
            return i;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Mailsettings
/// </summary>
public class Mailsettings
{

    #region Method
    public string MailFrom{get;set;}
    public string MailTo{get;set;}
    public string MailSubject{get;set;}
    public string MailBody{get;set;}

   #endregion 

   
    public static bool SendEmail(Mailsettings MAIL)
    {
        try
        {
            //Create Mail Message Object with content that you want to send with mail.
            System.Net.Mail.MailMessage MyMailMessage = new System.Net.Mail.MailMessage(MAIL.MailFrom, MAIL.MailTo, MAIL.MailSubject, MAIL.MailBody);
            MyMailMessage.IsBodyHtml = true;
            //Proper Authentication Details need to be passed when sending email from gmail
            System.Net.NetworkCredential mailAuthentication = new
            System.Net.NetworkCredential("narendra@cubek.com", "Narendra2468Cubek");
            //get it from respective server.
            System.Net.Mail.SmtpClient mailClient = new System.Net.Mail.SmtpClient("mail.cubek.com", 110);
            //Enable SSL
            mailClient.EnableSsl = true;

            mailClient.UseDefaultCredentials = false;

            mailClient.Credentials = mailAuthentication;

            mailClient.Send(MyMailMessage);
            return true;
        }    
        catch
        {
            return false;
        }
    }
}

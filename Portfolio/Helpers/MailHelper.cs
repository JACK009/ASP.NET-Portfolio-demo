using System.Configuration;
using System.Net.Mail;

namespace Portfolio.Helpers
{
    public class MailHelper
    {
        public static void SendContactMessage(
            string message,
            string fromEmail,
            string fromName,
            string subject,
            MailAddress to = null,
            bool isHtml = true
        ){
            SmtpClient smtpClient = new SmtpClient();
            MailMessage mail = new MailMessage();

            if(null == to)
            {
                MailAddress mailAddress = new MailAddress(
                ConfigurationManager.AppSettings["emailAddress"],
                ConfigurationManager.AppSettings["emailName"]);
                mail.To.Add(mailAddress);
            }
            else
            {
                mail.To.Add(to);
            }

            mail.From = new MailAddress(fromEmail, fromName);  
            mail.Subject = subject;
            mail.Body = $"<p>Message from: {fromName} <br>E-mail from: {fromEmail}</p><p>Message:</p><p>{message}</p>";
            mail.IsBodyHtml = isHtml;

            try
            {
                smtpClient.Send(mail);
            }
            catch(SmtpException)
            {
                throw new  SmtpException();
            }
        }

    }
}
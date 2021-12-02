using System;
using System.Net.Mail;

namespace GameHeavenAPI.Helpers
{
    public class EmailHelper
    {
        //email : noreplygameheavenmail@gmail.com
        //pw : GameHeavenMailSender666*
        public bool SendEmail(string userEmail, string confirmationLink,string bodyMessage, string subject = "Confirm your email")
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("noreplygameheavenmail@gmail.com");
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = bodyMessage;
                
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("noreplygameheavenmail@gmail.com", "GameHeavenMailSender666*");
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Host = "smtp.gmail.com";
            client.Port = 587;

            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }
    }
}

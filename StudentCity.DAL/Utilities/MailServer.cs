using System.Net.Mail;

namespace StudentCity.DAL.Utilities
{
    public class MailServer
    {
        public static void SendMail(string to, string subject, string body)
        {

            string from = "AbdelrhmanYounis7979@gmail.com";
            //string host = "mail.StudentCityhouse.net";
            var mail = new MailMessage();
            mail.To.Add(to);
            mail.From = new MailAddress(from, "Student City", System.Text.Encoding.UTF8);
            mail.Subject = subject;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = body;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            var client = new SmtpClient
            {
                Credentials = new System.Net.NetworkCredential(from, "AY7201979ay"),
            };
            //Add the Creddentials- use your own email id and password
            //   client.EnableSsl = true; //Gmail works on Server Secured Layer
            client.Send(mail);
        }
    }
}

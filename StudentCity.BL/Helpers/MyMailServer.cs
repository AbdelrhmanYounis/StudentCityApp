using System.Net;
using System.Net.Mail;

namespace StudentCity.BL.Helpers
{
    public class MyMailServer
    {
        public static void SendMail(string to, string subject, string body)
        {
            var mail = new MailMessage();
            var loginInfo = new NetworkCredential("AbdelrhmanYounis7979@gmail.com", "AY7201979ay");
            mail.From = new MailAddress("AbdelrhmanYounis7979@gmail.com");
            mail.To.Add(new MailAddress(to));

            mail.Subject = subject;
            mail.Body = body;

            var smtpClint = new SmtpClient("smtp.gmail.com", 587);
            smtpClint.EnableSsl = true;
            smtpClint.Credentials = loginInfo;
            smtpClint.Send(mail);

            //string from = "AbdelrhmanYounis7979@gmail.com";
            //string host = "smtp.gmail.com";
            //var mail = new MailMessage();
            //mail.To.Add(to);
            //mail.From = new MailAddress(from, "Student City", System.Text.Encoding.UTF8);
            //mail.Subject = subject;
            //mail.SubjectEncoding = System.Text.Encoding.UTF8;
            //mail.Body = body;
            //mail.BodyEncoding = System.Text.Encoding.UTF8;
            //mail.IsBodyHtml = true;
            //mail.Priority = MailPriority.High;
            //var client = new SmtpClient
            //{
            //    Credentials = new System.Net.NetworkCredential(from, "AY7201979ay"),
            //    Host = host
            //};
            ////Add the Creddentials- use your own email id and password
            ////   client.EnableSsl = true; //Gmail works on Server Secured Layer
            //client.Send(mail);
            //string from = "server@talentdevs.com ";
            //string host = "mail.talentdevs.com";
            //var mail = new System.Net.Mail.MailMessage();
            //mail.To.Add(to);
            //mail.From = new MailAddress(from, "TalentDevs", System.Text.Encoding.UTF8);
            //mail.Subject = subject;
            //mail.SubjectEncoding = System.Text.Encoding.UTF8;
            //mail.Body = body;
            //mail.BodyEncoding = System.Text.Encoding.UTF8;
            //mail.IsBodyHtml = true;
            //mail.Priority = MailPriority.High;
            //var client = new SmtpClient
            //                 {
            //                     Credentials = new System.Net.NetworkCredential(@from, "talentdevs951753"),
            //                     Port = 26,
            //                     Host = host
            //                 };
            ////Add the Creddentials- use your own email id and password
            ////   client.EnableSsl = true; //Gmail works on Server Secured Layer
            //client.Send(mail);
        }
    }
}

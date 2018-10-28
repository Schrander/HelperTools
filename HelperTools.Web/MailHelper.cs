using System;
using System.Net;
using System.Net.Mail;

namespace HelperTools.Web
{
    public class MailHelper
    {
        public static string GMailing(MailAddress from, MailAddress to, string body, string subject, string login, string password)
        {
            return Mailing(from, to, body, subject, "smtp.gmail.com", 587, login, password, true, 20000);
        }

        public static string Mailing(MailAddress from, MailAddress to, string body, string subject, string host, int port, string login, string password, bool useSecure, int timeout = 20000)
        {
            using (MailMessage message = new MailMessage(from, to))
            {
                message.ReplyToList.Add(from);
                message.Subject = subject;
                message.Body = body;

                SmtpClient client = new SmtpClient
                {
                    Host = host,
                    Port = port,
                    EnableSsl = useSecure,
                    Timeout = timeout,
                    Credentials = new NetworkCredential(login, password)
                };

                try
                {
                    client.Send(message);
                    return "Mail has been successfully sent!";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }

            }
        }
    }
}

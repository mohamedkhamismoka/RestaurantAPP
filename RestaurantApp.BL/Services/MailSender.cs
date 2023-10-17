using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.BL.Services
{
    public   class MailSender
    {
        public static async Task<bool> sendmail(string mail,string title,string body)
        {
            try
            {

                var email = new MimeMessage()
                {
                    Sender = MailboxAddress.Parse("atiffahmykhamis@gmail.com"),
                    Subject = title



                };
                email.To.Add(MailboxAddress.Parse(mail));
                var builder = new BodyBuilder();


                builder.HtmlBody = body;
                email.Body = builder.ToMessageBody();


                email.From.Add(new MailboxAddress("Mohamed Restaurant", "atiffahmykhamis@gmail.com"));



                using (var smtp = new SmtpClient())
                {
                    smtp.Connect("smtp.gmail.com", 587, false);
                    smtp.Authenticate("atiffahmykhamis@gmail.com", "dxtqjlfupztfmyli");
                    await smtp.SendAsync(email);
                    smtp.Disconnect(true);
                }


                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}

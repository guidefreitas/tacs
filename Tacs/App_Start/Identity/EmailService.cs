using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Tacs.App_Start.Identity
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            return SendMail(message);
        }

        private Task SendMail(IdentityMessage message)
        {
            if(ConfigurationManager.AppSettings["Internet"] == "true")
            {
                var text = HttpUtility.HtmlEncode(message.Body);
                var msg = new MailMessage();
                msg.From = new MailAddress("admin@tacs.com.br", "Admin do portal");
                msg.To.Add(new MailAddress(message.Destination));
                msg.Subject = message.Subject;
                msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
                msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Html));

                var smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));
                var credentials = new NetworkCredential(ConfigurationManager.AppSettings["usuarioEmail"], ConfigurationManager.AppSettings["senhaEmail"]);
                smtpClient.Credentials = credentials;
                smtpClient.EnableSsl = true;
                smtpClient.Send(msg);
            }

            return Task.FromResult(0);
        }
    }
}

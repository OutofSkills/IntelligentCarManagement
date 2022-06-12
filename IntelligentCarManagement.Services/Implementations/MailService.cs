using Api.Services.Interfaces;
using Api.Services.Utils;
using Microsoft.Extensions.Options;
using Models.Data_Transfer_Objects;
using Models.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Api.Services.Implementations
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;

        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public void SendEmail(EmailDTO dto, IEmailTemplate template)
        {
            var email = new MailMessage();
            email.From = new MailAddress(_mailSettings.Mail);
            email.To.Add(new MailAddress(dto.SendTo));
            email.Subject = dto.Title;

            email.IsBodyHtml = true;
            email.Body = template.GenerateTemplate(dto);

            Guid guid = new();
            guid = Guid.NewGuid();
            String MessageID = "<" + guid.ToString() + ">";
            email.Headers.Add("Message-ID", MessageID); //Set Message-ID in Email Header

            using SmtpClient client = new();
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_mailSettings.Mail, _mailSettings.Password);
            client.Host = _mailSettings.Host;
            client.Port = _mailSettings.Port;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            try
            {
                client.Send(email);
            }catch (SmtpException ex)
            {
                _ = ex.InnerException;
            }
        }
    }
}

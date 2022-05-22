using Api.Services.Utils;
using Models.Data_Transfer_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Api.Services.Interfaces
{
    public interface IMailService
    {
        void SendEmail(EmailDTO message, IEmailTemplate template);
    }
}

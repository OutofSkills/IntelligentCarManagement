using Microsoft.AspNetCore.Hosting;
using Models.Data_Transfer_Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.Utils.Email_Templates
{
    public class ApplicationAcceptedTemplate: IEmailTemplate
    {
        private readonly IWebHostEnvironment env;

        public ApplicationAcceptedTemplate(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public string GenerateTemplate(EmailDTO email)
        {
            //Fetching Email Body Text from EmailTemplate File.  
            string rootPath = System.IO.Path.GetDirectoryName(env.ContentRootPath);

            string filePath = Path.Combine(rootPath, "/IntelligentCarManagement.Api/Static Files/Email Templates/ApplicationAccepted.html");
            using StreamReader str = new(filePath);
            string mailText = str.ReadToEnd();

            //Repalce [newusername] = signup user name   
            mailText = mailText.Replace("[username]", email.SendTo);
            mailText = mailText.Replace("[type_of_action]", email.Action);
            mailText = mailText.Replace("[content]", email.Content);

            return mailText;
        }
    }
}

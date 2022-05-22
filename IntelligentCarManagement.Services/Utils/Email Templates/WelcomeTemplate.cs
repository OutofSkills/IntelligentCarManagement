using Microsoft.AspNetCore.Hosting;
using Models.Data_Transfer_Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.Utils
{
    public class WelcomeTemplate : IEmailTemplate
    {
        private readonly IWebHostEnvironment env;

        public WelcomeTemplate(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public string GenerateTemplate(EmailDTO email)
        {
            //Fetching Email Body Text from EmailTemplate File.  
            string rootPath = System.IO.Path.GetDirectoryName(env.ContentRootPath);

            string filePath = Path.Combine(rootPath, "IntelligentCarManagement.Services/Utils/Email Templates/Welcome.html");
            using StreamReader str = new(filePath);
            string mailText = str.ReadToEnd();

            //Repalce [newusername] = signup user name   
            mailText = mailText.Replace("[newusername]", email.SendTo);
            mailText = mailText.Replace("[type_of_action]", email.Action);

            return mailText;
        }
    }
}

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
        private readonly IWebHostEnvironment environment;

        public WelcomeTemplate(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        public string GenerateTemplate(EmailDTO email)
        {
            var root = environment.ContentRootPath;

            string filePath = Path.Combine(root, "wwwroot\\StaticFiles\\Welcome.html");
            using StreamReader str = new(filePath);
            string mailText = str.ReadToEnd();

            //Repalce [newusername] = signup user name   
            mailText = mailText.Replace("[newusername]", email.SendTo);
            mailText = mailText.Replace("[type_of_action]", email.Action);

            return mailText;
        }
    }
}

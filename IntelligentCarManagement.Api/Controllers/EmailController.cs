using Api.Services.Interfaces;
using Api.Services.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Data_Transfer_Objects;
using System.Net.Mail;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IMailService emailService;

        public EmailController(IMailService emailService)
        {
            this.emailService = emailService;
        }

        [HttpPost]
        public void Send(EmailDTO dTO, [FromServices] IWebHostEnvironment env)
        {
            emailService.SendEmail(dTO, new WelcomeTemplate(env));
        }
    }
}

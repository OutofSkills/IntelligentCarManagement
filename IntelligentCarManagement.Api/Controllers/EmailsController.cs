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
    public class EmailsController : ControllerBase
    {
        private readonly IMailService emailService;

        public EmailsController(IMailService emailService)
        {
            this.emailService = emailService;
        }

        [HttpPost]
        public void Send([FromBody] EmailDTO dTO, [FromServices] IWebHostEnvironment environment)
        {
            emailService.SendEmail(dTO, new GeneralEmailTemplate(environment));
        }
    }
}

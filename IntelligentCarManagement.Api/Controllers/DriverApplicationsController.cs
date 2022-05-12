using Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverApplicationsController : ControllerBase
    {
        private readonly IDriverApplicationsService applicationsService;

        public DriverApplicationsController(IDriverApplicationsService applicationsService)
        {
            this.applicationsService = applicationsService;
        }

        [HttpPost]
        public void Post(DriverApplicationDTO model)
        {
            applicationsService.Apply(model);
        }

        [HttpGet]
        public async Task<IEnumerable<DriverApplicationDTO>> GetAllAsync()
        {
            return await applicationsService.GetAll();
        }
    }
}

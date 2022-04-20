using Api.Services.Utils;
using IntelligentCarManagement.Api.Helpers;
using IntelligentCarManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Data_Transfer_Objects;
using Models.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Api.Controllers
{
    [EnableCors]
    [Authorize(Roles = nameof(RoleName.ADMIN) + "," + nameof(RoleName.CLIENT))]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientsService usersService;

        public ClientsController(IClientsService usersService)
        {
            this.usersService = usersService;
        }


        [HttpGet]
        [Route("byEmail")]
        public ClientDTO GetByEmailAsync([FromQuery] string email)
        {
            var client = usersService.Get(email);

            return client;
        }

        [HttpGet]
        [Route("byId")]
        public async Task<ClientDTO> GetByIdAsync([FromQuery] int clientId)
        {
            var client = await usersService.GetAsync(clientId);

            return client;
        }

        [HttpGet]
        public async Task<IEnumerable<ClientDTO>> GetAsync()
        {
            return await usersService.GetAllAsync();
        }
        [HttpPut]
        public async Task<ClientDTO> UpdatAsync([FromQuery] int id, [FromBody] ClientDTO client)
        {
            return await usersService.UpdateAsync(id, client);
        }

        [HttpPost]
        public ClientDTO Add(ClientDTO client)
        {
            return usersService.Add(client);
        }
    }
}

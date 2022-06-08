using Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Data_Transfer_Objects;
using Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminAccountController : ControllerBase
    {
        private readonly IAdminAccountService _adminAccountService;
        private readonly IAdminsService _adminsService;

        public AdminAccountController(IAdminAccountService adminAccountService, IAdminsService adminsService)
        {
            _adminAccountService = adminAccountService;
            _adminsService = adminsService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<LoginResponse> Login([FromBody] LoginModel loginModel)
        {
            return await _adminAccountService.Login(loginModel);
        }

        [HttpPost]
        [Route("register")]
        public async Task Register([FromBody] AdminRegisterModel registerModel)
        {
            await _adminAccountService.Register(registerModel);
            return;
        }

        [HttpGet]
        [Route("id")]
        public async Task<UserBaseDTO> Get([FromQuery] int id)
        {
            return await _adminsService.Get(id);
        }

        [HttpGet]
        public async Task<IEnumerable<UserBaseDTO>> GetAll()
        {
            return await _adminsService.GetAll();
        }
    }
}

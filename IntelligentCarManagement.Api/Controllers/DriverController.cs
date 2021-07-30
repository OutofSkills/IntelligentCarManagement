using IntelligentCarManagement.Models;
using IntelligentCarManagement.DataAccess.UnitsOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IUnitOfWork _repository;

        public DriverController(IUnitOfWork repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("getDriver/{carId}")]
        public IActionResult GetDriver(int carId)
        {
            return Ok(_repository.DriversRepo.GetById(carId));
        }
    }
}

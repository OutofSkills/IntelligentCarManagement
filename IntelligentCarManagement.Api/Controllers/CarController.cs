using IntelligentCarManagement.Models;
using IntelligentCarManagement.DataAccess.UnitsOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntelligentCarManagement.Api.Services;

namespace IntelligentCarManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService carService;

        public CarController(ICarService carService)
        {
            this.carService = carService;
        }

        [HttpGet]
        [Route("getCars")]
        public IActionResult GetCars() 
        {
            return Ok(carService.GetAllCars());
        }
    }
}

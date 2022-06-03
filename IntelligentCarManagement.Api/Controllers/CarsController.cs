using IntelligentCarManagement.DataAccess.UnitsOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using IntelligentCarManagement.Services;
using IntelligentCarManagement.Api.Helpers;
using System.Net.Http;
using System.Net;
using Models.DTOs;
using Models;
using Models.Data_Transfer_Objects;

namespace IntelligentCarManagement.Api.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarsService carService;

        public CarsController(ICarsService carService)
        {
            this.carService = carService;
        }

        [HttpGet]
        public async Task<IEnumerable<CarDTO>> GetAllAsync() 
        {
            var result = await carService.GetAllAsync();

            return result;
        }
    
        [HttpPost]
        public void AddCar([FromBody] CarDTO car)
        {
            carService.Create(car);
        }

        [HttpGet]
        [Route("assign")]
        public void AssignCar([FromQuery] int carId, [FromQuery] string driverEmail)
        {
            carService.AssignDriver(carId, driverEmail);
        }
    }
}

using IntelligentCarManagement.Models;
using IntelligentCarManagement.DataAccess.UnitsOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using IntelligentCarManagement.Services;
using IntelligentCarManagement.Models.NotMapped_Models;
using IntelligentCarManagement.Api.Helpers;

namespace IntelligentCarManagement.Api.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService carService;

        public CarsController(ICarService carService)
        {
            this.carService = carService;
        }

        [HttpGet]
        [Route("GetCars")]
        public async Task<IActionResult> GetCarsAsync([FromQuery] Pagination pagination) 
        {
            var collection = await carService.GetAllCars();

            HttpContext.InsertPaginationParameterInResponse(collection, pagination.NumberOfRecords);

            return Ok(collection.Paginate(pagination).ToList());
        }

        [HttpPost]
        [Route("removeCar")]
        public async Task<IActionResult> RemoveCarAsync([FromForm] int carId)
        {
            var result = await carService.RemoveCarAsync(carId);

            if (result is true)
            {
                return Ok();
            }

            return BadRequest();
        }
    
        [HttpPost]
        [Route("newCar")]
        public IActionResult AddNewCarAsync([FromBody] Car car)
        {
            var result = carService.AddCar(car);

            if (result is true)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}

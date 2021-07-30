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
    public class CarController : ControllerBase
    {
        private readonly IUnitOfWork _repository;

        public CarController(IUnitOfWork repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("getCars")]
        public IActionResult GetCars() 
        {
            var cars = _repository.CarsRepo.GetAll();

            return Ok(cars);
        }
    }
}

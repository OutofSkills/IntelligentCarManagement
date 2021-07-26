using IntelligentCarManagement.DataModels;
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
        [HttpGet]
        [Route("getCars")]
        public IActionResult GetCars() 
        {
            List<Car> cars = new List<Car>
            {
                new Car { Id = 1, Brand = "Dacia", Model = "Logan", FuelType = "Petrol", IsAvailable = true, 
                    Driver = new Driver{Id = 1, FirstName = "Micu", LastName = "Daniel", Age = 29, Email = "something@gmail.com",
                        Address = new UserAddress{ Id = 1, City = "Craiova", County = "Dolj", Street = "Amaradi 59" } } }
            };

            return Ok(cars);
        }
    }
}

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
    public class DriverController : ControllerBase
    {
        [HttpGet]
        [Route("getDriver/{carId}")]
        public IActionResult GetDriver(int carId)
        {
            Car car = new Car
            {
                Id = 1,
                Brand = "Dacia",
                Model = "Logan",
                FuelType = "Petrol",
                IsAvailable = true,
                Driver = new Driver
                {
                    Id = 1,
                    FirstName = "Micu",
                    LastName = "Daniel",
                    Username = "Dani777",
                    Age = 29,
                    Email = "something@gmail.com",
                    Address = new UserAddress { Id = 1, City = "Craiova", County = "Dolj", Street = "Amaradi 59" },
                    Experience = 5
                }
            };

            List<Driver> drivers = new List<Driver>
            {
                new Driver
                {
                    Id = 1,
                    FirstName = "Micu",
                    LastName = "Daniel",
                    Age = 29,
                    Username = "Dani777",
                    Email = "something@gmail.com",
                    Address = new UserAddress { Id = 1, City = "Craiova", County = "Dolj", Street = "Amaradi 59" },
                    Experience = 5,
                    Rating = 4.9f,
                    DeservedClients = 2,
                    Car = car
                }
            };

            return Ok(drivers.Where(driver => driver.Car.Id == carId).FirstOrDefault());
        }
    }
}

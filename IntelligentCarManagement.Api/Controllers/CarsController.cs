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
using Models.View_Models;
using Models;

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
        [Route("get-cars")]
        public async Task<IActionResult> GetCarsAsync([FromQuery] Pagination pagination) 
        {
            var collection = await carService.GetAllCars();

            HttpContext.InsertPaginationParameterInResponse(collection, pagination.NumberOfRecords);

            return Ok(collection.Paginate(pagination).ToList());
        }

        [HttpDelete]
        [Route("remove-car")]
        public async Task<IActionResult> RemoveCarAsync([FromQuery] int id)
        {
            try
            {
                await carService.RemoveCarAsync(id);
            }
            catch (Exception e)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(e.Message),
                    ReasonPhrase = "Remove car error"

                };

                throw new System.Web.Http.HttpResponseException(response);
            }

            return Ok();
        }
    
        [HttpPost]
        [Route("new-car")]
        public IActionResult AddNewCar([FromBody] Car car)
        {
            try
            {
                carService.AddCar(car);
            }
            catch (Exception e)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(e.Message),
                    ReasonPhrase = "Insert car error"

                };

                throw new System.Web.Http.HttpResponseException(response);
            }

            return Ok();
        }

        [HttpPut]
        [Route("edit-car")]
        public IActionResult EditCar([FromBody] Car car)
        {
            try
            {
                carService.EditCar(car);
            }
            catch (Exception e)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(e.Message),
                    ReasonPhrase = "Edit car error"

                };

                throw new System.Web.Http.HttpResponseException(response);
            }

            return Ok();
        }
    }
}

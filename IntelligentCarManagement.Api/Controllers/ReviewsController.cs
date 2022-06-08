using IntelligentCarManagement.Api.Services;
using IntelligentCarManagement.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IDriversService driversService;
        private readonly IRidesService ridesService;

        public ReviewsController(IDriversService driversService, IRidesService ridesService)
        {
            this.driversService = driversService;
            this.ridesService = ridesService;
        }

        [HttpPost]
        [Route("client")]
        public async Task ReviewClientAsync([FromQuery] int rideId, double rating)
        {
            await driversService.RateClientAsync(rideId, rating);
        }

        [HttpPost]
        [Route("ride/rating")]
        public async Task ReviewDriverAsync([FromQuery] int rideId, double rating)
        {
            await ridesService.RateAsync(rideId, rating);
        }

        [HttpPost]
        [Route("ride/accuracy")]
        public async Task EvaluateDriverAccuracyAsync([FromQuery] int rideId, [FromQuery] double accuracy)
        {
            await ridesService.EvaluateAccuracy(rideId, accuracy);
        }
    }
}

using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.Utils
{
    internal static class RatingCalculator
    {
        public static double CalculateRating(Client client)
        {
            var reviews = client.DriverReviews;
            if (reviews is null || reviews.Count is 0)
                return 0;

            var rating = reviews.Sum(r => r.Rating) / client.DriverReviews.Count;
            rating = Math.Round(rating, 1);

            return rating;
        }

        public static double GetDriverRating(IEnumerable<Ride> rides, int driverId)
        {
            var ratedRides = rides.Where(r => r.Review is not null && r.Review.Rating != null && r.DriverId == driverId).ToList();
            double driverRating = 0.0;
            if (ratedRides.Any())
            {
                driverRating = (double)(ratedRides.Sum(r => r.Review.Rating) / ratedRides.Count);
            }

            return driverRating;
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Api.Helpers
{
    public static class HttpContextExtensions
    {
        public static void InsertPaginationParameterInResponse<T>(this HttpContext httpContext, IEnumerable<T> collection, int recordsPerPage)
        {
            double count = collection.Count();
            double numberOfPages = Math.Ceiling(count/recordsPerPage);
            httpContext.Response.Headers.Add("numberOfPages", new(numberOfPages.ToString()));
        }
    }
}

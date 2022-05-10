using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Api.Helpers
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Paginate<T>(this IEnumerable<T> collection, Pagination pagination)
        {
            return collection.Skip((pagination.Page - 1) * pagination.NumberOfRecords).Take(pagination.NumberOfRecords);
        }
    }
}

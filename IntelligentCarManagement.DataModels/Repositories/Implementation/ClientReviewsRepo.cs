using Api.DataAccess.Repositories.Interfaces;
using IntelligentCarManagement.DataAccess;
using IntelligentCarManagement.DataAccess.Repositories.GenericRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.DataAccess.Repositories.Implementation
{
    public class ClientReviewsRepo : Repo<ClientReview>, IClientReviewsRepo
    {
        public ClientReviewsRepo(CarMngContext _context) : base(_context)
        {
        }
    }
}

﻿using IntelligentCarManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Services
{
    public interface IRolesService
    {
        Task<IEnumerable<Role>> GetRolesAsync();
    }
}

using IntelligentCarManagement.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Services
{
    public class RolesService : IRolesService
    {
        private readonly HttpClient httpClient;

        public RolesService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            var roles = await httpClient.GetJsonAsync<Role[]>("/api/Roles/get-roles");

            return roles;
        }
    }
}

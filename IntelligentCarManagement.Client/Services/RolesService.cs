using Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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

        public async Task<bool> AddRoleAsync(Role newRole)
        {
            var result = await httpClient.PostAsJsonAsync("/api/Roles/add-role", newRole);

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> EditRoleAsync(Role role)
        {
            var result = await httpClient.PutAsJsonAsync("/api/Roles/edit-role", role);

            return result.IsSuccessStatusCode;
        }
        public async Task<HttpResponseMessage> GetRolesAsync(int page = 1, int recordsPerPage = 10)
        {
            var response = await httpClient.GetAsync($"/api/Roles/get-roles?page={page}&recordsPerPage={recordsPerPage}");
            return response;
        }

        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            return await httpClient.GetFromJsonAsync<Role[]>("/api/Roles/get-all-roles");
        }

        public async Task<bool> RemoveRoleAsync(int id)
        {
            var result = await httpClient.DeleteAsync($"/api/Roles/remove-role?id={id}");

            return result.IsSuccessStatusCode;
        }
    }
}

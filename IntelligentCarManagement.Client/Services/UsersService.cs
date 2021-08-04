﻿using IntelligentCarManagement.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Services
{
    public class UsersService : IUsersService
    {
        private readonly HttpClient httpClient;

        public UsersService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await httpClient.GetJsonAsync<User[]>("/api/Users/GetUsers");
        }

        public bool NewUser(User user)
        {
            return true;
        }
    }
}

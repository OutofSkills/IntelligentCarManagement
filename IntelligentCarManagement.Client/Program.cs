using Blazorise;
using Blazorise.Icons.FontAwesome;
using IntelligentCarManagement.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:41427") });
            builder.Services.AddScoped<ICarService, CarService>();
            builder.Services.AddScoped<IDriverService, DriverService>();
            builder.Services.AddScoped<IUsersService, UsersService>();

            await builder.Build().RunAsync();
        }
    }
}

using Client.Authentication;
using ClientUI.Services;
using ClientUI.Services.Cars;
using ClientUI.Services.Clients;
using ClientUI.Services.Emails;
using ClientUI.Services.Notifications;
using ClientUI.Services.Rides;
using ClientUI.Services.Roles;
using ClientUI.Services.Statuses;
using IntelligentCarManagement.Authentication;
using Microsoft.AspNetCore.Components.Authorization;

namespace ClientUI.Utils
{
    public static class ProgramExtension
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IDriverService, DriverService>();
            services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
            services.AddScoped<ICarsService, CarsService>();
            services.AddScoped<IClientsService, ClientsService>();
            services.AddScoped<IRidesService, RidesService>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<IStatusesService, StatusesService>();
            services.AddScoped<INotificationsService, NotificationsService>();
            services.AddScoped<IEmailsService, EmailsService>();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}

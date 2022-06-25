using Api.Services.Implementations;
using Api.Services.Interfaces;
using CorePush.Apple;
using CorePush.Google;
using IntelligentCarManagement.Api.Services;
using IntelligentCarManagement.DataAccess.UnitsOfWork;
using IntelligentCarManagement.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models.Tools;

namespace Api.Helpers
{
    public static class StartupExtension
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICarsService, CarsService>();
            services.AddScoped<IDriversService, DriversService>();
            services.AddScoped<IClientsService, ClientsService>();
            services.AddScoped<IAdminsService, AdminsService>();
            services.AddScoped<ITokenBuilder, TokenBuilder>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<IStatusesService, StatusesService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IRidesService, RidesService>();
            services.AddScoped<IClientsAccountService, ClientsAccountService>();
            services.AddScoped<IDriversAccountService, DriversAccountService>();
            services.AddScoped<IAdminAccountService, AdminAccountService>();
            services.AddScoped<IDriverApplicationsService, DriverApplicationsService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddHttpClient<FcmSender>();
            services.AddHttpClient<ApnSender>();
            // SMTP email client
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddScoped<IMailService, MailService>();

            return services;
        }
    }
}

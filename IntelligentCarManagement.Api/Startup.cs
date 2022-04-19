using IntelligentCarManagement.Api.Services;
using IntelligentCarManagement.DataAccess;
using IntelligentCarManagement.DataAccess.UnitsOfWork;
using IntelligentCarManagement.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models;
using Api.Services.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Services.Interfaces;
using Api.Services.Implementations;

namespace IntelligentCarManagement.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowOrigin",
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:44304", "http://localhost:41427")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod()
                                            .AllowCredentials()
                                            .WithExposedHeaders("*"); ;
                    });
            });

            services.AddIdentity<UserBase, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password = new PasswordOptions
                {
                    RequireDigit = true,
                    RequiredLength = 6,
                    RequireLowercase = false,
                    RequireUppercase = false,
                    RequireNonAlphanumeric = false
                };
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

            }).AddEntityFrameworkStores<CarMngContext>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
                .AddJwtBearer("JwtBearer", JwtBearerOptions =>
                {
                    JwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecretKey")),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(5)
                    };
                });

            services.AddDbContext<CarMngContext>(options =>
                options.UseLazyLoadingProxies()
                       .UseSqlServer(Configuration.GetConnectionString("CarMngmentConnection"), builder =>
                       {
                           builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(1), null);
                       }));

            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo { Title= "Intelligent Car Management API", Version = "V1" });
            });

            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); 

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IDriversService, DriversService>();
            services.AddScoped<IClientsService, ClientsService>();
            services.AddScoped<ITokenBuilder, TokenBuilder>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<IAccountStatusService, AccountStatusService>();
            services.AddScoped<IDriverStatusService, DriverStatusService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IRidesService, RidesService>();
            services.AddScoped<IClientsAccountService, ClientsAccountService>();
            services.AddScoped<IDriversAccountService, DriversAccountService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));

            app.UseCors("AllowOrigin");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(
                x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "IntelligentCarManagement API V1")
                );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

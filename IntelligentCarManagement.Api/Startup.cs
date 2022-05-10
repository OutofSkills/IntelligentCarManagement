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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IO;
using Models.Helpers;
using CorePush.Google;
using CorePush.Apple;
using Models.Tools;
using Microsoft.Extensions.Hosting.Internal;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System.Diagnostics;

namespace IntelligentCarManagement.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            HostingEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostingEnvironment { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Build configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowOrigin",
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:44304", "https://localhost:44398")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod()
                                            .AllowCredentials()
                                            .WithExposedHeaders("*"); ;
                    });
            });

            // Get the secret key from appsettings
            var apiSettingsSection = config.GetSection("ApiSettings");
            services.Configure<ApiSettings>(apiSettingsSection);

            // Configure FIREBASE ADMIN
            var pathToKey = Path.Combine(Directory.GetCurrentDirectory(), "firebase-key.json");

            if (HostingEnvironment.IsEnvironment("local"))
                pathToKey = Path.Combine(Directory.GetCurrentDirectory(), "firebase-key.json");

            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile(pathToKey)
            });


            var apiSettings = apiSettingsSection.Get<ApiSettings>();
            var key = Encoding.ASCII.GetBytes(apiSettings.SecretKey);

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
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, JwtBearerOptions =>
                {
                    JwtBearerOptions.RequireHttpsMetadata = false;
                    JwtBearerOptions.SaveToken = true;
                    JwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true
                    };
                });

            services.AddDbContext<CarMngContext>(options =>
                options.UseLazyLoadingProxies()
                       .UseSqlServer(Configuration.GetConnectionString("CarMngmentConnection"), builder =>
                       {
                           builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(1), null);
                       }));

            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Intelligent Car Management API", Version = "V1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });


            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); 

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IDriversService, DriversService>();
            services.AddScoped<IClientsService, ClientsService>();
            services.AddScoped<IAdminsService, AdminsService>();
            services.AddScoped<ITokenBuilder, TokenBuilder>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<IAccountStatusService, AccountStatusService>();
            services.AddScoped<IDriverStatusService, DriverStatusService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IRidesService, RidesService>();
            services.AddScoped<IClientsAccountService, ClientsAccountService>();
            services.AddScoped<IDriversAccountService, DriversAccountService>();
            services.AddScoped<IAdminAccountService, AdminAccountService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddHttpClient<FcmSender>();
            services.AddHttpClient<ApnSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseHttpsRedirection();

            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));

            app.UseCors("AllowOrigin");

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

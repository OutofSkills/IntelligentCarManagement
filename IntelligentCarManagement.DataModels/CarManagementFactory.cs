using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IntelligentCarManagement.DataAccess
{
    public class CarManagement : IDesignTimeDbContextFactory<CarMngContext>
    {
        public CarMngContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json")
                 .Build();
            var builder = new DbContextOptionsBuilder<CarMngContext>();
            var connectionString = configuration.GetConnectionString("CarMngmentConnection");
            builder.UseSqlServer(connectionString);

            return new CarMngContext(builder.Options);
        }
    }
}

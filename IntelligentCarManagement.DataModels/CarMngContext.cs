using IntelligentCarManagement.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentCarManagement.DataAccess
{
    public class CarMngContext: DbContext
    {
        public CarMngContext(DbContextOptions options): base(options){ }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().ToTable("Car");
            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<Driver>().ToTable("Driver");
            modelBuilder.Entity<UserAddress>().ToTable("Address");
            modelBuilder.Entity<User>().ToTable("User");
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
    }
}

using Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentCarManagement.DataAccess
{
    public class CarMngContext: IdentityDbContext<UserBase, Role, int>
    {
        public CarMngContext(DbContextOptions options): base(options){ }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().ToTable("Cars");
            modelBuilder.Entity<Client>().ToTable("Clients");
            modelBuilder.Entity<Driver>().ToTable("Drivers");
            modelBuilder.Entity<ApplicationStatus>().ToTable("ApplicationStatuses");
            modelBuilder.Entity<UserAddress>().ToTable("Addresses");

            modelBuilder.Entity<UserRole>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(r => r.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany() // If you add `public ICollection<UserRoles> UserRoles{ get; set; }` navigation property to Role model class then replace `.WithMany()` with `.WithMany(b => b.UserRoles)`
                .HasForeignKey(ur => ur.RoleId);


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Car> Cars { get; set; }
        public override DbSet<UserBase> Users { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<ApplicationStatus> ApplicationStatuses { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationCategory> NotificationCategories { get; set; }
        public DbSet<Ride> Rides { get; set; }
        public DbSet<DriverApplication> DriverApplications { get; set; } 
        public DbSet<RideState> RideStates { get; set; }
    }
}

using IntelligentCarManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentCarManagement.DataAccess
{
    public class CarMngContext: IdentityDbContext<User, Role, int>
    {
        public CarMngContext(DbContextOptions options): base(options){ }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().ToTable("Car");
            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<Driver>().ToTable("Driver");
            modelBuilder.Entity<UserAddress>().ToTable("Address");
            modelBuilder.Entity<User>().ToTable("User");

            //modelBuilder.Entity<UserRole>().HasKey(ur => ur.Id);

            //modelBuilder.Entity<UserRole>()
            //    .HasOne(ur => ur.Role)
            //    .WithMany(ur => ur.Users)
            //    .HasForeignKey(ur => ur.RoleId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<UserRole>()
            //    .HasOne(ur => ur.User)
            //    .WithMany(ur => ur.Roles)
            //    .HasForeignKey(ur => ur.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);

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
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<UserRole> UsersRoles { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Doit.Models;
using Doit.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Doit.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {

        public DbSet<Gender> Genders { get; set; }
        public DbSet<Meditation> Meditations { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<TodoDescription> TodoDescriptions { get; set; }
        public DbSet<Login> Logins { get; set; }






        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<IdentityRole>().ToTable("Roles");

            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");

            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");

            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");

            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");

            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

          
        }

      
    }
      
}

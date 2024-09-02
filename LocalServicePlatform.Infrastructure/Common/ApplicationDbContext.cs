using LocalServicePlatform.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalServicePlatform.Infrastructure.Common
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
       public DbSet<Services> Services { get; set; }

        public DbSet<ServiceCategories> ServiceCategories { get; set; }
        public DbSet<Post> Post { get; set; }

        public DbSet<Bookings> Bookings { get; set; }

        public DbSet<PopularPro> PopularPros { get; set; }

        public DbSet<TaskersUpdate> TaskersUpdate { get; set; }

        public DbSet<TaskersService> TaskersService { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TaskersUpdate>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<TaskersService>()
                .HasKey(ts => new { ts.TaskersUpdateId, ts.ServiceId });
            modelBuilder.Entity<TaskersService>()
                .HasOne(ts => ts.Services)
                .WithMany()
                .HasForeignKey(ts => ts.ServiceId);
        }





    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DuHoc.Models;

namespace DuHoc.Data
{
    public class DuHocContext : DbContext
    {
        public DuHocContext(DbContextOptions<DuHocContext> options)
            : base(options)
        {
        }

        public DbSet<DuHoc.Models.Country> Country { get; set; }
        public DbSet<DuHoc.Models.User> User { get; set; }
        public DbSet<DuHoc.Models.Profile> Profile { get; set; }
        public DbSet<DuHoc.Models.University> University { get; set; }
        public DbSet<DuHoc.Models.Course> Course { get; set; }
        public DbSet<DuHoc.Models.NewsPost> NewsPost { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.user_id);

            modelBuilder.Entity<Profile>()
                .HasKey(p => p.user_id);

            modelBuilder.Entity<Course>()
                .HasKey(c => c.Course_Id);

            modelBuilder.Entity<University>()
                .HasKey(u => u.University_Id);

            modelBuilder.Entity<Country>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<NewsPost>()
                .HasKey(c => c.News_Id);

            // Define the one-to-one relationship between User and Profile
            modelBuilder.Entity<User>()
                .HasOne(u => u.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<Profile>(p => p.user_id);

            // Define the many-to-one relationship between Course and University
            modelBuilder.Entity<Course>()
                .HasOne(c => c.University)
                .WithMany(u => u.Course)
                .HasForeignKey(c => c.University_Id)
                .OnDelete(DeleteBehavior.Restrict); // You can change the delete behavior as needed

            // Define the one-to-many relationship between Country and University
            modelBuilder.Entity<Country>()
                .HasMany(c => c.University)
                .WithOne(u => u.Country)
                .HasForeignKey(u => u.Id)
                .OnDelete(DeleteBehavior.Restrict); // You can change the delete behavior as needed

            // Additional configurations and mappings...

            base.OnModelCreating(modelBuilder);
        }
    }
}
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
        public DbSet<DuHoc.Models.Appointment> Appointment { get; set; }
        public DbSet<DuHoc.Models.ParentComment> ParentComment { get; set; }
        public DbSet<DuHoc.Models.ChildComment> ChildComment { get; set; }



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

            modelBuilder.Entity<Appointment>()
                .HasKey(u => u.Appointment_Id);

            modelBuilder.Entity<ParentComment>()
                .HasKey(u => u.ParentComment_Id);

            modelBuilder.Entity<ChildComment>()
                .HasKey(u => u.Comment_Id);

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
                .OnDelete(DeleteBehavior.Restrict); 

            // Define the one-to-many relationship between Country and University
            modelBuilder.Entity<Country>()
                .HasMany(c => c.University)
                .WithOne(u => u.Country)
                .HasForeignKey(u => u.Id)
                .OnDelete(DeleteBehavior.Restrict);

            // Define the one-to-many relationship between User and Appointment
            modelBuilder.Entity<Appointment>()
                .HasOne(u => u.User)
                .WithMany(p => p.Appointment)
                .HasForeignKey(p => p.user_id)
                .OnDelete(DeleteBehavior.Restrict);

            // Define the one-to-many relationship between User and Appointment
            modelBuilder.Entity<User>()
                .HasMany(u => u.ParentComments)
                .WithOne(pc => pc.User)
                .HasForeignKey(pc => pc.user_id)
                .OnDelete(DeleteBehavior.Restrict);

            // Define the one-to-many relationship between User and Appointment
            modelBuilder.Entity<User>()
                .HasMany(u => u.ChildComments)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.user_id)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
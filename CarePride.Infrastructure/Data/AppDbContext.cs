using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using CarePride.Domain;
using CarePride.Domain.Entities;

namespace CarePride.Infrastructure.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) // constructor
        {

        }
        // set DBSET properties here
        public DbSet<Student> students { get; set; }
        public DbSet<Teacher> teachers { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Role> roles { get; set; }
        // still alot of these left, for subjects, cass, etc etc


        // configure entity relationships, constraints and seedings in the model below::
        public override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // STUDENT
            modelBuilder.Entity<Student>(entity =>
            {

                entity.HasKey(s => s.StudentId);
                entity.Property(s => s.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(s => s.LastName).IsRequired().HasMaxLength(50);
                entity.Property(s => s.Email).IsRequired().HasMaxLength(100);
                entity.HasIndex(s => s.Email).IsUnique();
                entity.Property(s => s.PhoneNumber).HasMaxLength(15);
                entity.Property(s => s.Address).HasMaxLength(200);
                entity.Property(s => s.Gender).HasMaxLength(20);
                entity.Property(s => s.EnrollmentDate).IsRequired();
                entity.Property(s => s.DateOfBirth).IsRequired();
                entity.Property(s => s.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(s => s.CreatedAt).IsRequired();
                entity.Property(s => s.UpdatedAt);
     
            });
            // TEACHER
            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasKey(t => t.TeacherId);
                entity.Property(t => t.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(t => t.LastName).IsRequired().HasMaxLength(50);
                entity.Property(t => t.Email).IsRequired().HasMaxLength(100);
                entity.HasIndex(t => t.Email).IsUnique();
                entity.Property(t => t.PhoneNumber).HasMaxLength(15);
                entity.Property(t => t.HireDate).IsRequired();
                entity.Property(t => t.IsActive).IsRequired().HasDefaultValue(true);

            });
            // CLASS






        }
    }
}

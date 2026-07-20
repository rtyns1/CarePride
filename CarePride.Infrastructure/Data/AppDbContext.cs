using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        public DbSet<Class> Classes { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<ClassSubject> ClassSubjects { get; set; }


        // configure entity relationships, constraints and seedings in the model below::
        protected override void OnModelCreating(ModelBuilder modelBuilder)
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
            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasKey(c => c.ClassId);
                entity.Property(c => c.ClassName).IsRequired().HasMaxLength(50);
                entity.Property(c => c.GradeLevel).IsRequired();
                entity.Property(c => c.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(c => c.CreatedAt).IsRequired();
                entity.Property(c => c.UpdatedAt).IsRequired();

                //relationships that teacher has with class
                entity.HasOne<Teacher>()
                     .WithMany()
                     .HasForeignKey(c => c.TeacherId)
                     .OnDelete(DeleteBehavior.Restrict);

            });
            modelBuilder.Entity<Subject>(entity => 
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
                entity.Property(s => s.Code).IsRequired().HasMaxLength(20);
                entity.HasIndex(s => s.Code).IsUnique();
                entity.Property(s => s.Description).HasMaxLength(500);

            });
            // ---- ENROLLMENT (Junction Table) ----
            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => e.EnrollmentId);
                entity.Property(e => e.EnrollmentDate).IsRequired();
                entity.Property(e => e.IsActive).IsRequired().HasDefaultValue(true);

                // Relationship: Enrollment belongs to one Student
                entity.HasOne<Student>()
                      .WithMany()
                      .HasForeignKey(e => e.StudentId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Relationship: Enrollment belongs to one Class
                entity.HasOne<Class>()
                      .WithMany()
                      .HasForeignKey(e => e.ClassId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ---- USER ----
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
                entity.HasIndex(u => u.Username).IsUnique();
                entity.Property(u => u.PasswordHash).IsRequired().HasMaxLength(255);
                entity.Property(u => u.RefreshToken).HasMaxLength(255);

                // Relationship: User belongs to one Role
                entity.HasOne<Role>()
                      .WithMany()
                      .HasForeignKey(u => u.RoleId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ---- ROLE ----
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Name).IsRequired().HasMaxLength(20);
                entity.HasIndex(r => r.Name).IsUnique();
            });

            // ---- CLASS SUBJECT (Junction Table) ----
            modelBuilder.Entity<ClassSubject>(entity =>
            {
                entity.HasKey(cs => cs.Id);

                // Relationship: ClassSubject belongs to one Class
                entity.HasOne<Class>()
                      .WithMany()
                      .HasForeignKey(cs => cs.ClassId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Relationship: ClassSubject belongs to one Subject
                entity.HasOne<Subject>()
                      .WithMany()
                      .HasForeignKey(cs => cs.SubjectId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            //updaes can and alway will be made, that is why we have version control and repositories

        }
    }
}

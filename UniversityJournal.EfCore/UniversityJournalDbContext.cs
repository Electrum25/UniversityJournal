using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Text.Json;
using System.Text.RegularExpressions;
using UniversityJournal.Core.Entities; // Предполагаю, что namespace для сущностей — UniversityJournal.Core.Entities
using BCrypt.Net; // Добавь NuGet пакет BCrypt.Net для хэширования

namespace UniversityJournal.EfCore
{
    public class UniversityJournalDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Core.Entities.Group> Groups { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        public UniversityJournalDbContext(DbContextOptions<UniversityJournalDbContext> options) : base(options)
        {
            //Database.EnsureDeleted(); 
            //Database.EnsureCreated(); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<Student>()
                .HasKey(s => s.StudentId);

            modelBuilder.Entity<Teacher>()
                .HasKey(t => t.TeacherId);

            modelBuilder.Entity<Core.Entities.Group>()
                .HasKey(g => g.GroupId);

            modelBuilder.Entity<Subject>()
                .HasKey(s => s.SubjectId);

            modelBuilder.Entity<StudentSubject>()
                .HasKey(ss => new { ss.StudentId, ss.SubjectId });

            modelBuilder.Entity<Grade>()
                .HasKey(g => g.GradeId);

            modelBuilder.Entity<Attendance>()
                .HasKey(a => a.AttendanceId);

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>();

            modelBuilder.Entity<Attendance>()
                .Property(a => a.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Student>()
                .HasOne<User>() 
                .WithMany() 
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasOne<Core.Entities.Group>()
                .WithMany() 
                .HasForeignKey(s => s.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Teacher>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Subject>()
                .HasOne<Teacher>()
                .WithMany() 
                .HasForeignKey(s => s.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudentSubject>()
                .HasOne<Student>()
                .WithMany() 
                .HasForeignKey(ss => ss.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StudentSubject>()
                .HasOne<Subject>()
                .WithMany()
                .HasForeignKey(ss => ss.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Grade>()
                .HasOne<Student>()
                .WithMany()
                .HasForeignKey(g => g.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Grade>()
                .HasOne<Subject>()
                .WithMany()
                .HasForeignKey(g => g.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Attendance>()
                .HasOne<Student>()
                .WithMany()
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Attendance>()
                .HasOne<Subject>()
                .WithMany()
                .HasForeignKey(a => a.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Login)
                .IsUnique(); 

            var adminUserId = Guid.NewGuid();
            var testGroupId = Guid.NewGuid();
            var testTeacherUserId = Guid.NewGuid();
            var testTeacherId = Guid.NewGuid();

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = adminUserId,
                    Login = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin"), 
                    Role = UserRole.Admin,
                    CreatedAt = DateTime.UtcNow
                },
                new User
                {
                    UserId = testTeacherUserId,
                    Login = "teacher",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("teacher"),
                    Role = UserRole.Teacher,
                    CreatedAt = DateTime.UtcNow
                }
            );

            modelBuilder.Entity<Core.Entities.Group>().HasData(
                new Core.Entities.Group
                {
                    GroupId = testGroupId,
                    GroupName = "ИТ-21",
                    Specialization = "Информатика",
                    Year = 2023
                }
            );

            modelBuilder.Entity<Teacher>().HasData(
                new Teacher
                {
                    TeacherId = testTeacherId,
                    UserId = testTeacherUserId,
                    FirstName = "Иван",
                    LastName = "Иванов",
                    Patronymic = "Иванович"
                }
            );

            
        }
    }
}
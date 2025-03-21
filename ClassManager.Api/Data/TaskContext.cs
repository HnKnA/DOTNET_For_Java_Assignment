﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ClassManager.Api.Data.Entities;
using ClassManager.Shared;

namespace ClassManager.Api.Data
{
    public class TaskContext(DbContextOptions<TaskContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Class> Classes { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Departments
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "Physics Dept", Subject = "Physics", AccessCode = "PHY123" },
                new Department { Id = 2, Name = "History Dept", Subject = "History", AccessCode = "HIS123" },
                new Department { Id = 3, Name = "Maths Dept", Subject = "Maths", AccessCode = "MTH123" }
            );

            // Users
            var adminUser = new User
            {
                Id = 1,
                Name = "University Admin",
                Email = "university@email.com",
                Phone = "1234567890",
                Role = nameof(UserRole.Admin),
                IsApproved = true
            };
            adminUser.PasswordHash = "AQAAAAIAAYagAAAAEIC2CVjh/3b2Y5SBSYJfUJgEQX7iNUBUCFhLPY3/qYgHZWkwvU9kHL0O4aznN1YNUw=="; // Password: 123456

            var managerUser = new User
            {
                Id = 2,
                Name = "University Manager",
                Email = "manager@email.com",
                Phone = "0987654321",
                Role = nameof(UserRole.Manager),
                IsApproved = true
            };
            managerUser.PasswordHash = "AQAAAAIAAYagAAAAEIC2CVjh/3b2Y5SBSYJfUJgEQX7iNUBUCFhLPY3/qYgHZWkwvU9kHL0O4aznN1YNUw=="; // Password: 123456

            var departmentUser = new User
            {
                Id = 3,
                Name = "Department Head",
                Email = "department@email.com",
                Phone = "1122334455",
                Role = nameof(UserRole.Department),
                IsApproved = true
            };
            departmentUser.PasswordHash = "AQAAAAIAAYagAAAAEIC2CVjh/3b2Y5SBSYJfUJgEQX7iNUBUCFhLPY3/qYgHZWkwvU9kHL0O4aznN1YNUw=="; // Password: 123456

            modelBuilder.Entity<User>()
                .HasData(adminUser, managerUser, departmentUser);

            // Classes
            modelBuilder.Entity<Class>().HasData(
                new Class { Id = 1, Name = "Quantum Mechanics", DepartmentId = 1 },
                new Class { Id = 2, Name = "Thermodynamics", DepartmentId = 1 },
                new Class { Id = 3, Name = "World History", DepartmentId = 2 },
                new Class { Id = 4, Name = "Linear Algebra", DepartmentId = 3 },
                new Class { Id = 5, Name = "Calculus", DepartmentId = 3 }
            );

            // Relationships
            modelBuilder.Entity<Class>()
                .HasOne(c => c.Department)
                .WithMany()
                .HasForeignKey(c => c.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

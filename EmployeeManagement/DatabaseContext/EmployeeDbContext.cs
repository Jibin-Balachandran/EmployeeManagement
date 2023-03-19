using System;
using EmployeeManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.DatabaseContext
{
	public class EmployeeDbContext: DbContext
	{
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("EmployeeDb");
        }

        public DbSet<EmployeeEntity> employees { get; set; }
    }
}


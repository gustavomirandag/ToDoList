using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoListMobileApp.Models;

namespace ToDoListMobileApp.Repositories.Azure
{
    public class ToDoListAzureContext : DbContext
    {
        private string _dbConnectionString;

        public ToDoListAzureContext(string dbConnectionString)
        {
            _dbConnectionString = dbConnectionString;
            // Create database if not there
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_dbConnectionString);
        }

        public DbSet<Item> Items { get; set; }
    }
}

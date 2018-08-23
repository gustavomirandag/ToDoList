using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoListMobileApp.Models;

namespace ToDoListMobileApp.Contexts
{
    public class ToDoListContext : DbContext
    {
        private string _dbPath;

        public ToDoListContext(string dbPath)
        {
            _dbPath = dbPath;
            // Create database if not there
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_dbPath}");
        }

        public DbSet<Item> Items { get; set; }
    }
}

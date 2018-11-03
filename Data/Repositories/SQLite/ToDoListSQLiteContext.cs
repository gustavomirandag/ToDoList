using DomainModel.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.SQLite
{
    public class ToDoListSQLiteContext : DbContext
    {
        private string _dbPath;

        public ToDoListSQLiteContext(string dbPath)
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

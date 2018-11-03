using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ToDoListWebAPI.Models;

namespace ToDoListWebAPI.Contexts
{
    public class ToDoListAzureContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public ToDoListAzureContext() 
            : base("Server = tcp:myinfnetsocialnetwork.database.windows.net, 1433; Initial Catalog = MySocialNetwork; Persist Security Info=False;User ID = olivato; Password=EDSInf123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30;")
        {
            Database.Initialize(true);
        }
    }
}
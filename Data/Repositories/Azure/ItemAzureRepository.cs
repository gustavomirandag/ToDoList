using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using System.IO;
using System.Diagnostics;
using DomainModel.Entities;
using DomainModel.Interfaces;

namespace Data.Repositories.Azure
{
    public class ItemAzureRepository : IItemRepository
    {
        private ToDoListAzureContext _db;

        public ItemAzureRepository()
        {
            string dbConnectionString = "CONNECTION STRING HERE";

            _db = new ToDoListAzureContext(dbConnectionString);
        }

        public void Add(Item item)
        {
            _db.Items.Add(item);
            _db.SaveChanges();
        }

        public void Update(Item item)
        {
            var entry = _db.Entry(item);
            _db.Items.Attach(item);
            entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
        }

        public IEnumerable<Item> GetAll()
        {
            return _db.Items;
        }

        public void Remove(Guid id)
        {
            var originalItem = _db.Items.SingleOrDefault(i => i.Id == id);
            _db.Items.Remove(originalItem);
            _db.SaveChanges();
        }

        public void RemoveAll()
        {
            foreach (var item in _db.Items)
                _db.Items.Remove(item);
            _db.SaveChanges();
        }

        public Item Get(Guid id)
        {
            return _db.Items.SingleOrDefault(i => i.Id == id);
        }
    }
}

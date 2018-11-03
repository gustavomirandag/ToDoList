using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using System.IO;
using Microsoft.EntityFrameworkCore;
using DomainModel.Entities;
using DomainModel.Interfaces;

namespace Data.Repositories.SQLite
{
    public class ItemSQLiteRepository : IItemRepository
    {
        private ToDoListSQLiteContext _db;

        public ItemSQLiteRepository(string devicePlatform)
        {
            string dbPath = String.Empty;

            switch (devicePlatform)
            {
                case "UWP":
                    dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "amazingnotes.sqlite");
                    break;
                case "iOS":
                    dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", "data", "amazingnotes.sqlite");
                    break;
                case "Android":
                    dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "amazingnotes.sqlite");
                    break;
            }

            _db = new ToDoListSQLiteContext(dbPath);
        }

        public void Add(Item item)
        {
            _db.Items.Add(item);
            _db.SaveChanges();
        }

        public void Update(Item item)
        {
            //##### Update the LocalDatabase #####
            var entry = _db.Entry(item);
            _db.Items.Attach(item);
            entry.State = EntityState.Modified;
            _db.SaveChanges();
            //####################################
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

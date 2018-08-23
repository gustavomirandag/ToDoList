using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ToDoListMobileApp.Contexts;
using ToDoListMobileApp.Interfaces;
using ToDoListMobileApp.Models;
using System.Linq;
using Xamarin.Forms;
using System.IO;

namespace ToDoListMobileApp.Repositories.SQLite
{
    public class ItemSQLiteRepository : IItemRepository
    {
        private ToDoListContext _db;
        public ObservableCollection<Item> Items { get; set; }

        public ItemSQLiteRepository()
        {
            string dbPath = String.Empty;

            switch (Device.RuntimePlatform)
            {
                case Device.UWP:
                    dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.sqlite");
                    //dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "database.sqlite");
                    break;
                case Device.iOS:
                    dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", "data", "database.sqlite");
                    break;
                case Device.Android:
                    dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "database.sqlite");
                    break;
            }

            _db = new ToDoListContext(dbPath);

            //#### Fill The ObservableCollection ####
            Items = new ObservableCollection<Item>();
            foreach (var item in _db.Items)
                Items.Add(item);
            //#######################################
        }

        public void Add(Item item)
        {
            Items.Add(item); //ObservableCollection
            _db.Items.Add(item);
            _db.SaveChanges();
        }

        public void Update(Item item)
        {
            Item originalItem;

            //##### Update the ObservableCollection #####
            originalItem = Items.SingleOrDefault(i => i.Id == item.Id);
            originalItem.Title = item.Title;
            originalItem.Description = item.Description;
            //###########################################

            //##### Update the LocalDatabase #####
            var entry = _db.Entry(item);
            _db.Items.Attach(item);
            entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
            //####################################
        }
    }
}

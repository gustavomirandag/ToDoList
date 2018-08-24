using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ToDoListMobileApp.Interfaces;
using ToDoListMobileApp.Models;
using System.Linq;
using Xamarin.Forms;
using System.IO;
using System.Diagnostics;

namespace ToDoListMobileApp.Repositories.Azure
{
    public class ItemAzureRepository : IItemRepository
    {
        private ToDoListAzureContext _db;
        public ObservableCollection<Item> Items { get; set; }

        public ItemAzureRepository()
        {
            string dbConnectionString = "COLOQUE AQUI SUA STRING DE CONEXÃO";

            _db = new ToDoListAzureContext(dbConnectionString);

            //#### Fill The ObservableCollection ####
            Items = new ObservableCollection<Item>();
            try
            {
                foreach (var item in _db.Items)
                    Items.Add(item);
            }
            catch
            {
            }
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

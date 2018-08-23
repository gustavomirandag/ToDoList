using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ToDoListMobileApp.Interfaces;
using ToDoListMobileApp.Models;
using System.Linq;

namespace ToDoListMobileApp.Repositories.Ram
{
    public class ItemRamRepository : IItemRepository
    {
        public ObservableCollection<Item> Items { get; set; }

        public void Add(Item item)
        {
            Items.Add(item);
        }

        public void Update(Item item)
        {
            Item originalItem = Items.SingleOrDefault(i => i.Id == item.Id);
            originalItem.Title = item.Title;
            originalItem.Description = item.Description;
        }

        public ItemRamRepository()
        {
            Items = new ObservableCollection<Item>();
        }
    }
}

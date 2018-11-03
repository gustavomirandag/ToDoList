using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using DomainModel.Entities;
using Data.Repositories.Azure;
using DomainModel.Interfaces;

namespace Data.Repositories.Ram
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

        public IEnumerable<Item> GetAll()
        {
            return Items;
        }

        public ItemRamRepository()
        {
            Items = new ObservableCollection<Item>();
        }

        public void Remove(Guid id)
        {
            var originalItem = Items.SingleOrDefault(i => i.Id == id);
            Items.Remove(originalItem);
        }

        public void RemoveAll()
        {
            Items.Clear();
        }

        public Item Get(Guid id)
        {
            return Items.SingleOrDefault(i => i.Id == id);
        }
    }
}

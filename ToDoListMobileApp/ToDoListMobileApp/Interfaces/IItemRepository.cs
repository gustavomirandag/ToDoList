using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ToDoListMobileApp.Models;

namespace ToDoListMobileApp.Interfaces
{
    public interface IItemRepository
    {
        ObservableCollection<Item> Items { get; set; }
        void Add(Item item);
        void Update(Item item);
    }
}

using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DomainModel.Interfaces
{
    public interface IItemRepository
    {
        void Add(Item item);
        void Update(Item item);
        IEnumerable<Item> GetAll();
        Item Get(Guid id);
        void Remove(Guid id);
        void RemoveAll();
    }
}

using DomainModel.Entities;
using DomainModel.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DomainService
{
    public class ItemService
    {
        private IItemRepository _repository;

        public ItemService(IItemRepository repository)
        {
            _repository = repository;
        }

        public void AddItem(Item item)
        {
            _repository.Add(item);
        }

        public void UpdateItem(Item item)
        {
            _repository.Update(item);
        }

        public IEnumerable<Item> GetAllItems()
        {
            return _repository.GetAll();
        }

        public Item GetItem(Guid id)
        {
            return _repository.Get(id);
        }

        public void RemoveItem(Guid id)
        {
            _repository.Remove(id); //Remove from Repository
        }

        public void RemoveAllItems()
        {
            _repository.RemoveAll();
        }
    }
}

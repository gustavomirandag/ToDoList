using DomainModel.Entities;
using DomainModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Linq;
using DomainService;
using System.Net;
using System.Text;

namespace ToDoListApp
{
    public class ToDoListAppService
    {
        public ObservableCollection<Item> Items { get; set; }
        private ItemService _itemService;

        public ToDoListAppService(ItemService itemService)
        {
            _itemService = itemService;
            Items = new ObservableCollection<Item>();

            //Fill the ObservableCollection
            var items = GetAllItems().OrderBy(i => i.PublishDateTime);
            foreach (var item in items)
                Items.Add(item);
        }   

        public void AddItem(Item item)
        {
            Items.Add(item); //ObservableCollection
            _itemService.AddItem(item);
        }

        public void UpdateItem(Item item)
        {
            _itemService.UpdateItem(item);
        }

        public IEnumerable<Item> GetAllItems()
        {
            return _itemService.GetAllItems();
        }

        public void RemoveItem(Guid id)
        {
            var originalItem = Items.SingleOrDefault(i => i.Id == id);
            Items.Remove(originalItem); //Remove from ObservableCollection
            _itemService.RemoveItem(id); //Remove from Repository
        }

        public void RemoveAllItems()
        {
            Items.Clear();
            _itemService.RemoveAllItems();
        }

        public async Task<bool> ExecuteBackup() //Backup to RemoteRepository
        {
            HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://localhost:55127/api/");
            client.BaseAddress = new Uri("https://amazingnoteswebapi.azurewebsites.net/api/");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var request = await client.DeleteAsync("items");
            if (!request.IsSuccessStatusCode)
                return false;
            foreach (var item in Items)
            {
                string serializedItem = Newtonsoft.Json.JsonConvert.SerializeObject(item);
                request = await client.PostAsync("items", new StringContent(serializedItem, Encoding.UTF8, "application/json"));
                if (!request.IsSuccessStatusCode)
                    return false;
            }
            return true;
        }

        public async Task<bool> RestoreBackup() //Restore from RemoteRepository
        {
            //##### ACESSO VIA API #####
            HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://localhost:55127/api/");
            client.BaseAddress = new Uri("https://amazingnoteswebapi.azurewebsites.net/api/");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;//Ignore SSL certificate errors

            var requestResult = await client.GetAsync("items");

            if (!requestResult.IsSuccessStatusCode)
                return false; //Could not restore the backup

            string serializedItems = await requestResult.Content.ReadAsStringAsync();
            IEnumerable<Item> restoredItems = Newtonsoft.Json.JsonConvert
                .DeserializeObject<IEnumerable<Item>>(serializedItems)
                .OrderBy(i => i.PublishDateTime);

            RemoveAllItems();
            foreach (var item in restoredItems)
            {
                AddItem(item);
            }

            return true;
        }
    }
}

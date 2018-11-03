using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Interfaces;
using DomainModel.Entities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ToDoListMobileApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddItemPage : ContentPage
	{
        public AddItemPage()
        {
            InitializeComponent();
        }

        private async void ButtonAdd_Clicked(object sender, EventArgs e)
        {
            //App.Service.Add(new Item(EntryTitle.Text, EntryDescription.Text));
            //##### ACESSO VIA API #####
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://infnettodolistapi.azurewebsites.net/");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Item item = new Item(EntryTitle.Text, EntryDescription.Text);
            string serializedItem = Newtonsoft.Json.JsonConvert.SerializeObject(item);
            //Cria o HttpContent
            var buffer = System.Text.Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);
            //-------------------
            await client.PostAsync("items", byteContent);
            //##########################

            await Navigation.PopModalAsync(true);
        }

        private void ButtonCancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync(true);
        }
    }
}
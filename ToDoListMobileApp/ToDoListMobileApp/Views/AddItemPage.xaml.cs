using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListMobileApp.Interfaces;
using ToDoListMobileApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoListMobileApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddItemPage : ContentPage
	{
        public AddItemPage()
        {
            InitializeComponent();
        }

        private void ButtonAdd_Clicked(object sender, EventArgs e)
        {
            App.Repository.Add(new Item(EntryTitle.Text, EntryDescription.Text));
            Navigation.PopModalAsync();
        }

        private void ButtonCancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
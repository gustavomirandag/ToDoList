using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Interfaces;
using DomainModel.Entities;
using Data.Repositories;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoListMobileApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListItemsPage : ContentPage
	{
        public ListItemsPage()
        {
            InitializeComponent();
            ListViewItems.ItemsSource = App.Service.GetReferenceToObservableCollection();
            ListViewItems.ItemTapped += ListViewItems_ItemTapped;
        }

        private void ListViewItems_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushModalAsync(new UpdateItemPage((Item)e.Item));
        }

        private void ButtonAddItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AddItemPage(), true);
        }
    }
}
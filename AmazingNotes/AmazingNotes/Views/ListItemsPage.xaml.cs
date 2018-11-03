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

namespace AmazingNotes.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListItemsPage : ContentPage
	{
        public ListItemsPage()
        {
            InitializeComponent();
            ListViewItems.ItemsSource = App.Service.Items;
            ListViewItems.ItemTapped += ListViewItems_ItemTapped;
        }

        private void ListViewItems_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushModalAsync(new UpdateItemPage((Item)e.Item));
            ListViewItems.SelectedItem = null;
        }

        private void ButtonAddItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AddItemPage(), true);
        }

        private void ButtonRemove_Clicked(object sender, EventArgs e)
        {
            Guid itemId = (Guid)((Button)sender).CommandParameter;
            App.Service.RemoveItem(itemId);
        }

        private async void Backup_Activated(object sender, EventArgs e)
        {
            bool result = await App.Service.ExecuteBackup();
            if (!result)
            {
                await DisplayAlert("Backup", "Error trying to backup. Check your internet connection and try again later!", "Ok");
                return;
            }
            await DisplayAlert("Backup", "Backup Done!", "Ok");
        }

        private async void RestoreBackup_Activated(object sender, EventArgs e)
        {
            bool result = await App.Service.RestoreBackup();
            if (!result)
            {
                await DisplayAlert("Backup", "Error trying to restore backup. Check your internet connection and try again later!", "Ok");
                return;
            }
            await DisplayAlert("Backup", "Backup Restored!", "Ok");
        }
    }
}
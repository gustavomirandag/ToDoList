using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoListMobileApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UpdateItemPage : ContentPage
	{
        private Item _item;

		public UpdateItemPage (Item item)
		{
			InitializeComponent ();
            _item = item;
            EntryTitle.Text = _item.Title;
            EntryDescription.Text = _item.Description;
        }

        private void ButtonCancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void ButtonSave_Clicked(object sender, EventArgs e)
        {
            _item.Title = EntryTitle.Text;
            _item.Description = EntryDescription.Text;
            App.Service.Update(_item);
            Navigation.PopModalAsync();
        }
    }
}
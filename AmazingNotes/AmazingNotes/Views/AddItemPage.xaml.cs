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
using Plugin.LocalNotifications;

namespace AmazingNotes.Views
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
            App.Service.AddItem(new Item(EntryTitle.Text, EntryDescription.Text));
            if (DatePickerReminder.IsEnabled)
            {
                CrossLocalNotifications.Current.Show("Note Reminder",
                    EntryTitle.Text,
                    App.Service.Items.Count - 1,
                    new DateTime(DatePickerReminder.Date.Year, DatePickerReminder.Date.Month,
                        DatePickerReminder.Date.Day,
                        TimePickerReminder.Time.Hours,
                        TimePickerReminder.Time.Minutes,
                        0)
                );
            }
            Navigation.PopModalAsync(true);
        }

        private void ButtonCancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync(true);
        }

        private void SwitchReminder_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                DatePickerReminder.MinimumDate = DateTime.Now;
                DatePickerReminder.Date = DateTime.Now;
                DatePickerReminder.IsEnabled = true;

                TimePickerReminder.Time = DateTime.Now.TimeOfDay;
                TimePickerReminder.IsEnabled = true;
            }
            else
            {
                DatePickerReminder.IsEnabled = false;
                TimePickerReminder.IsEnabled = false;
            }
        }
    }
}
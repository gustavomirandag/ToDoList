using System;
using DomainModel.Interfaces;
using Data.Repositories;
using Data.Repositories.Azure;
using Data.Repositories.Ram;
using Data.Repositories.SQLite;
using ToDoListMobileApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DomainService;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace ToDoListMobileApp
{
	public partial class App : Application
	{
        public static ItemService Service { get; set; }
         
		public App ()
		{
			InitializeComponent();

            Service = new ItemService(new ItemSQLiteRepository(Device.RuntimePlatform));
            MainPage = new NavigationPage(new ListItemsPage());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

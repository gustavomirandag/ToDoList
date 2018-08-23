using System;
using ToDoListMobileApp.Interfaces;
using ToDoListMobileApp.Repositories;
using ToDoListMobileApp.Repositories.SQLite;
using ToDoListMobileApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace ToDoListMobileApp
{
	public partial class App : Application
	{
        public static IItemRepository Repository { get; set; }
         
		public App ()
		{
			InitializeComponent();

            Repository = new ItemSQLiteRepository();
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

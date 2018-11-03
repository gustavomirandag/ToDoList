using AmazingNotes.Views;
using Data.Repositories.SQLite;
using DomainService;
using System;
using ToDoListApp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AmazingNotes
{
    public partial class App : Application
    {
        public static ToDoListAppService Service { get; set; }
        public App()
        {
            InitializeComponent();

            Service = new ToDoListAppService(new ItemService(new ItemSQLiteRepository(Device.RuntimePlatform)));
            MainPage = new NavigationPage(new ListItemsPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace OZDVidPlay
{
    public partial class App : Application
    {
        public static DatabaseManager DatabaseManager;

        public App()
        {
            InitializeComponent();

            DatabaseManager = new DatabaseManager();
            MainPage = new NavigationPage(new HomePage());
        }
    }
}

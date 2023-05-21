using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin21.Models;
using Xamarin21.Pages;

namespace Xamarin21
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new SetPinPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

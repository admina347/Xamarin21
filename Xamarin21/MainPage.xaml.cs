using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin21.Models;
using Xamarin21.Pages;

namespace Xamarin21
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void LogoutButton_Clicked(object sender, System.EventArgs e)
        {
            //Delete Pin
            Preferences.Clear();

            // Возврат на предыдущую страницу
            await Navigation.PushModalAsync(new SetPinPage());
        }
    }
}

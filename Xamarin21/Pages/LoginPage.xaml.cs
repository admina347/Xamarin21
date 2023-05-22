using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin21.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
		}

        private async void Login(object sender, EventArgs e)
        {
            string text = ((Entry)sender).Text;
            string secretPin = Preferences.Get("userPin", "");

            if (secretPin == text)
            {
                await Navigation.PushModalAsync(new ImagesPage());
            }
            else
            {
                await DisplayAlert("Ошибка", "Неверный пин" + text, "OK");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin21.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Xamarin21.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SetPinPage : ContentPage
	{
		public SetPinPage ()
		{
			InitializeComponent ();
		}

        /// <summary>
        /// Вызывается до того, как элемент становится видимым
        /// </summary>
        protected async override void OnAppearing()
        {
            // Проверяем, есть ли в словаре значение
            string userPin = Preferences.Get("userPin", null);

            if (!string.IsNullOrEmpty(userPin))
            {
                await Navigation.PushModalAsync(new LoginPage());
            }
            
            base.OnAppearing();
        }

        private void GoToSecondPin(object sender, EventArgs e)
        {
            secondPin.Focus();
        }

        private async void SavePin(object sender, EventArgs e)
        {
            string text = ((Entry)sender).Text;

            bool pinAccept = VerifyPin();
            if (pinAccept == true)
            {
                // Сохраним значения пин.
                Preferences.Set("userPin", text);
                await Navigation.PushModalAsync(new ImagesPage());
            }
            else
            {
                await DisplayAlert("Ошибка", "Пин не совпадает", "OK");
            }
        }

        private bool VerifyPin()
        {
            if (firstPin.Text == secondPin.Text)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
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

        /// <summary>
        /// Вызывается до того, как элемент становится видимым
        /// </summary>
        protected async override void OnAppearing()
        {
            // Проверяем, есть ли в словаре значение
            string userPin = Preferences.Get("userPin", null);

            if (string.IsNullOrEmpty(userPin))
            {
                //Пин не установлен
                pinText.Text = "Установите ПИН";
                loginPin.IsVisible = false;
                firstPin.IsVisible = true;
                secondPin.IsVisible = true;
            }
            base.OnAppearing();
        }


        private async void Login(object sender, EventArgs e)
        {
            string text = ((Entry)sender).Text;
            string secretPin = Preferences.Get("userPin", "");

            if (secretPin == text)
            {
                await Navigation.PushModalAsync(new ImageListPage());
            }
            else
            {
                await DisplayAlert("Ошибка", "Неверный пин" + text, "OK");
            }
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
                await Navigation.PushModalAsync(new ImageListPage());
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
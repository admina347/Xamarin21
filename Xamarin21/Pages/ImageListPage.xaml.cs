using CoreImage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;
using Xamarin21.Models;

namespace Xamarin21.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageListPage : ContentPage
    {
        GalleryImage SelectedImage;
        public ObservableCollection<GalleryImage> Images { get; set; } = new ObservableCollection<GalleryImage>();

        public ImageListPage()
        {
            InitializeComponent();

            string imagesPath = "/storage/emulated/0/DCIM/Camera";

            try
            {
                IEnumerable<string> allImages = Directory.EnumerateFiles(imagesPath);
                foreach (string filename in allImages)
                {

                    string imageName = System.IO.Path.GetFileName(filename);
                    string imageDescription = File.GetCreationTimeUtc(filename).ToString("yyyy:MM:dd HH:mm:ss");

                    Images.Add(new GalleryImage(Guid.NewGuid(), imageName, image: filename, description: imageDescription));
                }
                //Empty Folder
                if (Images.Count <= 0)
                {
                    errorLabel.Text = "В папке нет изображений";
                }
                else
                {
                    errorLabel.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
                imageListCaption.Text = "Ошибка";
                errorLabel.Text = ex.Message;
            }
            BindingContext = this;
        }

        private void imageList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            // распаковка модели из объекта
            var tappedImage = (GalleryImage)e.Item;
            // уведомление
            DisplayAlert("Нажатие", $"Вы нажали на элемент {tappedImage.Name}", "OK"); ; ;
        }

        /// <summary>
        /// Обработчик выбора
        /// </summary>
        private void imageList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // распаковка модели из объекта
            SelectedImage = (GalleryImage)e.SelectedItem;
        }

        /// <summary>
        /// Обработчик добавления нового устройства
        /// </summary>
        private async void OpenImage(object sender, EventArgs e)
        {
            // проверяем, выбрал ли пользователь устройство из списка
            if (SelectedImage == null)
            {
                await DisplayAlert(null, $"Пожалуйста, выберит изображение!", "OK");
                return;
            }

            // Переход на следующую страницу - страницу нового устройства (и помещение её в стек навигации)
            await Navigation.PushModalAsync(new ImagePage("Просмотр", SelectedImage));
        }

        /// <summary>
        /// Обработчик удаления устройства
        /// </summary>
        private async void DeleteImage(object sender, EventArgs e)
        {
            // Получаем и "распаковываем" выбранный элемент
            var imageToRemove = imageList.SelectedItem as GalleryImage;
            if (imageToRemove != null)
            {
                // Уведомляем пользователя
                bool deleteResult = await DisplayAlert("Удалить", $"Вы уверены, что хотите удалить {imageToRemove.Name}?", "No", "Yes");

                if (!deleteResult)
                {
                    // Удаляем
                    Images.Remove(imageToRemove);
                    File.Delete(imageToRemove.Image);
                    // Уведомляем пользователя
                    await DisplayAlert(imageToRemove.Name, $"Изображение '{imageToRemove.Image}' удалено", "ОК");
                }
            }
        }

        //Logout
        private async void LogoutButton_Clicked(object sender, System.EventArgs e)
        {
            //Delete Pin
            Preferences.Clear();
            // Возврат на предыдущую страницу
            await Navigation.PushModalAsync(new LoginPage());
        }
    }
}
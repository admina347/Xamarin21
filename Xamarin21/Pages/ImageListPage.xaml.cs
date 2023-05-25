using CoreImage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                //DisplayAlert("Строк" + gridRows, "Найдено файлов" + allImages.Count(), "OK");
                foreach (string filename in allImages)
                {
                    //Create an Image object for each bitmap
                    //DisplayAlert("File", filename, "OK");
                    string imageName = System.IO.Path.GetFileName(filename);
                    string imageDescription = File.GetCreationTimeUtc(filename).ToString("yyyy:MM:dd HH:mm:ss");
                    //DateTime imageDate = GetMyImageTakenDate(filename);
                    //Image image = new Image
                    //{
                    //    Source = ImageSource.FromFile(filename),   //FromUri(new Uri(filepath))
                    //    HeightRequest = 100

                    //};
                    //
                    Images.Add(new GalleryImage(Guid.NewGuid(), imageName, image: filename, description: imageDescription));



                }

            }
            catch (Exception ex)
            {

            }
            BindingContext = this;
        }

        //static DateTime GetMyImageTakenDate(string filename)
        //{
        //    DateTime takenDate = DateTime.Today;

        //    CGImageSource myImageSource;
        //    myImageSource = CGImageSource.FromUrl(filename, null);
        //    var ns = new NSDictionary();
        //    var imageProperties = myImageSource.CopyProperties(ns, 0);

        //    NSObject date = null;
        //    var exifDictionary = imageProperties.ValueForKey(ImageIO.CGImageProperties.ExifDictionary);
        //    if (exifDictionary != null)
        //    {
        //        date = exifDictionary.ValueForKey(ImageIO.CGImageProperties.ExifDateTimeOriginal);
        //    }
        //    takenDate = date != null ? DateTime.ParseExact(date.ToString(), "yyyy:MM:dd HH:mm:ss", null) : takenDate;
        //    return takenDate;
        //}

        /// <summary>
        /// Обработчик нажатия
        /// </summary>
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
                // Удаляем
                Images.Remove(imageToRemove);
                //
                File.Delete(imageToRemove.Image);
                // Уведомляем пользователя
                await DisplayAlert(imageToRemove.Name, $"Изображение '{imageToRemove.Image}' удалено", "ОК");
            }
        }
    }
}
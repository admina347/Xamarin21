using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin21.Models;

namespace Xamarin21.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageListPage : ContentPage
    {
        public List<GalleryImage> Images { get; set; } = new List<GalleryImage>();
        public ImageListPage()
        {
            InitializeComponent();
        }

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
            var selectedImage = (GalleryImage)e.SelectedItem;
            // уведомление
            DisplayAlert("Выбор", $"Вы выбрали {selectedImage.Name}", "OK"); ; ;
        }
    }
}
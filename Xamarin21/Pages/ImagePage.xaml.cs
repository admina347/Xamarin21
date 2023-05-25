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
    public partial class ImagePage : ContentPage
    {
        public static string PageName { get; set; }
        public static string ImageName { get; set; }
        public static string ImagePath { get; set; }
        public static string ImageDescription { get; set; }
 
        public GalleryImage GalleryImage { get; set; }

        public ImagePage(string pageName, GalleryImage galleryImage = null)
        {
            PageName = pageName;

            if (galleryImage != null)
            {
                GalleryImage = galleryImage;
                ImageName = galleryImage.Name;
                ImagePath = galleryImage.Image;
                ImageDescription = galleryImage.Description;
            }
            else
            {
                galleryImage = new GalleryImage(Guid.NewGuid(), name: "test");
            }
            InitializeComponent();
            OpenImage(ImagePath, ImageDescription);
        }

        public void OpenImage(string filename, string desc)
        {

            Image image = new Image
            {
                Source = ImageSource.FromFile(filename),   //FromUri(new Uri(filepath))
                Aspect = Aspect.AspectFill

            };
            
            stackLayout.Children.Add(image);
            stackLayout.Children.Add(new Label { Text = desc, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.White });

        }

    }
}
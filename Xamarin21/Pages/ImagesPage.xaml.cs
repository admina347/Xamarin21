using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace Xamarin21.Pages
{
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ImagesPage : ContentPage
	{


        public ImagesPage()
        {
            InitializeComponent();
            //LoadImages();
        }

        void LoadImages()
        {
            string imagesPath = "/storage/emulated/0/DCIM/Camera";

            // Создадим новый стек
            var innerStack = new FlexLayout();
            innerStack.Wrap = FlexWrap.Wrap;
            innerStack.JustifyContent = FlexJustify.SpaceAround;
            innerStack.AlignItems = FlexAlignItems.End;
            innerStack.AlignContent = FlexAlignContent.End;
            

            try
            {
                IEnumerable<string> allImages = Directory.EnumerateFiles(imagesPath);
                
                int gridCols = 3;
                int gridRows;
                //int gridCol = 3;
                
                int imagesCount = allImages.Count();
                gridRows = imagesCount / gridCols;

                //DisplayAlert("Строк" + gridRows, "Найдено файлов" + allImages.Count(), "OK");
                DisplayAlert("Строк" + gridRows, "Стобцов" + gridCols, "OK");
                foreach (string filename in allImages)
                {
                    //Create an Image object for each bitmap
                    //DisplayAlert("File", filename, "OK");
                    string imageName = Path.GetFileName(filename);
                    Image image = new Image
                    {
                        Source = ImageSource.FromFile(filename),   //FromUri(new Uri(filepath))
                        HeightRequest = 100

                    };
                    
                    innerStack.Children.Add(image);
                    innerStack.Children.Add(new Label { 
                        Text = imageName, 
                        HorizontalTextAlignment = TextAlignment.Center, 
                        VerticalTextAlignment = TextAlignment.Start,
                        FontSize = 8
                    });

                    //flexLayout.Children.Add(image);
                    //flexLayout.Children.Add(new Label
                    //{
                    //    Text = imageName
                    //});


                }

            }
            catch
            {
                innerStack.Children.Add(new Label
                {
                    Text = "Cannot access list of bitmap files"
                });
                
                //flexLayout.Children.Add(new Label
                //{
                //    Text = "Cannot access list of bitmap files"
                //});
            }

            activityIndicator.IsRunning = false;
            activityIndicator.IsVisible = false;
            // Сохраним стек внутрь уже имеющегося в xaml-файле блока прокручиваемой разметки
            scrollView.Content = innerStack;
        }

        //Logout
        private async void LogoutButton_Clicked(object sender, System.EventArgs e)
        {
            //Delete Pin
            Preferences.Clear();

            // Возврат на предыдущую страницу
            await Navigation.PushModalAsync(new SetPinPage());
        }
    }
}
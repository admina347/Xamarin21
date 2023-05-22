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
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ImagesPage : ContentPage
	{


        // Class for deserializing JSON list of sample bitmaps
        [DataContract]
        class ImageList
        {
            [DataMember(Name = "photos")]
            public List<string> Photos = null;
        }



        public ImagesPage()
        {
            InitializeComponent();

            LoadImages();
            //LoadBitmapCollection();
        }

        void LoadImages()
        {
            string imagesPath = "/storage/emulated/0/DCIM/Camera";


            try
            {
                IEnumerable<string> allImages = Directory.EnumerateFiles(imagesPath);
                //DisplayAlert("Ошибка", "Найдено файлов" + allImages.Count(), "OK");
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

                    flexLayout.Children.Add(image);
                    //flexLayout.Children.Add(new Label
                    //{
                    //    Text = imageName
                    //});


                }
            }
            catch 
            {
                flexLayout.Children.Add(new Label
                {
                    Text = "Cannot access list of bitmap files"
                });
            }

            activityIndicator.IsRunning = false;
            activityIndicator.IsVisible = false;
        }

        async void LoadBitmapCollection()
        {
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    // Download the list of stock photos
                    Uri uri = new Uri("https://raw.githubusercontent.com/xamarin/docs-archive/master/Images/stock/small/stock.json");
                    byte[] data = await webClient.DownloadDataTaskAsync(uri);

                    // Convert to a Stream object
                    using (Stream stream = new MemoryStream(data))
                    {
                        // Deserialize the JSON into an ImageList object
                        var jsonSerializer = new DataContractJsonSerializer(typeof(ImageList));
                        ImageList imageList = (ImageList)jsonSerializer.ReadObject(stream);

                        // Create an Image object for each bitmap
                        foreach (string filepath in imageList.Photos)
                        {
                            Image image = new Image
                            {
                                Source = ImageSource.FromUri(new Uri(filepath))
                            };
                            flexLayout.Children.Add(image);
                        }
                    }
                }
                catch
                {
                    flexLayout.Children.Add(new Label
                    {
                        Text = "Cannot access list of bitmap files"
                    });
                }
            }

            activityIndicator.IsRunning = false;
            activityIndicator.IsVisible = false;
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
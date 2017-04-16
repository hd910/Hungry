using ImageCircle.Forms.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Xml.Linq;
using Xamarin.Forms;

namespace Hungry
{
    public partial class MainPage : ContentPage
    {
        private List<Food> foodList = new List<Food>();
        private string url = "https://api.flickr.com/services/rest/?method=flickr.photos.search&api_key={0}&text={1}&safe_search=1&per_page={2}&sort=relevance";
        private HttpClient _client = new HttpClient();

        public MainPage()
        {
            InitializeComponent();

            loadImages();
        }

        private async void loadImages()
        {

            var formattedurl = string.Format(url, APIKeys.FLICKR_API_KEY, "Pizza", "5");

            var content = await _client.GetStringAsync(formattedurl);

            var xdoc = XDocument.Parse(content);
            foreach (var node in xdoc.Descendants("photos").Descendants("photo"))
            {
                var id = node.Attribute("id").Value;
                var secretId = node.Attribute("secret").Value;
                var farmId = node.Attribute("farm").Value;
                var serverId = node.Attribute("server").Value;

                var imageURL = string.Format("https://farm{0}.staticflickr.com/{1}/{2}_{3}_c.jpg", farmId, serverId, id, secretId);

                foodList.Add(new Food("This is a pizza", imageURL));
            }

            loadPreviewIcons();

        }

        private void loadPreviewIcons()
        {
            
            for(var i = 0; i < foodList.Count; i++)
            {
                Food food = foodList[i];
                if(food != null)
                {
                    string uri = foodList[i].uri;
                    var tempIcon = new CircleImage
                    {
                        HeightRequest = 50,
                        WidthRequest = 50,
                        Aspect = Aspect.AspectFill,
                        Source = UriImageSource.FromUri(new Uri(uri))
                    };

                    //tempIcon.GestureRecognizers.Add(new TapGestureRecognizer(OnTap));
                    previewImagesLayout.Children.Add(tempIcon);
                }
                
            }
        }
    }
}

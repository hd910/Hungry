﻿using ImageCircle.Forms.Plugin.Abstractions;
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
        private int currentSelected = 0;

        public MainPage()
        {
            InitializeComponent();

            loadImages("Pizza");
        }

        private async void loadImages(string foodType)
        {

            var formattedurl = string.Format(url, APIKeys.FLICKR_API_KEY, foodType, "5");

            var content = await _client.GetStringAsync(formattedurl);

            if(content != null)
            {
                var xdoc = XDocument.Parse(content);
                foreach (var node in xdoc.Descendants("photos").Descendants("photo"))
                {
                    var id = node.Attribute("id").Value;
                    var secretId = node.Attribute("secret").Value;
                    var farmId = node.Attribute("farm").Value;
                    var serverId = node.Attribute("server").Value;

                    var imageURL = string.Format("https://farm{0}.staticflickr.com/{1}/{2}_{3}_z.jpg", farmId, serverId, id, secretId);
                    var thumbImageURL = string.Format("https://farm{0}.staticflickr.com/{1}/{2}_{3}_s.jpg", farmId, serverId, id, secretId);

                    foodList.Add(new Food(foodType, imageURL, thumbImageURL));
                }

                loadFood(foodType);
            }

        }

        private void loadFood(string foodType)
        {
            mainImage.Source = foodList[0].uri;
            catagoryLabel.Text = foodList[0].description;
            mainButton.Text = string.Format("I WANT... {0}", foodType.ToUpper());

            mainImageLoading.SetBinding(ActivityIndicator.IsRunningProperty, "IsLoading");
            mainImageLoading.BindingContext = mainImage;

            for (var i = 0; i < foodList.Count; i++)
            {
                Food food = foodList[i];
                if(food != null)
                {
                    string uri = foodList[i].thumbnailUri;
                    var tempIcon = new CircleImage
                    {
                        HeightRequest = 50,
                        WidthRequest = 50,
                        Aspect = Aspect.AspectFill,
                        Source = UriImageSource.FromUri(new Uri(uri)),
                        ClassId = i.ToString()
                    };

                    var tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += (sender, e) =>
                    {
                        var s = (CircleImage)sender;
                        int index = Int32.Parse(s.ClassId);

                        if(index != currentSelected)
                        {
                            mainImage.Source = foodList[index].uri;
                            currentSelected = index;

                            mainImageLoading.SetBinding(ActivityIndicator.IsRunningProperty, "IsLoading");
                            mainImageLoading.BindingContext = mainImage;
                        }
                    };
                    tempIcon.GestureRecognizers.Add(tapGestureRecognizer);

                    previewImagesLayout.Children.Add(tempIcon);
                }
                
            }
        }

        private void NextFood(object sender, EventArgs e)
        {
            previewImagesLayout.Children.Clear();
            foodList.Clear();
            currentSelected = 0;
            loadImages("Burger");
            
        }
    }
}

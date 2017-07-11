using Hungry.Models;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hungry
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingPage : ContentPage
    {
        MobileServiceClient client = AzureManager.AzureManagerInstance.AzureClient;
        List<FoodModel> foodList;

        public LoadingPage()
        {
            InitializeComponent();

            Image loadingImage = this.FindByName<Image>("loadingImage");
            loadingImage.Source = ImageSource.FromResource("Hungry.Images.logo.png");


            loadImages();
        }

        private async void loadImages()
        {
            foodList = await AzureManager.AzureManagerInstance.GetFoodList();
            await Navigation.PushAsync(new MainPage(foodList));
        }
    }
}
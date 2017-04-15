using ImageCircle.Forms.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hungry
{
    public partial class MainPage : ContentPage
    {
        private Food[] foodList = new Food[10];
        public MainPage()
        {
            InitializeComponent();
            constructFoodArray();
            loadPreviewIcons();
        }

        private void constructFoodArray()
        {
            for(var i = 0; i < 4; i++)
            {
                foodList[i] = new Food(string.Format("This is food {0}", i), 
                                string.Format("http://lorempixel.com/output/food-q-c-80-80-{0}.jpg", (i+1)));
            }
        }

        private void loadPreviewIcons()
        {
            
            for(var i = 0; i < foodList.Length; i++)
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

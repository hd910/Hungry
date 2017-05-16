using System;
using Xamarin.Forms;

namespace Hungry
{
    public partial class MainPage : ContentPage
    {

        CardStackView cardStack;
        StackLayout frontLoadingLayout = new StackLayout() {
        };

        public MainPage()
        {
            this.BackgroundColor = Color.FromHex("#FFFFFF");

            RelativeLayout view = new RelativeLayout();

            cardStack = new CardStackView(frontLoadingLayout);
            cardStack.SwipedLeft += SwipedLeft;
            cardStack.SwipedRight += SwipedRight;

            view.Children.Add(cardStack,
                Constraint.Constant(0),
                Constraint.Constant(0),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width - 0;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height - 0;
                })
            );

            this.LayoutChanged += (object sender, EventArgs e) =>
            {
                cardStack.CardMoveDistance = (int)(this.Width * 0.40f);
            };

            Image logo = new Image()
            {
                HeightRequest = 100,
                WidthRequest = 100,
                Aspect = Aspect.AspectFit,
                Source = ImageSource.FromResource("Hungry.Images.logo.png")
            };

            frontLoadingLayout.Children.Add(logo);

            ActivityIndicator loadingIcon = new ActivityIndicator()
            {
                WidthRequest = 50,
                HeightRequest = 50,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 100, 0, 0)
            };
            loadingIcon.IsRunning = true;
            
            frontLoadingLayout.Children.Add(loadingIcon);


            //view.Children.Add(frontLoadingLayout, Constraint.RelativeToParent(parent => parent.Width * 0.33),
            //    Constraint.RelativeToParent(parent => parent.Height * 0.33));
            frontLoadingLayout.VerticalOptions = LayoutOptions.Center;

            //EDITED OUT BECAUSE IT DISABLES ADMOB
            //view.Children.Add(frontLoadingLayout,
            //    Constraint.Constant(0),
            //    Constraint.Constant(0),
            //    Constraint.RelativeToParent((parent) =>
            //    {
            //        return parent.Width;
            //    }),
            //    Constraint.RelativeToParent((parent) =>
            //    {
            //        return parent.Height;
            //    }));

            this.Content = view;
        }

        void SwipedLeft(int index)
        {
            // card swiped to the left
        }

        void SwipedRight(int index)
        {
            // card swiped to the right
        }
    }
}



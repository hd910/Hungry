using System;
using Xamarin.Forms;

namespace Hungry
{
    public partial class MainPageCards : ContentPage
    {

        CardStackView cardStack;
        StackLayout frontLoadingLayout = new StackLayout() {
        };

        public MainPageCards()
        {
            this.BackgroundColor = Color.FromHex("#FFFFFF");

            RelativeLayout view = new RelativeLayout();

            cardStack = new CardStackView(frontLoadingLayout);
            cardStack.SwipedLeft += SwipedLeft;
            cardStack.SwipedRight += SwipedRight;

            view.Children.Add(cardStack,
                Constraint.Constant(10),
                Constraint.Constant(10),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width - 20;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height - 10;
                })
            );

            this.LayoutChanged += (object sender, EventArgs e) =>
            {
                cardStack.CardMoveDistance = (int)(this.Width * 0.40f);
            };

            ActivityIndicator loadingIcon = new ActivityIndicator()
            {
                WidthRequest = 100,
                HeightRequest = 100,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            loadingIcon.IsRunning = true;

            
            frontLoadingLayout.Children.Add(loadingIcon);

            Label loadingLabel = new Label()
            {
                Text = "Loading Food",
                FontSize = 22
            };
            frontLoadingLayout.Children.Add(loadingLabel);

            view.Children.Add(frontLoadingLayout, Constraint.RelativeToParent(parent => parent.Width * 0.33),
                Constraint.RelativeToParent(parent => parent.Height * 0.33));

            

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



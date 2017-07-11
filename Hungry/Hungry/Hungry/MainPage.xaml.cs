using Hungry.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Hungry
{
    public partial class MainPage : ContentPage
    {

        CardStackView cardStack;
        List<FoodModel> foodList;

        public MainPage(List<FoodModel> foodList)
        {
            this.BackgroundColor = Color.FromHex("#FFFFFF");

            RelativeLayout view = new RelativeLayout();

            cardStack = new CardStackView(foodList);
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



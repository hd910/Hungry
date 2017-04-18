using System;
using Xamarin.Forms;

namespace Hungry
{
    public partial class MainPageCards : ContentPage
    {
        CardStackView cardStack;

        public MainPageCards()
        {
            this.BackgroundColor = Color.FromHex("#FFFFFF");

            RelativeLayout view = new RelativeLayout();

            cardStack = new CardStackView();
            cardStack.SwipedLeft += SwipedLeft;
            cardStack.SwipedRight += SwipedRight;

            view.Children.Add(cardStack,
                Constraint.Constant(10),
                Constraint.Constant(10),
                Constraint.RelativeToParent((parent) => {
                    return parent.Width - 20;
                }),
                Constraint.RelativeToParent((parent) => {
                    return parent.Height - 10;
                })
            );

            this.LayoutChanged += (object sender, EventArgs e) =>
            {
                cardStack.CardMoveDistance = (int)(this.Width * 0.60f);
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



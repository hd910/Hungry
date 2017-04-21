using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace Hungry
{
	public class CardView : ContentView
	{
		public Label Name { get; set;}
		public Image Photo { get; set;}
        public Image[] PreviewPhotos { get; set; }
        public StackLayout previewImagesLayout;
        public Button searchFoodButton;
        public int previewNumber = 5;

        public CardView()
        {
            RelativeLayout view = new RelativeLayout {
                
            };

            //Background box
            BoxView background = new BoxView {
                Color = Color.FromHex("#FFFFFF"),
                InputTransparent = true
            };
            view.Children.Add(background,
                Constraint.Constant(0), Constraint.Constant(0),
                Constraint.RelativeToParent((parent) => {
                    return parent.Width;
                }),
                Constraint.RelativeToParent((parent) => {
                    return parent.Height;
                })
            );

            Grid grid = new Grid {
                RowSpacing = 10,
                Padding = new Thickness(10, 10, 10, 10)
            };
            //Description
            grid.RowDefinitions.Add(new RowDefinition
            {
                Height = new GridLength(30, GridUnitType.Absolute)
            });
            //Main Image
            grid.RowDefinitions.Add(new RowDefinition
            {
                Height = new GridLength(1, GridUnitType.Star)
            });
            //Preview Images
            grid.RowDefinitions.Add(new RowDefinition
            {
                Height = new GridLength(1, GridUnitType.Auto)
            });
            
            //Button
            grid.RowDefinitions.Add(new RowDefinition
            {
                Height = new GridLength(60, GridUnitType.Absolute)
            });

            AbsoluteLayout absoluteLayout = new AbsoluteLayout();

            StackLayout swipeMessageLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            Image leftArrow = new Image()
            {
                WidthRequest = 100,
                Source = ImageSource.FromResource("Hungry.Images.arrow-left.png"),
                Opacity = 0.5
            };
            swipeMessageLayout.Children.Add(leftArrow);

            //Swipe label
            Name = new Label()
            {
                TextColor = Color.LightGray,
                Text = "Swipe to skip",
                FontSize = 16,
                InputTransparent = true
            };

            swipeMessageLayout.Children.Add(Name);

            Image rightArrow = new Image()
            {
                WidthRequest = 100,
                Source = ImageSource.FromResource("Hungry.Images.arrow-right.png"),
                Opacity = 0.5
            };
            swipeMessageLayout.Children.Add(rightArrow);

            grid.Children.Add(swipeMessageLayout, 0, 0);

            //Main food image
            Photo = new Image()
            {
                InputTransparent = true,
                Aspect = Aspect.AspectFill
            };

            absoluteLayout.Children.Add(Photo, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);

            ActivityIndicator loadingIcon = new ActivityIndicator();
            loadingIcon.SetBinding(ActivityIndicator.IsRunningProperty, "IsLoading");
            loadingIcon.BindingContext = Photo;
            absoluteLayout.Children.Add(loadingIcon, new Rectangle(0.5, 0.5, 1, 0.2), AbsoluteLayoutFlags.All);



            grid.Children.Add(absoluteLayout, 0, 1);

            previewImagesLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Start
            };

            grid.Children.Add(previewImagesLayout, 0, 2);

            

            //Button to find food places around
            searchFoodButton = new Button
            {
                Text = "I WANT... FOOD",
                BackgroundColor = Color.FromHex("#f12907"),
                TextColor = Color.FromHex("FFFFFF"),
                BorderRadius = 90,
                FontSize = 22
            };

            grid.Children.Add(searchFoodButton,0,3);

            view.Children.Add(grid,
                Constraint.Constant(0), Constraint.Constant(0),
                Constraint.RelativeToParent((parent) => {
                    return parent.Width;
                }),
                Constraint.RelativeToParent((parent) => {
                    return parent.Height;
                })
            );

            Content = view;
		}
	}
}


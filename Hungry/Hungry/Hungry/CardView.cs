using Xamarin.Forms;

namespace Hungry
{
	public class CardView : ContentView
	{
		public Label Name { get; set;}
		public Image Photo { get; set;}
        public Image[] PreviewPhotos { get; set; }

		public CardView ()
		{
			RelativeLayout view = new RelativeLayout {
                Padding = 10
            };

            //Background box
			BoxView background = new BoxView {
				Color = Color.FromHex("#FFFFFF"),
				InputTransparent=true
			};
			view.Children.Add (background,
				Constraint.Constant (0), Constraint.Constant (0),
				Constraint.RelativeToParent ((parent) => {					
					return parent.Width;
				}),
				Constraint.RelativeToParent ((parent) => {
					return parent.Height;
				})
			);

            Grid grid = new Grid {
                RowSpacing = 10
            };
            //Main Image
            grid.RowDefinitions.Add(new RowDefinition
            {
                Height = new GridLength(1, GridUnitType.Star)
            });
            //Preview Images
            grid.RowDefinitions.Add(new RowDefinition
            {
                Height = new GridLength(50, GridUnitType.Absolute)
            });
            //Description
            grid.RowDefinitions.Add(new RowDefinition
            {
                Height = new GridLength(40, GridUnitType.Absolute)
            });
            //Button
            grid.RowDefinitions.Add(new RowDefinition
            {
                Height = new GridLength(60, GridUnitType.Absolute)
            });

            AbsoluteLayout absoluteLayout = new AbsoluteLayout();

            //Main food image
            Photo = new Image()
            {
                InputTransparent = true,
                Aspect = Aspect.AspectFill
            };

            absoluteLayout.Children.Add(Photo, new Rectangle(0,0,1,1), AbsoluteLayoutFlags.All);

            grid.Children.Add(absoluteLayout, 0, 0);

            StackLayout previewImagesLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Start
            };

            grid.Children.Add(previewImagesLayout, 0, 1);

            //Food name
            Name = new Label()
            {
                TextColor = Color.Black,
                FontSize = 22,
                InputTransparent = true
            };

            grid.Children.Add(Name,0,2);

            //Button to find food places around
            Button searchFoodButton = new Button
            {
                Text = "I WANT... FOOD",
                BackgroundColor = Color.FromHex("#f00c0c"),
                TextColor = Color.FromHex("FFFFFF"),
                BorderRadius = 20,
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


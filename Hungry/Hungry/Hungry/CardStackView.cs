//
//  Copyright (c) 2016 MatchboxMobile
//  Licensed under The MIT License (MIT)
//  http://opensource.org/licenses/MIT
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
//  TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
//  THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
//  CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
//  IN THE SOFTWARE.
//
using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Net.Http;
using ImageCircle.Forms.Plugin.Abstractions;
using System.Linq;

namespace Hungry
{
	public class CardStackView : ContentView
	{
        public class Item
        {
            public string Name { get; set; }
            public List<FoodImage> foodImages { get; set; }
        }

        public class FoodImage
        {
            public string fullSizeUri { get; set; }
            public string thumbnailUri { get; set; }
        }

        string[] foodTypes = new string[14] { "Pizza", "Hamburger", "Sushi", "Pasta", "Bibimbap", "Pho", "Udon Noodles", "Donkatsu", "Salad", "Sandwich", "Burrito", "Fried Rice", "Pad Thai", "Dumpling" };

        // back card scale
        const float BackCardScale = 0.8f;
		// speed of the animations
		const int AnimLength = 250;	
			// 180 / pi
		const float DegreesToRadians = 57.2957795f; 
		// higher the number less the rotation effect
		const float CardRotationAdjuster = 0.3f; 
		// distance a card must be moved to consider to be swiped off
		public int CardMoveDistance {get; set;}

        const int PreviewNumber = 5;

		// two cards
		const int NumCards = 2;
		CardView[] cards = new CardView[NumCards];
		// the card at the top of the stack
		int topCardIndex;
		// distance the card has been moved
		float cardDistance = 0;
		// the last items index added to the stack of the cards
		int itemIndex = 0;
		bool ignoreTouch = false;
        StackLayout frontLoadingLayout;

        private string url = "https://api.flickr.com/services/rest/?method=flickr.photos.search&api_key={0}&text={1}&safe_search=1&per_page={2}&sort=relevance";
        private HttpClient _client = new HttpClient();

        // called when a card is swiped left/right with the card index in the ItemSource
        public Action<int> SwipedRight = null;
		public Action<int> SwipedLeft = null;

        private List<Item> Items = new List<Item>();
		public List<Item> ItemsSource {
			get {
                return Items;
			}
			set {
                if(Items != value)
                {
                    Items = value;
                }		
			}
		}

		public CardStackView (StackLayout loadingLayout)
		{
            Random rnd = new Random();
            foodTypes = foodTypes.OrderBy(x => rnd.Next()).ToArray();
            RelativeLayout view = new RelativeLayout ();

            frontLoadingLayout = loadingLayout;

            // create a stack of cards
            for (int i = 0; i < NumCards; i++) {
				var card = new CardView();
				cards[i] = card;
				card.InputTransparent = true;
				card.IsVisible = false;

				view.Children.Add(
					card,
					Constraint.Constant(0),
					Constraint.Constant(0),
					Constraint.RelativeToParent((parent) =>
					{
						return parent.Width;
					}),
					Constraint.RelativeToParent((parent) =>
					{
						return parent.Height;
					})
				);
			}
            //ActivityIndicator loadingIcon = new ActivityIndicator();
            //view.Children.Add(loadingIcon, widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
            //    heightConstraint: Constraint.RelativeToParent(parent => parent.Width));

            this.BackgroundColor = Color.White;
			this.Content = view;

            

            var panGesture = new PanGestureRecognizer ();
			panGesture.PanUpdated += OnPanUpdated;
			GestureRecognizers.Add (panGesture);

            loadImages();

        }

        private async void loadImages()
        {
            foreach(var foodType in foodTypes){
                var formattedurl = string.Format(url, APIKeys.FLICKR_API_KEY, foodType, PreviewNumber);

                var content = await _client.GetStringAsync(formattedurl);

                if (content != null)
                {
                    var xdoc = XDocument.Parse(content);
                    List<FoodImage> tempImages = new List<FoodImage>();
                    foreach (var node in xdoc.Descendants("photos").Descendants("photo"))
                    {
                        var id = node.Attribute("id").Value;
                        var secretId = node.Attribute("secret").Value;
                        var farmId = node.Attribute("farm").Value;
                        var serverId = node.Attribute("server").Value;

                        var imageURL = string.Format("https://farm{0}.staticflickr.com/{1}/{2}_{3}_z.jpg", farmId, serverId, id, secretId);
                        var thumbImageURL = string.Format("https://farm{0}.staticflickr.com/{1}/{2}_{3}_s.jpg", farmId, serverId, id, secretId);

                        tempImages.Add(new FoodImage()
                        {
                            fullSizeUri = imageURL,
                            thumbnailUri = thumbImageURL
                        });
                    }
                    Items.Add(new Item()
                    {
                        Name = foodType,
                        foodImages = tempImages
                    });
                }
            }
            Setup();

            frontLoadingLayout.Children.Clear();
        }

        void Setup()
		{
			// set the top card
			topCardIndex = 0;
			// create a stack of cards
			for (int i = 0; i < Math.Min(NumCards, ItemsSource.Count); i++)	{
				if (itemIndex >= ItemsSource.Count) break;
				var card = cards[i];
				card.Name.Text = "Hungry? How about " + ItemsSource[itemIndex].Name;

                if(card.previewImagesLayout.Children.Count == 0)
                {
                    for (var index = 0; index < PreviewNumber; index++)
                    {
                        var tempIcon = new CircleImage
                        {
                            HeightRequest = 50,
                            WidthRequest = 50,
                            Aspect = Aspect.AspectFill,
                            Source = UriImageSource.FromUri(new Uri(ItemsSource[itemIndex].foodImages[index].thumbnailUri)),
                            ClassId = itemIndex.ToString() + "_" + index.ToString()
                        };

                        var tapGestureRecognizer = new TapGestureRecognizer();
                        tapGestureRecognizer.Tapped += async (sender, e) =>
                        {
                            var s = (CircleImage)sender;
                            string[] temp = s.ClassId.Split('_');
                            int orderIndex = Int32.Parse(temp[0]);
                            int previewIndex = Int32.Parse(temp[1]);

                            await card.Photo.FadeTo(0, 250);
                            card.Photo.Source = ImageSource.FromUri(new Uri(ItemsSource[orderIndex].foodImages[previewIndex].fullSizeUri));
                            await card.Photo.FadeTo(1, 250);
                        };
                        tempIcon.GestureRecognizers.Add(tapGestureRecognizer);

                        card.previewImagesLayout.Children.Add(tempIcon);
                    }
                }
                
                if (ItemsSource[itemIndex].foodImages != null)
				    card.Photo.Source = ImageSource.FromUri(new Uri(ItemsSource[itemIndex].foodImages[0].fullSizeUri));
                card.searchFoodButton.Text = "Find " + ItemsSource[itemIndex].Name;

                card.IsVisible = true;
				card.Scale = GetScale(i);
				card.RotateTo (0, 0);
				card.TranslateTo (0, - card.Y, 0);
				((RelativeLayout)this.Content).LowerChild (card);
				itemIndex++;
			}
		}
		
		void OnPanUpdated (object sender, PanUpdatedEventArgs e)
		{
			switch (e.StatusType) {
				case GestureStatus.Started:
					HandleTouchStart();
					break;
				case GestureStatus.Running:
					HandleTouch((float)e.TotalX);	
	                break;
				case GestureStatus.Completed:	
					HandleTouchEnd();
					break;
			}
		}	

		// to hande when a touch event begins
		public void HandleTouchStart() 
		{			
			cardDistance = 0;
		}

		// to handle te ongoing touch event as the card is moved
		public void HandleTouch(float diff_x) 
		{				
			if (ignoreTouch) {
				return;
			}

			var topCard = cards [topCardIndex];
			var backCard = cards [PrevCardIndex (topCardIndex)];

			// move the top card
			if (topCard.IsVisible) {

				// move the card
				topCard.TranslationX = (diff_x);

				// calculate a angle for the card
				float rotationAngel = (float)(CardRotationAdjuster * Math.Min (diff_x / this.Width, 1.0f));
				topCard.Rotation = rotationAngel * DegreesToRadians;

				// keep a record of how far its moved
				cardDistance = diff_x;
			}

			// scale the backcard
			if (backCard.IsVisible) {
				backCard.Scale = Math.Min (BackCardScale + Math.Abs ((cardDistance / CardMoveDistance) * (1.0f - BackCardScale)), 1.0f);
			}			
		}

		// to handle the end of the touch event
		async public void HandleTouchEnd()
		{			
			ignoreTouch = true;

			var topCard = cards [topCardIndex];

			// if the card was move enough to be considered swiped off
			if (Math.Abs ((int)cardDistance) > CardMoveDistance) {

				// move off the screen
				await topCard.TranslateTo (cardDistance>0?this.Width:-this.Width, 0, AnimLength/2, Easing.SpringOut);
				topCard.IsVisible = false; 
				
				if (SwipedRight != null && cardDistance > 0) {
					SwipedRight(itemIndex);
				}
				else if (SwipedLeft != null)
				{
					SwipedLeft(itemIndex);
				}

				// show the next card
				ShowNextCard ();

			}
			// put the card back in the center
			else {

				// move the top card back to the center
				topCard.TranslateTo ((-topCard.X), - topCard.Y, AnimLength, Easing.SpringOut);
				topCard.RotateTo (0, AnimLength, Easing.SpringOut);

				// scale the back card down
				var prevCard = cards [PrevCardIndex (topCardIndex)];
				await prevCard.ScaleTo(BackCardScale, AnimLength, Easing.SpringOut);

			}	

			ignoreTouch = false;
		}
			
		// show the next card
		void ShowNextCard()
		{
            if (cards[0].IsVisible == false && cards[1].IsVisible == false)
            {
                Setup();
                return;
            }

            var topCard = cards[topCardIndex];
            topCardIndex = NextCardIndex(topCardIndex);

            if (itemIndex >= ItemsSource.Count)
            {
                itemIndex = 0;
            }

            // if there are more cards to show, show the next card in to place of 
            // the card that was swipped off the screen
            if (itemIndex < ItemsSource.Count)
            {
                // push it to the back z order
                ((RelativeLayout)this.Content).LowerChild(topCard);

                // reset its scale, opacity and rotation
                topCard.Scale = BackCardScale;
                topCard.RotateTo(0, 0);
                topCard.TranslateTo(0, -topCard.Y, 0);

                // set the data
                topCard.Name.Text = "Hungry? How about.. " + ItemsSource[itemIndex].Name;
                topCard.previewImagesLayout.Children.Clear();

                for (var index = 0; index < PreviewNumber; index++)
                {
                    var tempIcon = new CircleImage
                    {
                        HeightRequest = 50,
                        WidthRequest = 50,
                        Aspect = Aspect.AspectFill,
                        Source = UriImageSource.FromUri(new Uri(ItemsSource[itemIndex].foodImages[index].thumbnailUri)),
                        ClassId = itemIndex.ToString() + "_" + index.ToString()
                    };

                    var tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += async (sender, e) =>
                    {
                        var s = (CircleImage)sender;
                        string[] temp = s.ClassId.Split('_');
                        int orderIndex = Int32.Parse(temp[0]);
                        int previewIndex = Int32.Parse(temp[1]);

                        await topCard.Photo.FadeTo(0, 250);
                        topCard.Photo.Source = ImageSource.FromUri(new Uri(ItemsSource[orderIndex].foodImages[previewIndex].fullSizeUri));
                        await topCard.Photo.FadeTo(1, 250);
                    };
                    tempIcon.GestureRecognizers.Add(tapGestureRecognizer);

                    topCard.previewImagesLayout.Children.Add(tempIcon);
                }

                if (ItemsSource[itemIndex].foodImages != null)
                    topCard.Photo.Source = ImageSource.FromUri(new Uri(ItemsSource[itemIndex].foodImages[0].fullSizeUri));

                topCard.searchFoodButton.Text = "Find " + ItemsSource[itemIndex].Name;


                topCard.IsVisible = true;
                itemIndex++;
            }
        }

		// return the next card index from the top
		int NextCardIndex(int topIndex)
		{
			return topIndex == 0 ? 1 : 0;
		}

		// return the prev card index from the yop
		int PrevCardIndex(int topIndex)
		{
			return topIndex == 0 ? 1 : 0;
		}			

		// helper to get the scale based on the card index position relative to the top card
		float GetScale(int index) 
		{			
			return (index == topCardIndex) ? 1.0f : BackCardScale;
		}			
	}
}


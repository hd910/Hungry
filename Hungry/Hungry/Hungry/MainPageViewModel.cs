using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Hungry
{
	public class MainPageViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
        private string url = "https://api.flickr.com/services/rest/?method=flickr.photos.search&api_key={0}&text={1}&safe_search=1&per_page={2}&sort=relevance";
        private HttpClient _client = new HttpClient();

        private List<CardStackView.Item> items = new List<CardStackView.Item>();
		public List<CardStackView.Item> ItemsList
		{
			get	{
				return items;
			}
			set	{
				if (items == value)	{
					return;
				}
				items = value;
				OnPropertyChanged();
			}
		}
		
		protected virtual void OnPropertyChanged ([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null) {
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
			
		protected virtual void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
		{
			field = value;
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null) {
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}

        private async void loadImages(string foodType)
        {

            var formattedurl = string.Format(url, APIKeys.FLICKR_API_KEY, foodType, "5");

            var content = await _client.GetStringAsync(formattedurl);

            if (content != null)
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

                    items.Add(new CardStackView.Item()
                    {
                        Name = foodType,
                        Photo = imageURL
                    });

                }
                OnPropertyChanged(nameof(ItemsList));
            }
        }

        public MainPageViewModel()
		{
            //loadImages("Pizza");
            //items.Add(new CardStackView.Item() { Name = "Pizza", Photo = "" });
            //items.Add(new CardStackView.Item() { Name = "Pizza 2", Photo = "" });
            //items.Add(new CardStackView.Item() { Name = "Pizza 3", Photo = "" });
            //items.Add(new CardStackView.Item() { Name = "Pizza 4", Photo = "" });
            items.Add(new CardStackView.Item() { Name = "Pizza", Photo = "https://farm9.staticflickr.com/8242/8487666183_75e2e25206_z.jpg" });
            items.Add(new CardStackView.Item() { Name = "Pizza 2", Photo = "https://farm6.staticflickr.com/5238/5913452967_2c1cde583b_z.jpg" });
            items.Add(new CardStackView.Item() { Name = "Pizza 3", Photo = "https://farm9.staticflickr.com/8458/8061492584_0f320a0ef9_z.jpg" });
            items.Add(new CardStackView.Item() { Name = "Pizza 4", Photo = "https://farm8.staticflickr.com/7177/6825947764_6c42da48fe_z.jpg" });
            //items.Add(new CardStackView.Item() { Name = "Pizza to go", Photo = "one.jpg" });
            //items.Add(new CardStackView.Item() { Name = "Dragon & Peacock", Photo = "two.jpg" });
            //items.Add(new CardStackView.Item() { Name = "Murrays Food Palace", Photo = "three.jpg" });
            //items.Add(new CardStackView.Item() { Name = "Food to go", Photo = "four.jpg" });
            //items.Add(new CardStackView.Item() { Name = "Mexican Joint", Photo = "five.jpg" });
            //items.Add(new CardStackView.Item() { Name = "Mr Bens", Photo = "six.jpg" });
            //items.Add(new CardStackView.Item() { Name = "Corner Shop", Photo = "seven.jpg" });
            //items.Add(new CardStackView.Item() { Name = "Sarah's Cafe", Photo = "eight.jpg" });
            //items.Add(new CardStackView.Item() { Name = "Pata Place", Photo = "nine.jpg" });
            //items.Add(new CardStackView.Item() { Name = "Jerrys", Photo = "ten.jpg" });
        }
    }
}


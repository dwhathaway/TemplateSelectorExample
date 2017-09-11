using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TemplateSelectorExample.Models;
using Xamarin.Forms;

namespace TemplateSelectorExample.ViewModels
{
    public class ListItemsViewModel : BaseViewModel
    {
        public const string PlacedAPIKey = "AIzaSyCWfGCYUxbUWXPdYgHhvaSNPHKraXApluo";
        public const string GeocodeAPIKey = "AIzaSyC7Exl8DQvDDwEXC3h93viqe8EgINXiaGo";

        const string CoffeeQueryUrl = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?types=cafe&location={0},{1}&opennow=true&rankby=distance&key={2}";
        const string SearchQueryUrl = "https://maps.googleapis.com/maps/api/place/textsearch/json?query={0}&location={1},{2}&radius=1000&opennow=true&key={3}";
        const string LocationQueryUrl = "https://maps.googleapis.com/maps/api/place/autocomplete/json?input={0}&type=(cities)&key={1}";
        const string GeocodeQueryUrl = "https://maps.googleapis.com/maps/api/geocode/json?address={0}&key={1}";

        public ListItemsViewModel()
        {
            this.Title = "List of beings";
            ListItems = new ObservableCollection<IListViewItem>();
        }

        private string searchTest;
        /// <summary>
        /// Gets or sets if VM is busy working
        /// </summary>
        public string SearchText
        {
            get { return searchTest; }
            set { searchTest = value; OnPropertyChanged("SearchText"); }
        }

        private Geopoint geoPoint;

        public Geopoint Location
        {
            get { return geoPoint; }
            set { geoPoint = value; OnPropertyChanged(("GeoPoint")); }
        }

        /// <summary>
        /// gets or sets the feed items
        /// </summary>
        public ObservableCollection<IListViewItem> ListItems
        {
            get;
            private set;
        }

        private Command refreshSearchResultsCommand;
        /// <summary>
        /// Command to load/refresh autocomplete entries
        /// </summary>
        public Command RefreshSearchResults
		{
			get { return refreshSearchResultsCommand ?? (refreshSearchResultsCommand = new Command(async () => await ExecuteRefreshItems())); }
		}

        public async Task ExecuteRefreshItems()
        {
            ExecuteRefreshItems(null);
        }

        public async Task ExecuteRefreshItems(LocationResult location)
		{
			IsBusy = true;
            PullToRefresh = false;
            string requestUri = string.Empty;

            if(location == null)
			{
                //If no geopoint is already saved, get from device location
                if(Location == null)
                {
                    //ToDo: Implement device location lookup
                }
			}
			else
			{
                var geocodeRequestUri = string.Format(GeocodeQueryUrl, location.Title, GeocodeAPIKey);

				try
				{
					using (var client = new HttpClient())
					{
						client.Timeout = TimeSpan.FromSeconds(10);
						var result = await client.GetStringAsync(geocodeRequestUri);
                        var queryObject = JsonConvert.DeserializeObject<GeocodeResult>(result);

                        Location = new Geopoint() { lat = queryObject.results[0].geometry.location.lat, lng = queryObject.results[0].geometry.location.lng };

						requestUri = string.Format(CoffeeQueryUrl,
								  Location.lat, Location.lng,
								   PlacedAPIKey);
					}
				}
				catch (Exception ex)
				{
                    Console.WriteLine(ex.ToString());
				}
            }

			try
			{
				using (var client = new HttpClient())
				{
					client.Timeout = TimeSpan.FromSeconds(10);
					var result = await client.GetStringAsync(requestUri);
                    var queryObject = JsonConvert.DeserializeObject<SearchResults>(result);

                    ListItems.Clear();

                    foreach (var item in queryObject.results)
                    {
                    	ListItems.Add(new SearchResult() { Title = item.name + ", " + item.vicinity });
                    }
				}
			}
			catch (Exception ex)
			{
                Console.WriteLine(ex.ToString());
			}
			finally
			{
				IsBusy = false;
				PullToRefresh = true;
			}
        }

		private Command autocompleteCommand;
		/// <summary>
		/// Command to load/refresh autocomplete entries
		/// </summary>
		public Command UpdateLocationAutoComplete
		{
			get { return autocompleteCommand ?? (autocompleteCommand = new Command(async () => await ExecuteUpdateLocationAutoComplete())); }
        }

        private async Task ExecuteUpdateLocationAutoComplete()
        {
            if (IsBusy)
                return;

			PullToRefresh = false;
			IsBusy = true;

            IsBusy = true;

			var requestUri = string.Format(LocationQueryUrl,
                   SearchText,
					PlacedAPIKey);

            try
            {
                using (var client = new HttpClient())
    			{
    				client.Timeout = TimeSpan.FromSeconds(10);
    				var result = await client.GetStringAsync(requestUri);
                    var queryObject = JsonConvert.DeserializeObject<LocationQueryResult>(result);

                    ListItems.Clear();

                    foreach(var item in queryObject.Places)
                    {
                        ListItems.Add(new LocationResult() { Title = item.Terms[0].Value.ToString() + ", " + item.Terms[1].Value });
                    }
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());  
            } 
            finally
			{
				IsBusy = false;
            }
        }
    }
}

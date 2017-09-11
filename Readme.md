# A simple Xamarin.Forms Template Selector example

A simple Xamarin.Forms Template Selector example based on James Montemagno's [Coffee Filter](https://github.com/jamesmontemagno/Coffee-Filter) app.  The app allows the user to enter the name of the city that they want to search for cafe's in, and then select a location to show search results for.  The same `<ListView/>` object is used to show both the search for cities, as well as the actual subsequent search results, but uses Template Selectors to change the way the each cell in the `<ListView />` is displayed depending on the context of the view.

To use the app, compile and launch, and then enter a city into the `<SearchBar />` at the top of the page, and then submit the search.  Note that the list of results from the search will show right-justified, and in red text.  Tap on any item in the list to choose a city, and the list will refresh, and show actual search results within that city.  However, ther search results will show left-justified with blue text.

Pull to refresh is enabled on the Search Results page, but disabled on the City search page.

# General Features Demonstrated

- Xamarin.Forms Template Selectors
- Xamarin.Forms Pull-to-Refresh
- Google Places & Geocoding APIs
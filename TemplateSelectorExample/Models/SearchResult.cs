using System;
namespace TemplateSelectorExample.Models
{
    public class SearchResult : IListViewItem
    {
		public string Title { get; set; }
    }

    public class LocationResult : IListViewItem
	{
		public string Title { get; set; }
    }
}

using System;
using TemplateSelectorExample.Models;
using Xamarin.Forms;

namespace TemplateSelectorExample.DataTemplateSelectors
{
    public class ItemTemplateSelector : DataTemplateSelector
    {
        public ItemTemplateSelector()
        {
        }

		public DataTemplate SearchResultTemplate { get; set; }
		public DataTemplate LocationResultTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
		{
			IListViewItem listItem = (IListViewItem)item;

            DataTemplate dt = SearchResultTemplate;

            if (listItem.GetType() == typeof(LocationResult))
			{
				dt = LocationResultTemplate;
            }

            return dt;
        }
    }
}
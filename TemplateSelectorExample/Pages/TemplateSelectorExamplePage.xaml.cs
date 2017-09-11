using System.Collections.Generic;
using TemplateSelectorExample.Models;
using TemplateSelectorExample.ViewModels;
using Xamarin.Forms;

namespace TemplateSelectorExample.Pages
{
    public partial class TemplateSelectorExamplePage : ContentPage
    {
        ListItemsViewModel viewModel;

        public TemplateSelectorExamplePage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ListItemsViewModel();

            myListView.ItemTapped += async (object sender, ItemTappedEventArgs e) => {
                IListViewItem item = (IListViewItem)e.Item;

                if (item.GetType() == typeof(LocationResult))
                {
                    await viewModel.ExecuteRefreshItems((LocationResult)item);
				}
				else if (item.GetType() == typeof(SearchResult))
				{
					//Navigate to details page

				}
            };

			myListView.Refreshing += async (object sender, System.EventArgs e) => {
				await viewModel.ExecuteRefreshItems();
            };
        }
    }
}

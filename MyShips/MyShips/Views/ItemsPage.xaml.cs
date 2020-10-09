using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MyShips.Models;
using MyShips.Views;
using MyShips.ViewModels;

namespace MyShips.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemsPage : ContentPage
	{
		ItemsViewModel viewModel;

		public ItemsPage()
		{
			InitializeComponent();

			BindingContext = viewModel = new ItemsViewModel();
		}

		async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
		{
			var item = args.SelectedItem as Item;

			if (item != null)
			{
				try
				{
					ItemDetailViewModel idvm = new ItemDetailViewModel(item);

					ItemDetailPage idp = new ItemDetailPage(idvm);

					await Navigation.PushAsync(idp);

					// Manually deselect item.
					ItemsListView.SelectedItem = null;
				}
				catch (Exception exUnhandled)
				{

					String x = exUnhandled.TargetSite.ToString();

					exUnhandled.Data.Add("Item.ID", item?.Id);
				}
			}
			else
			{
				return;
			}
		}

		async void AddItem_Clicked(object sender, EventArgs e)
		{
			String encryptionKey = ContextMgr.Instance.ContextValues[MyShips.Properties.Resources.EncryptionKey].ToString();

			String encryptionSeed = ContextMgr.Instance.ContextValues[MyShips.Properties.Resources.EncryptionSeed].ToString();

			if ((encryptionKey.Length >= 32) && (encryptionSeed.Length >= 8))
			{
				await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
			}
			else
			{
				await DisplayAlert("Encryption Settings Missing", "Before you can add new items, you must open the Settings page and setup an encryption key and seed to encrypt the item data.", "OK", "Cancel");
			}
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (viewModel.Items.Count == 0)
				viewModel.LoadItemsCommand.Execute(null);
		}
	}
}
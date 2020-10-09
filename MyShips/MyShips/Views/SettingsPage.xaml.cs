using MyShips.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShips.Models;
using MyShips.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;


namespace MyShips.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{

		SettingsViewModel settingsViewModel;

		public SettingsPage()
		{

			InitializeComponent();

			String encryptionKey = ContextMgr.Instance.ContextValues[MyShips.Properties.Resources.EncryptionKey].ToString() ?? "";
			String encryptionSeed = ContextMgr.Instance.ContextValues[MyShips.Properties.Resources.EncryptionSeed].ToString() ?? "";

			SettingsModel settingsModel = new SettingsModel
			{
				EncryptionKey = encryptionKey,
				EncryptionSeed = encryptionSeed
			};

			settingsViewModel = new SettingsViewModel(settingsModel);

			BindingContext = settingsViewModel;

		}

		public SettingsPage(SettingsViewModel settingsVM)
		{
			InitializeComponent();

			BindingContext = this.settingsViewModel = settingsVM;
		}

		private async void btnSave_Clicked(object sender, EventArgs e)
		{

			String encryptionKey = txtEncryptionKey.Text.Trim();
			String encryptionSeed = txtEncryptionSeed.Text.Trim();

			if ((encryptionKey.Length >= 32) && (encryptionSeed.Length >= 8))
			{
				await SecureStorage.SetAsync(MyShips.Properties.Resources.EncryptionKey, encryptionKey);
				await SecureStorage.SetAsync(MyShips.Properties.Resources.EncryptionSeed, encryptionSeed);

				ContextMgr.Instance.ContextValues[MyShips.Properties.Resources.EncryptionKey] = encryptionKey;
				ContextMgr.Instance.ContextValues[MyShips.Properties.Resources.EncryptionSeed] = encryptionSeed;

				await DisplayAlert("Settings Saved", "The settings have been saved securely.", "OK");
			}
			else
			{
				await DisplayAlert("Settings NOT Saved", "The settings have been NOT been saved because the length was too short.", "OK");
			}


		}

		private void txtEncryptionKey_TextChanged(object sender, TextChangedEventArgs e)
		{
			if ((txtEncryptionKey.Text != null) &&
				(txtEncryptionSeed.Text != null))
			{
				if ((txtEncryptionKey.Text.Trim().Length >= 32) &&
					(txtEncryptionSeed.Text.Trim().Length >= 8))
				{
					btnSave.IsEnabled = true;
				}
				else
				{
					btnSave.IsEnabled = false;
				}

				lblEncryptionKey.Text = "Encryption Key (32 or more characters) [" + txtEncryptionKey.Text.Trim().Length.ToString() + "]";

			}
		}

		private void txtEncryptionKey_Completed(object sender, EventArgs e)
		{
			if ((txtEncryptionKey.Text != null) &&
				(txtEncryptionSeed.Text != null))
			{
				if ((txtEncryptionKey.Text.Trim().Length >= 32) &&
					(txtEncryptionSeed.Text.Trim().Length >= 8))
				{
					btnSave.IsEnabled = true;
				}
				else
				{
					btnSave.IsEnabled = false;
				}

				lblEncryptionKey.Text = "Encryption Key (32 or more characters) [" + txtEncryptionKey.Text.Trim().Length.ToString() + "]";

			}
		}

		private void txtEncryptionSeed_TextChanged(object sender, TextChangedEventArgs e)
		{
			if ((txtEncryptionKey.Text != null) &&
				(txtEncryptionSeed.Text != null))
			{
				if ((txtEncryptionKey.Text.Trim().Length >= 32) &&
					(txtEncryptionSeed.Text.Trim().Length >= 8))
				{
					btnSave.IsEnabled = true;
				}
				else
				{
					btnSave.IsEnabled = false;
				}

				lblEncryptionSeed.Text = "Encryption Seed (8 or more characters) [" + txtEncryptionSeed.Text.Trim().Length.ToString() + "]";

			}
		}

		private void txtEncryptionSeed_Completed(object sender, EventArgs e)
		{
			if ((txtEncryptionKey.Text != null) &&
				(txtEncryptionSeed.Text != null))
			{
				if ((txtEncryptionKey.Text.Trim().Length >= 32) &&
				(txtEncryptionSeed.Text.Trim().Length >= 8))
				{
					btnSave.IsEnabled = true;
				}
				else
				{
					btnSave.IsEnabled = false;
				}

				lblEncryptionSeed.Text = "Encryption Seed (8 or more characters) [" + txtEncryptionSeed.Text.Trim().Length.ToString() + "]";

			}
		}
	}
}
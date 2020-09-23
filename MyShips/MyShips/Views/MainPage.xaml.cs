using MyShips.Models;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyShips.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : MasterDetailPage
	{
		Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
		public MainPage()
		{
			InitializeComponent();

			MasterBehavior = MasterBehavior.Popover;

			MenuPages.Add((int)MenuItemType.Browse, (NavigationPage)Detail);

			if (Device.RuntimePlatform != "UWP")
			{
				if (CrossFingerprint.Current.IsAvailableAsync().Result)
				{

					Task<FingerprintAuthenticationResult> result = Task.Run(async () =>
					{
						AuthenticationRequestConfiguration conf = new AuthenticationRequestConfiguration("Login via Fingerprint", "Tap that fingerprint sensor!");

						FingerprintAuthenticationResult oResult = await CrossFingerprint.Current.AuthenticateAsync(conf);

						return oResult;
					});

					if (result.Result.Authenticated)

					{

						DisplayAlert("Results are here", "Valid fingerprint found", "Ok");

					}

					else

					{

						DisplayAlert("Results are here", "Invalid fingerprint", "Ok");

					}
				}
			}
		}

		public async Task NavigateFromMenu(int id)
		{
			if (!MenuPages.ContainsKey(id))
			{
				switch (id)
				{
					case (int)MenuItemType.Browse:
						MenuPages.Add(id, new NavigationPage(new ItemsPage()));
						break;
					case (int)MenuItemType.About:
						MenuPages.Add(id, new NavigationPage(new AboutPage()));
						break;
				}
			}

			var newPage = MenuPages[id];

			if (newPage != null && Detail != newPage)
			{
				Detail = newPage;

				if (Device.RuntimePlatform == Device.Android)
					await Task.Delay(100);

				IsPresented = false;
			}
		}
	}
}
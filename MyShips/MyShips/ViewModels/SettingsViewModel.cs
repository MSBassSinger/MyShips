using MyShips.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShips.ViewModels
{
	/// <summary>
	/// Class for an instance to hold settings for viewing.
	/// </summary>
	public class SettingsViewModel : BaseViewModel
	{

		public SettingsModel Settings { get; set; }


		public SettingsViewModel(SettingsModel settingsModel)
		{

			this.Settings = settingsModel;
			Title = "MyShips Settings";

		}

	}
}

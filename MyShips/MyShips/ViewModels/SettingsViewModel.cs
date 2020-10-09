using MyShips.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShips.ViewModels
{
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

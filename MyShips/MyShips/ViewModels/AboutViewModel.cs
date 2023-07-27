using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyShips.ViewModels
{
	/// <summary>
	/// Viewmodel for the About page.
	/// </summary>
	public class AboutViewModel : BaseViewModel
	{
		/// <summary>
		/// Brings up an external page about Xamarin Forms
		/// </summary>
		public AboutViewModel()
		{
			Title = "About";

			//OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));
            OpenWebCommand = new Command(() => Launcher.OpenAsync(new Uri("https://xamarin.com/platform")));
        }

		/// <summary>
		/// Gets the name of the device type
		/// </summary>
        public String DeviceType
		{
			get
			{
				DeviceType retVal = (DeviceType)ContextMgr.Instance.ContextValues["DeviceType"];

				return retVal.ToString();
			}
		}

		/// <summary>
		/// Returns the Manufacturer name 
		/// </summary>
		public String Manufacturer
		{
			get
			{
				String retVal = (String)ContextMgr.Instance.ContextValues["Manufacturer"];

				return retVal;
			}
		}

		/// <summary>
		/// Returns the Model name.
		/// </summary>
		public String Model
		{
			get
			{
				String retVal = (String)ContextMgr.Instance.ContextValues["Model"];

				return retVal;
			}
		}

		/// <summary>
		/// Returns the name.
		/// </summary>
		public String Name
		{
			get
			{
				String retVal = (String)ContextMgr.Instance.ContextValues["Name"];

				return retVal;
			}
		}

		/// <summary>
		/// Returns platform information
		/// </summary>
		public String Platform
		{
			get
			{
				DevicePlatform retVal = (DevicePlatform)ContextMgr.Instance.ContextValues["Platform"];

				return retVal.ToString();
			}
		}

		/// <summary>
		/// Returns a version number.
		/// </summary>
		public String Version
		{
			get
			{
				String retVal = (String)ContextMgr.Instance.ContextValues["Version"];

				return retVal;
			}
		}

		/// <summary>
		/// Returns the date and time the instance started.
		/// </summary>
		public String StartTime
		{
			get
			{
				DateTime retVal = (DateTime)ContextMgr.Instance.ContextValues["StartTime"];

				return retVal.ToShortDateString() + " " + retVal.ToShortTimeString();
			}
		}

		/// <summary>
		/// Returns with the number of processors available.
		/// </summary>
		public String ProcessorCount
		{
			get
			{
				Int32 retVal = (Int32)ContextMgr.Instance.ContextValues["ProcessorCount"];

				return retVal.ToString();
			}
		}

		/// <summary>
		/// Returns the app name
		/// </summary>
		public String AppName
		{
			get
			{
				String retVal = (String)ContextMgr.Instance.ContextValues["AppName"];

				return retVal;
			}
		}
		
		/// <summary>
		/// returns the batter level of the mobile device.
		/// </summary>
		public String BatteryLevel
		{
			get
			{
				Double retVal = Math.Abs((Double)ContextMgr.Instance.ContextValues["BatteryLevel"]);

				return $"{retVal.ToString("P")}";
			}
		}

		/// <summary>
		/// This is set in the constructor.
		/// </summary>
		public ICommand OpenWebCommand { get; }
	}
}
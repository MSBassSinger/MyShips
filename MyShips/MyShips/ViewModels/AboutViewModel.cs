using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyShips.ViewModels
{
	public class AboutViewModel : BaseViewModel
	{
		public AboutViewModel()
		{
			Title = "About";

			OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));
		}

		//ContextMgr.Instance.ContextValues.Add("DeviceType", DeviceInfo.DeviceType);
		public String DeviceType
		{
			get
			{
				DeviceType retVal = (DeviceType)ContextMgr.Instance.ContextValues["DeviceType"];

				return retVal.ToString();
			}
		}

		//ContextMgr.Instance.ContextValues.Add("Manufacturer", DeviceInfo.Manufacturer);
		public String Manufacturer
		{
			get
			{
				String retVal = (String)ContextMgr.Instance.ContextValues["Manufacturer"];

				return retVal;
			}
		}

		//ContextMgr.Instance.ContextValues.Add("Model", DeviceInfo.Model);
		public String Model
		{
			get
			{
				String retVal = (String)ContextMgr.Instance.ContextValues["Model"];

				return retVal;
			}
		}

		//ContextMgr.Instance.ContextValues.Add("Name", DeviceInfo.Name);
		public String Name
		{
			get
			{
				String retVal = (String)ContextMgr.Instance.ContextValues["Name"];

				return retVal;
			}
		}

		//ContextMgr.Instance.ContextValues.Add("Platform", DeviceInfo.Platform);
		public String Platform
		{
			get
			{
				DevicePlatform retVal = (DevicePlatform)ContextMgr.Instance.ContextValues["Platform"];

				return retVal.ToString();
			}
		}

		//ContextMgr.Instance.ContextValues.Add("Version", DeviceInfo.VersionString);
		public String Version
		{
			get
			{
				String retVal = (String)ContextMgr.Instance.ContextValues["Version"];

				return retVal;
			}
		}

		//ContextMgr.Instance.ContextValues.Add("StartTime", DateTime.Now);
		public String StartTime
		{
			get
			{
				DateTime retVal = (DateTime)ContextMgr.Instance.ContextValues["StartTime"];

				return retVal.ToShortDateString() + " " + retVal.ToShortTimeString();
			}
		}

		//ContextMgr.Instance.ContextValues.Add("ProcessorCount", Environment.ProcessorCount);
		public String ProcessorCount
		{
			get
			{
				Int32 retVal = (Int32)ContextMgr.Instance.ContextValues["ProcessorCount"];

				return retVal.ToString();
			}
		}

		//ContextMgr.Instance.ContextValues.Add("AppName", Xamarin.Essentials.AppInfo.Name);
		public String AppName
		{
			get
			{
				String retVal = (String)ContextMgr.Instance.ContextValues["AppName"];

				return retVal;
			}
		}
		//ContextMgr.Instance.ContextValues.Add("BatteryLevel", Xamarin.Essentials.Battery.ChargeLevel* 100);
		public String BatteryLevel
		{
			get
			{
				Double retVal = Math.Abs((Double)ContextMgr.Instance.ContextValues["BatteryLevel"]);

				return $"{retVal.ToString("P")}";
			}
		}

		public ICommand OpenWebCommand { get; }
	}
}
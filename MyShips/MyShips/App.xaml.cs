using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyShips.Services;
using MyShips.Views;
using Xamarin.Essentials;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MyShips
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        public static string AzureBackendUrl = "http://localhost:5000";
        public static bool UseMockDataStore = true;

        public App()
        {
            InitializeComponent();

            ContextMgr.Instance.ContextValues.Add("DeviceType", DeviceInfo.DeviceType);
            ContextMgr.Instance.ContextValues.Add("Manufacturer", DeviceInfo.Manufacturer);
            ContextMgr.Instance.ContextValues.Add("Model", DeviceInfo.Model);
            ContextMgr.Instance.ContextValues.Add("Name", DeviceInfo.Name);
            ContextMgr.Instance.ContextValues.Add("Platform", DeviceInfo.Platform);
            ContextMgr.Instance.ContextValues.Add("Version", DeviceInfo.VersionString);
            ContextMgr.Instance.ContextValues.Add("StartTime", DateTime.Now);
            ContextMgr.Instance.ContextValues.Add("ProcessorCount", Environment.ProcessorCount);
            ContextMgr.Instance.ContextValues.Add("AppName", Xamarin.Essentials.AppInfo.Name);
            ContextMgr.Instance.ContextValues.Add("BatteryLevel", Xamarin.Essentials.Battery.ChargeLevel);

            if (UseMockDataStore)
            {
                DependencyService.Register<MockDataStore>();
                 
            }
            else
            {
                DependencyService.Register<AzureDataStore>();
            }

            MainPage = new MainPage();
            //MainPage = new NavigationPage(new MainPage());

        }

        protected override void OnStart()
        {
            // Handle when your app starts
            
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

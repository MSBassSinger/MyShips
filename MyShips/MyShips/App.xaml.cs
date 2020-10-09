using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyShips.Services;
using MyShips.Views;
using MyShips.Properties;
using Xamarin.Essentials;
using System.Resources;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MyShips
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        public static string AzureBackendUrl = "http://localhost:5000";
        public static bool UseMockDataStore = true;

        //https://docs.microsoft.com/en-us/xamarin/essentials/ 
        //https://docs.microsoft.com/en-us/xamarin/essentials/file-system-helpers?context=xamarin%2Fxamarin-forms&tabs=ios 
        //https://www.bing.com/search?q=how%20to%20store%20files%20using%20xamarin%20with%20encryption&qs=n&form=QBRE&msbsrank=0_1&sp=-1&pq=how%20to%20store%20files%20using%20xamarin%20with%20encryption&sc=0-48&sk=&cvid=B7F0370665AF4A04A3DC677CA1D71BED 
        //https://medium.com/@EeKayOnline/two-ways-to-implement-secure-storage-in-your-xamarin-forms-mobile-app-96ed2cdc2fbf 
        //https://security.stackexchange.com/questions/191028/where-to-store-aes-keys-when-encrypting-files-at-rest 
        //https://www.bing.com/search?q=where+to+store+files+locally+in+xamarin&form=ANNTH1&refig=3a018f15ee96483ecbeb138794077e30&sp=-1&pq=where+to+store+files+locally+in+xamarin&sc=0-39&qs=n&sk=&cvid=3a018f15ee96483ecbeb138794077e30 


        public App()
        {
            InitializeComponent();

            // Read in encryption key and seed from SecureStorage
            String key = SecureStorage.GetAsync(MyShips.Properties.Resources.EncryptionKey).Result ?? "";
            String seed = SecureStorage.GetAsync(MyShips.Properties.Resources.EncryptionSeed).Result ?? "";

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
            ContextMgr.Instance.ContextValues.Add(MyShips.Properties.Resources.EncryptionKey, key);
            ContextMgr.Instance.ContextValues.Add(MyShips.Properties.Resources.EncryptionSeed, seed);

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

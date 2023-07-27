using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using MyShips.Models;
using System.Text.Json;
using System.IO;


namespace MyShips.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Item = new Item
            {
                Id = Guid.NewGuid().ToString(),
                Text = "",
                Description = "",
                LongDescription = ""
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            String fileFolder = FileSystem.AppDataDirectory;

            fileFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            String fileName = Item.Text.Replace(" ", "_").Replace("/", "_").Replace(@"\", "_") + ".item";

            String fullFileName = Path.Combine(fileFolder, fileName);

            String jsonValue = JsonSerializer.Serialize<Item>(Item);

            String encryptionKey = ContextMgr.Instance.ContextValues[MyShips.Properties.Resources.EncryptionKey].ToString();

            String encryptionSeed = ContextMgr.Instance.ContextValues[MyShips.Properties.Resources.EncryptionSeed].ToString();

            CryptoMgr encryptMgr = new CryptoMgr(encryptionKey, encryptionSeed);

            String encryptedString = encryptMgr.EncryptStringAES(jsonValue);


            try
            {
                File.WriteAllText(fullFileName, encryptedString);

                MessagingCenter.Send(this, "AddItem", Item);

            }
            catch (Exception exUnhandled)
            {
                await DisplayAlert("Unable to save the item.", $"The error message is: [{exUnhandled.Message}].", "OK", "Cancel");
            }

            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
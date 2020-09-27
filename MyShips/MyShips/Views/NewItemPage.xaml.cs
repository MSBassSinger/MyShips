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

            String fileName = Item.Text.Replace(" ", "_").Replace("/", "_").Replace(@"\", "_") + ".item";

            String fullFileName = fileFolder + "/" + fileName;

            if (Device.RuntimePlatform == Device.UWP)
            {
                fullFileName = fileFolder + @"\" + fileName;
            }

            String jsonValue = JsonSerializer.Serialize<Item>(Item);

            File.WriteAllText(fullFileName, jsonValue);

            MessagingCenter.Send(this, "AddItem", Item);

            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
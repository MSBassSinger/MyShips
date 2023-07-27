using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using MyShips.Models;
using MyShips.Views;

namespace MyShips.ViewModels
{
    /// <summary>
    /// ViewModel for the collection of Item instances
    /// </summary>
    public class ItemsViewModel : BaseViewModel
    {
        /// <summary>
        /// Internal collection
        /// </summary>
        public ObservableCollection<Item> Items { get; set; }

        /// <summary>
        /// Command that loads the items, set in the constructor
        /// </summary>
        public Command LoadItemsCommand { get; set; }

        /// <summary>
        /// Construtor
        /// </summary>
        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        /// <summary>
        /// Loads the items to the collection from the DataStore.
        /// </summary>
        /// <returns></returns>
        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
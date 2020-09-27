using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MyShips.Models;
using MyShips.ViewModels;

namespace MyShips.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Item 
            {
                Id = Guid.NewGuid().ToString(),
                Text = "",
                //Description = "",
                //LongDescription = "",
                //PictureName = "",
                //Picture = null
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }

	}
}
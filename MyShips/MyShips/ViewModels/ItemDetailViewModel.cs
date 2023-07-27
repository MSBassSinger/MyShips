using System;

using MyShips.Models;

namespace MyShips.ViewModels
{
    /// <summary>
    /// Class for instance(s) of the item detail.
    /// </summary>
    public class ItemDetailViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets or Sets the item 
        /// </summary>
        public Item Item { get; set; }

        /// <summary>
        /// The detail viewmodel constructor
        /// </summary>
        /// <param name="item"></param>
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}

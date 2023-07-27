using System;
using Xamarin.Forms;

namespace MyShips.Models
{
    /// <summary>
    /// Holds information about an item to be shown.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// The unique id for the item.
        /// </summary>
        public String Id { get; set; }

        /// <summary>
        /// What the item is.
        /// </summary>
        public String Text { get; set; }

        /// <summary>
        /// A brief descriotion of the item.
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// The binary image to be shown.
        /// </summary>
        public Image Picture { get; set; }

        /// <summary>
        /// A longer description for more detail.
        /// </summary>
        public String LongDescription { get; set; }

        /// <summary>
        /// A name for the Picture property.
        /// </summary>
        public String PictureName { get; set; }



    }
}
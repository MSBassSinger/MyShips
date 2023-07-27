using System;
using System.Collections.Generic;
using System.Text;

namespace MyShips.Models
{
    /// <summary>
    /// What type of menu item this instance is.
    /// </summary>
    public enum MenuItemType
    {
        Browse,
        About,
        Settings
    }

    /// <summary>
    /// A class used to define a menu item.
    /// </summary>
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public String Title { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyShips.Models
{
    public enum MenuItemType
    {
        Browse,
        About,
        Settings
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}

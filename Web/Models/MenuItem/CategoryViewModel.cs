using KO.Entities;
using System;
using System.Collections.Generic;
using Web.Models;

namespace KO.Web.Models.MenuItem
{
    public class CategoryViewModel : BaseViewModel
    {
        public CategoryViewModel()
        {

            this.MenuItems = new List<MenuItemViewModel>();

        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string CategoryImageUrl { get; set; }

        public bool Active { get; set; }

        public List<MenuItemViewModel> MenuItems { get; set; }

    }
}

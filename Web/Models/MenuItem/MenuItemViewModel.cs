using KO.Entities;
using System;
using System.Collections.Generic;
using Web.Models;

namespace KO.Web.Models.Aplicaciones
{
    public class MenuItemViewModel : BaseViewModel
    {
        public MenuItemViewModel()
        {

            this.CategoryList = new List<Category>();

        }

        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public bool Active { get; set; }

        public DateTime CreationDate { get; set; }

        public int CreationUser { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? UpdateUser { get; set; }

        public List<Category> CategoryList { get; set; }
    }
}

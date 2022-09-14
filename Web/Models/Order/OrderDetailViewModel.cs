using System;
using System.Collections.Generic;
using Web.Models;

namespace KO.Web.Models.Order
{
    public class OrderDetailViewModel : BaseViewModel
    {
        public OrderDetailViewModel() 
        {
            this.OrderDetails = new List<OrderDetailViewModel>();
        }
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int MenuItemId  { get; set; }

        public string MenuItemName { get; set; }

        public string MenuItemPicture { get; set; }

        public int Quantity { get; set; }

        public int? RelatedMenuItemId { get; set; }

        public string RelatedMenuItemName { get; set; }

        public double UnitPrice { get; set; }

        public int StateTypeId { get; set; }

        public string StateTypeName { get; set; }

        public List<OrderDetailViewModel> OrderDetails { get; set; }

    }

}


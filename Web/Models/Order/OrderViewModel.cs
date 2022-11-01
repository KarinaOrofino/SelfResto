using System;
using System.Collections.Generic;
using Web.Models;

namespace KO.Web.Models.Order
{
    public class OrderViewModel : BaseViewModel
    {
        public OrderViewModel()
        {

            this.OrderDetails = new List<OrderDetailViewModel>();
        }
        public int Id { get; set; }

        public int TableId { get; set; }

        public int TableNumber { get; set; }

        public bool Call { get; set; }

        public bool PaymentRequest { get; set; }

        public string WaiterName { get; set; }

        public string WaiterBackUpName { get; set; }

        public bool Active { get; set; }

        public DateTime RequestedTime { get; set; } //HORA DE PEDIDO DEL PRIMER ITEM (ORDER DETAIL)

        public string RequestedTimeString { get; set; }

        public string TiempoEnCocina { get; set; }

        public int ItemsTotalQuantity { get; set; }

        public List<OrderDetailViewModel> OrderDetails { get; set; }

    }
}


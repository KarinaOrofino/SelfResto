using System;
using Web.Models;

namespace KO.Web.Models.Order
{
    public class OrderDetailViewModel 
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int MenuItem { get; set; }

        public int Quantity { get; set; }

    }
}


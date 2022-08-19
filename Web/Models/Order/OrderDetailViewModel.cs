using System;
using Web.Models;

namespace KO.Web.Models.Medicos
{
    public class OrderDetailViewModel 
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int MenuItem { get; set; }

        public int Quantity { get; set; }

    }
}


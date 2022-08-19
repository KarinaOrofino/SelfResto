using System;

namespace KO.Entities
{
    public class Payment 
    {

        public int InvoiceId { get; set; }

        public int OrderId { get; set; }

        public double TotalAmount { get; set; }

        public double Tip { get; set; }

        public DateTime CreationDate { get; set; }

        public int CreationUser { get; set; }

    }
}

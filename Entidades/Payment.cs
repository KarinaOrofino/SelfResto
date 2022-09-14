using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KO.Entities
{
    public class Payment 
    {

        [Key]
        public int InvoiceId { get; set; }

        [Column(name: "ORDER_ID")]
        public int OrderId { get; set; }

        [Column(name: "TOTAL_AMOUNT")]
        public double TotalAmount { get; set; }

        public double Tip { get; set; }

        public DateTime CreationDate { get; set; }

        public int CreationUser { get; set; }

    }
}

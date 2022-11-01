using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KO.Entities
{
    public class Payment
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(name: "ORDER_ID")]
        public int OrderId { get; set; }

        [Column(name: "TOTAL_AMOUNT")]
        public double TotalAmount { get; set; }

        [Column(name: "CREATION_DATE")]
        public DateTime CreationDate { get; set; }

        [Column(name: "CREATION_USER")]
        public int CreationUser { get; set; }

    }
}

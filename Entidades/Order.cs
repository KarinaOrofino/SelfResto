using System;

namespace KO.Entities
{
    public class Order : BaseEntity
    {
        public int Id { get; set; }

        public int TableId { get; set; }

        public DateTime Date { get; set; }


    }

    public class OrderDetail 
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int MenuItem { get; set; }

        public int Quantity { get; set; }

        public DateTime CreationDate { get; set; }

        public int CreationUser { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? UpdateUser { get; set; }


    }
}

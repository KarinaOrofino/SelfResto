using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KO.Entities
{
    public class Order : BaseEntity
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }

        [Column(name: "TABLE_ID")]
        public byte TableId { get; set; }

    }

    public class OrderDetail : BaseEntity
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }

        [Column(name: "ORDER_ID")]
        public byte OrderId { get; set; }

        [Column(name: "MENU_ITEM_ID")]
        public byte MenuItemId { get; set; }

        public byte Quantity { get; set; }


    }
}

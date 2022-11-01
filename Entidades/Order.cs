using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KO.Entities
{
    public class Order : BaseEntity
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(name: "TABLE_ID")]
        public int TableId { get; set; }

        public bool Call { get; set; }

        [Column(name: "PAYMENT_REQUEST")]
        public bool PaymentRequest { get; set; }

        public virtual IList<OrderDetail> OrderDetails { get; set; }

        public virtual Table Table { get; set; }

    }

    public class OrderDetail : BaseEntity
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(name: "ORDER_ID")]
        public int OrderId { get; set; }

        [Column(name: "MENU_ITEM_ID")]
        public int MenuItemId { get; set; }

        public byte Quantity { get; set; }

        [Column(name: "RELATED_MENU_ITEM_ID")]
        public int? RelatedMenuItemId { get; set; }

        [Column(name: "ORDER_DETAIL_STATUS_ID")]
        public int OrderDetailStatusId { get; set; }

        public virtual MenuItem MenuItem { get; set; }

        public virtual MenuItem RelatedMenuItem { get; set; }

        [ForeignKey("OrderDetailStatusId")]
        public virtual OrderDetailStatus OrderDetailStatus { get; set; }

        public virtual Order Order { get; set; }


    }
}


using System.ComponentModel.DataAnnotations.Schema;

namespace KO.Entities
{
    public class Table : BaseEntity
    {

        public int Id { get; set; }

        public int Number { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [Column(name: "WAITER_ID")]
        public int? WaiterId { get; set; }

        [Column(name: "WAITER_BACK_UP_ID")]
        public int? WaiterBackUpId { get; set; }

        [ForeignKey("WaiterId")]
        public virtual User WaiterUser { get; set; }

        [ForeignKey("WaiterBackUpId")]
        public virtual User WaiterBackUpUser { get; set; }

    }
}

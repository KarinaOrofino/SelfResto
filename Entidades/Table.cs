
using System.ComponentModel.DataAnnotations.Schema;

namespace KO.Entities
{
    public class Table : BaseEntity
    {

        public int Id { get; set; }

        public int Number { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Waiter { get; set; }

        [Column(name: "WAITER_BACK_UP")]
        public int WaiterBackUp { get; set; }

        [ForeignKey("Waiter")]
        public virtual User WaiterUser { get; set; }

        [ForeignKey("WaiterBackUp")]
        public virtual User WaiterBackUpUser { get; set; }

    }
}

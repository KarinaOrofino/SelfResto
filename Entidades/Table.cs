
using System.ComponentModel.DataAnnotations.Schema;

namespace KO.Entities
{
    public class Table : BaseEntity
    {

        public byte Id { get; set; }

        public byte Number { get; set; }

        public byte Waiter { get; set; }

        [Column(name: "WAITER_BACK_UP")]
        public byte WaiterBackUp { get; set; }

    }
}

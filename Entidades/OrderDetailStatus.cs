using System.ComponentModel.DataAnnotations.Schema;

namespace KO.Entities
{
    public class OrderDetailStatus
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

    }
}


using System.ComponentModel.DataAnnotations.Schema;

namespace KO.Entities
{
    public class User : BaseEntity
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        [Column(name: "ACCESS_TYPE")]
        public int Access_Type { get; set; }

        [NotMapped]
        public string AccessTypeName { get; set; }

        [ForeignKey("Access_Type")]
        public virtual AccessType AccessType { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KO.Entities
{
    public class Category : BaseEntity
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        [Column(name: "IMAGEURL")]
        public string CategoryImageUrl { get; set; }

        public virtual IList<MenuItem> MenuItems { get; set; }

    }
}

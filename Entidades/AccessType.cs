
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KO.Entities
{
    public class AccessType : BaseEntity
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

    }
}

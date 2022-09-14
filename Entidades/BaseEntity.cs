using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KO.Entities
{
    public abstract class BaseEntity
    {

        public bool Active { get; set; }

        [Column(name: "CREATION_DATE")]
        public DateTime CreationDate { get; set; }

        [Column(name: "CREATION_USER")]
        public int CreationUser { get; set; }

        [Column(name: "UPDATE_DATE")]
        public DateTime? UpdateDate { get; set; }

        [Column(name: "UPDATE_USER")]
        public int? UpdateUser { get; set; }

    }
}

using System;

namespace KO.Entities
{
    public abstract class BaseEntity
    {

        public bool Active { get; set; }

        public DateTime CreationDate { get; set; }

        public int CreationUser { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? UpdateUser { get; set; }

    }
}

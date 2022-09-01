
using System;
using System.Collections.Generic;

namespace KO.Entities
{
    public class Category: BaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CategoryImageUrl { get; set; }

        public virtual IList<MenuItem> MenuItems { get; set; }

    }
}

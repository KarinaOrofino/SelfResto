using System;
using System.Collections.Generic;

namespace KO.Entities
{
    public class MenuItem 
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public int CategoryId { get; set; }

        public bool Active { get; set; }

        public DateTime CreationDate { get; set; }

        public int CreationUser { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? UpdateUser { get; set; }

    }
}

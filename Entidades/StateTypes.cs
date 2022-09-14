using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KO.Entities
{
    public class StateTypes 
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

    }
}

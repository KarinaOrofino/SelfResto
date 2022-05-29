using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KO.Entidades
{
    [Table(name: "FUNCIONALIDADES")]
    public class Funcionalidad : EntidadBase
    {
        [Column(name: "ID_FUNCIONALIDAD"), DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(name: "DESCRIPCION"), MaxLength(100)]
        public string Descripcion { get; set; }

    }
}

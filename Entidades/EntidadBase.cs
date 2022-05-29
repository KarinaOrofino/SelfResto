using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KO.Entidades
{
    public abstract class EntidadBase
    {
        [Column(name: "ACTIVO")]
        public bool Activo { get; set; }

        [Column(name: "FECHA_ALTA"), Required]
        public DateTime FechaAlta { get; set; }

        [Column(name: "ID_USUARIO_ALTA"), Required]
        public int IdUsuarioAlta { get; set; }

        [Column(name: "FECHA_MODIFICACION")]
        public DateTime? FechaModificacion { get; set; }

        [Column(name: "ID_USUARIO_MODIFICACION")]
        public int? IdUsuarioModificacion { get; set; }

        [ForeignKey("IdUsuarioAlta")]
        public virtual Usuario UsuarioAlta { get; set; }

        [ForeignKey("IdUsuarioModificacion")]
        public virtual Usuario UsuarioModificacion { get; set; }
    }
}

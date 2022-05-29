using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KO.Entidades
{
    [Table(name: "FUNCIONALIDADES_ROL")]
    public class FuncionalidadRol
    {

        [Key, Column(name: "ID_FUNCIONALIDAD", Order = 0)]
        public int IdFuncionalidad { get; set; }

        [Key, Column(name: "ID_ROL", Order = 1)]
        public int IdRol { get; set; }

        [Column(name: "ID_TIPO_ACCESO"), Required]
        public int IdTipoAcceso { get; set; }


        [ForeignKey("IdFuncionalidad")]
        public virtual Funcionalidad Funcionalidad { get; set; }

        [ForeignKey("IdTipoAcceso")]
        public virtual TipoAcceso Tipo_Acceso { get; set; }

    }
}

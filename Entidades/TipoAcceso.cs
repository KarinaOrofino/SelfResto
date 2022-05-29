using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KO.Entidades
{
    [Table(name: "TBL_TIPOS_ACCESO")]
    public class TipoAcceso : EntidadBase
    {

        [Key, Column(name: "ID_TIPO_ACCESO"), DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdTipoAcceso { get; set; }

        [Column(name: "DESCRIPCION"), MaxLength(50), Required]
        public string Descripcion { get; set; }

    }

    public enum TipoAccesoEnum
    {
        AccesoTotal = 1,
        Consulta = 2
    }
}

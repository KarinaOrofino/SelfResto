using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KO.Entidades
{
    [Table(name: "USUARIOS")]
    public class Usuario
    {
        [Column(name: "ID_USUARIO"), DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(name: "LOGIN"), MaxLength(50), Required]
        public string Login { get; set; }

        [Column(name: "NOMBRE"), MaxLength(50), Required]
        public string Nombre { get; set; }

        [Column(name: "DIR_CORREO"), MaxLength(80), Required]
        public string Email { get; set; }

        [Column(name: "ID_USUARIO_SGAA"), Required]
        public int IdUsuarioSGAA { get; set; }

        [Column(name: "ACTIVO"), Required]
        public bool Activo { get; set; }
    }
}

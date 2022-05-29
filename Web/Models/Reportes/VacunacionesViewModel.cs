using System;
using Web.Models;

namespace KO.Web.Models.Reportes
{
    public class VacunacionesViewModel : BaseViewModel
    {
        public VacunacionesViewModel()
        {
            //this.ListaCargas = new List<PacienteViewModel>(); 
        }
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public DateTime Fecha { get; set; }

        public bool Activo { get; set; }
    }
}

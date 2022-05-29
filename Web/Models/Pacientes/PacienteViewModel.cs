using System;
using System.Collections.Generic;
using Web.Models;

namespace KO.Web.Models.Pacientes
{
    public class PacienteViewModel : BaseViewModel
    {
        public PacienteViewModel()
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


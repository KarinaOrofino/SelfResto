using System;
using System.Collections.Generic;
using Web.Models;

namespace KO.Web.Models.Vacunas
{
    public class VacunaViewModel : BaseViewModel
    {
        public VacunaViewModel()
        {
        }
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public DateTime Fecha { get; set; }

        public bool Activo { get; set; }

    }
}


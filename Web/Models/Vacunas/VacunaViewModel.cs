using System;
using System.Collections.Generic;
using Web.Models;

namespace KO.Web.Models.Vacunas
{
    public class VacunaViewModel : BaseViewModel
    {
        public VacunaViewModel()
        {
            this.ListaVacunas = new List<string>();
        }
        public int Id { get; set; }

        public string Nombre { get; set; }

        public bool Estado { get; set; }

        public bool VacunaExistente { get; set; }

        public List<string> ListaVacunas { get; set; }

    }
}


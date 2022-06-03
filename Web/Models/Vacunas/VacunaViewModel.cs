using System;
using System.Collections.Generic;
using Web.Models;

namespace KO.Web.Models.Vacunas
{
    public class VacunaViewModel : BaseViewModel
    {
        public VacunaViewModel()
        {
            this.ListaVacunas = new List<VacunaViewModel>();
        }
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Marca { get; set; }

        public bool Estado { get; set; }

        public bool VacunaExistente { get; set; }

        public List<VacunaViewModel> ListaVacunas { get; set; }

    }
}


using KO.Entidades;
using System;
using System.Collections.Generic;
using Web.Models;

namespace KO.Web.Models.Pacientes
{
    public class PacienteViewModel : BaseViewModel
    {
        public PacienteViewModel()
        {
            this.ListaPacientes = new List<PacienteViewModel>();
            this.ListaObrasSociales = new List<ObraSocial>();
        }
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public int IdObraSocial { get; set; }

        public string ObraSocial { get; set; }

        public long NumeroObraSocial { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string FechaNacimientoString { get; set; }

        public bool PacienteExistente { get; set; }

        public List<PacienteViewModel> ListaPacientes { get; set; }

        public List<ObraSocial> ListaObrasSociales { get; set; }
    }
}


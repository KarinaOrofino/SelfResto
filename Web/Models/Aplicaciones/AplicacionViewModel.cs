using KO.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace KO.Web.Models.Aplicaciones
{
    public class AplicacionViewModel : BaseViewModel
    {
        public AplicacionViewModel()
        {
            this.ListaDetalles = new List<AplicacionDetalle>();
            this.ListaPacientes = new List<Paciente>();
            this.ListaIdsVacunas = new List<int>();
        }

        public int Id { get; set; }

        public int IdDetalle { get; set; }

        public DateTime Fecha { get; set; }

        public string FechaString { get; set; }

        public int IdPaciente { get; set; }

        public string NombrePaciente { get; set; }

        public int IdMedico { get; set; }

        public string NombreMedico { get; set; }

        public int IdVacuna { get; set; }

        public string NombreVacuna { get; set; }

        public List<AplicacionDetalle> ListaDetalles { get; set; }

        public string MarcaVacuna { get; set; }

        public List<Paciente> ListaPacientes { get; set; }

        public List<int> ListaIdsVacunas { get; set; }

    }
}

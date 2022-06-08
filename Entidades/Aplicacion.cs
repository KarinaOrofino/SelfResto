using System;
using System.Collections.Generic;

namespace KO.Entidades
{
    public class Aplicacion 
    {
        public int Id { get; set; }

        public int IdDetalle { get; set; }

        public DateTime Fecha { get; set; }

        public int IdPaciente { get; set; }

        public string NombrePaciente { get; set; }

        public int IdMedico { get; set; }

        public string NombreMedico { get; set; }

        public int IdVacuna { get; set; }

        public string NombreVacuna { get; set; }

        public List<Vacuna> ListaVacunas { get; set; }

        public string MarcaVacuna { get; set; }

        public List<int> ListaIdsVacunas { get; set; }
    }

    public class AplicacionDetalle
    {

        public int IdDetalle { get; set; }

        public int IdAplicacion { get; set; }

        public string Nombre { get; set; }

        public string Marca { get; set; }
      
    }
}

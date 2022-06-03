using System;

namespace KO.Entidades
{
    public class Paciente 
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public int IdObraSocial { get; set; }

        public string ObraSocial { get; set; }

        public long NumeroObraSocial { get; set; }

        public DateTime FechaNacimiento { get; set; }

    }
}

using System;

namespace KO.Entidades
{
    public class Paciente 
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public DateTime Fecha { get; set; }

        public bool Activo { get; set; }
    }
}

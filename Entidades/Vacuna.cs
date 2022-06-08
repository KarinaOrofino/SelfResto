using System;

namespace KO.Entidades
{
    public class Vacuna 
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public bool Estado { get; set; }

        public int IdMarca { get; set; }

        public string Marca { get; set; }

        public int IdAplicacion { get; set; }

    }
}

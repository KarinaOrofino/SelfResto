using KO.Entidades;
using KO.Servicios.Interfaces;
using System.Collections.Generic;

namespace Servicios.Interfaces
{
    public interface IServicioVPacientes : IServicioBase
    {

        public List<Paciente> ObtenerTodos();

        public List<Paciente> ObtenerFiltrados(string campoBusqueda);

        public Paciente Obtener(int id);

        public void Agregar(Paciente paciente);

        public void Actualizar(Paciente paciente);

        public List<ObraSocial> ObtenerObrasSociales();
    }
}


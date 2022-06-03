using KO.Entidades;
using System.Collections.Generic;

namespace KO.Datos.Interfaces
{
    public interface IDatosVPacientes : IDatosBase
    {

        public List<Paciente> ObtenerTodos();

        public List<Paciente> ObtenerFiltrados(string campoBusqueda);

        public Paciente Obtener(int id);
 
        public void Agregar(Paciente paciente);

        public void Actualizar(Paciente paciente);

        public List<ObraSocial> ObtenerObrasSociales();

    }
}

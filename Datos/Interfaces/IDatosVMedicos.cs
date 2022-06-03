using KO.Entidades;
using System.Collections.Generic;

namespace KO.Datos.Interfaces
{
    public interface IDatosVMedicos : IDatosBase
    {

        public List<Medico> ObtenerTodos();

        public List<Medico> ObtenerFiltrados(string campoBusqueda, bool? estado);

        public Medico Obtener(int matricula);

        public void Agregar(Medico medico);

        public void Actualizar(Medico medico);

        public void Inactivar(int matricula);

        public void Activar(int matricula);

    }
}

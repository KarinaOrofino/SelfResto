using KO.Entidades;
using KO.Servicios.Interfaces;
using System.Collections.Generic;

namespace Servicios.Interfaces
{
    public interface IServicioVMedicos : IServicioBase
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


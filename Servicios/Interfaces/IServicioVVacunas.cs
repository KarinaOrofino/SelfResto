using KO.Entidades;
using KO.Servicios.Interfaces;
using System.Collections.Generic;

namespace Servicios.Interfaces
{
    public interface IServicioVVacunas : IServicioBase
    {

        public List<Vacuna> ObtenerTodas();

        public List<Vacuna> ObtenerFiltradas(string campoBusqueda, bool? estado);

        public Vacuna Obtener(int id);

        public void Agregar(Vacuna vacuna);

        public void Actualizar(Vacuna vacuna);

        public void Inactivar(int id);

        public void Activar(int id);


    }
}


using KO.Entidades;
using Servicios.Interfaces;
using System.Collections.Generic;


namespace KO.Servicios.Interfaces
{
    public interface IServicioVAplicaciones : IServicioBase
    {

        public List<Aplicacion> ObtenerTodas();

        public List<Aplicacion> ObtenerFiltradas(string campoBusqueda);

        public Aplicacion Obtener(int id);

        public void Agregar(Aplicacion aplicacion);

        public void Eliminar(int idAplicacion);

        public List<AplicacionDetalle> ObtenerVacunasPorAplicacion(int idAplicacion);

    }
}

using KO.Entidades;
using System.Collections.Generic;

namespace KO.Datos.Interfaces
{
    public interface IDatosVAplicaciones : IDatosBase
    {

        public List<Aplicacion> ObtenerTodas();

        public List<Aplicacion> ObtenerFiltradas(string campoBusqueda);

        public Aplicacion Obtener(int id);

        public int Agregar(Aplicacion aplicacion);

        public void AgregarAplicacionDetalle(int idVacuna, int idAplicacion);

        public void Eliminar(int idAplicacion);

        public List<AplicacionDetalle> ObtenerVacunasPorAplicacion(int idAplicacion);
       
    }
}

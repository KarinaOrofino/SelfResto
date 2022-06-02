using KO.Entidades;
using System.Collections.Generic;

namespace KO.Datos.Interfaces
{
    public interface IDatosVVacunas : IDatosBase
    {

        public List<Vacuna> ObtenerTodas();

        public List<Vacuna> ObtenerFiltradas(string Nombre, bool? Estado);

        public Vacuna Obtener(int id);

        public void Agregar(Vacuna vacuna);

        public void Actualizar(Vacuna vacuna);

        public void Inactivar(int id);

        public void Activar(int id);

    }
}

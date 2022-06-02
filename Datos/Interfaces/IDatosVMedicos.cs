using KO.Entidades;
using System.Collections.Generic;

namespace KO.Datos.Interfaces
{
    public interface IDatosVMedicos : IDatosBase
    {

        public List<Medico> ObtenerTodos();

        public List<Medico> ObtenerFiltrados(int? Matricula, string Nombre, string Apellido, bool? Estado);

        public Medico Obtener(int Matricula);

        public void Agregar(Medico medico);

        public void Actualizar(Medico medico);

        public void Inactivar(int Matricula);

        public void Activar(int Matricula);

    }
}

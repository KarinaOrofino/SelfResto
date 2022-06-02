using KO.Entidades;
using KO.Servicios.Interfaces;
using System.Collections.Generic;

namespace Servicios.Interfaces
{
    public interface IServicioVMedicos : IServicioBase
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


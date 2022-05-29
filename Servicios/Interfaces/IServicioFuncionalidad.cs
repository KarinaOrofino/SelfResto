using KO.Entidades;
using KO.Servicios.Interfaces;
using System.Collections.Generic;

namespace Servicios.Interfaces
{
    public interface IServicioFuncionalidad : IServicioGenerico
    {
        public void AgregarFuncionalidad(Funcionalidad funcionalidad, IList<FuncionalidadRol> funcionalidadesRolesAModificar);

        public void ModificarFuncionalidad(Funcionalidad funcionalidad, IList<FuncionalidadRol> funcionalidadesRolesAModificar);
    }
}
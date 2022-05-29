using KO.Entidades;
using System.Collections.Generic;

namespace KO.Datos.Interfaces
{
    public interface IDatosIntegraciones : IDatosBase
    {
        void ObtenerUsuariosInternos();

        void ObtenerGerencias();

    }
}

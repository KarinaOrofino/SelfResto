using KO.Datos.EFScafolding;
using KO.Datos.Interfaces;
using Microsoft.Extensions.Configuration;

namespace KO.Datos.Implementacion
{
    public class DatosIntegraciones : DatosBase, IDatosIntegraciones
    {
        //private IConfiguration Configuration;


        public DatosIntegraciones(/*IConfiguration configuration, */KOContext context) : base(context)
        {
            //this.Configuration = configuration;

        }

        public void ObtenerGerencias()
        {
            
        }

        public void ObtenerUsuariosInternos()
        {
            
        }

    

    }
}

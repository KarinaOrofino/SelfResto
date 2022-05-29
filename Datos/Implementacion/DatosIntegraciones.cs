using KO.Datos.EFScafolding;
using KO.Datos.Interfaces;
using KO.Entidades;
using Framework.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;

namespace KO.Datos.Implementacion
{
    public class DatosIntegraciones : DatosBase, IDatosIntegraciones
    {
        private IConfiguration Configuration;


        public DatosIntegraciones(IConfiguration configuration, KOContext context) : base(context)
        {
            this.Configuration = configuration;

        }

        public void ObtenerGerencias()
        {
            
        }

        public void ObtenerUsuariosInternos()
        {
            
        }

    

    }
}

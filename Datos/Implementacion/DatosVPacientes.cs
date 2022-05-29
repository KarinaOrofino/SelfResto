using KO.Datos.EFScafolding;
using KO.Datos.Interfaces;
using KO.Entidades;
using Framework.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace KO.Datos.Implementacion
{
    public class DatosVPacientes : DatosBase, IDatosVPacientes
    {
        private IConfiguration Configuration;


        public DatosVPacientes(IConfiguration configuration, KOContext context) : base(context)
        {
            this.Configuration = configuration;
        }

     


    }
}

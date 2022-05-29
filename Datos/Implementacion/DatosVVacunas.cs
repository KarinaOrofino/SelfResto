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
    public class DatosVVacunas : DatosBase, IDatosVVacunas
    {
        private IConfiguration Configuration;


        public DatosVVacunas(IConfiguration configuration, KOContext context) : base(context)
        {
            this.Configuration = configuration;
        }

     


    }
}

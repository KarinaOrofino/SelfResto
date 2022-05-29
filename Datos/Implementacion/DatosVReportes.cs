using KO.Datos.Interfaces;
using KO.Datos.EFScafolding;
using KO.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Framework.Common;
using System;
using Microsoft.Data.SqlClient;

namespace KO.Datos.Implementacion
{
    public class DatosVReportes : DatosBase, IDatosVReportes
    {
        public DatosVReportes(KOContext context) : base(context)
        {
        }

    }
}

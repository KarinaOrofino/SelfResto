using KO.Datos.Implementacion;
using KO.Entidades;
using KO.Servicios.Interfaces;
using log4net;
using Servicios.Implementaciones;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace KO.Servicios.Implementaciones
{
    public class ServicioVReportes : ServicioBase<IDatosVReportes>, IServicioVReportes
    {
        protected readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public ServicioVReportes(IDatosVReportes datos) : base(datos) { }

    
    }
}

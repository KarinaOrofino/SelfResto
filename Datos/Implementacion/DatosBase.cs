using log4net;
using KO.Datos.Interfaces;
using System.Reflection;

namespace KO.Datos.EFScafolding
{
    public abstract class DatosBase : IDatosBase
    {
        protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly KOContext _context;

        public DatosBase(KOContext context)
        {
            this._context = context;
        }       
    }
}

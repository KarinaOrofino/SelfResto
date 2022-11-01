using KO.Data.Interfaces;
using log4net;
using System.Reflection;

namespace KO.Data.EFScafolding
{
    public abstract class BaseData : IBaseData
    {
        protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly KOContext _context;

        public BaseData(KOContext context)
        {
            this._context = context;
        }
    }
}

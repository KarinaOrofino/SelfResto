using KO.Data.Interfaces;
using KO.Entities;
using KO.Services.Interfaces;
using log4net;
using System.Collections.Generic;
using System.Reflection;

namespace KO.Services.Implementations
{
    public class TablesService : BaseService<ITablesData>,ITablesService
    {
        protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public TablesService(ITablesData datos) : base(datos)
        {
          
        }

    }

}


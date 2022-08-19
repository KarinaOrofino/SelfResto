using log4net;
using KO.Data.Interfaces;
using KO.Entities;
using System.Collections.Generic;
using System.Reflection;
using KO.Services.Interfaces;
using KO.Data.Implementations;

namespace KO.Services.Implementations
{
    public class OrdersService : IOrdersService

    {
        protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
              public OrdersService(IOrdersData datos) 
        {
           
        }

    }
}


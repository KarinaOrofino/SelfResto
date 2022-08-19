using KO.Data.Interfaces;
using KO.Entities;
using log4net;
using KO.Services.Interfaces;
using System.Collections.Generic;
using System.Reflection;
using KO.Data.Implementations;

namespace KO.Services.Implementations
{
    public class PaymentsService : BaseService<IPaymentsData>, IPaymentsService
    {
        protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public PaymentsService(IPaymentsData datos) : base(datos)
        {

        }

     }
}


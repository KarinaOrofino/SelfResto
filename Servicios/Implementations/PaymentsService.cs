﻿using KO.Data.Interfaces;
using KO.Entities;
using log4net;
using KO.Services.Interfaces;
using System.Collections.Generic;
using System.Reflection;
using KO.Data.Implementations;
using Framework.Utils;
using System.Transactions;

namespace KO.Services.Implementations
{
    public class PaymentsService : BaseService<IGenericData>, IPaymentsService
    {
        protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private IGenericService IGenericService { get; set; }

        public PaymentsService(IGenericService genericService, IGenericData datos) : base(datos)
        {
            IGenericService = genericService;
        }

        public void RequestBill(Order order, Payment payment) {

            using (TransactionScope scope = new ())
            {

                IGenericService.Update<Order>(order);
                IGenericService.Add<Payment>(payment);

                scope.Complete();

            }
        }
    }
}



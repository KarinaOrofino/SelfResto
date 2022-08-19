using KO.Data.EFScafolding;
using KO.Data.Interfaces;
using KO.Entities;
using Framework.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System;

namespace KO.Data.Implementations
{
    public class PaymentsData : BaseData, IPaymentsData
    {

        public PaymentsData(KOContext context) : base(context)
        {
            
        }

        public void Activate(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(Payment paym)
        {
            throw new NotImplementedException();
        }

        public List<Payment> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Payment> GetAllFiltered(string searchField, bool? active)
        {
            throw new NotImplementedException();
        }

        public Payment GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Inactivate(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Payment paym)
        {
            throw new NotImplementedException();
        }
    }
}

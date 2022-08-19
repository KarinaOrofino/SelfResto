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
    public class TablesData : BaseData, ITablesData
    {

        public TablesData(KOContext context) : base(context)
        {
            
        }

        public void Activate(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(Table table)
        {
            throw new NotImplementedException();
        }

        public List<Table> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Table> GetAllFiltered(string searchField, bool? active)
        {
            throw new NotImplementedException();
        }

        public Table GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Inactivate(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Table table)
        {
            throw new NotImplementedException();
        }
    }
}

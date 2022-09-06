using KO.Data.EFScafolding;
using KO.Data.Interfaces;
using KO.Entities;
using Framework.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System;
using System.Collections.Generic;

namespace KO.Data.Implementations
{
    public class OrdersData : BaseData, IOrdersData
    {
        public OrdersData(KOContext context) : base(context)
        {
            
        }

        public void Activate(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(Order order)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAllFiltered(string searchField, bool? active)
        {
            throw new NotImplementedException();
        }

        public Order GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Inactivate(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Order order)
        {
            throw new NotImplementedException();
        }

        public void AddItem(int itemId, int quantity)
        {
            throw new NotImplementedException();

        }
    }
}

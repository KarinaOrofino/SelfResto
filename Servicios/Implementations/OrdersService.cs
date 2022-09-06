using log4net;
using KO.Data.Interfaces;
using KO.Entities;
using System.Collections.Generic;
using System.Reflection;
using KO.Services.Interfaces;
using KO.Data.Implementations;

namespace KO.Services.Implementations
{
    public class OrdersService : BaseService<IOrdersData>, IOrdersService

    {
        protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
              public OrdersService(IOrdersData datos) : base(datos)
        {
           
        }

        public List<Order> GetAll() {

            return _datos.GetAll();
        }

        public List<Order> GetAllFiltered(string searchField, bool? active)
        {
            return _datos.GetAllFiltered(searchField, active);
        }

        public Order GetById(int id)
        {
            return _datos.GetById(id);
        }

        public void Create(Order order)
        {
            _datos.Create(order);
        }

        public void Update(Order order)
        {
            _datos.Update(order);
        }

        public void Inactivate(int id)
        {
            _datos.Inactivate(id);
        }

        public void Activate(int id)
        {
            _datos.Activate(id);
        }

        public void AddItem(int itemId, int quantity)
        {
            _datos.AddItem(itemId,quantity);

        }

    }
}


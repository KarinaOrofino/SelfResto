using KO.Entities;
using System.Collections.Generic;

namespace KO.Data.Interfaces
{
    public interface IOrdersData : IBaseData
    {
        public List<Order> GetAll();

        public List<Order> GetAllFiltered(string searchField, bool? active);

        public Order GetById(int id);

        public void Create(Order order);

        public void Update(Order order);

        public void Inactivate(int id);

        public void Activate(int id);

        public void AddItem(int itemId, int quantity);


    }
}

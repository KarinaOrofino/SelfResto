using KO.Entities;
using System.Collections.Generic;

namespace KO.Data.Interfaces
{
    public interface IPaymentsData : IBaseData
    {
        public List<Payment> GetAll();

        public List<Payment> GetAllFiltered(string searchField, bool? active);

        public Payment GetById(int id);

        public void Create(Payment paym);

        public void Update(Payment paym);

        public void Inactivate(int id);

        public void Activate(int id);

    }
}

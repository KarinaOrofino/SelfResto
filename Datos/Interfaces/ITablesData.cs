using KO.Entities;
using System.Collections.Generic;

namespace KO.Data.Interfaces
{
    public interface ITablesData : IBaseData
    {
        public List<Table> GetAll();

        public List<Table> GetAllFiltered(string searchField, bool? active);

        public Table GetById(int id);

        public void Create(Table table);

        public void Update(Table table);

        public void Inactivate(int id);

        public void Activate(int id);


    }
}

using KO.Entities;
using System.Collections.Generic;

namespace KO.Data.Interfaces
{
    public interface ITablesData : IBaseData
    {
        public List<Table> GetAllFiltered(string searchField, bool? active);

    }
}

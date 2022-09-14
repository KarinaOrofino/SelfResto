using KO.Entities;
using System.Collections.Generic;


namespace KO.Services.Interfaces
{
    public interface ITablesService
    {
        public List<Table> GetAllFiltered(string searchField, bool? active);
    }
}

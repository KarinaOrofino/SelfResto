using KO.Entities;
using System.Collections.Generic;

namespace KO.Data.Interfaces
{
    public interface IMenuItemsData : IBaseData
    {
        public List<Category> GetAllCategories();

        public List<MenuItem> GetAllFiltered(string searchField, bool? active);

        public List<MenuItem> GetAllFilteredByCatId(int catId, bool? active);

    }
}

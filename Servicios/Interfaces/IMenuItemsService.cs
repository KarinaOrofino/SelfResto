using KO.Entities;
using System.Collections.Generic;

namespace KO.Services.Interfaces
{
    public interface IMenuItemsService
    {
        public List<Category> GetAllCategories();

        public List<MenuItem> GetAllFiltered(string searchField, bool? active);

        public List<MenuItem> GetAllFilteredByCatId(int catId, bool? active);

    }
}


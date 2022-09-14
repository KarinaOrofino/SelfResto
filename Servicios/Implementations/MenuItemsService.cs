using KO.Data.Interfaces;
using KO.Entities;
using KO.Services.Interfaces;
using log4net;
using System.Collections.Generic;
using System.Reflection;
using KO.Services.Implementations;
using KO.Data.Implementations;

namespace KO.Services.Implementations
{
    public class MenuItemsService : BaseService<IMenuItemsData>, IMenuItemsService
    {
        protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public MenuItemsService(IMenuItemsData datos) : base(datos)
        {

        }

        public List<Category> GetAllCategories()
        {
            return _datos.GetAllCategories();
        }

        public List<MenuItem> GetAllFiltered(string searchField, bool? active)
        {
            return _datos.GetAllFiltered(searchField, active);
        }

        public List<MenuItem> GetAllFilteredByCatId(int catId, bool? active)
        {
            return _datos.GetAllFilteredByCatId(catId, active);
        }

    }
}

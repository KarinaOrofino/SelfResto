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

        public List<MenuItem> GetAll()
        {
            return _datos.GetAll();
        }

        public List<MenuItem> GetAllFiltered(string searchField, bool? active)
        {
            return _datos.GetAllFiltered(searchField, active);
        }

        public MenuItem GetById(int id)
        {
            return _datos.GetById(id);
        }

        public void Create(MenuItem menuItem)
        {
            _datos.Create(menuItem);
        }

        public void Update(MenuItem menuItem)
        {
            _datos.Update(menuItem);
        }

        public void Inactivate(int id)
        {
            _datos.Inactivate(id);
        }

        public void Activate(int id)
        {
            _datos.Activate(id);
        }
    }
}

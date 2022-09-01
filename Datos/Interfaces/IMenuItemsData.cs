using KO.Entities;
using System.Collections.Generic;

namespace KO.Data.Interfaces
{
    public interface IMenuItemsData : IBaseData
    {

        public List<Category> GetAllCategories();

        public List<MenuItem> GetAll();

        public List<MenuItem> GetAllFiltered(string searchField, bool? active);

        public MenuItem GetById(int id);

        public void Create(MenuItem menuItem);

        public void Update(MenuItem menuItem);

        public void Inactivate(int id);

        public void Activate(int id);

    }
}

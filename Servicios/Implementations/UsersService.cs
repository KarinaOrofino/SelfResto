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
    public class UsersService : BaseService<IUsersData>, IUsersService
    {
        protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public UsersService(IUsersData datos) : base(datos)
        {

        }
        public List<User> GetAll()
        {
            return _datos.GetAll();
        }

        public List<User> GetAllFiltered(string searchField, bool? active)
        {
            return _datos.GetAllFiltered(searchField, active);
        }

        public User GetById(int id)
        {
            return _datos.GetById(id);
        }

        public User GetByEmail(string email)
        {
            return _datos.GetByEmail(email);
        }

        public void Create(User user)
        {
            _datos.Create(user);
        }

        public void Update(User user)
        {
            _datos.Update(user);
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

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
        public List<User> GetAllFiltered(string searchField, bool? active)
        {
            return _datos.GetAllFiltered(searchField, active);
        }

        public User GetByEmail(string email)
        {
            return _datos.GetByEmail(email);
        }

    }
}

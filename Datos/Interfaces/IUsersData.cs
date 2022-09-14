using KO.Entities;
using System.Collections.Generic;

namespace KO.Data.Interfaces
{
    public interface IUsersData : IBaseData
    {

        public List<User> GetAllFiltered(string searchField, bool? active);

        public User GetByEmail(string email);

    }
}

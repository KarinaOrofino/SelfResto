using KO.Entities;
using System.Collections.Generic;

namespace KO.Services.Interfaces
{
    public interface IUsersService
    {
        public List<User> GetAllFiltered(string searchField, bool? active);

        public User GetByEmail(string email);

    }
}


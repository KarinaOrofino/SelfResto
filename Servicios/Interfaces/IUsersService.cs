using KO.Entities;
using System.Collections.Generic;

namespace KO.Services.Interfaces
{
    public interface IUsersService
    {
        public List<User> GetAll();

        public List<User> GetAllFiltered(string searchField, bool? active);

        public User GetById(int id);

        public User GetByEmail(string email);

        public void Create(User user);

        public void Update(User user);

        public void Inactivate(int id);

        public void Activate(int id);
    }
}


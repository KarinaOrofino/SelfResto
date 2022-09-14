using System.Collections.Generic;
using Web.Models;

namespace KO.Web.Models.Account
{
    public class UserViewModel : BaseViewModel
    {
        public UserViewModel()
        {
            this.UsersList = new List<UserViewModel>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int AccessType { get; set; }

        public string AccessTypeName { get; set; }

        public bool Active { get; set; }

        public List<UserViewModel> UsersList { get; set; }


    }
}

public class AccessTypeViewModel : BaseViewModel
{
    public AccessTypeViewModel()
    {
        this.accessesVM = new List<AccessTypeViewModel>();
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public bool Active { get; set; }

    public List<AccessTypeViewModel> accessesVM { get; set; }


}


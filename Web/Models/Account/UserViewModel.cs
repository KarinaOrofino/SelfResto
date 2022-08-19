using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Framework.Helpers;
using Microsoft.AspNetCore.Authentication;
using KO.Resources;

namespace Web.Models.Account
{
    public class UserViewModel : BaseViewModel
    {
        public UserViewModel()
        {
            this.UsersList = new List<UserViewModel>();
        }
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int AccessType { get; set; }

        public List<UserViewModel> UsersList { get; set; }


    }
}

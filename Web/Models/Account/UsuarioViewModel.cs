using System.Collections.Generic;
using Web.Models;

namespace KO.Web.Models.Account
{
    public class UsuarioViewModel : BaseViewModel
    {

        public UsuarioViewModel()
        {
            this.ListaUsuarios = new List<UsuarioViewModel>();
        }
        public int IdUsuario { get; set; }

        public string NombreUsuario { get; set; }

        public bool Activo { get; set; }

        public List<UsuarioViewModel> ListaUsuarios { get; set; }


    }
}


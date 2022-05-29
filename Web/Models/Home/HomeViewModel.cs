using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace KO.Web.Models.Home
{
    public class HomeViewModel : BaseViewModel
    {
        
        public string Descripcion { get; set; }
        public bool Activo { get; set; }

    }
}

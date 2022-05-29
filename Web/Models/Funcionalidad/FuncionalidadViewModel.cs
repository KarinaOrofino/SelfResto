using System.Collections.Generic;
using Web.Models;

namespace KO.Web.Models.Funcionalidad
{
    public class FuncionalidadViewModel : BaseViewModel
    {
        public FuncionalidadViewModel()
        {
            this.tipoAcceso = new TipoAccesoViewModel();
            this.funcRol = new FuncionalidadRolViewModel();
            this.listaRolesxFuncionalidad = new List<FuncionalidadRolViewModel>();
        }
        public int? id { get; set; }

        public string descripcion { get; set; }

        public TipoAccesoViewModel tipoAcceso { get; set; }

        public FuncionalidadRolViewModel funcRol { get; set; }

        public List<FuncionalidadRolViewModel> listaRolesxFuncionalidad { get; set; }

        public bool activo { get; set; }


    }
}


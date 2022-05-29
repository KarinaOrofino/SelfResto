using Web.Models;

namespace KO.Web.Models.Funcionalidad
{
    public class TipoAccesoViewModel : BaseViewModel
    {

        public int? id { get; set; }

        public string descripcion { get; set; }

        public bool activo { get; set; }


    }
}


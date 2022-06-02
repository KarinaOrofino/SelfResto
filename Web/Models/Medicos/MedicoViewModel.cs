using System.Collections.Generic;
using Web.Models;

namespace KO.Web.Models.Medicos
{
    public class MedicoViewModel : BaseViewModel
    {
        public MedicoViewModel()
        {
            this.ListaMatriculasMedicos = new List<string>(); 
        }
        public int Matricula { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public bool Estado { get; set; }

        public bool MedicoExistente { get; set; }

        public List<string> ListaMatriculasMedicos { get; set; }

    }
}


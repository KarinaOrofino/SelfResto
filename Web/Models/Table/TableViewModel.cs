using Web.Models;

namespace KO.Web.Models.Vacunas
{
    public class TableViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public int Waiter { get; set; }

        public int WaiterBackUp { get; set; }
    }
}


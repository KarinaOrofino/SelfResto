using System.Collections.Generic;
using Web.Models;

namespace KO.Web.Models.Table
{
    public class TableViewModel : BaseViewModel
    {

        public TableViewModel() {

            List<TableViewModel> tables = new();
        
        }
        public int Id { get; set; }

        public int Number { get; set; }

        public int Waiter { get; set; }

        public int WaiterBackUp { get; set; }

        public List<TableViewModel> tables { get; set; }
    }
}


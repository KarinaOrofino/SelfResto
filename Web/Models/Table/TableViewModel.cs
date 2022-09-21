using KO.Entities;
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

        public string Name { get; set; }

        public string Description { get; set; }

        public int? WaiterId { get; set; }

        public string WaiterName { get; set; }

        public int? WaiterBackUpId { get; set; }

        public string WaiterBackUpName { get; set; }

        public int? OrderStatusId { get; set; }

        public bool Active { get; set; }

        public bool Closed { get; set; }

        public List<TableViewModel> tables { get; set; }

        public OrderStatus OrderStatus { get; set; }


    }
}


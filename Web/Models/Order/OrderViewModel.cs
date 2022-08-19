using System;
using Web.Models;

namespace KO.Web.Models.Medicos
{
    public class OrderViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public int TableId { get; set; }

        public DateTime Date { get; set; }
    }
}


using KO.Entities;
using System;
using System.Collections.Generic;
using Web.Models;

namespace KO.Web.Models.Payment
{
    public class PaymentViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public double TotalAmount { get; set; }

   }
}


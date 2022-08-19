using KO.Entities;
using System;
using System.Collections.Generic;
using Web.Models;

namespace KO.Web.Models.Pacientes
{
    public class PaymentViewModel : BaseViewModel
    {
        public int InvoiceId { get; set; }

        public int OrderId { get; set; }

        public double TotalAmount { get; set; }

        public double Tip { get; set; }
    }
}


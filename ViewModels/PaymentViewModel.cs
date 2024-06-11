using System;
using System.Collections.Generic;

namespace semissssloan.ViewModels
{

public partial class PaymentViewModel
{
    public int PaymentId { get; set; }

    public int LoanId { get; set; }

    public int ClientId { get; set; }

    public decimal Collectable { get; set; }

    public DateTime Date { get; set; }
        public decimal CollectableOG { get; internal set; }
    }
}
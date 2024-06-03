using System;
using System.Collections.Generic;

namespace semissssloan.Entities;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int LoanId { get; set; }

    public int ClientId { get; set; }

    public decimal Collectable { get; set; }

    public DateTime Date { get; set; }
}

using System;
using System.Collections.Generic;

namespace semissssloan.Entities;

public partial class Transac
{
    public int Id { get; set; }

    public int? PaymentId { get; set; }

    public int? LoanId { get; set; }

    public decimal? Amount { get; set; }

    public DateTime? Date { get; set; }
}

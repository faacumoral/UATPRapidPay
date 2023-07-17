using System;
using System.Collections.Generic;

namespace UATP.RapidPay.DAL.EfModels;

public partial class Payment
{
    public int PaymentId { get; set; }

    public decimal Amount { get; set; }

    public int CardId { get; set; }

    public decimal Fee { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Card Card { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace UATP.RapidPay.DAL.EfModels;

public partial class Card
{
    public int CardId { get; set; }

    public string CardNumber { get; set; } = null!;

    public int UserId { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual User User { get; set; } = null!;
}

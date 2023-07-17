using System;
using System.Collections.Generic;

namespace UATP.RapidPay.DAL.EfModels;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();
}

namespace UATP.RapidPay.Interfaces.Models;

public class BalanceModel
{
    public IReadOnlyCollection<PaymentModel> Payments { get; set; }
    public CardModel Card { get; set; }
    public decimal TotalBalance => Payments?.Sum(p => p.Amount + p.Fee) ?? 0;
}

namespace UATP.RapidPay.Interfaces.Models
{
    public class PaymentModel
    {
        public int PaymentID { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

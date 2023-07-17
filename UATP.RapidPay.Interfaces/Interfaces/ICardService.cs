using UATP.RapidPay.Interfaces.Models;
using UATP.RapidPay.Interfaces.Requests;

namespace UATP.RapidPay.Interfaces.Interfaces
{
    public interface ICardService
    {
        Task<PaymentModel> AddPayment(int userId, int cardId, CardPaymentRequest request);
        Task<CardModel> CreateCard(int userId, CreateCardRequest request);
        Task<BalanceModel> GetBalance(int userId, int cardId);
    }
}

using Microsoft.EntityFrameworkCore;
using UATP.RapidPay.Core.PaymentFee;
using UATP.RapidPay.DAL.EfModels;
using UATP.RapidPay.Interfaces.Exceptions;
using UATP.RapidPay.Interfaces.Interfaces;
using UATP.RapidPay.Interfaces.Models;
using UATP.RapidPay.Interfaces.Requests;

namespace UATP.RapidPay.Core.Services
{
    public class CardService : ICardService
    {
        private const int CARD_NUMBER_LENGTH = 15;

        private readonly RapidPayContext _context;

        public CardService(RapidPayContext context)
        { 
            _context = context;
        }

        private async Task<Card> GetCardIfExists(int userId, int cardId)
        {
            var card = await _context.Cards.Include(c => c.Payments).FirstOrDefaultAsync(c => c.CardId == cardId && c.UserId == userId)
                ?? throw new DomainException("Card not found");
            return card;
        }

        public async Task<PaymentModel> AddPayment(int userId, int cardId, CardPaymentRequest request)
        {
            var card = await GetCardIfExists(userId, cardId);

            if (request.Amount < 0) throw new DomainException("Payment amount must be greater than $0");

            var payment = new Payment
            {
                Amount = request.Amount,
                CardId = card.CardId,
                CreatedAt = DateTime.UtcNow,
                Fee = (decimal)UFE.Instance.ActualFee
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return new PaymentModel
            {
                Amount = payment.Amount,
                CreatedAt = payment.CreatedAt,
                PaymentID = payment.PaymentId,
                Fee = payment.Fee
            };
        }

        public async Task<CardModel> CreateCard(int userId, CreateCardRequest request)
        {
            if (string.IsNullOrEmpty(request.CardNumber) || request.CardNumber.Length != CARD_NUMBER_LENGTH)
                throw new DomainException("Card number must be 15 digit");

            if (_context.Cards.Any(c => c.CardNumber == request.CardNumber))
                throw new DomainException("Card number already exists");

            var card = new Card
            {
                UserId = userId,
                CardNumber = request.CardNumber
            };

            _context.Add(card);

            await _context.SaveChangesAsync();

            return new CardModel
            {
                CardId = card.CardId,
                CardNumber = card.CardNumber,
                UserId = card.UserId
            };
        }

        public async Task<BalanceModel> GetBalance(int userId, int cardId)
        {
            var card = await GetCardIfExists(userId, cardId);

            var payments = card.Payments.Select(p => new PaymentModel
            {
                Amount = p.Amount,
                CreatedAt = p.CreatedAt,
                PaymentID = p.PaymentId,
                Fee = p.Fee
            }).ToList();

            return new BalanceModel
            {
                Card = new CardModel
                {
                    CardId = card.CardId,
                    CardNumber = card.CardNumber,
                    UserId = card.UserId
                },
                Payments = payments
            };
        }
    }
}

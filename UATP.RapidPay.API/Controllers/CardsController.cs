using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UATP.RapidPay.Interfaces.Interfaces;
using UATP.RapidPay.Interfaces.Models;
using UATP.RapidPay.Interfaces.Requests;

namespace UATP.RapidPay.API.Controllers
{
    public class CardsController : BaseController
    {
        private readonly ICardService _cardService;

        public CardsController(ICardService cardService, ILogger<BaseController> logger) : base(logger)
        {
            _cardService = cardService;
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CardModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> CreateCard([FromBody]CreateCardRequest request)
        {
            var result = await ProcessRequest(() => _cardService.CreateCard(UserId, request));

            return result;
        }

        [HttpPost("{cardId:int}/pay")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> Pay([FromRoute] int cardId, [FromBody] CardPaymentRequest request)
        {
            var result = await ProcessRequest(() => _cardService.AddPayment(UserId, cardId, request) );
            return result;
        }

        [HttpGet("{cardId:int}/balance")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BalanceModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> GetBalance([FromRoute] int cardId)
        {
            var result = await ProcessRequest(() => _cardService.GetBalance(UserId, cardId));
            return result;
        }

    }
}

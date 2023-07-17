using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UATP.RapidPay.Interfaces.Exceptions;

namespace UATP.RapidPay.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BaseController : ControllerBase
    {
        protected readonly ILogger<BaseController> _logger;
        public BaseController(ILogger<BaseController> logger)
        { 
            _logger = logger;
        }

        protected int UserId { get => int.Parse(User.Claims.First(c => c.Type == "UserID").Value); }


        protected async Task<IActionResult> ProcessRequest<T>(Func<Task<T>> handler)
        {
            try
            {
                var result = await handler();
                return Ok(result);
            }
            catch (DomainException ex)
            {
                // this exception handling could be migrated to a ExceptionMiddleware/ActionFilter
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocurred");
                return StatusCode(500, "An error ocurred");
            }
        }
    }
}

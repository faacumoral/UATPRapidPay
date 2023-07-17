using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UATP.RapidPay.Interfaces.Exceptions;
using UATP.RapidPay.Interfaces.Interfaces;
using UATP.RapidPay.Interfaces.Requests;

namespace UATP.RapidPay.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(
            ILogger<BaseController> logger,
            IUserService userService) :base(logger)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Tuple<string, DateTime>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var (token, expDate) = await _userService.Login(request);
                return Ok(new { token , expDate });
            }
            catch (LoginException ex)
            { 
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, "An error ocurred");
            }
           
        }
    
    }

}
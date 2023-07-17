using Microsoft.EntityFrameworkCore;
using UATP.RapidPay.DAL.EfModels;
using UATP.RapidPay.Interfaces.Exceptions;
using UATP.RapidPay.Interfaces.Interfaces;
using UATP.RapidPay.Interfaces.Requests;
using UATP.RapidPay.Interfaces.Settings;
using UATP.RapidPay.Shared;
using UATP.RapidPay.Shared.Jwt;

namespace UATP.RapidPay.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IJwtManager _jwtManager;
        private readonly RapidPayContext _context;


        public UserService(
            IJwtManager jwtManager,
            RapidPayContext context) 
        {
            _jwtManager = jwtManager;
            _context = context;
        }


        public async Task<(string, DateTime)> Login(LoginRequest request)
        {
            var username = request.Username.ToLower();
            var encryptPassword = Encrypter.Encrypt(request.Password, ApplicationSettings.Config.EncryptKey).ToLower();

            var user = await _context.Users
                .Where(u => u.Username.ToLower() == username)
                .Where(u => u.Password.ToLower() == encryptPassword)
                .FirstOrDefaultAsync() ?? throw new LoginException("Username/password does not exist");

            var (jwtToken, expDate) = _jwtManager.GenerateToken(user.UserId);

            return (jwtToken, expDate);
        }

    }
}

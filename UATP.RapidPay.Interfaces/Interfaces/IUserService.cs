using UATP.RapidPay.Interfaces.Requests;

namespace UATP.RapidPay.Interfaces.Interfaces
{
    public interface IUserService
    {
        public Task<(string, DateTime)> Login(LoginRequest request);
    }
}

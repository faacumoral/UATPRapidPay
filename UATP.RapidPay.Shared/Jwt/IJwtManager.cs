namespace UATP.RapidPay.Shared.Jwt;

public interface IJwtManager
{
    public (string, DateTime) GenerateToken(int userId);
}
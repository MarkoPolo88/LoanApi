namespace LoanApi.Services.Jwt
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(string username, string role, int userId);
    }
}

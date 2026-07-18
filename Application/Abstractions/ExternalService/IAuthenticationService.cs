using Contrats.Request;


namespace Application.Abstractions.ExternalService
{
    public interface IAuthenticationService
    {
        string? Login(LoginRequest request);
    }
}

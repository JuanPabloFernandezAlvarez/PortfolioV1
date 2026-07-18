using Contrats.Request;
using Domain.Entities;

namespace Application.Abstractions.ExternalService
{
    public interface IUserRepository
    {
        User? GetByUserAndPassword(LoginRequest request);
    }
}
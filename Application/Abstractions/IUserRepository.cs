using Domain.Entities;

namespace Application.Abstractions.ExternalService
{
    public interface IUserRepository
    {
        User? GetByUsername(string username);
    }
}
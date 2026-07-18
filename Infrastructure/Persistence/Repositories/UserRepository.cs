

using Application.Abstractions.ExternalService;
using Contrats.Request;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PortfolioDbContext _context;

        public UserRepository(PortfolioDbContext context)
        {
            _context = context;
        }
        public User? GetByUserAndPassword(LoginRequest request)
        {
            return _context.Users.FirstOrDefault(x => x.Username == request.Username && x.Password == request.Password);
        }
    }
}

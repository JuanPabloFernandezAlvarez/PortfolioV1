using Application.Abstractions.ExternalService;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly PortfolioDbContext _context;

    public UserRepository(PortfolioDbContext context)
    {
        _context = context;
    }

    public User? GetByUsername(string username)
    {
        return _context.Users
            .FirstOrDefault(x => x.Username == username);
    }
}
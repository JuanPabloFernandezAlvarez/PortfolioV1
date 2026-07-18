using Application.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistence.Repositories
{
    public class ExperienceRepository : IExperienceRepository
    {
        private readonly PortfolioDbContext _context;
        public ExperienceRepository(PortfolioDbContext context)
        {
            _context = context;
        }
        public List<Experience> GetAll()
        {
            return _context.Experiences.ToList();
        }
        public Experience? GetById(int id)
        {
            return _context.Experiences.FirstOrDefault(e => e.Id == id);
        }
        public int Create(Experience exp)
        {
            try
            {
                _context.Experiences.Add(exp);
                _context.SaveChanges();
                return exp.Id; 
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Error al insertar entidad: {ex.InnerException?.Message ?? ex.Message}");
                return 0;
            }
        }
        public bool Delete(Experience exp)
        {
            _context.Experiences.Remove(exp);
            _context.SaveChanges();

            return true;
        }
        public bool Update(Experience exp)
        {
            _context.Experiences.Update(exp);
            _context.SaveChanges();

            return true;
        }
    }
}

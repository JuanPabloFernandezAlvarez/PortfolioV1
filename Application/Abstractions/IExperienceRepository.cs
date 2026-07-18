using Domain.Entities;


namespace Application.Abstractions
{
    public interface IExperienceRepository
    {
        List<Experience> GetAll();
        Experience? GetById(int id);
        int Create(Experience exp);
        bool Update(Experience exp);
        bool Delete(Experience exp);
    }
}

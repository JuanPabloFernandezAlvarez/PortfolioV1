using Contrats.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IExperienceService 
    {
        List<ExperienceResponse> GetAll();
        ExperienceResponse? GetById(int id);
        int Create(ExperienceForCreationAndUpdateRequest experience);
        bool Update(int id, ExperienceForCreationAndUpdateRequest request);
        bool Delete(int id);

    }
}

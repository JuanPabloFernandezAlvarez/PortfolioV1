using Contrats.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    internal class ExperienceService : IExperienceService
    {
        public int Create(ExperienceForCreationAndUpdateRequest experience)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Experience> GetAll()
        {
            throw new NotImplementedException();
        }

        public ExperienceResponse? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(int id, ExperienceForCreationAndUpdateRequest request)
        {
            throw new NotImplementedException();
        }

        List<ExperienceResponse> IExperienceService.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}

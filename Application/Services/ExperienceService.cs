using Application.Abstractions;
using Contrats.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ExperienceService : IExperienceService
    {
        private readonly IExperienceRepository _experienceRepository;
        public ExperienceService(IExperienceRepository experienceRepository)
        {
            _experienceRepository = experienceRepository;
        }

        public List<ExperienceResponse> GetAll()
        {
            var experiencesList = _experienceRepository.GetAll().Select(e => new ExperienceResponse
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                Summary = e.Summary,
                ImagePath = e.ImagePath
            }).ToList();
            return experiencesList;
        }
        public ExperienceResponse? GetById(int id)
        {
            var experience = _experienceRepository.GetById(id) is Experience exp ?
    new ExperienceResponse()
    {
        Id = exp.Id,
        Title = exp.Title,
        Description = exp.Description,
        Summary = exp.Summary,
        ImagePath = exp.ImagePath
    } : null;

            return experience;
        }
        public int Create(ExperienceForCreationAndUpdateRequest experience)
        {
            if (experience == null || string.IsNullOrWhiteSpace(experience.Title))
                return 0;

            var newExperience = new Experience
            {
                Title = experience.Title.Trim(),
                Description = experience.Description,
                ImagePath = experience.ImagePath,
                Summary = experience.Summary
            };

            return _experienceRepository.Create(newExperience);
        }
        public bool Update(int id, ExperienceForCreationAndUpdateRequest request)
        {
            var experienceExisting = _experienceRepository.GetById(id);
            if (experienceExisting == null)
            {
                return false;
            }

            var titleTrim = request.Title.Trim();
            experienceExisting.Title = titleTrim;
            experienceExisting.Description = request.Description;
            experienceExisting.ImagePath = request.ImagePath;
            experienceExisting.Summary = request.Summary;
            return _experienceRepository.Update(experienceExisting);
        }

        public bool Delete(int id)
        {
            var experience = _experienceRepository.GetById(id);
            if (experience == null)
            {
                return false;
            }

            return _experienceRepository.Delete(experience);
        }
    }
}

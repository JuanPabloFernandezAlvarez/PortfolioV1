using Application.Services;
using Contrats.Response;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class ExperienceController : ControllerBase

    {
        private readonly IExperienceService _experienceService;
        public ExperienceController(IExperienceService experienceService)
        {
            _experienceService = experienceService;
        }

        [HttpGet]
        public ActionResult<List<ExperienceResponse>> GetAll()
        {
            var experiencesList = _experienceService.GetAll();

            if (experiencesList == null || !experiencesList.Any())
            {
                return NotFound("No se encontraron experiencias");
            }
            return Ok(experiencesList);
        }
        [HttpGet("{id}")]
        public ActionResult<Experience> GetById([FromRoute] int id)
        {
            var experience = _experienceService.GetById(id);
            if (experience == null)
            {
                return NotFound("Experiencia no encontrada");
            }
            return Ok(experience);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create([FromBody] ExperienceForCreationAndUpdateRequest experience)
        {
            if (experience == null)
            {
                return BadRequest("La experiencia no puede ser nula");
            }

            if (string.IsNullOrWhiteSpace(experience.Title))
            {
                return BadRequest("El título es requerido");
            }
            if (experience.Description   == null)
            {
                return BadRequest("El campo description es requerido");
            }
            if (experience.Summary == null)
            {
                return BadRequest("El campo summary es requerido");
            }
            if (experience.Title.Length < 3 || experience.Title.Length > 25)
            {
                return BadRequest("El título debe tener entre 3 y 25 caracteres");
            }

            if (experience.Description.Length < 1 || experience.Description.Length > 250)
            {
                return BadRequest("La descripción debe tener entre 1 y 250 caracteres");
            }
            try
            {
                var createdId = _experienceService.Create(experience);
                if (createdId <= 0)
                {
                    return Conflict("No se pudo crear la experiencia");
                }
                return CreatedAtAction(nameof(GetById), new { id = createdId }, new { Message = "Experiencia creada correctamente", Id = createdId });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { Message = ex.Message });
            }
            catch
            {
                return StatusCode(500, "Error interno al crear la experiencia");
            }
        }
        [Authorize]
        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] ExperienceForCreationAndUpdateRequest request)
        {
            if (request == null)
            {
                return BadRequest("La solicitud no puede ser nula");
            }

            if (string.IsNullOrWhiteSpace(request.Title))
            {
                return BadRequest("El título es requerido");
            }

            if (request.Title.Length < 3 || request.Title.Length > 25)
            {
                return BadRequest("El título debe tener entre 3 y 25 caracteres");
            }

            if (request.Description == null)
            {
                return BadRequest("La descripción es requerida");
            }
            if (request.Summary == null)
            {
                return BadRequest("El campo summary es requerido");
            }

            if (request.Description.Length < 1 || request.Description.Length > 250)
            {
                return BadRequest("La descripción debe tener entre 1 y 250 caracteres");
            }
            var existing = _experienceService.GetById(id);
            if (existing == null)
            {
                return NotFound($"Id: {id} no encontrado");
            }

            try
            {
                var isUpdated = _experienceService.Update(id, request);
                if (!isUpdated)
                {
                    return Conflict("Error al actualizar la experiencia");
                }
                return Ok(new { Message = "Experiencia actualizada correctamente" });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { Message = ex.Message });
            }
            catch
            {
                return StatusCode(500, "Error interno al actualizar la experiencia");
            }
        }
        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            try
            {
                var isDeleted = _experienceService.Delete(id);
                if (!isDeleted)
                {
                    return NotFound("Experiencia no encontrada o no se pudo eliminar");
                }
                return Ok(new { Message = $"Experiencia ID:{id} eliminada" });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { Message = ex.Message });
            }
            catch
            {
                return StatusCode(500, "Error interno al eliminar la experiencia");
            }
        }

    }
}

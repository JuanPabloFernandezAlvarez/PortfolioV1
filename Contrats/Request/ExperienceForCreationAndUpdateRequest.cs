using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contrats.Response
{
    public class ExperienceForCreationAndUpdateRequest
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El titulo de la experiencia es requerida") ]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El título debe tener entre 3 y 100 caracteres.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "La descripción de la experiencia es requerida")]
        [MaxLength(250, ErrorMessage = "La descripción no puede exceder los 250 caracteres.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "El resumen de la experiencia es requerido")]
        public string Summary { get; set; }
        [Required(ErrorMessage = "La ruta de la imagen de la experiencia es requerida")]
        public string ImagePath { get; set; }
    }
}


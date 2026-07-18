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
        public string Title { get; set; }
        [Required(ErrorMessage = "La descripción de la experiencia es requerida")]
        [MaxLength(100)]
        public string Description { get; set; }
        [Required(ErrorMessage = "El resumen de la experiencia es requerido")]
        public string Summary { get; set; }
        [Required(ErrorMessage = "La ruta de la imagen de la experiencia es requerida")]
        public string ImagePath { get; set; }
    }
}


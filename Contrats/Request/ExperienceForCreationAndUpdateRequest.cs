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
        [Required]
        public string Title { get; set; }
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        [Required]
        public string ImagePath { get; set; }
    }
}


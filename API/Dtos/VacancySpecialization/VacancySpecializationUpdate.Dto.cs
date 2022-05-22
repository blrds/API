using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class VacancySpecializationUpdateDto
    {
        [Required]
        public int IdVacancy { get; set; }

        [Required]
        public int IdSpecialization { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class VacancySkill
    {
        [Required]
        public int IdVacancy { get; set; }

        [Required]
        public int IdSkill { get; set; }


    }
}

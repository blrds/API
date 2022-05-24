using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class VacancySpecialization
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int IdVacancy { get; set; }

        [Required]
        public string IdSpecialization { get; set; }


    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Vacancy
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public int IdArea { get; set; }
        public decimal SalaryFrom { get; set; }
        public decimal SalaryTo { get; set; }

        [MaxLength(10)]
        public string SalaryCurency { get; set; }

        [Required]
        public DateTime PublishedDate { get; set; }

        [MaxLength(100)]
        public string SnippetRequirement { get; set; }

        [MaxLength(100)]
        public string SnippetResponsibility { get; set; }

        public string Description { get; set; }

        [MaxLength(50)]
        public string IdExperience { get; set; }
    }
}

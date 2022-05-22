using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Area
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }


    }
}

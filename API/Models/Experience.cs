using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Experience
    {
        [Key]
        [MaxLength(50)]
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }


    }
}

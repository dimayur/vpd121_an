using System.ComponentModel.DataAnnotations;

namespace WebApplication12.Models
{
    public class Tovars
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}

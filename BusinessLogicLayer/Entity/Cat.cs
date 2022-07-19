using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Entity
{
    public class Cat
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Summary { get; set; }
    }
}

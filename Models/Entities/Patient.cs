using System.ComponentModel.DataAnnotations;

namespace HMS.API.Models.Entities
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int Age { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string Phone { get; set; } = string.Empty;

        // Relationship
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}

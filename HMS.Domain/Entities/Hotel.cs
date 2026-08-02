using System.ComponentModel.DataAnnotations;

namespace HMS.Domain.Entities
{
    public class Hotel
    {
        [Key]
        public int HotelId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }
        [Required]
        [MaxLength(50)]
        public string Country { get; set; }
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        [Required]
        [MaxLength(100)]
        public string Address { get; set; }

        public ICollection<Manager> Managers { get; set; }
        public ICollection<Room> Rooms { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Application.Models.Room
{
    public class RoomForCreatingDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Range(100, 100000, ErrorMessage = "Room number must be between 100 and 100000.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int HotelId { get; set; }
    }
}

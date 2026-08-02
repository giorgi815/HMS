using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Domain.Entities
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Range(100, 100000, ErrorMessage = "Room number must be between 100 and 100000.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [ForeignKey(nameof(Hotel))]
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }

        public ICollection<ReservationRoom> ReservationRooms { get; set; } = new List<ReservationRoom>();
    }
}

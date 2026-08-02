using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Domain.Entities
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        [ForeignKey(nameof(Guest))]
        public int GuestId { get; set; }
        public Guest Guest { get; set; } = null!;

        public ICollection<ReservationRoom> ReservationRooms { get; set; } = new List<ReservationRoom>();
    }
}

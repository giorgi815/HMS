using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Domain.Entities
{
    public class ReservationRoom
    {
        [ForeignKey(nameof(Reservation))]
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; } = null!;

        [ForeignKey(nameof(Room))]
        public int RoomId { get; set; }
        public Room Room { get; set; } = null!;
    }
}

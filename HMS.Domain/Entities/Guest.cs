using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Domain.Entities
{
    public class Guest
    {
        [Key]
        public int GuestId { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(11)]
        public string PersonalNumber { get; set; }
        [Required]
        [MaxLength(9)]
        public string PhoneNumber { get; set; }
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        public string ApplicationId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}

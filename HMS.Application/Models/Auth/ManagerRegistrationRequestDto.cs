using System.ComponentModel.DataAnnotations;

namespace HMS.Application.Models.Auth
{
    public class ManagerRegistrationRequestDto
    {
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
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        [MaxLength(9)]
        public string PhoneNumber { get; set; }

        public int HotelId { get; set; }
    }
}

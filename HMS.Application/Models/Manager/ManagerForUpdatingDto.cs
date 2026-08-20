
using HMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace HMS.Application.Models.Manager
{
    public class ManagerForUpdatingDto
    {
        [Required]
        public int Id { get; set; }
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

    }
}

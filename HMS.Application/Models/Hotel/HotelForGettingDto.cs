
using System.ComponentModel.DataAnnotations;

namespace HMS.Application.Models.Hotel
{
    public class HotelForGettingDto
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

    }
}

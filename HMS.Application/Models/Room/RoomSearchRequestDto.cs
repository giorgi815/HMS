using HMS.Application.Models.Common;

namespace HMS.Application.Models.Room
{
    public class RoomSearchRequestDto : PagedRequestDto
    {
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
    }
}

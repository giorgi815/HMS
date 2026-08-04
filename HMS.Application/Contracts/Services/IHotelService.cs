
using HMS.Application.Models.Common;
using HMS.Application.Models.Hotel;

namespace HMS.Application.Contracts.Services
{
    public interface IHotelService
    {
        Task<PagedResponseDto<HotelForGettingDto>> GetAllHotelsAsync(PagedRequestDto parameters);
        Task<HotelForGettingDto> GetHotelByIdAsync(int hotelId);
        Task<int> CreateHotelsAsync(HotelForCreatingDto model);
        Task<HotelForGettingDto> UpdateHotelAsync(HotelForUpdatingDto model);
        Task DeleteHotelAsync(int hotelId);
    }
}

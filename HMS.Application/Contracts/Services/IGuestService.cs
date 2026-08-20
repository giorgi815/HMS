using HMS.Application.Models.Guest;

namespace HMS.Application.Contracts.Services
{
    public interface IGuestService
    {
        Task<GuestForGettingDto> CreateGuestAsync(GuestForCreatingDto model);
        Task<GuestForGettingDto> UpdateGuestAsync(GuestForUpdatingDto model);
        Task<int> DeleteGuestAsync(int id);
    }
}

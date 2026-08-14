using HMS.Application.Models.Common;
using HMS.Application.Models.Room;

namespace HMS.Application.Contracts.Services
{
    public interface IRoomService
    {
        Task<PagedResponseDto<RoomForGettingDto>> GetAllRoomsAsync(PagedRequestDto parameters);
        Task<RoomForGettingDto> GetRoomByIdAsync(int roomId);
        Task<PagedResponseDto<RoomForGettingDto>> SearchAvailableRoomsAsync(RoomSearchRequestDto parameters);
        Task<int> CreateRoomAsync(RoomForCreatingDto model);
        Task<RoomForGettingDto> UpdateRoomAsync(RoomForUpdatingDto model);
        Task DeleteRoomAsync(int roomId);
    }
}

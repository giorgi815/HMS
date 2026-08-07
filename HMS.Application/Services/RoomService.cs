using HMS.Application.Contracts.Presistence;
using HMS.Application.Contracts.Services;
using HMS.Application.Exceptions;
using HMS.Application.Models.Common;
using HMS.Application.Models.Room;
using HMS.Domain.Entities;
using MapsterMapper;
using System.Linq.Expressions;

namespace HMS.Application.Services
{
    public class RoomService(IRoomRepository roomRepository, IMapper mapper) : IRoomService
    {
        public async Task<PagedResponseDto<RoomForGettingDto>> GetAllRoomsAsync(PagedRequestDto parameters)
        {
            var rooms = await roomRepository.GetAllAsync(
                orderBy: BuildOrderBy(parameters.SortBy),
                ascending: parameters.Ascending,
                pageNumber: parameters.PageNumber,
                pageSize: parameters.PageSize
                );

            return MapToPagedResponseDto(rooms, parameters);
        }

        

        public async Task<RoomForGettingDto> GetRoomByIdAsync(int roomId)
        {
            if (roomId == null)
                throw new BadRequestException("Room Id is required");

            var room = roomRepository.GetAsync(r => r.RoomId == roomId);

            if (room is null)
                throw new NotFoundException($"Room with Id {roomId} not found");

            return mapper.Map<RoomForGettingDto>(room);

        }
        public async Task<int> CreateRoomAsync(RoomForCreatingDto model)
        {
            if (model is null)
                throw new BadRequestException("Room model is required");

            //if room is preserved same room cant be preserved again
            var existingRoom = await roomRepository.GetAsync(r => r.Name == model.Name);

            if (existingRoom is not null)
                throw new BadRequestException($"Room with name {model.Name} already exists");

            var room = mapper.Map<Room>(model);
            await roomRepository.AddAsync(room);
            await roomRepository.SaveAsync();
            return room.RoomId;

        }
        public async Task<RoomForGettingDto> UpdateRoomAsync(RoomForUpdatingDto model)
        {
            if (model is null)
                throw new BadRequestException("Room model is required");

            var room = await roomRepository.GetAsync(r => r.RoomId == model.RoomId);

            if (room is null)
                throw new NotFoundException($"Room with Id {model.RoomId} not found");

            mapper.Map(model, room);
            roomRepository.Update(room);
            await roomRepository.SaveAsync();

            return mapper.Map<RoomForGettingDto>(room);

        }

        public async Task DeleteRoomAsync(int roomId)
        {
            if (roomId == null)
                throw new BadRequestException("Room Id is required");

            var room = await roomRepository.GetAsync(r => r.RoomId == roomId);

            if (room is null)
                throw new NotFoundException($"Room with Id {roomId} not found");

            var today = DateTime.UtcNow.Date;

            bool hasActiveOrFutureReservations = room.ReservationRooms
            .Any(rr => rr.Reservation.CheckOutDate >= today);

            if(hasActiveOrFutureReservations)
                throw new BadRequestException($"Room with Id {roomId} has active or future reservations and cannot be deleted");

            roomRepository.Remove(room);
            await roomRepository.SaveAsync();

        }

        #region
        private static Expression<Func<Room, object>> BuildOrderBy(string sortBy)
        {
            return sortBy?.ToLower() switch
            {
                "name" => c => c.Name,
                "price" => c => c.Price,
                _ => c => c.RoomId
            };
        }

        private PagedResponseDto<RoomForGettingDto> MapToPagedResponseDto((IEnumerable<Room> Items, int TotalCount) room, PagedRequestDto paramaters)
        {
            return new PagedResponseDto<RoomForGettingDto>
            {
                Items = room.Items.Any()
                ? mapper.Map<IEnumerable<RoomForGettingDto>>(room.Items)
                : Enumerable.Empty<RoomForGettingDto>(),
                TotalCount = room.TotalCount,
                PageNumber = paramaters.PageNumber,
                PageSize = paramaters.PageSize
            };
        #endregion


        }
    }
}

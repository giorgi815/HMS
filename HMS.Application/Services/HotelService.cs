using HMS.Application.Contracts.Presistence;
using HMS.Application.Contracts.Services;
using HMS.Application.Exceptions;
using HMS.Application.Models.Common;
using HMS.Application.Models.Hotel;
using HMS.Domain.Entities;
using MapsterMapper;
using System.Linq.Expressions;

namespace HMS.Application.Services
{
    public class HotelService(IHotelRepository hotelRepository, IMapper mapper) : IHotelService
    {
        public async Task<PagedResponseDto<HotelForGettingDto>> GetAllHotelsAsync(PagedRequestDto parameters)
        {
            var hotels = await hotelRepository.GetAllAsync(
                orderBy: BuildOrderBy(parameters.SortBy),
                ascending: parameters.Ascending,
                pageNumber: parameters.PageNumber,
                pageSize: parameters.PageSize
                );

            return MapToPagedResponseDto(hotels, parameters);
        }

        public async Task<HotelForGettingDto> GetHotelByIdAsync(int hotelId)
        {
            if(hotelId == null)
                throw new BadRequestException("Hotel Id cannot be null.");

            var hotel = await hotelRepository.GetAsync(h => h.HotelId == hotelId);

            if(hotel is null)
                throw new NotFoundException($"Hotel with Id {hotelId} not found.");

            return mapper.Map<HotelForGettingDto>(hotel);

        }

        public async Task<int> CreateHotelsAsync(HotelForCreatingDto model)
        {
            if(model is null)
                throw new BadRequestException("Hotel model cannot be null.");

            var hotel = mapper.Map<Hotel>(model);
            await hotelRepository.AddAsync(hotel);
            await hotelRepository.SaveAsync();
            return hotel.HotelId;
        }

        public async Task<HotelForGettingDto> UpdateHotelAsync(HotelForUpdatingDto model)
        {
            if(model is null)
                throw new BadRequestException("Hotel model cannot be null.");

            var hotel = await hotelRepository.GetAsync(fillter: h => h.HotelId == model.HotelId);

            if(hotel is null)
                throw new NotFoundException($"Hotel with Id {model.HotelId} not found.");

            mapper.Map(model, hotel);
            hotelRepository.Update(hotel);
            await hotelRepository.SaveAsync();
            return mapper.Map<HotelForGettingDto>(hotel);
        }

        public async Task DeleteHotelAsync(int hotelId)
        {
            if(hotelId == null)
                throw new BadRequestException("Hotel Id cannot be null.");

            var hotel = await hotelRepository.GetAsync(h => h.HotelId == hotelId);

            if(hotel is null)
                throw new NotFoundException($"Hotel with Id {hotelId} not found.");

            hotelRepository.Remove(hotel);
            await hotelRepository.SaveAsync();

        }


        #region
        private static Expression<Func<Hotel, object>> BuildOrderBy(string sortBy)
        {
            return sortBy?.ToLower() switch
            {
                "name" => c => c.Name,
                "rating" => c => c.Rating,
                "country" => c => c.Country,
                "city" => c => c.City,
                "address" => c => c.Address,
                _ => c => c.HotelId
            };
        }

        private PagedResponseDto<HotelForGettingDto> MapToPagedResponseDto((IEnumerable<Hotel> Items, int TotalCount) hotel, PagedRequestDto paramaters)
        {
            return new PagedResponseDto<HotelForGettingDto>
            {
                Items = hotel.Items.Any()
                ? mapper.Map<IEnumerable<HotelForGettingDto>>(hotel.Items)
                : Enumerable.Empty<HotelForGettingDto>(),
                TotalCount = hotel.TotalCount,
                PageNumber = paramaters.PageNumber,
                PageSize = paramaters.PageSize
            };
        #endregion

        }
    }
}

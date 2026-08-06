using HMS.Application.Models.Hotel;
using HMS.Application.Models.Room;
using HMS.Domain.Entities;
using Mapster;

namespace HMS.Application.Mapping
{
    public class MappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Hotel, HotelForGettingDto>()
                .Map(dest => dest.HotelId, src => src.HotelId)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Rating, src => src.Rating)
                .Map(dest => dest.Country, src => src.Country)
                .Map(dest => dest.City, src => src.City)
                .Map(dest => dest.Address, src => src.Address);
            config.NewConfig<HotelForCreatingDto, Hotel>();
            config.NewConfig<HotelForUpdatingDto, Hotel>();


            config.NewConfig<Room, RoomForGettingDto>()
                .Map(dest => dest.RoomId, src => src.RoomId)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Price, src => src.Price);
            config.NewConfig<RoomForCreatingDto, Room>();
            config.NewConfig<RoomForUpdatingDto, Room>();
        }
    }
}

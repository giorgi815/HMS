using HMS.Application.Contracts.Presistence;
using HMS.Domain.Entities;
using HMS.Infrastructure.Data;

namespace HMS.Infrastructure.Presistence
{
    public class HotelRepository : RepositoryBase<Hotel, ApplicationDbContext>, IHotelRepository
    {
        public HotelRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

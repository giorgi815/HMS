using HMS.Application.Contracts.Presistence;
using HMS.Domain.Entities;
using HMS.Infrastructure.Data;

namespace HMS.Infrastructure.Presistence
{
    public class ReservationRoomRepository : RepositoryBase<ReservationRoom, ApplicationDbContext>, IReservationRoomRepository
    {
        public ReservationRoomRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

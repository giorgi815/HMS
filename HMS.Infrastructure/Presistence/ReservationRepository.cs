using HMS.Application.Contracts.Presistence;
using HMS.Domain.Entities;
using HMS.Infrastructure.Data;

namespace HMS.Infrastructure.Presistence
{
    public class ReservationRepository : RepositoryBase<Reservation, ApplicationDbContext>, IReservationRepository
    {
        public ReservationRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HMS.Application.Contracts.Presistence
{
    public interface IReservationRepository : IRepositoryBase<Reservation, DbContext>
    {
    }
}

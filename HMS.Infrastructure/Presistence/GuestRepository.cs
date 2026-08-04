using HMS.Application.Contracts.Presistence;
using HMS.Domain.Entities;
using HMS.Infrastructure.Data;

namespace HMS.Infrastructure.Presistence
{
    public class GuestRepository : RepositoryBase<Guest, ApplicationDbContext>, IGuestRepository
    {
        public GuestRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

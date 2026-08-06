using HMS.Application.Contracts.Presistence;
using HMS.Domain.Entities;
using HMS.Infrastructure.Data;

namespace HMS.Infrastructure.Presistence
{
    public class RoomRepository : RepositoryBase<Room, ApplicationDbContext>, IRoomRepository
    {
        public RoomRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

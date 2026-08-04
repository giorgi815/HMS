using HMS.Application.Contracts.Presistence;
using HMS.Domain.Entities;
using HMS.Infrastructure.Data;

namespace HMS.Infrastructure.Presistence
{
    public class ManagerRepository : RepositoryBase<Manager, ApplicationDbContext>, IManagerRepository
    {
        public ManagerRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

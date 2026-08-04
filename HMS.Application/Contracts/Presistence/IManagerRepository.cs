using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HMS.Application.Contracts.Presistence
{
    public interface IManagerRepository : IRepositoryBase<Manager, DbContext>
    {
    }
}

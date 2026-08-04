using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HMS.Application.Contracts.Presistence
{
    public interface IGuestRepository : IRepositoryBase<Guest, DbContext>
    {
    }
}

using HMS.Application.Models.Manager;
using HMS.Domain.Entities;

namespace HMS.Application.Contracts.Services
{
    public interface IManagerService
    {
        Task<int> CreateManagerAsync(Manager model);
        Task<int> DeleteManagerAsync(int id);
        Task<int> UpdateManagerAsync(ManagerForUpdatingDto model);
        Task<IEnumerable<ManagerForGettingDto>> GetManagerAsync();
    }
}


using HMS.Domain.Entities;

namespace HMS.Application.Contracts.Services
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}

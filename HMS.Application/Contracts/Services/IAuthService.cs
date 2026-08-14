
using HMS.Application.Models.Auth;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Primitives;

namespace HMS.Application.Contracts.Services
{
    public interface IAuthService
    {
        Task<string> RegisterManagerAsync(ManagerRegistrationRequestDto model);

        Task<string> RegisterAdminAsync(AdminRegistrationRequestDto model);

        Task<string> RegisterGuestAsync(GuestRegistrationRequestDto model);
        Task<LoginResponseDto> LoginAsync(LoginRequestDto model);


    }
}

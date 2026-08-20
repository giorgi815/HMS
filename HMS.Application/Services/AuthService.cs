using HMS.Application.Contracts.Services;
using HMS.Application.Exceptions;
using HMS.Application.Models.Auth;
using HMS.Domain.Entities;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace HMS.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IManagerService _managerService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IAdminService _adminService;
        private readonly IGuestService _guestService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        private const string _adminRole = "Admin";
        private const string _managerRole = "Manager";
        private const string _guestRole = "Guest";


        public AuthService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IManagerService managerService,
            IGuestService guestService,
            IJwtTokenGenerator jwtTokenGenerator,
            IMapper mapper,
            IConfiguration configuration,
            IAdminService adminService
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _managerService = managerService;
            _guestService = guestService;
            _jwtTokenGenerator = jwtTokenGenerator;
            _mapper = mapper;
            _configuration = configuration;
            _adminService = adminService;
        }




        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
                throw new NotFoundException("User not found.");

            if (!user.EmailConfirmed)
                throw new BadRequestException("Email is not confirmed.");

            if (await _userManager.IsLockedOutAsync(user))
                throw new BadRequestException("Your account is locked.");

            bool isValid = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!isValid)
            {
                await _userManager.AccessFailedAsync(user);
                throw new BadRequestException("Username or Password is Incorrect!");
            }
            else
            {
                await _userManager.ResetAccessFailedCountAsync(user);
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            return new LoginResponseDto() { AccessToken = token };

        }

        public async Task<string> RegisterAdminAsync(AdminRegistrationRequestDto model)
        {
            var user = _mapper.Map<ApplicationUser>(model);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new BadRequestException(result.Errors.First().Description);
            }

            await AddRoleAsync(user, _adminRole);

            var admin = new Admin
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PersonalNumber = model.PersonalNumber,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                ApplicationUserId = user.Id
            };

            await _adminService.CreateAdminAsync(admin);

            return user.Id;


        }

        public async Task<string> RegisterGuestAsync(GuestRegistrationRequestDto model)
        {
            var user = _mapper.Map<ApplicationUser>(model);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new BadRequestException(result.Errors.First().Description);
            }

            await AddRoleAsync(user, _guestRole);

            var guest = new Guest
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PersonalNumber = model.PersonalNumber,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                ApplicationId = user.Id
            };

            await _guestService.CreateGuestAsync(guest);

            return user.Id;

        }

        public Task<string> RegisterManagerAsync(ManagerRegistrationRequestDto model)
        {
            throw new NotImplementedException();
        }



        private async Task AddRoleAsync(
   ApplicationUser user,
   string role)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(
                    new IdentityRole(role));
            }

            await _userManager.AddToRoleAsync(user, role);
        }
    }
}

using HMS.Application.Contracts.Presistence;
using HMS.Application.Contracts.Services;
using HMS.Application.Exceptions;
using HMS.Application.Models.Manager;
using HMS.Domain.Entities;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;

namespace HMS.Application.Services
{
    public class ManagerService : IManagerService
    {

        private readonly IManagerRepository _managerRepoitory;
        private readonly IHotelService _hotelService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public ManagerService(
            IManagerRepository managerRepository,
            IHotelService hotelService,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _managerRepoitory = managerRepository;
            _hotelService = hotelService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<int> CreateManagerAsync(Manager model)
        {

            if (model is null)
                throw new BadRequestException("model is mepty");


            var hotel = await _hotelService.GetHotelByIdAsync(model.HotelId);

            if (hotel is null)
                throw new BadRequestException("Hotel wasn't found");

            var personalNumberTaken = await _managerRepoitory.ExistsAsync(m => m.PersonalNumber == model.PersonalNumber);
            if (personalNumberTaken)
                throw new BadRequestException($"Personal number {model.PersonalNumber} is already in use");

            var emailTaken = await _managerRepoitory.ExistsAsync(m => m.Email == model.Email);
            if (emailTaken)
                throw new BadRequestException($"Email {model.Email} is already in use");


            await _managerRepoitory.AddAsync(model);
            await _managerRepoitory.SaveAsync();

            return model.HotelId;
        }

        public async Task<int> DeleteManagerAsync(int id)
        {
            var manager = await _managerRepoitory.GetAsync(m => m.ManagerId == id);

            if (manager is null)
                throw new BadRequestException("Manager not found");

            var hasAnotherManager = await _managerRepoitory.ExistsAsync(h => h.HotelId == manager.HotelId && h.ManagerId != manager.ManagerId);

            if (!hasAnotherManager)
                throw new BadRequestException("Mannager can't be deleted, it must have at least on manager");

            var applicationUser = manager.ApplicationUser;

            _managerRepoitory.Remove(manager);

            if (applicationUser != null)
            {
                var result = await _userManager.DeleteAsync(applicationUser);

                if (!result.Succeeded)
                    throw new BadRequestException(result.Errors.FirstOrDefault().Description);

            }

            await _managerRepoitory.SaveAsync();

            return id;
        }

        public async Task<IEnumerable<ManagerForGettingDto>> GetManagerAsync()
        {
            var manager = await _managerRepoitory.GetAllAsync();

            return _mapper.Map<IEnumerable<ManagerForGettingDto>>(manager.Items);

        }

        public async Task<int> UpdateManagerAsync(ManagerForUpdatingDto model)
        {
            if (model is null)
                throw new BadRequestException("Manager id not found");

            var manager = await _managerRepoitory.GetAsync(m => m.ManagerId == model.Id);


            if (manager is null)
                throw new BadRequestException("Manager not found");


            _mapper.Map(model, manager);
            _managerRepoitory.Update(manager);
            await _managerRepoitory.SaveAsync();
            return manager.HotelId;
        }
    }
}

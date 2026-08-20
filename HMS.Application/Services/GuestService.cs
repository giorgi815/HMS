using HMS.Application.Contracts.Presistence;
using HMS.Application.Contracts.Services;
using HMS.Application.Exceptions;
using HMS.Application.Models.Guest;
using HMS.Domain.Entities;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HMS.Application.Services
{
    public class GuestService : IGuestService
    {

        private readonly IGuestRepository _guestRepository;
        private readonly IReservationRepository _reservationRepositoy;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public GuestService(
            IGuestRepository guestRepository,
            IReservationRepository reservationRepository,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _guestRepository = guestRepository;
            _reservationRepositoy = reservationRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<GuestForGettingDto> CreateGuestAsync(GuestForCreatingDto model)
        {
            if (model is null)
                throw new BadRequestException("Model is requred");
            
            if (!model.Email.Contains("@"))
                throw new BadRequestException("Invalid Email format");

            var personalNumberTaken = await _guestRepository.ExistsAsync(
            g => g.PersonalNumber == model.PersonalNumber);

            if (personalNumberTaken)
                throw new BadRequestException($"Personal number {model.PersonalNumber} is already in use");

            var phoneNumberTaken = await _guestRepository.ExistsAsync(
            g => g.PhoneNumber == model.PhoneNumber);

            if (phoneNumberTaken)
                throw new BadRequestException($"Phone number {model.PhoneNumber} is already in use");



            var mapp = _mapper.Map<Guest>(model);


            await _guestRepository.AddAsync(mapp);
            await _guestRepository.SaveAsync();

            return _mapper.Map<GuestForGettingDto>(mapp);
        }

        public async Task<int> DeleteGuestAsync(int id)
        {
            var Guest = await _guestRepository.GetAsync(g => g.GuestId == id, include: query => query.Include(x => x.ApplicationId));

            var hasActiveReservatios = await _reservationRepositoy.ExistsAsync(g => g.GuestId == id && g.CheckOutDate > DateTime.UtcNow);

            if (hasActiveReservatios)
                throw new BadRequestException("Guest can't be deleted while it has active or future reservation");

            if (Guest == null)
                throw new BadRequestException("Id not found");

            _guestRepository.Remove(Guest);
            await _guestRepository.SaveAsync();

            return id;
        }

        public async Task<GuestForGettingDto> UpdateGuestAsync(GuestForUpdatingDto model)
        {
            if (model == null)
                throw new BadRequestException("Model is empty");

            var guest = await _guestRepository.GetAsync(g => g.GuestId == model.GuestId);

            if (guest == null)
                throw new BadRequestException("Manager can't be found");

            _mapper.Map(model, guest);
            _guestRepository.Update(guest);
            await _guestRepository.SaveAsync();

            return _mapper.Map<GuestForGettingDto>(guest);
        }
    }
}

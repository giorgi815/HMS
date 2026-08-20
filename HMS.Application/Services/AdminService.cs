using HMS.Application.Contracts.Presistence;
using HMS.Application.Contracts.Services;
using HMS.Application.Exceptions;
using HMS.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HMS.Application.Services
{
    public class AdminService : IAdminService
    {

        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<int> CreateAdminAsync(Admin model)
        {
            if (model is null)
                throw new BadRequestException("Model is required");

            if(string.IsNullOrEmpty(model.FirstName))
                throw new BadRequestException("First name is required");

            if (model.FirstName.Length < 2 || model.FirstName.Length > 100)
                throw new BadRequestException("First name must be between 2 and 100 characters");

            if (string.IsNullOrEmpty(model.LastName))
                throw new BadRequestException("Last name is required");

            if (model.LastName.Length < 2 || model.LastName.Length > 100)
                throw new BadRequestException("Last name must be between 2 and 100 characters");

            if (string.IsNullOrEmpty(model.PersonalNumber))
                throw new BadRequestException("Personal number is required");

            if (model.PersonalNumber.Length != 11)
                throw new BadRequestException("Personal number must be 11 characters long");

            if (string.IsNullOrEmpty(model.Email))
                throw new BadRequestException("Email is required");
            if (!model.Email.Contains("@"))
                throw new BadRequestException("Invalid email format");

            if (string.IsNullOrEmpty(model.PhoneNumber))
                throw new BadRequestException("Phone number is required");
            if (model.PhoneNumber.Length != 9)
                throw new BadRequestException("Phone number must be 9 characters long");


            await _adminRepository.AddAsync(model);
            await _adminRepository.SaveAsync();

            return model.AdminId;
        }
    }
}

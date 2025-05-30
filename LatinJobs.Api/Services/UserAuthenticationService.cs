﻿using LatinJobs.Api.DTOs;
using LatinJobs.Api.Entities;
using LatinJobs.Api.Exceptions;
using LatinJobs.Api.Repositories.Interfaces;
using LatinJobs.Api.Services.Interfaces;
using LatinJobs.Api.Shared;
using LatinJobs.Api.ViewModels;

namespace LatinJobs.Api.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;

        public UserAuthenticationService(IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }

        public async Task<UserAuthenticationViewModel> CreateAsync(CreateUserAuthenticationDto createAuthenticationDto, CancellationToken cancel)
        {
            var foundUser = await _unitOfWork.UserRepository.FindOneByEmail(createAuthenticationDto.Email!, cancel);
            
            if (foundUser is null)
            {
                throw new NotFoundException($"User Not Found, Email = {createAuthenticationDto.Email}");
            }

            if (!string.Equals(foundUser.PasswordHash, Utils.GetSha256Hash(createAuthenticationDto.Password!)))
            { 
                throw new UnauthorizedException("Invalid Password");
            }

            var authentication = await _unitOfWork.UserAuthenticationRepository.CreateAsync(new UserAuthentication
            {
                UserId = foundUser.Id,
                Date = DateTime.UtcNow
            }, cancel);

            return new UserAuthenticationViewModel
            {
                Id = authentication.Id,
                UserId = authentication.UserId,
                Date = authentication.Date,
                Token = _jwtService.GetJwtToken(foundUser.Id, Constants.Jwt.TokenExpirationSeconds)
            };
        }

        public async Task<IEnumerable<UserAuthenticatedDto>> FindAllAsync(CancellationToken cancel)
        {
            var authentications = await _unitOfWork.UserAuthenticationRepository.FindAllAsync(cancel);
            return authentications.Select(auth => new UserAuthenticatedDto 
            {
                AuthenticationId = auth.Id,
                UserId = auth.UserId,
                Date = auth.Date
            });
        }

        public async Task<IEnumerable<UserAuthenticatedDto>> FindByUserIdAsync(int userId, CancellationToken cancel)
        {
            var authentication = await _unitOfWork.UserAuthenticationRepository.FindByUserIdAsync(userId, cancel);
            return authentication.Select(auth => new UserAuthenticatedDto
            {
                AuthenticationId = auth.Id,
                UserId = auth.UserId,
                Date = auth.Date
            });
        }

        public async Task<UserAuthenticatedDto?> RemoveAsync(int id, CancellationToken cancel)
        {
            var removedAuthentication = await _unitOfWork.UserAuthenticationRepository.RemoveAsync(id, cancel);
            if (removedAuthentication is null)
            {
                throw new NotFoundException($"Record Not Found");
            }

            return new UserAuthenticatedDto
            {
                AuthenticationId = removedAuthentication.Id,
                UserId = removedAuthentication.UserId,
                Date = removedAuthentication.Date
            };
        }
    }
}

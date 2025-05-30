﻿using LatinJobs.Api.DTOs;
using LatinJobs.Api.Entities;
using LatinJobs.Api.Pagination;
using LatinJobs.Api.Repositories.Interfaces;
using LatinJobs.Api.ViewModels;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace LatinJobs.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User user, CancellationToken cancel)
        {
            var newUser = await _context.AddAsync(user, cancel);
            await _context.SaveChangesAsync(cancel);
            return newUser.Entity;
        }

        public async Task<PagedResult<UserViewModel>> FindAllAsync(PaginationParametersDto paginationParametersDto,CancellationToken cancel)
        {
            int totalCount = await _context.Users.CountAsync(cancel);

            var users = await _context.Users
                .AsNoTracking()
                .Skip((paginationParametersDto.PageNumber - 1)
                       * paginationParametersDto.PageSize)
                .Take(paginationParametersDto.PageSize)
                .ToListAsync(cancel);

            return new PagedResult<UserViewModel>
            {
                TotalCount = totalCount,
                Items = users.Adapt<IEnumerable<UserViewModel>>()
            };
        }

        public async Task<User?> FindOneAsync(int id, CancellationToken cancel)
        {
            return await _context.Users
                .Where(u => u.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancel);
        }

        public async Task<User?> FindOneByEmail(string email, CancellationToken cancel)
        {
            return await _context.Users
                .Where(u => string.Equals(u.Email, email))
                .AsNoTracking()
                .FirstOrDefaultAsync(cancel);
        }

        public async Task<User?> UpdateAsync(User user, CancellationToken cancel)
        {
            var existingUser = await _context.Users
                .Where(u => u.Id == user.Id)
                .FirstOrDefaultAsync(cancel);

            if (existingUser is null)
            {
                return null;
            }

            existingUser.Email = user.Email;
            existingUser.PasswordHash = user.PasswordHash;
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Updated = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancel);
            return existingUser;
        }

        public async Task<User?> SoftDelete(int id, CancellationToken cancel)
        {
            var existingUser = await _context.Users
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync(cancel);

            if (existingUser is null)
            {
                return null;
            }

            existingUser.IsDeleted = true;
            existingUser.Deleted = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancel);
            return existingUser;
        }

        public async Task<User?> RemoveAsync(int id, CancellationToken cancel)
        {
            var foundUser = await _context.Users
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync(cancel);

            if (foundUser is null)
            {
                return null;
            }
            
            _context.Users.Remove(foundUser);
            await _context.SaveChangesAsync(cancel);
            return foundUser;
        }
    }
}

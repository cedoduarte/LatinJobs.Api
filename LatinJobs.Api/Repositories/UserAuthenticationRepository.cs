using LatinJobs.Api.Entities;
using LatinJobs.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LatinJobs.Api.Repositories
{
    public class UserAuthenticationRepository : IUserAuthenticationRepository
    {
        private readonly AppDbContext _context;

        public UserAuthenticationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserAuthentication> CreateAsync(UserAuthentication authentication, CancellationToken cancel)
        {
            var newAuthentication = await _context.AddAsync(authentication, cancel);
            await _context.SaveChangesAsync(cancel);
            return newAuthentication.Entity;
        }

        public async Task<IEnumerable<UserAuthentication>> FindAllAsync(CancellationToken cancel)
        {
            return await _context.UserAuthentications
                .AsNoTracking()
                .ToListAsync(cancel);
        }

        public async Task<IEnumerable<UserAuthentication>> FindByUserIdAsync(int userId, CancellationToken cancel)
        {
            return await _context.UserAuthentications
                .Where(a => a.UserId == userId)
                .AsNoTracking()
                .ToListAsync(cancel);
        }

        public async Task<UserAuthentication?> RemoveAsync(int id, CancellationToken cancel)
        {
            var foundAuthentication = await _context.UserAuthentications
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync(cancel);

            if (foundAuthentication is null)
            {
                return null;
            }

            _context.UserAuthentications.Remove(foundAuthentication);
            await _context.SaveChangesAsync(cancel);
            return foundAuthentication;
        }
    }
}

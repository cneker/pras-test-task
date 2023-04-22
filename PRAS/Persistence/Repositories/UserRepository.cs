using Microsoft.EntityFrameworkCore;
using PRAS.Contracts.Repositories;
using PRAS.Models;

namespace PRAS.Persistence.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task CreateUserAsync(User user) =>
            await CreateAsync(user);

        public async Task<User> GetUserAsync(Guid userId) =>
            await GetByCondition(u => u.Id == userId, false).SingleOrDefaultAsync();

        public async Task<User> GetUserByEmailAsync(string email) =>
            await GetByCondition(u => u.Email.Equals(email), false).Include(u => u.Role)
                .SingleOrDefaultAsync();
    }
}

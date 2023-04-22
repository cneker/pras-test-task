using PRAS.Models;

namespace PRAS.Contracts.Repositories
{
    public interface IUserRepository
    {
        Task CreateUserAsync(User user);
        Task<User> GetUserAsync(Guid userId);
        Task<User> GetUserByEmailAsync(string email);
    }
}

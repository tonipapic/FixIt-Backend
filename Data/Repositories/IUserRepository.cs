using FixIt_Backend.Data.Models;

namespace FixIt_Backend.Data.Repositories
{
    public interface IUserRepository
    {
        public Task<User?> CreateNewUserAsync(User user);
        public Task<User?> GetUserById(int userId);
    }
}

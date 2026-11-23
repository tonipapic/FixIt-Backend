using FixIt_Backend.Data.Models.Database;

namespace FixIt_Backend.Data.Repositories.UserRepo
{
    public interface IUserRepository
    {
        public Task<User?> CreateNewUserAsync(User user);
        public Task<User?> GetUserByEmail(string email);
        public Task<User?> GetUserById(int userId);
    }
}

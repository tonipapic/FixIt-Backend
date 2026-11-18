using FixIt_Backend.Data.Models.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace FixIt_Backend.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FixItContext _fixItContext;
        public UserRepository(FixItContext fixItContext)
        {
            _fixItContext = fixItContext;
        }
        public async Task<User?> CreateNewUserAsync(User user)
        {
            await _fixItContext.Users.AddAsync(user);

            int res = await _fixItContext.SaveChangesAsync();

            if(res > 0)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _fixItContext.Users.FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task<User?> GetUserById(int userId)
        {
            return await _fixItContext.Users.Include(p => p.Role).FirstOrDefaultAsync(p => p.Id == userId);
        }
    }
}

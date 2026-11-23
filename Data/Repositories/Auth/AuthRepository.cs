using FixIt_Backend.Data.Models.Database;
using FixItShared.Data.DTOs.Responses;
using Microsoft.EntityFrameworkCore;

namespace FixIt_Backend.Data.Repositories.Auth
{
    public class AuthRepository:IAuthRepository
    {
        private readonly FixItContext _dbContext;
        public AuthRepository(FixItContext dbContextFactory)
        {
            _dbContext = dbContextFactory;
        }


        public async Task<UserLocalAuth?> CreateNewUserAsync(UserLocalAuth user)
        {
            await _dbContext.UserLocalAuths.AddAsync(user);
            int res = await _dbContext.SaveChangesAsync();

            if (res > 0)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public async Task<UserLocalAuth?> GetUserByUsernameAsync(string username)
        {
            return await _dbContext.UserLocalAuths.FirstOrDefaultAsync(p => p.Username == username);

        }
    }
}

using FixIt_Backend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FixIt_Backend.Data.Repositories
{
    public class AuthRepository:IAuthRepository
    {
        private readonly IDbContextFactory<FixItContext> _dbContextFactory;
        public AuthRepository(IDbContextFactory<FixItContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Task<User> CreateNewUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}

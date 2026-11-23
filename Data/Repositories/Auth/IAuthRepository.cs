using FixIt_Backend.Data.Models.Database;
using FixItShared.Data.DTOs.Responses;

namespace FixIt_Backend.Data.Repositories.Auth
{
    public interface IAuthRepository
    {
       public Task<UserLocalAuth> CreateNewUserAsync(UserLocalAuth user);
        public Task<UserLocalAuth?> GetUserByUsernameAsync(string username);
    }
}
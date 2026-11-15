using FixIt_Backend.Data.Models;
using FixItShared.Data.DTOs.Responses;

namespace FixIt_Backend.Data.Repositories
{
    public interface IAuthRepository
    {
       public Task<UserResponseDTO> CreateNewUserAsync(UserLocalAuth user);
        public Task<UserLocalAuth?> GetUserByUsernameAsync(string username);
    }
}
using FixIt_Backend.Data.Models;
using FixIt_Backend.Data.Repositories;
using FixItShared.Data.DTOs.Requests.Auth;
using FixItShared.Data.DTOs.Responses;
using FixItShared.Data.Enum;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Web.Helpers;

namespace FixIt_Backend.Data.Services
{
    public class AuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IUserRepository _userRepository;


        public AuthService(IAuthRepository authRepository, IUserRepository userRepository) 
        {
            _authRepository = authRepository;
            _userRepository = userRepository;
        
        }

        public async Task<UserResponseDTO?> RegisterNewUserAsync(RegisterRequestDTO? registerRequestDTO)
        {
            if (registerRequestDTO == null) 
            {
                return null;
            }

            var user = new User()
            {
                Email = registerRequestDTO.Email,
                Name = registerRequestDTO.Name,
                Surname = registerRequestDTO.Surname,
                RoleId = (int)RoleEnum.Reporter
            };

            var newUserEntity = await _userRepository.CreateNewUserAsync(user);

            if(newUserEntity == null)
            {
                return null;
            }

            var userLocalAuth = new UserLocalAuth()
            {
                Username = registerRequestDTO.Username,
                Salt = Crypto.GenerateSalt(32),
                UserId = newUserEntity.Id,
            };
            userLocalAuth.Password = new PasswordHasher<User>().HashPassword(newUserEntity,registerRequestDTO.Password+userLocalAuth.Salt);

            return await _authRepository.CreateNewUserAsync(userLocalAuth);
        }

        public async Task<UserResponseDTO?> LoginUserAsync(LoginRequestDTO? loginRequestDTO)
        {
            if(loginRequestDTO == null)
            {
                return null;
            }

            var userAuth = await _authRepository.GetUserByUsernameAsync(loginRequestDTO.Username);
            if (userAuth == null)
            {
                return null;
            }

            var user = await _userRepository.GetUserById(userAuth.UserId);
            
            if(user == null)
            {
                return null;
            }

            var passwordVerificationResult = new PasswordHasher<User>().VerifyHashedPassword(user, userAuth.Password, loginRequestDTO.Password + userAuth.Salt);

            if(passwordVerificationResult != PasswordVerificationResult.Failed)
            {
                return new UserResponseDTO()
                {
                    Id = user.Id,
                    Name = user.Name,
                    RoleId = user.RoleId,
                    Surname = user.Surname
                };
            }
            else
            {
                return null;
            }
        }
    }
}

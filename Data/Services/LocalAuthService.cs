using FixIt_Backend.Data.Models;
using FixIt_Backend.Data.Models.Database;
using FixIt_Backend.Data.Repositories.Auth;
using FixIt_Backend.Data.Repositories.UserRepo;
using FixIt_Backend.Data.UnitOfWork;
using FixIt_Backend.Extensions.Exceptions;
using FixItShared.Data.DTOs.Requests.Auth;
using FixItShared.Data.DTOs.Responses;
using FixItShared.Data.Enum;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Web.Helpers;

namespace FixIt_Backend.Data.Services
{
    public class LocalAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JWTService _jwtService;


        public LocalAuthService(IAuthRepository authRepository, IUserRepository userRepository, IUnitOfWork unitOfWork, JWTService jWTService)
        {
            _authRepository = authRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _jwtService = jWTService;

        }

        public async Task<UserResponseDTO?> RegisterNewUserAsync(RegisterRequestDTO? registerRequestDTO)
        {
            if (registerRequestDTO == null)
            {
                return null;
            }

            if (await UserExists(registerRequestDTO))
            {
                throw new UserAlreadyExistsException("User already exists");
            }

            await _unitOfWork.BeginTransaction();

            var newUserEntity = await CreateNewUser(registerRequestDTO);

            if (newUserEntity == null)
            {
                await _unitOfWork.RollbackTransaction();

                return null;
            }

            var userLoaclAuth = await CreateLocalAuthForUser(registerRequestDTO, newUserEntity);

            if (userLoaclAuth != null && userLoaclAuth.Id != 0)
            {
                await _unitOfWork.CommitTransaction();

                return new UserResponseDTO()
                {
                    Id = newUserEntity.Id,
                    Name = newUserEntity.Name,
                    Surname = newUserEntity.Surname,
                    RoleId = newUserEntity.RoleId
                };
            }
            else
            {
                await _unitOfWork.RollbackTransaction();

                return null;
            }


        }

        private async Task<bool> UserExists(RegisterRequestDTO registerRequestDTO)
        {
            return (await _authRepository.GetUserByUsernameAsync(registerRequestDTO.Username)) != null || (await _userRepository.GetUserByEmail(registerRequestDTO.Email)) != null;
        }

        public async Task<UserResponseDTO?> LoginUserAsync(LoginRequestDTO? loginRequestDTO)
        {
            if (loginRequestDTO == null)
            {
                return null;
            }

            var userAuth = await _authRepository.GetUserByUsernameAsync(loginRequestDTO.Username);
            if (userAuth == null)
            {
                return null;
            }

            var user = await _userRepository.GetUserById(userAuth.UserId);

            if (user == null)
            {
                return null;
            }

            var passwordVerificationResult = new PasswordHasher<User>().VerifyHashedPassword(user, userAuth.Password, loginRequestDTO.Password + userAuth.Salt);

            if (passwordVerificationResult != PasswordVerificationResult.Failed)
            {


                return new UserResponseDTO()
                {
                    Id = user.Id,
                    Name = user.Name,
                    RoleId = user.RoleId,
                    Surname = user.Surname,
                    JWTToken = _jwtService.GenerateJWTToken(user, userAuth.Username)
                };
            }
            else
            {
                return null;
            }
        }

        private async Task<User?> CreateNewUser(RegisterRequestDTO registerRequestDTO)
        {
            var user = new User()
            {
                Email = registerRequestDTO.Email,
                Name = registerRequestDTO.Name,
                Surname = registerRequestDTO.Surname,
                RoleId = (int)RoleEnum.Reporter
            };

            var newUserEntity = await _userRepository.CreateNewUserAsync(user);
            return newUserEntity;
        }

        private async Task<UserLocalAuth?> CreateLocalAuthForUser(RegisterRequestDTO registerRequestDTO, User newUserEntity)
        {
            var salt = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
            var userLocalAuth = new UserLocalAuth()
            {
                Username = registerRequestDTO.Username,
                Salt = salt,
                UserId = newUserEntity.Id,
                Password = new PasswordHasher<User>().HashPassword(newUserEntity, registerRequestDTO.Password + salt)
            };

            var userLoaclAuth = await _authRepository.CreateNewUserAsync(userLocalAuth);
            return userLoaclAuth;
        }
    }
}

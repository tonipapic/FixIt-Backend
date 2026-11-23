using FixIt_Backend.Data.Models.Database;
using FixIt_Backend.Data.Services;
using FixIt_Backend.Extensions.Exceptions;
using FixItShared.Data.DTOs.Requests.Auth;
using FixItShared.Data.DTOs.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FixIt_Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly LocalAuthService _authService;
        public AuthController(LocalAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserResponseDTO>> Login(LoginRequestDTO loginRequestDTO)
        {
            try
            {
                var userResponse = await _authService.LoginUserAsync(loginRequestDTO);
                if (userResponse != null)
                {
                    return Ok(userResponse);
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponseDTO>> Register(RegisterRequestDTO registerRequest)
        {
            try
            {
                var userResponse = await _authService.RegisterNewUserAsync(registerRequest);
                if (userResponse != null)
                {
                    return Ok(userResponse);
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (UserAlreadyExistsException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

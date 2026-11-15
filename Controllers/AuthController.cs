
using FixIt_Backend.Data.Models;
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
        private readonly IDbContextFactory<FixItContext> _dbContextFactory;
        public AuthController(IDbContextFactory<FixItContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        [HttpGet("details/{id}")]
        public ActionResult<UserResponseDTO> GetUser(int? id)
        {
            
        }

        [HttpPost("register")]
        public ActionResult<UserResponseDTO> Register(RegisterRequestDTO registerRequest)
        {


            return Ok();
        } 
    }
}

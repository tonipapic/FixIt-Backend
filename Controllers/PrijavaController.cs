using FixIt_Backend.Data.Services;
using FixItShared.Data.DTOs.Responses.Prijava;
using FixItShared.Data.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace FixIt_Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PrijavaController : ControllerBase
    {
        private readonly PrijavaService _prijavaService;
        public PrijavaController(PrijavaService prijavaService)
        {
            _prijavaService = prijavaService;
        }

        [HttpGet("userreports/{id}")]
        [Authorize]
        public async Task<ActionResult<string>> GetUsersPrijave(int? id)
        {
            if(id is null || id == 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var prijaveJSON = await _prijavaService.GetPrijaveByReporterId(id);
            if(prijaveJSON != null && prijaveJSON.Any())
            {
                return Ok(prijaveJSON);

            }
            return NoContent();
        }
        [HttpPost("userreports")]
        [Authorize]
        public async Task<ActionResult<string>> CreateUsersPrijave(PrijavaRequestDTO newPrijavaRequestDTO)
        {
            
            if(newPrijavaRequestDTO is null)
            {
                throw new ArgumentNullException();
            }

            var prijaveJSON = await _prijavaService.AddNewPrijave(newPrijavaRequestDTO);
            if (prijaveJSON != null)
            {
                return Ok(prijaveJSON);

            }
            return NoContent();
        }

        [HttpGet("allreports")]
        [Authorize(Roles = $"{nameof(RoleEnum.Admin)}")]
        public async Task<ActionResult<string>> GetAllPrijave()
        {
            var prijaveJSON = await _prijavaService.GetAllPrijave();
            if(prijaveJSON != null && prijaveJSON.Any())
            {
                return Ok(prijaveJSON);

            }
            return NoContent();
        }
    
        [HttpGet("allunsetreports")]
        [Authorize(Roles = $"{nameof(RoleEnum.Admin)}")]
        public async Task<ActionResult<string>> GetAllUnsetPrijave()
        {
            

            var prijaveJSON = await _prijavaService.GetAllPrijave();
            if(prijaveJSON != null && prijaveJSON.Any())
            {
                return Ok(prijaveJSON);

            }
            return NotFound();
        }
    }
}

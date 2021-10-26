using Gestion_Recommandation.API.Services;
using Gestion_Recommandation.Shared.Models;
using Gestion_Recommandation.Shared.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestion_Recommandation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RecommandationsController : ControllerBase
    {
        private readonly IRecommandationService _recommandationService;
        public RecommandationsController(IRecommandationService recommandationService)
        {
            _recommandationService = recommandationService;
        }

        //api/AllRecommandations?query={query}&userId={userId}&pageNumber={pageNumber}&pageSize={pageSize}
        [HttpGet("AllRecommandations")]
        public async Task<IActionResult> GetAsync(string query, string userId, int pageNumber, int pageSize)
        {
            var result = await _recommandationService.GetRecommandationsAsync(query, userId, pageNumber, pageSize);
            if (result != null)
                return Ok(result);
            else
                return BadRequest(result);
        }

        //api/Recommandations?id={id}&userId={userId}
        [HttpGet()]
        public async Task<IActionResult> Get(int id, string userId)
        {
            var result = await _recommandationService.GetByIdAsync(id, userId);
            if (result != null)
                return Ok(result);
            else
                return BadRequest(result);
        }

        //api/Recommandations
        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] Recommandations model)
        {
            var result = await _recommandationService.CreateAsync(model);
            return Ok(result);
        }

        //api/Recommandations
        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] Recommandations model)
        {
            var result = await _recommandationService.EditAsync(model);
            return Ok(result);
        }
    }
}

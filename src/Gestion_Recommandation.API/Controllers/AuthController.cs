using Gestion_Recommandation.API.Services;
using Gestion_Recommandation.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gestion_Recommandation.API.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase 
    { 
        private IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        //api/auth/Register
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterRequest model)
        {
            if(ModelState.IsValid)
            { 
                var result = await _authService.RegisterAsync(model);
                if (result.IsSuccess)
                    return Ok(result); //200

                return BadRequest(result); //400 
            }
            return BadRequest("Some properties are not valid"); // 400
        }

        //api/auth/Register
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.LoginAsync(model);
                if (result.IsSuccess)
                    return Ok(result); //200

                return BadRequest(result); //400 
            }
            return BadRequest("Some properties are not valid"); // 400
        }
    }
}

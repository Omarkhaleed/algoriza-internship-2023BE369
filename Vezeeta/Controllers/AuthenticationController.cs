using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesLayer.DTOs.Authentication.Requests;
using ServicesLayer.Interfaces;
using Vezeeta.Filters;

namespace Vezeeta.Controllers
{
    [Route("api/Authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserAuthentication _userAuthentication;
        
        public AuthenticationController(IUserAuthentication userAuthentication)
        {
            _userAuthentication = userAuthentication;
        }

        [HttpPost("Login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var result = await _userAuthentication.LoginUserAsync( model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);
            //throw new Exception("Exception while Login.");
            return Ok(result);
        }

        [HttpPost("Register")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequestDto model)
        {
            var result = await _userAuthentication.RegisterUserAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

    }
}

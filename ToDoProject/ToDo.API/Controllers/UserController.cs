using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using ToDo.API.Infrastructure.Authentication;
using ToDo.App.Users;
using ToDo.App.Users.Requests;
using ToDo.API.Infrastructure.ActionResults;

namespace ToDo.API.Controllers
{

    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IOptions<JWTConfiguration> _options;

        public UserController(IUserService userService, IOptions<JWTConfiguration> options)
        {
            _userService = userService;
            _options = options;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] UserRegisterRequestModel user, CancellationToken token)
        {
            int res = await _userService.Register(user, token).ConfigureAwait(false);
            return Ok(res);
        }

        // GET: api/<UserController>
        [HttpPost("access-token")]
        public async Task<ActionResult<string>> Get([FromBody] UserLoginRequestModel user, CancellationToken token)
        {
            var responseModel = await _userService.LogIn(user, token).ConfigureAwait(false);
            var result = JWTHelper.GenerateSecurityToken(responseModel.Username, responseModel.Id, _options);
            return Ok(result);
        }


    }


}

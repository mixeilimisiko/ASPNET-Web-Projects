using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.Claims;
using ToDo.API.Infrastructure.ActionResults;
using ToDo.App.Exceptions;
using ToDo.App.Subtasks;
using ToDo.App.Subtasks.Requests;
using ToDo.App.Subtasks.Responses;

namespace ToDo.API.Controllers
{

    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    [ApiController]
    public class SubtasksController : ControllerBase
    {
        private readonly ISubtaskService _service;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SubtasksController(ISubtaskService service, IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
        }



        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] SubtaskRequestModel subtask, CancellationToken token)
        {
            var userId = GetUserId();
            await _service.CreateSubtaskAsync(subtask, userId, token).ConfigureAwait(false);
            return new Created();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubtaskResponseModel>> GetAsync(int id, CancellationToken token)
        {
            var userId = GetUserId();
            var result = await _service.ReadSubtaskAsync(id, userId, token).ConfigureAwait(false);
            
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id, CancellationToken token)
        {
            var userId = GetUserId();
            await _service.SoftDeleteSubtaskAsync(id, userId, token).ConfigureAwait(false);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] SubtaskUpdateRequestModel subtask, CancellationToken token)
        {
            var userId = GetUserId();
            await _service.UpdateAsync(id, userId, subtask, token).ConfigureAwait(false);

            return Ok();
        }

        private int GetUserId()
        {
            var identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var userIdClaim = identity?.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                throw new UnauthorizedError("Not Authorized Properly");
            }
            return int.Parse(userIdClaim.Value);
        }
    }
}

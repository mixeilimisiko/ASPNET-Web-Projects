using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using ToDo.API.Infrastructure.ActionResults;
using ToDo.App.Exceptions;
using ToDo.App.Todos;
using ToDo.App.Todos.Requests;
using ToDo.App.Todos.Responses;
using ToDo.Domain.Enums;
using ToDo.Infrastructure.Todos;

namespace ToDo.API.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class TodosController : ControllerBase
    {

        private readonly ITodoService _todoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TodosController(ITodoService service, IHttpContextAccessor httpContextAccessor)
        {
            _todoService = service;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] TodoCreateRequestModel todo, CancellationToken token)
        {
            var userId = GetUserId();
            await _todoService.CreateTodoAsync(todo, userId, token).ConfigureAwait(false);
            return new Created();
        }

        [HttpGet]
        public async Task<ActionResult<List<TodoResponseModel>>> GetAllUserTodos(CancellationToken token, [FromQuery] Statuses? status = null)
        {
            var userId = GetUserId();

            var todos = await _todoService.ReadUserTodosAsync(userId, status, token).ConfigureAwait(false);

            return Ok(todos.ToList()); // Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoResponseModel>> GetTodoById(int id, CancellationToken token)
        {
            var userId = GetUserId();

            var result = await _todoService.ReadUserTodoAsync(id, userId, token).ConfigureAwait(false);
            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTodo(int id, [FromBody] TodoUpdateRequestModel todoUpdateModel, CancellationToken token)
        {

            var userId = GetUserId();

            await _todoService.UpdateTodoAsync(id, userId, todoUpdateModel, token).ConfigureAwait(false);

            return Ok();
        }


        [HttpPost("{id}/done")]
        public async Task<ActionResult> MarkTodoAsDone(int id, CancellationToken token)
        {
            var userId = GetUserId();
            await _todoService.MarkTodoAsDoneAsync(id, userId, token).ConfigureAwait(false);

            return Ok();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTodo(int id, CancellationToken token)
        {

            var userId = GetUserId();
            await _todoService.SoftDeleteTodoAsync(id, userId, token).ConfigureAwait(false);

            return NoContent();
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

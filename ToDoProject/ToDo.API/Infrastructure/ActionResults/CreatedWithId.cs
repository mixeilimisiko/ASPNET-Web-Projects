using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ToDo.API.Infrastructure.ActionResults
{


    public class CreatedWithId : ActionResult
    {
        private readonly string _id;

        public CreatedWithId(string id)
        {
            _id = id;
        }

        public override async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(new { Id = _id })
            {
                StatusCode = StatusCodes.Status201Created
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }




}

using Microsoft.AspNetCore.Mvc;

namespace ToDo.API.Infrastructure.ActionResults
{
    public class Created : ActionResult
    {


        public override async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(null)
            {
                StatusCode = StatusCodes.Status201Created
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}

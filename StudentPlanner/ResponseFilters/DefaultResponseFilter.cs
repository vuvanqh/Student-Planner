using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.OpenApi;
using StudentPlanner.Core.Errors;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace StudentPlanner.UI;

public class DefaultResponseFilter: IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Responses.TryAdd("200", new OpenApiResponse { Description = "Success" });
        operation.Responses.TryAdd("400", new OpenApiResponse { Description = "Bad Request" });
        operation.Responses.TryAdd("401", new OpenApiResponse { Description = "Unauthorized" });
        operation.Responses.TryAdd("403", new OpenApiResponse { Description = "Forbidden" });
        operation.Responses.TryAdd("500", new OpenApiResponse { Description = "Internal Server Error" });
    }
}

//public class IdentityExceptionFilter : IExceptionFilter
//{
//    public void OnException(ExceptionContext context)
//    {
//        if (context.Exception is IdentityOperationException)
//        {
//            context.Result = new ConflictObjectResult(
//                new ProblemDetails
//                {
//                    Title = "Email already exists",
//                    Status = StatusCodes.Status409Conflict
//                });

//            context.ExceptionHandled = true;
//        }
//    }
//}

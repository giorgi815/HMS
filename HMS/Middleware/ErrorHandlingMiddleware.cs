using HMS.Application.Exceptions;
using System.Text.Json;

namespace HMS.Middleware
{
    public class ErrorHandlingMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var statusCode = ex switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                NotAllowedException => StatusCodes.Status405MethodNotAllowed,
                _ => StatusCodes.Status500InternalServerError
            };

            var response = new CommonResponse(ex.Message, null, false, statusCode);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });


            await context.Response.WriteAsync(json);
        }
    }
}

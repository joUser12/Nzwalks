using System.Net;

namespace NZWalks.API.Middlewares
{
    public class ExceptionHandleMiddlewares
    {

        private readonly  ILogger<ExceptionHandleMiddlewares> logger;
        private readonly RequestDelegate next;
        public ExceptionHandleMiddlewares(ILogger<ExceptionHandleMiddlewares> logger,RequestDelegate next)
        {

            this.logger = logger;
            this.next = next;
            
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {

            try
            {
                await next(httpContext);

            }
            catch (Exception ex)
            {

                var errorId = Guid.NewGuid();

                //log this 4exception

                logger.LogError(ex ,$"{errorId}:{ex.Message}");


                // Return A custom Error Resonse

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

               httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id= errorId,
                    ErrorMessage ="Some thing went wrong! We are loooking into resolving this"

                };

                await httpContext.Response.WriteAsJsonAsync(error);



            }

        }
    }
}

using Microsoft.AspNetCore.Mvc.Filters;
using MongoDB.Driver;
using System.Net;

namespace AppPurchases.Shared.Exceptions
{
    public class UnhandledExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            // TODO: Implementar fluxo que loga os erros

            if (context.Exception is HttpRequestException)
                ExceptionHandler(HttpStatusCode.UnprocessableEntity, context);

            if (context.Exception is MongoException)
                ExceptionHandler(HttpStatusCode.InternalServerError, context);

            ExceptionHandler(HttpStatusCode.InternalServerError, context);
        }

        private void ExceptionHandler(HttpStatusCode statusCode, ExceptionContext context)
        {
            base.OnException(context);
        }
    }
}

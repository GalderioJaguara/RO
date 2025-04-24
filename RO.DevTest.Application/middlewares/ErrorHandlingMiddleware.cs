using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RO.DevTest.Domain.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Middlewares {
    public class ErrorHandlingMiddleware {

        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next) {
            this.next = next;
        }

        public async Task Invoke(HttpContext context) {
            try {
                await next(context);
            } catch (Exception exception) {
                await HandleExceptionAsync(context, exception);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception) {
            var code = HttpStatusCode.InternalServerError;

            if (exception is ApiException apiException) {
                context.Response.StatusCode = (int)apiException.StatusCode;
                context.Response.ContentType = "application/json";

                var result = JsonConvert.SerializeObject(new {
                    errors = apiException.Errors
                });
                return context.Response.WriteAsync(result);
            }

            var genericResult = JsonConvert.SerializeObject(new {
                errors = new[] { "An unexpected error occurred." }
            });
            context.Response.StatusCode = (int)code;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(genericResult);
        }
    }
}

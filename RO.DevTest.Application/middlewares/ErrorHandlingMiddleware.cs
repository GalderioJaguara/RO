using Microsoft.AspNetCore.Http;


namespace RO.DevTest.Application.Middlewares;

public abstract class ErrorHandlingMiddleware {
    private readonly RequestDelegate next;

}
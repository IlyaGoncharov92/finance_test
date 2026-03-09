using System.Security.Claims;
using Auth.Application.Todos.Commands;
using Auth.Application.Todos.Queries;
using MediatR;

namespace Auth.API.Endpoints;

internal sealed class RegisterUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/auth/register", async (
            RegisterUserCommand command,
            IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return result.IsSuccess 
                ? Results.Ok(result.Value) 
                : Results.BadRequest(result.Error);
        });
    }
}

internal sealed class LoginUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/auth/login", async (
            LoginUserQuery query,
            IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return result.IsSuccess 
                ? Results.Ok(new { token = result.Value }) 
                : Results.Unauthorized();
        });
    }
}

internal sealed class TestEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/test", (ClaimsPrincipal user, IMediator mediator) =>
        {
            var userId = Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);
            
            return 123;
        })
        .RequireAuthorization();;
    }
}

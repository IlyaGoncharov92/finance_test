using Auth.Application.Todos.Commands;
using MediatR;

namespace Auth.API.Endpoints;

internal sealed class RegisterUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("register", async (
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

using Auth.API.Endpoints;
using Auth.Application.Todos.Queries;
using MediatR;

internal sealed class LoginUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("login", async (
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


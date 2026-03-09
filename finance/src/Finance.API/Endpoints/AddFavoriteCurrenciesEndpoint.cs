using System.Security.Claims;
using Finance.Application.Todos.Commands;
using MediatR;

namespace Finance.API.Endpoints;

internal sealed class AddFavoriteCurrencyEndpoint : IEndpoint
{
    public sealed class Request
    {
        public Guid[] CurrencyIds { get; set; } = [];
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("favorites", async (
            Request request,
            ClaimsPrincipal user,
            IMediator mediator) =>
        {
            var userId = user.GetUserId();

            var command = new AddFavoriteCurrenciesCommand(userId, request.CurrencyIds);

            await mediator.Send(command);

            return Results.Ok();
        });
        // .RequireAuthorization();
    }
}
